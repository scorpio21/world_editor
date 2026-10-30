Imports System
Imports System.IO
Imports Microsoft.VisualBasic

Namespace WorldEditorNet.Core
    ''' <summary>
    ''' Estado que CargarMapIni rellenaba en variables globales y controles VB6.
    ''' Esta clase no conoce formularios: la UI podrá aplicar sus valores después.
    ''' </summary>
    Public NotInheritable Class WorldEditorSettings
        Public Property IniPath As String = String.Empty
        Public Property DirGraficos As String = String.Empty
        Public Property DirMidi As String = String.Empty
        Public Property DirIndex As String = String.Empty
        Public Property DirDats As String = String.Empty
        Public Property LastMapPath As String = String.Empty

        Public Property ObjTranslado As Short
        Public Property MaxGrhs As Short
        Public Property UserPos As Position
        Public Property PantallaX As Short
        Public Property PantallaY As Short
        Public Property ClienteHeight As Short
        Public Property ClienteWidth As Short

        Public Property GuardarConfig As Boolean
        Public Property AutoCapturarTranslados As Boolean
        Public Property AutoCapturarSuperficie As Boolean
        Public Property UtilizarDeshacer As Boolean
        Public Property VerAutomatico As Boolean
        Public Property VerCapa2 As Boolean
        Public Property VerCapa3 As Boolean
        Public Property VerCapa4 As Boolean
        Public Property VerTranslados As Boolean
        Public Property VerObjetos As Boolean
        Public Property VerNpcs As Boolean
        Public Property VerTriggers As Boolean
        Public Property VerGrilla As Boolean
        Public Property VerBloqueos As Boolean

        Public Property ConfigurationMissing As Boolean
    End Class

    ''' <summary>
    ''' Carga Datos\WorldEditor.ini reproduciendo las decisiones de CargarMapIni.
    ''' </summary>
    Public NotInheritable Class WorldEditorSettingsLoader
        Private Const DefaultMaxGrhs As Short = 32200
        Private Const DefaultPantallaX As Short = 19
        Private Const DefaultPantallaY As Short = 22

        Public Function Load(ByVal appPath As String) As WorldEditorSettings
            If String.IsNullOrWhiteSpace(appPath) Then
                Throw New ArgumentException("appPath no puede estar vacío.", NameOf(appPath))
            End If

            Dim normalizedAppPath As String = Path.GetFullPath(appPath)
            Dim settings As New WorldEditorSettings With {
                .IniPath = EnsureTrailingSeparator(normalizedAppPath)
            }

            Dim configurationFile As String = Path.Combine(settings.IniPath, "Datos", "WorldEditor.ini")
            If Not File.Exists(configurationFile) Then
                ApplyMissingConfigurationDefaults(settings)
                Return settings
            End If

            Dim reader As New clsIniReader()
            reader.Initialize(configurationFile)

            settings.ObjTranslado = ToShort(reader.GetValue("CONFIGURACION", "ObjTranslado"))
            settings.AutoCapturarTranslados = ToBoolean(reader.GetValue("CONFIGURACION", "AutoCapturarTrans"))
            settings.AutoCapturarSuperficie = ToBoolean(reader.GetValue("CONFIGURACION", "AutoCapturarSup"))
            settings.UtilizarDeshacer = ToBoolean(reader.GetValue("CONFIGURACION", "UtilizarDeshacer"))
            settings.GuardarConfig = ToBoolean(reader.GetValue("CONFIGURACION", "GuardarConfig"))

            settings.MaxGrhs = ToShort(GeneralUtilities.GetVar(configurationFile, "INDEX", "MaxGrhs"))
            If settings.MaxGrhs < 1 Then settings.MaxGrhs = DefaultMaxGrhs

            settings.LastMapPath = reader.GetValue("PATH", "UltimoMapa")
            settings.DirGraficos = ResolveAndValidateDirectory(
                reader.GetValue("PATH", "DirGraficos"), settings.IniPath, "Graficos")
            settings.DirMidi = ResolveAndValidateDirectory(
                reader.GetValue("PATH", "DirMidi"), settings.IniPath, "MIDI")
            settings.DirIndex = ResolveAndValidateDirectory(
                reader.GetValue("PATH", "DirIndex"), settings.IniPath, "INIT")
            settings.DirDats = ResolveAndValidateDirectory(
                reader.GetValue("PATH", "DirDats"), settings.IniPath, "DATS")

            Dim lastPosition As String = reader.GetValue("MOSTRAR", "LastPos")
            settings.UserPos = New Position With {
                .X = ToShort(GeneralUtilities.ReadField(1, lastPosition, AscW("-"c))),
                .Y = ToShort(GeneralUtilities.ReadField(2, lastPosition, AscW("-"c)))
            }
            NormalizeUserPosition(settings)

            settings.VerAutomatico = ToBoolean(reader.GetValue("MOSTRAR", "ControlAutomatico"))
            settings.VerCapa2 = ToBoolean(reader.GetValue("MOSTRAR", "Capa2"))
            settings.VerCapa3 = ToBoolean(reader.GetValue("MOSTRAR", "Capa3"))
            settings.VerCapa4 = ToBoolean(reader.GetValue("MOSTRAR", "Capa4"))
            settings.VerTranslados = ToBoolean(reader.GetValue("MOSTRAR", "Translados"))
            settings.VerObjetos = ToBoolean(reader.GetValue("MOSTRAR", "Objetos"))
            settings.VerNpcs = ToBoolean(reader.GetValue("MOSTRAR", "NPCs"))
            settings.VerTriggers = ToBoolean(reader.GetValue("MOSTRAR", "Triggers"))
            settings.VerGrilla = ToBoolean(reader.GetValue("MOSTRAR", "Grilla"))
            settings.VerBloqueos = ToBoolean(reader.GetValue("MOSTRAR", "Bloqueos"))

            settings.PantallaX = ToShort(reader.GetValue("MOSTRAR", "PantallaX"))
            settings.PantallaY = ToShort(reader.GetValue("MOSTRAR", "PantallaY"))
            If settings.PantallaX > 23 OrElse settings.PantallaX <= 2 Then settings.PantallaX = 23
            If settings.PantallaY > 32 OrElse settings.PantallaY <= 2 Then settings.PantallaY = 32

            settings.ClienteHeight = ToShort(reader.GetValue("MOSTRAR", "ClienteHeight"))
            settings.ClienteWidth = ToShort(reader.GetValue("MOSTRAR", "ClienteWidth"))
            If settings.ClienteHeight <= 0 Then settings.ClienteHeight = 13
            If settings.ClienteWidth <= 0 Then settings.ClienteWidth = 17

            Return settings
        End Function

        Private Shared Sub ApplyMissingConfigurationDefaults(ByVal settings As WorldEditorSettings)
            settings.ConfigurationMissing = True
            settings.GuardarConfig = True
            settings.DirGraficos = EnsureTrailingSeparator(Path.Combine(settings.IniPath, "Graficos"))
            settings.DirIndex = EnsureTrailingSeparator(Path.Combine(settings.IniPath, "INIT"))
            settings.DirMidi = EnsureTrailingSeparator(Path.Combine(settings.IniPath, "MIDI"))
            settings.DirDats = EnsureTrailingSeparator(Path.Combine(settings.IniPath, "DATS"))
            settings.MaxGrhs = DefaultMaxGrhs
            settings.UserPos = New Position With {.X = 50, .Y = 50}
            settings.PantallaX = DefaultPantallaX
            settings.PantallaY = DefaultPantallaY
        End Sub

        Private Shared Function ResolveAndValidateDirectory(ByVal configuredPath As String,
                                                              ByVal appPath As String,
                                                              ByVal fallbackName As String) As String
            Dim resolved As String = AutoCompletaPath(configuredPath, appPath)
            If resolved = "\" Then
                resolved = EnsureTrailingSeparator(Path.Combine(appPath, fallbackName))
            End If

            If Not Directory.Exists(resolved) Then
                Throw New DirectoryNotFoundException(
                    "El directorio configurado no existe: " & resolved)
            End If

            Return resolved
        End Function

        Private Shared Sub NormalizeUserPosition(ByVal settings As WorldEditorSettings)
            Dim position As Position = settings.UserPos
            If position.X < XMinMapSize OrElse position.X > XMaxMapSize Then
                position.X = 50
            End If
            If position.Y < YMinMapSize OrElse position.Y > YMaxMapSize Then
                position.Y = 50
            End If
            settings.UserPos = position
        End Sub

        Private Shared Function ToShort(ByVal value As String) As Short
            Dim parsed As Double = Val(If(value, String.Empty))
            If parsed > Short.MaxValue Then Return Short.MaxValue
            If parsed < Short.MinValue Then Return Short.MinValue
            Return CShort(parsed)
        End Function

        Private Shared Function ToBoolean(ByVal value As String) As Boolean
            Return Val(If(value, String.Empty)) <> 0
        End Function

        Private Shared Function AutoCompletaPath(ByVal value As String,
                                                 ByVal appPath As String) As String
            Return GeneralUtilities.AutoCompletaPath(value, appPath)
        End Function

        Private Shared Function EnsureTrailingSeparator(ByVal value As String) As String
            If value.EndsWith("\", StringComparison.Ordinal) Then Return value
            Return value & "\"
        End Function
    End Class

    Public NotInheritable Class WorldEditorSettingsSaver
        Private Sub New()
        End Sub

        Public Shared Sub Save(ByVal appPath As String, ByVal settings As WorldEditorSettings)
            If settings Is Nothing Then Throw New ArgumentNullException(NameOf(settings))
            Dim root As String = Path.GetFullPath(appPath)
            Dim dataDirectory As String = Path.Combine(root, "Datos")
            Directory.CreateDirectory(dataDirectory)
            Dim configurationFile As String = Path.Combine(dataDirectory, "WorldEditor.ini")

            GeneralUtilities.WriteVar(configurationFile, "CONFIGURACION", "ObjTranslado", settings.ObjTranslado.ToString())
            GeneralUtilities.WriteVar(configurationFile, "CONFIGURACION", "GuardarConfig", If(settings.GuardarConfig, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "CONFIGURACION", "AutoCapturarTrans", If(settings.AutoCapturarTranslados, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "CONFIGURACION", "AutoCapturarSup", If(settings.AutoCapturarSuperficie, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "CONFIGURACION", "UtilizarDeshacer", If(settings.UtilizarDeshacer, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "INDEX", "MaxGrhs", settings.MaxGrhs.ToString())
            GeneralUtilities.WriteVar(configurationFile, "PATH", "UltimoMapa", settings.LastMapPath)
            GeneralUtilities.WriteVar(configurationFile, "PATH", "DirGraficos", settings.DirGraficos)
            GeneralUtilities.WriteVar(configurationFile, "PATH", "DirMidi", settings.DirMidi)
            GeneralUtilities.WriteVar(configurationFile, "PATH", "DirIndex", settings.DirIndex)
            GeneralUtilities.WriteVar(configurationFile, "PATH", "DirDats", settings.DirDats)
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "LastPos", settings.UserPos.X & "-" & settings.UserPos.Y)
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "ControlAutomatico", If(settings.VerAutomatico, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "Capa2", If(settings.VerCapa2, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "Capa3", If(settings.VerCapa3, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "Capa4", If(settings.VerCapa4, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "Translados", If(settings.VerTranslados, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "Objetos", If(settings.VerObjetos, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "NPCs", If(settings.VerNpcs, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "Triggers", If(settings.VerTriggers, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "Grilla", If(settings.VerGrilla, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "Bloqueos", If(settings.VerBloqueos, "1", "0"))
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "PantallaX", settings.PantallaX.ToString())
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "PantallaY", settings.PantallaY.ToString())
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "ClienteHeight", settings.ClienteHeight.ToString())
            GeneralUtilities.WriteVar(configurationFile, "MOSTRAR", "ClienteWidth", settings.ClienteWidth.ToString())
        End Sub
    End Class
End Namespace
