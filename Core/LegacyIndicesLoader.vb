Imports System.IO

Namespace WorldEditorNet.Core
    ''' <summary>
    ''' Carga los índices INI que modIndices.bas cargaba al iniciar el editor.
    ''' </summary>
    Public NotInheritable Class LegacyIndicesLoader
        Private Sub New()
        End Sub

        Public Shared Function LoadSurfaces(ByVal dataDirectory As String) As SupData()
            Dim filePath As String = Path.Combine(dataDirectory, "indices.ini")
            EnsureFileExists(filePath, "indices.ini")

            Dim reader As New clsIniReader()
            reader.Initialize(filePath)
            Dim maxSup As Integer = ReadNonNegativeInt(reader.GetValue("INIT", "Referencias"))
            Dim result(Math.Max(maxSup, 0)) As SupData

            For index As Integer = 0 To maxSup
                result(index).Name = reader.GetValue("REFERENCIA" & index, "Nombre")
                result(index).Grh = ToShort(reader.GetValue("REFERENCIA" & index, "GrhIndice"))
                result(index).Width = ToByte(reader.GetValue("REFERENCIA" & index, "Ancho"))
                result(index).Height = ToByte(reader.GetValue("REFERENCIA" & index, "Alto"))
                result(index).Block = ReadNonNegativeInt(reader.GetValue("REFERENCIA" & index, "Bloquear")) = 1
                result(index).Capa = ToByte(reader.GetValue("REFERENCIA" & index, "Capa"))
            Next

            Return result
        End Function

        Public Shared Function LoadTriggers(ByVal dataDirectory As String) As String()
            Dim filePath As String = Path.Combine(dataDirectory, "Triggers.ini")
            EnsureFileExists(filePath, "Triggers.ini")

            Dim reader As New clsIniReader()
            reader.Initialize(filePath)
            Dim count As Integer = ReadNonNegativeInt(reader.GetValue("INIT", "NumTriggers"))
            Dim result(Math.Max(count - 1, 0)) As String

            For trigger As Integer = 1 To count
                result(trigger - 1) = reader.GetValue("Trig" & trigger, "Name")
            Next

            Return result
        End Function

        ''' <summary>
        ''' Carga DATS\OBJ.dat como CargarIndicesOBJ en modIndices.bas.
        ''' El índice de objeto empieza en 1 en el formato original.
        ''' </summary>
        Public Shared Function LoadObjects(ByVal dataDirectory As String) As ObjData()
            Dim filePath As String = Path.Combine(dataDirectory, "OBJ.dat")
            EnsureFileExists(filePath, "OBJ.dat")

            Dim reader As New clsIniReader()
            reader.Initialize(filePath)
            Dim count As Integer = ReadNonNegativeInt(reader.GetValue("INIT", "NumOBJs"))
            Dim result(Math.Max(count, 0)) As ObjData

            For index As Integer = 1 To count
                Dim section As String = "OBJ" & index
                result(index).Name = reader.GetValue(section, "Name")
                result(index).GrhIndex = ToShort(reader.GetValue(section, "GrhIndex"))
                result(index).ObjType = ToShort(reader.GetValue(section, "ObjType"))
                result(index).Ropaje = ToShort(reader.GetValue(section, "NumRopaje"))
                result(index).Info = reader.GetValue(section, "Info")
                result(index).WeaponAnim = ToShort(reader.GetValue(section, "Anim"))
                result(index).Texto = reader.GetValue(section, "Texto")
                result(index).GrhSecundario = ToShort(reader.GetValue(section, "GrhSec"))
            Next

            Return result
        End Function

        ''' <summary>
        ''' Carga NPCs.dat y NPCs-HOSTILES.dat como CargarIndicesNPC en modIndices.bas.
        ''' Los hostiles conservan el desplazamiento +500 del formato original.
        ''' </summary>
        Public Shared Function LoadNpcs(ByVal dataDirectory As String) As NpcData()
            Dim normalPath As String = Path.Combine(dataDirectory, "NPCs.dat")
            Dim hostilePath As String = Path.Combine(dataDirectory, "NPCs-HOSTILES.dat")
            EnsureFileExists(normalPath, "NPCs.dat")
            EnsureFileExists(hostilePath, "NPCs-HOSTILES.dat")

            Dim normalReader As New clsIniReader()
            normalReader.Initialize(normalPath)
            Dim normalCount As Integer = ReadNonNegativeInt(normalReader.GetValue("INIT", "NumNPCs"))

            Dim hostileReader As New clsIniReader()
            hostileReader.Initialize(hostilePath)
            Dim hostileCount As Integer = ReadNonNegativeInt(hostileReader.GetValue("INIT", "NumNPCs"))

            Dim result(Math.Max(normalCount, hostileCount + 500)) As NpcData
            For index As Integer = 1 To normalCount
                result(index) = ReadNpc(normalReader, index)
            Next
            For index As Integer = 1 To hostileCount
                result(index + 500) = ReadNpc(hostileReader, index + 500)
            Next
            Return result
        End Function

        Private Shared Function ReadNpc(ByVal reader As clsIniReader, ByVal index As Integer) As NpcData
            Return New NpcData With {
                .Name = reader.GetValue("NPC" & index, "Name"),
                .Body = ToShort(reader.GetValue("NPC" & index, "Body")),
                .Head = ToShort(reader.GetValue("NPC" & index, "Head")),
                .Heading = ToByte(reader.GetValue("NPC" & index, "Heading"))}
        End Function

        Private Shared Sub EnsureFileExists(ByVal filePath As String, ByVal fileName As String)
            If Not File.Exists(filePath) Then
                Throw New FileNotFoundException("Falta el archivo 'Datos\" & fileName & "'.", filePath)
            End If
        End Sub

        Private Shared Function ReadNonNegativeInt(ByVal value As String) As Integer
            Dim parsed As Integer
            If Not Integer.TryParse(If(value, String.Empty).Trim(), parsed) Then Return 0
            Return Math.Max(parsed, 0)
        End Function

        Private Shared Function ToShort(ByVal value As String) As Short
            Dim parsed As Integer = ReadNonNegativeInt(value)
            Return CShort(Math.Min(parsed, Short.MaxValue))
        End Function

        Private Shared Function ToByte(ByVal value As String) As Byte
            Dim parsed As Integer = ReadNonNegativeInt(value)
            Return CByte(Math.Min(parsed, Byte.MaxValue))
        End Function
    End Class
End Namespace
