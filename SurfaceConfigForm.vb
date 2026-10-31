Imports System
Imports System.Windows.Forms
Imports WorldEditorNet.WorldEditorNet.Core

Namespace WorldEditorNet
    Public Partial Class SurfaceConfigForm
        Public Property Settings As SurfacePatternSettings

        Public Sub New()
            InitializeComponent()
            Settings = New SurfacePatternSettings()
            LoadSettings()
        End Sub

        Private Sub LoadSettings()
            mosaicCheckBox.Checked = Settings.Mosaic
            widthTextBox.Text = Settings.Width.ToString()
            heightTextBox.Text = Settings.Height.ToString()
            offsetCheckBox.Checked = Settings.OffsetEnabled
            offsetXTextBox.Text = Settings.OffsetX.ToString()
            offsetYTextBox.Text = Settings.OffsetY.ToString()
            UpdateEnabledState()
        End Sub

        Private Sub SurfaceConfigForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadSettings()
        End Sub

        Private Sub MosaicCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles mosaicCheckBox.CheckedChanged
            UpdateEnabledState()
        End Sub

        Private Sub OffsetCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles offsetCheckBox.CheckedChanged
            UpdateEnabledState()
        End Sub

        Private Sub UpdateEnabledState()
            widthTextBox.Enabled = mosaicCheckBox.Checked
            heightTextBox.Enabled = mosaicCheckBox.Checked
            offsetCheckBox.Enabled = mosaicCheckBox.Checked
            offsetXTextBox.Enabled = mosaicCheckBox.Checked AndAlso offsetCheckBox.Checked
            offsetYTextBox.Enabled = mosaicCheckBox.Checked AndAlso offsetCheckBox.Checked
        End Sub

        Private Sub AcceptButton_Click(sender As Object, e As EventArgs) Handles acceptDialogButton.Click
            Dim width As Integer
            Dim height As Integer
            Dim offsetX As Integer
            Dim offsetY As Integer
            If Not Integer.TryParse(widthTextBox.Text, width) OrElse width < 1 Then
                warningLabel.Text = "El ancho debe ser un número mayor que cero."
                DialogResult = DialogResult.None
                Return
            End If
            If Not Integer.TryParse(heightTextBox.Text, height) OrElse height < 1 Then
                warningLabel.Text = "El largo debe ser un número mayor que cero."
                DialogResult = DialogResult.None
                Return
            End If
            If Not Integer.TryParse(offsetXTextBox.Text, offsetX) Then
                warningLabel.Text = "El desplazamiento X no es válido."
                DialogResult = DialogResult.None
                Return
            End If
            If Not Integer.TryParse(offsetYTextBox.Text, offsetY) Then
                warningLabel.Text = "El desplazamiento Y no es válido."
                DialogResult = DialogResult.None
                Return
            End If

            Settings.Mosaic = mosaicCheckBox.Checked
            Settings.Width = width
            Settings.Height = height
            Settings.OffsetEnabled = offsetCheckBox.Checked
            Settings.OffsetX = offsetX
            Settings.OffsetY = offsetY
            DialogResult = DialogResult.OK
            Close()
        End Sub
    End Class
End Namespace
