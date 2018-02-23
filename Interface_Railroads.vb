Public Class Interface_Railroads

    Private Sub Interface_Railroads_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Update_Info()
    End Sub

    Private Sub Update_Info()
        ComboBox1.Text = RR.X + 1
        ComboBox2.Text = RR.Y + 1
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Update_Info()
    End Sub

    Private Sub ComboBox1_Click(sender As Object, e As EventArgs) Handles ComboBox1.Click
        ComboBox1.Items.Clear()
        If RS.Nodes IsNot Nothing Then
            For i As Integer = 0 To RS.Nodes.Length - 1
                ComboBox1.Items.Add(i + 1)
            Next
        End If
        ComboBox1.Items.Add("Add")
    End Sub

    Private Sub ComboBox2_Click(sender As Object, e As EventArgs) Handles ComboBox2.Click
        ComboBox2.Items.Clear()
        If RS.Nodes IsNot Nothing Then
            For i As Integer = 0 To RS.Nodes.Length - 1
                ComboBox2.Items.Add(i + 1)
            Next
        End If
        ComboBox2.Items.Add("Add")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Add()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox2.Checked Then
            'if I have to delete the conection in one direction or in both
            RS.Railroads_Priority(RR.X)(RR.Y) = 0
            RS.Railroads_Priority(RR.Y)(RR.X) = 0
        Else
            'if I only remove the railroad in one direction
            RS.Railroads_Priority(RR.X)(RR.Y) = 0
        End If
        Add()
        RS.Update_Railroads_Lenght()
    End Sub

    Private Sub Add()
        Dim ID1 As Integer = ComboBox1.Text - 1
        Dim ID2 As Integer = ComboBox2.Text - 1
        RS.Add_Railroad(ID1, ID2, CheckBox2.Checked)
        RR = New Point(ID1, ID2)
    End Sub
End Class
