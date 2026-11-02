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
    Friend WithEvents rightContentPanel As Panel
    Friend WithEvents leftSidebar As Panel
    Friend WithEvents mapInfoPanel As Panel
    Friend WithEvents functionContentPanel As Panel
    Friend WithEvents lblFNombreMapa As Label
    Friend WithEvents lblFMusica As Label
    Friend WithEvents lblFVersion As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblMapNombre As Label
    Friend WithEvents lblMapMusica As Label
    Friend WithEvents lblMapVersion As Label
    Friend WithEvents lblMapNivelMin As Label
    Friend WithEvents mapInfoButton As Button
    Friend WithEvents clearFunctionsButton As Button
    Friend WithEvents surfaceFunctionPanel As Panel
    Friend WithEvents surfaceListBox As ListBox
    Friend WithEvents surfaceFilterCombo As ComboBox
    Friend WithEvents layerCombo As ComboBox
    Friend WithEvents grhTextBox As TextBox
    Friend WithEvents surfaceFilterLabel As Label
    Friend WithEvents layerLabel As Label
    Friend WithEvents grhLabel As Label
    Friend WithEvents hostileNpcFilterLabel As Label
    Friend WithEvents objectFilterLabel As Label
    Friend WithEvents npcFilterCaptionLabel As Label
    Friend WithEvents npcFunctionNumberLabel As Label
    Friend WithEvents npcFunctionAmountLabel As Label
    Friend WithEvents hostileNpcFunctionNumberLabel As Label
    Friend WithEvents hostileNpcFunctionAmountLabel As Label
    Friend WithEvents objectFunctionNumberLabel As Label
    Friend WithEvents objectFunctionAmountLabel As Label
    Friend WithEvents lMapN As Label
    Friend WithEvents removeSurfaceLayerButton As Button
    Friend WithEvents removeSurfaceAllLayersButton As Button
    Friend WithEvents selectSurfaceButton As Button
    Friend WithEvents transferFunctionPanel As Panel
    Friend WithEvents transferMapTextBox As TextBox
    Friend WithEvents transferXTextBox As TextBox
    Friend WithEvents transferYTextBox As TextBox
    Friend WithEvents insertTransferButton As Button
    Friend WithEvents insertTransferObjectButton As Button
    Friend WithEvents unionManualButton As Button
    Friend WithEvents unionAutoButton As Button
    Friend WithEvents removeTransferButton As Button
    Friend WithEvents blockingFunctionPanel As Panel
    Friend WithEvents insertBlockingButton As Button
    Friend WithEvents removeBlockingButton As Button
    Friend WithEvents viewBlockingButton As Button
    Friend WithEvents npcFunctionPanel As Panel
    Friend WithEvents npcListBox As ListBox
    Friend WithEvents npcFilterCombo As ComboBox
    Friend WithEvents npcNumberCombo As ComboBox
    Friend WithEvents npcAmountCombo As ComboBox
    Friend WithEvents hostileNpcNumberCombo As ComboBox
    Friend WithEvents hostileNpcAmountCombo As ComboBox
    Friend WithEvents insertNpcFunctionButton As Button
    Friend WithEvents removeNpcFunctionButton As Button
    Friend WithEvents randomNpcFunctionButton As Button
    Friend WithEvents hostileNpcFunctionPanel As Panel
    Friend WithEvents hostileNpcListBox As ListBox
    Friend WithEvents hostileNpcFilterCombo As ComboBox
    Friend WithEvents insertHostileNpcFunctionButton As Button
    Friend WithEvents removeHostileNpcFunctionButton As Button
    Friend WithEvents randomHostileNpcFunctionButton As Button
    Friend WithEvents objectFunctionPanel As Panel
    Friend WithEvents objectListBox As ListBox
    Friend WithEvents objectFilterCombo As ComboBox
    Friend WithEvents objectNumberCombo As ComboBox
    Friend WithEvents objectAmountCombo As ComboBox
    Friend WithEvents insertObjectFunctionButton As Button
    Friend WithEvents removeObjectFunctionButton As Button
    Friend WithEvents randomObjectFunctionButton As Button
    Friend WithEvents triggerFunctionPanel As Panel
    Friend WithEvents triggerListBox As ListBox
    Friend WithEvents insertTriggerButton As Button
    Friend WithEvents removeTriggerButton As Button
    Friend WithEvents viewTriggerButton As Button
    Friend WithEvents radarPanel As Panel
    Friend WithEvents radarPictureBox As PictureBox
    Friend WithEvents FPS As Label
    Friend WithEvents POSX As Label
    Friend WithEvents Shape1 As Panel
    Friend WithEvents ApuntadorRadar As Panel
    Friend WithEvents lXhor As Label
    Friend WithEvents lYver As Label
    Friend WithEvents Separacion1 As Panel
    Friend WithEvents Separacion2 As Panel
    Friend WithEvents Image As PictureBox
    Friend WithEvents CBVerCuadricula As Button
    Friend WithEvents CBVerBloqueosT As Button
    Friend WithEvents CBVerBloqueosA As Button
    Friend WithEvents CBVerTriggers As Button
    Friend WithEvents CBVerTraslados As Button
    Friend WithEvents MainViewShp As Panel
    Friend WithEvents COPIAR_GRH0 As Label
    Friend WithEvents COPIAR_GRH1 As Label
    Friend WithEvents COPIAR_GRH2 As Label
    Friend WithEvents COPIAR_GRH3 As Label
    Friend WithEvents MemoriaAuxiliar As Label
    Friend WithEvents functionBarPanel As Panel
    Friend WithEvents surfacePanelButton As LegacyPanelButton
    Friend WithEvents transfersPanelButton As LegacyPanelButton
    Friend WithEvents blockedPanelButton As LegacyPanelButton
    Friend WithEvents npcsPanelButton As LegacyPanelButton
    Friend WithEvents hostileNpcsPanelButton As LegacyPanelButton
    Friend WithEvents objectsPanelButton As LegacyPanelButton
    Friend WithEvents triggersPanelButton As LegacyPanelButton
    Friend WithEvents panelStateRow As FlowLayoutPanel
    Friend WithEvents MapPest1 As Label
    Friend WithEvents MapPest2 As Label
    Friend WithEvents MapPest3 As Label
    Friend WithEvents MapPest4 As Label
    Friend WithEvents MapPest5 As Label
    Friend WithEvents MapPest6 As Label
    Friend WithEvents MapPest7 As Label
    Friend WithEvents MapPest8 As Label
    Friend WithEvents MapPest9 As Label
    Friend WithEvents MapPest10 As Label
    Friend WithEvents MapPest11 As Label
    Friend WithEvents MapPest12 As Label
    Friend WithEvents MapPest13 As Label
    Friend WithEvents statusStrip1 As StatusStrip
    Friend WithEvents statusLabel As ToolStripStatusLabel
    Friend WithEvents openMapDialog As OpenFileDialog
    Friend WithEvents saveMapDialog As SaveFileDialog
    Friend WithEvents tileCompositionPanel As Panel
    Friend WithEvents StatTxt As TextBox
    Friend WithEvents PreviewGrh As PictureBox
    Friend WithEvents Picture2 As PictureBox
    Friend WithEvents Picture5 As PictureBox
    Friend WithEvents Picture6 As PictureBox
    Friend WithEvents Picture7 As PictureBox
    Friend WithEvents Picture8 As PictureBox
    Friend WithEvents Picture9 As PictureBox
    Friend WithEvents Picture11 As PictureBox
    Friend WithEvents tilePositionGroup As GroupBox
    Friend WithEvents tileLayersGroup As GroupBox
    Friend WithEvents tileTransferGroup As GroupBox
    Friend WithEvents tileNpcGroup As GroupBox
    Friend WithEvents tileObjectGroup As GroupBox
    Friend WithEvents autoSaveTimer As Timer
    Friend WithEvents LabelPosX As Label
    Friend WithEvents LabelPosY As Label
    Friend WithEvents LabelBloqueado As Label
    Friend WithEvents LabelCapa1 As Label
    Friend WithEvents LabelCapa2 As Label
    Friend WithEvents LabelCapa3 As Label
    Friend WithEvents LabelCapa4 As Label
    Friend WithEvents LabelTrasMapa As Label
    Friend WithEvents LabelTrasX As Label
    Friend WithEvents LabelTrasY As Label
    Friend WithEvents LabelTipoNPC As Label
    Friend WithEvents LabelNPCIndex As Label
    Friend WithEvents LabelNPCName As Label
    Friend WithEvents LabelOBJIndex As Label
    Friend WithEvents LabelOBJName As Label
    Friend WithEvents LabelOBJCantidad As Label

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
        menuStrip1 = New MenuStrip()
        fileMenuItem = New ToolStripMenuItem()
        newMapMenuItem = New ToolStripMenuItem()
        openMapMenuItem = New ToolStripMenuItem()
        reopenMapMenuItem = New ToolStripMenuItem()
        newFormatMenuItem = New ToolStripMenuItem()
        saveMapMenuItem = New ToolStripMenuItem()
        saveMapAsMenuItem = New ToolStripMenuItem()
        saveBmpMenuItem = New ToolStripMenuItem()
        saveJpgMenuItem = New ToolStripMenuItem()
        exitMenuItem = New ToolStripMenuItem()
        editMenuItem = New ToolStripMenuItem()
        undoMenuItem = New ToolStripMenuItem()
        useUndoMenuItem = New ToolStripMenuItem()
        mapInfoMenuItem = New ToolStripMenuItem()
        insertMenuItem = New ToolStripMenuItem()
        insertAdjacentMenuItem = New ToolStripMenuItem()
        insertRandomMenuItem = New ToolStripMenuItem()
        insertBordersMenuItem = New ToolStripMenuItem()
        insertAllMenuItem = New ToolStripMenuItem()
        insertBlockBordersMenuItem = New ToolStripMenuItem()
        insertBlockMapMenuItem = New ToolStripMenuItem()
        removeMenuItem = New ToolStripMenuItem()
        removeTransfersMenuItem = New ToolStripMenuItem()
        unblockAllMenuItem = New ToolStripMenuItem()
        removeNpcsMenuItem = New ToolStripMenuItem()
        removeHostileMenuItem = New ToolStripMenuItem()
        removeObjectsMenuItem = New ToolStripMenuItem()
        removeTriggersMenuItem = New ToolStripMenuItem()
        removeSurfaceBordersMenuItem = New ToolStripMenuItem()
        removeSelectedLayerMenuItem = New ToolStripMenuItem()
        removeAllMenuItem = New ToolStripMenuItem()
        functionsMenuItem = New ToolStripMenuItem()
        removeFunctionsMenuItem = New ToolStripMenuItem()
        autoRemoveFunctionsMenuItem = New ToolStripMenuItem()
        surfaceConfigMenuItem = New ToolStripMenuItem()
        autoCompleteMenuItem = New ToolStripMenuItem()
        autoCaptureSurfaceMenuItem = New ToolStripMenuItem()
        autoCaptureTransfersMenuItem = New ToolStripMenuItem()
        autoSaveMapsMenuItem = New ToolStripMenuItem()
        viewMenuItem = New ToolStripMenuItem()
        viewLayersMenuItem = New ToolStripMenuItem()
        layer1MenuItem = New ToolStripMenuItem()
        layer2MenuItem = New ToolStripMenuItem()
        layer3MenuItem = New ToolStripMenuItem()
        layer4MenuItem = New ToolStripMenuItem()
        transladosMenuItem = New ToolStripMenuItem()
        blockedMenuItem = New ToolStripMenuItem()
        npcsMenuItem = New ToolStripMenuItem()
        objectsMenuItem = New ToolStripMenuItem()
        triggersMenuItem = New ToolStripMenuItem()
        gridMenuItem = New ToolStripMenuItem()
        automaticViewMenuItem = New ToolStripMenuItem()
        panelsMenuItem = New ToolStripMenuItem()
        panelSurfaceMenuItem = New ToolStripMenuItem()
        panelTransfersMenuItem = New ToolStripMenuItem()
        panelBlockMenuItem = New ToolStripMenuItem()
        panelNpcsMenuItem = New ToolStripMenuItem()
        panelHostileMenuItem = New ToolStripMenuItem()
        panelObjectsMenuItem = New ToolStripMenuItem()
        panelTriggersMenuItem = New ToolStripMenuItem()
        optionsMenuItem = New ToolStripMenuItem()
        reportsMenuItem = New ToolStripMenuItem()
        updateIndicesMenuItem = New ToolStripMenuItem()
        walkModeMenuItem = New ToolStripMenuItem()
        grhBmpMenuItem = New ToolStripMenuItem()
        optimizeMenuItem = New ToolStripMenuItem()
        saveConfigMenuItem = New ToolStripMenuItem()
        helpMenuItem = New ToolStripMenuItem()
        screenMenuItem = New ToolStripMenuItem()
        horizontalMenuItem = New ToolStripMenuItem()
        verticalMenuItem = New ToolStripMenuItem()
        toggleBlockedMenuItem = New ToolStripMenuItem()
        setLayer1MenuItem = New ToolStripMenuItem()
        setLayer2MenuItem = New ToolStripMenuItem()
        setLayer3MenuItem = New ToolStripMenuItem()
        setLayer4MenuItem = New ToolStripMenuItem()
        clearLayer1MenuItem = New ToolStripMenuItem()
        clearLayer2MenuItem = New ToolStripMenuItem()
        clearLayer3MenuItem = New ToolStripMenuItem()
        clearLayer4MenuItem = New ToolStripMenuItem()
        blockAllMenuItem = New ToolStripMenuItem()
        fillLayer1MenuItem = New ToolStripMenuItem()
        fillLayer2MenuItem = New ToolStripMenuItem()
        fillLayer3MenuItem = New ToolStripMenuItem()
        fillLayer4MenuItem = New ToolStripMenuItem()
        paintSelectedMenuItem = New ToolStripMenuItem()
        brushMenuItem = New ToolStripMenuItem()
        mapScrollPanel = New Panel()
        mapSurface = New MapSurfaceControl()
        MainViewShp = New Panel()
        rightContentPanel = New Panel()
        panelStateRow = New FlowLayoutPanel()
        MapPest1 = New Label()
        MapPest2 = New Label()
        MapPest3 = New Label()
        MapPest4 = New Label()
        MapPest5 = New Label()
        MapPest6 = New Label()
        MapPest7 = New Label()
        MapPest8 = New Label()
        MapPest9 = New Label()
        MapPest10 = New Label()
        MapPest11 = New Label()
        MapPest12 = New Label()
        MapPest13 = New Label()
        functionBarPanel = New Panel()
        surfacePanelButton = New LegacyPanelButton()
        transfersPanelButton = New LegacyPanelButton()
        blockedPanelButton = New LegacyPanelButton()
        npcsPanelButton = New LegacyPanelButton()
        hostileNpcsPanelButton = New LegacyPanelButton()
        objectsPanelButton = New LegacyPanelButton()
        triggersPanelButton = New LegacyPanelButton()
        tileCompositionPanel = New Panel()
        tileObjectGroup = New GroupBox()
        LabelOBJCantidad = New Label()
        LabelOBJName = New Label()
        LabelOBJIndex = New Label()
        tileNpcGroup = New GroupBox()
        LabelNPCName = New Label()
        LabelNPCIndex = New Label()
        LabelTipoNPC = New Label()
        tileTransferGroup = New GroupBox()
        LabelTrasY = New Label()
        LabelTrasX = New Label()
        LabelTrasMapa = New Label()
        tileLayersGroup = New GroupBox()
        LabelCapa4 = New Label()
        LabelCapa3 = New Label()
        LabelCapa2 = New Label()
        LabelCapa1 = New Label()
        tilePositionGroup = New GroupBox()
        LabelBloqueado = New Label()
        LabelPosY = New Label()
        LabelPosX = New Label()
        Separacion1 = New Panel()
        Separacion2 = New Panel()
        Image = New PictureBox()
        CBVerCuadricula = New Button()
        CBVerBloqueosT = New Button()
        CBVerBloqueosA = New Button()
        CBVerTriggers = New Button()
        CBVerTraslados = New Button()
        COPIAR_GRH0 = New Label()
        COPIAR_GRH1 = New Label()
        COPIAR_GRH2 = New Label()
        COPIAR_GRH3 = New Label()
        MemoriaAuxiliar = New Label()
        StatTxt = New TextBox()
        lMapN = New Label()
        PreviewGrh = New PictureBox()
        Picture2 = New PictureBox()
        Picture5 = New PictureBox()
        Picture6 = New PictureBox()
        Picture7 = New PictureBox()
        Picture8 = New PictureBox()
        Picture9 = New PictureBox()
        Picture11 = New PictureBox()
        leftSidebar = New Panel()
        functionContentPanel = New Panel()
        surfaceFunctionPanel = New Panel()
        surfaceListBox = New ListBox()
        grhTextBox = New TextBox()
        grhLabel = New Label()
        layerCombo = New ComboBox()
        layerLabel = New Label()
        surfaceFilterCombo = New ComboBox()
        surfaceFilterLabel = New Label()
        transferFunctionPanel = New Panel()
        removeTransferButton = New Button()
        unionAutoButton = New Button()
        unionManualButton = New Button()
        insertTransferObjectButton = New Button()
        insertTransferButton = New Button()
        transferYTextBox = New TextBox()
        transferXTextBox = New TextBox()
        transferMapTextBox = New TextBox()
        blockingFunctionPanel = New Panel()
        viewBlockingButton = New Button()
        removeBlockingButton = New Button()
        insertBlockingButton = New Button()
        npcFunctionPanel = New Panel()
        npcFunctionAmountLabel = New Label()
        npcFunctionNumberLabel = New Label()
        npcFilterCaptionLabel = New Label()
        npcAmountCombo = New ComboBox()
        npcNumberCombo = New ComboBox()
        npcListBox = New ListBox()
        npcFilterCombo = New ComboBox()
        randomNpcFunctionButton = New Button()
        removeNpcFunctionButton = New Button()
        insertNpcFunctionButton = New Button()
        hostileNpcFunctionPanel = New Panel()
        hostileNpcFunctionAmountLabel = New Label()
        hostileNpcFunctionNumberLabel = New Label()
        hostileNpcFilterLabel = New Label()
        hostileNpcAmountCombo = New ComboBox()
        hostileNpcNumberCombo = New ComboBox()
        randomHostileNpcFunctionButton = New Button()
        removeHostileNpcFunctionButton = New Button()
        insertHostileNpcFunctionButton = New Button()
        hostileNpcListBox = New ListBox()
        hostileNpcFilterCombo = New ComboBox()
        objectFunctionPanel = New Panel()
        objectFunctionAmountLabel = New Label()
        objectFunctionNumberLabel = New Label()
        objectFilterLabel = New Label()
        objectAmountCombo = New ComboBox()
        objectNumberCombo = New ComboBox()
        randomObjectFunctionButton = New Button()
        removeObjectFunctionButton = New Button()
        insertObjectFunctionButton = New Button()
        objectListBox = New ListBox()
        objectFilterCombo = New ComboBox()
        triggerFunctionPanel = New Panel()
        triggerListBox = New ListBox()
        viewTriggerButton = New Button()
        removeTriggerButton = New Button()
        insertTriggerButton = New Button()
        lXhor = New Label()
        lYver = New Label()
        selectSurfaceButton = New Button()
        removeSurfaceAllLayersButton = New Button()
        removeSurfaceLayerButton = New Button()
        clearFunctionsButton = New Button()
        radarPanel = New Panel()
        radarPictureBox = New PictureBox()
        Shape1 = New Panel()
        ApuntadorRadar = New Panel()
        FPS = New Label()
        POSX = New Label()
        mapInfoPanel = New Panel()
        lblMapNombre = New Label()
        lblMapMusica = New Label()
        lblMapVersion = New Label()
        lblMapNivelMin = New Label()
        lblFNombreMapa = New Label()
        lblFMusica = New Label()
        lblFVersion = New Label()
        Label1 = New Label()
        mapInfoButton = New Button()
        pPaneles = New PictureBox()
        statusStrip1 = New StatusStrip()
        statusLabel = New ToolStripStatusLabel()
        openMapDialog = New OpenFileDialog()
        saveMapDialog = New SaveFileDialog()
        menuStrip1.SuspendLayout()
        mapScrollPanel.SuspendLayout()
        rightContentPanel.SuspendLayout()
        panelStateRow.SuspendLayout()
        functionBarPanel.SuspendLayout()
        tileCompositionPanel.SuspendLayout()
        tileObjectGroup.SuspendLayout()
        tileNpcGroup.SuspendLayout()
        tileTransferGroup.SuspendLayout()
        tileLayersGroup.SuspendLayout()
        tilePositionGroup.SuspendLayout()
        CType(Image, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(PreviewGrh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Picture2, System.ComponentModel.ISupportInitialize).BeginInit()
        Picture2.SuspendLayout()
        CType(Picture5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Picture6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Picture7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Picture8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Picture9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Picture11, System.ComponentModel.ISupportInitialize).BeginInit()
        leftSidebar.SuspendLayout()
        functionContentPanel.SuspendLayout()
        surfaceFunctionPanel.SuspendLayout()
        transferFunctionPanel.SuspendLayout()
        blockingFunctionPanel.SuspendLayout()
        npcFunctionPanel.SuspendLayout()
        hostileNpcFunctionPanel.SuspendLayout()
        objectFunctionPanel.SuspendLayout()
        triggerFunctionPanel.SuspendLayout()
        radarPanel.SuspendLayout()
        CType(radarPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        mapInfoPanel.SuspendLayout()
        CType(pPaneles, System.ComponentModel.ISupportInitialize).BeginInit()
        statusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' menuStrip1
        ' 
        menuStrip1.Items.AddRange(New ToolStripItem() {fileMenuItem, editMenuItem, viewMenuItem, panelsMenuItem, optionsMenuItem, helpMenuItem, screenMenuItem})
        menuStrip1.Location = New Point(0, 0)
        menuStrip1.Name = "menuStrip1"
        menuStrip1.Size = New Size(1295, 24)
        menuStrip1.TabIndex = 0
        ' 
        ' fileMenuItem
        ' 
        fileMenuItem.DropDownItems.AddRange(New ToolStripItem() {newMapMenuItem, openMapMenuItem, reopenMapMenuItem, newFormatMenuItem, saveMapMenuItem, saveMapAsMenuItem, saveBmpMenuItem, saveJpgMenuItem, exitMenuItem})
        fileMenuItem.Name = "fileMenuItem"
        fileMenuItem.Size = New Size(60, 20)
        fileMenuItem.Text = "&Archivo"
        ' 
        ' newMapMenuItem
        ' 
        newMapMenuItem.Name = "newMapMenuItem"
        newMapMenuItem.ShortcutKeys = Keys.Control Or Keys.N
        newMapMenuItem.Size = New Size(253, 22)
        newMapMenuItem.Text = "&Nuevo Mapa"
        ' 
        ' openMapMenuItem
        ' 
        openMapMenuItem.Name = "openMapMenuItem"
        openMapMenuItem.ShortcutKeys = Keys.Control Or Keys.A
        openMapMenuItem.Size = New Size(253, 22)
        openMapMenuItem.Text = "&Abrir Mapa"
        ' 
        ' reopenMapMenuItem
        ' 
        reopenMapMenuItem.Name = "reopenMapMenuItem"
        reopenMapMenuItem.Size = New Size(253, 22)
        reopenMapMenuItem.Text = "&Re-Abrir Mapa"
        ' 
        ' newFormatMenuItem
        ' 
        newFormatMenuItem.Name = "newFormatMenuItem"
        newFormatMenuItem.Size = New Size(253, 22)
        newFormatMenuItem.Text = "Utilizar &Nuevo Formato al Guardar"
        ' 
        ' saveMapMenuItem
        ' 
        saveMapMenuItem.Name = "saveMapMenuItem"
        saveMapMenuItem.ShortcutKeys = Keys.Control Or Keys.G
        saveMapMenuItem.Size = New Size(253, 22)
        saveMapMenuItem.Text = "&Guardar Mapa"
        ' 
        ' saveMapAsMenuItem
        ' 
        saveMapAsMenuItem.Name = "saveMapAsMenuItem"
        saveMapAsMenuItem.Size = New Size(253, 22)
        saveMapAsMenuItem.Text = "Guardar Mapa &como..."
        ' 
        ' saveBmpMenuItem
        ' 
        saveBmpMenuItem.Name = "saveBmpMenuItem"
        saveBmpMenuItem.Size = New Size(253, 22)
        saveBmpMenuItem.Text = "Guardar como &BMP"
        ' 
        ' saveJpgMenuItem
        ' 
        saveJpgMenuItem.Name = "saveJpgMenuItem"
        saveJpgMenuItem.Size = New Size(253, 22)
        saveJpgMenuItem.Text = "Guardar como &JPG"
        ' 
        ' exitMenuItem
        ' 
        exitMenuItem.Name = "exitMenuItem"
        exitMenuItem.Size = New Size(253, 22)
        exitMenuItem.Text = "&Salir"
        ' 
        ' editMenuItem
        ' 
        editMenuItem.DropDownItems.AddRange(New ToolStripItem() {undoMenuItem, useUndoMenuItem, mapInfoMenuItem, insertMenuItem, removeMenuItem, functionsMenuItem, surfaceConfigMenuItem, autoCompleteMenuItem, autoCaptureSurfaceMenuItem, autoCaptureTransfersMenuItem, autoSaveMapsMenuItem})
        editMenuItem.Name = "editMenuItem"
        editMenuItem.Size = New Size(58, 20)
        editMenuItem.Text = "&Edición"
        ' 
        ' undoMenuItem
        ' 
        undoMenuItem.Enabled = False
        undoMenuItem.Name = "undoMenuItem"
        undoMenuItem.ShortcutKeys = Keys.Control Or Keys.Z
        undoMenuItem.Size = New Size(312, 22)
        undoMenuItem.Text = "&Deshacer"
        ' 
        ' useUndoMenuItem
        ' 
        useUndoMenuItem.Name = "useUndoMenuItem"
        useUndoMenuItem.Size = New Size(312, 22)
        useUndoMenuItem.Text = "&Utilizar Deshacer"
        ' 
        ' mapInfoMenuItem
        ' 
        mapInfoMenuItem.Name = "mapInfoMenuItem"
        mapInfoMenuItem.Size = New Size(312, 22)
        mapInfoMenuItem.Text = "&Información del Mapa"
        ' 
        ' insertMenuItem
        ' 
        insertMenuItem.DropDownItems.AddRange(New ToolStripItem() {insertAdjacentMenuItem, insertRandomMenuItem, insertBordersMenuItem, insertAllMenuItem, insertBlockBordersMenuItem, insertBlockMapMenuItem})
        insertMenuItem.Name = "insertMenuItem"
        insertMenuItem.Size = New Size(312, 22)
        insertMenuItem.Text = "&Insertar"
        ' 
        ' insertAdjacentMenuItem
        ' 
        insertAdjacentMenuItem.Name = "insertAdjacentMenuItem"
        insertAdjacentMenuItem.Size = New Size(251, 22)
        insertAdjacentMenuItem.Text = "&Translados a Mapas Adyasentes"
        ' 
        ' insertRandomMenuItem
        ' 
        insertRandomMenuItem.Name = "insertRandomMenuItem"
        insertRandomMenuItem.Size = New Size(251, 22)
        insertRandomMenuItem.Text = "Superficie al &Azar"
        ' 
        ' insertBordersMenuItem
        ' 
        insertBordersMenuItem.Name = "insertBordersMenuItem"
        insertBordersMenuItem.Size = New Size(251, 22)
        insertBordersMenuItem.Text = "Superficie en los &Bordes del Mapa"
        ' 
        ' insertAllMenuItem
        ' 
        insertAllMenuItem.Name = "insertAllMenuItem"
        insertAllMenuItem.Size = New Size(251, 22)
        insertAllMenuItem.Text = "Superficie en Todo el Mapa"
        ' 
        ' insertBlockBordersMenuItem
        ' 
        insertBlockBordersMenuItem.Name = "insertBlockBordersMenuItem"
        insertBlockBordersMenuItem.Size = New Size(251, 22)
        insertBlockBordersMenuItem.Text = "Bloqueo en &Bordes del Mapa"
        ' 
        ' insertBlockMapMenuItem
        ' 
        insertBlockMapMenuItem.Name = "insertBlockMapMenuItem"
        insertBlockMapMenuItem.Size = New Size(251, 22)
        insertBlockMapMenuItem.Text = "Bloqueo en &Todo el Mapa"
        ' 
        ' removeMenuItem
        ' 
        removeMenuItem.DropDownItems.AddRange(New ToolStripItem() {removeTransfersMenuItem, unblockAllMenuItem, removeNpcsMenuItem, removeHostileMenuItem, removeObjectsMenuItem, removeTriggersMenuItem, removeSurfaceBordersMenuItem, removeSelectedLayerMenuItem, removeAllMenuItem})
        removeMenuItem.Name = "removeMenuItem"
        removeMenuItem.Size = New Size(312, 22)
        removeMenuItem.Text = "&Quitar"
        ' 
        ' removeTransfersMenuItem
        ' 
        removeTransfersMenuItem.Name = "removeTransfersMenuItem"
        removeTransfersMenuItem.Size = New Size(256, 22)
        removeTransfersMenuItem.Text = "Todos los &Translados"
        ' 
        ' unblockAllMenuItem
        ' 
        unblockAllMenuItem.Name = "unblockAllMenuItem"
        unblockAllMenuItem.Size = New Size(256, 22)
        ' 
        ' removeNpcsMenuItem
        ' 
        removeNpcsMenuItem.Name = "removeNpcsMenuItem"
        removeNpcsMenuItem.Size = New Size(256, 22)
        removeNpcsMenuItem.Text = "Todos los &NPC's"
        ' 
        ' removeHostileMenuItem
        ' 
        removeHostileMenuItem.Name = "removeHostileMenuItem"
        removeHostileMenuItem.Size = New Size(256, 22)
        removeHostileMenuItem.Text = "Todos los NPC's &Hostiles"
        ' 
        ' removeObjectsMenuItem
        ' 
        removeObjectsMenuItem.Name = "removeObjectsMenuItem"
        removeObjectsMenuItem.Size = New Size(256, 22)
        removeObjectsMenuItem.Text = "Todos los &Objetos"
        ' 
        ' removeTriggersMenuItem
        ' 
        removeTriggersMenuItem.Name = "removeTriggersMenuItem"
        removeTriggersMenuItem.Size = New Size(256, 22)
        removeTriggersMenuItem.Text = "Todos los Tri&gger's"
        ' 
        ' removeSurfaceBordersMenuItem
        ' 
        removeSurfaceBordersMenuItem.Name = "removeSurfaceBordersMenuItem"
        removeSurfaceBordersMenuItem.Size = New Size(256, 22)
        removeSurfaceBordersMenuItem.Text = "Superficie de los B&ordes"
        ' 
        ' removeSelectedLayerMenuItem
        ' 
        removeSelectedLayerMenuItem.Name = "removeSelectedLayerMenuItem"
        removeSelectedLayerMenuItem.Size = New Size(256, 22)
        removeSelectedLayerMenuItem.Text = "Superficie de la &Capa Seleccionada"
        ' 
        ' removeAllMenuItem
        ' 
        removeAllMenuItem.Name = "removeAllMenuItem"
        removeAllMenuItem.Size = New Size(256, 22)
        removeAllMenuItem.Text = "TODO"
        ' 
        ' functionsMenuItem
        ' 
        functionsMenuItem.DropDownItems.AddRange(New ToolStripItem() {removeFunctionsMenuItem, autoRemoveFunctionsMenuItem})
        functionsMenuItem.Name = "functionsMenuItem"
        functionsMenuItem.Size = New Size(312, 22)
        functionsMenuItem.Text = "&Funciones"
        ' 
        ' removeFunctionsMenuItem
        ' 
        removeFunctionsMenuItem.Name = "removeFunctionsMenuItem"
        removeFunctionsMenuItem.Size = New Size(195, 22)
        removeFunctionsMenuItem.Text = "&Quitar Funciones"
        ' 
        ' autoRemoveFunctionsMenuItem
        ' 
        autoRemoveFunctionsMenuItem.Name = "autoRemoveFunctionsMenuItem"
        autoRemoveFunctionsMenuItem.Size = New Size(195, 22)
        autoRemoveFunctionsMenuItem.Text = "Auto-&Quitar Funciones"
        ' 
        ' surfaceConfigMenuItem
        ' 
        surfaceConfigMenuItem.Name = "surfaceConfigMenuItem"
        surfaceConfigMenuItem.Size = New Size(312, 22)
        surfaceConfigMenuItem.Text = "Configuración A&vanzada de Superficie"
        ' 
        ' autoCompleteMenuItem
        ' 
        autoCompleteMenuItem.Name = "autoCompleteMenuItem"
        autoCompleteMenuItem.Size = New Size(312, 22)
        autoCompleteMenuItem.Text = "Auto-Completar &Superficies"
        ' 
        ' autoCaptureSurfaceMenuItem
        ' 
        autoCaptureSurfaceMenuItem.Name = "autoCaptureSurfaceMenuItem"
        autoCaptureSurfaceMenuItem.Size = New Size(312, 22)
        autoCaptureSurfaceMenuItem.Text = "Auto-C&apturar información de la Superficie"
        ' 
        ' autoCaptureTransfersMenuItem
        ' 
        autoCaptureTransfersMenuItem.Name = "autoCaptureTransfersMenuItem"
        autoCaptureTransfersMenuItem.Size = New Size(312, 22)
        autoCaptureTransfersMenuItem.Text = "Auto-&Capturar información de los Translados"
        ' 
        ' autoSaveMapsMenuItem
        ' 
        autoSaveMapsMenuItem.Name = "autoSaveMapsMenuItem"
        autoSaveMapsMenuItem.Size = New Size(312, 22)
        autoSaveMapsMenuItem.Text = "Configuración de Auto-&Guardar Mapas"
        ' 
        ' viewMenuItem
        ' 
        viewMenuItem.DropDownItems.AddRange(New ToolStripItem() {viewLayersMenuItem, transladosMenuItem, blockedMenuItem, npcsMenuItem, objectsMenuItem, triggersMenuItem, gridMenuItem, automaticViewMenuItem})
        viewMenuItem.Name = "viewMenuItem"
        viewMenuItem.Size = New Size(35, 20)
        viewMenuItem.Text = "&Ver"
        ' 
        ' viewLayersMenuItem
        ' 
        viewLayersMenuItem.DropDownItems.AddRange(New ToolStripItem() {layer1MenuItem, layer2MenuItem, layer3MenuItem, layer4MenuItem})
        viewLayersMenuItem.Name = "viewLayersMenuItem"
        viewLayersMenuItem.Size = New Size(213, 22)
        viewLayersMenuItem.Text = "...&Capas"
        ' 
        ' layer1MenuItem
        ' 
        layer1MenuItem.Name = "layer1MenuItem"
        layer1MenuItem.Size = New Size(181, 22)
        layer1MenuItem.Text = "Capa &1 (Piso)"
        ' 
        ' layer2MenuItem
        ' 
        layer2MenuItem.Name = "layer2MenuItem"
        layer2MenuItem.Size = New Size(181, 22)
        layer2MenuItem.Text = "Capa &2 (costas, etc)"
        ' 
        ' layer3MenuItem
        ' 
        layer3MenuItem.Name = "layer3MenuItem"
        layer3MenuItem.Size = New Size(181, 22)
        layer3MenuItem.Text = "Capa &3 (arboles, etc)"
        ' 
        ' layer4MenuItem
        ' 
        layer4MenuItem.Name = "layer4MenuItem"
        layer4MenuItem.Size = New Size(181, 22)
        layer4MenuItem.Text = "Capa &4 (techos, etc)"
        ' 
        ' transladosMenuItem
        ' 
        transladosMenuItem.Name = "transladosMenuItem"
        transladosMenuItem.Size = New Size(213, 22)
        transladosMenuItem.Text = "...&Translados"
        ' 
        ' blockedMenuItem
        ' 
        blockedMenuItem.Name = "blockedMenuItem"
        blockedMenuItem.Size = New Size(213, 22)
        blockedMenuItem.Text = "...&Bloqueos"
        ' 
        ' npcsMenuItem
        ' 
        npcsMenuItem.Name = "npcsMenuItem"
        npcsMenuItem.Size = New Size(213, 22)
        npcsMenuItem.Text = "...&NPC's"
        ' 
        ' objectsMenuItem
        ' 
        objectsMenuItem.Name = "objectsMenuItem"
        objectsMenuItem.Size = New Size(213, 22)
        objectsMenuItem.Text = "...&Objetos"
        ' 
        ' triggersMenuItem
        ' 
        triggersMenuItem.Name = "triggersMenuItem"
        triggersMenuItem.Size = New Size(213, 22)
        triggersMenuItem.Text = "...Tri&gger's"
        ' 
        ' gridMenuItem
        ' 
        gridMenuItem.Name = "gridMenuItem"
        gridMenuItem.Size = New Size(213, 22)
        gridMenuItem.Text = "...Gri&lla"
        ' 
        ' automaticViewMenuItem
        ' 
        automaticViewMenuItem.Name = "automaticViewMenuItem"
        automaticViewMenuItem.Size = New Size(213, 22)
        automaticViewMenuItem.Text = "Control &Automaticamente"
        ' 
        ' panelsMenuItem
        ' 
        panelsMenuItem.DropDownItems.AddRange(New ToolStripItem() {panelSurfaceMenuItem, panelTransfersMenuItem, panelBlockMenuItem, panelNpcsMenuItem, panelHostileMenuItem, panelObjectsMenuItem, panelTriggersMenuItem})
        panelsMenuItem.Name = "panelsMenuItem"
        panelsMenuItem.Size = New Size(59, 20)
        panelsMenuItem.Text = "&Paneles"
        ' 
        ' panelSurfaceMenuItem
        ' 
        panelSurfaceMenuItem.Name = "panelSurfaceMenuItem"
        panelSurfaceMenuItem.Size = New Size(151, 22)
        panelSurfaceMenuItem.Text = "&Superficie"
        ' 
        ' panelTransfersMenuItem
        ' 
        panelTransfersMenuItem.Name = "panelTransfersMenuItem"
        panelTransfersMenuItem.Size = New Size(151, 22)
        panelTransfersMenuItem.Text = "&Translados"
        ' 
        ' panelBlockMenuItem
        ' 
        panelBlockMenuItem.Name = "panelBlockMenuItem"
        panelBlockMenuItem.Size = New Size(151, 22)
        panelBlockMenuItem.Text = "&Bloquear"
        ' 
        ' panelNpcsMenuItem
        ' 
        panelNpcsMenuItem.Name = "panelNpcsMenuItem"
        panelNpcsMenuItem.Size = New Size(151, 22)
        panelNpcsMenuItem.Text = "&NPC's"
        ' 
        ' panelHostileMenuItem
        ' 
        panelHostileMenuItem.Name = "panelHostileMenuItem"
        panelHostileMenuItem.Size = New Size(151, 22)
        panelHostileMenuItem.Text = "NPC's &Hostiles"
        ' 
        ' panelObjectsMenuItem
        ' 
        panelObjectsMenuItem.Name = "panelObjectsMenuItem"
        panelObjectsMenuItem.Size = New Size(151, 22)
        panelObjectsMenuItem.Text = "&Objetos"
        ' 
        ' panelTriggersMenuItem
        ' 
        panelTriggersMenuItem.Name = "panelTriggersMenuItem"
        panelTriggersMenuItem.Size = New Size(151, 22)
        panelTriggersMenuItem.Text = "Tri&gger's"
        ' 
        ' optionsMenuItem
        ' 
        optionsMenuItem.DropDownItems.AddRange(New ToolStripItem() {reportsMenuItem, updateIndicesMenuItem, walkModeMenuItem, grhBmpMenuItem, optimizeMenuItem, saveConfigMenuItem})
        optionsMenuItem.Name = "optionsMenuItem"
        optionsMenuItem.Size = New Size(69, 20)
        optionsMenuItem.Text = "&Opciones"
        ' 
        ' reportsMenuItem
        ' 
        reportsMenuItem.Name = "reportsMenuItem"
        reportsMenuItem.Size = New Size(233, 22)
        reportsMenuItem.Text = "&Informes"
        ' 
        ' updateIndicesMenuItem
        ' 
        updateIndicesMenuItem.Name = "updateIndicesMenuItem"
        updateIndicesMenuItem.Size = New Size(233, 22)
        updateIndicesMenuItem.Text = "&Actualizar Indices de..."
        ' 
        ' walkModeMenuItem
        ' 
        walkModeMenuItem.Name = "walkModeMenuItem"
        walkModeMenuItem.Size = New Size(233, 22)
        walkModeMenuItem.Text = "Modalidad &Caminata"
        ' 
        ' grhBmpMenuItem
        ' 
        grhBmpMenuItem.Name = "grhBmpMenuItem"
        grhBmpMenuItem.Size = New Size(233, 22)
        grhBmpMenuItem.Text = "&GRH => BMP"
        ' 
        ' optimizeMenuItem
        ' 
        optimizeMenuItem.Name = "optimizeMenuItem"
        optimizeMenuItem.Size = New Size(233, 22)
        optimizeMenuItem.Text = "Optimi&zar Mapa"
        ' 
        ' saveConfigMenuItem
        ' 
        saveConfigMenuItem.Name = "saveConfigMenuItem"
        saveConfigMenuItem.Size = New Size(233, 22)
        saveConfigMenuItem.Text = "&Guardar Ultima Configuración"
        ' 
        ' helpMenuItem
        ' 
        helpMenuItem.Name = "helpMenuItem"
        helpMenuItem.Size = New Size(53, 20)
        helpMenuItem.Text = "Ay&uda"
        ' 
        ' screenMenuItem
        ' 
        screenMenuItem.DropDownItems.AddRange(New ToolStripItem() {horizontalMenuItem, verticalMenuItem})
        screenMenuItem.Name = "screenMenuItem"
        screenMenuItem.Size = New Size(61, 20)
        screenMenuItem.Text = "&Pantalla"
        ' 
        ' horizontalMenuItem
        ' 
        horizontalMenuItem.Name = "horizontalMenuItem"
        horizontalMenuItem.Size = New Size(175, 22)
        horizontalMenuItem.Text = "&Tamaño Horizontal"
        ' 
        ' verticalMenuItem
        ' 
        verticalMenuItem.Name = "verticalMenuItem"
        verticalMenuItem.Size = New Size(175, 22)
        verticalMenuItem.Text = "&Tamaño Vertical"
        ' 
        ' toggleBlockedMenuItem
        ' 
        toggleBlockedMenuItem.Name = "toggleBlockedMenuItem"
        toggleBlockedMenuItem.Size = New Size(32, 19)
        ' 
        ' setLayer1MenuItem
        ' 
        setLayer1MenuItem.Name = "setLayer1MenuItem"
        setLayer1MenuItem.Size = New Size(32, 19)
        ' 
        ' setLayer2MenuItem
        ' 
        setLayer2MenuItem.Name = "setLayer2MenuItem"
        setLayer2MenuItem.Size = New Size(32, 19)
        ' 
        ' setLayer3MenuItem
        ' 
        setLayer3MenuItem.Name = "setLayer3MenuItem"
        setLayer3MenuItem.Size = New Size(32, 19)
        ' 
        ' setLayer4MenuItem
        ' 
        setLayer4MenuItem.Name = "setLayer4MenuItem"
        setLayer4MenuItem.Size = New Size(32, 19)
        ' 
        ' clearLayer1MenuItem
        ' 
        clearLayer1MenuItem.Name = "clearLayer1MenuItem"
        clearLayer1MenuItem.Size = New Size(32, 19)
        ' 
        ' clearLayer2MenuItem
        ' 
        clearLayer2MenuItem.Name = "clearLayer2MenuItem"
        clearLayer2MenuItem.Size = New Size(32, 19)
        ' 
        ' clearLayer3MenuItem
        ' 
        clearLayer3MenuItem.Name = "clearLayer3MenuItem"
        clearLayer3MenuItem.Size = New Size(32, 19)
        ' 
        ' clearLayer4MenuItem
        ' 
        clearLayer4MenuItem.Name = "clearLayer4MenuItem"
        clearLayer4MenuItem.Size = New Size(32, 19)
        ' 
        ' blockAllMenuItem
        ' 
        blockAllMenuItem.Name = "blockAllMenuItem"
        blockAllMenuItem.Size = New Size(32, 19)
        ' 
        ' fillLayer1MenuItem
        ' 
        fillLayer1MenuItem.Name = "fillLayer1MenuItem"
        fillLayer1MenuItem.Size = New Size(32, 19)
        ' 
        ' fillLayer2MenuItem
        ' 
        fillLayer2MenuItem.Name = "fillLayer2MenuItem"
        fillLayer2MenuItem.Size = New Size(32, 19)
        ' 
        ' fillLayer3MenuItem
        ' 
        fillLayer3MenuItem.Name = "fillLayer3MenuItem"
        fillLayer3MenuItem.Size = New Size(32, 19)
        ' 
        ' fillLayer4MenuItem
        ' 
        fillLayer4MenuItem.Name = "fillLayer4MenuItem"
        fillLayer4MenuItem.Size = New Size(32, 19)
        ' 
        ' paintSelectedMenuItem
        ' 
        paintSelectedMenuItem.Name = "paintSelectedMenuItem"
        paintSelectedMenuItem.Size = New Size(32, 19)
        ' 
        ' brushMenuItem
        ' 
        brushMenuItem.Name = "brushMenuItem"
        brushMenuItem.Size = New Size(32, 19)
        ' 
        ' mapScrollPanel
        ' 
        mapScrollPanel.AutoScroll = True
        mapScrollPanel.BackColor = Color.DimGray
        mapScrollPanel.Controls.Add(mapSurface)
        mapScrollPanel.Controls.Add(MainViewShp)
        mapScrollPanel.Location = New Point(6, 103)
        mapScrollPanel.Name = "mapScrollPanel"
        mapScrollPanel.Padding = New Padding(8)
        mapScrollPanel.Size = New Size(908, 583)
        mapScrollPanel.TabIndex = 1
        ' 
        ' mapSurface
        ' 
        mapSurface.BackColor = Color.White
        mapSurface.BrushEnabled = False
        mapSurface.CellSize = 24
        mapSurface.Document = Nothing
        mapSurface.GraphicsDirectory = ""
        mapSurface.GraphicsIndex = Nothing
        mapSurface.Location = New Point(8, 8)
        mapSurface.Name = "mapSurface"
        mapSurface.ShowBlocked = False
        mapSurface.ShowGrid = False
        mapSurface.ShowLayer1 = True
        mapSurface.ShowLayer2 = True
        mapSurface.ShowLayer3 = True
        mapSurface.ShowLayer4 = True
        mapSurface.ShowNpcs = False
        mapSurface.ShowObjects = False
        mapSurface.ShowTranslados = False
        mapSurface.ShowTriggers = False
        mapSurface.Size = New Size(892, 567)
        mapSurface.TabIndex = 0
        ' 
        ' MainViewShp
        ' 
        MainViewShp.BackColor = Color.Transparent
        MainViewShp.BorderStyle = BorderStyle.FixedSingle
        MainViewShp.Enabled = False
        MainViewShp.Location = New Point(8, 8)
        MainViewShp.Name = "MainViewShp"
        MainViewShp.Size = New Size(892, 567)
        MainViewShp.TabIndex = 1
        MainViewShp.Visible = False
        ' 
        ' rightContentPanel
        ' 
        rightContentPanel.Controls.Add(mapScrollPanel)
        rightContentPanel.Controls.Add(panelStateRow)
        rightContentPanel.Controls.Add(functionBarPanel)
        rightContentPanel.Controls.Add(tileCompositionPanel)
        rightContentPanel.Controls.Add(Separacion1)
        rightContentPanel.Controls.Add(Separacion2)
        rightContentPanel.Controls.Add(Image)
        rightContentPanel.Controls.Add(CBVerCuadricula)
        rightContentPanel.Controls.Add(CBVerBloqueosT)
        rightContentPanel.Controls.Add(CBVerBloqueosA)
        rightContentPanel.Controls.Add(CBVerTriggers)
        rightContentPanel.Controls.Add(CBVerTraslados)
        rightContentPanel.Controls.Add(COPIAR_GRH0)
        rightContentPanel.Controls.Add(COPIAR_GRH1)
        rightContentPanel.Controls.Add(COPIAR_GRH2)
        rightContentPanel.Controls.Add(COPIAR_GRH3)
        rightContentPanel.Controls.Add(MemoriaAuxiliar)
        rightContentPanel.Dock = DockStyle.Fill
        rightContentPanel.Location = New Point(320, 24)
        rightContentPanel.Name = "rightContentPanel"
        rightContentPanel.Size = New Size(975, 789)
        rightContentPanel.TabIndex = 4
        ' 
        ' panelStateRow
        ' 
        panelStateRow.BackColor = Color.FromArgb(CByte(190), CByte(245), CByte(226))
        panelStateRow.Controls.Add(MapPest1)
        panelStateRow.Controls.Add(MapPest2)
        panelStateRow.Controls.Add(MapPest3)
        panelStateRow.Controls.Add(MapPest4)
        panelStateRow.Controls.Add(MapPest5)
        panelStateRow.Controls.Add(MapPest6)
        panelStateRow.Controls.Add(MapPest7)
        panelStateRow.Controls.Add(MapPest8)
        panelStateRow.Controls.Add(MapPest9)
        panelStateRow.Controls.Add(MapPest10)
        panelStateRow.Controls.Add(MapPest11)
        panelStateRow.Controls.Add(MapPest12)
        panelStateRow.Controls.Add(MapPest13)
        panelStateRow.Location = New Point(0, 69)
        panelStateRow.Name = "panelStateRow"
        panelStateRow.Padding = New Padding(16, 0, 0, 0)
        panelStateRow.Size = New Size(726, 16)
        panelStateRow.TabIndex = 2
        panelStateRow.WrapContents = False
        ' 
        ' MapPest1
        ' 
        MapPest1.BackColor = Color.White
        MapPest1.BorderStyle = BorderStyle.FixedSingle
        MapPest1.Location = New Point(17, 0)
        MapPest1.Margin = New Padding(1, 0, 0, 0)
        MapPest1.Name = "MapPest1"
        MapPest1.Size = New Size(50, 17)
        MapPest1.TabIndex = 0
        MapPest1.Text = "Nada"
        MapPest1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MapPest2
        ' 
        MapPest2.BackColor = Color.White
        MapPest2.BorderStyle = BorderStyle.FixedSingle
        MapPest2.Location = New Point(68, 0)
        MapPest2.Margin = New Padding(1, 0, 0, 0)
        MapPest2.Name = "MapPest2"
        MapPest2.Size = New Size(50, 17)
        MapPest2.TabIndex = 1
        MapPest2.Text = "Nada"
        MapPest2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MapPest3
        ' 
        MapPest3.BackColor = Color.White
        MapPest3.BorderStyle = BorderStyle.FixedSingle
        MapPest3.Location = New Point(119, 0)
        MapPest3.Margin = New Padding(1, 0, 0, 0)
        MapPest3.Name = "MapPest3"
        MapPest3.Size = New Size(50, 17)
        MapPest3.TabIndex = 2
        MapPest3.Text = "Nada"
        MapPest3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MapPest4
        ' 
        MapPest4.BackColor = Color.White
        MapPest4.BorderStyle = BorderStyle.FixedSingle
        MapPest4.Location = New Point(170, 0)
        MapPest4.Margin = New Padding(1, 0, 0, 0)
        MapPest4.Name = "MapPest4"
        MapPest4.Size = New Size(50, 17)
        MapPest4.TabIndex = 3
        MapPest4.Text = "Nada"
        MapPest4.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MapPest5
        ' 
        MapPest5.BackColor = Color.White
        MapPest5.BorderStyle = BorderStyle.FixedSingle
        MapPest5.Location = New Point(221, 0)
        MapPest5.Margin = New Padding(1, 0, 0, 0)
        MapPest5.Name = "MapPest5"
        MapPest5.Size = New Size(50, 17)
        MapPest5.TabIndex = 4
        MapPest5.Text = "Nada"
        MapPest5.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MapPest6
        ' 
        MapPest6.BackColor = Color.FromArgb(CByte(128), CByte(255), CByte(128))
        MapPest6.BorderStyle = BorderStyle.FixedSingle
        MapPest6.Location = New Point(272, 0)
        MapPest6.Margin = New Padding(1, 0, 0, 0)
        MapPest6.Name = "MapPest6"
        MapPest6.Size = New Size(50, 17)
        MapPest6.TabIndex = 5
        MapPest6.Text = "Nada"
        MapPest6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MapPest7
        ' 
        MapPest7.BackColor = Color.White
        MapPest7.BorderStyle = BorderStyle.FixedSingle
        MapPest7.Location = New Point(323, 0)
        MapPest7.Margin = New Padding(1, 0, 0, 0)
        MapPest7.Name = "MapPest7"
        MapPest7.Size = New Size(50, 17)
        MapPest7.TabIndex = 6
        MapPest7.Text = "Nada"
        MapPest7.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MapPest8
        ' 
        MapPest8.BackColor = Color.White
        MapPest8.BorderStyle = BorderStyle.FixedSingle
        MapPest8.Location = New Point(374, 0)
        MapPest8.Margin = New Padding(1, 0, 0, 0)
        MapPest8.Name = "MapPest8"
        MapPest8.Size = New Size(50, 17)
        MapPest8.TabIndex = 7
        MapPest8.Text = "Nada"
        MapPest8.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MapPest9
        ' 
        MapPest9.BackColor = Color.White
        MapPest9.BorderStyle = BorderStyle.FixedSingle
        MapPest9.Location = New Point(425, 0)
        MapPest9.Margin = New Padding(1, 0, 0, 0)
        MapPest9.Name = "MapPest9"
        MapPest9.Size = New Size(50, 17)
        MapPest9.TabIndex = 8
        MapPest9.Text = "Nada"
        MapPest9.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MapPest10
        ' 
        MapPest10.BackColor = Color.White
        MapPest10.BorderStyle = BorderStyle.FixedSingle
        MapPest10.Location = New Point(476, 0)
        MapPest10.Margin = New Padding(1, 0, 0, 0)
        MapPest10.Name = "MapPest10"
        MapPest10.Size = New Size(50, 17)
        MapPest10.TabIndex = 9
        MapPest10.Text = "Nada"
        MapPest10.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MapPest11
        ' 
        MapPest11.BackColor = Color.White
        MapPest11.BorderStyle = BorderStyle.FixedSingle
        MapPest11.Location = New Point(527, 0)
        MapPest11.Margin = New Padding(1, 0, 0, 0)
        MapPest11.Name = "MapPest11"
        MapPest11.Size = New Size(50, 17)
        MapPest11.TabIndex = 10
        MapPest11.Text = "Nada"
        MapPest11.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MapPest12
        ' 
        MapPest12.BackColor = Color.White
        MapPest12.BorderStyle = BorderStyle.FixedSingle
        MapPest12.Location = New Point(578, 0)
        MapPest12.Margin = New Padding(1, 0, 0, 0)
        MapPest12.Name = "MapPest12"
        MapPest12.Size = New Size(50, 17)
        MapPest12.TabIndex = 11
        MapPest12.Text = "Nada"
        MapPest12.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MapPest13
        ' 
        MapPest13.BackColor = Color.White
        MapPest13.BorderStyle = BorderStyle.FixedSingle
        MapPest13.Location = New Point(629, 0)
        MapPest13.Margin = New Padding(1, 0, 0, 0)
        MapPest13.Name = "MapPest13"
        MapPest13.Size = New Size(50, 17)
        MapPest13.TabIndex = 12
        MapPest13.Text = "Nada"
        MapPest13.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' functionBarPanel
        ' 
        functionBarPanel.BackColor = Color.FromArgb(CByte(190), CByte(245), CByte(226))
        functionBarPanel.Controls.Add(surfacePanelButton)
        functionBarPanel.Controls.Add(transfersPanelButton)
        functionBarPanel.Controls.Add(blockedPanelButton)
        functionBarPanel.Controls.Add(npcsPanelButton)
        functionBarPanel.Controls.Add(hostileNpcsPanelButton)
        functionBarPanel.Controls.Add(objectsPanelButton)
        functionBarPanel.Controls.Add(triggersPanelButton)
        functionBarPanel.Location = New Point(0, 0)
        functionBarPanel.Name = "functionBarPanel"
        functionBarPanel.Size = New Size(726, 69)
        functionBarPanel.TabIndex = 2
        ' 
        ' surfacePanelButton
        ' 
        surfacePanelButton.BackColor = Color.Transparent
        surfacePanelButton.ButtonImage = Nothing
        surfacePanelButton.Font = New Font("Arial", 8.25F)
        surfacePanelButton.ForeColor = Color.Black
        surfacePanelButton.GradientColor = Color.FromArgb(CByte(190), CByte(245), CByte(226))
        surfacePanelButton.Location = New Point(3, 0)
        surfacePanelButton.Name = "surfacePanelButton"
        surfacePanelButton.Selected = True
        surfacePanelButton.SelectedColor = Color.FromArgb(CByte(130), CByte(220), CByte(185))
        surfacePanelButton.Size = New Size(121, 69)
        surfacePanelButton.TabIndex = 0
        surfacePanelButton.Text = "Superficie" & vbCrLf & "(F5)"
        ' 
        ' transfersPanelButton
        ' 
        transfersPanelButton.BackColor = Color.Transparent
        transfersPanelButton.ButtonImage = Nothing
        transfersPanelButton.Font = New Font("Arial", 8.25F)
        transfersPanelButton.ForeColor = Color.Black
        transfersPanelButton.GradientColor = Color.FromArgb(CByte(190), CByte(245), CByte(226))
        transfersPanelButton.Location = New Point(104, -1)
        transfersPanelButton.Name = "transfersPanelButton"
        transfersPanelButton.SelectedColor = Color.FromArgb(CByte(130), CByte(220), CByte(185))
        transfersPanelButton.Size = New Size(121, 69)
        transfersPanelButton.TabIndex = 1
        transfersPanelButton.Text = "Translados" & vbCrLf & "(F6)"
        ' 
        ' blockedPanelButton
        ' 
        blockedPanelButton.BackColor = Color.Transparent
        blockedPanelButton.ButtonImage = Nothing
        blockedPanelButton.Font = New Font("Arial", 8.25F)
        blockedPanelButton.ForeColor = Color.Black
        blockedPanelButton.GradientColor = Color.FromArgb(CByte(190), CByte(245), CByte(226))
        blockedPanelButton.Location = New Point(196, -1)
        blockedPanelButton.Name = "blockedPanelButton"
        blockedPanelButton.SelectedColor = Color.FromArgb(CByte(130), CByte(220), CByte(185))
        blockedPanelButton.Size = New Size(121, 69)
        blockedPanelButton.TabIndex = 2
        blockedPanelButton.Text = "Bloqueos (F7)"
        ' 
        ' npcsPanelButton
        ' 
        npcsPanelButton.BackColor = Color.Transparent
        npcsPanelButton.ButtonImage = Nothing
        npcsPanelButton.Font = New Font("Arial", 8.25F)
        npcsPanelButton.ForeColor = Color.Black
        npcsPanelButton.GradientColor = Color.FromArgb(CByte(190), CByte(245), CByte(226))
        npcsPanelButton.Location = New Point(297, 0)
        npcsPanelButton.Name = "npcsPanelButton"
        npcsPanelButton.SelectedColor = Color.FromArgb(CByte(130), CByte(220), CByte(185))
        npcsPanelButton.Size = New Size(121, 69)
        npcsPanelButton.TabIndex = 3
        npcsPanelButton.Text = "NPC's (F8)"
        ' 
        ' hostileNpcsPanelButton
        ' 
        hostileNpcsPanelButton.BackColor = Color.Transparent
        hostileNpcsPanelButton.ButtonImage = Nothing
        hostileNpcsPanelButton.Font = New Font("Arial", 8.25F)
        hostileNpcsPanelButton.ForeColor = Color.Black
        hostileNpcsPanelButton.GradientColor = Color.FromArgb(CByte(190), CByte(245), CByte(226))
        hostileNpcsPanelButton.Location = New Point(399, 0)
        hostileNpcsPanelButton.Name = "hostileNpcsPanelButton"
        hostileNpcsPanelButton.SelectedColor = Color.FromArgb(CByte(130), CByte(220), CByte(185))
        hostileNpcsPanelButton.Size = New Size(121, 69)
        hostileNpcsPanelButton.TabIndex = 4
        hostileNpcsPanelButton.Text = "NPC's" & vbCrLf & "Hostiles (F9)"
        ' 
        ' objectsPanelButton
        ' 
        objectsPanelButton.BackColor = Color.Transparent
        objectsPanelButton.ButtonImage = Nothing
        objectsPanelButton.Font = New Font("Arial", 8.25F)
        objectsPanelButton.ForeColor = Color.Black
        objectsPanelButton.GradientColor = Color.FromArgb(CByte(190), CByte(245), CByte(226))
        objectsPanelButton.Location = New Point(500, 0)
        objectsPanelButton.Name = "objectsPanelButton"
        objectsPanelButton.SelectedColor = Color.FromArgb(CByte(130), CByte(220), CByte(185))
        objectsPanelButton.Size = New Size(121, 69)
        objectsPanelButton.TabIndex = 5
        objectsPanelButton.Text = "Objetos (F11)"
        ' 
        ' triggersPanelButton
        ' 
        triggersPanelButton.BackColor = Color.Transparent
        triggersPanelButton.ButtonImage = Nothing
        triggersPanelButton.Font = New Font("Arial", 8.25F)
        triggersPanelButton.ForeColor = Color.Black
        triggersPanelButton.GradientColor = Color.FromArgb(CByte(190), CByte(245), CByte(226))
        triggersPanelButton.Location = New Point(602, 0)
        triggersPanelButton.Name = "triggersPanelButton"
        triggersPanelButton.SelectedColor = Color.FromArgb(CByte(130), CByte(220), CByte(185))
        triggersPanelButton.Size = New Size(121, 69)
        triggersPanelButton.TabIndex = 6
        triggersPanelButton.Text = "Trigger's (F12)"
        ' 
        ' tileCompositionPanel
        ' 
        tileCompositionPanel.BackColor = Color.FromArgb(CByte(225), CByte(225), CByte(225))
        tileCompositionPanel.Controls.Add(tileObjectGroup)
        tileCompositionPanel.Controls.Add(tileNpcGroup)
        tileCompositionPanel.Controls.Add(tileTransferGroup)
        tileCompositionPanel.Controls.Add(tileLayersGroup)
        tileCompositionPanel.Controls.Add(tilePositionGroup)
        tileCompositionPanel.Location = New Point(0, 688)
        tileCompositionPanel.Name = "tileCompositionPanel"
        tileCompositionPanel.Size = New Size(697, 97)
        tileCompositionPanel.TabIndex = 5
        ' 
        ' tileObjectGroup
        ' 
        tileObjectGroup.Controls.Add(LabelOBJCantidad)
        tileObjectGroup.Controls.Add(LabelOBJName)
        tileObjectGroup.Controls.Add(LabelOBJIndex)
        tileObjectGroup.Location = New Point(544, 16)
        tileObjectGroup.Name = "tileObjectGroup"
        tileObjectGroup.Size = New Size(145, 65)
        tileObjectGroup.TabIndex = 16
        tileObjectGroup.TabStop = False
        tileObjectGroup.Text = "Objeto"
        ' 
        ' LabelOBJCantidad
        ' 
        LabelOBJCantidad.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelOBJCantidad.Location = New Point(8, 46)
        LabelOBJCantidad.Name = "LabelOBJCantidad"
        LabelOBJCantidad.Size = New Size(129, 17)
        LabelOBJCantidad.TabIndex = 0
        LabelOBJCantidad.Text = "Cantidad="
        ' 
        ' LabelOBJName
        ' 
        LabelOBJName.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelOBJName.Font = New Font("Microsoft Sans Serif", 6.75F)
        LabelOBJName.Location = New Point(8, 30)
        LabelOBJName.Name = "LabelOBJName"
        LabelOBJName.Size = New Size(129, 17)
        LabelOBJName.TabIndex = 1
        LabelOBJName.Text = "Name="
        ' 
        ' LabelOBJIndex
        ' 
        LabelOBJIndex.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelOBJIndex.Location = New Point(8, 14)
        LabelOBJIndex.Name = "LabelOBJIndex"
        LabelOBJIndex.Size = New Size(129, 17)
        LabelOBJIndex.TabIndex = 2
        LabelOBJIndex.Text = "Index="
        ' 
        ' tileNpcGroup
        ' 
        tileNpcGroup.Controls.Add(LabelNPCName)
        tileNpcGroup.Controls.Add(LabelNPCIndex)
        tileNpcGroup.Controls.Add(LabelTipoNPC)
        tileNpcGroup.Location = New Point(387, 16)
        tileNpcGroup.Name = "tileNpcGroup"
        tileNpcGroup.Size = New Size(161, 65)
        tileNpcGroup.TabIndex = 17
        tileNpcGroup.TabStop = False
        tileNpcGroup.Text = "NPC"
        ' 
        ' LabelNPCName
        ' 
        LabelNPCName.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelNPCName.Font = New Font("Microsoft Sans Serif", 6.75F)
        LabelNPCName.Location = New Point(8, 46)
        LabelNPCName.Name = "LabelNPCName"
        LabelNPCName.Size = New Size(145, 17)
        LabelNPCName.TabIndex = 0
        LabelNPCName.Text = "Name="
        ' 
        ' LabelNPCIndex
        ' 
        LabelNPCIndex.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelNPCIndex.Location = New Point(8, 30)
        LabelNPCIndex.Name = "LabelNPCIndex"
        LabelNPCIndex.Size = New Size(145, 17)
        LabelNPCIndex.TabIndex = 1
        LabelNPCIndex.Text = "Index="
        ' 
        ' LabelTipoNPC
        ' 
        LabelTipoNPC.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelTipoNPC.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Underline)
        LabelTipoNPC.Location = New Point(8, 14)
        LabelTipoNPC.Name = "LabelTipoNPC"
        LabelTipoNPC.Size = New Size(145, 17)
        LabelTipoNPC.TabIndex = 2
        LabelTipoNPC.Text = "Hostil"
        LabelTipoNPC.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' tileTransferGroup
        ' 
        tileTransferGroup.Controls.Add(LabelTrasY)
        tileTransferGroup.Controls.Add(LabelTrasX)
        tileTransferGroup.Controls.Add(LabelTrasMapa)
        tileTransferGroup.Location = New Point(321, 16)
        tileTransferGroup.Name = "tileTransferGroup"
        tileTransferGroup.Size = New Size(73, 65)
        tileTransferGroup.TabIndex = 18
        tileTransferGroup.TabStop = False
        tileTransferGroup.Text = "Traslado"
        ' 
        ' LabelTrasY
        ' 
        LabelTrasY.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelTrasY.Location = New Point(8, 46)
        LabelTrasY.Name = "LabelTrasY"
        LabelTrasY.Size = New Size(57, 17)
        LabelTrasY.TabIndex = 0
        LabelTrasY.Text = "Y="
        ' 
        ' LabelTrasX
        ' 
        LabelTrasX.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelTrasX.Location = New Point(8, 30)
        LabelTrasX.Name = "LabelTrasX"
        LabelTrasX.Size = New Size(57, 17)
        LabelTrasX.TabIndex = 1
        LabelTrasX.Text = "X="
        ' 
        ' LabelTrasMapa
        ' 
        LabelTrasMapa.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelTrasMapa.Location = New Point(8, 14)
        LabelTrasMapa.Name = "LabelTrasMapa"
        LabelTrasMapa.Size = New Size(57, 17)
        LabelTrasMapa.TabIndex = 2
        LabelTrasMapa.Text = "Mapa="
        ' 
        ' tileLayersGroup
        ' 
        tileLayersGroup.Controls.Add(LabelCapa4)
        tileLayersGroup.Controls.Add(LabelCapa3)
        tileLayersGroup.Controls.Add(LabelCapa2)
        tileLayersGroup.Controls.Add(LabelCapa1)
        tileLayersGroup.Location = New Point(77, 16)
        tileLayersGroup.Name = "tileLayersGroup"
        tileLayersGroup.Size = New Size(245, 81)
        tileLayersGroup.TabIndex = 19
        tileLayersGroup.TabStop = False
        tileLayersGroup.Text = "Superficies de capas"
        ' 
        ' LabelCapa4
        ' 
        LabelCapa4.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelCapa4.Font = New Font("Microsoft Sans Serif", 6.75F)
        LabelCapa4.Location = New Point(8, 64)
        LabelCapa4.Name = "LabelCapa4"
        LabelCapa4.Size = New Size(233, 17)
        LabelCapa4.TabIndex = 0
        LabelCapa4.Text = "Capa4="
        ' 
        ' LabelCapa3
        ' 
        LabelCapa3.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelCapa3.Font = New Font("Microsoft Sans Serif", 6.75F)
        LabelCapa3.Location = New Point(8, 48)
        LabelCapa3.Name = "LabelCapa3"
        LabelCapa3.Size = New Size(233, 17)
        LabelCapa3.TabIndex = 1
        LabelCapa3.Text = "Capa3="
        ' 
        ' LabelCapa2
        ' 
        LabelCapa2.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelCapa2.Font = New Font("Microsoft Sans Serif", 6.75F)
        LabelCapa2.Location = New Point(8, 32)
        LabelCapa2.Name = "LabelCapa2"
        LabelCapa2.Size = New Size(233, 17)
        LabelCapa2.TabIndex = 2
        LabelCapa2.Text = "Capa2="
        ' 
        ' LabelCapa1
        ' 
        LabelCapa1.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelCapa1.Font = New Font("Microsoft Sans Serif", 6.75F)
        LabelCapa1.Location = New Point(8, 16)
        LabelCapa1.Name = "LabelCapa1"
        LabelCapa1.Size = New Size(233, 17)
        LabelCapa1.TabIndex = 3
        LabelCapa1.Text = "Capa1="
        ' 
        ' tilePositionGroup
        ' 
        tilePositionGroup.Controls.Add(LabelBloqueado)
        tilePositionGroup.Controls.Add(LabelPosY)
        tilePositionGroup.Controls.Add(LabelPosX)
        tilePositionGroup.Location = New Point(8, 16)
        tilePositionGroup.Name = "tilePositionGroup"
        tilePositionGroup.Size = New Size(73, 81)
        tilePositionGroup.TabIndex = 20
        tilePositionGroup.TabStop = False
        tilePositionGroup.Text = "Tile"
        ' 
        ' LabelBloqueado
        ' 
        LabelBloqueado.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelBloqueado.Location = New Point(8, 48)
        LabelBloqueado.Name = "LabelBloqueado"
        LabelBloqueado.Size = New Size(57, 25)
        LabelBloqueado.TabIndex = 2
        LabelBloqueado.Text = "Bloqueado"
        LabelBloqueado.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabelPosY
        ' 
        LabelPosY.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelPosY.Location = New Point(8, 32)
        LabelPosY.Name = "LabelPosY"
        LabelPosY.Size = New Size(57, 17)
        LabelPosY.TabIndex = 1
        LabelPosY.Text = "Y="
        ' 
        ' LabelPosX
        ' 
        LabelPosX.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        LabelPosX.Location = New Point(8, 16)
        LabelPosX.Name = "LabelPosX"
        LabelPosX.Size = New Size(57, 17)
        LabelPosX.TabIndex = 0
        LabelPosX.Text = "X="
        ' 
        ' Separacion1
        ' 
        Separacion1.BackColor = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        Separacion1.Location = New Point(21, 8)
        Separacion1.Name = "Separacion1"
        Separacion1.Size = New Size(1, 80)
        Separacion1.TabIndex = 8
        ' 
        ' Separacion2
        ' 
        Separacion2.BackColor = Color.FromArgb(CByte(128), CByte(128), CByte(128))
        Separacion2.Location = New Point(22, 8)
        Separacion2.Name = "Separacion2"
        Separacion2.Size = New Size(1, 80)
        Separacion2.TabIndex = 9
        ' 
        ' Image
        ' 
        Image.BackColor = Color.Transparent
            Image.Image = WorldEditorNet.My.Resources.EditorResources.Ojo
        Image.Location = New Point(929, 170)
        Image.Name = "Image"
        Image.Size = New Size(31, 17)
        Image.TabIndex = 10
        Image.TabStop = False
        ' 
        ' CBVerCuadricula
        ' 
        CBVerCuadricula.BackColor = SystemColors.Control
            CBVerCuadricula.Image = WorldEditorNet.My.Resources.EditorResources.Ver_Cuadricula
        CBVerCuadricula.Location = New Point(929, 194)
        CBVerCuadricula.Name = "CBVerCuadricula"
        CBVerCuadricula.Size = New Size(33, 33)
        CBVerCuadricula.TabIndex = 11
        CBVerCuadricula.UseVisualStyleBackColor = True
        ' 
        ' CBVerBloqueosT
        ' 
        CBVerBloqueosT.BackColor = SystemColors.Control
            CBVerBloqueosT.Image = WorldEditorNet.My.Resources.EditorResources.Bloqueos_Rojos
        CBVerBloqueosT.Location = New Point(929, 226)
        CBVerBloqueosT.Name = "CBVerBloqueosT"
        CBVerBloqueosT.Size = New Size(33, 33)
        CBVerBloqueosT.TabIndex = 12
        CBVerBloqueosT.UseVisualStyleBackColor = True
        ' 
        ' CBVerBloqueosA
        ' 
        CBVerBloqueosA.BackColor = SystemColors.Control
            CBVerBloqueosA.Image = WorldEditorNet.My.Resources.EditorResources.Bloqueos_Azules
        CBVerBloqueosA.Location = New Point(929, 258)
        CBVerBloqueosA.Name = "CBVerBloqueosA"
        CBVerBloqueosA.Size = New Size(33, 33)
        CBVerBloqueosA.TabIndex = 13
        CBVerBloqueosA.UseVisualStyleBackColor = True
        ' 
        ' CBVerTriggers
        ' 
        CBVerTriggers.BackColor = SystemColors.Control
            CBVerTriggers.Image = WorldEditorNet.My.Resources.EditorResources.Triggers
        CBVerTriggers.Location = New Point(929, 290)
        CBVerTriggers.Name = "CBVerTriggers"
        CBVerTriggers.Size = New Size(33, 33)
        CBVerTriggers.TabIndex = 14
        CBVerTriggers.UseVisualStyleBackColor = True
        ' 
        ' CBVerTraslados
        ' 
        CBVerTraslados.BackColor = SystemColors.Control
            CBVerTraslados.Image = WorldEditorNet.My.Resources.EditorResources.Traslados
        CBVerTraslados.Location = New Point(929, 322)
        CBVerTraslados.Name = "CBVerTraslados"
        CBVerTraslados.Size = New Size(33, 33)
        CBVerTraslados.TabIndex = 15
        CBVerTraslados.UseVisualStyleBackColor = True
        ' 
        ' COPIAR_GRH0
        ' 
        COPIAR_GRH0.BackColor = Color.White
        COPIAR_GRH0.BorderStyle = BorderStyle.FixedSingle
        COPIAR_GRH0.Location = New Point(787, 2)
        COPIAR_GRH0.Name = "COPIAR_GRH0"
        COPIAR_GRH0.RightToLeft = RightToLeft.Yes
        COPIAR_GRH0.Size = New Size(48, 35)
        COPIAR_GRH0.TabIndex = 130
        COPIAR_GRH0.Text = "CopiarGRH"
        COPIAR_GRH0.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' COPIAR_GRH1
        ' 
        COPIAR_GRH1.BackColor = Color.White
        COPIAR_GRH1.BorderStyle = BorderStyle.FixedSingle
        COPIAR_GRH1.Location = New Point(834, 36)
        COPIAR_GRH1.Name = "COPIAR_GRH1"
        COPIAR_GRH1.Size = New Size(48, 35)
        COPIAR_GRH1.TabIndex = 131
        COPIAR_GRH1.Text = "Copiar" & vbCrLf & "GRH"
        COPIAR_GRH1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' COPIAR_GRH2
        ' 
        COPIAR_GRH2.BackColor = Color.White
        COPIAR_GRH2.BorderStyle = BorderStyle.FixedSingle
        COPIAR_GRH2.Location = New Point(739, 36)
        COPIAR_GRH2.Name = "COPIAR_GRH2"
        COPIAR_GRH2.Size = New Size(48, 35)
        COPIAR_GRH2.TabIndex = 132
        COPIAR_GRH2.Text = "Copiar" & vbCrLf & "GRH"
        COPIAR_GRH2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' COPIAR_GRH3
        ' 
        COPIAR_GRH3.BackColor = Color.White
        COPIAR_GRH3.BorderStyle = BorderStyle.FixedSingle
        COPIAR_GRH3.Location = New Point(787, 70)
        COPIAR_GRH3.Name = "COPIAR_GRH3"
        COPIAR_GRH3.Size = New Size(48, 35)
        COPIAR_GRH3.TabIndex = 133
        COPIAR_GRH3.Text = "CopiarGRH"
        COPIAR_GRH3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' MemoriaAuxiliar
        ' 
        MemoriaAuxiliar.BackColor = Color.White
        MemoriaAuxiliar.BorderStyle = BorderStyle.FixedSingle
        MemoriaAuxiliar.Location = New Point(850, 12)
        MemoriaAuxiliar.Name = "MemoriaAuxiliar"
        MemoriaAuxiliar.Size = New Size(99, 21)
        MemoriaAuxiliar.TabIndex = 129
        MemoriaAuxiliar.Text = "Copiar memoria"
        MemoriaAuxiliar.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' StatTxt
        ' 
        StatTxt.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        StatTxt.BorderStyle = BorderStyle.None
        StatTxt.ForeColor = Color.White
        StatTxt.Location = New Point(16, 418)
        StatTxt.Multiline = True
        StatTxt.Name = "StatTxt"
        StatTxt.ReadOnly = True
        StatTxt.ScrollBars = ScrollBars.Vertical
        StatTxt.Size = New Size(290, 285)
        StatTxt.TabIndex = 8
        StatTxt.Text = "Información del tile seleccionado"
        StatTxt.TextAlign = HorizontalAlignment.Center
        ' 
        ' lMapN
        ' 
        lMapN.AutoSize = True
        lMapN.ForeColor = Color.White
        lMapN.Location = New Point(8, 8)
        lMapN.Name = "lMapN"
        lMapN.Size = New Size(40, 15)
        lMapN.TabIndex = 5
        lMapN.Text = "Mapa:"
        ' 
        ' PreviewGrh
        ' 
        PreviewGrh.BackColor = Color.FromArgb(CByte(0), CByte(64), CByte(0))
        PreviewGrh.Location = New Point(0, 8)
        PreviewGrh.Name = "PreviewGrh"
        PreviewGrh.Size = New Size(291, 252)
        PreviewGrh.SizeMode = PictureBoxSizeMode.Zoom
        PreviewGrh.TabIndex = 1
        PreviewGrh.TabStop = False
        ' 
        ' Picture2
        ' 
        Picture2.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        Picture2.Controls.Add(PreviewGrh)
        Picture2.Location = New Point(4, 416)
        Picture2.Name = "Picture2"
        Picture2.Size = New Size(297, 364)
        Picture2.TabIndex = 0
        Picture2.TabStop = False
        ' 
        ' Picture5
        ' 
        Picture5.Location = New Point(0, 0)
        Picture5.Name = "Picture5"
        Picture5.Size = New Size(100, 50)
        Picture5.TabIndex = 0
        Picture5.TabStop = False
        Picture5.Visible = False
        ' 
        ' Picture6
        ' 
        Picture6.Location = New Point(0, 0)
        Picture6.Name = "Picture6"
        Picture6.Size = New Size(100, 50)
        Picture6.TabIndex = 0
        Picture6.TabStop = False
        Picture6.Visible = False
        ' 
        ' Picture7
        ' 
        Picture7.Location = New Point(0, 0)
        Picture7.Name = "Picture7"
        Picture7.Size = New Size(100, 50)
        Picture7.TabIndex = 0
        Picture7.TabStop = False
        Picture7.Visible = False
        ' 
        ' Picture8
        ' 
        Picture8.Location = New Point(0, 0)
        Picture8.Name = "Picture8"
        Picture8.Size = New Size(100, 50)
        Picture8.TabIndex = 0
        Picture8.TabStop = False
        Picture8.Visible = False
        ' 
        ' Picture9
        ' 
        Picture9.Location = New Point(0, 0)
        Picture9.Name = "Picture9"
        Picture9.Size = New Size(100, 50)
        Picture9.TabIndex = 0
        Picture9.TabStop = False
        Picture9.Visible = False
        ' 
        ' Picture11
        ' 
        Picture11.Location = New Point(0, 0)
        Picture11.Name = "Picture11"
        Picture11.Size = New Size(100, 50)
        Picture11.TabIndex = 0
        Picture11.TabStop = False
        Picture11.Visible = False
        ' 
        ' leftSidebar
        ' 
        leftSidebar.BackColor = Color.FromArgb(CByte(225), CByte(225), CByte(225))
        leftSidebar.Controls.Add(StatTxt)
        leftSidebar.Controls.Add(Picture2)
        leftSidebar.Controls.Add(clearFunctionsButton)
        leftSidebar.Controls.Add(radarPanel)
        leftSidebar.Controls.Add(mapInfoPanel)
        leftSidebar.Controls.Add(pPaneles)
        leftSidebar.Controls.Add(functionContentPanel)
        leftSidebar.Dock = DockStyle.Left
        leftSidebar.Location = New Point(0, 24)
        leftSidebar.Name = "leftSidebar"
        leftSidebar.Padding = New Padding(8)
        leftSidebar.Size = New Size(320, 811)
        leftSidebar.TabIndex = 3
        ' 
        ' functionContentPanel
        ' 
        functionContentPanel.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        functionContentPanel.Controls.Add(surfaceFunctionPanel)
        functionContentPanel.Controls.Add(transferFunctionPanel)
        functionContentPanel.Controls.Add(blockingFunctionPanel)
        functionContentPanel.Controls.Add(npcFunctionPanel)
        functionContentPanel.Controls.Add(hostileNpcFunctionPanel)
        functionContentPanel.Controls.Add(objectFunctionPanel)
        functionContentPanel.Controls.Add(triggerFunctionPanel)
        functionContentPanel.Controls.Add(lXhor)
        functionContentPanel.Controls.Add(lYver)
        functionContentPanel.Controls.Add(selectSurfaceButton)
        functionContentPanel.Controls.Add(removeSurfaceAllLayersButton)
        functionContentPanel.Controls.Add(removeSurfaceLayerButton)
        functionContentPanel.Location = New Point(8, 120)
        functionContentPanel.Name = "functionContentPanel"
        functionContentPanel.Size = New Size(293, 293)
        functionContentPanel.TabIndex = 2
        ' 
        ' surfaceFunctionPanel
        ' 
        surfaceFunctionPanel.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        surfaceFunctionPanel.Controls.Add(surfaceListBox)
        surfaceFunctionPanel.Controls.Add(grhTextBox)
        surfaceFunctionPanel.Controls.Add(grhLabel)
        surfaceFunctionPanel.Controls.Add(layerCombo)
        surfaceFunctionPanel.Controls.Add(layerLabel)
        surfaceFunctionPanel.Controls.Add(surfaceFilterCombo)
        surfaceFunctionPanel.Controls.Add(surfaceFilterLabel)
        surfaceFunctionPanel.Location = New Point(0, 0)
        surfaceFunctionPanel.Name = "surfaceFunctionPanel"
        surfaceFunctionPanel.Padding = New Padding(4)
        surfaceFunctionPanel.Size = New Size(293, 293)
        surfaceFunctionPanel.TabIndex = 1
        ' 
        ' surfaceListBox
        ' 
        surfaceListBox.BackColor = Color.Black
        surfaceListBox.ForeColor = Color.White
        surfaceListBox.ItemHeight = 15
        surfaceListBox.Location = New Point(8, 8)
        surfaceListBox.Name = "surfaceListBox"
        surfaceListBox.Size = New Size(273, 169)
        surfaceListBox.TabIndex = 1
        ' 
        ' grhTextBox
        ' 
        grhTextBox.Location = New Point(192, 208)
        grhTextBox.Name = "grhTextBox"
        grhTextBox.Size = New Size(89, 23)
        grhTextBox.TabIndex = 2
        ' 
        ' grhLabel
        ' 
        grhLabel.AutoSize = True
        grhLabel.ForeColor = Color.White
        grhLabel.Location = New Point(136, 213)
        grhLabel.Name = "grhLabel"
        grhLabel.Size = New Size(34, 15)
        grhLabel.TabIndex = 3
        grhLabel.Text = "GRH:"
        ' 
        ' layerCombo
        ' 
        layerCombo.DropDownStyle = ComboBoxStyle.DropDownList
        layerCombo.Items.AddRange(New Object() {"1", "2", "3", "4"})
        layerCombo.Location = New Point(72, 208)
        layerCombo.Name = "layerCombo"
        layerCombo.Size = New Size(57, 23)
        layerCombo.TabIndex = 4
        ' 
        ' layerLabel
        ' 
        layerLabel.AutoSize = True
        layerLabel.ForeColor = Color.White
        layerLabel.Location = New Point(8, 213)
        layerLabel.Name = "layerLabel"
        layerLabel.Size = New Size(37, 15)
        layerLabel.TabIndex = 5
        layerLabel.Text = "Capa:"
        ' 
        ' surfaceFilterCombo
        ' 
        surfaceFilterCombo.Location = New Point(40, 184)
        surfaceFilterCombo.Name = "surfaceFilterCombo"
        surfaceFilterCombo.Size = New Size(241, 23)
        surfaceFilterCombo.TabIndex = 6
        ' 
        ' surfaceFilterLabel
        ' 
        surfaceFilterLabel.AutoSize = True
        surfaceFilterLabel.ForeColor = Color.White
        surfaceFilterLabel.Location = New Point(8, 188)
        surfaceFilterLabel.Name = "surfaceFilterLabel"
        surfaceFilterLabel.Size = New Size(40, 15)
        surfaceFilterLabel.TabIndex = 7
        surfaceFilterLabel.Text = "Filtrar:"
        ' 
        ' transferFunctionPanel
        ' 
        transferFunctionPanel.BackColor = Color.Black
        transferFunctionPanel.Controls.Add(removeTransferButton)
        transferFunctionPanel.Controls.Add(unionAutoButton)
        transferFunctionPanel.Controls.Add(unionManualButton)
        transferFunctionPanel.Controls.Add(insertTransferObjectButton)
        transferFunctionPanel.Controls.Add(insertTransferButton)
        transferFunctionPanel.Controls.Add(lMapN)
        transferFunctionPanel.Controls.Add(transferYTextBox)
        transferFunctionPanel.Controls.Add(transferXTextBox)
        transferFunctionPanel.Controls.Add(transferMapTextBox)
        transferFunctionPanel.Location = New Point(0, 0)
        transferFunctionPanel.Name = "transferFunctionPanel"
        transferFunctionPanel.Size = New Size(293, 293)
        transferFunctionPanel.TabIndex = 2
        transferFunctionPanel.Visible = False
        ' 
        ' removeTransferButton
        ' 
        removeTransferButton.Location = New Point(16, 200)
        removeTransferButton.Name = "removeTransferButton"
        removeTransferButton.Size = New Size(257, 25)
        removeTransferButton.TabIndex = 0
        removeTransferButton.Text = "Quitar Translados"
        removeTransferButton.UseVisualStyleBackColor = True
        ' 
        ' unionAutoButton
        ' 
        unionAutoButton.Location = New Point(16, 168)
        unionAutoButton.Name = "unionAutoButton"
        unionAutoButton.Size = New Size(257, 25)
        unionAutoButton.TabIndex = 1
        unionAutoButton.Text = "Unión con Mapas Adyacentes (auto)"
        unionAutoButton.UseVisualStyleBackColor = True
        ' 
        ' unionManualButton
        ' 
        unionManualButton.Location = New Point(16, 144)
        unionManualButton.Name = "unionManualButton"
        unionManualButton.Size = New Size(257, 25)
        unionManualButton.TabIndex = 2
        unionManualButton.Text = "Unión con Mapa Adyacente (manual)"
        unionManualButton.UseVisualStyleBackColor = True
        ' 
        ' insertTransferObjectButton
        ' 
        insertTransferObjectButton.Location = New Point(16, 112)
        insertTransferObjectButton.Name = "insertTransferObjectButton"
        insertTransferObjectButton.Size = New Size(257, 25)
        insertTransferObjectButton.TabIndex = 3
        insertTransferObjectButton.Text = "Colocar automáticamente Objeto"
        insertTransferObjectButton.UseVisualStyleBackColor = True
        ' 
        ' insertTransferButton
        ' 
        insertTransferButton.Location = New Point(16, 88)
        insertTransferButton.Name = "insertTransferButton"
        insertTransferButton.Size = New Size(257, 25)
        insertTransferButton.TabIndex = 4
        insertTransferButton.Text = "Insertar Translados"
        insertTransferButton.UseVisualStyleBackColor = True
        ' 
        ' transferYTextBox
        ' 
        transferYTextBox.Location = New Point(80, 64)
        transferYTextBox.Name = "transferYTextBox"
        transferYTextBox.Size = New Size(193, 23)
        transferYTextBox.TabIndex = 6
        transferYTextBox.Text = "1"
        ' 
        ' transferXTextBox
        ' 
        transferXTextBox.Location = New Point(80, 40)
        transferXTextBox.Name = "transferXTextBox"
        transferXTextBox.Size = New Size(193, 23)
        transferXTextBox.TabIndex = 7
        transferXTextBox.Text = "1"
        ' 
        ' transferMapTextBox
        ' 
        transferMapTextBox.Location = New Point(80, 16)
        transferMapTextBox.Name = "transferMapTextBox"
        transferMapTextBox.Size = New Size(193, 23)
        transferMapTextBox.TabIndex = 8
        transferMapTextBox.Text = "1"
        ' 
        ' blockingFunctionPanel
        ' 
        blockingFunctionPanel.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        blockingFunctionPanel.Controls.Add(viewBlockingButton)
        blockingFunctionPanel.Controls.Add(removeBlockingButton)
        blockingFunctionPanel.Controls.Add(insertBlockingButton)
        blockingFunctionPanel.Location = New Point(0, 0)
        blockingFunctionPanel.Name = "blockingFunctionPanel"
        blockingFunctionPanel.Size = New Size(293, 293)
        blockingFunctionPanel.TabIndex = 3
        blockingFunctionPanel.Visible = False
        ' 
        ' viewBlockingButton
        ' 
        viewBlockingButton.Location = New Point(8, 8)
        viewBlockingButton.Name = "viewBlockingButton"
        viewBlockingButton.Size = New Size(273, 33)
        viewBlockingButton.TabIndex = 0
        viewBlockingButton.Text = "Ver Bloqueos"
        viewBlockingButton.UseVisualStyleBackColor = True
        ' 
        ' removeBlockingButton
        ' 
        removeBlockingButton.Location = New Point(8, 104)
        removeBlockingButton.Name = "removeBlockingButton"
        removeBlockingButton.Size = New Size(273, 49)
        removeBlockingButton.TabIndex = 1
        removeBlockingButton.Text = "Quitar Bloqueo"
        removeBlockingButton.UseVisualStyleBackColor = True
        ' 
        ' insertBlockingButton
        ' 
        insertBlockingButton.Location = New Point(8, 48)
        insertBlockingButton.Name = "insertBlockingButton"
        insertBlockingButton.Size = New Size(273, 49)
        insertBlockingButton.TabIndex = 2
        insertBlockingButton.Text = "Insertar Bloqueo"
        insertBlockingButton.UseVisualStyleBackColor = True
        ' 
        ' npcFunctionPanel
        ' 
        npcFunctionPanel.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        npcFunctionPanel.Controls.Add(npcFunctionAmountLabel)
        npcFunctionPanel.Controls.Add(npcFunctionNumberLabel)
        npcFunctionPanel.Controls.Add(npcFilterCaptionLabel)
        npcFunctionPanel.Controls.Add(npcAmountCombo)
        npcFunctionPanel.Controls.Add(npcNumberCombo)
        npcFunctionPanel.Controls.Add(npcListBox)
        npcFunctionPanel.Controls.Add(npcFilterCombo)
        npcFunctionPanel.Controls.Add(randomNpcFunctionButton)
        npcFunctionPanel.Controls.Add(removeNpcFunctionButton)
        npcFunctionPanel.Controls.Add(insertNpcFunctionButton)
        npcFunctionPanel.Location = New Point(0, 0)
        npcFunctionPanel.Name = "npcFunctionPanel"
        npcFunctionPanel.Size = New Size(293, 293)
        npcFunctionPanel.TabIndex = 4
        npcFunctionPanel.Visible = False
        ' 
        ' npcFunctionAmountLabel
        ' 
        npcFunctionAmountLabel.AutoSize = True
        npcFunctionAmountLabel.ForeColor = Color.White
        npcFunctionAmountLabel.Location = New Point(8, 213)
        npcFunctionAmountLabel.Name = "npcFunctionAmountLabel"
        npcFunctionAmountLabel.Size = New Size(58, 15)
        npcFunctionAmountLabel.TabIndex = 0
        npcFunctionAmountLabel.Text = "Cantidad:"
        ' 
        ' npcFunctionNumberLabel
        ' 
        npcFunctionNumberLabel.AutoSize = True
        npcFunctionNumberLabel.ForeColor = Color.White
        npcFunctionNumberLabel.Location = New Point(144, 213)
        npcFunctionNumberLabel.Name = "npcFunctionNumberLabel"
        npcFunctionNumberLabel.Size = New Size(97, 15)
        npcFunctionNumberLabel.TabIndex = 1
        npcFunctionNumberLabel.Text = "Numero de NPC:"
        ' 
        ' npcFilterCaptionLabel
        ' 
        npcFilterCaptionLabel.AutoSize = True
        npcFilterCaptionLabel.ForeColor = Color.White
        npcFilterCaptionLabel.Location = New Point(8, 188)
        npcFilterCaptionLabel.Name = "npcFilterCaptionLabel"
        npcFilterCaptionLabel.Size = New Size(40, 15)
        npcFilterCaptionLabel.TabIndex = 2
        npcFilterCaptionLabel.Text = "Filtrar:"
        ' 
        ' npcAmountCombo
        ' 
        npcAmountCombo.Location = New Point(56, 208)
        npcAmountCombo.Name = "npcAmountCombo"
        npcAmountCombo.Size = New Size(81, 23)
        npcAmountCombo.TabIndex = 3
        ' 
        ' npcNumberCombo
        ' 
        npcNumberCombo.Location = New Point(224, 208)
        npcNumberCombo.Name = "npcNumberCombo"
        npcNumberCombo.Size = New Size(57, 23)
        npcNumberCombo.TabIndex = 4
        ' 
        ' npcListBox
        ' 
        npcListBox.BackColor = Color.Black
        npcListBox.ForeColor = Color.White
        npcListBox.ItemHeight = 15
        npcListBox.Location = New Point(8, 8)
        npcListBox.Name = "npcListBox"
        npcListBox.Size = New Size(273, 169)
        npcListBox.TabIndex = 5
        ' 
        ' npcFilterCombo
        ' 
        npcFilterCombo.Location = New Point(40, 184)
        npcFilterCombo.Name = "npcFilterCombo"
        npcFilterCombo.Size = New Size(241, 23)
        npcFilterCombo.TabIndex = 6
        ' 
        ' randomNpcFunctionButton
        ' 
        randomNpcFunctionButton.Location = New Point(8, 232)
        randomNpcFunctionButton.Name = "randomNpcFunctionButton"
        randomNpcFunctionButton.Size = New Size(145, 25)
        randomNpcFunctionButton.TabIndex = 7
        randomNpcFunctionButton.Text = "Agregar función al azar"
        randomNpcFunctionButton.UseVisualStyleBackColor = True
        ' 
        ' removeNpcFunctionButton
        ' 
        removeNpcFunctionButton.Location = New Point(8, 256)
        removeNpcFunctionButton.Name = "removeNpcFunctionButton"
        removeNpcFunctionButton.Size = New Size(145, 25)
        removeNpcFunctionButton.TabIndex = 8
        removeNpcFunctionButton.Text = "Quitar función"
        removeNpcFunctionButton.UseVisualStyleBackColor = True
        ' 
        ' insertNpcFunctionButton
        ' 
        insertNpcFunctionButton.Location = New Point(160, 232)
        insertNpcFunctionButton.Name = "insertNpcFunctionButton"
        insertNpcFunctionButton.Size = New Size(121, 49)
        insertNpcFunctionButton.TabIndex = 9
        insertNpcFunctionButton.Text = "Insertar función"
        insertNpcFunctionButton.UseVisualStyleBackColor = True
        ' 
        ' hostileNpcFunctionPanel
        ' 
        hostileNpcFunctionPanel.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        hostileNpcFunctionPanel.Controls.Add(hostileNpcFunctionAmountLabel)
        hostileNpcFunctionPanel.Controls.Add(hostileNpcFunctionNumberLabel)
        hostileNpcFunctionPanel.Controls.Add(hostileNpcFilterLabel)
        hostileNpcFunctionPanel.Controls.Add(hostileNpcAmountCombo)
        hostileNpcFunctionPanel.Controls.Add(hostileNpcNumberCombo)
        hostileNpcFunctionPanel.Controls.Add(randomHostileNpcFunctionButton)
        hostileNpcFunctionPanel.Controls.Add(removeHostileNpcFunctionButton)
        hostileNpcFunctionPanel.Controls.Add(insertHostileNpcFunctionButton)
        hostileNpcFunctionPanel.Controls.Add(hostileNpcListBox)
        hostileNpcFunctionPanel.Controls.Add(hostileNpcFilterCombo)
        hostileNpcFunctionPanel.Location = New Point(0, 0)
        hostileNpcFunctionPanel.Name = "hostileNpcFunctionPanel"
        hostileNpcFunctionPanel.Size = New Size(293, 293)
        hostileNpcFunctionPanel.TabIndex = 5
        hostileNpcFunctionPanel.Visible = False
        ' 
        ' hostileNpcFunctionAmountLabel
        ' 
        hostileNpcFunctionAmountLabel.AutoSize = True
        hostileNpcFunctionAmountLabel.ForeColor = Color.White
        hostileNpcFunctionAmountLabel.Location = New Point(8, 213)
        hostileNpcFunctionAmountLabel.Name = "hostileNpcFunctionAmountLabel"
        hostileNpcFunctionAmountLabel.Size = New Size(58, 15)
        hostileNpcFunctionAmountLabel.TabIndex = 0
        hostileNpcFunctionAmountLabel.Text = "Cantidad:"
        ' 
        ' hostileNpcFunctionNumberLabel
        ' 
        hostileNpcFunctionNumberLabel.AutoSize = True
        hostileNpcFunctionNumberLabel.ForeColor = Color.White
        hostileNpcFunctionNumberLabel.Location = New Point(144, 213)
        hostileNpcFunctionNumberLabel.Name = "hostileNpcFunctionNumberLabel"
        hostileNpcFunctionNumberLabel.Size = New Size(97, 15)
        hostileNpcFunctionNumberLabel.TabIndex = 1
        hostileNpcFunctionNumberLabel.Text = "Numero de NPC:"
        ' 
        ' hostileNpcFilterLabel
        ' 
        hostileNpcFilterLabel.AutoSize = True
        hostileNpcFilterLabel.ForeColor = Color.White
        hostileNpcFilterLabel.Location = New Point(8, 188)
        hostileNpcFilterLabel.Name = "hostileNpcFilterLabel"
        hostileNpcFilterLabel.Size = New Size(40, 15)
        hostileNpcFilterLabel.TabIndex = 2
        hostileNpcFilterLabel.Text = "Filtrar:"
        ' 
        ' hostileNpcAmountCombo
        ' 
        hostileNpcAmountCombo.Location = New Point(56, 208)
        hostileNpcAmountCombo.Name = "hostileNpcAmountCombo"
        hostileNpcAmountCombo.Size = New Size(81, 23)
        hostileNpcAmountCombo.TabIndex = 3
        ' 
        ' hostileNpcNumberCombo
        ' 
        hostileNpcNumberCombo.Location = New Point(224, 208)
        hostileNpcNumberCombo.Name = "hostileNpcNumberCombo"
        hostileNpcNumberCombo.Size = New Size(57, 23)
        hostileNpcNumberCombo.TabIndex = 4
        ' 
        ' randomHostileNpcFunctionButton
        ' 
        randomHostileNpcFunctionButton.Location = New Point(8, 232)
        randomHostileNpcFunctionButton.Name = "randomHostileNpcFunctionButton"
        randomHostileNpcFunctionButton.Size = New Size(145, 25)
        randomHostileNpcFunctionButton.TabIndex = 5
        randomHostileNpcFunctionButton.Text = "Agregar función al azar"
        randomHostileNpcFunctionButton.UseVisualStyleBackColor = True
        ' 
        ' removeHostileNpcFunctionButton
        ' 
        removeHostileNpcFunctionButton.Location = New Point(8, 256)
        removeHostileNpcFunctionButton.Name = "removeHostileNpcFunctionButton"
        removeHostileNpcFunctionButton.Size = New Size(145, 25)
        removeHostileNpcFunctionButton.TabIndex = 6
        removeHostileNpcFunctionButton.Text = "Quitar función"
        removeHostileNpcFunctionButton.UseVisualStyleBackColor = True
        ' 
        ' insertHostileNpcFunctionButton
        ' 
        insertHostileNpcFunctionButton.Location = New Point(160, 232)
        insertHostileNpcFunctionButton.Name = "insertHostileNpcFunctionButton"
        insertHostileNpcFunctionButton.Size = New Size(121, 49)
        insertHostileNpcFunctionButton.TabIndex = 7
        insertHostileNpcFunctionButton.Text = "Insertar función"
        insertHostileNpcFunctionButton.UseVisualStyleBackColor = True
        ' 
        ' hostileNpcListBox
        ' 
        hostileNpcListBox.BackColor = Color.Black
        hostileNpcListBox.ForeColor = Color.White
        hostileNpcListBox.ItemHeight = 15
        hostileNpcListBox.Location = New Point(8, 8)
        hostileNpcListBox.Name = "hostileNpcListBox"
        hostileNpcListBox.Size = New Size(273, 169)
        hostileNpcListBox.TabIndex = 8
        ' 
        ' hostileNpcFilterCombo
        ' 
        hostileNpcFilterCombo.Location = New Point(40, 184)
        hostileNpcFilterCombo.Name = "hostileNpcFilterCombo"
        hostileNpcFilterCombo.Size = New Size(241, 23)
        hostileNpcFilterCombo.TabIndex = 9
        ' 
        ' objectFunctionPanel
        ' 
        objectFunctionPanel.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        objectFunctionPanel.Controls.Add(objectFunctionAmountLabel)
        objectFunctionPanel.Controls.Add(objectFunctionNumberLabel)
        objectFunctionPanel.Controls.Add(objectFilterLabel)
        objectFunctionPanel.Controls.Add(objectAmountCombo)
        objectFunctionPanel.Controls.Add(objectNumberCombo)
        objectFunctionPanel.Controls.Add(randomObjectFunctionButton)
        objectFunctionPanel.Controls.Add(removeObjectFunctionButton)
        objectFunctionPanel.Controls.Add(insertObjectFunctionButton)
        objectFunctionPanel.Controls.Add(objectListBox)
        objectFunctionPanel.Controls.Add(objectFilterCombo)
        objectFunctionPanel.Location = New Point(0, 0)
        objectFunctionPanel.Name = "objectFunctionPanel"
        objectFunctionPanel.Size = New Size(293, 293)
        objectFunctionPanel.TabIndex = 6
        objectFunctionPanel.Visible = False
        ' 
        ' objectFunctionAmountLabel
        ' 
        objectFunctionAmountLabel.AutoSize = True
        objectFunctionAmountLabel.ForeColor = Color.White
        objectFunctionAmountLabel.Location = New Point(8, 213)
        objectFunctionAmountLabel.Name = "objectFunctionAmountLabel"
        objectFunctionAmountLabel.Size = New Size(58, 15)
        objectFunctionAmountLabel.TabIndex = 0
        objectFunctionAmountLabel.Text = "Cantidad:"
        ' 
        ' objectFunctionNumberLabel
        ' 
        objectFunctionNumberLabel.AutoSize = True
        objectFunctionNumberLabel.ForeColor = Color.White
        objectFunctionNumberLabel.Location = New Point(144, 213)
        objectFunctionNumberLabel.Name = "objectFunctionNumberLabel"
        objectFunctionNumberLabel.Size = New Size(93, 15)
        objectFunctionNumberLabel.TabIndex = 1
        objectFunctionNumberLabel.Text = "Numero de OBJ:"
        ' 
        ' objectFilterLabel
        ' 
        objectFilterLabel.AutoSize = True
        objectFilterLabel.ForeColor = Color.White
        objectFilterLabel.Location = New Point(8, 188)
        objectFilterLabel.Name = "objectFilterLabel"
        objectFilterLabel.Size = New Size(40, 15)
        objectFilterLabel.TabIndex = 2
        objectFilterLabel.Text = "Filtrar:"
        ' 
        ' objectAmountCombo
        ' 
        objectAmountCombo.Location = New Point(56, 208)
        objectAmountCombo.Name = "objectAmountCombo"
        objectAmountCombo.Size = New Size(81, 23)
        objectAmountCombo.TabIndex = 3
        ' 
        ' objectNumberCombo
        ' 
        objectNumberCombo.Location = New Point(224, 208)
        objectNumberCombo.Name = "objectNumberCombo"
        objectNumberCombo.Size = New Size(57, 23)
        objectNumberCombo.TabIndex = 4
        ' 
        ' randomObjectFunctionButton
        ' 
        randomObjectFunctionButton.Location = New Point(8, 232)
        randomObjectFunctionButton.Name = "randomObjectFunctionButton"
        randomObjectFunctionButton.Size = New Size(145, 25)
        randomObjectFunctionButton.TabIndex = 5
        randomObjectFunctionButton.Text = "Agregar función al azar"
        randomObjectFunctionButton.UseVisualStyleBackColor = True
        ' 
        ' removeObjectFunctionButton
        ' 
        removeObjectFunctionButton.Location = New Point(8, 256)
        removeObjectFunctionButton.Name = "removeObjectFunctionButton"
        removeObjectFunctionButton.Size = New Size(145, 25)
        removeObjectFunctionButton.TabIndex = 6
        removeObjectFunctionButton.Text = "Quitar función"
        removeObjectFunctionButton.UseVisualStyleBackColor = True
        ' 
        ' insertObjectFunctionButton
        ' 
        insertObjectFunctionButton.Location = New Point(160, 232)
        insertObjectFunctionButton.Name = "insertObjectFunctionButton"
        insertObjectFunctionButton.Size = New Size(121, 49)
        insertObjectFunctionButton.TabIndex = 7
        insertObjectFunctionButton.Text = "Insertar función"
        insertObjectFunctionButton.UseVisualStyleBackColor = True
        ' 
        ' objectListBox
        ' 
        objectListBox.BackColor = Color.Black
        objectListBox.ForeColor = Color.White
        objectListBox.ItemHeight = 15
        objectListBox.Location = New Point(8, 8)
        objectListBox.Name = "objectListBox"
        objectListBox.Size = New Size(273, 169)
        objectListBox.TabIndex = 8
        ' 
        ' objectFilterCombo
        ' 
        objectFilterCombo.Location = New Point(40, 184)
        objectFilterCombo.Name = "objectFilterCombo"
        objectFilterCombo.Size = New Size(241, 23)
        objectFilterCombo.TabIndex = 9
        ' 
        ' triggerFunctionPanel
        ' 
        triggerFunctionPanel.BackColor = Color.FromArgb(CByte(64), CByte(64), CByte(64))
        triggerFunctionPanel.Controls.Add(triggerListBox)
        triggerFunctionPanel.Controls.Add(viewTriggerButton)
        triggerFunctionPanel.Controls.Add(removeTriggerButton)
        triggerFunctionPanel.Controls.Add(insertTriggerButton)
        triggerFunctionPanel.Location = New Point(0, 0)
        triggerFunctionPanel.Name = "triggerFunctionPanel"
        triggerFunctionPanel.Size = New Size(293, 293)
        triggerFunctionPanel.TabIndex = 7
        triggerFunctionPanel.Visible = False
        ' 
        ' triggerListBox
        ' 
        triggerListBox.BackColor = Color.Black
        triggerListBox.ForeColor = Color.White
        triggerListBox.ItemHeight = 15
        triggerListBox.Location = New Point(8, 8)
        triggerListBox.Name = "triggerListBox"
        triggerListBox.Size = New Size(273, 169)
        triggerListBox.TabIndex = 0
        ' 
        ' viewTriggerButton
        ' 
        viewTriggerButton.Location = New Point(8, 232)
        viewTriggerButton.Name = "viewTriggerButton"
        viewTriggerButton.Size = New Size(145, 25)
        viewTriggerButton.TabIndex = 1
        viewTriggerButton.Text = "Ver Triggers"
        viewTriggerButton.UseVisualStyleBackColor = True
        ' 
        ' removeTriggerButton
        ' 
        removeTriggerButton.Location = New Point(8, 256)
        removeTriggerButton.Name = "removeTriggerButton"
        removeTriggerButton.Size = New Size(145, 25)
        removeTriggerButton.TabIndex = 2
        removeTriggerButton.Text = "Quitar Trigger"
        removeTriggerButton.UseVisualStyleBackColor = True
        ' 
        ' insertTriggerButton
        ' 
        insertTriggerButton.Location = New Point(160, 232)
        insertTriggerButton.Name = "insertTriggerButton"
        insertTriggerButton.Size = New Size(121, 49)
        insertTriggerButton.TabIndex = 3
        insertTriggerButton.Text = "Insertar Trigger"
        insertTriggerButton.UseVisualStyleBackColor = True
        ' 
        ' lXhor
        ' 
        lXhor.AutoSize = True
        lXhor.BackColor = Color.Transparent
        lXhor.Font = New Font("Arial", 8.25F)
        lXhor.ForeColor = Color.Black
        lXhor.Location = New Point(16, 43)
        lXhor.Name = "lXhor"
        lXhor.Size = New Size(67, 14)
        lXhor.TabIndex = 20
        lXhor.Text = "X horizontal:"
        lXhor.Visible = False
        ' 
        ' lYver
        ' 
        lYver.AutoSize = True
        lYver.BackColor = Color.Transparent
        lYver.Font = New Font("Arial", 8.25F)
        lYver.ForeColor = Color.Black
        lYver.Location = New Point(16, 67)
        lYver.Name = "lYver"
        lYver.Size = New Size(56, 14)
        lYver.TabIndex = 21
        lYver.Text = "Y vertical:"
        lYver.Visible = False
        ' 
        ' selectSurfaceButton
        ' 
        selectSurfaceButton.Location = New Point(160, 232)
        selectSurfaceButton.Name = "selectSurfaceButton"
        selectSurfaceButton.Size = New Size(121, 49)
        selectSurfaceButton.TabIndex = 9
        selectSurfaceButton.Text = "Insertar Superficie"
        selectSurfaceButton.UseVisualStyleBackColor = True
        ' 
        ' removeSurfaceAllLayersButton
        ' 
        removeSurfaceAllLayersButton.Location = New Point(8, 256)
        removeSurfaceAllLayersButton.Name = "removeSurfaceAllLayersButton"
        removeSurfaceAllLayersButton.Size = New Size(145, 25)
        removeSurfaceAllLayersButton.TabIndex = 10
        removeSurfaceAllLayersButton.Text = "Quitar en Capas 2 y 3"
        removeSurfaceAllLayersButton.UseVisualStyleBackColor = True
        ' 
        ' removeSurfaceLayerButton
        ' 
        removeSurfaceLayerButton.Location = New Point(8, 232)
        removeSurfaceLayerButton.Name = "removeSurfaceLayerButton"
        removeSurfaceLayerButton.Size = New Size(145, 25)
        removeSurfaceLayerButton.TabIndex = 11
        removeSurfaceLayerButton.Text = "Quitar en esta Capa"
        removeSurfaceLayerButton.UseVisualStyleBackColor = True
        ' 
        ' clearFunctionsButton
        ' 
        clearFunctionsButton.Location = New Point(120, 94)
        clearFunctionsButton.Name = "clearFunctionsButton"
        clearFunctionsButton.Size = New Size(179, 21)
        clearFunctionsButton.TabIndex = 3
        clearFunctionsButton.Text = "Quitar Funciones (F4)"
        clearFunctionsButton.UseVisualStyleBackColor = True
        ' 
        ' radarPanel
        ' 
        radarPanel.BackColor = Color.FromArgb(CByte(0), CByte(128), CByte(0))
        radarPanel.Controls.Add(radarPictureBox)
        radarPanel.Controls.Add(Shape1)
        radarPanel.Controls.Add(ApuntadorRadar)
        radarPanel.Controls.Add(FPS)
        radarPanel.Controls.Add(POSX)
        radarPanel.Location = New Point(8, 8)
        radarPanel.Name = "radarPanel"
        radarPanel.Padding = New Padding(4)
        radarPanel.Size = New Size(107, 106)
        radarPanel.TabIndex = 1
        ' 
        ' radarPictureBox
        ' 
        radarPictureBox.BackColor = Color.FromArgb(CByte(0), CByte(128), CByte(0))
        radarPictureBox.BorderStyle = BorderStyle.FixedSingle
        radarPictureBox.Dock = DockStyle.Fill
        radarPictureBox.Location = New Point(4, 4)
        radarPictureBox.Name = "radarPictureBox"
        radarPictureBox.Size = New Size(99, 98)
        radarPictureBox.SizeMode = PictureBoxSizeMode.Zoom
        radarPictureBox.TabIndex = 0
        radarPictureBox.TabStop = False
        ' 
        ' Shape1
        ' 
        Shape1.BackColor = Color.Transparent
        Shape1.BorderStyle = BorderStyle.FixedSingle
        Shape1.Location = New Point(8, 8)
        Shape1.Name = "Shape1"
        Shape1.Size = New Size(91, 91)
        Shape1.TabIndex = 5
        ' 
        ' ApuntadorRadar
        ' 
        ApuntadorRadar.BackColor = Color.White
        ApuntadorRadar.BorderStyle = BorderStyle.FixedSingle
        ApuntadorRadar.Location = New Point(40, 40)
        ApuntadorRadar.Name = "ApuntadorRadar"
        ApuntadorRadar.Size = New Size(25, 22)
        ApuntadorRadar.TabIndex = 6
        ' 
        ' FPS
        ' 
        FPS.AutoSize = True
        FPS.BackColor = Color.Transparent
        FPS.Font = New Font("Tahoma", 6.0F, FontStyle.Bold)
        FPS.ForeColor = Color.FromArgb(CByte(192), CByte(255), CByte(255))
        FPS.Location = New Point(71, 96)
        FPS.Name = "FPS"
        FPS.Size = New Size(35, 10)
        FPS.TabIndex = 8
        FPS.Text = "FPS: ??"
        ' 
        ' POSX
        ' 
        POSX.AutoSize = True
        POSX.BackColor = Color.Transparent
        POSX.Font = New Font("Tahoma", 6.0F, FontStyle.Bold)
        POSX.ForeColor = Color.White
        POSX.Location = New Point(8, 96)
        POSX.Name = "POSX"
        POSX.Size = New Size(50, 10)
        POSX.TabIndex = 7
        POSX.Text = "X: ?? - Y:??"
        ' 
        ' mapInfoPanel
        ' 
        mapInfoPanel.BackColor = Color.Black
        mapInfoPanel.Controls.Add(lblMapNombre)
        mapInfoPanel.Controls.Add(lblMapMusica)
        mapInfoPanel.Controls.Add(lblMapVersion)
        mapInfoPanel.Controls.Add(lblMapNivelMin)
        mapInfoPanel.Controls.Add(lblFNombreMapa)
        mapInfoPanel.Controls.Add(lblFMusica)
        mapInfoPanel.Controls.Add(lblFVersion)
        mapInfoPanel.Controls.Add(Label1)
        mapInfoPanel.Controls.Add(mapInfoButton)
        mapInfoPanel.Location = New Point(114, 8)
        mapInfoPanel.Name = "mapInfoPanel"
        mapInfoPanel.Padding = New Padding(6)
        mapInfoPanel.Size = New Size(189, 82)
        mapInfoPanel.TabIndex = 0
        ' 
        ' lblMapNombre
        ' 
        lblMapNombre.AutoSize = True
        lblMapNombre.BackColor = Color.FromArgb(CByte(192), CByte(192), CByte(192))
        lblMapNombre.Font = New Font("Arial", 8.25F)
        lblMapNombre.ForeColor = Color.Maroon
        lblMapNombre.Location = New Point(72, 6)
        lblMapNombre.Name = "lblMapNombre"
        lblMapNombre.Size = New Size(67, 14)
        lblMapNombre.TabIndex = 9
        lblMapNombre.Text = "Nuevo Mapa"
        ' 
        ' lblMapMusica
        ' 
        lblMapMusica.AutoSize = True
        lblMapMusica.BackColor = Color.FromArgb(CByte(192), CByte(192), CByte(192))
        lblMapMusica.Font = New Font("Arial", 8.25F)
        lblMapMusica.ForeColor = Color.Maroon
        lblMapMusica.Location = New Point(72, 23)
        lblMapMusica.Name = "lblMapMusica"
        lblMapMusica.Size = New Size(13, 14)
        lblMapMusica.TabIndex = 10
        lblMapMusica.Text = "0"
        ' 
        ' lblMapVersion
        ' 
        lblMapVersion.AutoSize = True
        lblMapVersion.BackColor = Color.FromArgb(CByte(192), CByte(192), CByte(192))
        lblMapVersion.Font = New Font("Arial", 9.75F)
        lblMapVersion.ForeColor = Color.Maroon
        lblMapVersion.Location = New Point(72, 40)
        lblMapVersion.Name = "lblMapVersion"
        lblMapVersion.Size = New Size(14, 16)
        lblMapVersion.TabIndex = 11
        lblMapVersion.Text = "0"
        ' 
        ' lblMapNivelMin
        ' 
        lblMapNivelMin.AutoSize = True
        lblMapNivelMin.BackColor = Color.FromArgb(CByte(192), CByte(192), CByte(192))
        lblMapNivelMin.Font = New Font("Arial", 9.75F)
        lblMapNivelMin.ForeColor = Color.Maroon
        lblMapNivelMin.Location = New Point(160, 40)
        lblMapNivelMin.Name = "lblMapNivelMin"
        lblMapNivelMin.Size = New Size(14, 16)
        lblMapNivelMin.TabIndex = 12
        lblMapNivelMin.Text = "0"
        ' 
        ' lblFNombreMapa
        ' 
        lblFNombreMapa.BackColor = Color.FromArgb(CByte(192), CByte(192), CByte(192))
        lblFNombreMapa.BorderStyle = BorderStyle.FixedSingle
        lblFNombreMapa.Font = New Font("Arial", 8.25F, FontStyle.Bold)
        lblFNombreMapa.ForeColor = Color.Black
        lblFNombreMapa.Location = New Point(4, 4)
        lblFNombreMapa.Name = "lblFNombreMapa"
        lblFNombreMapa.Size = New Size(181, 17)
        lblFNombreMapa.TabIndex = 5
        lblFNombreMapa.Text = "Nombre:"
        ' 
        ' lblFMusica
        ' 
        lblFMusica.BackColor = Color.FromArgb(CByte(192), CByte(192), CByte(192))
        lblFMusica.BorderStyle = BorderStyle.FixedSingle
        lblFMusica.Font = New Font("Arial", 8.25F, FontStyle.Bold)
        lblFMusica.ForeColor = Color.Black
        lblFMusica.Location = New Point(4, 22)
        lblFMusica.Name = "lblFMusica"
        lblFMusica.Size = New Size(181, 17)
        lblFMusica.TabIndex = 6
        lblFMusica.Text = "Musica:"
        ' 
        ' lblFVersion
        ' 
        lblFVersion.BackColor = Color.FromArgb(CByte(192), CByte(192), CByte(192))
        lblFVersion.BorderStyle = BorderStyle.FixedSingle
        lblFVersion.Font = New Font("Arial", 8.25F, FontStyle.Bold)
        lblFVersion.ForeColor = Color.Black
        lblFVersion.Location = New Point(4, 40)
        lblFVersion.Name = "lblFVersion"
        lblFVersion.Size = New Size(89, 19)
        lblFVersion.TabIndex = 7
        lblFVersion.Text = "Versión:"
        ' 
        ' Label1
        ' 
        Label1.BackColor = Color.FromArgb(CByte(192), CByte(192), CByte(192))
        Label1.BorderStyle = BorderStyle.FixedSingle
        Label1.Font = New Font("Arial", 8.25F, FontStyle.Bold)
        Label1.ForeColor = Color.Black
        Label1.Location = New Point(96, 40)
        Label1.Name = "Label1"
        Label1.Size = New Size(89, 19)
        Label1.TabIndex = 8
        Label1.Text = "Nivel Min:"
        ' 
        ' mapInfoButton
        ' 
        mapInfoButton.BackColor = Color.FromArgb(CByte(255), CByte(192), CByte(128))
        mapInfoButton.Location = New Point(3, 58)
        mapInfoButton.Name = "mapInfoButton"
        mapInfoButton.Size = New Size(183, 21)
        mapInfoButton.TabIndex = 0
        mapInfoButton.Text = "Información del Mapa"
        mapInfoButton.UseVisualStyleBackColor = False
        ' 
        ' pPaneles
        ' 
        pPaneles.BackColor = Color.Black
        pPaneles.Location = New Point(8, 120)
        pPaneles.Name = "pPaneles"
        pPaneles.Size = New Size(293, 293)
        pPaneles.TabIndex = 134
        pPaneles.TabStop = False
        ' 
        ' statusStrip1
        ' 
        statusStrip1.Items.AddRange(New ToolStripItem() {statusLabel})
        statusStrip1.Location = New Point(320, 813)
        statusStrip1.Name = "statusStrip1"
        statusStrip1.Size = New Size(975, 22)
        statusStrip1.TabIndex = 5
        ' 
        ' statusLabel
        ' 
        statusLabel.Name = "statusLabel"
        statusLabel.Size = New Size(32, 17)
        statusLabel.Text = "Listo"
        ' 
        ' openMapDialog
        ' 
        openMapDialog.Filter = "Mapas V1 (*.map)|*.map|Todos los archivos (*.*)|*.*"
        ' 
        ' saveMapDialog
        ' 
        saveMapDialog.DefaultExt = "map"
        saveMapDialog.Filter = "Mapas V1 (*.map)|*.map|Todos los archivos (*.*)|*.*"
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1295, 835)
        Controls.Add(rightContentPanel)
        Controls.Add(statusStrip1)
        Controls.Add(leftSidebar)
        Controls.Add(menuStrip1)
        KeyPreview = True
        MainMenuStrip = menuStrip1
        Name = "MainForm"
        Text = "WorldEditor .NET - V1"
        menuStrip1.ResumeLayout(False)
        menuStrip1.PerformLayout()
        mapScrollPanel.ResumeLayout(False)
        rightContentPanel.ResumeLayout(False)
        panelStateRow.ResumeLayout(False)
        functionBarPanel.ResumeLayout(False)
        tileCompositionPanel.ResumeLayout(False)
        tileObjectGroup.ResumeLayout(False)
        tileNpcGroup.ResumeLayout(False)
        tileTransferGroup.ResumeLayout(False)
        tileLayersGroup.ResumeLayout(False)
        tilePositionGroup.ResumeLayout(False)
        CType(Image, System.ComponentModel.ISupportInitialize).EndInit()
        CType(PreviewGrh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Picture2, System.ComponentModel.ISupportInitialize).EndInit()
        Picture2.ResumeLayout(False)
        CType(Picture5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Picture6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Picture7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Picture8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Picture9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Picture11, System.ComponentModel.ISupportInitialize).EndInit()
        leftSidebar.ResumeLayout(False)
        leftSidebar.PerformLayout()
        functionContentPanel.ResumeLayout(False)
        functionContentPanel.PerformLayout()
        surfaceFunctionPanel.ResumeLayout(False)
        surfaceFunctionPanel.PerformLayout()
        transferFunctionPanel.ResumeLayout(False)
        transferFunctionPanel.PerformLayout()
        blockingFunctionPanel.ResumeLayout(False)
        npcFunctionPanel.ResumeLayout(False)
        npcFunctionPanel.PerformLayout()
        hostileNpcFunctionPanel.ResumeLayout(False)
        hostileNpcFunctionPanel.PerformLayout()
        objectFunctionPanel.ResumeLayout(False)
        objectFunctionPanel.PerformLayout()
        triggerFunctionPanel.ResumeLayout(False)
        radarPanel.ResumeLayout(False)
        radarPanel.PerformLayout()
        CType(radarPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        mapInfoPanel.ResumeLayout(False)
        mapInfoPanel.PerformLayout()
        CType(pPaneles, System.ComponentModel.ISupportInitialize).EndInit()
        statusStrip1.ResumeLayout(False)
        statusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents reopenMapMenuItem As ToolStripMenuItem
    Friend WithEvents newFormatMenuItem As ToolStripMenuItem
    Friend WithEvents saveMapAsMenuItem As ToolStripMenuItem
    Friend WithEvents saveBmpMenuItem As ToolStripMenuItem
    Friend WithEvents saveJpgMenuItem As ToolStripMenuItem
    Friend WithEvents useUndoMenuItem As ToolStripMenuItem
    Friend WithEvents mapInfoMenuItem As ToolStripMenuItem
    Friend WithEvents insertMenuItem As ToolStripMenuItem
    Friend WithEvents insertAdjacentMenuItem As ToolStripMenuItem
    Friend WithEvents insertRandomMenuItem As ToolStripMenuItem
    Friend WithEvents insertBordersMenuItem As ToolStripMenuItem
    Friend WithEvents insertAllMenuItem As ToolStripMenuItem
    Friend WithEvents insertBlockBordersMenuItem As ToolStripMenuItem
    Friend WithEvents insertBlockMapMenuItem As ToolStripMenuItem
    Friend WithEvents removeMenuItem As ToolStripMenuItem
    Friend WithEvents removeTransfersMenuItem As ToolStripMenuItem
    Friend WithEvents removeNpcsMenuItem As ToolStripMenuItem
    Friend WithEvents removeHostileMenuItem As ToolStripMenuItem
    Friend WithEvents removeObjectsMenuItem As ToolStripMenuItem
    Friend WithEvents removeTriggersMenuItem As ToolStripMenuItem
    Friend WithEvents removeSurfaceBordersMenuItem As ToolStripMenuItem
    Friend WithEvents removeSelectedLayerMenuItem As ToolStripMenuItem
    Friend WithEvents removeAllMenuItem As ToolStripMenuItem
    Friend WithEvents functionsMenuItem As ToolStripMenuItem
    Friend WithEvents removeFunctionsMenuItem As ToolStripMenuItem
    Friend WithEvents autoRemoveFunctionsMenuItem As ToolStripMenuItem
    Friend WithEvents autoCompleteMenuItem As ToolStripMenuItem
    Friend WithEvents autoCaptureSurfaceMenuItem As ToolStripMenuItem
    Friend WithEvents autoCaptureTransfersMenuItem As ToolStripMenuItem
    Friend WithEvents autoSaveMapsMenuItem As ToolStripMenuItem
    Friend WithEvents viewLayersMenuItem As ToolStripMenuItem
    Friend WithEvents automaticViewMenuItem As ToolStripMenuItem
    Friend WithEvents panelSurfaceMenuItem As ToolStripMenuItem
    Friend WithEvents panelTransfersMenuItem As ToolStripMenuItem
    Friend WithEvents panelBlockMenuItem As ToolStripMenuItem
    Friend WithEvents panelNpcsMenuItem As ToolStripMenuItem
    Friend WithEvents panelHostileMenuItem As ToolStripMenuItem
    Friend WithEvents panelObjectsMenuItem As ToolStripMenuItem
    Friend WithEvents panelTriggersMenuItem As ToolStripMenuItem
    Friend WithEvents reportsMenuItem As ToolStripMenuItem
    Friend WithEvents updateIndicesMenuItem As ToolStripMenuItem
    Friend WithEvents walkModeMenuItem As ToolStripMenuItem
    Friend WithEvents grhBmpMenuItem As ToolStripMenuItem
    Friend WithEvents optimizeMenuItem As ToolStripMenuItem
    Friend WithEvents horizontalMenuItem As ToolStripMenuItem
    Friend WithEvents verticalMenuItem As ToolStripMenuItem
    Friend WithEvents pPaneles As PictureBox
    End Class
End Namespace
