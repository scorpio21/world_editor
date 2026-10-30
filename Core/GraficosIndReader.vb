Imports System.IO
Imports System.Text

Namespace WorldEditorNet.Core
    Public Enum GrhIndexStorageFormat
        IntegerIndexes
        LongIndexes
    End Enum

    Public NotInheritable Class GrhIndexRecord
        Public Property Id As Integer
        Public Property FileNumber As Integer
        Public Property X As Integer
        Public Property Y As Integer
        Public Property Width As Integer
        Public Property Height As Integer
        Public Property Speed As Integer
        Public ReadOnly Property Frames As New List(Of Integer)()
        Public ReadOnly Property IsAnimation As Boolean
            Get
                Return Frames.Count > 1
            End Get
        End Property
    End Class

    ''' <summary>
    ''' Lee el formato Graficos.ind usado por el WorldEditor VB6.
    ''' La cabecera es String * 255 + Long + Long + cinco Integer reservados.
    ''' </summary>
    Public NotInheritable Class GraficosIndReader
        Public Const PrefixSize As Integer = 273
        Public Const MaxFrames As Integer = 25

        Public ReadOnly Property Records As New Dictionary(Of Integer, GrhIndexRecord)()

        Public Shared Function Load(path As String,
                                     Optional format As GrhIndexStorageFormat = GrhIndexStorageFormat.IntegerIndexes) As GraficosIndReader
            If String.IsNullOrWhiteSpace(path) Then Throw New ArgumentException("La ruta de Graficos.ind está vacía.", NameOf(path))
            If Not File.Exists(path) Then Throw New FileNotFoundException("No se encontró Graficos.ind.", path)

            Dim result As New GraficosIndReader()
            Using stream As FileStream = File.OpenRead(path)
                Using reader As New BinaryReader(stream, Encoding.Default, leaveOpen:=False)
                    If stream.Length < PrefixSize Then Throw New InvalidDataException("Graficos.ind no contiene una cabecera completa.")
                    reader.ReadBytes(PrefixSize)

                    Dim lastId As Integer = 0
                    While stream.Position < stream.Length
                        Dim id As Integer = ReadIndex(reader, format, "identificador GRH")
                        If id = 0 Then
                            If stream.Position <> stream.Length Then Throw New InvalidDataException("Graficos.ind contiene datos después del terminador.")
                            Exit While
                        End If
                        If id < 0 OrElse id <= lastId Then Throw New InvalidDataException($"GRH inválido o desordenado: {id}.")
                        If result.Records.ContainsKey(id) Then Throw New InvalidDataException($"GRH duplicado: {id}.")

                        Dim count As Short = ReadInt16(reader, $"número de frames del GRH {id}")
                        If count <= 0 OrElse count > MaxFrames Then Throw New InvalidDataException($"GRH {id}: frames inválidos ({count}).")

                        Dim record As New GrhIndexRecord With {.Id = id}
                        If count = 1 Then
                            record.FileNumber = ReadIndex(reader, format, $"BMP del GRH {id}")
                            record.X = ReadInt16(reader, $"X del GRH {id}")
                            record.Y = ReadInt16(reader, $"Y del GRH {id}")
                            record.Width = ReadInt16(reader, $"ancho del GRH {id}")
                            record.Height = ReadInt16(reader, $"alto del GRH {id}")
                            record.Frames.Add(id)
                        Else
                            For frameIndex As Integer = 1 To count
                                Dim frameId As Integer = ReadIndex(reader, format, $"frame {frameIndex} del GRH {id}")
                                If frameId <= 0 Then Throw New InvalidDataException($"GRH {id}: frame inválido.")
                                record.Frames.Add(frameId)
                            Next
                            record.Speed = ReadInt16(reader, $"velocidad del GRH {id}")
                            If record.Speed <= 0 Then Throw New InvalidDataException($"GRH {id}: velocidad inválida.")
                        End If

                        result.Records.Add(id, record)
                        lastId = id
                    End While
                End Using
            End Using

            Return result
        End Function

        Public Function TryGetRenderable(grh As Integer, ByRef record As GrhIndexRecord) As Boolean
            record = Nothing
            If Not Records.TryGetValue(grh, record) Then Return False
            If Not record.IsAnimation Then Return True
            If record.Frames.Count = 0 Then Return False
            Return Records.TryGetValue(record.Frames(0), record)
        End Function

        Private Shared Function ReadIndex(reader As BinaryReader,
                                          format As GrhIndexStorageFormat,
                                          description As String) As Integer
            If format = GrhIndexStorageFormat.LongIndexes Then
                Return reader.ReadInt32()
            End If
            Return ReadInt16(reader, description)
        End Function

        Private Shared Function ReadInt16(reader As BinaryReader, description As String) As Short
            Try
                Return reader.ReadInt16()
            Catch ex As EndOfStreamException
                Throw New InvalidDataException("Faltan bytes para " & description & ".", ex)
            End Try
        End Function
    End Class
End Namespace
