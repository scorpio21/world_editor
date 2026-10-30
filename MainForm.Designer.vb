Imports System.Drawing
Imports System.Windows.Forms

Namespace WorldEditorNet
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class MainForm
        Inherits System.Windows.Forms.Form

        Private components As System.ComponentModel.IContainer
        Friend WithEvents menuStrip1 As MenuStrip
        Friend WithEvents fileMenuItem As ToolStripMenuItem
        Friend WithEvents newMapMenuItem As ToolStripMenuItem
        Friend WithEvents openMapMenuItem As ToolStripMenuItem
        Friend WithEvents saveMapMenuItem As ToolStripMenuItem
        Friend WithEvents exitMenuItem As ToolStripMenuItem
        Friend WithEvents viewMenuItem As ToolStripMenuItem
        Friend WithEvents layer1MenuItem As ToolStripMenuItem
        Friend WithEvents layer2MenuItem As ToolStripMenuItem
        Friend WithEvents layer3MenuItem As ToolStripMenuItem
        Friend WithEvents layer4MenuItem As ToolStripMenuItem
        Friend WithEvents blockedMenuItem As ToolStripMenuItem
        Friend WithEvents gridMenuItem As ToolStripMenuItem
        Friend WithEvents mapScrollPanel As Panel
        Friend WithEvents mapSurface As MapSurfaceControl
        Friend WithEvents statusStrip1 As StatusStrip
        Friend WithEvents statusLabel As ToolStripStatusLabel
        Friend WithEvents openMapDialog As OpenFileDialog
        Friend WithEvents saveMapDialog As SaveFileDialog

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
            Me.menuStrip1 = New MenuStrip()
            Me.fileMenuItem = New ToolStripMenuItem()
            Me.newMapMenuItem = New ToolStripMenuItem()
            Me.openMapMenuItem = New ToolStripMenuItem()
            Me.saveMapMenuItem = New ToolStripMenuItem()
            Me.exitMenuItem = New ToolStripMenuItem()
            Me.viewMenuItem = New ToolStripMenuItem()
            Me.layer1MenuItem = New ToolStripMenuItem()
            Me.layer2MenuItem = New ToolStripMenuItem()
            Me.layer3MenuItem = New ToolStripMenuItem()
            Me.layer4MenuItem = New ToolStripMenuItem()
            Me.blockedMenuItem = New ToolStripMenuItem()
            Me.gridMenuItem = New ToolStripMenuItem()
            Me.mapScrollPanel = New Panel()
            Me.mapSurface = New MapSurfaceControl()
            Me.statusStrip1 = New StatusStrip()
            Me.statusLabel = New ToolStripStatusLabel()
            Me.openMapDialog = New OpenFileDialog()
            Me.saveMapDialog = New SaveFileDialog()
            Me.menuStrip1.SuspendLayout()
            Me.mapScrollPanel.SuspendLayout()
            Me.statusStrip1.SuspendLayout()
            Me.SuspendLayout()

            Me.menuStrip1.Items.AddRange(New ToolStripItem() {Me.fileMenuItem, Me.viewMenuItem})
            Me.menuStrip1.Location = New Point(0, 0)
            Me.menuStrip1.Name = "menuStrip1"
            Me.menuStrip1.Size = New Size(1100, 24)
            Me.menuStrip1.TabIndex = 0

            Me.fileMenuItem.DropDownItems.AddRange(New ToolStripItem() {
                Me.newMapMenuItem, Me.openMapMenuItem, Me.saveMapMenuItem,
                New ToolStripSeparator(), Me.exitMenuItem})
            Me.fileMenuItem.Text = "Archivo"
            Me.newMapMenuItem.Text = "Nuevo mapa"
            Me.openMapMenuItem.Text = "Abrir mapa..."
            Me.saveMapMenuItem.Text = "Guardar mapa"
            Me.exitMenuItem.Text = "Salir"

            Me.viewMenuItem.DropDownItems.AddRange(New ToolStripItem() {
                Me.layer1MenuItem, Me.layer2MenuItem, Me.layer3MenuItem,
                Me.layer4MenuItem, New ToolStripSeparator(),
                Me.blockedMenuItem, Me.gridMenuItem})
            Me.viewMenuItem.Text = "Ver"
            Me.layer1MenuItem.Text = "Capa 1 (piso)"
            Me.layer2MenuItem.Text = "Capa 2"
            Me.layer3MenuItem.Text = "Capa 3"
            Me.layer4MenuItem.Text = "Capa 4"
            Me.blockedMenuItem.Text = "Bloqueos"
            Me.gridMenuItem.Text = "Cuadrícula"

            Me.mapScrollPanel.AutoScroll = True
            Me.mapScrollPanel.BackColor = Color.DimGray
            Me.mapScrollPanel.Controls.Add(Me.mapSurface)
            Me.mapScrollPanel.Dock = DockStyle.Fill
            Me.mapScrollPanel.Location = New Point(0, 24)
            Me.mapScrollPanel.Name = "mapScrollPanel"
            Me.mapScrollPanel.Padding = New Padding(8)
            Me.mapScrollPanel.TabIndex = 1

            Me.mapSurface.Location = New Point(8, 8)
            Me.mapSurface.Name = "mapSurface"
            Me.mapSurface.TabIndex = 0

            Me.statusStrip1.Items.AddRange(New ToolStripItem() {Me.statusLabel})
            Me.statusStrip1.Location = New Point(0, 578)
            Me.statusStrip1.Name = "statusStrip1"
            Me.statusStrip1.Size = New Size(1100, 22)
            Me.statusLabel.Text = "Listo"

            Me.openMapDialog.Filter = "Mapas V1 (*.map)|*.map|Todos los archivos (*.*)|*.*"
            Me.saveMapDialog.DefaultExt = "map"
            Me.saveMapDialog.Filter = "Mapas V1 (*.map)|*.map|Todos los archivos (*.*)|*.*"

            Me.AutoScaleDimensions = New SizeF(7.0F, 15.0F)
            Me.AutoScaleMode = AutoScaleMode.Font
            Me.ClientSize = New Size(1100, 600)
            Me.Controls.Add(Me.mapScrollPanel)
            Me.Controls.Add(Me.statusStrip1)
            Me.Controls.Add(Me.menuStrip1)
            Me.MainMenuStrip = Me.menuStrip1
            Me.Name = "MainForm"
            Me.Text = "WorldEditor .NET - V1"
            Me.menuStrip1.ResumeLayout(False)
            Me.menuStrip1.PerformLayout()
            Me.mapScrollPanel.ResumeLayout(False)
            Me.statusStrip1.ResumeLayout(False)
            Me.statusStrip1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub
    End Class
End Namespace
