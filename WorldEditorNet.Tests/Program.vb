Imports System
Imports System.IO
Imports System.Text
Imports WorldEditorNet.WorldEditorNet.Core

Module Program
    Sub Main()
        Dim root As String = Path.Combine(Path.GetTempPath(), "worldeditor-v1-test-" & Guid.NewGuid().ToString("N"))
        Directory.CreateDirectory(root)
        Try
            Dim source As New MapV1Document() With {
                .MapVersion = 42,
                .Header = New GameIni.tCabecera With {
                    .Desc = "WorldEditor V1 smoke test",
                    .CRC = 1234,
                    .MagicWord = 5678
                }
            }
            Dim expected As MapV1Cell = source.Cells(1, 1)
            expected.Blocked = 1
            expected.Graphic1 = 6005
            expected.Graphic2 = 6006
            expected.Trigger = 99
            expected.ExitMap = 7
            expected.ExitX = 45
            expected.ExitY = 66
            expected.NpcIndex = 12
            expected.ObjIndex = 33
            expected.ObjAmount = 4
            source.Cells(1, 1) = expected

            Dim mapPath As String = Path.Combine(root, "Mapa1.map")
            Dim codec As New MapV1Codec()
            codec.Save(mapPath, source)
            AssertEqual(False, source.Changed, "estado Changed tras guardar")
            AssertEqual(130273L, New FileInfo(mapPath).Length, "tamaño .map")
            AssertEqual(160010L, New FileInfo(Path.ChangeExtension(mapPath, ".inf")).Length, "tamaño .inf")

            Dim loaded As MapV1Document = codec.Load(mapPath)
            AssertEqual(False, loaded.Changed, "estado Changed tras cargar")
            Dim actual As MapV1Cell = loaded.Cells(1, 1)
            AssertEqual(CShort(42), loaded.MapVersion, "versión")
            AssertEqual("WorldEditor V1 smoke test", loaded.Header.Desc, "descripción")
            AssertEqual(1234, loaded.Header.CRC, "CRC")
            AssertEqual(CShort(1), CShort(actual.Blocked), "bloqueo")
            AssertEqual(CShort(6005), actual.Graphic1, "GRH capa 1")
            AssertEqual(CShort(6006), actual.Graphic2, "GRH capa 2")
            AssertEqual(CShort(99), actual.Trigger, "trigger")
            AssertEqual(CShort(7), actual.ExitMap, "mapa de salida")
            AssertEqual(CShort(45), actual.ExitX, "X de salida")
            AssertEqual(CShort(66), actual.ExitY, "Y de salida")
            AssertEqual(CShort(12), actual.NpcIndex, "NPC")
            AssertEqual(CShort(33), actual.ObjIndex, "objeto")
            AssertEqual(CShort(4), actual.ObjAmount, "cantidad")

            loaded.Changed = True
            codec.Save(mapPath, loaded)
            AssertEqual(False, loaded.Changed, "estado Changed tras segundo guardado")

            Dim indexPath As String = Path.Combine(root, "Graficos.ind")
            WriteSyntheticGraphicsIndex(indexPath)
            Dim graphicsIndex As GraficosIndReader = GraficosIndReader.Load(indexPath)
            AssertEqual(2, graphicsIndex.Records.Count, "registros Graficos.ind")
            Dim renderable As GrhIndexRecord = Nothing
            If Not graphicsIndex.TryGetRenderable(2, renderable) Then
                Throw New InvalidOperationException("No se resolvió la animación del GRH 2.")
            End If
            AssertEqual(1, renderable.FileNumber, "BMP del primer frame")
            AssertEqual(32, renderable.Width, "ancho del primer frame")

            Dim settings As New WorldEditorSettings With {
                .GuardarConfig = True,
                .DirGraficos = Path.Combine(root, "Graficos") & "\",
                .DirMidi = Path.Combine(root, "MIDI") & "\",
                .DirIndex = Path.Combine(root, "INIT") & "\",
                .DirDats = Path.Combine(root, "DATS") & "\",
                .MaxGrhs = 32200,
                .VerCapa2 = True,
                .VerGrilla = True,
                .UserPos = New Position With {.X = 45, .Y = 66}}
            Directory.CreateDirectory(Path.Combine(root, "Graficos"))
            Directory.CreateDirectory(Path.Combine(root, "MIDI"))
            Directory.CreateDirectory(Path.Combine(root, "INIT"))
            Directory.CreateDirectory(Path.Combine(root, "DATS"))
            WorldEditorSettingsSaver.Save(root, settings)
            Dim loadedSettings As WorldEditorSettings = New WorldEditorSettingsLoader().Load(root)
            AssertEqual(True, loadedSettings.GuardarConfig, "GuardarConfig")
            AssertEqual(True, loadedSettings.VerGrilla, "Grilla")
            AssertEqual(CShort(45), loadedSettings.UserPos.X, "posición X")
            AssertEqual(CShort(66), loadedSettings.UserPos.Y, "posición Y")
            Console.WriteLine("MapV1Codec smoke test: OK")
        Finally
            If Directory.Exists(root) Then Directory.Delete(root, recursive:=True)
        End Try
    End Sub

    Private Sub WriteSyntheticGraphicsIndex(path As String)
        Using stream As FileStream = File.Create(path)
            Using writer As New BinaryWriter(stream, Encoding.Default, leaveOpen:=False)
                writer.Write(New Byte(272) {})

                writer.Write(CShort(1))
                writer.Write(CShort(1))
                writer.Write(CShort(1))
                writer.Write(CShort(0))
                writer.Write(CShort(0))
                writer.Write(CShort(32))
                writer.Write(CShort(32))

                writer.Write(CShort(2))
                writer.Write(CShort(2))
                writer.Write(CShort(1))
                writer.Write(CShort(3))
                writer.Write(CShort(1))
            End Using
        End Using
    End Sub

    Private Sub AssertEqual(Of T)(ByVal expected As T, ByVal actual As T, ByVal name As String)
        If Not Object.Equals(expected, actual) Then
            Throw New InvalidOperationException($"{name}: esperado '{expected}', obtenido '{actual}'.")
        End If
    End Sub
End Module
