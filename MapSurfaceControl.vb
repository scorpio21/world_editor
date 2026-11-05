Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.ComponentModel
Imports WorldEditorNet.WorldEditorNet.Core
Imports System.Collections.Generic

Namespace WorldEditorNet

    Public Class MapSurfaceControl
        Inherits Control

        Private _document As MapV1Document
        Private _cellSize As Integer = 24
        Private _selectedX As Integer
        Private _selectedY As Integer
        Private _undoX As Integer
        Private _undoY As Integer
        Private _undoCell As MapV1Cell
        Private _hasUndo As Boolean
        Private _brushEnabled As Boolean
        Private _brushLayer As Integer
        Private _brushBaseGrh As Integer
        Private _brushSettings As SurfacePatternSettings
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
        Private _previewGrhIndex As Integer
        Private _previewFrameNumber As Integer = 1
        Private _previewSpeedCounter As Integer
        Private _emptyMapView As Boolean = True
        Private _cursorX As Integer
        Private _cursorY As Integer

        Private NotInheritable Class AnimationState
            Public Property GrhIndex As Integer
            Public Property FrameNumber As Integer = 1
            Public Property SpeedCounter As Integer
        End Class

        Public Event CellSelected As EventHandler
        Public Event CellEdited As EventHandler
        Public Event PreviewFrameChanged As EventHandler
        Public Event FrameRendered As EventHandler

        Public Property EmptyMapView As Boolean
            Get
                Return _emptyMapView
            End Get
            Set(value As Boolean)
                _emptyMapView = value
                Invalidate()
            End Set
        End Property

        Public Property BrushEnabled As Boolean
            Get
                Return _brushEnabled
            End Get
            Set(value As Boolean)
                _brushEnabled = value
                If Not value Then Capture = False
            End Set
        End Property

        Public Sub ConfigureBrush(ByVal layer As Integer,
                                  ByVal baseGrh As Integer,
                                  ByVal settings As SurfacePatternSettings)
            If layer < 1 OrElse layer > 4 Then Throw New ArgumentOutOfRangeException(NameOf(layer))
            If baseGrh < 0 OrElse baseGrh > Short.MaxValue Then Throw New ArgumentOutOfRangeException(NameOf(baseGrh))
            _brushLayer = layer
            _brushBaseGrh = baseGrh
            _brushSettings = settings
            _brushEnabled = True
        End Sub

        Public Property Document As MapV1Document
            Get
                Return _document
            End Get
            Set(value As MapV1Document)
                _document = value
                _hasUndo = False
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
            BeginUndo(cell)
            cell.Blocked = If(cell.Blocked = 0, CByte(1), CByte(0))
            _document.Cells(_selectedX, _selectedY) = cell
            _document.Changed = True
            Invalidate()
            RaiseEvent CellEdited(Me, EventArgs.Empty)
            Return True
        End Function

        Public Function SetSelectedGraphic(ByVal layer As Integer, ByVal grhIndex As Integer) As Boolean
            If _document Is Nothing OrElse _selectedX < 1 OrElse _selectedY < 1 Then Return False
            If layer < 1 OrElse layer > 4 OrElse grhIndex < 0 OrElse grhIndex > Short.MaxValue Then Return False

            Dim cell As MapV1Cell = _document.Cells(_selectedX, _selectedY)
            Dim oldGrh As Integer = GetGraphic(cell, layer)
            If oldGrh = grhIndex Then Return False

            BeginUndo(cell)
            SetGraphic(cell, layer, CShort(grhIndex))
            _document.Cells(_selectedX, _selectedY) = cell
            _document.Changed = True
            Invalidate()
            RaiseEvent CellEdited(Me, EventArgs.Empty)
            Return True
        End Function

        ''' <summary>Escribe el translado de la celda seleccionada.</summary>
        Public Function SetSelectedTransfer(ByVal mapNumber As Integer,
                                             ByVal destinationX As Integer,
                                             ByVal destinationY As Integer) As Boolean
            If _document Is Nothing OrElse _selectedX < 1 OrElse _selectedY < 1 Then Return False
            If mapNumber < 0 OrElse mapNumber > 9000 Then Return False
            If destinationX < 0 OrElse destinationX > 100 OrElse
               destinationY < 0 OrElse destinationY > 100 Then Return False

            Dim cell As MapV1Cell = _document.Cells(_selectedX, _selectedY)
            If cell.ExitMap = mapNumber AndAlso cell.ExitX = destinationX AndAlso
               cell.ExitY = destinationY Then Return False

            BeginUndo(cell)
            cell.ExitMap = CShort(mapNumber)
            cell.ExitX = CShort(destinationX)
            cell.ExitY = CShort(destinationY)
            _document.Cells(_selectedX, _selectedY) = cell
            _document.Changed = True
            Invalidate()
            RaiseEvent CellEdited(Me, EventArgs.Empty)
            Return True
        End Function

        ''' <summary>Quita el translado de la celda seleccionada.</summary>
        Public Function ClearSelectedTransfer() As Boolean
            If _document Is Nothing OrElse _selectedX < 1 OrElse _selectedY < 1 Then Return False

            Dim cell As MapV1Cell = _document.Cells(_selectedX, _selectedY)
            If cell.ExitMap = 0 AndAlso cell.ExitX = 0 AndAlso cell.ExitY = 0 Then Return False

            BeginUndo(cell)
            cell.ExitMap = 0
            cell.ExitX = 0
            cell.ExitY = 0
            _document.Cells(_selectedX, _selectedY) = cell
            _document.Changed = True
            Invalidate()
            RaiseEvent CellEdited(Me, EventArgs.Empty)
            Return True
        End Function

        Public Function SetSelectedObject(ByVal objectIndex As Integer,
                                          ByVal amount As Integer) As Boolean
            If _document Is Nothing OrElse SelectedX < 1 OrElse SelectedY < 1 Then Return False
            If objectIndex < 1 OrElse objectIndex > Short.MaxValue Then Return False
            If amount < 1 OrElse amount > Short.MaxValue Then Return False

            Dim cell As MapV1Cell = _document.Cells(SelectedX, SelectedY)
            If cell.ObjIndex = objectIndex AndAlso cell.ObjAmount = amount Then Return False
            cell.ObjIndex = CShort(objectIndex)
            cell.ObjAmount = CShort(amount)
            _document.Cells(SelectedX, SelectedY) = cell
            _document.Changed = True
            Invalidate()
            RaiseEvent CellEdited(Me, EventArgs.Empty)
            Return True
        End Function

        Public Function ClearSelectedObject() As Boolean
            If _document Is Nothing OrElse SelectedX < 1 OrElse SelectedY < 1 Then Return False

            Dim cell As MapV1Cell = _document.Cells(SelectedX, SelectedY)
            If cell.ObjIndex = 0 AndAlso cell.ObjAmount = 0 Then Return False
            BeginUndo(cell)
            cell.ObjIndex = 0
            cell.ObjAmount = 0
            _document.Cells(SelectedX, SelectedY) = cell
            _document.Changed = True
            Invalidate()
            RaiseEvent CellEdited(Me, EventArgs.Empty)
            Return True
        End Function

        Public Function SetSelectedNpc(ByVal npcIndex As Integer) As Boolean
            If _document Is Nothing OrElse SelectedX < 1 OrElse SelectedY < 1 Then Return False
            If npcIndex < 1 OrElse npcIndex > Short.MaxValue Then Return False

            Dim cell As MapV1Cell = _document.Cells(SelectedX, SelectedY)
            If cell.NpcIndex = npcIndex Then Return False
            BeginUndo(cell)
            cell.NpcIndex = CShort(npcIndex)
            _document.Cells(SelectedX, SelectedY) = cell
            _document.Changed = True
            Invalidate()
            RaiseEvent CellEdited(Me, EventArgs.Empty)
            Return True
        End Function

        Public Function ClearSelectedNpc() As Boolean
            If _document Is Nothing OrElse SelectedX < 1 OrElse SelectedY < 1 Then Return False

            Dim cell As MapV1Cell = _document.Cells(SelectedX, SelectedY)
            If cell.NpcIndex = 0 Then Return False
            BeginUndo(cell)
            cell.NpcIndex = 0
            _document.Cells(SelectedX, SelectedY) = cell
            _document.Changed = True
            Invalidate()
            RaiseEvent CellEdited(Me, EventArgs.Empty)
            Return True
        End Function

        Public Function PlaceRandomNpcs(ByVal count As Integer, ByVal npcIndex As Integer) As Integer
            If _document Is Nothing OrElse count < 1 OrElse npcIndex < 1 Then Return 0

            Dim random As New Random()
            Dim placed As Integer = 0
            Dim attempts As Integer = 0
            Dim maxAttempts As Integer = Math.Max(100, count * 100)
            While placed < count AndAlso attempts < maxAttempts
                attempts += 1
                Dim x As Integer = random.Next(XMinMapSize, XMaxMapSize)
                Dim y As Integer = random.Next(YMinMapSize, YMaxMapSize)
                Dim cell As MapV1Cell = _document.Cells(x, y)
                If cell.Blocked <> 0 Then Continue While

                BeginUndo(cell)
                cell.NpcIndex = CShort(Math.Min(npcIndex, Short.MaxValue))
                _document.Cells(x, y) = cell
                placed += 1
            End While

            If placed > 0 Then
                _document.Changed = True
                Invalidate()
                RaiseEvent CellEdited(Me, EventArgs.Empty)
            End If
            Return placed
        End Function

        Public Function SetSelectedTrigger(ByVal triggerIndex As Integer) As Boolean
            If _document Is Nothing OrElse SelectedX < 1 OrElse SelectedY < 1 Then Return False
            If triggerIndex < 0 OrElse triggerIndex > Short.MaxValue Then Return False

            Dim cell As MapV1Cell = _document.Cells(SelectedX, SelectedY)
            If cell.Trigger = triggerIndex Then Return False
            BeginUndo(cell)
            cell.Trigger = CShort(triggerIndex)
            _document.Cells(SelectedX, SelectedY) = cell
            _document.Changed = True
            Invalidate()
            RaiseEvent CellEdited(Me, EventArgs.Empty)
            Return True
        End Function

        Public Function ClearSelectedTrigger() As Boolean
            Return SetSelectedTrigger(0)
        End Function

        ''' <summary>Dibuja el frame actual del GRH en un visor externo.</summary>
        Public Function DrawGraphicPreview(ByVal graphics As Graphics,
                                           ByVal grhIndex As Integer,
                                           ByVal destination As Rectangle) As Boolean
            If graphics Is Nothing OrElse destination.Width <= 0 OrElse destination.Height <= 0 OrElse
               _graphicsIndex Is Nothing OrElse String.IsNullOrWhiteSpace(_graphicsDirectory) Then Return False

            Dim parent As GrhIndexRecord = Nothing
            If Not _graphicsIndex.Records.TryGetValue(grhIndex, parent) OrElse parent Is Nothing Then Return False
            If _previewGrhIndex <> grhIndex Then
                _previewGrhIndex = grhIndex
                _previewFrameNumber = 1
                _previewSpeedCounter = 0
            End If

            Dim record As GrhIndexRecord = Nothing
            If Not _graphicsIndex.TryGetFrame(grhIndex, _previewFrameNumber, record) OrElse record Is Nothing Then Return False
            If record.FileNumber <= 0 OrElse record.Width <= 0 OrElse record.Height <= 0 Then Return False

            Dim bitmap As Bitmap = GetBitmap(record.FileNumber)
            If bitmap Is Nothing OrElse record.X < 0 OrElse record.Y < 0 OrElse
               record.X + record.Width > bitmap.Width OrElse record.Y + record.Height > bitmap.Height Then Return False

            Dim scale As Double = Math.Min(destination.Width / CDbl(record.Width),
                                           destination.Height / CDbl(record.Height))
            Dim drawWidth As Integer = Math.Max(1, CInt(record.Width * scale))
            Dim drawHeight As Integer = Math.Max(1, CInt(record.Height * scale))
            Dim left As Integer = destination.Left + (destination.Width - drawWidth) \ 2
            Dim top As Integer = destination.Top + (destination.Height - drawHeight) \ 2
            graphics.DrawImage(bitmap,
                               New Rectangle(left, top, drawWidth, drawHeight),
                               New Rectangle(record.X, record.Y, record.Width, record.Height),
                               GraphicsUnit.Pixel)
            Return True
        End Function

        Public Function SetSelectedGraphicWithPattern(ByVal layer As Integer,
                                                       ByVal baseGrh As Integer,
                                                       ByVal settings As SurfacePatternSettings) As Boolean
            If _document Is Nothing OrElse _selectedX < 1 OrElse _selectedY < 1 OrElse settings Is Nothing Then Return False
            Dim grhIndex As Integer = settings.GetGraphicIndex(baseGrh, _selectedX, _selectedY)
            Return SetSelectedGraphic(layer, grhIndex)
        End Function

        Public Function ClearLayer(ByVal layer As Integer) As Boolean
            If _document Is Nothing OrElse layer < 1 OrElse layer > 4 Then Return False

            Dim replacement As Short = If(layer = 1, CShort(1), CShort(0))
            Dim changed As Boolean = False
            For y As Integer = 1 To 100
                For x As Integer = 1 To 100
                    Dim cell As MapV1Cell = _document.Cells(x, y)
                    If GetGraphic(cell, layer) <> replacement Then
                        SetGraphic(cell, layer, replacement)
                        _document.Cells(x, y) = cell
                        changed = True
                    End If
                Next
            Next

            If changed Then
                _document.Changed = True
                _hasUndo = False
                Invalidate()
                RaiseEvent CellEdited(Me, EventArgs.Empty)
            End If
            Return changed
        End Function

        Public Function ClearSelectedLayer(ByVal layer As Integer) As Boolean
            If _document Is Nothing OrElse _selectedX < 1 OrElse _selectedY < 1 Then Return False
            If layer < 1 OrElse layer > 4 Then Return False

            Dim cell As MapV1Cell = _document.Cells(_selectedX, _selectedY)
            Dim replacement As Short = If(layer = 1, CShort(1), CShort(0))
            If GetGraphic(cell, layer) = replacement Then Return False

            BeginUndo(cell)
            SetGraphic(cell, layer, replacement)
            _document.Cells(_selectedX, _selectedY) = cell
            _document.Changed = True
            Invalidate()
            RaiseEvent CellEdited(Me, EventArgs.Empty)
            Return True
        End Function

        Public Function ClearSelectedLayers(ByVal firstLayer As Integer,
                                            ByVal lastLayer As Integer) As Boolean
            If firstLayer > lastLayer Then Return False
            If _document Is Nothing OrElse _selectedX < 1 OrElse _selectedY < 1 Then Return False

            Dim cell As MapV1Cell = _document.Cells(_selectedX, _selectedY)
            Dim changed As Boolean = False
            For layer As Integer = firstLayer To lastLayer
                If layer < 1 OrElse layer > 4 Then Continue For
                Dim replacement As Short = If(layer = 1, CShort(1), CShort(0))
                If GetGraphic(cell, layer) <> replacement Then
                    If Not changed Then BeginUndo(cell)
                    SetGraphic(cell, layer, replacement)
                    changed = True
                End If
            Next

            If Not changed Then Return False
            _document.Cells(_selectedX, _selectedY) = cell
            _document.Changed = True
            Invalidate()
            RaiseEvent CellEdited(Me, EventArgs.Empty)
            Return True
        End Function

        Public Function SetAllBlocked(ByVal blocked As Boolean) As Boolean
            If _document Is Nothing Then Return False

            Dim value As Byte = If(blocked, CByte(1), CByte(0))
            Dim changed As Boolean = False
            For y As Integer = 1 To 100
                For x As Integer = 1 To 100
                    Dim cell As MapV1Cell = _document.Cells(x, y)
                    If cell.Blocked <> value Then
                        cell.Blocked = value
                        _document.Cells(x, y) = cell
                        changed = True
                    End If
                Next
            Next

            If changed Then
                _document.Changed = True
                _hasUndo = False
                Invalidate()
                RaiseEvent CellEdited(Me, EventArgs.Empty)
            End If
            Return changed
        End Function

        Public Function FillLayer(ByVal layer As Integer, ByVal grhIndex As Integer) As Boolean
            If _document Is Nothing OrElse layer < 1 OrElse layer > 4 Then Return False
            If grhIndex < 0 OrElse grhIndex > Short.MaxValue Then Return False

            Dim value As Short = CShort(grhIndex)
            Dim changed As Boolean = False
            For y As Integer = 1 To 100
                For x As Integer = 1 To 100
                    Dim cell As MapV1Cell = _document.Cells(x, y)
                    If GetGraphic(cell, layer) <> value Then
                        SetGraphic(cell, layer, value)
                        _document.Cells(x, y) = cell
                        changed = True
                    End If
                Next
            Next

            If changed Then
                _document.Changed = True
                _hasUndo = False
                Invalidate()
                RaiseEvent CellEdited(Me, EventArgs.Empty)
            End If
            Return changed
        End Function

        Public Function FillLayerWithPattern(ByVal layer As Integer,
                                             ByVal baseGrh As Integer,
                                             ByVal settings As SurfacePatternSettings) As Boolean
            If _document Is Nothing OrElse layer < 1 OrElse layer > 4 OrElse settings Is Nothing Then Return False

            Dim changed As Boolean = False
            For y As Integer = 1 To 100
                For x As Integer = 1 To 100
                    Dim grhIndex As Integer = settings.GetGraphicIndex(baseGrh, x, y)
                    If grhIndex < 0 OrElse grhIndex > Short.MaxValue Then
                        Throw New InvalidOperationException("El mosaico genera un GRH fuera del rango V1.")
                    End If

                    Dim cell As MapV1Cell = _document.Cells(x, y)
                    If GetGraphic(cell, layer) <> grhIndex Then
                        SetGraphic(cell, layer, CShort(grhIndex))
                        _document.Cells(x, y) = cell
                        changed = True
                    End If
                Next
            Next

            If changed Then
                _document.Changed = True
                _hasUndo = False
                Invalidate()
                RaiseEvent CellEdited(Me, EventArgs.Empty)
            End If
            Return changed
        End Function

        Public Function UndoLastEdit() As Boolean
            If _document Is Nothing OrElse Not _hasUndo Then Return False

            _document.Cells(_undoX, _undoY) = _undoCell
            _selectedX = _undoX
            _selectedY = _undoY
            _document.Changed = True
            _hasUndo = False
            Invalidate()
            RaiseEvent CellEdited(Me, EventArgs.Empty)
            Return True
        End Function

        Public Sub ClearUndo()
            _hasUndo = False
        End Sub

        Private Sub BeginUndo(ByVal cell As MapV1Cell)
            _undoX = _selectedX
            _undoY = _selectedY
            _undoCell = cell
            _hasUndo = True
        End Sub

        Private Shared Function GetGraphic(ByVal cell As MapV1Cell, ByVal layer As Integer) As Integer
            Select Case layer
                Case 1 : Return cell.Graphic1
                Case 2 : Return cell.Graphic2
                Case 3 : Return cell.Graphic3
                Case 4 : Return cell.Graphic4
                Case Else : Return -1
            End Select
        End Function

        Private Shared Sub SetGraphic(ByRef cell As MapV1Cell, ByVal layer As Integer, ByVal grhIndex As Short)
            Select Case layer
                Case 1 : cell.Graphic1 = grhIndex
                Case 2 : cell.Graphic2 = grhIndex
                Case 3 : cell.Graphic3 = grhIndex
                Case 4 : cell.Graphic4 = grhIndex
            End Select
        End Sub

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
                _previewGrhIndex = 0
                _previewFrameNumber = 1
                _previewSpeedCounter = 0
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
            If LicenseManager.UsageMode <> LicenseUsageMode.Designtime Then
                _animationTimer.Start()
            End If
        End Sub

        Public Sub ResetSelection()
            _selectedX = 0
            _selectedY = 0
            Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            e.Graphics.Clear(Color.Black)
            If _emptyMapView Then
                DrawCursorCoordinates(e.Graphics)
                RaiseEvent FrameRendered(Me, EventArgs.Empty)
                Return
            End If

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
            DrawCursorCoordinates(e.Graphics)
            RaiseEvent FrameRendered(Me, EventArgs.Empty)
        End Sub

        Private Sub DrawCursorCoordinates(graphics As Graphics)
            If _cursorX < 1 OrElse _cursorY < 1 Then Return
            TextRenderer.DrawText(graphics, $"X= {_cursorX} ; Y= {_cursorY}", Font,
                                  New Point(4, 4), Color.Red, TextFormatFlags.NoPadding)
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
            If _graphicsIndex IsNot Nothing Then
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

                Dim preview As GrhIndexRecord = Nothing
                If _previewGrhIndex > 0 AndAlso
                   _graphicsIndex.Records.TryGetValue(_previewGrhIndex, preview) AndAlso
                   preview IsNot Nothing AndAlso preview.IsAnimation Then
                    If _previewSpeedCounter > 0 Then
                        _previewSpeedCounter -= 1
                    Else
                        _previewSpeedCounter = Math.Max(1, preview.Speed)
                        _previewFrameNumber += 1
                        If _previewFrameNumber > preview.Frames.Count Then _previewFrameNumber = 1
                        RaiseEvent PreviewFrameChanged(Me, EventArgs.Empty)
                    End If
                End If
            End If

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
            SelectCellAt(e.X, e.Y, _brushEnabled)
        End Sub

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            If e.Button = MouseButtons.Left AndAlso _brushEnabled Then
                Capture = True
                SelectCellAt(e.X, e.Y, paint:=True)
            End If
        End Sub

        Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
            MyBase.OnMouseMove(e)
            UpdateCursorCell(e.X, e.Y)
            If _brushEnabled AndAlso e.Button = MouseButtons.Left Then
                SelectCellAt(e.X, e.Y, paint:=True)
            End If
        End Sub

        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            MyBase.OnMouseUp(e)
            If e.Button = MouseButtons.Left Then Capture = False
        End Sub

        Private Sub UpdateCursorCell(pixelX As Integer, pixelY As Integer)
            Dim x As Integer = (pixelX \ _cellSize) + 1
            Dim y As Integer = (pixelY \ _cellSize) + 1
            If x < 1 OrElse x > 100 OrElse y < 1 OrElse y > 100 Then
                _cursorX = 0
                _cursorY = 0
            Else
                _cursorX = x
                _cursorY = y
            End If
            Invalidate()
        End Sub

        Private Sub SelectCellAt(ByVal pixelX As Integer,
                                 ByVal pixelY As Integer,
                                 ByVal paint As Boolean)
            Dim x As Integer = (pixelX \ _cellSize) + 1
            Dim y As Integer = (pixelY \ _cellSize) + 1
            If x < 1 OrElse x > 100 OrElse y < 1 OrElse y > 100 Then Return

            Dim changedCell As Boolean = _selectedX <> x OrElse _selectedY <> y
            _selectedX = x
            _selectedY = y
            Invalidate()
            RaiseEvent CellSelected(Me, EventArgs.Empty)

            If paint AndAlso _brushEnabled AndAlso _brushSettings IsNot Nothing Then
                SetSelectedGraphicWithPattern(_brushLayer, _brushBaseGrh, _brushSettings)
            End If
        End Sub

        Private Sub UpdateSurfaceSize()
            AutoSize = False
            Size = New Size(_cellSize * 100 + 1, _cellSize * 100 + 1)
        End Sub
    End Class
End Namespace
