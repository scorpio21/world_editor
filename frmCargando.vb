Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class frmCargando
    Public Sub Prepare(ByVal versionText As String)
        verX.Text = versionText
        X.Text = "..."
        For Each image As PictureBox In {P1, P2, P3, P4, P5, P6}
            image.Visible = False
        Next
        For Each label As Label In {L_0, L_1, L_2, L_3, L_4, L_5}
            label.Visible = False
        Next
    End Sub

    Public Sub SetLoadingStep(ByVal stepIndex As Integer, ByVal message As String)
        X.Text = message
        If stepIndex < 0 OrElse stepIndex > 5 Then Return

        Dim images() As PictureBox = {P1, P2, P3, P4, P5, P6}
        Dim labels() As Label = {L_0, L_1, L_2, L_3, L_4, L_5}
        images(stepIndex).Visible = True
        labels(stepIndex).Visible = True
        Update()
    End Sub

    Private Sub frmCargando_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim radio As Integer = 15

        Dim path As New GraphicsPath()

        path.AddArc(0, 0, radio, radio, 180, 90)
        path.AddArc(Panel1.Width - radio, 0, radio, radio, 270, 90)
        path.AddArc(Panel1.Width - radio, Panel1.Height - radio, radio, radio, 0, 90)
        path.AddArc(0, Panel1.Height - radio, radio, radio, 90, 90)

        path.CloseFigure()

        Panel1.Region = New Region(path)

    End Sub
End Class


