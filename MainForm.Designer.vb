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
        Friend WithEvents blockAllMenuItem As ToolStripMenuItem
        Friend WithEvents unblockAllMenuItem As ToolStripMenuItem
        Friend WithEvents fillLayer1MenuItem As ToolStripMenuItem
        Friend WithEvents fillLayer2MenuItem As ToolStripMenuItem
        Friend WithEvents fillLayer3MenuItem As ToolStripMenuItem
        Friend WithEvents fillLayer4MenuItem As ToolStripMenuItem
        Friend WithEvents paintSelectedMenuItem As ToolStripMenuItem
        Friend WithEvents brushMenuItem As ToolStripMenuItem
        Friend WithEvents viewMenuItem As ToolStripMenuItem
        Friend WithEvents panelsMenuItem As ToolStripMenuItem
        Friend WithEvents optionsMenuItem As ToolStripMenuItem
        Friend WithEvents helpMenuItem As ToolStripMenuItem
        Friend WithEvents screenMenuItem As ToolStripMenuItem
        Friend WithEvents saveConfigMenuItem As ToolStripMenuItem
        Friend WithEvents surfaceConfigMenuItem As ToolStripMenuItem
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
            Me.blockAllMenuItem = New ToolStripMenuItem()
            Me.unblockAllMenuItem = New ToolStripMenuItem()
            Me.fillLayer1MenuItem = New ToolStripMenuItem()
            Me.fillLayer2MenuItem = New ToolStripMenuItem()
            Me.fillLayer3MenuItem = New ToolStripMenuItem()
            Me.fillLayer4MenuItem = New ToolStripMenuItem()
            Me.paintSelectedMenuItem = New ToolStripMenuItem()
            Me.brushMenuItem = New ToolStripMenuItem()
            Me.viewMenuItem = New ToolStripMenuItem()
            Me.panelsMenuItem = New ToolStripMenuItem()
            Me.optionsMenuItem = New ToolStripMenuItem()
            Me.helpMenuItem = New ToolStripMenuItem()
            Me.screenMenuItem = New ToolStripMenuItem()
            Me.saveConfigMenuItem = New ToolStripMenuItem()
            Me.surfaceConfigMenuItem = New ToolStripMenuItem()
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

            Dim reopenMapMenuItem As New ToolStripMenuItem("&Re-Abrir Mapa") With {.Enabled = False}
            Dim newFormatMenuItem As New ToolStripMenuItem("Utilizar &Nuevo Formato al Guardar") With {.Enabled = False}
            Dim saveMapAsMenuItem As New ToolStripMenuItem("Guardar Mapa &como...") With {.Enabled = False}
            Dim saveBmpMenuItem As New ToolStripMenuItem("Guardar como &BMP") With {.Enabled = False}
            Dim saveJpgMenuItem As New ToolStripMenuItem("Guardar como &JPG") With {.Enabled = False}
            Dim useUndoMenuItem As New ToolStripMenuItem("&Utilizar Deshacer") With {.Checked = True, .Enabled = False}
            Dim mapInfoMenuItem As New ToolStripMenuItem("&Información del Mapa") With {.Enabled = False}
            Dim insertMenuItem As New ToolStripMenuItem("&Insertar")
            Dim removeMenuItem As New ToolStripMenuItem("&Quitar")
            Dim functionsMenuItem As New ToolStripMenuItem("&Funciones")
            Dim autoCompleteMenuItem As New ToolStripMenuItem("Auto-Completar &Superficies") With {.Enabled = False}
            Dim autoCaptureSurfaceMenuItem As New ToolStripMenuItem("Auto-C&apturar información de la Superficie") With {.Enabled = False}
            Dim autoCaptureTransfersMenuItem As New ToolStripMenuItem("Auto-&Capturar información de los Translados") With {.Checked = True, .Enabled = False}
            Dim autoSaveMapsMenuItem As New ToolStripMenuItem("Configuración de Auto-&Guardar Mapas") With {.Enabled = False}

            Me.menuStrip1.Items.AddRange(New ToolStripItem() {Me.fileMenuItem, Me.editMenuItem,
                Me.viewMenuItem, Me.panelsMenuItem, Me.optionsMenuItem, Me.helpMenuItem, Me.screenMenuItem})
            Me.menuStrip1.Location = New Point(0, 0)
            Me.menuStrip1.Name = "menuStrip1"
            Me.menuStrip1.Size = New Size(1100, 24)
            Me.menuStrip1.TabIndex = 0

            Me.fileMenuItem.Text = "&Archivo"
            Me.newMapMenuItem.Text = "&Nuevo Mapa"
            Me.newMapMenuItem.ShortcutKeys = Keys.Control Or Keys.N
            Me.openMapMenuItem.Text = "&Abrir Mapa"
            Me.openMapMenuItem.ShortcutKeys = Keys.Control Or Keys.A
            Me.saveMapMenuItem.Text = "&Guardar Mapa"
            Me.saveMapMenuItem.ShortcutKeys = Keys.Control Or Keys.G
            Me.exitMenuItem.Text = "&Salir"
            Me.fileMenuItem.DropDownItems.AddRange(New ToolStripItem() {
                New ToolStripSeparator(), Me.newMapMenuItem, Me.openMapMenuItem, New ToolStripSeparator(), reopenMapMenuItem,
                New ToolStripSeparator(), newFormatMenuItem, New ToolStripSeparator(), Me.saveMapMenuItem,
                saveMapAsMenuItem, New ToolStripSeparator(), saveBmpMenuItem, saveJpgMenuItem,
                New ToolStripSeparator(), Me.exitMenuItem, New ToolStripSeparator()})

            Me.editMenuItem.Text = "&Edición"
            Me.undoMenuItem.Text = "&Deshacer"
            Me.undoMenuItem.ShortcutKeys = Keys.Control Or Keys.Z
            Me.undoMenuItem.Enabled = False
            Me.surfaceConfigMenuItem.Text = "Configuración A&vanzada de Superficie"
            Dim insertAdjacentMenuItem As New ToolStripMenuItem("&Translados a Mapas Adyasentes") With {.Enabled = False}
            Dim insertRandomMenuItem As New ToolStripMenuItem("Superficie al &Azar") With {.Enabled = False}
            Dim insertBordersMenuItem As New ToolStripMenuItem("Superficie en los &Bordes del Mapa") With {.Enabled = False}
            Dim insertAllMenuItem As New ToolStripMenuItem("Superficie en Todo el Mapa") With {.Enabled = False}
            Dim insertBlockBordersMenuItem As New ToolStripMenuItem("Bloqueo en &Bordes del Mapa") With {.Enabled = False}
            Dim insertBlockMapMenuItem As New ToolStripMenuItem("Bloqueo en &Todo el Mapa") With {.Enabled = False}
            insertMenuItem.DropDownItems.AddRange(New ToolStripItem() {insertAdjacentMenuItem, insertRandomMenuItem,
                insertBordersMenuItem, insertAllMenuItem, insertBlockBordersMenuItem, insertBlockMapMenuItem})
            Dim removeTransfersMenuItem As New ToolStripMenuItem("Todos los &Translados") With {.Enabled = False}
            Dim removeBlocksMenuItem As ToolStripMenuItem = Me.unblockAllMenuItem
            removeBlocksMenuItem.Text = "Todos los &Bloqueos"
            Dim removeNpcsMenuItem As New ToolStripMenuItem("Todos los &NPC's") With {.Enabled = False}
            Dim removeHostileMenuItem As New ToolStripMenuItem("Todos los NPC's &Hostiles") With {.Enabled = False}
            Dim removeObjectsMenuItem As New ToolStripMenuItem("Todos los &Objetos") With {.Enabled = False}
            Dim removeTriggersMenuItem As New ToolStripMenuItem("Todos los Tri&gger's") With {.Enabled = False}
            Dim removeSurfaceBordersMenuItem As New ToolStripMenuItem("Superficie de los B&ordes") With {.Enabled = False}
            Dim removeSelectedLayerMenuItem As New ToolStripMenuItem("Superficie de la &Capa Seleccionada") With {.Enabled = False}
            Dim removeAllMenuItem As New ToolStripMenuItem("TODO") With {.Enabled = False}
            removeMenuItem.DropDownItems.AddRange(New ToolStripItem() {removeTransfersMenuItem, removeBlocksMenuItem,
                removeNpcsMenuItem, removeHostileMenuItem, removeObjectsMenuItem, removeTriggersMenuItem,
                removeSurfaceBordersMenuItem, removeSelectedLayerMenuItem, New ToolStripSeparator(), removeAllMenuItem})
            Dim removeFunctionsMenuItem As New ToolStripMenuItem("&Quitar Funciones") With {.Enabled = False}
            Dim autoRemoveFunctionsMenuItem As New ToolStripMenuItem("Auto-&Quitar Funciones") With {.Checked = True, .Enabled = False}
            functionsMenuItem.DropDownItems.AddRange(New ToolStripItem() {removeFunctionsMenuItem, autoRemoveFunctionsMenuItem})
            Me.editMenuItem.DropDownItems.AddRange(New ToolStripItem() {Me.undoMenuItem, useUndoMenuItem,
                mapInfoMenuItem, New ToolStripSeparator(), insertMenuItem, removeMenuItem,
                New ToolStripSeparator(), functionsMenuItem, Me.surfaceConfigMenuItem,
                New ToolStripSeparator(), autoCompleteMenuItem, autoCaptureSurfaceMenuItem,
                autoCaptureTransfersMenuItem, autoSaveMapsMenuItem})

            Me.viewMenuItem.Text = "&Ver"
            Dim viewLayersMenuItem As New ToolStripMenuItem("...&Capas")
            Me.layer1MenuItem.Text = "Capa &1 (Piso)"
            Me.layer2MenuItem.Text = "Capa &2 (costas, etc)"
            Me.layer3MenuItem.Text = "Capa &3 (arboles, etc)"
            Me.layer4MenuItem.Text = "Capa &4 (techos, etc)"
            viewLayersMenuItem.DropDownItems.AddRange(New ToolStripItem() {Me.layer1MenuItem, Me.layer2MenuItem, Me.layer3MenuItem, Me.layer4MenuItem})
            Me.transladosMenuItem.Text = "...&Translados"
            Me.blockedMenuItem.Text = "...&Bloqueos"
            Me.npcsMenuItem.Text = "...&NPC's"
            Me.objectsMenuItem.Text = "...&Objetos"
            Me.triggersMenuItem.Text = "...Tri&gger's"
            Me.gridMenuItem.Text = "...Gri&lla"
            Dim automaticViewMenuItem As New ToolStripMenuItem("Control &Automaticamente") With {.Checked = True, .Enabled = False}
            Me.viewMenuItem.DropDownItems.AddRange(New ToolStripItem() {viewLayersMenuItem, Me.transladosMenuItem,
                Me.blockedMenuItem, Me.npcsMenuItem, Me.objectsMenuItem, Me.triggersMenuItem, Me.gridMenuItem,
                New ToolStripSeparator(), automaticViewMenuItem})

            Me.panelsMenuItem.Text = "&Paneles"
            Dim panelSurfaceMenuItem As New ToolStripMenuItem("&Superficie") With {.ShortcutKeys = Keys.F5, .Enabled = False}
            Dim panelTransfersMenuItem As New ToolStripMenuItem("&Translados") With {.ShortcutKeys = Keys.F6, .Enabled = False}
            Dim panelBlockMenuItem As New ToolStripMenuItem("&Bloquear") With {.ShortcutKeys = Keys.F7, .Enabled = False}
            Dim panelNpcsMenuItem As New ToolStripMenuItem("&NPC's") With {.ShortcutKeys = Keys.F8, .Enabled = False}
            Dim panelHostileMenuItem As New ToolStripMenuItem("NPC's &Hostiles") With {.ShortcutKeys = Keys.F9, .Enabled = False}
            Dim panelObjectsMenuItem As New ToolStripMenuItem("&Objetos") With {.ShortcutKeys = Keys.F11, .Enabled = False}
            Dim panelTriggersMenuItem As New ToolStripMenuItem("Tri&gger's") With {.ShortcutKeys = Keys.F12, .Enabled = False}
            Me.panelsMenuItem.DropDownItems.AddRange(New ToolStripItem() {panelSurfaceMenuItem, panelTransfersMenuItem,
                panelBlockMenuItem, panelNpcsMenuItem, panelHostileMenuItem, panelObjectsMenuItem, panelTriggersMenuItem,
                New ToolStripSeparator(), New ToolStripMenuItem("Ocultar Superficie") With {.Enabled = False},
                New ToolStripMenuItem("Ocultar Translados") With {.Enabled = False}, New ToolStripMenuItem("Ocultar Bloquear") With {.Enabled = False},
                New ToolStripMenuItem("Ocultar NPC's") With {.Enabled = False}, New ToolStripMenuItem("Ocultar NPC's Hostiles") With {.Enabled = False},
                New ToolStripMenuItem("Ocultar Objetos") With {.Enabled = False}, New ToolStripMenuItem("Ocultar Trigger's") With {.Enabled = False}})

            Me.optionsMenuItem.Text = "&Opciones"
            Dim reportsMenuItem As New ToolStripMenuItem("&Informes") With {.Enabled = False}
            Dim updateIndicesMenuItem As New ToolStripMenuItem("&Actualizar Indices de...") With {.Enabled = False}
            updateIndicesMenuItem.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("&Superficies"), New ToolStripMenuItem("&NPC's"), New ToolStripMenuItem("&Objetos"), New ToolStripMenuItem("&Trigger's"), New ToolStripMenuItem("C&abezas"), New ToolStripMenuItem("C&uerpos"), New ToolStripMenuItem("&Graficos")})
            Dim walkModeMenuItem As New ToolStripMenuItem("Modalidad &Caminata") With {.Enabled = False}
            Dim grhBmpMenuItem As New ToolStripMenuItem("&GRH => BMP") With {.Enabled = False}
            Dim optimizeMenuItem As New ToolStripMenuItem("Optimi&zar Mapa") With {.ShortcutKeys = Keys.Control Or Keys.O, .Enabled = False}
            Me.saveConfigMenuItem.Text = "&Guardar Ultima Configuración"
            Me.optionsMenuItem.DropDownItems.AddRange(New ToolStripItem() {reportsMenuItem, updateIndicesMenuItem,
                walkModeMenuItem, grhBmpMenuItem, New ToolStripSeparator(), optimizeMenuItem,
                New ToolStripSeparator(), Me.saveConfigMenuItem})

            Me.helpMenuItem.Text = "Ay&uda"
            Me.helpMenuItem.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("&Manual") With {.ShortcutKeys = Keys.F1, .Enabled = False}, New ToolStripSeparator(), New ToolStripMenuItem("&Acerca de...") With {.Enabled = False}})
            Me.screenMenuItem.Text = "&Pantalla"
            Dim horizontalMenuItem As New ToolStripMenuItem("&Tamaño Horizontal")
            horizontalMenuItem.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("&Chico") With {.Checked = True, .Enabled = False}, New ToolStripMenuItem("&Mediano") With {.Enabled = False}, New ToolStripMenuItem("&Grande") With {.Enabled = False}})
            Dim verticalMenuItem As New ToolStripMenuItem("&Tamaño Vertical")
            verticalMenuItem.DropDownItems.AddRange(New ToolStripItem() {New ToolStripMenuItem("&Chico") With {.Checked = True, .Enabled = False}, New ToolStripMenuItem("&Mediano") With {.Enabled = False}, New ToolStripMenuItem("&Grande") With {.Enabled = False}})
            Me.screenMenuItem.DropDownItems.AddRange(New ToolStripItem() {horizontalMenuItem, verticalMenuItem})

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
