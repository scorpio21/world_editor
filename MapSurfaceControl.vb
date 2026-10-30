Imports System.Drawing
Imports System.Windows.Forms
Imports WorldEditorNet.WorldEditorNet.Core

Namespace WorldEditorNet
    Public Class MapSurfaceControl
        Inherits Control

        Private _document As MapV1Document
        Private _cellSize As Integer = 24
        Private _selectedX As Integer
        Private _selectedY As Integer
        Private _showLayer1 As Boolean = True
        Private _showLayer2 As Boolean = True
        Private _showLayer3 As Boolean = True
        Private _showLayer4 As Boolean = True
        Private _showGrid As Boolean
        Private _showBlocked As Boolean

        Public Event CellSelected As EventHandler

        Public Property Document As MapV1Document
            Get
                Return _document
            End Get
            Set(value As MapV1Document)
                _document = value
                Invalidate()
            End Set
        End Property

        Public Property CellSize As Integer
            Get
                Return _cellSize
            End Get
            Set(value As Integer)
                _cellSize = Math.Max(8, value)
                UpdateSurfaceSize()
                Invalidate()
            End Set
        End Property

        Public ReadOnly Property SelectedX As Integer
            Get
                Return _selectedX
            End Get
        End Property

        Public ReadOnly Property SelectedY As Integer
            Get
                Return _selectedY
            End Get
        End Property

        Public Property ShowLayer1 As Boolean
            Get
                Return _showLayer1
            End Get
            Set(value As Boolean)
                _showLayer1 = value
                Invalidate()
            End Set
        End Property

        Public Property ShowLayer2 As Boolean
            Get
                Return _showLayer2
            End Get
            Set(value As Boolean)
                _showLayer2 = value
                Invalidate()
            End Set
        End Property

        Public Property ShowLayer3 As Boolean
            Get
                Return _showLayer3
            End Get
            Set(value As Boolean)
                _showLayer3 = value
                Invalidate()
            End Set
        End Property

        Public Property ShowLayer4 As Boolean
            Get
                Return _showLayer4
            End Get
            Set(value As Boolean)
                _showLayer4 = value
                Invalidate()
            End Set
        End Property

        Public Property ShowGrid As Boolean
            Get
                Return _showGrid
            End Get
            Set(value As Boolean)
                _showGrid = value
                Invalidate()
            End Set
        End Property

        Public Property ShowBlocked As Boolean
            Get
                Return _showBlocked
            End Get
            Set(value As Boolean)
                _showBlocked = value
                Invalidate()
            End Set
        End Property

        Public Sub New()
            DoubleBuffered = True
            BackColor = Color.White
            TabStop = True
            UpdateSurfaceSize()
        End Sub

        Public Sub ResetSelection()
            _selectedX = 0
            _selectedY = 0
            Invalidate()
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            Using gridPen As New Pen(Color.LightGray)
                Using blockedBrush As New SolidBrush(Color.FromArgb(150, Color.IndianRed))
                    Using selectedBrush As New SolidBrush(Color.FromArgb(120, Color.Gold))
                        For y As Integer = 1 To 100
                            For x As Integer = 1 To 100
                                Dim left As Integer = (x - 1) * _cellSize
                                Dim top As Integer = (y - 1) * _cellSize
                                Dim cellRect As New Rectangle(left, top, _cellSize, _cellSize)

                                If _showBlocked AndAlso _document IsNot Nothing AndAlso _document.Cells(x, y).Blocked <> 0 Then
                                    e.Graphics.FillRectangle(blockedBrush, cellRect)
                                End If
                                If x = _selectedX AndAlso y = _selectedY Then
                                    e.Graphics.FillRectangle(selectedBrush, cellRect)
                                End If

                                If _showGrid Then e.Graphics.DrawRectangle(gridPen, cellRect)
                                If _document IsNot Nothing Then
                                    Dim cell As MapV1Cell = _document.Cells(x, y)
                                    Dim yOffset As Integer = 2
                                    If _showLayer1 Then DrawGraphic(e.Graphics, cell.Graphic1, left, top, yOffset)
                                    If _showLayer2 Then DrawGraphic(e.Graphics, cell.Graphic2, left, top, yOffset)
                                    If _showLayer3 Then DrawGraphic(e.Graphics, cell.Graphic3, left, top, yOffset)
                                    If _showLayer4 Then DrawGraphic(e.Graphics, cell.Graphic4, left, top, yOffset)
                                End If
                            Next
                        Next
                    End Using
                End Using
            End Using
        End Sub

        Private Sub DrawGraphic(graphics As Graphics, graphic As Short,
                                left As Integer, top As Integer, ByRef yOffset As Integer)
            If graphic = 0 Then Return
            TextRenderer.DrawText(graphics, graphic.ToString(), Font,
                                   New Point(left + 2, top + yOffset), Color.Black,
                                   TextFormatFlags.NoPadding)
            yOffset += Font.Height
        End Sub

        Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
            MyBase.OnMouseClick(e)
            If e.Button <> MouseButtons.Left Then Return

            Dim x As Integer = (e.X \ _cellSize) + 1
            Dim y As Integer = (e.Y \ _cellSize) + 1
            If x < 1 OrElse x > 100 OrElse y < 1 OrElse y > 100 Then Return

            _selectedX = x
            _selectedY = y
            Invalidate()
            RaiseEvent CellSelected(Me, EventArgs.Empty)
        End Sub

        Private Sub UpdateSurfaceSize()
            AutoSize = False
            Size = New Size(_cellSize * 100 + 1, _cellSize * 100 + 1)
        End Sub
    End Class
End Namespace
