Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Module Control_TCP

    Public Sub Starting_SERVER()
        ' Data buffer for incoming data.
        Dim bytes() As Byte = New [Byte](1024) {}

        Dim ipAddress As IPAddress = IPAddress.Parse("127.0.0.1")
        Dim localEndPoint As New IPEndPoint(ipAddress, 6000)

        ' Create a TCP/IP socket.
        Dim listener As New Socket(SocketType.Stream, ProtocolType.Tcp)

        ' Bind the socket to the local endpoint and listen for incoming connections.

        listener.Bind(localEndPoint)
        listener.Listen(5)

        ' Start listening for connections.
        While TCP_On
            'Label1.Text = "Waiting for a connection..."
            ' Program is suspended while waiting for an incoming connection.
            Dim handler As Socket = listener.Accept()
            data = Nothing

            ' An incoming connection needs to be processed.
            While True
                bytes = New Byte(1024) {}
                Dim bytesRec As Integer = handler.Receive(bytes)
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec)
                If data.IndexOf("<EOF>") > -1 Then
                    Exit While
                End If
            End While
            Dim answer As String = Process_Data(data)
            ' Show the data on the console.
            'MsgBox("Text answered : " & answer)
            ' Echo the data back to the client.
            Dim msg As Byte() = Encoding.ASCII.GetBytes(answer)
            handler.Send(msg)
            handler.Shutdown(SocketShutdown.Both)
            handler.Close()
        End While
    End Sub

    Private Function Process_Data(s As String) As String
        Dim ret As String = "Couldn't process data"
        Dim WholeInstruction As String = s.Split("<EOF>")(0)
        'MsgBox(s)
        Dim TypeOfInstruction As String = WholeInstruction.Split(":")(0)
        Select Case TypeOfInstruction
            Case "recv"
                'MsgBox("RECIVE DATA")
                Dim TypeOfObject As String = WholeInstruction.Split(":")(1).Split("-")(0)
                Dim WhatToMakeWithTheObject As String = WholeInstruction.Split(":")(1).Split("-")(1).Split("/")(0)
                Dim SpecificationsOfTheAction As String = ""
                If WholeInstruction.Split(":")(1).Split("-")(1).Split("/").Length > 1 Then
                    SpecificationsOfTheAction = WholeInstruction.Split(":")(1).Split("-")(1).Split("/")(1)
                End If
                Select Case TypeOfObject
                    Case "n"
                        'MsgBox("n " + WhatToMakeWithTheObject)
                        Select Case WhatToMakeWithTheObject
                            Case "q"
                                'MsgBox("Count")
                                ret = RS.Nodes.Length.ToString
                            Case "info"
                                'MsgBox("SEND INFO")
                                'MsgBox(SpecificationsOfTheAction)
                                ret = "POSITION: "
                                ret += RS.Nodes(CInt(SpecificationsOfTheAction)).Position.ToString
                                ret += vbLf & "Painted?: "
                                ret += RS.Nodes(CInt(SpecificationsOfTheAction)).Painted.ToString
                                ret += vbLf & "Station?: "
                                ret += RS.Nodes(CInt(SpecificationsOfTheAction)).Station.ToString
                                If RS.Nodes(CInt(SpecificationsOfTheAction)).SavedPriority.Length <> 0 Then
                                    ret += vbLf & "Saved Priority?: "
                                    ret += String.Join(",", RS.Nodes(CInt(SpecificationsOfTheAction.Split(">")(0))).SavedPriority(0))
                                    ret += vbLf & "Saved Priority?: "
                                    ret += String.Join(",", RS.Nodes(CInt(SpecificationsOfTheAction.Split(">")(0))).SavedPriority(1))
                                End If
                            Case "list"
                                'MsgBox("LIST")
                                ret = ""
                                For i As Integer = 0 To RS.Nodes.Length - 1
                                    Select Case SpecificationsOfTheAction
                                        Case "position"
                                            ret += "Node " & i.ToString & ": " & RS.Nodes(i).Position.ToString & vbLf
                                        Case "painted"
                                            ret += "Node " & i.ToString & ": " & RS.Nodes(i).Painted.ToString & vbLf
                                        Case "station"
                                            ret += "Node " & i.ToString & ": " & RS.Nodes(i).Station.ToString & vbLf
                                    End Select
                                Next
                            Case Else
                                ret = "Action not understood"
                                MsgBox("ERROR WITH ACTION")
                        End Select
                    Case "rr"
                        'MsgBox("rr " + WhatToMakeWithTheObject)
                        'MsgBox("n " + WhatToMakeWithTheObject)
                        Select Case WhatToMakeWithTheObject
                            Case "q"
                                'MsgBox("Count")
                                Dim count As Integer = 0
                                For Each line In RS.Railroads_Priority
                                    For Each element In line
                                        If element <> 0 Then
                                            count += 1
                                        End If
                                    Next
                                Next
                                ret = count.ToString
                            Case "info"
                                'MsgBox("SEND INFO")
                                'MsgBox(SpecificationsOfTheAction)
                                Dim x As Integer = CInt(SpecificationsOfTheAction.Split(",")(0))
                                Dim y As Integer = CInt(SpecificationsOfTheAction.Split(",")(1))
                                ret = "Priority: "
                                ret += RS.Railroads_Priority(x)(y).ToString
                                ret += vbLf & "Length: "
                                ret += RS.Railroads_Lenght(x)(y).ToString
                            Case "list"
                                'MsgBox("LIST")
                                ret = ""
                                Select Case SpecificationsOfTheAction
                                    Case "priority"
                                        For x As Integer = 0 To RS.Railroads_Priority.Length - 1
                                            For y As Integer = 0 To RS.Railroads_Priority(x).Length - 1
                                                ret += "Railroad " & x & "," & y & ": " & RS.Railroads_Priority(x)(y).ToString & vbLf
                                            Next
                                        Next
                                    Case "length"
                                        For x As Integer = 0 To RS.Railroads_Lenght.Length - 1
                                            For y As Integer = 0 To RS.Railroads_Lenght(x).Length - 1
                                                ret += "Railroad " & x & "," & y & ": " & RS.Railroads_Lenght(x)(y).ToString & vbLf
                                            Next
                                        Next
                                    Case Else
                                        ret = "Action wrongly especificated"
                                End Select
                            Case Else
                                ret = "Action not understood"
                                MsgBox("ERROR WITH ACTION")
                        End Select
                    Case "t"
                        'MsgBox("t " + WhatToMakeWithTheObject)
                        'MsgBox("n " + WhatToMakeWithTheObject)
                        Select Case WhatToMakeWithTheObject
                            Case "q"
                                'MsgBox("Count")
                                ret = RS.Trains.Length.ToString
                            Case "info"
                                'MsgBox("SEND INFO")
                                'MsgBox(SpecificationsOfTheAction)
                                ret = "POSITION ON RAILROAD: "
                                ret += RS.Trains(CInt(SpecificationsOfTheAction)).Position_On_Railroad.ToString
                                ret += vbLf & "RAILROAD ON: "
                                ret += RS.Trains(CInt(SpecificationsOfTheAction)).Railroad_On.ToString
                                ret += vbLf & "VELOCITY: "
                                ret += RS.Trains(CInt(SpecificationsOfTheAction)).Velocity.ToString
                                ret += vbLf & "VELOCITY TO REACH: "
                                ret += RS.Trains(CInt(SpecificationsOfTheAction)).Velocity_To_Reach.ToString
                            Case "list"
                                'MsgBox("LIST")
                                ret = ""
                                For i As Integer = 0 To RS.Trains.Length - 1
                                    Select Case SpecificationsOfTheAction
                                        Case "position"
                                            ret += "Train " & i.ToString & ": " & RS.Trains(i).Position_On_Railroad.ToString & vbLf
                                        Case "railroad"
                                            ret += "Train " & i.ToString & ": " & RS.Trains(i).Railroad_On.ToString & vbLf
                                        Case "velocity"
                                            ret += "Train " & i.ToString & ": " & RS.Trains(i).Velocity.ToString & vbLf
                                        Case "velocitytoreach"
                                            ret += "Train " & i.ToString & ": " & RS.Trains(i).Velocity_To_Reach.ToString & vbLf
                                        Case Else
                                            ret = "Specification of the action wrong"
                                    End Select
                                Next
                            Case Else
                                ret = "Action not understood"
                                MsgBox("ERROR WITH ACTION")
                        End Select
                    Case "st"
                        'MsgBox("st " + WhatToMakeWithTheObject)
                        Select Case WhatToMakeWithTheObject
                            Case "q"
                                'MsgBox("Count")
                                Dim count As Integer = 0
                                For Each no In RS.Nodes
                                    If no.Station Then
                                        count += 1
                                    End If
                                Next
                                ret = count.ToString
                            Case Else
                                ret = "Action not understood"
                                MsgBox("ERROR WITH ACTION")
                        End Select
                    Case Else
                        ret = "Error with the type"
                End Select
        End Select
        Return ret
    End Function

End Module
