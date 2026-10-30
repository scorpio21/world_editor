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

                                If _document IsNot Nothing AndAlso _document.Cells(x, y).Blocked <> 0 Then
                                    e.Graphics.FillRectangle(blockedBrush, cellRect)
                                End If
                                If x = _selectedX AndAlso y = _selectedY Then
                                    e.Graphics.FillRectangle(selectedBrush, cellRect)
                                End If

                                e.Graphics.DrawRectangle(gridPen, cellRect)
                                If _document IsNot Nothing Then
                                    Dim grh As Short = _document.Cells(x, y).Graphic1
                                    TextRenderer.DrawText(e.Graphics, grh.ToString(), Font,
                                                           New Point(left + 2, top + 2), Color.Black,
                                                           TextFormatFlags.NoPadding)
                                End If
                            Next
                        Next
                    End Using
                End Using
            End Using
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
