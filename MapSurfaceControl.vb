Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports WorldEditorNet.WorldEditorNet.Core
Imports System.Collections.Generic

Namespace WorldEditorNet
    Public Class MapSurfaceControl
        Inherits Control

        Private _document As MapV1Document
        Private _cellSize As Integer = 24
        Private _selectedX As Integer
        Private _selectedY As Integer
        Private _showLayer1 As Boolean = True
        Private _showLayer2 As Boolean = True
        Private _showLayer3 As Boolean = True
        Private _showLayer4 As Boolean = True
        Private _showGrid As Boolean
        Private _showBlocked As Boolean
        Private _showNpcs As Boolean
        Private _showObjects As Boolean
        Private _showTriggers As Boolean
        Private _showTranslados As Boolean
        Private _graphicsIndex As GraficosIndReader
        Private _graphicsDirectory As String = String.Empty
        Private ReadOnly _bitmapCache As New Dictionary(Of Integer, Bitmap)()
        Private ReadOnly _animationStates As New Dictionary(Of Integer, AnimationState)()
        Private ReadOnly _animationTimer As New Timer With {.Interval = 16}

        Private NotInheritable Class AnimationState
            Public Property GrhIndex As Integer
            Public Property FrameNumber As Integer = 1
            Public Property SpeedCounter As Integer
        End Class

        Public Event CellSelected As EventHandler
        Public Event CellEdited As EventHandler

        Public Property Document As MapV1Document
            Get
                Return _document
            End Get
            Set(value As MapV1Document)
                _document = value
                Invalidate()
            End Set
        End Property

        Public Property CellSize As Integer
            Get
                Return _cellSize
            End Get
            Set(value As Integer)
                _cellSize = Math.Max(8, value)
                UpdateSurfaceSize()
                Invalidate()
            End Set
        End Property

        Public ReadOnly Property SelectedX As Integer
            Get
                Return _selectedX
            End Get
        End Property

        Public ReadOnly Property SelectedY As Integer
            Get
                Return _selectedY
            End Get
        End Property

        Public Function ToggleSelectedBlocked() As Boolean
            If _document Is Nothing OrElse _selectedX < 1 OrElse _selectedY < 1 Then Return False

            Dim cell As MapV1Cell = _document.Cells(_selectedX, _selectedY)
            cell.Blocked = If(cell.Blocked = 0, CByte(1), CByte(0))
            _document.Cells(_selectedX, _selectedY) = cell
            _document.Changed = True
            Invalidate()
            RaiseEvent CellEdited(Me, EventArgs.Empty)
            Return True
        End Function

        Public Property ShowLayer1 As Boolean
            Get
                Return _showLayer1
            End Get
            Set(value As Boolean)
                _showLayer1 = value
                Invalidate()
            End Set
        End Property

        Public Property ShowLayer2 As Boolean
            Get
                Return _showLayer2
            End Get
            Set(value As Boolean)
                _showLayer2 = value
                Invalidate()
            End Set
        End Property

        Public Property ShowLayer3 As Boolean
            Get
                Return _showLayer3
            End Get
            Set(value As Boolean)
                _showLayer3 = value
                Invalidate()
            End Set
        End Property

        Public Property ShowLayer4 As Boolean
            Get
                Return _showLayer4
            End Get
            Set(value As Boolean)
                _showLayer4 = value
                Invalidate()
            End Set
        End Property

        Public Property ShowGrid As Boolean
            Get
                Return _showGrid
            End Get
            Set(value As Boolean)
                _showGrid = value
                Invalidate()
            End Set
        End Property

        Public Property ShowBlocked As Boolean
            Get
                Return _showBlocked
            End Get
            Set(value As Boolean)
                _showBlocked = value
                Invalidate()
            End Set
        End Property

        Public Property ShowNpcs As Boolean
            Get
                Return _showNpcs
            End Get
            Set(value As Boolean)
                _showNpcs = value
                Invalidate()
            End Set
        End Property

        Public Property ShowObjects As Boolean
            Get
                Return _showObjects
            End Get
            Set(value As Boolean)
                _showObjects = value
                Invalidate()
            End Set
        End Property

        Public Property ShowTriggers As Boolean
            Get
                Return _showTriggers
            End Get
            Set(value As Boolean)
                _showTriggers = value
                Invalidate()
            End Set
        End Property

        Public Property ShowTranslados As Boolean
            Get
                Return _showTranslados
            End Get
            Set(value As Boolean)
                _showTranslados = value
                Invalidate()
            End Set
        End Property

        Public Property GraphicsIndex As GraficosIndReader
            Get
                Return _graphicsIndex
            End Get
            Set(value As GraficosIndReader)
                _graphicsIndex = value
                Invalidate()
            End Set
        End Property

        Public Property GraphicsDirectory As String
            Get
                Return _graphicsDirectory
            End Get
            Set(value As String)
                _graphicsDirectory = If(value, String.Empty)
                ClearBitmapCache()
                Invalidate()
            End Set
        End Property

        Public Sub New()
            DoubleBuffered = True
            BackColor = Color.White
            TabStop = True
            UpdateSurfaceSize()
            AddHandler _animationTimer.Tick, AddressOf AnimationTimer_Tick
            _animationTimer.Start()
        End Sub

        Public Sub ResetSelection()
            _selectedX = 0
            _selectedY = 0
            Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Using gridPen As New Pen(Color.LightGray)
                Using blockedBrush As New SolidBrush(Color.FromArgb(150, Color.IndianRed))
                    Using selectedBrush As New SolidBrush(Color.FromArgb(120, Color.Gold))
                        For y As Integer = 1 To 100
                            For x As Integer = 1 To 100
                                Dim left As Integer = (x - 1) * _cellSize
                                Dim top As Integer = (y - 1) * _cellSize
                                Dim cellRect As New Rectangle(left, top, _cellSize, _cellSize)

                                Dim hasGraphic As Boolean = False
                                If _document IsNot Nothing Then
                                    Dim cell As MapV1Cell = _document.Cells(x, y)
                                    Dim yOffset As Integer = 2
                                    If _showLayer1 AndAlso DrawGraphic(e.Graphics, cell.Graphic1, x, y, 1, left, top, yOffset) Then hasGraphic = True
                                    If _showLayer2 AndAlso DrawGraphic(e.Graphics, cell.Graphic2, x, y, 2, left, top, yOffset) Then hasGraphic = True
                                    If _showLayer3 AndAlso DrawGraphic(e.Graphics, cell.Graphic3, x, y, 3, left, top, yOffset) Then hasGraphic = True
                                    If _showLayer4 AndAlso DrawGraphic(e.Graphics, cell.Graphic4, x, y, 4, left, top, yOffset) Then hasGraphic = True

                                    If Not hasGraphic AndAlso cell.Graphic1 <> 0 Then
                                        TextRenderer.DrawText(e.Graphics, cell.Graphic1.ToString(), Font,
                                                               New Point(left + 2, top + 2), Color.Black,
                                                               TextFormatFlags.NoPadding)
                                    End If

                                    DrawMetadataMarkers(e.Graphics, cell, left, top)
                                End If

                                If _showBlocked AndAlso _document IsNot Nothing AndAlso _document.Cells(x, y).Blocked <> 0 Then
                                    e.Graphics.FillRectangle(blockedBrush, cellRect)
                                End If
                                If x = _selectedX AndAlso y = _selectedY Then
                                    e.Graphics.FillRectangle(selectedBrush, cellRect)
                                End If

                                If _showGrid Then e.Graphics.DrawRectangle(gridPen, cellRect)
                            Next
                        Next
                    End Using
                End Using
            End Using
        End Sub

        Private Sub DrawMetadataMarkers(graphics As Graphics, cell As MapV1Cell,
                                        left As Integer, top As Integer)
            Dim markerY As Integer = top + 2
            If _showNpcs AndAlso cell.NpcIndex <> 0 Then
                DrawMarker(graphics, "N" & cell.NpcIndex, left + 2, markerY, Color.DarkBlue)
                markerY += Font.Height
            End If
            If _showObjects AndAlso cell.ObjIndex <> 0 Then
                DrawMarker(graphics, "O" & cell.ObjIndex, left + 2, markerY, Color.DarkGreen)
                markerY += Font.Height
            End If
            If _showTranslados AndAlso cell.ExitMap <> 0 Then
                DrawMarker(graphics, "E" & cell.ExitMap, left + 2, markerY, Color.DarkMagenta)
                markerY += Font.Height
            End If
            If _showTriggers AndAlso cell.Trigger <> 0 Then
                DrawMarker(graphics, "T" & cell.Trigger, left + 2, markerY, Color.Red)
            End If
        End Sub

        Private Sub DrawMarker(graphics As Graphics, text As String,
                               left As Integer, top As Integer, color As Color)
            TextRenderer.DrawText(graphics, text, Font, New Point(left, top), color,
                                  TextFormatFlags.NoPadding)
        End Sub

        Private Function DrawGraphic(graphics As Graphics, graphic As Short,
                                     cellX As Integer, cellY As Integer, layer As Integer,
                                     left As Integer, top As Integer, ByRef yOffset As Integer) As Boolean
            If graphic = 0 OrElse _graphicsIndex Is Nothing OrElse String.IsNullOrWhiteSpace(_graphicsDirectory) Then Return False
            Dim record As GrhIndexRecord = Nothing
            Dim stateKey As Integer = ((cellY - 1) * 100 + (cellX - 1)) * 4 + layer
            Dim state As AnimationState = GetAnimationState(stateKey, graphic)
            If Not _graphicsIndex.TryGetFrame(graphic, state.FrameNumber, record) Then Return False
            If record.FileNumber <= 0 OrElse record.Width <= 0 OrElse record.Height <= 0 Then Return False

            Dim bitmap As Bitmap = GetBitmap(record.FileNumber)
            If bitmap Is Nothing OrElse record.X < 0 OrElse record.Y < 0 OrElse
               record.X + record.Width > bitmap.Width OrElse record.Y + record.Height > bitmap.Height Then Return False

            Dim destination As New Rectangle(left, top, _cellSize, _cellSize)
            Dim source As New Rectangle(record.X, record.Y, record.Width, record.Height)
            graphics.DrawImage(bitmap, destination, source, GraphicsUnit.Pixel)
            yOffset += Font.Height
            Return True
        End Function

        Private Function GetAnimationState(key As Integer, graphic As Integer) As AnimationState
            Dim state As AnimationState = Nothing
            If Not _animationStates.TryGetValue(key, state) OrElse state.GrhIndex <> graphic Then
                state = New AnimationState With {.GrhIndex = graphic, .FrameNumber = 1, .SpeedCounter = 0}
                _animationStates(key) = state
            End If
            Return state
        End Function

        Private Sub AnimationTimer_Tick(sender As Object, e As EventArgs)
            If _graphicsIndex Is Nothing Then Return
            For Each state As AnimationState In _animationStates.Values
                Dim parent As GrhIndexRecord = Nothing
                If Not _graphicsIndex.Records.TryGetValue(state.GrhIndex, parent) OrElse Not parent.IsAnimation Then Continue For
                If state.SpeedCounter > 0 Then
                    state.SpeedCounter -= 1
                Else
                    state.SpeedCounter = Math.Max(1, parent.Speed)
                    state.FrameNumber += 1
                    If state.FrameNumber > parent.Frames.Count Then state.FrameNumber = 1
                End If
            Next
            Invalidate()
        End Sub

        Private Function GetBitmap(fileNumber As Integer) As Bitmap
            Dim cached As Bitmap = Nothing
            If _bitmapCache.TryGetValue(fileNumber, cached) Then Return cached
            Dim filePath As String = System.IO.Path.Combine(_graphicsDirectory, fileNumber.ToString() & ".bmp")
            If Not File.Exists(filePath) Then filePath = System.IO.Path.Combine(_graphicsDirectory, fileNumber.ToString() & ".BMP")
            If Not File.Exists(filePath) Then Return Nothing
            Try
                Using source As Image = Image.FromFile(filePath)
                    Dim bitmap As New Bitmap(source)
                    _bitmapCache(fileNumber) = bitmap
                    Return bitmap
                End Using
            Catch
                Return Nothing
            End Try
        End Function

        Private Sub ClearBitmapCache()
            For Each bitmap As Bitmap In _bitmapCache.Values
                bitmap.Dispose()
            Next
            _bitmapCache.Clear()
        End Sub

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then
                _animationTimer.Stop()
                RemoveHandler _animationTimer.Tick, AddressOf AnimationTimer_Tick
                _animationTimer.Dispose()
                ClearBitmapCache()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
            MyBase.OnMouseClick(e)
            If e.Button <> MouseButtons.Left Then Return

            Dim x As Integer = (e.X \ _cellSize) + 1
            Dim y As Integer = (e.Y \ _cellSize) + 1
            If x < 1 OrElse x > 100 OrElse y < 1 OrElse y > 100 Then Return

            _selectedX = x
            _selectedY = y
            Invalidate()
            RaiseEvent CellSelected(Me, EventArgs.Empty)
        End Sub

        Private Sub UpdateSurfaceSize()
            AutoSize = False
            Size = New Size(_cellSize * 100 + 1, _cellSize * 100 + 1)
        End Sub
    End Class
End Namespace
