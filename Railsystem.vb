Public Class Railsystem
    Public Nodes() As Node = {}
    Public Railroads_Lenght()() As Double = {} 'https://www.dotnetperls.com/2d-vbnet
    Public Railroads_Priority()() As Integer = {}
    Public Trains() As Train = {}

    Public Sub New()
        'For i As Integer = 0 To 3
        '    'Ajust Lenght of jagged array
        '    If i >= Me.Railroads.Length Then
        '        ReDim Preserve Me.Railroads(i)
        '    End If
        '    'Add new small array
        '    Dim temp() As Node = {}
        '    For j As Integer = 0 To 3
        '        ReDim Preserve temp(j)
        '        temp(j) = New Node
        '        'temp(j).Int = i * j
        '    Next
        '    Me.Railroads(i) = temp
        'Next

        'If Railroads(0) Is Nothing Then

        'Else
        '    For Each line In Railroads
        '        ReDim Preserve line(line.Length)
        '        line(line.Length - 1) = New Node
        '        line(line.Length - 1).Position = Pos
        '    Next
        '    ReDim Preserve Me.Railroads(Me.Railroads.Length)
        'End If
    End Sub

    Public Sub Add_Train(Pos As Double, Railroad As Point)
        ReDim Preserve Me.Trains(Me.Trains.Length)
        Me.Trains(Me.Trains.Length - 1) = New Train(Pos, Railroad)
    End Sub

    Public Sub Add_Node(Pos As Point, St As Boolean)
        ReDim Preserve Nodes(Nodes.Length)
        Nodes(Nodes.Length - 1) = New Node(Pos, St)
        Update_Railroads_Size()
    End Sub

    Public Sub Update_Railroads_Size()
        If Railroads_Lenght.Length <> 0 Then
            'There is a matrix
            For i As Integer = 0 To Railroads_Lenght.Length - 1
                ReDim Preserve Railroads_Lenght(i)(Railroads_Lenght(i).Length)
                Railroads_Lenght(i)(Railroads_Lenght(i).Length - 1) = 0
            Next
            ReDim Preserve Railroads_Lenght(Railroads_Lenght.Length)
            Railroads_Lenght(Railroads_Lenght.Length - 1) = {}
            For i As Integer = 0 To Railroads_Lenght.Length - 1
                ReDim Preserve Railroads_Lenght(Railroads_Lenght.Length - 1)(i)
                Railroads_Lenght(Railroads_Lenght.Length - 1)(i) = 0
            Next
        Else
            'There is no matrix
            ReDim Preserve Railroads_Lenght(Railroads_Lenght.Length)
            Railroads_Lenght(Railroads_Lenght.Length - 1) = {0}
        End If

        If Railroads_Priority.Length <> 0 Then
            'There is a matrix
            For i As Integer = 0 To Railroads_Priority.Length - 1
                ReDim Preserve Railroads_Priority(i)(Railroads_Priority(i).Length)
                Railroads_Priority(i)(Railroads_Priority(i).Length - 1) = 0
            Next
            ReDim Preserve Railroads_Priority(Railroads_Priority.Length)
            Railroads_Priority(Railroads_Priority.Length - 1) = {}
            For i As Integer = 0 To Railroads_Priority.Length - 1
                ReDim Preserve Railroads_Priority(Railroads_Priority.Length - 1)(i)
                Railroads_Priority(Railroads_Priority.Length - 1)(i) = 0
            Next
        Else
            'There is no matrix
            ReDim Preserve Railroads_Priority(Railroads_Priority.Length)
            Railroads_Priority(Railroads_Priority.Length - 1) = {0}
        End If
    End Sub

    Public Sub Update_Railroads_Lenght()
        For i As Integer = 0 To Me.Railroads_Lenght.Length - 1
            For j As Integer = 0 To Me.Railroads_Lenght(i).Length - 1
                If Me.Railroads_Priority(i)(j) <> 0 Then
                    Dim x As Double = Me.Nodes(i).Position.X - Me.Nodes(j).Position.X
                    Dim y As Double = Me.Nodes(i).Position.Y - Me.Nodes(j).Position.Y
                    Me.Railroads_Lenght(i)(j) = Math.Sqrt(x ^ 2 + y ^ 2)
                End If
            Next
        Next
    End Sub

    Public Sub Add_Railroad(ID1 As Integer, ID2 As Integer, Both As Boolean)
        If Both Then
            Me.Railroads_Priority(ID1)(ID2) = 10
            Me.Railroads_Priority(ID2)(ID1) = 10
        Else
            Me.Railroads_Priority(ID1)(ID2) = 10
        End If
        Update_Railroads_Lenght()
    End Sub

    Public Sub Show()
        'Set Dimensions of Bitmap and paint it black
        Dim max_x As Integer = 0
        Dim max_y As Integer = 0
        For i As Integer = 0 To Me.Nodes.Length - 1
            If Me.Nodes(i).Position.X > max_x Then
                max_x = Me.Nodes(i).Position.X
            End If
            If Me.Nodes(i).Position.Y > max_y Then
                max_y = Me.Nodes(i).Position.Y
            End If
        Next
        Dim BM As New Drawing.Bitmap(max_x + 10, max_y + 10)
        Dim Gr As Graphics = Graphics.FromImage(BM)
        For x As Integer = 0 To BM.Width - 1
            For y As Integer = 0 To BM.Height - 1
                BM.SetPixel(x, y, Color.Black)
            Next
        Next
        'Paint Railroads
        For i As Integer = 0 To Me.Railroads_Priority.Length - 1
            For j As Integer = 0 To Me.Railroads_Priority(i).Length - 1
                If Me.Railroads_Priority(i)(j) <> 0 Then
                    Dim p1 As New Pen(Color.White)
                    'If Me.Railroads(i).Painted Then p1.Color = Color.Violet!!!!
                    Gr.DrawLine(p1, Me.Nodes(i).Position.X, Me.Nodes(i).Position.Y, Me.Nodes(j).Position.X, Me.Nodes(j).Position.Y)
                End If
            Next
        Next
        'Paint Trains
        If Me.Trains IsNot Nothing Then
            For i As Integer = 0 To Me.Trains.Length - 1
                Dim p1 As New Pen(Color.Yellow)
                Gr.DrawEllipse(p1, Me.Trains(i).Get_Pos.X, Me.Trains(i).Get_Pos.Y, 5, 5)
            Next
        End If
        'Paint Nodes!!!!
        Dim lenghtOfLines As Integer = 3 '!!!!!!!

        If Me.Nodes IsNot Nothing Then
            For i As Integer = 0 To Me.Nodes.Length - 1
                If Me.Nodes(i).Painted Then
                    Dim p1 As New Pen(Color.Red)
                    Gr.DrawEllipse(p1, Me.Nodes(i).Position.X, Me.Nodes(i).Position.Y, 2, 2)
                    Dim s As String = i + 1
                    Dim f As Font = Form1.F
                    Gr.DrawString(s, f, Brushes.Blue, Me.Nodes(i).Position.X - 5, Me.Nodes(i).Position.Y - 5)
                End If
                If Me.Nodes(i).Station Then
                    Dim p1 As New Pen(Color.Blue)
                    Gr.DrawRectangle(p1, Me.Nodes(i).Position.X - 5, Me.Nodes(i).Position.Y - 3, 10, 6)
                End If
                'If Me.Nodes(i).Conections IsNot Nothing Then
                '    If Me.Nodes(i).Conections.Count = 3 Then 'paint switch
                '        'Dim pMain As New Point
                '        'pMain = Me.Nodes(i).GetRailroad(Me.Nodes(i).Main_Road).GetOtherNode(Me.Nodes(i)).Position
                '        Dim T1 As New Train(0, Me.Nodes(i).GetRailroad(Me.Nodes(i).Main_Road))
                '        If T1.Railroad_On.Nodes_Connected_To(T1.Origen).Position = Me.Nodes(i).Position Then
                '            T1.Position_On_Railroad = lenghtOfLines
                '        Else
                '            T1.Position_On_Railroad = T1.Lenght_Of_Railroad - lenghtOfLines
                '        End If
                '        Dim pMainroad As New Point
                '        pMainroad = T1.GetPos()
                '        pMainroad.X += 3.5
                '        pMainroad.Y += 3.5
                '        Dim p1 As New Pen(Color.Red)
                '        Gr.DrawLine(p1, pMainroad.X, pMainroad.Y, Me.Nodes(i).Position.X, Me.Nodes(i).Position.Y)
                '        Gr.DrawLine(p1, pMainroad.X - 1, pMainroad.Y - 1, Me.Nodes(i).Position.X - 1, Me.Nodes(i).Position.Y - 1)
                '        Gr.DrawLine(p1, pMainroad.X + 1, pMainroad.Y + 1, Me.Nodes(i).Position.X + 1, Me.Nodes(i).Position.Y + 1)
                '        If Me.Nodes(i).RailroadPointingTo Then
                '            T1.Railroad_On = Me.Nodes(i).GetRailroad(Me.Nodes(i).Conections(1))
                '        Else
                '            T1.Railroad_On = Me.Nodes(i).GetRailroad(Me.Nodes(i).Conections(2))
                '        End If
                '        If T1.Railroad_On.Nodes_Connected_To(T1.Origen).Position = Me.Nodes(i).Position Then
                '            T1.Position_On_Railroad = lenghtOfLines
                '        Else
                '            T1.Position_On_Railroad = T1.Lenght_Of_Railroad - lenghtOfLines
                '        End If
                '        pMainroad = T1.GetPos()
                '        pMainroad.X += 3.5
                '        pMainroad.Y += 3.5
                '        Gr.DrawLine(p1, pMainroad.X, pMainroad.Y, Me.Nodes(i).Position.X, Me.Nodes(i).Position.Y)
                '        Gr.DrawLine(p1, pMainroad.X - 1, pMainroad.Y - 1, Me.Nodes(i).Position.X - 1, Me.Nodes(i).Position.Y - 1)
                '        Gr.DrawLine(p1, pMainroad.X + 1, pMainroad.Y + 1, Me.Nodes(i).Position.X + 1, Me.Nodes(i).Position.Y + 1)
                '    End If
                'End If
            Next
        End If
        Form1.Imag = BM
    End Sub

    Public Sub Update() 'Updates all the trains
        If Me.Trains IsNot Nothing Then
            For i As Integer = 0 To Me.Trains.Length - 1
                Me.Trains(i).Update()
            Next
        End If
        Me.Show()
    End Sub

    Public Sub Update_Train_Railroad_Length()
        Update_Railroads_Lenght()

        For Each train In Me.Trains
            train.Lenght_Of_Railroad = RS.Railroads_Lenght(train.Railroad_On.X)(train.Railroad_On.Y)
        Next
    End Sub

End Class
