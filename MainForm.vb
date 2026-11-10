Imports WorldEditorNet.WorldEditorNet.Core
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Namespace WorldEditorNet

    Partial Public Class MainForm
        Private _document As MapV1Document
        Private _mapPath As String = String.Empty
        Private _settings As WorldEditorSettings
        Private _graphicsIndex As GraficosIndReader
        Private _surfaceSettings As New SurfacePatternSettings()
        Private ReadOnly _filteredSurfaceIndices As New List(Of Integer)()
        Private _startupMessage As String = String.Empty

        Private Enum SurfaceEditMode
            None
            Insert
            RemoveCurrentLayer
            RemoveLayersTwoAndThree
        End Enum

        Private Enum TransferEditMode
            None
            Insert
            Remove
        End Enum

        Private Enum NpcEditMode
            None
            InsertNormal
            RemoveNormal
            InsertHostile
            RemoveHostile
        End Enum

        Private Enum TriggerEditMode
            None
            Insert
            Remove
        End Enum

        Private _surfaceMode As SurfaceEditMode
        Private _transferMode As TransferEditMode
        Private _manualTransfer As Boolean
        Private _insertTransferObject As Boolean
        Private _objectInsertEnabled As Boolean
        Private _objectRemoveEnabled As Boolean
        Private _npcMode As NpcEditMode
        Private _triggerMode As TriggerEditMode
        Private _mapLoaded As Boolean
        Private ReadOnly _mapTabPaths As String() = New String(12) {}
        Private ReadOnly _fpsTimer As New Timer With {.Interval = 1000}
        ' Equivalente al sondeo CheckKeys de modGeneral.bas.
        Private ReadOnly _movementTimer As New Timer With {.Interval = 50}
        Private _framesThisSecond As Integer
        Private ReadOnly _loadingForm As frmCargando

        <DllImport("user32.dll")>
        Private Shared Function GetKeyState(ByVal virtualKey As Integer) As Short
        End Function

        Public Sub New(Optional loadingForm As frmCargando = Nothing)
            _loadingForm = loadingForm
            InitializeComponent()
            PreviewGrh.BackColor = Color.Black
            ClearFunctionsButton_Click(clearFunctionsButton, EventArgs.Empty)
        End Sub

        Private Sub SurfacePanelButton_Click(sender As Object, e As EventArgs) Handles surfacePanelButton.Click
            SelectPanelMode(0, "Superficie")
        End Sub

        Private Sub TransfersPanelButton_Click(sender As Object, e As EventArgs) Handles transfersPanelButton.Click
            SelectPanelMode(1, "Translados")
        End Sub

        Private Sub BlockedPanelButton_Click(sender As Object, e As EventArgs) Handles blockedPanelButton.Click
            SelectPanelMode(2, "Bloqueos")
        End Sub

        Private Sub NpcsPanelButton_Click(sender As Object, e As EventArgs) Handles npcsPanelButton.Click
            SelectPanelMode(3, "NPC's")
        End Sub

        Private Sub HostileNpcsPanelButton_Click(sender As Object, e As EventArgs) Handles hostileNpcsPanelButton.Click
            SelectPanelMode(4, "NPC's Hostiles")
        End Sub

        Private Sub ObjectsPanelButton_Click(sender As Object, e As EventArgs) Handles objectsPanelButton.Click
            SelectPanelMode(5, "Objetos")
        End Sub

        Private Sub TriggersPanelButton_Click(sender As Object, e As EventArgs) Handles triggersPanelButton.Click
            SelectPanelMode(6, "Trigger's")
        End Sub

        Private Sub SelectPanelMode(index As Integer, caption As String)
            For Each button As LegacyPanelButton In {
                surfacePanelButton, transfersPanelButton, blockedPanelButton,
                npcsPanelButton, hostileNpcsPanelButton, objectsPanelButton,
                triggersPanelButton}
                button.Selected = False
            Next

            Dim selected = {
                surfacePanelButton, transfersPanelButton, blockedPanelButton,
                npcsPanelButton, hostileNpcsPanelButton, objectsPanelButton,
                triggersPanelButton}(index)
            selected.Selected = True
            SetFunctionMode(index)
            If index = 2 Then SetBlockedVisibility(True)
            If index <> 5 Then
                _objectInsertEnabled = False
                _objectRemoveEnabled = False
                ApplyObjectEditMode()
            End If
            If index <> 3 AndAlso index <> 4 Then SetNpcEditMode(NpcEditMode.None)
            If index <> 6 Then SetTriggerEditMode(TriggerEditMode.None)
            statusLabel.Text = $"Panel seleccionado: {caption} (F{If(index < 5, index + 5, If(index = 5, 11, 12))})"
            StatTxt.Text = $"Panel {caption} seleccionado."
        End Sub

        Private Sub SetFunctionMode(index As Integer)
            functionContentPanel.Visible = True
            panelStateRow.Visible = _mapLoaded
            surfaceFunctionPanel.Visible = index = 0
            transferFunctionPanel.Visible = index = 1
            blockingFunctionPanel.Visible = index = 2
            npcFunctionPanel.Visible = index = 3
            hostileNpcFunctionPanel.Visible = index = 4
            objectFunctionPanel.Visible = index = 5
            triggerFunctionPanel.Visible = index = 6

            Dim surfaceControlsVisible = index = 0
            removeSurfaceLayerButton.Visible = surfaceControlsVisible
            removeSurfaceAllLayersButton.Visible = surfaceControlsVisible
            selectSurfaceButton.Visible = surfaceControlsVisible
            PreviewGrh.Visible = surfaceControlsVisible

            If index <> 1 Then SetTransferEditMode(TransferEditMode.None)

            If surfaceControlsVisible Then
                'El panel de superficies ocupa todo el contenedor; los botones deben quedar delante.
                removeSurfaceLayerButton.BringToFront()
                removeSurfaceAllLayersButton.BringToFront()
                selectSurfaceButton.BringToFront()
                StatTxt.Location = New Point(16, 672)
                StatTxt.Size = New Size(290, 37)
            Else
                StatTxt.Location = New Point(16, 418)
                StatTxt.Size = New Size(290, 285)
            End If
        End Sub

        Private Sub ClearFunctionsButton_Click(sender As Object, e As EventArgs) Handles clearFunctionsButton.Click
            SetSurfaceEditMode(SurfaceEditMode.None)
            SetTransferEditMode(TransferEditMode.None)
            _objectInsertEnabled = False
            _objectRemoveEnabled = False
            ApplyObjectEditMode()
            SetNpcEditMode(NpcEditMode.None)
            SetTriggerEditMode(TriggerEditMode.None)
            For Each button As LegacyPanelButton In {
                surfacePanelButton, transfersPanelButton, blockedPanelButton,
                npcsPanelButton, hostileNpcsPanelButton, objectsPanelButton,
                triggersPanelButton}
                button.Selected = False
            Next

            surfaceFunctionPanel.Visible = False
            transferFunctionPanel.Visible = False
            blockingFunctionPanel.Visible = False
            npcFunctionPanel.Visible = False
            hostileNpcFunctionPanel.Visible = False
            objectFunctionPanel.Visible = False
            triggerFunctionPanel.Visible = False
            functionContentPanel.Visible = False
            panelStateRow.Visible = _mapLoaded
            removeSurfaceLayerButton.Visible = False
            removeSurfaceAllLayersButton.Visible = False
            selectSurfaceButton.Visible = False
            PreviewGrh.Visible = False
            StatTxt.Location = New Point(16, 418)
            StatTxt.Size = New Size(290, 285)

            StatTxt.Text = "Hace clic derecho sobre la celda para obtener la información."
            statusLabel.Text = "Funciones quitadas"
        End Sub

        Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
            Select Case e.KeyCode
                Case Keys.F4
                    ClearFunctionsButton_Click(clearFunctionsButton, EventArgs.Empty)
                Case Keys.F5
                    SelectPanelMode(0, "Superficie")
                Case Keys.F6
                    SelectPanelMode(1, "Translados")
                Case Keys.F7
                    SelectPanelMode(2, "Bloqueos")
                Case Keys.F8
                    SelectPanelMode(3, "NPC's")
                Case Keys.F9
                    SelectPanelMode(4, "NPC's Hostiles")
                Case Keys.F11
                    SelectPanelMode(5, "Objetos")
                Case Keys.F12
                    SelectPanelMode(6, "Trigger's")
                Case Keys.Up
                    If Not _mapLoaded Then Return
                Case Keys.Right
                    If Not _mapLoaded Then Return
                Case Keys.Down
                    If Not _mapLoaded Then Return
                Case Keys.Left
                    If Not _mapLoaded Then Return
                Case Else
                    Return
            End Select

            e.Handled = True
            e.SuppressKeyPress = True
        End Sub

        Private Sub MainForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
            _movementTimer.Stop()
            RemoveHandler _movementTimer.Tick, AddressOf MovementTimer_Tick
            _movementTimer.Dispose()
        End Sub

        Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Try
                ConfigureMapTabLabels()
                _loadingForm?.SetLoadingStep(0, "Cargando configuración...")
                LoadSettings()
                _loadingForm?.SetLoadingStep(1, "Cargando índices de superficies...")
                LoadLegacyIndices()
                _loadingForm?.SetLoadingStep(4, "Cargando objetos...")
                LoadGraphicsIndex()
                _loadingForm?.SetLoadingStep(5, "Iniciando ventana de edición...")
                CreateNewMap()
                FPS.Text = "FPS: 0"
                RefreshRadar()
                AddHandler _fpsTimer.Tick, AddressOf FpsTimer_Tick
                _fpsTimer.Start()
                AddHandler _movementTimer.Tick, AddressOf MovementTimer_Tick
                _movementTimer.Start()
                radarPictureBox.SendToBack()
                Shape1.SendToBack()
                ' El indicador se pinta sobre la imagen del radar para que el
                ' rectángulo sea transparente, como el Shape del VB6.
                Shape1.Visible = False
                ApuntadorRadar.Visible = False
            Finally
                If _loadingForm IsNot Nothing Then
                    _loadingForm.Hide()
                    _loadingForm.Dispose()
                End If
            End Try
        End Sub

        'MapPest usaba Arial 8,25 en el formulario VB6. Mantener esa fuente
        'evita que Mapa10, Mapa11, etc. se recorten dentro de las etiquetas de
        '50 píxeles que conserva el diseño original.
        Private Sub ConfigureMapTabLabels()
            Dim mapTabs As Label() = GetMapTabLabels()
            For Each mapTab As Label In mapTabs
                mapTab.Font = New Font("Arial", 8.25F, FontStyle.Regular)
                mapTab.Padding = New Padding(0)
                mapTab.AutoEllipsis = False
            Next
        End Sub

        Private Sub MapSurface_FrameRendered(sender As Object, e As EventArgs) Handles mapSurface.FrameRendered
            _framesThisSecond += 1
        End Sub

        Private Sub FpsTimer_Tick(sender As Object, e As EventArgs)
            FPS.Text = $"FPS: {_framesThisSecond}"
            _framesThisSecond = 0
        End Sub

        Private Sub MovementTimer_Tick(sender As Object, e As EventArgs)
            If Not _mapLoaded OrElse Not ContainsFocus Then Return

            ' CheckKeys de VB6 comprueba Up, Right, Down y Left en este orden
            ' y mueve una sola casilla por iteración.
            Dim deltaX As Integer = 0
            Dim deltaY As Integer = 0

            If GetKeyState(CInt(Keys.Up)) < 0 Then
                deltaY = -1
            ElseIf GetKeyState(CInt(Keys.Right)) < 0 Then
                deltaX = 1
            ElseIf GetKeyState(CInt(Keys.Down)) < 0 Then
                deltaY = 1
            ElseIf GetKeyState(CInt(Keys.Left)) < 0 Then
                deltaX = -1
            Else
                Return
            End If

            mapSurface.MoveViewport(deltaX, deltaY)
            RefreshRadar()
        End Sub

        Private Sub MapSurface_MouseMove(sender As Object, e As MouseEventArgs) _
            Handles mapSurface.MouseMove
            Dim firstX As Integer = Math.Max(1, mapSurface.ViewportX - (ParentVisibleColumns() \ 2))
            Dim firstY As Integer = Math.Max(1, mapSurface.ViewportY - (ParentVisibleRows() \ 2))
            Dim x As Integer = firstX + (e.X \ mapSurface.CellSize)
            Dim y As Integer = firstY + (e.Y \ mapSurface.CellSize)
            If x >= 1 AndAlso x <= 100 AndAlso y >= 1 AndAlso y <= 100 Then
                POSX.Text = $"X: {x} - Y: {y}"
                If x < 10 OrElse y < 10 OrElse x > 90 OrElse y > 90 Then
                    POSX.ForeColor = Color.Red
                Else
                    POSX.ForeColor = Color.White
                End If
            End If
        End Sub

        Private Sub RadarPictureBox_MouseClick(sender As Object, e As MouseEventArgs) _
            Handles radarPictureBox.MouseClick
            If e.Button <> MouseButtons.Left Then Return

            Dim x As Integer = Math.Max(1, Math.Min(100, CInt(e.X * 100 / Math.Max(1, radarPictureBox.ClientSize.Width))))
            Dim y As Integer = Math.Max(1, Math.Min(100, CInt(e.Y * 100 / Math.Max(1, radarPictureBox.ClientSize.Height))))
            ApuntadorRadar.Left = Math.Max(Shape1.Left, Shape1.Left + CInt((x - 1) * Shape1.Width / 100.0R))
            ApuntadorRadar.Top = Math.Max(Shape1.Top, Shape1.Top + CInt((y - 1) * Shape1.Height / 100.0R))
            mapSurface.SetViewport(x, y)
            POSX.Text = $"X: {x} - Y: {y}"
            ApuntadorRadar.BringToFront()
            RefreshRadar()
        End Sub

        Private Function ParentVisibleColumns() As Integer
            Return Math.Max(1, MainViewShp.ClientSize.Width \ mapSurface.CellSize)
        End Function

        Private Function ParentVisibleRows() As Integer
            Return Math.Max(1, MainViewShp.ClientSize.Height \ mapSurface.CellSize)
        End Function

        Private Sub RefreshRadar()
            If radarPictureBox.ClientSize.Width <= 0 OrElse radarPictureBox.ClientSize.Height <= 0 Then Return
            Dim previous As Image = radarPictureBox.Image
            Dim radarImage As Bitmap = mapSurface.CreateRadarBitmap(
                radarPictureBox.ClientSize.Width, radarPictureBox.ClientSize.Height)
            If previous IsNot Nothing Then previous.Dispose()

            Dim columns As Integer = Math.Max(1, ParentVisibleColumns())
            Dim rows As Integer = Math.Max(1, ParentVisibleRows())
            Dim pointerWidth As Integer = Math.Max(2, CInt(columns * radarImage.Width / 100.0R))
            Dim pointerHeight As Integer = Math.Max(2, CInt(rows * radarImage.Height / 100.0R))
            Dim pointerLeft As Integer = CInt((mapSurface.ViewportX - 1 - columns \ 2) * radarImage.Width / 100.0R)
            Dim pointerTop As Integer = CInt((mapSurface.ViewportY - 1 - rows \ 2) * radarImage.Height / 100.0R)
            pointerLeft = Math.Max(0, Math.Min(radarImage.Width - pointerWidth, pointerLeft))
            pointerTop = Math.Max(0, Math.Min(radarImage.Height - pointerHeight, pointerTop))

            Using radarGraphics As Graphics = Graphics.FromImage(radarImage)
                Using pointerPen As New Pen(Color.White, 1.0F)
                    radarGraphics.DrawRectangle(pointerPen, pointerLeft, pointerTop,
                                                pointerWidth - 1, pointerHeight - 1)
                End Using
            End Using

            radarPictureBox.Image = radarImage
        End Sub

        Private Sub LoadLegacyIndices()
            If _settings Is Nothing Then Return

            Try
                Dim dataDirectory As String = Path.Combine(AppContext.BaseDirectory, "Datos")
                LegacyDeclarations.SupDataArray = LegacyIndicesLoader.LoadSurfaces(dataDirectory)
                LegacyDeclarations.MaxSup = CShort(Math.Min(LegacyDeclarations.SupDataArray.Length - 1, Short.MaxValue))

                surfaceFilterCombo.Text = String.Empty
                layerCombo.SelectedIndex = 0
                RefreshSurfaceList()

                LegacyDeclarations.TriggerNames = LegacyIndicesLoader.LoadTriggers(dataDirectory)
                LegacyDeclarations.NumTriggers = CShort(Math.Min(LegacyDeclarations.TriggerNames.Length, Short.MaxValue))

                triggerListBox.Items.Clear()
                For index As Integer = 0 To LegacyDeclarations.TriggerNames.Length - 1
                    triggerListBox.Items.Add(LegacyDeclarations.TriggerNames(index) & " - #" & index)
                Next
            Catch ex As Exception
                _startupMessage &= " - índices INI no cargados: " & ex.Message
            End Try

            _loadingForm?.SetLoadingStep(2, "Cargando Cuerpos...")
            Try
                LegacyDeclarations.BodyData = LegacyIndicesLoader.LoadBodies(_settings.DirIndex)
                LegacyDeclarations.NumBodies = CShort(Math.Min(LegacyDeclarations.BodyData.Length - 1, Short.MaxValue))
            Catch ex As Exception
                _startupMessage &= " - Personajes.ind no cargado: " & ex.Message
            End Try

            _loadingForm?.SetLoadingStep(3, "Cargando Cabezas...")
            Try
                LegacyDeclarations.HeadData = LegacyIndicesLoader.LoadHeads(_settings.DirIndex)
                LegacyDeclarations.Numheads = CShort(Math.Min(LegacyDeclarations.HeadData.Length - 1, Short.MaxValue))
            Catch ex As Exception
                _startupMessage &= " - Cabezas.ind no cargado: " & ex.Message
            End Try

            _loadingForm?.SetLoadingStep(4, "Cargando NPC's...")
            Try
                LegacyDeclarations.NpcDataArray = LegacyIndicesLoader.LoadNpcs(_settings.DirDats)
                npcListBox.Items.Clear()
                hostileNpcListBox.Items.Clear()
                For index As Integer = 1 To Math.Min(499, LegacyDeclarations.NpcDataArray.Length - 1)
                    If Not String.IsNullOrWhiteSpace(LegacyDeclarations.NpcDataArray(index).Name) Then
                        npcListBox.Items.Add(LegacyDeclarations.NpcDataArray(index).Name & " - #" & index)
                    End If
                Next
                For index As Integer = 500 To LegacyDeclarations.NpcDataArray.Length - 1
                    If Not String.IsNullOrWhiteSpace(LegacyDeclarations.NpcDataArray(index).Name) Then
                        hostileNpcListBox.Items.Add(LegacyDeclarations.NpcDataArray(index).Name & " - #" & index)
                    End If
                Next
                LegacyDeclarations.NumNPCs = CShort(Math.Min(499, Short.MaxValue))
                LegacyDeclarations.NumNPCsHOST = CShort(Math.Min(Math.Max(0, LegacyDeclarations.NpcDataArray.Length - 501), Short.MaxValue))
            Catch ex As Exception
                _startupMessage &= " - NPCs no cargados: " & ex.Message
            End Try

            _loadingForm?.SetLoadingStep(5, "Cargando objetos...")
            Try
                LegacyDeclarations.ObjDataArray = LegacyIndicesLoader.LoadObjects(_settings.DirDats)
                LegacyDeclarations.NumOBJs = CShort(Math.Min(LegacyDeclarations.ObjDataArray.Length - 1, Short.MaxValue))
                objectListBox.Items.Clear()
                For index As Integer = 1 To LegacyDeclarations.NumOBJs
                    _loadingForm?.SetLoadingStep(5, $"Cargando Datos de Objetos...{index}/{LegacyDeclarations.NumOBJs}")
                    Application.DoEvents()
                    objectListBox.Items.Add(LegacyDeclarations.ObjDataArray(index).Name & " - #" & index)
                Next
                objectAmountCombo.Text = "1"
                _loadingForm?.SetLoadingStep(5, "Cargando Triggers...")
            Catch ex As Exception
                _startupMessage &= " - OBJ.dat no cargado: " & ex.Message
            End Try
        End Sub

        Private Sub ObjectListBox_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles objectListBox.SelectedIndexChanged
            If objectListBox.SelectedIndex < 0 Then Return
            objectNumberCombo.Text = (objectListBox.SelectedIndex + 1).ToString()
            statusLabel.Text = "Objeto seleccionado: " & objectNumberCombo.Text
        End Sub

        Private Sub NpcListBox_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles npcListBox.SelectedIndexChanged
            If npcListBox.SelectedIndex >= 0 Then
                npcNumberCombo.Text = ExtractIndex(npcListBox.SelectedItem.ToString())
            End If
        End Sub

        Private Sub HostileNpcListBox_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles hostileNpcListBox.SelectedIndexChanged
            If hostileNpcListBox.SelectedIndex >= 0 Then
                hostileNpcNumberCombo.Text = ExtractIndex(hostileNpcListBox.SelectedItem.ToString())
            End If
        End Sub

        Private Shared Function ExtractIndex(ByVal item As String) As String
            Dim marker As Integer = item.LastIndexOf("#", StringComparison.Ordinal)
            Return If(marker >= 0, item.Substring(marker + 1).Trim(), String.Empty)
        End Function

        Private Sub InsertNpcFunctionButton_Click(sender As Object, e As EventArgs) _
            Handles insertNpcFunctionButton.Click, insertHostileNpcFunctionButton.Click
            Dim nextMode As NpcEditMode = If(sender Is insertHostileNpcFunctionButton,
                                             NpcEditMode.InsertHostile, NpcEditMode.InsertNormal)
            SetNpcEditMode(If(_npcMode = nextMode, NpcEditMode.None, nextMode))
        End Sub

        Private Sub RemoveNpcFunctionButton_Click(sender As Object, e As EventArgs) _
            Handles removeNpcFunctionButton.Click, removeHostileNpcFunctionButton.Click
            Dim nextMode As NpcEditMode = If(sender Is removeHostileNpcFunctionButton,
                                             NpcEditMode.RemoveHostile, NpcEditMode.RemoveNormal)
            SetNpcEditMode(If(_npcMode = nextMode, NpcEditMode.None, nextMode))
        End Sub

        Private Sub SetNpcEditMode(ByVal mode As NpcEditMode)
            _npcMode = mode
            insertNpcFunctionButton.BackColor = If(mode = NpcEditMode.InsertNormal, SystemColors.Highlight, SystemColors.Control)
            removeNpcFunctionButton.BackColor = If(mode = NpcEditMode.RemoveNormal, SystemColors.Highlight, SystemColors.Control)
            insertHostileNpcFunctionButton.BackColor = If(mode = NpcEditMode.InsertHostile, SystemColors.Highlight, SystemColors.Control)
            removeHostileNpcFunctionButton.BackColor = If(mode = NpcEditMode.RemoveHostile, SystemColors.Highlight, SystemColors.Control)
        End Sub

        Private Sub ApplyNpcEditMode()
            If _npcMode = NpcEditMode.None Then Return
            Dim changed As Boolean
            If _npcMode = NpcEditMode.RemoveNormal OrElse _npcMode = NpcEditMode.RemoveHostile Then
                changed = mapSurface.ClearSelectedNpc()
            Else
                Dim number As Integer
                Dim text As String = If(_npcMode = NpcEditMode.InsertHostile, hostileNpcNumberCombo.Text, npcNumberCombo.Text)
                If Not Integer.TryParse(text, number) OrElse number < 1 OrElse number >= LegacyDeclarations.NpcDataArray.Length OrElse
                   String.IsNullOrWhiteSpace(LegacyDeclarations.NpcDataArray(number).Name) Then
                    statusLabel.Text = "El NPC seleccionado no existe."
                    Return
                End If
                changed = mapSurface.SetSelectedNpc(number)
            End If
            If changed Then
                undoMenuItem.Enabled = True
                UpdateMapStatus(If(_npcMode = NpcEditMode.RemoveNormal OrElse _npcMode = NpcEditMode.RemoveHostile,
                                   "NPC eliminado", "NPC insertado"))
            End If
        End Sub

        Private Sub RandomNpcFunctionButton_Click(sender As Object, e As EventArgs) _
            Handles randomNpcFunctionButton.Click, randomHostileNpcFunctionButton.Click
            Dim numberText As String = If(sender Is randomHostileNpcFunctionButton,
                                          hostileNpcNumberCombo.Text, npcNumberCombo.Text)
            Dim amountText As String = If(sender Is randomHostileNpcFunctionButton,
                                          hostileNpcAmountCombo.Text, npcAmountCombo.Text)
            Dim npcIndex As Integer
            Dim amount As Integer
            If Not Integer.TryParse(numberText, npcIndex) OrElse
               Not Integer.TryParse(amountText, amount) OrElse amount < 1 OrElse amount > 200 Then
                statusLabel.Text = "El NPC o la cantidad no son válidos (1 a 200)."
                Return
            End If
            If npcIndex < 1 OrElse npcIndex >= LegacyDeclarations.NpcDataArray.Length OrElse
               String.IsNullOrWhiteSpace(LegacyDeclarations.NpcDataArray(npcIndex).Name) Then
                statusLabel.Text = "El NPC seleccionado no existe."
                Return
            End If

            Dim placed As Integer = mapSurface.PlaceRandomNpcs(amount, npcIndex)
            If placed > 0 Then
                undoMenuItem.Enabled = True
                UpdateMapStatus($"NPCs colocados al azar: {placed}")
            Else
                statusLabel.Text = "No se pudo colocar ningún NPC en el mapa."
            End If
        End Sub

        Private Sub InsertTriggerButton_Click(sender As Object, e As EventArgs) Handles insertTriggerButton.Click
            SetTriggerEditMode(If(_triggerMode = TriggerEditMode.Insert, TriggerEditMode.None, TriggerEditMode.Insert))
        End Sub

        Private Sub RemoveTriggerButton_Click(sender As Object, e As EventArgs) Handles removeTriggerButton.Click
            SetTriggerEditMode(If(_triggerMode = TriggerEditMode.Remove, TriggerEditMode.None, TriggerEditMode.Remove))
        End Sub

        Private Sub SetTriggerEditMode(ByVal mode As TriggerEditMode)
            _triggerMode = mode
            insertTriggerButton.BackColor = If(mode = TriggerEditMode.Insert, SystemColors.Highlight, SystemColors.Control)
            removeTriggerButton.BackColor = If(mode = TriggerEditMode.Remove, SystemColors.Highlight, SystemColors.Control)
        End Sub

        Private Sub ApplyTriggerEditMode()
            If _triggerMode = TriggerEditMode.None Then Return
            Dim changed As Boolean
            If _triggerMode = TriggerEditMode.Remove Then
                changed = mapSurface.ClearSelectedTrigger()
            ElseIf triggerListBox.SelectedIndex >= 0 Then
                changed = mapSurface.SetSelectedTrigger(triggerListBox.SelectedIndex)
            Else
                statusLabel.Text = "Selecciona un trigger antes de insertarlo."
                Return
            End If
            If changed Then
                undoMenuItem.Enabled = True
                UpdateMapStatus(If(_triggerMode = TriggerEditMode.Remove, "Trigger eliminado", "Trigger insertado"))
            End If
        End Sub

        Private Sub InsertObjectFunctionButton_Click(sender As Object, e As EventArgs) _
            Handles insertObjectFunctionButton.Click
            _objectInsertEnabled = Not _objectInsertEnabled
            _objectRemoveEnabled = False
            ApplyObjectEditMode()
        End Sub

        Private Sub RemoveObjectFunctionButton_Click(sender As Object, e As EventArgs) _
            Handles removeObjectFunctionButton.Click
            _objectRemoveEnabled = Not _objectRemoveEnabled
            _objectInsertEnabled = False
            ApplyObjectEditMode()
        End Sub

        Private Sub ApplyObjectEditMode()
            insertObjectFunctionButton.BackColor = If(_objectInsertEnabled,
                                                       SystemColors.Highlight, SystemColors.Control)
            removeObjectFunctionButton.BackColor = If(_objectRemoveEnabled,
                                                       SystemColors.Highlight, SystemColors.Control)
            If Not _objectInsertEnabled AndAlso Not _objectRemoveEnabled Then Return
            If mapSurface.SelectedX < 1 OrElse mapSurface.SelectedY < 1 Then
                statusLabel.Text = "Selecciona una celda para editar el objeto."
                Return
            End If

            Dim changed As Boolean
            If _objectRemoveEnabled Then
                changed = mapSurface.ClearSelectedObject()
            Else
                Dim objectIndex As Integer
                Dim amount As Integer
                If Not Integer.TryParse(objectNumberCombo.Text, objectIndex) OrElse
                   Not Integer.TryParse(objectAmountCombo.Text, amount) Then
                    statusLabel.Text = "Objeto o cantidad no válidos."
                    Return
                End If
                If objectIndex < 1 OrElse objectIndex > LegacyDeclarations.NumOBJs Then
                    statusLabel.Text = "El objeto seleccionado no existe en OBJ.dat."
                    Return
                End If
                If amount < 1 OrElse amount > Short.MaxValue Then
                    statusLabel.Text = "La cantidad debe estar entre 1 y 32767."
                    Return
                End If
                changed = mapSurface.SetSelectedObject(objectIndex, amount)
            End If

            If changed Then
                undoMenuItem.Enabled = True
                UpdateMapStatus(If(_objectRemoveEnabled, "Objeto eliminado", "Objeto insertado"))
            Else
                statusLabel.Text = "El objeto ya tenía ese estado."
            End If
        End Sub

        'Equivalente a lListado(0)_Click del frmMain VB6.
        Private Sub SurfaceListBox_SelectedIndexChanged(sender As Object,
                                                         e As EventArgs) _
            Handles surfaceListBox.SelectedIndexChanged
            Dim listIndex As Integer = surfaceListBox.SelectedIndex
            If listIndex < 0 OrElse listIndex >= _filteredSurfaceIndices.Count Then Return
            Dim selectedIndex As Integer = _filteredSurfaceIndices(listIndex)

            Dim surface As SupData = LegacyDeclarations.SupDataArray(selectedIndex)
            grhTextBox.Text = surface.Grh.ToString()
            _surfaceSettings.Mosaic = surface.Width > 0 AndAlso surface.Height > 0
            _surfaceSettings.Width = Math.Max(1, surface.Width)
            _surfaceSettings.Height = Math.Max(1, surface.Height)

            If surface.Capa >= 1 AndAlso surface.Capa <= 4 Then
                layerCombo.SelectedIndex = surface.Capa - 1
            End If

            statusLabel.Text = $"Superficie {selectedIndex}: GRH {surface.Grh}"
            If surface.Block Then
                statusLabel.Text &= " (bloqueada)"
            End If
            UpdateSurfacePreview()
            PreviewGrh.Invalidate()
        End Sub

        Private Sub SurfaceFilterCombo_TextChanged(sender As Object, e As EventArgs) _
            Handles surfaceFilterCombo.TextChanged
            If LegacyDeclarations.SupDataArray Is Nothing OrElse
               LegacyDeclarations.SupDataArray.Length = 0 Then Return
            RefreshSurfaceList()
        End Sub

        Private Sub RefreshSurfaceList()
            _filteredSurfaceIndices.Clear()
            surfaceListBox.Items.Clear()

            Dim filter As String = surfaceFilterCombo.Text.Trim()
            For index As Integer = 0 To LegacyDeclarations.SupDataArray.Length - 1
                Dim surface As SupData = LegacyDeclarations.SupDataArray(index)
                If filter.Length = 0 OrElse
                   (surface.Name IsNot Nothing AndAlso
                    surface.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0) Then
                    _filteredSurfaceIndices.Add(index)
                    surfaceListBox.Items.Add(surface.Name & " - #" & index)
                End If
            Next
        End Sub

        Private Sub PreviewGrh_Paint(sender As Object, e As PaintEventArgs) Handles PreviewGrh.Paint
            e.Graphics.Clear(Color.Black)
            Dim grh As Integer
            If Not Integer.TryParse(grhTextBox.Text, grh) OrElse grh <= 0 Then Return

            If Not mapSurface.DrawGraphicPreview(e.Graphics, grh,
                                                 New Rectangle(4, 4,
                                                               Math.Max(1, PreviewGrh.ClientSize.Width - 8),
                                                               Math.Max(1, PreviewGrh.ClientSize.Height - 8))) Then
                TextRenderer.DrawText(e.Graphics, "GRH no disponible", PreviewGrh.Font,
                                      New Point(8, 8), Color.White)
            End If
        End Sub

        Private Sub MapSurface_PreviewFrameChanged(sender As Object, e As EventArgs) _
            Handles mapSurface.PreviewFrameChanged
            PreviewGrh.Invalidate()
        End Sub

        Private Sub InsertTransferButton_Click(sender As Object, e As EventArgs) _
            Handles insertTransferButton.Click
            _insertTransferObject = False
            _manualTransfer = False
            SetTransferEditMode(If(_transferMode = TransferEditMode.Insert,
                                   TransferEditMode.None,
                                   TransferEditMode.Insert))
        End Sub

        Private Sub RemoveTransferButton_Click(sender As Object, e As EventArgs) _
            Handles removeTransferButton.Click
            _insertTransferObject = False
            SetTransferEditMode(If(_transferMode = TransferEditMode.Remove,
                                   TransferEditMode.None,
                                   TransferEditMode.Remove))
        End Sub

        Private Sub InsertTransferObjectButton_Click(sender As Object, e As EventArgs) _
            Handles insertTransferObjectButton.Click
            Dim objectIndex As Integer = _settings.ObjTranslado
            If objectIndex < 1 OrElse objectIndex > LegacyDeclarations.NumOBJs Then
                StatTxt.Text = "El objeto de translado configurado no existe en OBJ.dat."
                Return
            End If
            If LegacyDeclarations.ObjDataArray(objectIndex).ObjType <> 19 Then
                StatTxt.Text = "El objeto configurado no es de tipo translado (19)."
                Return
            End If
            _insertTransferObject = True
            _manualTransfer = False
            SetTransferEditMode(TransferEditMode.Insert)
            StatTxt.Text = "Objeto de translado activado: selecciona una celda para colocarlo."
        End Sub

        Private Sub UnionManualButton_Click(sender As Object, e As EventArgs) _
            Handles unionManualButton.Click
            _insertTransferObject = False
            Dim disableMode As Boolean = _transferMode = TransferEditMode.Insert AndAlso _manualTransfer
            _manualTransfer = Not disableMode
            SetTransferEditMode(If(disableMode, TransferEditMode.None, TransferEditMode.Insert))
            StatTxt.Text = "Unión manual: selecciona una celda del borde para crear el translado."
        End Sub

        Private Sub UnionAutoButton_Click(sender As Object, e As EventArgs) _
            Handles unionAutoButton.Click, insertAdjacentMenuItem.Click
            If _document Is Nothing Then
                StatTxt.Text = "Carga un mapa antes de unir mapas adyacentes."
                Return
            End If

            Using dialog As New UnionAdyacenteForm(_document)
                If dialog.ShowDialog(Me) = DialogResult.OK Then
                    mapSurface.Invalidate()
                    UpdateMapStatus()
                    StatTxt.Text = "Traslados a mapas adyacentes aplicados."
                End If
            End Using
        End Sub

        Private Sub SetTransferEditMode(ByVal mode As TransferEditMode)
            _transferMode = mode
            If mode = TransferEditMode.None Then
                _manualTransfer = False
                _insertTransferObject = False
            End If
            Dim editing As Boolean = mode <> TransferEditMode.None
            insertTransferButton.BackColor = If(mode = TransferEditMode.Insert,
                                                SystemColors.Highlight, SystemColors.Control)
            removeTransferButton.BackColor = If(mode = TransferEditMode.Remove,
                                                SystemColors.Highlight, SystemColors.Control)
            insertTransferButton.Enabled = Not (mode = TransferEditMode.Remove)
            insertTransferObjectButton.Enabled = Not editing
            unionManualButton.Enabled = Not (mode = TransferEditMode.Remove)
            unionAutoButton.Enabled = Not editing
            removeTransferButton.Enabled = Not (mode = TransferEditMode.Insert)
            transferMapTextBox.Enabled = Not (mode = TransferEditMode.Remove)
            transferXTextBox.Enabled = Not (mode = TransferEditMode.Remove)
            transferYTextBox.Enabled = Not (mode = TransferEditMode.Remove)
        End Sub

        Private Sub ApplyTransferEditMode()
            If _transferMode = TransferEditMode.None Then Return

            Dim changed As Boolean
            If _transferMode = TransferEditMode.Remove Then
                changed = mapSurface.ClearSelectedTransfer()
            Else
                If _insertTransferObject Then
                    changed = mapSurface.SetSelectedObject(_settings.ObjTranslado, 1) OrElse changed
                End If
                Dim mapNumber As Integer
                Dim destinationX As Integer
                Dim destinationY As Integer
                If Not Integer.TryParse(transferMapTextBox.Text, mapNumber) OrElse
                   Not Integer.TryParse(transferXTextBox.Text, destinationX) OrElse
                   Not Integer.TryParse(transferYTextBox.Text, destinationY) Then
                    StatTxt.Text = "Translado inválido: usa números para Mapa, X e Y."
                    Return
                End If
                If _manualTransfer Then
                    destinationX = 0
                    destinationY = 0
                    If mapSurface.SelectedX >= 90 Then
                        destinationX = 12
                        destinationY = mapSurface.SelectedY
                    ElseIf mapSurface.SelectedX <= 11 Then
                        destinationX = 91
                        destinationY = mapSurface.SelectedY
                    End If
                    If mapSurface.SelectedY >= 91 Then
                        destinationY = 11
                        destinationX = mapSurface.SelectedX
                    ElseIf mapSurface.SelectedY <= 10 Then
                        destinationY = 90
                        destinationX = mapSurface.SelectedX
                    End If
                End If
                changed = mapSurface.SetSelectedTransfer(mapNumber, destinationX, destinationY)
            End If

            If changed Then
                undoMenuItem.Enabled = True
                UpdateMapStatus("Translado aplicado")
            End If
        End Sub

        Private Sub SelectSurfaceButton_Click(sender As Object, e As EventArgs) _
            Handles selectSurfaceButton.Click
            SetSurfaceEditMode(If(_surfaceMode = SurfaceEditMode.Insert,
                                  SurfaceEditMode.None,
                                  SurfaceEditMode.Insert))
        End Sub

        Private Sub RemoveSurfaceLayerButton_Click(sender As Object, e As EventArgs) _
            Handles removeSurfaceLayerButton.Click
            SetSurfaceEditMode(If(_surfaceMode = SurfaceEditMode.RemoveCurrentLayer,
                                  SurfaceEditMode.None,
                                  SurfaceEditMode.RemoveCurrentLayer))
        End Sub

        Private Sub RemoveSurfaceAllLayersButton_Click(sender As Object, e As EventArgs) _
            Handles removeSurfaceAllLayersButton.Click
            SetSurfaceEditMode(If(_surfaceMode = SurfaceEditMode.RemoveLayersTwoAndThree,
                                  SurfaceEditMode.None,
                                  SurfaceEditMode.RemoveLayersTwoAndThree))
        End Sub

        Private Sub SetSurfaceEditMode(ByVal mode As SurfaceEditMode)
            _surfaceMode = mode
            Dim editing As Boolean = mode <> SurfaceEditMode.None
            surfaceListBox.Enabled = Not editing OrElse mode = SurfaceEditMode.Insert
            surfaceFilterCombo.Enabled = surfaceListBox.Enabled
            grhTextBox.Enabled = surfaceListBox.Enabled
            layerCombo.Enabled = mode <> SurfaceEditMode.RemoveLayersTwoAndThree
            selectSurfaceButton.Enabled = mode = SurfaceEditMode.Insert OrElse Not editing
            removeSurfaceLayerButton.Enabled = mode = SurfaceEditMode.RemoveCurrentLayer OrElse Not editing
            removeSurfaceAllLayersButton.Enabled = mode = SurfaceEditMode.RemoveLayersTwoAndThree OrElse Not editing

            selectSurfaceButton.BackColor = If(mode = SurfaceEditMode.Insert,
                                               SystemColors.Highlight, SystemColors.Control)
            removeSurfaceLayerButton.BackColor = If(mode = SurfaceEditMode.RemoveCurrentLayer,
                                                     SystemColors.Highlight, SystemColors.Control)
            removeSurfaceAllLayersButton.BackColor = If(mode = SurfaceEditMode.RemoveLayersTwoAndThree,
                                                         SystemColors.Highlight, SystemColors.Control)
            UpdateSurfacePreview()
        End Sub

        Private Sub UpdateSurfacePreview()
            mapSurface.InsertPreviewEnabled = _surfaceMode = SurfaceEditMode.Insert
            mapSurface.InsertPreviewLayer = layerCombo.SelectedIndex + 1
            mapSurface.InsertPreviewSettings = _surfaceSettings
            Dim grh As Integer
            mapSurface.InsertPreviewGrh = If(Integer.TryParse(grhTextBox.Text, grh), grh, 0)
        End Sub

        Private Sub SurfacePreviewInputChanged(sender As Object, e As EventArgs) _
            Handles grhTextBox.TextChanged, layerCombo.SelectedIndexChanged
            UpdateSurfacePreview()
        End Sub

        Private Sub ApplySurfaceEditMode()
            If _surfaceMode = SurfaceEditMode.None Then Return

            Dim layer As Integer = layerCombo.SelectedIndex + 1
            If layer < 1 OrElse layer > 4 Then Return

            Dim changed As Boolean
            Select Case _surfaceMode
                Case SurfaceEditMode.Insert
                    Dim grh As Integer
                    If Not Integer.TryParse(grhTextBox.Text, grh) Then Return
                    changed = mapSurface.SetSelectedGraphicWithPattern(layer, grh, _surfaceSettings)
                Case SurfaceEditMode.RemoveCurrentLayer
                    changed = mapSurface.ClearSelectedLayer(layer)
                Case SurfaceEditMode.RemoveLayersTwoAndThree
                    changed = mapSurface.ClearSelectedLayers(2, 3)
            End Select

            If changed Then
                undoMenuItem.Enabled = True
                UpdateMapStatus("Edición de superficie aplicada")
            End If
        End Sub

        Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
            If Not ConfirmSaveChanges() Then
                e.Cancel = True
                Return
            End If

            If _settings Is Nothing Then Return
            _settings.GuardarConfig = saveConfigMenuItem.Checked
            If Not _settings.GuardarConfig Then Return
            _settings.LastMapPath = _mapPath
            If mapSurface.SelectedX > 0 Then
                _settings.UserPos = New Position With {
                    .X = CShort(mapSurface.SelectedX),
                    .Y = CShort(mapSurface.SelectedY)}
            End If
            _settings.UserPos = New Position With {
                .X = CShort(mapSurface.ViewportX),
                .Y = CShort(mapSurface.ViewportY)}
            Try
                WorldEditorSettingsSaver.Save(AppContext.BaseDirectory, _settings)
            Catch ex As Exception
                statusLabel.Text = "No se pudo guardar la configuración: " & ex.Message
            End Try
        End Sub

        Private Sub LoadSettings()
            Dim loader As New WorldEditorSettingsLoader()

            Try
                _settings = loader.Load(AppContext.BaseDirectory)

                If Not HasRequiredResourceDirectories(_settings) Then
                    If Not ConfigureResourceDirectories(_settings) Then
                        _startupMessage = "Nuevo mapa - configuración pendiente"
                        Return
                    End If

                    WorldEditorSettingsSaver.Save(AppContext.BaseDirectory, _settings)
                    _settings = loader.Load(AppContext.BaseDirectory)
                ElseIf _settings.ConfigurationMissing Then
                    'Aunque el instalador no traiga el INI, persistimos las rutas
                    'detectadas para que el siguiente arranque sea reproducible.
                    WorldEditorSettingsSaver.Save(AppContext.BaseDirectory, _settings)
                    _settings = loader.Load(AppContext.BaseDirectory)
                End If

                _startupMessage = "Nuevo mapa - configuración cargada"
            Catch ex As Exception
                _settings = CreateDefaultSettings()
                If ConfigureResourceDirectories(_settings) Then
                    Try
                        WorldEditorSettingsSaver.Save(AppContext.BaseDirectory, _settings)
                        _settings = loader.Load(AppContext.BaseDirectory)
                        _startupMessage = "Nuevo mapa - configuración cargada"
                    Catch saveException As Exception
                        _startupMessage = "Nuevo mapa - configuración pendiente: " & saveException.Message
                    End Try
                Else
                    _startupMessage = "Nuevo mapa - configuración pendiente: " & ex.Message
                End If
            End Try
        End Sub

        Private Shared Function CreateDefaultSettings() As WorldEditorSettings
            Dim root As String = AppContext.BaseDirectory
            Return New WorldEditorSettings With {
                .IniPath = EnsureTrailingSeparator(root),
                .DirGraficos = EnsureTrailingSeparator(Path.Combine(root, "Graficos")),
                .DirIndex = EnsureTrailingSeparator(Path.Combine(root, "INIT")),
                .DirMidi = EnsureTrailingSeparator(Path.Combine(root, "MIDI")),
                .DirDats = EnsureTrailingSeparator(Path.Combine(root, "DATS")),
                .GuardarConfig = True,
                .MaxGrhs = 32200,
                .UserPos = New Position With {.X = 50, .Y = 50}}
        End Function

        Private Shared Function HasRequiredResourceDirectories(ByVal settings As WorldEditorSettings) As Boolean
            If settings Is Nothing Then Return False
            If Not DirectoryHasFiles(settings.DirGraficos) Then Return False
            If Not Directory.Exists(settings.DirMidi) Then Return False
            If Not DirectoryHasFiles(settings.DirDats) Then Return False
            If Not File.Exists(Path.Combine(settings.DirIndex, "Graficos.ind")) Then Return False
            Return True
        End Function

        Private Shared Function DirectoryHasFiles(ByVal path As String) As Boolean
            If String.IsNullOrWhiteSpace(path) OrElse Not Directory.Exists(path) Then Return False
            Return Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly).Any()
        End Function

        Private Function ConfigureResourceDirectories(ByVal settings As WorldEditorSettings) As Boolean
            Dim selections = New (String, String)() {
                ("Graficos", settings.DirGraficos),
                ("DATS", settings.DirDats),
                ("INIT", settings.DirIndex),
                ("MIDI", settings.DirMidi)}

            For Each selection In selections
                Using dialog As New FolderBrowserDialog()
                    dialog.Description = "Selecciona la carpeta " & selection.Item1 & " del WorldEditor."
                    dialog.ShowNewFolderButton = False
                    If Directory.Exists(selection.Item2) Then dialog.SelectedPath = selection.Item2
                    If dialog.ShowDialog(Me) <> DialogResult.OK Then Return False

                    Dim selectedPath As String = EnsureTrailingSeparator(Path.GetFullPath(dialog.SelectedPath))
                    Select Case selection.Item1
                        Case "Graficos"
                            settings.DirGraficos = selectedPath
                        Case "DATS"
                            settings.DirDats = selectedPath
                        Case "INIT"
                            settings.DirIndex = selectedPath
                        Case "MIDI"
                            settings.DirMidi = selectedPath
                    End Select
                End Using
            Next

            Return HasRequiredResourceDirectories(settings)
        End Function

        Private Shared Function EnsureTrailingSeparator(ByVal directoryPath As String) As String
            If String.IsNullOrWhiteSpace(directoryPath) Then Return String.Empty
            Dim normalized As String = System.IO.Path.GetFullPath(directoryPath)
            If normalized.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal) Then
                Return normalized
            End If
            Return normalized & Path.DirectorySeparatorChar
        End Function

        Private Sub LoadGraphicsIndex()
            If _settings Is Nothing Then Return
            Dim indexPath As String = Path.Combine(_settings.DirIndex, "Graficos.ind")
            If Not File.Exists(indexPath) Then
                _startupMessage &= " - Graficos.ind no encontrado"
                Return
            End If

            Try
                _graphicsIndex = GraficosIndReader.Load(indexPath)
                mapSurface.GraphicsIndex = _graphicsIndex
                mapSurface.GraphicsDirectory = _settings.DirGraficos
            Catch ex As Exception
                _graphicsIndex = Nothing
                _startupMessage &= " - índice gráfico no cargado: " & ex.Message
            End Try
        End Sub

        Private Sub NewMapMenuItem_Click(sender As Object, e As EventArgs) Handles newMapMenuItem.Click
            If Not ConfirmSaveChanges() Then Return

            'Equivalente a frmMain.Dialog.FileName = Empty en mnuNuevoMapa_Click
            'del VB6: el siguiente Abrir mapa debe empezar sin una selección previa.
            openMapDialog.FileName = String.Empty
            CreateNewMap()
            ShowMapInfoDialog()
        End Sub

        Private Sub OpenMapMenuItem_Click(sender As Object, e As EventArgs) Handles openMapMenuItem.Click
            If Not ConfirmSaveChanges() Then Return
            If openMapDialog.ShowDialog(Me) <> DialogResult.OK Then Return
            OpenMapFile(openMapDialog.FileName)
        End Sub

        Private Sub OpenMapFile(ByVal fileName As String)
            Try
                Dim codec As New MapV1Codec()
                _document = codec.Load(fileName)
                _mapPath = fileName
                _mapLoaded = True
                mapSurface.Document = _document
                mapSurface.SetViewport(If(_settings Is Nothing, 50, _settings.UserPos.X),
                                       If(_settings Is Nothing, 50, _settings.UserPos.Y))
                mapSurface.EmptyMapView = False
                MainViewShp.BackColor = Color.Black
                MainViewShp.Enabled = True
                MainViewShp.Visible = True
                MainViewShp.BringToFront()
                mapSurface.BringToFront()
                panelStateRow.Visible = True
                RefreshMapTabs(_mapPath)
                LoadMapInfo(_mapPath)
                RefreshRadar()
                DisableBrush()
                undoMenuItem.Enabled = False
                UpdateMapStatus()
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message, "No se pudo abrir el mapa",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub MapPest_Click(sender As Object, e As EventArgs) _
            Handles MapPest1.Click, MapPest2.Click, MapPest3.Click,
                    MapPest4.Click, MapPest5.Click, MapPest6.Click,
                    MapPest7.Click, MapPest8.Click, MapPest9.Click,
                    MapPest10.Click, MapPest11.Click, MapPest12.Click,
                    MapPest13.Click
            If Not _mapLoaded OrElse String.IsNullOrWhiteSpace(_mapPath) Then Return
            If Not ConfirmSaveChanges() Then Return

            Dim tabIndex As Integer = Array.IndexOf(GetMapTabLabels(), DirectCast(sender, Label))
            If tabIndex < 0 OrElse tabIndex > _mapTabPaths.GetUpperBound(0) Then Return

            Dim mapPath As String = _mapTabPaths(tabIndex)
            If String.IsNullOrWhiteSpace(mapPath) OrElse Not File.Exists(mapPath) Then Return
            OpenMapFile(mapPath)
        End Sub

        Private Function TryGetMapTabPath(ByVal currentMapPath As String,
                                          ByVal tabIndex As Integer,
                                          ByRef mapPath As String) As Boolean
            If tabIndex < 0 OrElse tabIndex > 12 Then Return False

            Dim mapName As String = Path.GetFileNameWithoutExtension(currentMapPath)
            Dim firstDigit As Integer = -1
            For index As Integer = 0 To mapName.Length - 1
                If Char.IsDigit(mapName(index)) Then
                    firstDigit = index
                    Exit For
                End If
            Next
            If firstDigit < 1 OrElse firstDigit >= mapName.Length Then Return False

            Dim mapNumber As Integer
            If Not Integer.TryParse(mapName.Substring(firstDigit), mapNumber) Then Return False
            Dim candidateNumber As Integer = mapNumber + tabIndex - 4
            If candidateNumber < 0 Then Return False

            mapPath = Path.Combine(Path.GetDirectoryName(currentMapPath),
                                   mapName.Substring(0, firstDigit) & candidateNumber & ".map")
            Return True
        End Function

        Private Sub SaveMapMenuItem_Click(sender As Object, e As EventArgs) Handles saveMapMenuItem.Click
            SaveCurrentMap()
        End Sub

        Private Function SaveCurrentMap() As Boolean
            If _document Is Nothing Then Return False
            If String.IsNullOrWhiteSpace(_mapPath) Then
                If saveMapDialog.ShowDialog(Me) <> DialogResult.OK Then Return False
                _mapPath = saveMapDialog.FileName
            End If

            Try
                Dim codec As New MapV1Codec()
                codec.Save(_mapPath, _document)
                MapInfo.Changed = 0
                RefreshMapTabs(_mapPath)
                UpdateMapStatus("Mapa guardado")
                Return True
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message, "No se pudo guardar el mapa",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Function

        Private Function ConfirmSaveChanges() As Boolean
            If _document Is Nothing OrElse (Not _document.Changed AndAlso MapInfo.Changed = 0) Then Return True

            Dim answer As DialogResult = MessageBox.Show(
                Me,
                "El mapa tiene cambios sin guardar. ¿Quieres guardarlos?",
                "Cambios sin guardar",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning)

            Select Case answer
                Case DialogResult.Yes
                    Return SaveCurrentMap()
                Case DialogResult.No
                    Return True
                Case Else
                    Return False
            End Select
        End Function

        Private Sub ExitMenuItem_Click(sender As Object, e As EventArgs) Handles exitMenuItem.Click
            Close()
        End Sub

        Private Sub ToggleBlockedMenuItem_Click(sender As Object, e As EventArgs) Handles toggleBlockedMenuItem.Click
            If mapSurface.ToggleSelectedBlocked() Then
                undoMenuItem.Enabled = True
                UpdateMapStatus("Mapa modificado: bloqueo alternado")
            Else
                statusLabel.Text = "Selecciona una celda antes de editarla"
            End If
        End Sub

        Private Sub UndoMenuItem_Click(sender As Object, e As EventArgs) Handles undoMenuItem.Click
            If mapSurface.UndoLastEdit() Then
                undoMenuItem.Enabled = False
                UpdateMapStatus("Última edición deshecha")
            End If
        End Sub

        Private Sub SetLayer1MenuItem_Click(sender As Object, e As EventArgs) Handles setLayer1MenuItem.Click
            SetSelectedLayerGraphic(1)
        End Sub

        Private Sub SetLayer2MenuItem_Click(sender As Object, e As EventArgs) Handles setLayer2MenuItem.Click
            SetSelectedLayerGraphic(2)
        End Sub

        Private Sub SetLayer3MenuItem_Click(sender As Object, e As EventArgs) Handles setLayer3MenuItem.Click
            SetSelectedLayerGraphic(3)
        End Sub

        Private Sub SetLayer4MenuItem_Click(sender As Object, e As EventArgs) Handles setLayer4MenuItem.Click
            SetSelectedLayerGraphic(4)
        End Sub

        Private Sub ClearLayer1MenuItem_Click(sender As Object, e As EventArgs) Handles clearLayer1MenuItem.Click
            ClearCompleteLayer(1)
        End Sub

        Private Sub ClearLayer2MenuItem_Click(sender As Object, e As EventArgs) Handles clearLayer2MenuItem.Click
            ClearCompleteLayer(2)
        End Sub

        Private Sub ClearLayer3MenuItem_Click(sender As Object, e As EventArgs) Handles clearLayer3MenuItem.Click
            ClearCompleteLayer(3)
        End Sub

        Private Sub ClearLayer4MenuItem_Click(sender As Object, e As EventArgs) Handles clearLayer4MenuItem.Click
            ClearCompleteLayer(4)
        End Sub

        Private Sub BlockAllMenuItem_Click(sender As Object, e As EventArgs) Handles blockAllMenuItem.Click
            SetAllBlocked(True)
        End Sub

        Private Sub UnblockAllMenuItem_Click(sender As Object, e As EventArgs) Handles unblockAllMenuItem.Click
            SetAllBlocked(False)
        End Sub

        Private Sub FillLayer1MenuItem_Click(sender As Object, e As EventArgs) Handles fillLayer1MenuItem.Click
            FillCompleteLayer(1)
        End Sub

        Private Sub FillLayer2MenuItem_Click(sender As Object, e As EventArgs) Handles fillLayer2MenuItem.Click
            FillCompleteLayer(2)
        End Sub

        Private Sub FillLayer3MenuItem_Click(sender As Object, e As EventArgs) Handles fillLayer3MenuItem.Click
            FillCompleteLayer(3)
        End Sub

        Private Sub FillLayer4MenuItem_Click(sender As Object, e As EventArgs) Handles fillLayer4MenuItem.Click
            FillCompleteLayer(4)
        End Sub

        Private Sub PaintSelectedMenuItem_Click(sender As Object, e As EventArgs) Handles paintSelectedMenuItem.Click
            If _document Is Nothing OrElse mapSurface.SelectedX < 1 Then
                statusLabel.Text = "Selecciona una celda antes de pintarla"
                Return
            End If

            Dim layerText As String = Microsoft.VisualBasic.Interaction.InputBox(
                "Capa (1 a 4):", "Pintar celda seleccionada", "1")
            If String.IsNullOrWhiteSpace(layerText) Then Return

            Dim layer As Integer
            If Not Integer.TryParse(layerText, layer) OrElse layer < 1 OrElse layer > 4 Then
                statusLabel.Text = "La capa debe estar entre 1 y 4"
                Return
            End If

            Dim grhText As String = Microsoft.VisualBasic.Interaction.InputBox(
                "GRH base:", "Pintar celda seleccionada", "1")
            If String.IsNullOrWhiteSpace(grhText) Then Return

            Dim baseGrh As Integer
            If Not Integer.TryParse(grhText, baseGrh) OrElse baseGrh < 0 OrElse baseGrh > Short.MaxValue Then
                statusLabel.Text = "El GRH debe ser un número entre 0 y 32767"
                Return
            End If

            Try
                If mapSurface.SetSelectedGraphicWithPattern(layer, baseGrh, _surfaceSettings) Then
                    undoMenuItem.Enabled = True
                    UpdateMapStatus($"Celda pintada en capa {layer}")
                Else
                    statusLabel.Text = "El GRH de la celda no ha cambiado"
                End If
            Catch ex As Exception
                statusLabel.Text = ex.Message
            End Try
        End Sub

        Private Sub BrushMenuItem_Click(sender As Object, e As EventArgs) Handles brushMenuItem.Click
            If mapSurface.BrushEnabled Then
                DisableBrush()
                statusLabel.Text = "Pincel desactivado"
                Return
            End If

            Dim layerText As String = Microsoft.VisualBasic.Interaction.InputBox(
                "Capa del pincel (1 a 4):", "Activar pincel", "1")
            If String.IsNullOrWhiteSpace(layerText) Then Return

            Dim layer As Integer
            If Not Integer.TryParse(layerText, layer) OrElse layer < 1 OrElse layer > 4 Then
                statusLabel.Text = "La capa del pincel debe estar entre 1 y 4"
                Return
            End If

            Dim grhText As String = Microsoft.VisualBasic.Interaction.InputBox(
                "GRH base del pincel:", "Activar pincel", "1")
            If String.IsNullOrWhiteSpace(grhText) Then Return

            Dim baseGrh As Integer
            If Not Integer.TryParse(grhText, baseGrh) OrElse baseGrh < 0 OrElse baseGrh > Short.MaxValue Then
                statusLabel.Text = "El GRH del pincel debe estar entre 0 y 32767"
                Return
            End If

            Try
                mapSurface.ConfigureBrush(layer, baseGrh, _surfaceSettings)
                brushMenuItem.Checked = True
                brushMenuItem.Text = "Desactivar pincel"
                statusLabel.Text = $"Pincel activo: capa {layer}, GRH {baseGrh}"
            Catch ex As Exception
                statusLabel.Text = ex.Message
            End Try
        End Sub

        Private Sub DisableBrush()
            mapSurface.BrushEnabled = False
            brushMenuItem.Checked = False
            brushMenuItem.Text = "Activar pincel..."
        End Sub

        Private Sub FillCompleteLayer(ByVal layer As Integer)
            If _document Is Nothing Then Return

            Dim value As String = Microsoft.VisualBasic.Interaction.InputBox(
                $"GRH que se aplicará a las 10.000 celdas de la capa {layer}:",
                "Rellenar capa completa",
                "1")
            If String.IsNullOrWhiteSpace(value) Then Return

            Dim grhIndex As Integer
            If Not Integer.TryParse(value, grhIndex) OrElse grhIndex < 0 OrElse grhIndex > Short.MaxValue Then
                statusLabel.Text = "El GRH debe ser un número entre 0 y 32767"
                Return
            End If

            Dim answer As DialogResult = MessageBox.Show(
                Me,
                $"¿Aplicar la configuración de superficie a toda la capa {layer}?",
                "Rellenar capa completa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
            If answer <> DialogResult.Yes Then Return

            Try
                If mapSurface.FillLayerWithPattern(layer, grhIndex, _surfaceSettings) Then
                    undoMenuItem.Enabled = False
                    UpdateMapStatus($"Capa {layer} rellenada con la configuración de superficie")
                Else
                    statusLabel.Text = $"La capa {layer} ya tenía la configuración indicada"
                End If
            Catch ex As Exception
                statusLabel.Text = ex.Message
            End Try
        End Sub

        Private Sub SetAllBlocked(ByVal blocked As Boolean)
            Dim action As String = If(blocked, "bloquear", "quitar todos los bloqueos de")
            Dim answer As DialogResult = MessageBox.Show(
                Me,
                $"¿Quieres {action} todo el mapa?",
                "Cambiar bloqueos",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
            If answer <> DialogResult.Yes Then Return

            If mapSurface.SetAllBlocked(blocked) Then
                undoMenuItem.Enabled = False
                UpdateMapStatus(If(blocked, "Mapa bloqueado", "Bloqueos eliminados"))
            Else
                statusLabel.Text = "El estado de bloqueos ya estaba aplicado"
            End If
        End Sub

        Private Sub ClearCompleteLayer(ByVal layer As Integer)
            If _document Is Nothing Then Return

            Dim answer As DialogResult = MessageBox.Show(
                Me,
                $"¿Borrar completamente la capa {layer} en las 100x100 celdas?",
                "Borrar capa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)
            If answer <> DialogResult.Yes Then Return

            If mapSurface.ClearLayer(layer) Then
                undoMenuItem.Enabled = False
                UpdateMapStatus($"Capa {layer} borrada")
            Else
                statusLabel.Text = $"La capa {layer} ya estaba vacía"
            End If
        End Sub

        Private Sub SetSelectedLayerGraphic(ByVal layer As Integer)
            If _document Is Nothing OrElse mapSurface.SelectedX < 1 Then
                statusLabel.Text = "Selecciona una celda antes de establecer un GRH"
                Return
            End If

            Dim current As MapV1Cell = _document.Cells(mapSurface.SelectedX, mapSurface.SelectedY)
            Dim currentGrh As Short
            Select Case layer
                Case 1 : currentGrh = current.Graphic1
                Case 2 : currentGrh = current.Graphic2
                Case 3 : currentGrh = current.Graphic3
                Case 4 : currentGrh = current.Graphic4
            End Select

            Dim value As String = Microsoft.VisualBasic.Interaction.InputBox(
                $"GRH para la capa {layer}:",
                "Establecer GRH",
                currentGrh.ToString())
            If String.IsNullOrWhiteSpace(value) Then Return

            Dim grhIndex As Integer
            If Not Integer.TryParse(value, grhIndex) OrElse grhIndex < 0 OrElse grhIndex > Short.MaxValue Then
                statusLabel.Text = "El GRH debe ser un número entre 0 y 32767"
                Return
            End If

            If mapSurface.SetSelectedGraphic(layer, grhIndex) Then
                undoMenuItem.Enabled = True
                UpdateMapStatus($"Capa {layer} modificada")
            Else
                statusLabel.Text = "El GRH no ha cambiado"
            End If
        End Sub

        Private Sub Layer1MenuItem_Click(sender As Object, e As EventArgs) Handles layer1MenuItem.Click
            ToggleViewItem(layer1MenuItem, Sub(value) mapSurface.ShowLayer1 = value)
        End Sub

        Private Sub Layer2MenuItem_Click(sender As Object, e As EventArgs) Handles layer2MenuItem.Click
            ToggleViewItem(layer2MenuItem, Sub(value) mapSurface.ShowLayer2 = value)
        End Sub

        Private Sub Layer3MenuItem_Click(sender As Object, e As EventArgs) Handles layer3MenuItem.Click
            ToggleViewItem(layer3MenuItem, Sub(value) mapSurface.ShowLayer3 = value)
        End Sub

        Private Sub Layer4MenuItem_Click(sender As Object, e As EventArgs) Handles layer4MenuItem.Click
            ToggleViewItem(layer4MenuItem, Sub(value) mapSurface.ShowLayer4 = value)
        End Sub

        Private Sub BlockedMenuItem_Click(sender As Object, e As EventArgs) Handles blockedMenuItem.Click
            SetBlockedVisibility(Not blockedMenuItem.Checked)
        End Sub

        Private Sub GridMenuItem_Click(sender As Object, e As EventArgs) Handles gridMenuItem.Click
            SetGridVisibility(Not mapSurface.ShowGrid)
        End Sub

        Private Sub NpcsMenuItem_Click(sender As Object, e As EventArgs) Handles npcsMenuItem.Click
            ToggleViewItem(npcsMenuItem, Sub(value) mapSurface.ShowNpcs = value)
        End Sub

        Private Sub ObjectsMenuItem_Click(sender As Object, e As EventArgs) Handles objectsMenuItem.Click
            ToggleViewItem(objectsMenuItem, Sub(value) mapSurface.ShowObjects = value)
        End Sub

        Private Sub TransladosMenuItem_Click(sender As Object, e As EventArgs) Handles transladosMenuItem.Click
            ToggleViewItem(transladosMenuItem, Sub(value) mapSurface.ShowTranslados = value)
        End Sub

        Private Sub TriggersMenuItem_Click(sender As Object, e As EventArgs) Handles triggersMenuItem.Click
            ToggleViewItem(triggersMenuItem, Sub(value) mapSurface.ShowTriggers = value)
        End Sub

        ' Equivalentes de los eventos explícitos del frmMain.frm original.
        ' CBVerCuadricula y CBVerBloqueosA no tenían procedimiento Click en VB6.
        ' El botón de cuadrícula representa el mismo estado que ...Grilla del menú Ver.
        Private Sub CBVerCuadricula_Click(sender As Object, e As EventArgs) Handles CBVerCuadricula.Click
            SetGridVisibility(Not mapSurface.ShowGrid)
        End Sub

        Private Sub CBVerBloqueosT_Click(sender As Object, e As EventArgs) Handles CBVerBloqueosT.Click
            SetBlockedVisibility(Not mapSurface.ShowBlocked)
        End Sub

        Private Sub ViewBlockingButton_Click(sender As Object, e As EventArgs) Handles viewBlockingButton.Click
            SetBlockedVisibility(Not mapSurface.ShowBlocked)
        End Sub

        Private Sub CBVerTraslados_Click(sender As Object, e As EventArgs) Handles CBVerTraslados.Click
            TransladosMenuItem_Click(transladosMenuItem, EventArgs.Empty)
        End Sub

        Private Sub CBVerTriggers_Click(sender As Object, e As EventArgs) Handles CBVerTriggers.Click
            TriggersMenuItem_Click(triggersMenuItem, EventArgs.Empty)
        End Sub

        Private Sub SaveConfigMenuItem_Click(sender As Object, e As EventArgs) Handles saveConfigMenuItem.Click
            saveConfigMenuItem.Checked = Not saveConfigMenuItem.Checked
        End Sub

        Private Sub SurfaceConfigMenuItem_Click(sender As Object, e As EventArgs) Handles surfaceConfigMenuItem.Click
            Using dialog As New SurfaceConfigForm()
                dialog.Settings = _surfaceSettings
                If dialog.ShowDialog(Me) = DialogResult.OK Then
                    _surfaceSettings = dialog.Settings
                    statusLabel.Text = "Configuración avanzada de superficie actualizada"
                End If
            End Using
        End Sub

        Private Shared Sub ToggleViewItem(item As ToolStripMenuItem, apply As Action(Of Boolean))
            item.Checked = Not item.Checked
            apply(item.Checked)
        End Sub

        Private Sub SetBlockedVisibility(value As Boolean)
            blockedMenuItem.Checked = value
            mapSurface.ShowBlocked = value
            CBVerBloqueosT.BackColor = If(value, Color.LightGreen, SystemColors.Control)
        End Sub

        Private Sub SetGridVisibility(value As Boolean)
            gridMenuItem.Checked = value
            mapSurface.ShowGrid = value
            CBVerCuadricula.BackColor = If(value, Color.LightGreen, SystemColors.Control)
        End Sub

        Private Sub MapSurface_CellSelected(sender As Object, e As EventArgs) Handles mapSurface.CellSelected
            If _settings IsNot Nothing AndAlso _settings.AutoCapturarTranslados AndAlso
               mapSurface.SelectedX > 0 Then
                Dim selectedCell As MapV1Cell = _document.Cells(mapSurface.SelectedX, mapSurface.SelectedY)
                transferMapTextBox.Text = selectedCell.ExitMap.ToString()
                transferXTextBox.Text = selectedCell.ExitX.ToString()
                transferYTextBox.Text = selectedCell.ExitY.ToString()
            End If
            UpdateMapStatus()
            ApplySurfaceEditMode()
            ApplyTransferEditMode()
            ApplyObjectEditMode()
            ApplyNpcEditMode()
            ApplyTriggerEditMode()
        End Sub

        Private Sub MapSurface_CellEdited(sender As Object, e As EventArgs) Handles mapSurface.CellEdited
            MapInfo.Changed = 1
            UpdateMapStatus("Mapa modificado")
            RefreshRadar()
        End Sub

        Private Sub CreateNewMap()
            MapInfo = New MapInfoRecord With {
                .Music = "0",
                .Name = "Nuevo Mapa",
                .MapVersion = 0,
                .Pk = True,
                .SeCaenItems = False,
                .MagiaSinEfecto = 0,
                .MinLevel = 0,
                .PuedeMascotas = 0,
                .Terreno = "BOSQUE",
                .Zona = "CAMPO",
                .Restringir = "No",
                .BackUp = 0,
                .Changed = 0
            }

            _document = New MapV1Document With {
                .MapVersion = 0,
                .Changed = False,
                .Header = New GameIni.tCabecera With {
                    .Desc = "Argentum Online by Noland Studios.",
                    .CRC = 0,
                    .MagicWord = 0
                }
            }

            For y As Integer = 1 To 100
                For x As Integer = 1 To 100
                    Dim cell As MapV1Cell = _document.Cells(x, y)
                    cell.Graphic1 = 1
                    _document.Cells(x, y) = cell
                Next
            Next

            _mapPath = String.Empty
            'modMapIO.NuevoMapa termina con MapaCargado = True y EngineRun = True.
            'El mapa nuevo es una matriz válida de 100x100, no una vista sin mapa.
            _mapLoaded = True
            mapSurface.Document = _document
            mapSurface.EmptyMapView = False
            MainViewShp.BackColor = Color.Black
            MainViewShp.Enabled = True
            MainViewShp.Visible = True
            MainViewShp.BringToFront()
            mapSurface.BringToFront()
            panelStateRow.Visible = False
            ResetMapTabs()
            SetDefaultMapInfo()
            DisableBrush()
            undoMenuItem.Enabled = False
            mapSurface.GraphicsIndex = _graphicsIndex
            If _settings IsNot Nothing Then mapSurface.GraphicsDirectory = _settings.DirGraficos
            mapSurface.SetViewport(If(_settings Is Nothing, 50, _settings.UserPos.X),
                                   If(_settings Is Nothing, 50, _settings.UserPos.Y))
            RefreshRadar()
            ApplySettingsToView()
            mapSurface.ResetSelection()
            UpdateMapStatus(If(String.IsNullOrWhiteSpace(_startupMessage), "Nuevo mapa", _startupMessage))
        End Sub

        'Equivalente a Pestañas() de modMapIO.bas.
        'VB6 muestra las ventanas de mapas vecinos que realmente existen y
        'oculta las posiciones restantes de la matriz MapPest.
        Private Sub RefreshMapTabs(ByVal mapPath As String)
            ResetMapTabs()

            If String.IsNullOrWhiteSpace(mapPath) OrElse Not File.Exists(mapPath) Then Return

            Dim mapName As String = Path.GetFileNameWithoutExtension(mapPath)
            Dim firstDigit As Integer = -1
            For index As Integer = 0 To mapName.Length - 1
                If Char.IsDigit(mapName(index)) Then
                    firstDigit = index
                    Exit For
                End If
            Next

            If firstDigit < 1 OrElse firstDigit >= mapName.Length Then Return

            Dim mapPrefix As String = mapName.Substring(0, firstDigit)
            Dim mapNumber As Integer
            If Not Integer.TryParse(mapName.Substring(firstDigit), mapNumber) Then Return

            Dim tabLabels As Label() = GetMapTabLabels()
            For relative As Integer = -4 To 8
                Dim candidateNumber As Integer = mapNumber + relative
                Dim tabIndex As Integer = relative + 4
                Dim candidatePath As String = String.Empty

                If candidateNumber >= 0 Then
                    candidatePath = FindMapPath(
                        Path.GetDirectoryName(mapPath), mapPrefix, candidateNumber)
                End If

                Dim exists As Boolean = Not String.IsNullOrWhiteSpace(candidatePath)
                _mapTabPaths(tabIndex) = If(exists, candidatePath, String.Empty)
                tabLabels(tabIndex).Text = If(exists, mapPrefix & candidateNumber, "Nada")
                tabLabels(tabIndex).Visible = exists
                tabLabels(tabIndex).Enabled = exists
                tabLabels(tabIndex).BackColor = If(exists AndAlso tabIndex = 4,
                                                   Color.FromArgb(128, 255, 128),
                                                   Color.White)
            Next
        End Sub

        Private Function FindMapPath(ByVal folderPath As String,
                                     ByVal prefix As String,
                                     ByVal number As Integer) As String
            If String.IsNullOrWhiteSpace(folderPath) OrElse Not IO.Directory.Exists(folderPath) Then
                Return String.Empty
            End If

            Dim mapName As String = prefix & number & ".map"
            Dim directPath As String = Path.Combine(folderPath, mapName)
            If File.Exists(directPath) Then Return directPath

            Return IO.Directory.EnumerateFiles(folderPath, "*.map").FirstOrDefault(
                Function(candidate) String.Equals(
                    Path.GetFileName(candidate), mapName, StringComparison.OrdinalIgnoreCase))
        End Function

        Private Sub ResetMapTabs()
            Dim tabLabels As Label() = GetMapTabLabels()
            For index As Integer = 0 To tabLabels.Length - 1
                _mapTabPaths(index) = String.Empty
                Dim tab As Label = tabLabels(index)
                tab.Text = "Nada"
                tab.Visible = False
                tab.Enabled = False
                tab.BackColor = Color.White
            Next
        End Sub

        Private Function GetMapTabLabels() As Label()
            Return {
                MapPest1, MapPest2, MapPest3, MapPest4, MapPest5,
                MapPest6, MapPest7, MapPest8, MapPest9, MapPest10,
                MapPest11, MapPest12, MapPest13}
        End Function

        Private Sub LoadMapInfo(ByVal mapPath As String)
            SetDefaultMapInfo()
            Dim infoPath As String = Path.ChangeExtension(mapPath, ".dat")
            If Not File.Exists(infoPath) Then
                lblMapVersion.Text = _document.MapVersion.ToString()
                Return
            End If

            Try
                Dim info As MapInfoRecord = New MapInfoFile().Load(infoPath)
                MapInfo = info
                lblMapNombre.Text = If(String.IsNullOrWhiteSpace(info.Name),
                                       Path.GetFileNameWithoutExtension(mapPath), info.Name)
                lblMapMusica.Text = If(String.IsNullOrWhiteSpace(info.Music), "0", info.Music)
                lblMapVersion.Text = _document.MapVersion.ToString()
                lblMapNivelMin.Text = info.MinLevel.ToString()
            Catch ex As Exception
                _startupMessage &= " - información del mapa no cargada: " & ex.Message
                lblMapVersion.Text = _document.MapVersion.ToString()
            End Try
        End Sub

        Private Sub SetDefaultMapInfo()
            If String.IsNullOrWhiteSpace(MapInfo.Name) Then
                MapInfo = New MapInfoRecord With {
                    .Music = "0",
                    .Name = "Nuevo Mapa",
                    .MapVersion = If(_document Is Nothing, CShort(0), _document.MapVersion),
                    .Pk = True,
                    .Terreno = "BOSQUE",
                    .Zona = "CAMPO",
                    .Restringir = "No"
                }
            End If

            lblMapNombre.Text = "Nuevo Mapa"
            lblMapMusica.Text = "0"
            lblMapVersion.Text = If(_document Is Nothing, "0", _document.MapVersion.ToString())
            lblMapNivelMin.Text = "0"
        End Sub

        Private Sub ShowMapInfoDialog()
            Using dialog As New frmMapInfo(MapInfo)
                If dialog.ShowDialog(Me) = DialogResult.OK Then
                    MapInfo = dialog.EditedMapInfo
                    lblMapNombre.Text = MapInfo.Name
                    lblMapMusica.Text = MapInfo.Music
                    lblMapVersion.Text = MapInfo.MapVersion.ToString()
                    lblMapNivelMin.Text = MapInfo.MinLevel.ToString()
                    UpdateMapStatus("Información del mapa actualizada")
                End If
            End Using
        End Sub

        Private Sub MapInfo_Click(sender As Object, e As EventArgs) _
            Handles mapInfoButton.Click, mapInfoMenuItem.Click
            If _document Is Nothing Then Return
            ShowMapInfoDialog()
        End Sub

        Private Sub ApplySettingsToView()
            If _settings Is Nothing Then Return
            layer1MenuItem.Checked = True
            saveConfigMenuItem.Checked = _settings.GuardarConfig
            layer2MenuItem.Checked = _settings.VerCapa2
            layer3MenuItem.Checked = _settings.VerCapa3
            layer4MenuItem.Checked = _settings.VerCapa4
            blockedMenuItem.Checked = _settings.VerBloqueos
            npcsMenuItem.Checked = _settings.VerNpcs
            objectsMenuItem.Checked = _settings.VerObjetos
            transladosMenuItem.Checked = _settings.VerTranslados
            triggersMenuItem.Checked = _settings.VerTriggers
            gridMenuItem.Checked = _settings.VerGrilla
            mapSurface.ShowLayer1 = layer1MenuItem.Checked
            mapSurface.ShowLayer2 = layer2MenuItem.Checked
            mapSurface.ShowLayer3 = layer3MenuItem.Checked
            mapSurface.ShowLayer4 = layer4MenuItem.Checked
            SetBlockedVisibility(blockedMenuItem.Checked)
            mapSurface.ShowNpcs = npcsMenuItem.Checked
            mapSurface.ShowObjects = objectsMenuItem.Checked
            mapSurface.ShowTranslados = transladosMenuItem.Checked
            mapSurface.ShowTriggers = triggersMenuItem.Checked
            mapSurface.ShowGrid = gridMenuItem.Checked
        End Sub

        Private Sub UpdateMapStatus(Optional message As String = "")
            If Not String.IsNullOrWhiteSpace(message) Then
                statusLabel.Text = message
                Return
            End If
            If mapSurface.SelectedX = 0 Then
                statusLabel.Text = "Mapa V1 - selecciona una celda"
                Return
            End If

            Dim cell As MapV1Cell = _document.Cells(mapSurface.SelectedX, mapSurface.SelectedY)
            statusLabel.Text = $"X={mapSurface.SelectedX} Y={mapSurface.SelectedY} GRH={cell.Graphic1} Bloqueado={cell.Blocked} " &
                               $"Translado={cell.ExitMap},{cell.ExitX},{cell.ExitY} Trigger={cell.Trigger}"
        End Sub

        Private Sub COPIAR_GRH3_Click(sender As Object, e As EventArgs) Handles COPIAR_GRH3.Click

        End Sub

        'El diseñador conserva este evento del formulario original.
        'No añade comportamiento: las acciones se gestionan en cada elemento del menú.
        Private Sub menuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles menuStrip1.ItemClicked
        End Sub
    End Class
End Namespace
