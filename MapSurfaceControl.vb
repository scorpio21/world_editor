Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports System.ComponentModel
Imports WorldEditorNet.WorldEditorNet.Core
Imports System.Collections.Generic
Imports System.Diagnostics

Namespace WorldEditorNet

    Public Class MapSurfaceControl
        Inherits Control

        Private _document As MapV1Document
        ' El cliente VB6 trabaja con baldosas de 32x32 píxeles.
        ' Mantener 32 aquí es necesario para que el anclaje de casas,
        ' objetos y la cuadrícula coincida con RenderScreen.
        Private _cellSize As Integer = 32
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
        Private _insertPreviewEnabled As Boolean
        Private _viewportX As Integer = 50
        Private _viewportY As Integer = 50

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

        Public Property InsertPreviewEnabled As Boolean
            Get
                Return _insertPreviewEnabled
            End Get
            Set(value As Boolean)
                _insertPreviewEnabled = value
                Invalidate()
            End Set
        End Property

        Public Property InsertPreviewLayer As Integer
        Public Property InsertPreviewGrh As Integer
        Public Property InsertPreviewSettings As SurfacePatternSettings

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
                If _document Is Nothing Then
                    _viewportX = 50
                    _viewportY = 50
                End If
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

        Public ReadOnly Property ViewportX As Integer
            Get
                Return _viewportX
            End Get
        End Property

        Public ReadOnly Property ViewportY As Integer
            Get
                Return _viewportY
            End Get
        End Property

        Public Sub SetViewport(ByVal centerX As Integer, ByVal centerY As Integer)
            _viewportX = Math.Max(XMinMapSize, Math.Min(XMaxMapSize, centerX))
            _viewportY = Math.Max(YMinMapSize, Math.Min(YMaxMapSize, centerY))
            Invalidate()
        End Sub

        Public Sub MoveViewport(ByVal deltaX As Integer, ByVal deltaY As Integer)
            SetViewport(_viewportX + deltaX, _viewportY + deltaY)
        End Sub

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

            'VistaPreviaDeSup del VB6 dibuja el GRH a su tamaño real, sin Zoom.
            Dim drawWidth As Integer = record.Width
            Dim drawHeight As Integer = record.Height
            Dim left As Integer = destination.Left
            Dim top As Integer = destination.Top
            graphics.DrawImage(bitmap,
                               New Rectangle(left, top, drawWidth, drawHeight),
                               New Rectangle(record.X, record.Y, record.Width, record.Height),
                               GraphicsUnit.Pixel)
            Return True
        End Function

        Public Function CreateRadarBitmap(ByVal width As Integer, ByVal height As Integer) As Bitmap
            Dim bitmap As New Bitmap(Math.Max(1, width), Math.Max(1, height))
            Using graphics As Graphics = Graphics.FromImage(bitmap)
                graphics.Clear(Color.DarkGreen)
                If _document Is Nothing Then Return bitmap

                For y As Integer = 1 To 100
                    For x As Integer = 1 To 100
                        Dim cell As MapV1Cell = _document.Cells(x, y)
                        Dim color As Color = Color.FromArgb(0, 110, 0)
                        Dim record As GrhIndexRecord = Nothing
                        If _graphicsIndex IsNot Nothing AndAlso
                           _graphicsIndex.TryGetFrame(cell.Graphic1, 1, record) AndAlso
                           record IsNot Nothing Then
                            Dim sourceBitmap As Bitmap = GetBitmap(record.FileNumber)
                            If sourceBitmap IsNot Nothing AndAlso
                               record.X >= 0 AndAlso record.Y >= 0 AndAlso
                               record.X < sourceBitmap.Width AndAlso record.Y < sourceBitmap.Height Then
                                color = sourceBitmap.GetPixel(
                                    Math.Min(sourceBitmap.Width - 1, record.X + Math.Max(0, record.Width \ 2)),
                                    Math.Min(sourceBitmap.Height - 1, record.Y + Math.Max(0, record.Height \ 2)))
                            End If
                        End If
                        Using brush As New SolidBrush(color)
                            Dim left As Integer = (x - 1) * width \ 100
                            Dim top As Integer = (y - 1) * height \ 100
                            Dim right As Integer = Math.Max(left + 1, x * width \ 100)
                            Dim bottom As Integer = Math.Max(top + 1, y * height \ 100)
                            graphics.FillRectangle(brush, left, top, right - left, bottom - top)
                        End Using
                    Next
                Next
            End Using
            Return bitmap
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

            Using blockedBrush As New SolidBrush(Color.Red)
                    Using selectedBrush As New SolidBrush(Color.FromArgb(120, Color.Gold))
                        'ConvertCPtoTP del VB6 usa MainViewShp, no el tamaño
                        'interno del buffer de dibujo.
                        Dim visibleColumns As Integer = Math.Max(1, ParentClientWidth() \ _cellSize)
                        Dim visibleRows As Integer = Math.Max(1, ParentClientHeight() \ _cellSize)

                        ' RenderScreen del VB6 dibuja TileBufferSize celdas fuera
                        ' de la vista y después presenta solo el rectángulo visible.
                        ' Es imprescindible para que los GRH grandes no se corten
                        ' cuando su celda ancla está junto al borde.
                        Const tileBufferSize As Integer = 8
                        Dim originX As Integer = Math.Max(1, _viewportX - (visibleColumns \ 2))
                        Dim originY As Integer = Math.Max(1, _viewportY - (visibleRows \ 2))
                        Dim firstX As Integer = Math.Max(1, originX - tileBufferSize)
                        Dim firstY As Integer = Math.Max(1, originY - tileBufferSize)
                        Dim lastX As Integer = Math.Min(100, originX + visibleColumns + tileBufferSize)
                        Dim lastY As Integer = Math.Min(100, originY + visibleRows + tileBufferSize)

                        ' RenderScreen del VB6 no dibuja una celda completa de una
                        ' vez. Hace pasadas globales para que una superficie grande
                        ' no tape objetos o superficies de otra celda.
                        If _document IsNot Nothing Then
                            ' Pasada 1: capas de suelo y segunda capa.
                            Dim surfaceDrawn As New HashSet(Of Integer)()
                            For y As Integer = firstY To lastY
                                For x As Integer = firstX To lastX
                                    Dim left As Integer = (x - originX) * _cellSize
                                    Dim top As Integer = (y - originY) * _cellSize
                                    Dim cell As MapV1Cell = _document.Cells(x, y)
                                    Dim yOffset As Integer = 2
                                    If _showLayer1 AndAlso DrawGraphic(e.Graphics, cell.Graphic1, x, y, 1, left, top, yOffset) Then
                                        surfaceDrawn.Add(y * 1000 + x)
                                    End If
                                    If _showLayer2 Then DrawGraphic(e.Graphics, cell.Graphic2, x, y, 2, left, top, yOffset)
                                Next
                            Next

                            ' Pasada 2: objetos, NPCs y tercera capa.
                            Dim objectDrawn As New HashSet(Of Integer)()
                            Dim npcDrawn As New HashSet(Of Integer)()
                            For y As Integer = firstY To lastY
                                For x As Integer = firstX To lastX
                                    Dim left As Integer = (x - originX) * _cellSize
                                    Dim top As Integer = (y - originY) * _cellSize
                                    Dim cell As MapV1Cell = _document.Cells(x, y)
                                    Dim yOffset As Integer = 2
                                    Dim cellKey As Integer = y * 1000 + x

                                    If _showObjects AndAlso cell.ObjIndex > 0 AndAlso
                                       LegacyDeclarations.ObjDataArray IsNot Nothing AndAlso
                                       cell.ObjIndex < LegacyDeclarations.ObjDataArray.Length Then
                                        Dim objectGrh As Short = LegacyDeclarations.ObjDataArray(cell.ObjIndex).GrhIndex
                                        If DrawGraphic(e.Graphics, objectGrh, x, y, 5, left, top, yOffset) Then
                                            objectDrawn.Add(cellKey)
                                        End If
                                    End If

                                    If _showNpcs AndAlso cell.NpcIndex > 0 AndAlso
                                       LegacyDeclarations.NpcDataArray IsNot Nothing AndAlso
                                       cell.NpcIndex < LegacyDeclarations.NpcDataArray.Length AndAlso
                                       DrawNpc(e.Graphics, LegacyDeclarations.NpcDataArray(cell.NpcIndex),
                                               x, y, left, top, yOffset) Then
                                        npcDrawn.Add(cellKey)
                                    End If

                                    If _showLayer3 Then DrawGraphic(e.Graphics, cell.Graphic3, x, y, 3, left, top, yOffset)
                                Next
                            Next

                            ' Pasada 3: capa superior y elementos de edición.
                            For y As Integer = firstY To lastY
                                For x As Integer = firstX To lastX
                                    Dim left As Integer = (x - originX) * _cellSize
                                    Dim top As Integer = (y - originY) * _cellSize
                                    Dim cellRect As New Rectangle(left, top, _cellSize, _cellSize)
                                    Dim cell As MapV1Cell = _document.Cells(x, y)
                                    Dim yOffset As Integer = 2
                                    Dim cellKey As Integer = y * 1000 + x
                                    If _showLayer4 Then DrawGraphic(e.Graphics, cell.Graphic4, x, y, 4, left, top, yOffset)

                                    If Not surfaceDrawn.Contains(cellKey) AndAlso cell.Graphic1 <> 0 Then
                                        TextRenderer.DrawText(e.Graphics, cell.Graphic1.ToString(), Font,
                                                               New Point(left + 2, top + 2), Color.Black,
                                                               TextFormatFlags.NoPadding)
                                    End If
                                    DrawMetadataMarkers(e.Graphics, cell, left, top,
                                                        objectDrawn.Contains(cellKey), npcDrawn.Contains(cellKey))

                                    If _showBlocked AndAlso cell.Blocked <> 0 Then
                                        ' VB6: DrawBox(celda + 16, celda + 16, celda + 21, celda + 21).
                                        Dim markerSize As Integer = Math.Min(5, Math.Max(1, _cellSize))
                                        Dim markerOffset As Integer = Math.Min(16, Math.Max(0, _cellSize - markerSize))
                                        e.Graphics.FillRectangle(blockedBrush,
                                                                  cellRect.Left + markerOffset,
                                                                  cellRect.Top + markerOffset,
                                                                  markerSize, markerSize)
                                    End If
                                    If Not _insertPreviewEnabled AndAlso x = _selectedX AndAlso y = _selectedY Then
                                        e.Graphics.FillRectangle(selectedBrush, cellRect)
                                    End If

                                    If _showGrid Then
                                        ' VB6: DDrawSpecialToSurface(..., 17, ...).
                                        ' El gráfico 17 es el recorte 17 del atlas Graficos\0.bmp.
                                        DrawSpecialGraphic(e.Graphics, 17, left, top)
                                    End If
                                Next
                            Next
                        End If
                    End Using
                End Using
            If _insertPreviewEnabled AndAlso _cursorX >= 1 AndAlso _cursorY >= 1 AndAlso
               _cursorX <= 100 AndAlso _cursorY <= 100 AndAlso InsertPreviewGrh > 0 Then
                Dim originX As Integer = Math.Max(1, _viewportX - (Math.Max(1, ParentClientWidth() \ _cellSize) \ 2))
                Dim originY As Integer = Math.Max(1, _viewportY - (Math.Max(1, ParentClientHeight() \ _cellSize) \ 2))

                'Equivalente a modDirectDraw: special 5 marca el rectángulo
                'ocupado por el mosaico bajo el puntero usando Graficos\0.bmp.
                If InsertPreviewSettings IsNot Nothing AndAlso InsertPreviewSettings.Mosaic Then
                    Dim width As Integer = Math.Max(1, InsertPreviewSettings.Width)
                    Dim height As Integer = Math.Max(1, InsertPreviewSettings.Height)
                    Dim offsetX As Integer = If(InsertPreviewSettings.OffsetEnabled, InsertPreviewSettings.OffsetX, 0)
                    Dim offsetY As Integer = If(InsertPreviewSettings.OffsetEnabled, InsertPreviewSettings.OffsetY, 0)
                    Dim firstX As Integer = CInt(Math.Floor(((_cursorX + offsetX) / CDbl(width)))) * width - offsetX
                    Dim firstY As Integer = CInt(Math.Floor(((_cursorY + offsetY) / CDbl(height)))) * height - offsetY

                    'Previsualización temporal: no modifica el mapa hasta el clic.
                    Dim previewGrh As Integer = InsertPreviewSettings.GetGraphicIndex(
                        InsertPreviewGrh, _cursorX, _cursorY)
                    Dim previewOffset As Integer = 2
                    DrawGraphic(e.Graphics,
                                CShort(Math.Min(Short.MaxValue, Math.Max(0, previewGrh))),
                                _cursorX, _cursorY, InsertPreviewLayer,
                                (_cursorX - originX) * _cellSize,
                                (_cursorY - originY) * _cellSize,
                                previewOffset)

                    For y As Integer = firstY To firstY + height - 1
                        For x As Integer = firstX To firstX + width - 1
                            If x >= 1 AndAlso x <= 100 AndAlso y >= 1 AndAlso y <= 100 Then
                                Dim mosaicLeft As Integer = (x - originX) * _cellSize
                                Dim mosaicTop As Integer = (y - originY) * _cellSize
                                'VB6 no dibuja un GRH provisional aquí. Solo pinta
                                'el gráfico especial 5 sobre el render normal.
                                DrawSpecialGraphic(e.Graphics, 5,
                                                   mosaicLeft,
                                                   mosaicTop)
                            End If
                        Next
                    Next
                Else
                    Dim previewOffset As Integer = 2
                    DrawGraphic(e.Graphics,
                                CShort(Math.Min(Short.MaxValue, Math.Max(0, InsertPreviewGrh))),
                                _cursorX, _cursorY, InsertPreviewLayer,
                                (_cursorX - originX) * _cellSize,
                                (_cursorY - originY) * _cellSize,
                                previewOffset)
                End If
            End If
            DrawCursorCoordinates(e.Graphics)
            RaiseEvent FrameRendered(Me, EventArgs.Empty)
        End Sub

        Private Sub DrawCursorCoordinates(graphics As Graphics)
            If _cursorX < 1 OrElse _cursorY < 1 Then Return
            TextRenderer.DrawText(graphics, $"X= {_cursorX} ; Y= {_cursorY}", Font,
                                  New Point(4, 4), Color.Red, TextFormatFlags.NoPadding)
        End Sub

        Private Sub DrawMetadataMarkers(graphics As Graphics, cell As MapV1Cell,
                                        left As Integer, top As Integer,
                                        Optional objectDrawn As Boolean = False,
                                        Optional npcDrawn As Boolean = False)
            Dim markerY As Integer = top + 2
            If _showNpcs AndAlso cell.NpcIndex <> 0 AndAlso Not npcDrawn Then
                DrawMarker(graphics, "N" & cell.NpcIndex, left + 2, markerY, Color.DarkBlue)
                markerY += Font.Height
            End If
            If _showObjects AndAlso cell.ObjIndex <> 0 AndAlso Not objectDrawn Then
                DrawMarker(graphics, "O" & cell.ObjIndex, left + 2, markerY, Color.DarkGreen)
                markerY += Font.Height
            End If
            If _showTranslados AndAlso cell.ExitMap <> 0 Then
                ' VB6: modRenderer dibuja los translados con el gráfico especial
                ' 1, almacenado en Graficos\0.bmp. No se debe pintar el número
                ' del mapa como texto (por ejemplo, "E16").
                DrawSpecialGraphic(graphics, 1, left, top)
            End If
            If _showTriggers AndAlso cell.Trigger <> 0 Then
                DrawMarker(graphics, "T" & cell.Trigger, left + 2, markerY, Color.Red)
            End If
        End Sub

        Private Function DrawNpc(graphics As Graphics,
                                  npc As NpcData,
                                  cellX As Integer,
                                  cellY As Integer,
                                  left As Integer,
                                  top As Integer,
                                  ByRef yOffset As Integer) As Boolean
            Dim direction As Integer = npc.Heading
            If direction < 1 OrElse direction > 4 Then Return False
            If npc.Body <= 0 OrElse LegacyDeclarations.BodyData Is Nothing OrElse
               npc.Body >= LegacyDeclarations.BodyData.Length Then Return False

            Dim body As tBodyData = LegacyDeclarations.BodyData(npc.Body)
            If body.Walk Is Nothing OrElse direction >= body.Walk.Length Then Return False
            Dim bodyDrawn As Boolean = DrawGraphic(graphics, body.Walk(direction).GrhIndex,
                                                   cellX, cellY, 5, left, top, yOffset)
            If Not bodyDrawn Then Return False

            If npc.Head > 0 AndAlso LegacyDeclarations.HeadData IsNot Nothing AndAlso
               npc.Head < LegacyDeclarations.HeadData.Length Then
                Dim head As tHeadData = LegacyDeclarations.HeadData(npc.Head)
                If head.Head IsNot Nothing AndAlso direction < head.Head.Length Then
                    DrawGraphic(graphics, head.Head(direction).GrhIndex,
                                cellX, cellY, 6, left + body.HeadOffset.X,
                                top + body.HeadOffset.Y, yOffset)
                End If
            End If
            Return True
        End Function

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

            ' VB6 dibuja el GRH con su tamaño real. Solo las superficies de la
            ' primera capa parten de la esquina de la celda; las capas superiores
            ' se centran sobre ella, igual que DDrawTransGrhtoSurface(Center=1).
            Dim drawLeft As Integer = left
            Dim drawTop As Integer = top
            If layer > 1 Then
                drawLeft -= CInt(Math.Truncate((record.Width - _cellSize) / 2.0R))
                drawTop -= record.Height - _cellSize
            End If

            Dim destination As New Rectangle(drawLeft, drawTop, record.Width, record.Height)
            Dim source As New Rectangle(record.X, record.Y, record.Width, record.Height)
            graphics.DrawImage(bitmap, destination, source, GraphicsUnit.Pixel)
            yOffset += Font.Height
            Return True
        End Function

        Private Sub DrawSpecialGraphic(graphics As Graphics, specialNumber As Integer,
                                       left As Integer, top As Integer)
            If specialNumber <= 0 OrElse String.IsNullOrWhiteSpace(_graphicsDirectory) Then Return

            Dim atlas As Bitmap = GetBitmap(0)
            If atlas Is Nothing Then Return

            Dim sourceLeft As Integer = (specialNumber - 1) * _cellSize
            If sourceLeft < 0 OrElse sourceLeft + _cellSize > atlas.Width OrElse
               _cellSize > atlas.Height Then Return

            graphics.DrawImage(atlas,
                               New Rectangle(left, top, _cellSize, _cellSize),
                               New Rectangle(sourceLeft, 0, _cellSize, _cellSize),
                               GraphicsUnit.Pixel)
        End Sub

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
                    ' DirectDraw del cliente VB6 carga las superficies con
                    ' SetColorKey(DDCKEY_SRCBLT) usando el color 0. En los
                    ' BMP de AO ese color es negro; debe quedar transparente
                    ' al componer el GRH sobre el mapa.
                    bitmap.MakeTransparent(Color.Black)
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
            Focus()
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
            Dim visibleColumns As Integer = Math.Max(1, ParentClientWidth() \ _cellSize)
            Dim visibleRows As Integer = Math.Max(1, ParentClientHeight() \ _cellSize)
            Dim firstX As Integer = Math.Max(1, _viewportX - visibleColumns \ 2)
            Dim firstY As Integer = Math.Max(1, _viewportY - visibleRows \ 2)
            Dim offsetX As Integer = pixelX Mod _cellSize
            Dim offsetY As Integer = pixelY Mod _cellSize
            Dim worldX As Integer = firstX + (pixelX \ _cellSize)
            Dim worldY As Integer = firstY + (pixelY \ _cellSize)
            Dim x As Integer = worldX
            Dim y As Integer = worldY

            Debug.WriteLine($"MouseX={pixelX}")
            Debug.WriteLine($"MouseY={pixelY}")
            Debug.WriteLine($"WorldX={worldX}")
            Debug.WriteLine($"WorldY={worldY}")
            Debug.WriteLine($"TileX={x}")
            Debug.WriteLine($"TileY={y}")
            Debug.WriteLine($"OffsetX={offsetX}")
            Debug.WriteLine($"OffsetY={offsetY}")
            Debug.WriteLine($"ViewportX={_viewportX}; ViewportY={_viewportY}; FirstY={firstY}; ParentHeight={ParentClientHeight()}; ControlHeight={ClientSize.Height}")

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
            Dim x As Integer = ViewportCellX(pixelX)
            Dim y As Integer = ViewportCellY(pixelY)
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

        Private Function ParentClientWidth() As Integer
            Return If(Parent Is Nothing, ClientSize.Width, Parent.ClientSize.Width)
        End Function

        Private Function ParentClientHeight() As Integer
            Return If(Parent Is Nothing, ClientSize.Height, Parent.ClientSize.Height)
        End Function

        Private Function ViewportCellX(ByVal pixelX As Integer) As Integer
            Dim visibleColumns As Integer = Math.Max(1, ParentClientWidth() \ _cellSize)
            Dim firstX As Integer = Math.Max(1, _viewportX - visibleColumns \ 2)
            Return firstX + (pixelX \ _cellSize)
        End Function

        Private Function ViewportCellY(ByVal pixelY As Integer) As Integer
            Dim visibleRows As Integer = Math.Max(1, ParentClientHeight() \ _cellSize)
            Dim firstY As Integer = Math.Max(1, _viewportY - visibleRows \ 2)
            Return firstY + (pixelY \ _cellSize)
        End Function
    End Class
End Namespace
