Imports System

Namespace WorldEditorNet.Core
    ''' <summary>
    ''' Reproduce la lógica de cmdAplicar_Click de frmUnionAdyacente.frm.
    ''' La ventana solo proporciona mapas, límites y casillas; esta clase no
    ''' decide valores ni crea controles.
    ''' </summary>
    Public NotInheritable Class AdjacentTransferOptions
        Public Property NorthMap As Integer = 0
        Public Property EastMap As Integer = 0
        Public Property SouthMap As Integer = 0
        Public Property WestMap As Integer = 0
        Public Property ApplyNorth As Boolean
        Public Property ApplyEast As Boolean
        Public Property ApplySouth As Boolean
        Public Property ApplyWest As Boolean

        ' PosLim(0..7) del formulario VB6.
        Public Property MaxY As Integer = 91
        Public Property MinY As Integer = 10
        Public Property MaxX As Integer = 90
        Public Property MinX As Integer = 11
        Public Property NorthDestinationY As Integer = 90
        Public Property SouthDestinationY As Integer = 11
        Public Property EastDestinationX As Integer = 12
        Public Property WestDestinationX As Integer = 89
    End Class

    Public NotInheritable Class AdjacentTransferService
        Private Sub New()
        End Sub

        Public Shared Function Apply(ByVal document As MapV1Document,
                                     ByVal options As AdjacentTransferOptions) As Boolean
            If document Is Nothing Then Throw New ArgumentNullException(NameOf(document))
            If options Is Nothing Then Throw New ArgumentNullException(NameOf(options))
            ValidateLimits(options)

            Dim changed As Boolean = False
            If options.ApplyNorth Then changed = ApplyNorth(document, options) OrElse changed
            If options.ApplyEast Then changed = ApplyEast(document, options) OrElse changed
            If options.ApplySouth Then changed = ApplySouth(document, options) OrElse changed
            If options.ApplyWest Then changed = ApplyWest(document, options) OrElse changed
            If changed Then
                document.Changed = True
            End If
            Return changed
        End Function

        Public Shared Function ReadExistingMaps(ByVal document As MapV1Document,
                                                ByVal options As AdjacentTransferOptions) As Integer()
            If document Is Nothing Then Throw New ArgumentNullException(NameOf(document))
            If options Is Nothing Then Throw New ArgumentNullException(NameOf(options))
            ValidateLimits(options)

            Return {
                ReadNorth(document, options),
                ReadEast(document, options),
                ReadSouth(document, options),
                ReadWest(document, options)}
        End Function

        Public Shared Sub UseBasicLimits(ByVal options As AdjacentTransferOptions)
            If options Is Nothing Then Throw New ArgumentNullException(NameOf(options))
            options.MaxY = 91
            options.MinY = 10
            options.MaxX = 90
            options.MinX = 11
            options.NorthDestinationY = 90
            options.SouthDestinationY = 11
            options.EastDestinationX = 12
            options.WestDestinationX = 89
        End Sub

        Public Shared Sub UseUllaLimits(ByVal options As AdjacentTransferOptions)
            If options Is Nothing Then Throw New ArgumentNullException(NameOf(options))
            options.MaxY = 94
            options.MinY = 7
            options.MaxX = 92
            options.MinX = 9
            options.NorthDestinationY = 93
            options.SouthDestinationY = 8
            options.EastDestinationX = 10
            options.WestDestinationX = 91
        End Sub

        Private Shared Function ApplyNorth(document As MapV1Document,
                                            options As AdjacentTransferOptions) As Boolean
            Dim changed As Boolean = False
            For x As Integer = options.MinX + 1 To options.MaxX - 1
                changed = SetExit(document, x, options.MinY, options.NorthMap,
                                   If(options.NorthMap = 0, 0, x),
                                   If(options.NorthMap = 0, 0, options.NorthDestinationY)) OrElse changed
            Next
            Return changed
        End Function

        Private Shared Function ApplyEast(document As MapV1Document,
                                          options As AdjacentTransferOptions) As Boolean
            Dim changed As Boolean = False
            For y As Integer = options.MinY + 1 To options.MaxY - 1
                changed = SetExit(document, options.MaxX, y, options.EastMap,
                                   If(options.EastMap = 0, 0, options.EastDestinationX),
                                   If(options.EastMap = 0, 0, y)) OrElse changed
            Next
            Return changed
        End Function

        Private Shared Function ApplySouth(document As MapV1Document,
                                           options As AdjacentTransferOptions) As Boolean
            Dim changed As Boolean = False
            For x As Integer = options.MinX + 1 To options.MaxX - 1
                changed = SetExit(document, x, options.MaxY, options.SouthMap,
                                   If(options.SouthMap = 0, 0, x),
                                   If(options.SouthMap = 0, 0, options.SouthDestinationY)) OrElse changed
            Next
            Return changed
        End Function

        Private Shared Function ApplyWest(document As MapV1Document,
                                          options As AdjacentTransferOptions) As Boolean
            Dim changed As Boolean = False
            For y As Integer = options.MinY + 1 To options.MaxY - 1
                changed = SetExit(document, options.MinX, y, options.WestMap,
                                   If(options.WestMap = 0, 0, options.WestDestinationX),
                                   If(options.WestMap = 0, 0, y)) OrElse changed
            Next
            Return changed
        End Function

        Private Shared Function SetExit(document As MapV1Document,
                                        x As Integer,
                                        y As Integer,
                                        map As Integer,
                                        destinationX As Integer,
                                        destinationY As Integer) As Boolean
            If document.Cells(x, y).Blocked <> 0 Then Return False
            Dim cell As MapV1Cell = document.Cells(x, y)
            If cell.ExitMap = map AndAlso cell.ExitX = destinationX AndAlso cell.ExitY = destinationY Then Return False
            cell.ExitMap = CShort(map)
            cell.ExitX = CShort(destinationX)
            cell.ExitY = CShort(destinationY)
            document.Cells(x, y) = cell
            Return True
        End Function

        Private Shared Function ReadNorth(document As MapV1Document, options As AdjacentTransferOptions) As Integer
            For x As Integer = options.MinX + 1 To options.MaxX - 1
                If document.Cells(x, options.MinY).ExitMap > 0 Then Return document.Cells(x, options.MinY).ExitMap
            Next
            Return 0
        End Function

        Private Shared Function ReadEast(document As MapV1Document, options As AdjacentTransferOptions) As Integer
            For y As Integer = options.MinY + 1 To options.MaxY - 1
                If document.Cells(options.MaxX, y).ExitMap > 0 Then Return document.Cells(options.MaxX, y).ExitMap
            Next
            Return 0
        End Function

        Private Shared Function ReadSouth(document As MapV1Document, options As AdjacentTransferOptions) As Integer
            For x As Integer = options.MinX + 1 To options.MaxX - 1
                If document.Cells(x, options.MaxY).ExitMap > 0 Then Return document.Cells(x, options.MaxY).ExitMap
            Next
            Return 0
        End Function

        Private Shared Function ReadWest(document As MapV1Document, options As AdjacentTransferOptions) As Integer
            For y As Integer = options.MinY + 1 To options.MaxY - 1
                If document.Cells(options.MinX, y).ExitMap > 0 Then Return document.Cells(options.MinX, y).ExitMap
            Next
            Return 0
        End Function

        Private Shared Sub ValidateLimits(options As AdjacentTransferOptions)
            If options.MinX < 1 OrElse options.MinY < 1 OrElse
               options.MaxX > 100 OrElse options.MaxY > 100 OrElse
               options.MinX >= options.MaxX OrElse options.MinY >= options.MaxY Then
                Throw New ArgumentOutOfRangeException(NameOf(options), "Los límites deben estar dentro de 1..100.")
            End If
        End Sub
    End Class
End Namespace
