Imports WorldEditorNet.WorldEditorNet.Core
Imports System.Windows.Forms

Namespace WorldEditorNet
    Public Partial Class MainForm
        Private _document As MapV1Document
        Private _mapPath As String = String.Empty

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            CreateNewMap()
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
            mapSurface.ResetSelection()
            UpdateMapStatus("Nuevo mapa")
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
