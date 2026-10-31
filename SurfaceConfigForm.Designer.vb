Imports System.Drawing
Imports System.Windows.Forms

Namespace WorldEditorNet
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class SurfaceConfigForm
        Inherits Form

        Private components As System.ComponentModel.IContainer
        Friend WithEvents mosaicCheckBox As CheckBox
        Friend WithEvents widthLabel As Label
        Friend WithEvents heightLabel As Label
        Friend WithEvents widthTextBox As TextBox
        Friend WithEvents heightTextBox As TextBox
        Friend WithEvents offsetCheckBox As CheckBox
        Friend WithEvents offsetXLabel As Label
        Friend WithEvents offsetYLabel As Label
        Friend WithEvents offsetXTextBox As TextBox
        Friend WithEvents offsetYTextBox As TextBox
        Friend WithEvents acceptDialogButton As Button
        Friend WithEvents cancelDialogButton As Button
        Friend WithEvents warningLabel As Label

        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then components.Dispose()
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Me.mosaicCheckBox = New CheckBox()
            Me.widthLabel = New Label()
            Me.heightLabel = New Label()
            Me.widthTextBox = New TextBox()
            Me.heightTextBox = New TextBox()
            Me.offsetCheckBox = New CheckBox()
            Me.offsetXLabel = New Label()
            Me.offsetYLabel = New Label()
            Me.offsetXTextBox = New TextBox()
            Me.offsetYTextBox = New TextBox()
            Me.acceptDialogButton = New Button()
            Me.cancelDialogButton = New Button()
            Me.warningLabel = New Label()
            Me.SuspendLayout()

            Me.mosaicCheckBox.AutoSize = True
            Me.mosaicCheckBox.Location = New Point(18, 18)
            Me.mosaicCheckBox.Text = "Mosaico"

            Me.widthLabel.AutoSize = True
            Me.widthLabel.Location = New Point(18, 55)
            Me.widthLabel.Text = "Ancho"
            Me.widthTextBox.Location = New Point(105, 52)
            Me.widthTextBox.Size = New Size(90, 23)

            Me.heightLabel.AutoSize = True
            Me.heightLabel.Location = New Point(18, 88)
            Me.heightLabel.Text = "Largo"
            Me.heightTextBox.Location = New Point(105, 85)
            Me.heightTextBox.Size = New Size(90, 23)

            Me.offsetCheckBox.AutoSize = True
            Me.offsetCheckBox.Location = New Point(18, 125)
            Me.offsetCheckBox.Text = "Desplazamiento de mosaico"

            Me.offsetXLabel.AutoSize = True
            Me.offsetXLabel.Location = New Point(18, 160)
            Me.offsetXLabel.Text = "Desplazamiento X"
            Me.offsetXTextBox.Location = New Point(150, 157)
            Me.offsetXTextBox.Size = New Size(45, 23)

            Me.offsetYLabel.AutoSize = True
            Me.offsetYLabel.Location = New Point(215, 160)
            Me.offsetYLabel.Text = "Y"
            Me.offsetYTextBox.Location = New Point(240, 157)
            Me.offsetYTextBox.Size = New Size(45, 23)

            Me.warningLabel.AutoSize = False
            Me.warningLabel.Location = New Point(18, 195)
            Me.warningLabel.Size = New Size(330, 45)
            Me.warningLabel.Text = "Algunas superficies tienen límites de mosaico."
            Me.warningLabel.ForeColor = Color.Red

            Me.acceptDialogButton.DialogResult = DialogResult.OK
            Me.acceptDialogButton.Location = New Point(185, 255)
            Me.acceptDialogButton.Size = New Size(80, 28)
            Me.acceptDialogButton.Text = "Aceptar"
            Me.cancelDialogButton.DialogResult = DialogResult.Cancel
            Me.cancelDialogButton.Location = New Point(275, 255)
            Me.cancelDialogButton.Size = New Size(80, 28)
            Me.cancelDialogButton.Text = "Cancelar"

            Me.ClientSize = New Size(375, 300)
            Me.Controls.AddRange(New Control() {Me.mosaicCheckBox, Me.widthLabel, Me.heightLabel,
                Me.widthTextBox, Me.heightTextBox, Me.offsetCheckBox, Me.offsetXLabel,
                Me.offsetYLabel, Me.offsetXTextBox, Me.offsetYTextBox, Me.warningLabel,
                Me.acceptDialogButton, Me.cancelDialogButton})
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.StartPosition = FormStartPosition.CenterParent
            Me.Text = "Configuración avanzada de superficie"
            Me.AcceptButton = Me.acceptDialogButton
            Me.CancelButton = Me.cancelDialogButton
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub
    End Class
End Namespace
