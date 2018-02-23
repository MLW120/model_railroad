Public Class Node
    Public Position As Point
    Public Painted As Boolean = False
    Public Station As Boolean = False
    Public SavedPriority()() As Integer = {}

    Public Sub New()

    End Sub

    Public Sub New(Pos As Point, st As Boolean)
        Me.Position = Pos
        Me.Station = st
        ReDim Preserve Me.SavedPriority(1)
    End Sub

    Public Function Get_New_Railroad(Railroad As Point, ID As Integer) As Point
        Dim ret As New Point

        Dim TheOtherNode As Integer
        If Railroad.X = ID Then
            TheOtherNode = Railroad.Y
        Else
            TheOtherNode = Railroad.X
        End If

        Dim ID_mem As Integer = TheOtherNode
        Dim Priority_mem As Integer = 0
        For i As Integer = 0 To RS.Railroads_Priority(ID).Length - 1 'each element of my id
            If RS.Railroads_Priority(ID)(i) > Priority_mem And i <> ID And i <> TheOtherNode Then
                Priority_mem = RS.Railroads_Priority(ID)(i)
                ID_mem = i
            End If
        Next
        If Railroad.X = ID Then
            ret.Y = ID
            ret.X = ID_mem
        Else
            ret.X = ID
            ret.Y = ID_mem
        End If
        Return ret
    End Function

    Public Sub Save_Priority(Memory As Integer)
        If Memory <> 0 And Memory <> 1 Then Exit Sub
        Dim ID As Integer = -99
        For i As Integer = 0 To RS.Nodes.Length - 1
            If RS.Nodes(i).Position = N.Position Then
                ID = i
                Exit For
            End If
        Next
        If ID = -99 Then MsgBox("ERROR FINDING NODE")
        ReDim Me.SavedPriority(Memory)(RS.Railroads_Priority(ID).Length - 1)
        For i = 0 To RS.Railroads_Priority(ID).Length - 1
            Me.SavedPriority(Memory)(i) = RS.Railroads_Priority(ID)(i)
        Next
    End Sub

    Public Sub Set_Priority(Memory As Integer)
        If Memory <> 0 And Memory <> 1 Then Exit Sub
        If Me.SavedPriority.Length = 0 Then
            ReDim Preserve Me.SavedPriority(1)
        End If
        If Me.SavedPriority(Memory) Is Nothing Then
            Exit Sub
        End If
        Dim ID As Integer = -99
        For i As Integer = 0 To RS.Nodes.Length - 1
            If RS.Nodes(i).Position = N.Position Then
                ID = i
                Exit For
            End If
        Next
        If ID = -99 Then MsgBox("ERROR FINDING NODE")
        If Me.SavedPriority(Memory).Length <> RS.Railroads_Priority(ID).Length Then
            MsgBox("NOT THE SAME SIZE")
            Exit Sub
        End If
        For i As Integer = 0 To RS.Railroads_Priority(ID).Length - 1
            RS.Railroads_Priority(ID)(i) = Me.SavedPriority(Memory)(i)
        Next
    End Sub
End Class
