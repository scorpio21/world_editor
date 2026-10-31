Imports System

Namespace WorldEditorNet.Core
    Public Class SurfacePatternSettings
        Public Property Mosaic As Boolean = True
        Public Property Width As Integer = 1
        Public Property Height As Integer = 1
        Public Property OffsetEnabled As Boolean
        Public Property OffsetX As Integer
        Public Property OffsetY As Integer

        Public Function GetGraphicIndex(ByVal baseGrh As Integer,
                                        ByVal x As Integer,
                                        ByVal y As Integer) As Integer
            If Not Mosaic Then Return baseGrh
            If Width < 1 OrElse Height < 1 Then
                Throw New InvalidOperationException("El ancho y el largo del mosaico deben ser mayores que cero.")
            End If

            Dim shiftX As Integer = If(OffsetEnabled, Me.OffsetX, 0)
            Dim shiftY As Integer = If(OffsetEnabled, Me.OffsetY, 0)
            Return baseGrh + ((y + shiftY) Mod Height) * Width + ((x + shiftX) Mod Width)
        End Function
    End Class
End Namespace
