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
        Friend WithEvents editMenuItem As ToolStripMenuItem
        Friend WithEvents undoMenuItem As ToolStripMenuItem
        Friend WithEvents toggleBlockedMenuItem As ToolStripMenuItem
        Friend WithEvents setLayer1MenuItem As ToolStripMenuItem
        Friend WithEvents setLayer2MenuItem As ToolStripMenuItem
        Friend WithEvents setLayer3MenuItem As ToolStripMenuItem
        Friend WithEvents setLayer4MenuItem As ToolStripMenuItem
        Friend WithEvents clearLayer1MenuItem As ToolStripMenuItem
        Friend WithEvents clearLayer2MenuItem As ToolStripMenuItem
        Friend WithEvents clearLayer3MenuItem As ToolStripMenuItem
        Friend WithEvents clearLayer4MenuItem As ToolStripMenuItem
        Friend WithEvents viewMenuItem As ToolStripMenuItem
        Friend WithEvents optionsMenuItem As ToolStripMenuItem
        Friend WithEvents saveConfigMenuItem As ToolStripMenuItem
        Friend WithEvents layer1MenuItem As ToolStripMenuItem
        Friend WithEvents layer2MenuItem As ToolStripMenuItem
        Friend WithEvents layer3MenuItem As ToolStripMenuItem
        Friend WithEvents layer4MenuItem As ToolStripMenuItem
        Friend WithEvents blockedMenuItem As ToolStripMenuItem
        Friend WithEvents gridMenuItem As ToolStripMenuItem
        Friend WithEvents npcsMenuItem As ToolStripMenuItem
        Friend WithEvents objectsMenuItem As ToolStripMenuItem
        Friend WithEvents triggersMenuItem As ToolStripMenuItem
        Friend WithEvents transladosMenuItem As ToolStripMenuItem
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
            Me.editMenuItem = New ToolStripMenuItem()
            Me.undoMenuItem = New ToolStripMenuItem()
            Me.toggleBlockedMenuItem = New ToolStripMenuItem()
            Me.setLayer1MenuItem = New ToolStripMenuItem()
            Me.setLayer2MenuItem = New ToolStripMenuItem()
            Me.setLayer3MenuItem = New ToolStripMenuItem()
            Me.setLayer4MenuItem = New ToolStripMenuItem()
            Me.clearLayer1MenuItem = New ToolStripMenuItem()
            Me.clearLayer2MenuItem = New ToolStripMenuItem()
            Me.clearLayer3MenuItem = New ToolStripMenuItem()
            Me.clearLayer4MenuItem = New ToolStripMenuItem()
            Me.viewMenuItem = New ToolStripMenuItem()
            Me.optionsMenuItem = New ToolStripMenuItem()
            Me.saveConfigMenuItem = New ToolStripMenuItem()
            Me.layer1MenuItem = New ToolStripMenuItem()
            Me.layer2MenuItem = New ToolStripMenuItem()
            Me.layer3MenuItem = New ToolStripMenuItem()
            Me.layer4MenuItem = New ToolStripMenuItem()
            Me.blockedMenuItem = New ToolStripMenuItem()
            Me.gridMenuItem = New ToolStripMenuItem()
            Me.npcsMenuItem = New ToolStripMenuItem()
            Me.objectsMenuItem = New ToolStripMenuItem()
            Me.triggersMenuItem = New ToolStripMenuItem()
            Me.transladosMenuItem = New ToolStripMenuItem()
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

            Me.menuStrip1.Items.AddRange(New ToolStripItem() {Me.fileMenuItem, Me.editMenuItem, Me.viewMenuItem, Me.optionsMenuItem})
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

            Me.editMenuItem.DropDownItems.AddRange(New ToolStripItem() {
                Me.undoMenuItem, New ToolStripSeparator(), Me.toggleBlockedMenuItem,
                New ToolStripSeparator(), Me.setLayer1MenuItem, Me.setLayer2MenuItem,
                Me.setLayer3MenuItem, Me.setLayer4MenuItem, New ToolStripSeparator(),
                Me.clearLayer1MenuItem, Me.clearLayer2MenuItem, Me.clearLayer3MenuItem,
                Me.clearLayer4MenuItem})
            Me.editMenuItem.Text = "Edición"
            Me.undoMenuItem.Text = "Deshacer"
            Me.undoMenuItem.Enabled = False
            Me.toggleBlockedMenuItem.Text = "Alternar bloqueo de la celda"
            Me.setLayer1MenuItem.Text = "Establecer GRH en capa 1..."
            Me.setLayer2MenuItem.Text = "Establecer GRH en capa 2..."
            Me.setLayer3MenuItem.Text = "Establecer GRH en capa 3..."
            Me.setLayer4MenuItem.Text = "Establecer GRH en capa 4..."
            Me.clearLayer1MenuItem.Text = "Borrar capa 1 completa"
            Me.clearLayer2MenuItem.Text = "Borrar capa 2 completa"
            Me.clearLayer3MenuItem.Text = "Borrar capa 3 completa"
            Me.clearLayer4MenuItem.Text = "Borrar capa 4 completa"

            Me.viewMenuItem.DropDownItems.AddRange(New ToolStripItem() {
                Me.layer1MenuItem, Me.layer2MenuItem, Me.layer3MenuItem,
                Me.layer4MenuItem, New ToolStripSeparator(),
                Me.blockedMenuItem, Me.npcsMenuItem, Me.objectsMenuItem,
                Me.transladosMenuItem, Me.triggersMenuItem, Me.gridMenuItem})
            Me.viewMenuItem.Text = "Ver"
            Me.layer1MenuItem.Text = "Capa 1 (piso)"
            Me.layer2MenuItem.Text = "Capa 2"
            Me.layer3MenuItem.Text = "Capa 3"
            Me.layer4MenuItem.Text = "Capa 4"
            Me.blockedMenuItem.Text = "Bloqueos"
            Me.npcsMenuItem.Text = "NPCs"
            Me.objectsMenuItem.Text = "Objetos"
            Me.transladosMenuItem.Text = "Translados"
            Me.triggersMenuItem.Text = "Triggers"
            Me.gridMenuItem.Text = "Cuadrícula"

            Me.optionsMenuItem.DropDownItems.Add(Me.saveConfigMenuItem)
            Me.optionsMenuItem.Text = "Opciones"
            Me.saveConfigMenuItem.Text = "Guardar última configuración"
            Me.saveConfigMenuItem.Checked = True

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
