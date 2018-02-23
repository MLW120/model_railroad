Public Class Interface_Trains
    Private Sub Interface_Trains_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If T Is Nothing Then
            'Timer1.Enabled = False !!!! For Adding New ones
        End If
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        For Each Tr In RS.Trains
            If Tr.Equals(T) Then
                Tr.Velocity_To_Reach = TrackBar1.Value
            End If
        Next
    End Sub

    Public Shared Sub Update_Info()
        If T Is Nothing Then
            'Timer1.Enabled = False For adding new ones!!!!
            Exit Sub
        End If
        For Each Tr In RS.Trains
            If Tr.Equals(T) Then
                Interface_Trains.txtRailroadOn.Text = Tr.Railroad_On.X.ToString & "," & Tr.Railroad_On.Y.ToString
            End If
        Next

        For Each Tr In RS.Trains
            If Tr.Equals(T) Then
                Interface_Trains.txtPosition.Text = Tr.Position_On_Railroad
            End If
        Next
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Timer1.Enabled = CheckBox1.Checked
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If T Is Nothing Then
            Timer1.Enabled = False
        End If
        Update_Info()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If T Is Nothing Then
            If txtPosition.Text = "" Or txtRailroadOn.Text = "" Then Exit Sub
            Dim Pos As Double = txtPosition.Text
            Dim Railroad As New Point
            Dim s As String = txtRailroadOn.Text
            Railroad.X = CInt(s.Split(",")(0)) - 1
            Railroad.Y = CInt(s.Split(",")(1)) - 1
            RS.Add_Train(Pos, Railroad)
            T = RS.Trains(RS.Trains.Length - 1)
        Else
            For Each tr In RS.Trains
                If tr.Equals(T) Then
                    If txtPosition.Text = "" Or txtRailroadOn.Text = "" Then Exit Sub
                    tr.Position_On_Railroad = txtPosition.Text
                    Dim s As String = txtRailroadOn.Text
                    tr.Railroad_On.X = s.Split(",")(0)
                    tr.Railroad_On.Y = s.Split(",")(1)
                    tr.Lenght_Of_Railroad = RS.Railroads_Lenght(tr.Railroad_On.X)(tr.Railroad_On.Y)
                    T = tr
                End If
            Next
        End If
    End Sub

    'Add New ones
End Class
