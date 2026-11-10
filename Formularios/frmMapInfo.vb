Imports WorldEditorNet.WorldEditorNet.Core

Imports System.Windows.Forms

Public Class frmMapInfo
    Public Property EditedMapInfo As MapInfoRecord

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal initialInfo As MapInfoRecord)
        Me.New()
        EditedMapInfo = initialInfo
        LoadControlsFromMapInfo()
    End Sub

    Private Sub LoadControlsFromMapInfo()
        txtMapNombre.Text = If(String.IsNullOrWhiteSpace(EditedMapInfo.Name), "Nuevo Mapa", EditedMapInfo.Name)
        txtMapVersion.Text = EditedMapInfo.MapVersion.ToString()
        txtMapMusica.Text = If(String.IsNullOrWhiteSpace(EditedMapInfo.Music), "0", EditedMapInfo.Music)
        txtMapNivelMin.Text = EditedMapInfo.MinLevel.ToString()
        SelectComboValue(txtMapZona, EditedMapInfo.Zona, "CAMPO")
        SelectComboValue(txtMapTerreno, EditedMapInfo.Terreno, "BOSQUE")
        SelectComboValue(txtMapRestringir, EditedMapInfo.Restringir, "No")
        ChkMapSeCaenItems.Checked = EditedMapInfo.SeCaenItems
        chkMapBackup.Checked = EditedMapInfo.BackUp <> 0
        chkMapMagiaSinEfecto.Checked = EditedMapInfo.MagiaSinEfecto <> 0
        ChkMapPuedeMascota.Checked = EditedMapInfo.PuedeMascotas <> 0
        chkMapPK.Checked = EditedMapInfo.Pk
    End Sub

    Private Shared Sub SelectComboValue(ByVal combo As ComboBox, ByVal value As String, ByVal fallback As String)
        Dim selectedValue = If(String.IsNullOrWhiteSpace(value), fallback, value)
        Dim index = combo.FindStringExact(selectedValue)
        If index < 0 Then index = combo.FindStringExact(fallback)
        If index >= 0 Then combo.SelectedIndex = index
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim mapVersion As Short
        Dim minLevel As Short
        If Not Short.TryParse(txtMapVersion.Text, mapVersion) Then
            txtMapVersion.Focus()
            Return
        End If
        If Not Short.TryParse(txtMapNivelMin.Text, minLevel) Then
            txtMapNivelMin.Focus()
            Return
        End If

        Dim info As MapInfoRecord = EditedMapInfo
        info.MapVersion = mapVersion
        info.MinLevel = minLevel
        info.Name = txtMapNombre.Text
        info.Music = txtMapMusica.Text
        info.Zona = If(txtMapZona.SelectedItem, txtMapZona.Text).ToString()
        info.Terreno = If(txtMapTerreno.SelectedItem, txtMapTerreno.Text).ToString()
        info.Restringir = If(txtMapRestringir.SelectedItem, txtMapRestringir.Text).ToString()
        info.SeCaenItems = ChkMapSeCaenItems.Checked
        info.BackUp = If(chkMapBackup.Checked, CByte(1), CByte(0))
        info.MagiaSinEfecto = If(chkMapMagiaSinEfecto.Checked, CByte(1), CByte(0))
        info.PuedeMascotas = If(ChkMapPuedeMascota.Checked, CByte(1), CByte(0))
        info.Pk = chkMapPK.Checked
        info.Changed = 1
        EditedMapInfo = info
        DialogResult = DialogResult.OK
        Hide()
    End Sub

    Private Sub frmMapInfo_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing AndAlso DialogResult <> DialogResult.OK Then
            e.Cancel = True
            Hide()
        End If
    End Sub

    Private Sub cmdMusica_Click(sender As Object, e As EventArgs) Handles cmdMusica.Click
        'VB6 abría frmMusica; ese formulario aún no está migrado.
    End Sub
End Class
