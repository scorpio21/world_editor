Imports System
Imports System.Runtime.InteropServices
Imports System.Text

Namespace WorldEditorNet.Core
    ''' <summary>
    ''' Parte independiente de modGeneral.bas.
    '''
    ''' Se mantienen aquí las funciones que no dependen de formularios, DirectX,
    ''' sonido ni del estado del mapa. El arranque y el bucle principal se
    ''' trasladarán después de portar sus módulos dependientes.
    ''' </summary>
    Public Module GeneralUtilities
        Private Const IniBufferLength As Integer = 5000

        Private Declare Auto Function GetPrivateProfileString Lib "kernel32" _
            (lpAppName As String,
             lpKeyName As String,
             lpDefault As String,
             lpReturnedString As StringBuilder,
             nSize As Integer,
             lpFileName As String) As Integer

        Private Declare Auto Function WritePrivateProfileString Lib "kernel32" _
            (lpAppName As String,
             lpKeyName As String,
             lpString As String,
             lpFileName As String) As Boolean

        ''' <summary>Equivalente directo de la función VB6 MinInt.</summary>
        Public Function MinInt(ByVal a As Short, ByVal b As Short) As Short
            If a <= b Then Return a
            Return b
        End Function

        ''' <summary>Equivalente directo de la función VB6 MaxInt.</summary>
        Public Function MaxInt(ByVal a As Short, ByVal b As Short) As Short
            If a >= b Then Return a
            Return b
        End Function

        ''' <summary>
        ''' Devuelve el campo posicional separado por el carácter indicado.
        ''' Conserva los campos vacíos finales, como el código VB6 original.
        ''' </summary>
        Public Function ReadField(ByVal position As Short,
                                  ByVal text As String,
                                  ByVal separatorAscii As Short) As String
            If position <= 0 Then Return String.Empty
            If text Is Nothing Then text = String.Empty

            Dim separator As Char = ChrW(separatorAscii)
            Dim fields() As String = text.Split(New Char() {separator},
                                                 StringSplitOptions.None)

            If position > fields.Length Then Return String.Empty
            Return fields(position - 1)
        End Function

        ''' <summary>
        ''' Normaliza una ruta al estilo usado por VB6: convierte '/', añade la
        ''' barra final y resuelve una ruta que empieza por '\' contra appPath.
        ''' </summary>
        Public Function AutoCompletaPath(ByVal path As String,
                                         ByVal appPath As String) As String
            If path Is Nothing Then path = String.Empty
            If appPath Is Nothing Then appPath = String.Empty

            path = path.Replace("/"c, "\"c)
            If path.StartsWith("\", StringComparison.Ordinal) Then
                path = appPath.TrimEnd("\"c) & path
            End If

            If Not path.EndsWith("\", StringComparison.Ordinal) Then
                path &= "\"
            End If

            Return path
        End Function

        ''' <summary>
        ''' Lee una variable de un INI usando la API Win32 que empleaba VB6.
        ''' </summary>
        Public Function GetVar(ByVal file As String,
                               ByVal main As String,
                               ByVal variable As String) As String
            Dim buffer As New StringBuilder(IniBufferLength)
            Dim length As Integer = GetPrivateProfileString(
                main, variable, String.Empty, buffer, buffer.Capacity, file)

            If length <= 0 Then Return String.Empty
            Return buffer.ToString(0, Math.Min(length, buffer.Length)).TrimEnd(" "c)
        End Function

        ''' <summary>Escribe una variable de un INI mediante Win32.</summary>
        Public Sub WriteVar(ByVal file As String,
                            ByVal main As String,
                            ByVal variable As String,
                            ByVal value As String)
            If Not WritePrivateProfileString(main, variable, value, file) Then
                Throw New ComponentModel.Win32Exception(Marshal.GetLastWin32Error())
            End If
        End Sub

        ''' <summary>
        ''' Equivalente de RandomNumber. VB6 reinicializaba la semilla con Timer
        ''' en cada llamada; Random.Shared evita compartir estado manualmente,
        ''' pero conserva el intervalo inclusivo documentado por el original.
        ''' </summary>
        Public Function RandomNumber(ByVal lowerBound As Single,
                                     ByVal upperBound As Single) As Single
            If upperBound < lowerBound Then
                Throw New ArgumentException("upperBound debe ser mayor o igual que lowerBound.")
            End If

            Return CSng((upperBound - lowerBound + 1.0F) * Random.Shared.NextSingle() + lowerBound)
        End Function
    End Module
End Namespace
