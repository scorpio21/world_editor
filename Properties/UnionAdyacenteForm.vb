
Imports WorldEditorNet.WorldEditorNet.Core
Imports System.Globalization
Imports System.Windows.Forms

Partial Public Class UnionAdyacenteForm
    Private ReadOnly _document As MapV1Document

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal document As MapV1Document)
        Me.New()
        _document = document
    End Sub

    Private Sub UnionAdyacenteForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _document Is Nothing Then Return

        Dim options = CreateOptionsFromControls()
        Dim maps = AdjacentTransferService.ReadExistingMaps(_document, options)
        Mapa0TextBox.Text = maps(0).ToString(CultureInfo.InvariantCulture)
        Mapa1TextBox.Text = maps(1).ToString(CultureInfo.InvariantCulture)
        Mapa2TextBox.Text = maps(2).ToString(CultureInfo.InvariantCulture)
        Mapa3TextBox.Text = maps(3).ToString(CultureInfo.InvariantCulture)
        Aplicar0CheckBox.Checked = False
        Aplicar1CheckBox.Checked = False
        Aplicar2CheckBox.Checked = False
        Aplicar3CheckBox.Checked = False
    End Sub

    Private Sub cmdDefault_Click(sender As Object, e As EventArgs) Handles cmdDefault.Click
        PosLim_0.Text = "91"
        PosLim_1.Text = "10"
        PosLim_2.Text = "90"
        PosLim_3.Text = "11"
        PosLim_4.Text = "90"
        PosLim_5.Text = "11"
        PosLim_6.Text = "12"
        PosLim_7.Text = "89"
    End Sub

    Private Sub cmdCancelar_Click(sender As Object, e As EventArgs) Handles cmdCancelar.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub cmdAplicar_Click(sender As Object, e As EventArgs) Handles cmdAplicar.Click
        If _document Is Nothing Then
            MessageBox.Show(Me, "No hay ningún mapa cargado.", "Unir mapas adyacentes", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            Dim options = CreateOptionsFromControls()
            If Not options.ApplyNorth AndAlso Not options.ApplyEast AndAlso
               Not options.ApplySouth AndAlso Not options.ApplyWest Then
                MessageBox.Show(Me, "Selecciona al menos un borde para aplicar.", "Unir mapas adyacentes", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            AdjacentTransferService.Apply(_document, options)
            DialogResult = DialogResult.OK
            Close()
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "No se pudo aplicar la unión", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Function CreateOptionsFromControls() As AdjacentTransferOptions
        Dim options As New AdjacentTransferOptions With {
            .NorthMap = ReadMap(Mapa0TextBox),
            .EastMap = ReadMap(Mapa1TextBox),
            .SouthMap = ReadMap(Mapa2TextBox),
            .WestMap = ReadMap(Mapa3TextBox),
            .ApplyNorth = Aplicar0CheckBox.Checked,
            .ApplyEast = Aplicar1CheckBox.Checked,
            .ApplySouth = Aplicar2CheckBox.Checked,
            .ApplyWest = Aplicar3CheckBox.Checked,
            .MaxY = ReadLimit(PosLim_0),
            .MinY = ReadLimit(PosLim_1),
            .MaxX = ReadLimit(PosLim_2),
            .MinX = ReadLimit(PosLim_3),
            .NorthDestinationY = ReadLimit(PosLim_4),
            .SouthDestinationY = ReadLimit(PosLim_5),
            .EastDestinationX = ReadLimit(PosLim_6),
            .WestDestinationX = ReadLimit(PosLim_7)
        }
        Return options
    End Function

    Private Shared Function ReadMap(control As TextBox) As Integer
        Dim value As Integer
        If Not Integer.TryParse(control.Text.Trim(), value) OrElse value < 0 Then
            Throw New ArgumentException("Los mapas deben ser números iguales o superiores a 0.")
        End If
        Return value
    End Function

    Private Shared Function ReadLimit(control As TextBox) As Integer
        Dim value As Integer
        If Not Integer.TryParse(control.Text.Trim(), value) Then
            Throw New ArgumentException("Los límites deben ser números enteros.")
        End If
        Return value
    End Function
End Class
