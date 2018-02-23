Public Class Form1
    Public Imag As Bitmap
    Public F As Font
    Private Calculant As Boolean = False
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        F = Label1.Font
        RS = New Railsystem
        'RS.Add_Node(New Point(0, 0), False)
        'RS.Add_Node(New Point(100, 100), False)
        'RS.Add_Node(New Point(200, 100), False)
        'RS.Add_Node(New Point(300, 300), False)
        'RS.Add_Node(New Point(100, 200), False)
        'RS.Add_Railroad(0, 1, True)
        'RS.Add_Railroad(1, 2, True)
        'RS.Add_Railroad(0, 2, True)
        'RS.Add_Railroad(0, 3, True)
        'RS.Add_Train(10, New Point(1, 2))
        'RS.Trains(0).Velocity_To_Reach = 0
        RS.Show()
        PictureBox1.Image = Imag
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Calculant Then Exit Sub
        Calculant = True
        RS.Update()
        RS.Show()
        PictureBox1.Image = Imag
        Calculant = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Serialize_RS()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Deserialize_RS()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim frm As New Form
        'frm = Interface_Trains
        'T = RS.Trains(0)
        'frm.Show()

        'Change_Train_Velocity()

        'Dim frm As New TCP_Server
        'frm.Show()

        BackgroundWorker1.RunWorkerAsync()
        lblTCP.Text = "CONNECTED"
    End Sub

    Private Sub CBTrains_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBTrains.SelectedIndexChanged
        Dim frm As New Form
        frm = Interface_Trains
        If CBTrains.Text <> "Add" Then
            T = RS.Trains(CBTrains.Text - 1)
        Else
            T = Nothing
        End If
        frm.Show()
    End Sub

    Private Sub CBTrains_Click(sender As Object, e As EventArgs) Handles CBTrains.Click
        CBTrains.Items.Clear()
        If RS.Trains IsNot Nothing Then
            For i As Integer = 0 To RS.Trains.Length - 1
                CBTrains.Items.Add(i + 1)
            Next
        End If
        CBTrains.Items.Add("Add")
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        PaintAllNodes(CheckBox1.Checked)
    End Sub

    Private Sub CBNodes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBNodes.SelectedIndexChanged
        Dim frm As New Form
        frm = Interface_Nodes
        If CBNodes.Text <> "Add" Then
            N = RS.Nodes(CBNodes.Text - 1)
        Else
            N = Nothing
        End If
        frm.Show()
        Interface_Nodes.Update_Info()
    End Sub

    Private Sub CBNodes_Click(sender As Object, e As EventArgs) Handles CBNodes.Click
        CBNodes.Items.Clear()
        If RS.Nodes IsNot Nothing Then
            For i As Integer = 0 To RS.Nodes.Length - 1
                CBNodes.Items.Add(i + 1)
            Next
        End If
        CBNodes.Items.Add("Add")
    End Sub

    Private Sub btnRailroads_Click(sender As Object, e As EventArgs) Handles btnRailroads.Click
        Dim frm As New Form
        frm = Interface_Railroads
        frm.Show()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim loc As System.Windows.Forms.MouseEventArgs = e
        Dim MouseX = loc.X
        Dim MouseY = loc.Y
        Dim X As Double = -404
        Dim Y As Double = -404
        Dim ProportionOfImag As Double = Imag.Height / Imag.Width
        Dim ProportionOfPicureBox As Double = PictureBox1.Height / PictureBox1.Width
        'PI > PP --> Toca als eixos horizontals
        'PI < PP -->   "   "    "   verticalse
        Dim TouchesAtTheSides As Boolean = False
        If ProportionOfImag < ProportionOfPicureBox Then
            TouchesAtTheSides = True
        End If
        'MsgBox(TouchesAtTheSides.ToString)
        If TouchesAtTheSides Then
            Dim Proportion As Double = Imag.Width / PictureBox1.Width
            X = MouseX * Proportion
            Dim RespectivY As Double = MouseY - PictureBox1.Height / 2
            Y = RespectivY * Proportion + Imag.Height / 2
            'MsgBox(X.ToString & " : " & Y.ToString)
        Else
            Dim Proportion As Double = Imag.Height / PictureBox1.Height
            Y = MouseY * Proportion
            Dim RespectivX As Double = MouseX - PictureBox1.Width / 2
            X = RespectivX * Proportion + Imag.Width / 2
            'MsgBox(X.ToString & " : " & Y.ToString)
        End If
        If CInt(X) < 0 Or CInt(X) > Imag.Width Or CInt(Y) < 0 Or CInt(Y) > Imag.Height Then Exit Sub
        Dim ret As New Point(CInt(X), CInt(Y))
        If cbAddNodes.Checked And loc.Button = MouseButtons.Left Then
            RS.Add_Node(ret, False)
        End If
        If loc.Button = MouseButtons.Right Then
            For Each node In RS.Nodes
                Dim distance As Double = Math.Sqrt((ret.X - node.Position.X) ^ 2 + (ret.Y - node.Position.Y) ^ 2)
                If distance < 3 Then
                    Dim frm As New Form
                    frm = Interface_Nodes
                    N = node
                    frm.Show()
                    Interface_Nodes.Update_Info()
                End If
            Next
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Starting_SERVER()
    End Sub

End Class
