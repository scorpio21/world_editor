<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMapInfo
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New System.Windows.Forms.Label()
        Label6_2 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label7 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        txtMapNombre = New System.Windows.Forms.TextBox()
        txtMapVersion = New System.Windows.Forms.TextBox()
        txtMapMusica = New System.Windows.Forms.TextBox()
        txtMapNivelMin = New System.Windows.Forms.TextBox()
        txtMapZona = New System.Windows.Forms.ComboBox()
        txtMapTerreno = New System.Windows.Forms.ComboBox()
        txtMapRestringir = New System.Windows.Forms.ComboBox()
        ChkMapSeCaenItems = New System.Windows.Forms.CheckBox()
        chkMapMagiaSinEfecto = New System.Windows.Forms.CheckBox()
        chkMapPK = New System.Windows.Forms.CheckBox()
        chkMapBackup = New System.Windows.Forms.CheckBox()
        ChkMapPuedeMascota = New System.Windows.Forms.CheckBox()
        cmdMusica = New System.Windows.Forms.Button()
        Button2 = New System.Windows.Forms.Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.Font = New System.Drawing.Font("Arial", 8.25F)
        Label1.ImageAlign = Drawing.ContentAlignment.MiddleLeft
        Label1.Location = New System.Drawing.Point(8, 8)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(97, 17)
        Label1.TabIndex = 0
        Label1.Text = "Nombre del Mapa:"
        ' 
        ' Label6_2
        ' 
        Label6_2.Font = New System.Drawing.Font("Arial", 8.25F)
        Label6_2.Location = New System.Drawing.Point(8, 32)
        Label6_2.Name = "Label6_2"
        Label6_2.Size = New System.Drawing.Size(97, 17)
        Label6_2.TabIndex = 1
        Label6_2.Text = "Versión del Mapa:"
        ' 
        ' Label2
        ' 
        Label2.Font = New System.Drawing.Font("Arial", 8.25F)
        Label2.Location = New System.Drawing.Point(8, 56)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(97, 17)
        Label2.TabIndex = 2
        Label2.Text = "Musica:"
        ' 
        ' Label3
        ' 
        Label3.Font = New System.Drawing.Font("Arial", 8.25F)
        Label3.Location = New System.Drawing.Point(8, 85)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(97, 17)
        Label3.TabIndex = 3
        Label3.Text = "Zona:"
        ' 
        ' Label4
        ' 
        Label4.Font = New System.Drawing.Font("Arial", 8.25F)
        Label4.Location = New System.Drawing.Point(8, 114)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(97, 17)
        Label4.TabIndex = 4
        Label4.Text = "Terreno:"
        ' 
        ' Label7
        ' 
        Label7.Font = New System.Drawing.Font("Arial", 8.25F)
        Label7.Location = New System.Drawing.Point(8, 143)
        Label7.Name = "Label7"
        Label7.Size = New System.Drawing.Size(97, 17)
        Label7.TabIndex = 5
        Label7.Text = "Nivel Minimo:"
        ' 
        ' Label5
        ' 
        Label5.Font = New System.Drawing.Font("Arial", 8.25F)
        Label5.Location = New System.Drawing.Point(8, 171)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(97, 17)
        Label5.TabIndex = 6
        Label5.Text = "Restringir:"
        ' 
        ' txtMapNombre
        ' 
        txtMapNombre.Location = New System.Drawing.Point(112, 8)
        txtMapNombre.Multiline = True
        txtMapNombre.Name = "txtMapNombre"
        txtMapNombre.Size = New System.Drawing.Size(177, 19)
        txtMapNombre.TabIndex = 7
        txtMapNombre.Text = "Nuevo Mapa"
        ' 
        ' txtMapVersion
        ' 
        txtMapVersion.Location = New System.Drawing.Point(112, 32)
        txtMapVersion.Multiline = True
        txtMapVersion.Name = "txtMapVersion"
        txtMapVersion.Size = New System.Drawing.Size(177, 19)
        txtMapVersion.TabIndex = 8
        txtMapVersion.Text = "0"
        ' 
        ' txtMapMusica
        ' 
        txtMapMusica.Location = New System.Drawing.Point(112, 56)
        txtMapMusica.Multiline = True
        txtMapMusica.Name = "txtMapMusica"
        txtMapMusica.Size = New System.Drawing.Size(121, 19)
        txtMapMusica.TabIndex = 9
        txtMapMusica.Text = "0"
        ' 
        ' txtMapNivelMin
        ' 
        txtMapNivelMin.Location = New System.Drawing.Point(112, 139)
        txtMapNivelMin.Name = "txtMapNivelMin"
        txtMapNivelMin.Size = New System.Drawing.Size(177, 23)
        txtMapNivelMin.TabIndex = 10
        txtMapNivelMin.Text = "0"
        ' 
        ' txtMapZona
        ' 
        txtMapZona.FormattingEnabled = True
        txtMapZona.Items.AddRange(New Object() {"CIUDAD", "CAMPO", "DUNGEON"})
        txtMapZona.Location = New System.Drawing.Point(112, 81)
        txtMapZona.Name = "txtMapZona"
        txtMapZona.Size = New System.Drawing.Size(177, 23)
        txtMapZona.TabIndex = 11
        ' 
        ' txtMapTerreno
        ' 
        txtMapTerreno.FormattingEnabled = True
        txtMapTerreno.Items.AddRange(New Object() {"NIEVE", "DESIERTO", "BOSQUE"})
        txtMapTerreno.Location = New System.Drawing.Point(112, 110)
        txtMapTerreno.Name = "txtMapTerreno"
        txtMapTerreno.Size = New System.Drawing.Size(177, 23)
        txtMapTerreno.TabIndex = 12
        ' 
        ' txtMapRestringir
        ' 
        txtMapRestringir.FormattingEnabled = True
        txtMapRestringir.Items.AddRange(New Object() {"Si", "No"})
        txtMapRestringir.Location = New System.Drawing.Point(112, 167)
        txtMapRestringir.Name = "txtMapRestringir"
        txtMapRestringir.Size = New System.Drawing.Size(177, 23)
        txtMapRestringir.TabIndex = 13
        ' 
        ' ChkMapSeCaenItems
        ' 
        ChkMapSeCaenItems.Location = New System.Drawing.Point(8, 192)
        ChkMapSeCaenItems.Name = "ChkMapSeCaenItems"
        ChkMapSeCaenItems.Size = New System.Drawing.Size(105, 17)
        ChkMapSeCaenItems.TabIndex = 14
        ChkMapSeCaenItems.Text = "Se Caen Items"
        ChkMapSeCaenItems.UseVisualStyleBackColor = True
        ' 
        ' chkMapMagiaSinEfecto
        ' 
        chkMapMagiaSinEfecto.AutoSize = True
        chkMapMagiaSinEfecto.Location = New System.Drawing.Point(8, 215)
        chkMapMagiaSinEfecto.Name = "chkMapMagiaSinEfecto"
        chkMapMagiaSinEfecto.Size = New System.Drawing.Size(114, 19)
        chkMapMagiaSinEfecto.TabIndex = 15
        chkMapMagiaSinEfecto.Text = "Magia Sin Efecto"
        chkMapMagiaSinEfecto.UseVisualStyleBackColor = True
        ' 
        ' chkMapPK
        ' 
        chkMapPK.AutoSize = True
        chkMapPK.Location = New System.Drawing.Point(8, 240)
        chkMapPK.Name = "chkMapPK"
        chkMapPK.Size = New System.Drawing.Size(97, 19)
        chkMapPK.TabIndex = 16
        chkMapPK.Text = "PK (inseguro)"
        chkMapPK.UseVisualStyleBackColor = True
        ' 
        ' chkMapBackup
        ' 
        chkMapBackup.AutoSize = True
        chkMapBackup.Location = New System.Drawing.Point(132, 190)
        chkMapBackup.Name = "chkMapBackup"
        chkMapBackup.Size = New System.Drawing.Size(65, 19)
        chkMapBackup.TabIndex = 17
        chkMapBackup.Text = "Backup"
        chkMapBackup.UseVisualStyleBackColor = True
        ' 
        ' ChkMapPuedeMascota
        ' 
        ChkMapPuedeMascota.AutoSize = True
        ChkMapPuedeMascota.Location = New System.Drawing.Point(132, 215)
        ChkMapPuedeMascota.Name = "ChkMapPuedeMascota"
        ChkMapPuedeMascota.Size = New System.Drawing.Size(112, 19)
        ChkMapPuedeMascota.TabIndex = 18
        ChkMapPuedeMascota.Text = "Puedes Mascota"
        ChkMapPuedeMascota.UseVisualStyleBackColor = True
        ' 
        ' cmdMusica
        ' 
        cmdMusica.Location = New System.Drawing.Point(240, 54)
        cmdMusica.Name = "cmdMusica"
        cmdMusica.Size = New System.Drawing.Size(49, 22)
        cmdMusica.TabIndex = 19
        cmdMusica.Text = "&Más"
        cmdMusica.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New System.Drawing.Point(8, 272)
        Button2.Name = "Button2"
        Button2.Size = New System.Drawing.Size(281, 25)
        Button2.TabIndex = 20
        Button2.Text = "&Cerrar"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' frmMapInfo
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(294, 300)
        Controls.Add(Button2)
        Controls.Add(cmdMusica)
        Controls.Add(ChkMapPuedeMascota)
        Controls.Add(chkMapBackup)
        Controls.Add(chkMapPK)
        Controls.Add(chkMapMagiaSinEfecto)
        Controls.Add(ChkMapSeCaenItems)
        Controls.Add(txtMapRestringir)
        Controls.Add(txtMapTerreno)
        Controls.Add(txtMapZona)
        Controls.Add(txtMapNivelMin)
        Controls.Add(txtMapMusica)
        Controls.Add(txtMapVersion)
        Controls.Add(txtMapNombre)
        Controls.Add(Label5)
        Controls.Add(Label7)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label6_2)
        Controls.Add(Label1)
        Name = "frmMapInfo"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "Información del Mapa"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6_2 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtMapNombre As System.Windows.Forms.TextBox
    Friend WithEvents txtMapVersion As System.Windows.Forms.TextBox
    Friend WithEvents txtMapMusica As System.Windows.Forms.TextBox
    Friend WithEvents txtMapNivelMin As System.Windows.Forms.TextBox
    Friend WithEvents txtMapZona As System.Windows.Forms.ComboBox
    Friend WithEvents txtMapTerreno As System.Windows.Forms.ComboBox
    Friend WithEvents txtMapRestringir As System.Windows.Forms.ComboBox
    Friend WithEvents ChkMapSeCaenItems As System.Windows.Forms.CheckBox
    Friend WithEvents chkMapMagiaSinEfecto As System.Windows.Forms.CheckBox
    Friend WithEvents chkMapPK As System.Windows.Forms.CheckBox
    Friend WithEvents chkMapBackup As System.Windows.Forms.CheckBox
    Friend WithEvents ChkMapPuedeMascota As System.Windows.Forms.CheckBox
    Friend WithEvents cmdMusica As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
