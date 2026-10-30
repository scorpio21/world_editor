Imports WorldEditorNet.WorldEditorNet.Core
Imports System.IO
Imports System.Windows.Forms

Namespace WorldEditorNet
    Public Partial Class MainForm
        Private _document As MapV1Document
        Private _mapPath As String = String.Empty
        Private _settings As WorldEditorSettings
        Private _graphicsIndex As GraficosIndReader
        Private _startupMessage As String = String.Empty

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadSettings()
            LoadGraphicsIndex()
            CreateNewMap()
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
            CreateNewMap()
        End Sub

        Private Sub OpenMapMenuItem_Click(sender As Object, e As EventArgs) Handles openMapMenuItem.Click
            If openMapDialog.ShowDialog(Me) <> DialogResult.OK Then Return
            Try
                Dim codec As New MapV1Codec()
                _document = codec.Load(openMapDialog.FileName)
                _mapPath = openMapDialog.FileName
                mapSurface.Document = _document
                UpdateMapStatus()
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message, "No se pudo abrir el mapa",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub SaveMapMenuItem_Click(sender As Object, e As EventArgs) Handles saveMapMenuItem.Click
            If _document Is Nothing Then Return
            If String.IsNullOrWhiteSpace(_mapPath) Then
                If saveMapDialog.ShowDialog(Me) <> DialogResult.OK Then Return
                _mapPath = saveMapDialog.FileName
            End If

            Try
                Dim codec As New MapV1Codec()
                codec.Save(_mapPath, _document)
                UpdateMapStatus("Mapa guardado")
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message, "No se pudo guardar el mapa",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub ExitMenuItem_Click(sender As Object, e As EventArgs) Handles exitMenuItem.Click
            Close()
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

        Private Shared Sub ToggleViewItem(item As ToolStripMenuItem, apply As Action(Of Boolean))
            item.Checked = Not item.Checked
            apply(item.Checked)
        End Sub

        Private Sub MapSurface_CellSelected(sender As Object, e As EventArgs) Handles mapSurface.CellSelected
            UpdateMapStatus()
        End Sub

        Private Sub CreateNewMap()
            _document = New MapV1Document With {
                .MapVersion = 0,
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
            mapSurface.GraphicsIndex = _graphicsIndex
            If _settings IsNot Nothing Then mapSurface.GraphicsDirectory = _settings.DirGraficos
            ApplySettingsToView()
            mapSurface.ResetSelection()
            UpdateMapStatus(If(String.IsNullOrWhiteSpace(_startupMessage), "Nuevo mapa", _startupMessage))
        End Sub

        Private Sub ApplySettingsToView()
            If _settings Is Nothing Then Return
            layer1MenuItem.Checked = True
            layer2MenuItem.Checked = _settings.VerCapa2
            layer3MenuItem.Checked = _settings.VerCapa3
            layer4MenuItem.Checked = _settings.VerCapa4
            blockedMenuItem.Checked = _settings.VerBloqueos
            gridMenuItem.Checked = _settings.VerGrilla
            mapSurface.ShowLayer1 = layer1MenuItem.Checked
            mapSurface.ShowLayer2 = layer2MenuItem.Checked
            mapSurface.ShowLayer3 = layer3MenuItem.Checked
            mapSurface.ShowLayer4 = layer4MenuItem.Checked
            mapSurface.ShowBlocked = blockedMenuItem.Checked
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
