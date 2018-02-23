Module mTrain_Sim
    Public RS As Railsystem
    Public T As Train
    Public RR As Point
    Public N As Node
    Public data As String = Nothing
    Public TCP_On As Boolean = True
    Public Sub Change_Train_Velocity()
        RS.Trains(0).Velocity_To_Reach = 20
    End Sub
    Public Sub Serialize_RS()
        Form1.Timer1.Enabled = False
        Dim mySerializer As New Xml.Serialization.XmlSerializer(RS.GetType)
        Dim myWriter As New IO.StreamWriter("Saved\InUse.xml")
        mySerializer.Serialize(myWriter, RS)
        myWriter.Close()
        Form1.Timer1.Enabled = True
        MsgBox("SAVED")
    End Sub

    Public Sub Deserialize_RS()
        Dim RS2 As New Railsystem
        Dim mySerializer As New Xml.Serialization.XmlSerializer(RS2.GetType)
        Dim myReader As New IO.StreamReader("Saved\InUse.xml")
        Dim s As String = myReader.ReadToEnd
        Dim stringreader As New IO.StringReader(s)
        'Dim reader As Xml.XmlReader = Xml.XmlReader.Create(myReader)
        RS2 = CType(mySerializer.Deserialize(stringreader), Railsystem)
        myReader.Close()
        stringreader.Close()
        RS = RS2
        'S.Set_MainRoads()
        MsgBox("Loaded")
    End Sub

    Public Function Return_Train_Lenght() As Integer
        Return RS.Trains.Length
    End Function

    Public Function Return_Nodes_Lenght() As Integer
        Return RS.Nodes.Length
    End Function

    Public Function SeeRailroads() As List(Of Point)
        Dim ret As New List(Of Point)
        For x As Integer = 0 To RS.Railroads_Priority.Length - 1
            For y As Integer = 0 To RS.Railroads_Priority(x).Length - 1
                If RS.Railroads_Priority(x)(y) <> 0 Then
                    ret.Add(New Point(x, y))
                End If
            Next
        Next
        Return ret
    End Function

    Public Sub PaintAllNodes(bool As Boolean)
        For Each node In RS.Nodes
            node.Painted = bool
        Next
    End Sub

    'Enter interfaces
    'Add New ones
    'Add the possibility to save a model of priorities
End Module
