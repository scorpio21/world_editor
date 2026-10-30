Imports System
Imports System.IO
Imports System.Text

Namespace WorldEditorNet.Core
    ''' <summary>Contenido de una celda del formato V1.</summary>
    Public Structure MapV1Cell
        Public Blocked As Byte
        Public Graphic1 As Short
        Public Graphic2 As Short
        Public Graphic3 As Short
        Public Graphic4 As Short
        Public Trigger As Short
        Public ExitMap As Short
        Public ExitX As Short
        Public ExitY As Short
        Public NpcIndex As Short
        Public ObjIndex As Short
        Public ObjAmount As Short
    End Structure

    ''' <summary>
    ''' Mapa V1 leído de .map/.inf. Las celdas usan una celda 0 reservada para
    ''' que los índices 1..100 del formato original sean visibles sin conversión.
    ''' </summary>
    Public NotInheritable Class MapV1Document
        Public Property MapVersion As Short
        Public Property Header As GameIni.tCabecera
        Public Property Changed As Boolean
        Public ReadOnly Property Cells As MapV1Cell(,)

        Public Sub New()
            Cells = New MapV1Cell(100, 100) {}
        End Sub
    End Class

    ''' <summary>
    ''' Codec verificado del formato V1 de AomaniaWorldEditor.
    ''' </summary>
    Public NotInheritable Class MapV1Codec
        Private Const MapMin As Integer = 1
        Private Const MapMax As Integer = 100
        Private Const HeaderDescriptionLength As Integer = 255

        Public Function Load(ByVal mapPath As String) As MapV1Document
            Dim infPath As String = Path.ChangeExtension(mapPath, ".inf")
            If Not File.Exists(infPath) Then
                Throw New FileNotFoundException("No se encontró el archivo .inf asociado.", infPath)
            End If

            Dim document As New MapV1Document()
            Using mapReader As New BinaryReader(File.Open(mapPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                document.MapVersion = mapReader.ReadInt16()
                document.Header = ReadHeader(mapReader)
                document.Changed = False
                For i As Integer = 1 To 4
                    mapReader.ReadInt16()
                Next

                For y As Integer = MapMin To MapMax
                    For x As Integer = MapMin To MapMax
                        Dim cell As MapV1Cell = document.Cells(x, y)
                        cell.Blocked = mapReader.ReadByte()
                        cell.Graphic1 = mapReader.ReadInt16()
                        cell.Graphic2 = mapReader.ReadInt16()
                        cell.Graphic3 = mapReader.ReadInt16()
                        cell.Graphic4 = mapReader.ReadInt16()
                        cell.Trigger = mapReader.ReadInt16()
                        mapReader.ReadInt16() ' reserved field
                        document.Cells(x, y) = cell
                    Next
                Next
            End Using

            Using infReader As New BinaryReader(File.Open(infPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                For i As Integer = 1 To 5
                    infReader.ReadInt16()
                Next

                For y As Integer = MapMin To MapMax
                    For x As Integer = MapMin To MapMax
                        Dim cell As MapV1Cell = document.Cells(x, y)
                        cell.ExitMap = infReader.ReadInt16()
                        cell.ExitX = infReader.ReadInt16()
                        cell.ExitY = infReader.ReadInt16()
                        cell.NpcIndex = infReader.ReadInt16()
                        cell.ObjIndex = infReader.ReadInt16()
                        cell.ObjAmount = infReader.ReadInt16()
                        infReader.ReadInt16() ' reserved field
                        infReader.ReadInt16() ' reserved field
                        document.Cells(x, y) = cell
                    Next
                Next
            End Using

            Return document
        End Function

        Public Sub Save(ByVal mapPath As String, ByVal document As MapV1Document)
            Dim infPath As String = Path.ChangeExtension(mapPath, ".inf")

            Using mapWriter As New BinaryWriter(File.Open(mapPath, FileMode.Create, FileAccess.Write, FileShare.None))
                mapWriter.Write(document.MapVersion)
                WriteHeader(mapWriter, document.Header)
                For i As Integer = 1 To 4
                    mapWriter.Write(CShort(0))
                Next

                For y As Integer = MapMin To MapMax
                    For x As Integer = MapMin To MapMax
                        Dim cell As MapV1Cell = document.Cells(x, y)
                        mapWriter.Write(cell.Blocked)
                        mapWriter.Write(cell.Graphic1)
                        mapWriter.Write(cell.Graphic2)
                        mapWriter.Write(cell.Graphic3)
                        mapWriter.Write(cell.Graphic4)
                        mapWriter.Write(cell.Trigger)
                        mapWriter.Write(CShort(0))
                    Next
                Next
            End Using

            Using infWriter As New BinaryWriter(File.Open(infPath, FileMode.Create, FileAccess.Write, FileShare.None))
                For i As Integer = 1 To 5
                    infWriter.Write(CShort(0))
                Next

                For y As Integer = MapMin To MapMax
                    For x As Integer = MapMin To MapMax
                        Dim cell As MapV1Cell = document.Cells(x, y)
                        infWriter.Write(cell.ExitMap)
                        infWriter.Write(cell.ExitX)
                        infWriter.Write(cell.ExitY)
                        infWriter.Write(cell.NpcIndex)
                        infWriter.Write(cell.ObjIndex)
                        infWriter.Write(cell.ObjAmount)
                        infWriter.Write(CShort(0))
                        infWriter.Write(CShort(0))
                    Next
                Next
            End Using

            document.Changed = False
        End Sub

        Private Shared Function ReadHeader(ByVal reader As BinaryReader) As GameIni.tCabecera
            Dim descriptionBytes() As Byte = reader.ReadBytes(HeaderDescriptionLength)
            If descriptionBytes.Length <> HeaderDescriptionLength Then
                Throw New EndOfStreamException("La cabecera del mapa está truncada.")
            End If

            Return New GameIni.tCabecera With {
                .Desc = Encoding.Latin1.GetString(descriptionBytes).TrimEnd(" "c, ChrW(0)),
                .CRC = reader.ReadInt32(),
                .MagicWord = reader.ReadInt32()
            }
        End Function

        Private Shared Sub WriteHeader(ByVal writer As BinaryWriter,
                                       ByVal header As GameIni.tCabecera)
            Dim descriptionBytes(HeaderDescriptionLength - 1) As Byte
            For i As Integer = 0 To descriptionBytes.Length - 1
                descriptionBytes(i) = &H20
            Next

            Dim encoded() As Byte = Encoding.Latin1.GetBytes(If(header.Desc, String.Empty))
            Array.Copy(encoded, descriptionBytes, Math.Min(encoded.Length, descriptionBytes.Length))
            writer.Write(descriptionBytes)
            writer.Write(header.CRC)
            writer.Write(header.MagicWord)
        End Sub
    End Class
End Namespace
