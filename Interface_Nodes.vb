Public Class Interface_Nodes
    Private Sub Interface_Nodes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckBox1.Checked = N.Painted
        'cbStation.Checked = N.Station
        'Set Switcher to current one!!!!
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        N.Painted = CheckBox1.Checked
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Change Position
        N.Position.X = txtX.Text
        N.Position.Y = txtY.Text
        'Change Priority 
        Dim id As Integer = 0
        For i As Integer = 0 To RS.Nodes.Length - 1
            If RS.Nodes(i).Equals(N) Then
                id = i
            End If
        Next
        Dim int As Integer = cbPriority.Text
        int -= 1
        RS.Railroads_Priority(id)(int) = txtPriority.Text
        Update_Info()
        RS.Update_Train_Railroad_Length()
    End Sub

    Public Shared Sub Update_Info()
        If N Is Nothing Then Exit Sub
        Interface_Nodes.CheckBox1.Checked = N.Painted
        Interface_Nodes.cbStation.Checked = N.Station
        'Update Switcher state
        Interface_Nodes.txtX.Text = N.Position.X
        Interface_Nodes.txtY.Text = N.Position.Y
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If N Is Nothing Then
            Dim Pos As New Point(txtX.Text, txtY.Text)
            RS.Add_Node(Pos, cbStation.Checked)
            N = RS.Nodes(RS.Nodes.Length - 1)
            Update_Info()
            RS.Update_Train_Railroad_Length()
        End If
    End Sub

    Private Sub cbStation_CheckedChanged(sender As Object, e As EventArgs) Handles cbStation.CheckedChanged
        N.Station = cbStation.Checked
    End Sub

    Private Sub cbPriority_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPriority.SelectedIndexChanged
        Dim id As Integer = 0
        For i As Integer = 0 To RS.Nodes.Length - 1
            If RS.Nodes(i).Equals(N) Then
                id = i
            End If
        Next
        Dim int As Integer = cbPriority.Text
        int -= 1
        txtPriority.Text = RS.Railroads_Priority(id)(int).ToString
    End Sub

    Private Sub cbPriority_Click(sender As Object, e As EventArgs) Handles cbPriority.Click
        cbPriority.Items.Clear()
        For i As Integer = 0 To RS.Nodes.Length - 1
            If RS.Nodes(i).Equals(N) Then
                For j As Integer = 0 To RS.Railroads_Priority(i).Length - 1
                    cbPriority.Items.Add((j + 1).ToString)
                Next
                Exit For
            End If
        Next
    End Sub

    Private Sub btnSavePriority_Click(sender As Object, e As EventArgs) Handles btnSavePriority.Click
        N.Save_Priority(cbSavePriority.Text)
    End Sub

    Private Sub cbSavePriority_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSavePriority.SelectedIndexChanged
        N.Set_Priority(cbSavePriority.Text)
    End Sub
End Class
