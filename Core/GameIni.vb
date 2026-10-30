Imports System
Imports Microsoft.VisualBasic

Namespace WorldEditorNet.Core
    ''' <summary>
    ''' Traducción de las estructuras y operaciones de cabecera de modGameIni.bas.
    '''
    ''' La parte de serialización de tGameIni queda bloqueada hasta documentar el
    ''' formato binario real de Inicio.con. El UDT original contiene String de
    ''' longitud variable y VB6 lo escribe mediante Get/Put; no es equivalente a
    ''' escribir directamente una estructura .NET con BinaryReader/BinaryWriter.
    ''' </summary>
    Public Module GameIni
        Public Structure tCabecera
            ''' <summary>Texto de cabecera de longitud fija (255 caracteres en VB6).</summary>
            Public Desc As String
            Public CRC As Integer
            Public MagicWord As Integer
        End Structure

        Public Structure tGameIni
            Public Puerto As Integer
            Public Musica As Byte
            Public fx As Byte
            Public tip As Byte
            Public Password As String
            Public name As String
            Public DirGraficos As String
            Public DirSonidos As String
            Public DirMusica As String
            Public DirMapas As String
            Public NumeroDeBMPs As Integer
            Public NumeroMapas As Short
        End Structure

        Public MiCabecera As tCabecera
        Public Config_Inicio As tGameIni

        ''' <summary>
        ''' Inicializa la cabecera con la misma fórmula usada por VB6.
        ''' No se llama a Randomize aquí porque el original tampoco lo hace.
        ''' </summary>
        Public Sub IniciarCabecera(ByRef Cabecera As tCabecera)
            Cabecera.Desc = "Argentum Online by Noland Studios. Copyright Noland-Studios 2001, pablomarquez@noland-studios.com.ar"
            Cabecera.CRC = CInt(VBMath.Rnd() * 100.0F)
            Cabecera.MagicWord = CInt(VBMath.Rnd() * 10.0F)
        End Sub

        ''' <summary>
        ''' La lectura binaria se mantiene pendiente hasta verificar el formato de Inicio.con.
        ''' </summary>
        Public Function LeerGameIni() As tGameIni
            Throw New NotSupportedException(
                "La lectura de Inicio.con requiere especificar el formato binario de los String variables de tGameIni.")
        End Function

        ''' <summary>
        ''' La escritura binaria se mantiene pendiente hasta verificar el formato de Inicio.con.
        ''' </summary>
        Public Sub EscribirGameIni(ByRef GameIniConfiguration As tGameIni)
            Throw New NotSupportedException(
                "La escritura de Inicio.con requiere especificar el formato binario de los String variables de tGameIni.")
        End Sub
    End Module
End Namespace
