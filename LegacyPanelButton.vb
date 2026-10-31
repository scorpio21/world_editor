Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Windows.Forms

Namespace WorldEditorNet
    <DefaultEvent("Click")>
    Public Class LegacyPanelButton
        Inherits Control

        Private _hovered As Boolean
        Private _pressed As Boolean
        Private _selected As Boolean
        Private _gradientColor As Color = Color.FromArgb(190, 245, 226)
        Private _selectedColor As Color = Color.FromArgb(130, 220, 185)
        Private _diagonal As Boolean = True

        <Category("Aomania")>
        <DefaultValue(False)>
        Public Property Selected As Boolean
            Get
                Return _selected
            End Get
            Set(value As Boolean)
                If _selected = value Then Return
                _selected = value
                Invalidate()
            End Set
        End Property

        <Category("Aomania")>
        <DefaultValue(True)>
        Public Property DiagonalShape As Boolean
            Get
                Return _diagonal
            End Get
            Set(value As Boolean)
                If _diagonal = value Then Return
                _diagonal = value
                Invalidate()
            End Set
        End Property

        <Category("Aomania")>
        Public Property GradientColor As Color
            Get
                Return _gradientColor
            End Get
            Set(value As Color)
                _gradientColor = value
                Invalidate()
            End Set
        End Property

        <Category("Aomania")>
        Public Property SelectedColor As Color
            Get
                Return _selectedColor
            End Get
            Set(value As Color)
                _selectedColor = value
                Invalidate()
            End Set
        End Property

        <Category("Aomania")>
        Public Property ButtonImage As Image
            Get
                Return MyBase.BackgroundImage
            End Get
            Set(value As Image)
                MyBase.BackgroundImage = value
                Invalidate()
            End Set
        End Property

        Public Sub New()
            SetStyle(ControlStyles.UserPaint Or ControlStyles.AllPaintingInWmPaint Or
                     ControlStyles.OptimizedDoubleBuffer Or ControlStyles.ResizeRedraw, True)
            TabStop = True
            Font = New Font("Arial", 8.25F, FontStyle.Regular)
            ForeColor = Color.Black
            BackColor = Color.FromArgb(190, 245, 226)
            Size = New Size(130, 66)
        End Sub

        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            MyBase.OnPaint(e)
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias
            Dim bounds = New Rectangle(1, 1, Math.Max(0, Width - 3), Math.Max(0, Height - 3))
            If bounds.Width < 2 OrElse bounds.Height < 2 Then Return

            Dim baseColor = If(_selected, _selectedColor, _gradientColor)
            If _hovered Then baseColor = ControlPaint.Light(baseColor, 0.18F)
            If _pressed Then baseColor = ControlPaint.Dark(baseColor, 0.12F)

            Using path As New GraphicsPath()
                If _diagonal AndAlso bounds.Width >= 20 Then
                    path.AddPolygon({
                        New Point(bounds.Left, bounds.Top),
                        New Point(bounds.Right - 16, bounds.Top),
                        New Point(bounds.Right, bounds.Top + bounds.Height \ 2),
                        New Point(bounds.Right - 16, bounds.Bottom),
                        New Point(bounds.Left, bounds.Bottom),
                        New Point(bounds.Left + 16, bounds.Top + bounds.Height \ 2)})
                Else
                    path.AddRectangle(bounds)
                End If

                Using brush As New LinearGradientBrush(bounds, ControlPaint.Light(baseColor, 0.25F),
                                                       ControlPaint.Dark(baseColor, 0.12F),
                                                       LinearGradientMode.Vertical)
                    e.Graphics.FillPath(brush, path)
                End Using
                Using pen As New Pen(If(_selected OrElse _hovered, Color.DarkGreen, Color.DarkGray), 1.0F)
                    e.Graphics.DrawPath(pen, path)
                End Using

                Dim content = If(String.IsNullOrEmpty(Text), Name, Text).Replace(" (", Environment.NewLine & "(")
                Dim textBounds = bounds
                If ButtonImage IsNot Nothing Then
                    Dim imageRect = New Rectangle(bounds.Left + 6, bounds.Top + (bounds.Height - 24) \ 2, 24, 24)
                    e.Graphics.DrawImage(ButtonImage, imageRect)
                    textBounds = New Rectangle(imageRect.Right + 3, bounds.Top, bounds.Width - imageRect.Right + bounds.Left - 3, bounds.Height)
                End If
                Using format As New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
                    Using textBrush As New SolidBrush(ForeColor)
                        e.Graphics.DrawString(content, Font, textBrush, textBounds, format)
                    End Using
                End Using
            End Using

            If Focused Then
                Using focusPen As New Pen(Color.FromArgb(90, Color.Black))
                    focusPen.DashStyle = DashStyle.Dot
                    e.Graphics.DrawRectangle(focusPen, 3, 3, Width - 7, Height - 7)
                End Using
            End If
        End Sub

        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            _hovered = True
            Invalidate()
            MyBase.OnMouseEnter(e)
        End Sub

        Protected Overrides Sub OnMouseLeave(e As EventArgs)
            _hovered = False
            _pressed = False
            Invalidate()
            MyBase.OnMouseLeave(e)
        End Sub

        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            If e.Button = MouseButtons.Left Then
                _pressed = True
                Invalidate()
            End If
            MyBase.OnMouseDown(e)
        End Sub

        Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
            _pressed = False
            Invalidate()
            MyBase.OnMouseUp(e)
        End Sub
    End Class
End Namespace
