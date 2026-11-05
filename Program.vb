Imports System
Imports System.Diagnostics
Imports System.Threading
Imports System.Windows.Forms

Namespace WorldEditorNet

    Public Module Program
        <STAThread>
        Public Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WorldEditorApplicationContext())
        End Sub
    End Module

    Friend NotInheritable Class WorldEditorApplicationContext
        Inherits ApplicationContext

        Private ReadOnly _loadingForm As frmCargando
        Private _mainForm As MainForm
        Private ReadOnly _loadingStartedAt As Stopwatch = Stopwatch.StartNew()

        Public Sub New()
            _loadingForm = New frmCargando()
            _loadingForm.Prepare("v" & Application.ProductVersion)
            AddHandler _loadingForm.Shown, AddressOf LoadingForm_Shown
            _loadingForm.Show()
        End Sub

        Private Sub LoadingForm_Shown(sender As Object, e As EventArgs)
            Application.DoEvents()
            While _loadingStartedAt.ElapsedMilliseconds < 500
                Application.DoEvents()
                Thread.Sleep(10)
            End While
            _mainForm = New MainForm(_loadingForm)
            Me.MainForm = _mainForm
            AddHandler _mainForm.FormClosed, AddressOf MainForm_FormClosed
            _mainForm.Show()
        End Sub

        Private Sub MainForm_FormClosed(sender As Object, e As FormClosedEventArgs)
            ExitThread()
        End Sub
    End Class
End Namespace
