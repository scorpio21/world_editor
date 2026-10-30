Imports System.Globalization
Imports System.IO
Imports System.Text

Namespace WorldEditorNet.Core
    ''' <summary>
    ''' Equivalent of the VB6 clsIniReader.
    ''' Loads an INI file into memory, sorts sections and keys, and retrieves
    ''' values using binary search.
    ''' </summary>
    Public NotInheritable Class clsIniReader
        Private NotInheritable Class ChildNode
            Public Property Key As String = String.Empty
            Public Property Value As String = String.Empty
        End Class

        Private NotInheritable Class MainNode
            Public Property Name As String = String.Empty
            Public ReadOnly Property Values As New List(Of ChildNode)()
        End Class

        Private ReadOnly fileData As New List(Of MainNode)()

        ''' <summary>
        ''' Loads the file. As in VB6, loading again on the same instance does
        ''' not clear previously loaded sections; callers normally create one
        ''' reader per file.
        ''' </summary>
        Public Sub Initialize(file As String)
            Using reader As New StreamReader(file, GetLegacyEncoding(), detectEncodingFromByteOrderMarks:=True)
                While Not reader.EndOfStream
                    Dim textLine As String = reader.ReadLine()
                    If textLine Is Nothing OrElse textLine.Length = 0 Then
                        Continue While
                    End If

                    If textLine.StartsWith("[", StringComparison.Ordinal) Then
                        Dim closingBracket As Integer = textLine.IndexOf("]"c, 1)
                        If closingBracket >= 1 Then
                            Dim node As New MainNode With {
                                .Name = NormalizeSection(textLine.Substring(1, closingBracket - 1))
                            }
                            fileData.Add(node)
                        End If
                    Else
                        Dim equalsPosition As Integer = textLine.IndexOf("="c, 1)
                        If equalsPosition >= 1 AndAlso fileData.Count > 0 Then
                            Dim node As MainNode = fileData(fileData.Count - 1)
                            node.Values.Add(New ChildNode With {
                                .Value = textLine.Substring(equalsPosition + 1),
                                .Key = NormalizeKey(textLine.Substring(0, equalsPosition))
                            })
                        End If
                    End If
                End While
            End Using

            If fileData.Count = 0 Then Return

            SortMainNodes(0, fileData.Count - 1)
            For Each node As MainNode In fileData
                If node.Values.Count > 0 Then
                    SortChildNodes(node, 0, node.Values.Count - 1)
                End If
            Next
        End Sub

        ''' <summary>
        ''' Returns an empty string when the section or key does not exist,
        ''' matching the VB6 implementation.
        ''' </summary>
        Public Function GetValue(main As String, key As String) As String
            Dim sectionIndex As Integer = FindMain(NormalizeKey(main))
            If sectionIndex < 0 Then Return String.Empty

            Dim valueIndex As Integer = FindKey(fileData(sectionIndex), NormalizeKey(key))
            If valueIndex < 0 Then Return String.Empty

            Return fileData(sectionIndex).Values(valueIndex).Value
        End Function

        Private Shared Function GetLegacyEncoding() As Encoding
            Dim codePage As Integer = CultureInfo.CurrentCulture.TextInfo.ANSICodePage
            If codePage <= 0 Then codePage = 1252
            Return Encoding.GetEncoding(codePage)
        End Function

        Private Shared Function NormalizeSection(value As String) As String
            Return value.Trim().ToUpper(CultureInfo.CurrentCulture)
        End Function

        Private Shared Function NormalizeKey(value As String) As String
            'The VB6 code does not Trim keys before UCase; preserve that.
            Return value.ToUpper(CultureInfo.CurrentCulture)
        End Function

        Private Shared Sub SortChildNodes(node As MainNode, first As Integer, last As Integer)
            Dim minimum As Integer = first
            Dim maximum As Integer = last
            Dim comparisonKey As String = node.Values((minimum + maximum) \ 2).Key

            While minimum <= maximum
                While String.CompareOrdinal(node.Values(minimum).Key, comparisonKey) < 0 AndAlso minimum < last
                    minimum += 1
                End While
                While String.CompareOrdinal(node.Values(maximum).Key, comparisonKey) > 0 AndAlso maximum > first
                    maximum -= 1
                End While
                If minimum <= maximum Then
                    Dim temporary As ChildNode = node.Values(minimum)
                    node.Values(minimum) = node.Values(maximum)
                    node.Values(maximum) = temporary
                    minimum += 1
                    maximum -= 1
                End If
            End While

            If first < maximum Then SortChildNodes(node, first, maximum)
            If minimum < last Then SortChildNodes(node, minimum, last)
        End Sub

        Private Sub SortMainNodes(first As Integer, last As Integer)
            Dim minimum As Integer = first
            Dim maximum As Integer = last
            Dim comparisonName As String = fileData((minimum + maximum) \ 2).Name

            While minimum <= maximum
                While String.CompareOrdinal(fileData(minimum).Name, comparisonName) < 0 AndAlso minimum < last
                    minimum += 1
                End While
                While String.CompareOrdinal(fileData(maximum).Name, comparisonName) > 0 AndAlso maximum > first
                    maximum -= 1
                End While
                If minimum <= maximum Then
                    Dim temporary As MainNode = fileData(minimum)
                    fileData(minimum) = fileData(maximum)
                    fileData(maximum) = temporary
                    minimum += 1
                    maximum -= 1
                End If
            End While

            If first < maximum Then SortMainNodes(first, maximum)
            If minimum < last Then SortMainNodes(minimum, last)
        End Sub

        Private Shared Function FindKey(node As MainNode, key As String) As Integer
            Dim minimum As Integer = 0
            Dim maximum As Integer = node.Values.Count - 1
            Dim middle As Integer = 0

            While minimum <= maximum
                middle = (minimum + maximum) \ 2
                Dim comparison As Integer = String.CompareOrdinal(node.Values(middle).Key, key)
                If comparison < 0 Then
                    minimum = middle + 1
                ElseIf comparison > 0 Then
                    maximum = middle - 1
                Else
                    Return middle
                End If
            End While

            Return Not middle
        End Function

        Private Function FindMain(name As String) As Integer
            Dim minimum As Integer = 0
            Dim maximum As Integer = fileData.Count - 1
            Dim middle As Integer = 0

            While minimum <= maximum
                middle = (minimum + maximum) \ 2
                Dim comparison As Integer = String.CompareOrdinal(fileData(middle).Name, name)
                If comparison < 0 Then
                    minimum = middle + 1
                ElseIf comparison > 0 Then
                    maximum = middle - 1
                Else
                    Return middle
                End If
            End While

            Return Not middle
        End Function
    End Class
End Namespace
