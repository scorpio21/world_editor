Imports WorldEditorNet.WorldEditorNet.Core
Imports System.IO
Imports System.Windows.Forms

Namespace WorldEditorNet
    Public Partial Class MainForm
        Private _document As MapV1Document
        Private _mapPath As String = String.Empty
        Private _settings As WorldEditorSettings
        Private _graphicsIndex As GraficosIndReader
        Private _surfaceSettings As New SurfacePatternSettings()
        Private _startupMessage As String = String.Empty

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadSettings()
            LoadGraphicsIndex()
            CreateNewMap()
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
            Try
                WorldEditorSettingsSaver.Save(AppContext.BaseDirectory, _settings)
            Catch ex As Exception
                statusLabel.Text = "No se pudo guardar la configuración: " & ex.Message
            End Try
        End Sub

        Private Sub LoadSettings()
            Try
                Dim loader As New WorldEditorSettingsLoader()
                _settings = loader.Load(AppContext.BaseDirectory)

                If _settings.ConfigurationMissing Then
                    _startupMessage = "Nuevo mapa - configuración predeterminada"
                Else
                    _startupMessage = "Nuevo mapa - configuración cargada"
                End If
            Catch ex As Exception
                ' El editor puede abrirse aunque todavía no estén instalados los
                ' recursos gráficos o las rutas históricas del INI.
                _settings = Nothing
                _startupMessage = "Nuevo mapa - configuración pendiente: " & ex.Message
            End Try
        End Sub

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
            CreateNewMap()
        End Sub

        Private Sub OpenMapMenuItem_Click(sender As Object, e As EventArgs) Handles openMapMenuItem.Click
            If Not ConfirmSaveChanges() Then Return
            If openMapDialog.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                Dim codec As New MapV1Codec()
                _document = codec.Load(openMapDialog.FileName)
                _mapPath = openMapDialog.FileName
                mapSurface.Document = _document
                DisableBrush()
                undoMenuItem.Enabled = False
                UpdateMapStatus()
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message, "No se pudo abrir el mapa",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

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
                UpdateMapStatus("Mapa guardado")
                Return True
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message, "No se pudo guardar el mapa",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Function

        Private Function ConfirmSaveChanges() As Boolean
            If _document Is Nothing OrElse Not _document.Changed Then Return True

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
            ToggleViewItem(blockedMenuItem, Sub(value) mapSurface.ShowBlocked = value)
        End Sub

        Private Sub GridMenuItem_Click(sender As Object, e As EventArgs) Handles gridMenuItem.Click
            ToggleViewItem(gridMenuItem, Sub(value) mapSurface.ShowGrid = value)
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

        Private Sub MapSurface_CellSelected(sender As Object, e As EventArgs) Handles mapSurface.CellSelected
            UpdateMapStatus()
        End Sub

        Private Sub MapSurface_CellEdited(sender As Object, e As EventArgs) Handles mapSurface.CellEdited
            UpdateMapStatus("Mapa modificado")
        End Sub

        Private Sub CreateNewMap()
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
            mapSurface.Document = _document
            DisableBrush()
            undoMenuItem.Enabled = False
            mapSurface.GraphicsIndex = _graphicsIndex
            If _settings IsNot Nothing Then mapSurface.GraphicsDirectory = _settings.DirGraficos
            ApplySettingsToView()
            mapSurface.ResetSelection()
            UpdateMapStatus(If(String.IsNullOrWhiteSpace(_startupMessage), "Nuevo mapa", _startupMessage))
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
            mapSurface.ShowBlocked = blockedMenuItem.Checked
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
            statusLabel.Text = $"X={mapSurface.SelectedX} Y={mapSurface.SelectedY} GRH={cell.Graphic1} Bloqueado={cell.Blocked} Trigger={cell.Trigger}"
        End Sub
    End Class
End Namespace
