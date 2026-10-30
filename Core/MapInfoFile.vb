Imports System
Imports System.Globalization
Imports System.IO

Namespace WorldEditorNet.Core
    ''' <summary>
    ''' Utilidades y serialización del archivo textual .dat asociado al mapa.
    ''' El binario .map/.inf se mantiene separado hasta confirmar su layout.
    ''' </summary>
    Public Module MapFileUtilities
        ''' <summary>Equivalente de FileSize: devuelve -1 si no puede abrirse.</summary>
        Public Function FileSize(ByVal fileName As String) As Long
            Try
                Return New FileInfo(fileName).Length
            Catch
                Return -1
            End Try
        End Function

        ''' <summary>
        ''' Comprueba un archivo o un directorio. Es explícito porque .NET no
        ''' tiene el mismo parámetro VbFileAttribute que usaba el original.
        ''' </summary>
        Public Function FileExist(ByVal path As String,
                                  Optional ByVal directory As Boolean = False) As Boolean
            If directory Then Return System.IO.Directory.Exists(path)
            Return System.IO.File.Exists(path)
        End Function
    End Module

    ''' <summary>
    ''' Lectura y escritura de la sección de información de un mapa (.dat).
    ''' </summary>
    Public NotInheritable Class MapInfoFile
        Private mapTitle As String = String.Empty

        ''' <summary>
        ''' Guarda MapInfo con las mismas claves y conversiones del VB6 original.
        ''' </summary>
        Public Sub Save(ByVal fileName As String,
                        ByVal mapInfo As MapInfoRecord,
                        Optional ByVal fallbackMapName As String = "")
            If String.IsNullOrWhiteSpace(mapTitle) Then
                mapTitle = fallbackMapName
            End If

            If String.IsNullOrWhiteSpace(mapTitle) Then
                mapTitle = Path.GetFileNameWithoutExtension(fileName)
            End If

            Dim section As String = mapTitle
            GeneralUtilities.WriteVar(fileName, section, "Name", mapInfo.Name)
            GeneralUtilities.WriteVar(fileName, section, "MusicNum", mapInfo.Music)
            GeneralUtilities.WriteVar(fileName, section, "MinLevel", Val(mapInfo.MinLevel).ToString(CultureInfo.InvariantCulture))
            GeneralUtilities.WriteVar(fileName, section, "MagiaSinefecto", Val(mapInfo.MagiaSinEfecto).ToString(CultureInfo.InvariantCulture))

            'Se conserva la inversión presente en MapInfo_Guardar de VB6.
            GeneralUtilities.WriteVar(fileName, section, "SeCaenItems", If(mapInfo.SeCaenItems, "0", "1"))
            GeneralUtilities.WriteVar(fileName, section, "PuedeMascotas", Val(mapInfo.PuedeMascotas).ToString(CultureInfo.InvariantCulture))
            GeneralUtilities.WriteVar(fileName, section, "Terreno", mapInfo.Terreno)
            GeneralUtilities.WriteVar(fileName, section, "Zona", mapInfo.Zona)
            GeneralUtilities.WriteVar(fileName, section, "Restringir", mapInfo.Restringir)
            GeneralUtilities.WriteVar(fileName, section, "BackUp", Microsoft.VisualBasic.Conversion.Str(CDbl(mapInfo.BackUp)))
            GeneralUtilities.WriteVar(fileName, section, "Pk", If(mapInfo.Pk, "0", "1"))
        End Sub

        ''' <summary>
        ''' Carga la sección cuyo nombre es el nombre del archivo sin extensión.
        ''' </summary>
        Public Function Load(ByVal fileName As String) As MapInfoRecord
            Dim reader As New clsIniReader()
            reader.Initialize(fileName)

            mapTitle = Path.GetFileNameWithoutExtension(fileName).ToUpper(CultureInfo.CurrentCulture)
            Dim result As New MapInfoRecord With {
                .Name = reader.GetValue(mapTitle, "Name"),
                .Music = reader.GetValue(mapTitle, "MusicNum"),
                .MinLevel = ToShort(reader.GetValue(mapTitle, "MinLevel")),
                .MagiaSinEfecto = ToByte(reader.GetValue(mapTitle, "MagiaSinefecto")),
                .PuedeMascotas = ToByte(reader.GetValue(mapTitle, "PuedeMascotas")),
                .Terreno = reader.GetValue(mapTitle, "Terreno"),
                .Zona = reader.GetValue(mapTitle, "Zona"),
                .Restringir = reader.GetValue(mapTitle, "Restringir"),
                .BackUp = ToByte(reader.GetValue(mapTitle, "BACKUP")),
                .Pk = ToShort(reader.GetValue(mapTitle, "Pk")) = 0
            }

            'El VB6 lee SeCaeItems (sin n), aunque al guardar escribe SeCaenItems.
            result.SeCaenItems = ToShort(reader.GetValue(mapTitle, "SeCaeItems")) = 1
            Return result
        End Function

        Private Shared Function ToShort(ByVal value As String) As Short
            Dim parsed As Double = Val(If(value, String.Empty))
            If parsed > Short.MaxValue Then Return Short.MaxValue
            If parsed < Short.MinValue Then Return Short.MinValue
            Return CShort(parsed)
        End Function

        Private Shared Function ToByte(ByVal value As String) As Byte
            Dim parsed As Integer = CInt(Val(If(value, String.Empty)))
            If parsed < Byte.MinValue Then Return Byte.MinValue
            If parsed > Byte.MaxValue Then Return Byte.MaxValue
            Return CByte(parsed)
        End Function
    End Class
End Namespace
