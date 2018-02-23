Public Class Train
    Public Velocity As Double
    Public Railroad_On As Point
    Public Position_On_Railroad As Double
    Public Lenght_Of_Railroad As Double
    Public Velocity_To_Reach As Double

    Public Sub New()

    End Sub

    Public Sub New(Pos As Double, Railroad As Point)
        Me.Railroad_On = Railroad
        Me.Velocity = 0
        Me.Position_On_Railroad = Pos
        Me.Lenght_Of_Railroad = RS.Railroads_Lenght(Railroad.X)(Railroad.Y)
    End Sub

    Public Sub Update()
        'Update Position
        Dim a As Double = (Me.Velocity_To_Reach - Me.Velocity) / 5
        Me.Velocity = Me.Velocity + a
        Me.Position_On_Railroad += Me.Velocity
        'Check if train entered a station and if it stopped in it !!!!!
        If Me.Velocity_To_Reach = 0 Then 'If there is the intention to stop there
            If (Me.Velocity < 1) And (Me.Velocity > -1) Then 'If the velocity is tiny enough to stop
                If RS.Nodes(Me.Railroad_On.X).Station Then
                    If (Me.Position_On_Railroad < 2) And (Me.Position_On_Railroad <> 0) Then
                        OnStaion(0)
                        Exit Sub
                    End If
                End If
                If RS.Nodes(Me.Railroad_On.Y).Station Then
                    If Me.Position_On_Railroad > (Me.Lenght_Of_Railroad - 2) And (Me.Position_On_Railroad <> Me.Lenght_Of_Railroad) Then
                        OnStaion(1)
                        Exit Sub
                    End If
                End If
            End If
        End If
        'Me.Lenght_Of_Railroad = Me.Railroad_On.GetLenght() Check if the lenght changed
        If Me.Position_On_Railroad < 0 Then
            'Si arribo a 0 pero no es pot sortir perque es unidireccional parar a 0!!!!
            Dim N As Node = RS.Nodes(Me.Railroad_On.X) ' X = Beginning of Railroad; Y = End
            Me.Railroad_On = RS.Nodes(Me.Railroad_On.X).Get_New_Railroad(Me.Railroad_On, Me.Railroad_On.X)
            Me.Lenght_Of_Railroad = RS.Railroads_Lenght(Me.Railroad_On.X)(Me.Railroad_On.Y)
            Me.Position_On_Railroad += Me.Lenght_Of_Railroad
            Interface_Trains.Update_Info()
        End If
        If Me.Position_On_Railroad > Me.Lenght_Of_Railroad Then
            Dim N As Node = RS.Nodes(Me.Railroad_On.Y)
            Me.Position_On_Railroad -= RS.Railroads_Lenght(Me.Railroad_On.X)(Me.Railroad_On.Y)
            Me.Railroad_On = RS.Nodes(Me.Railroad_On.Y).Get_New_Railroad(Me.Railroad_On, Me.Railroad_On.Y)
            Me.Lenght_Of_Railroad = RS.Railroads_Lenght(Me.Railroad_On.X)(Me.Railroad_On.Y)
            Interface_Trains.Update_Info()
        End If
    End Sub

    Public Function Get_Pos() As Point
        Dim x As Double = RS.Nodes(Me.Railroad_On.Y).Position.X - RS.Nodes(Me.Railroad_On.X).Position.X
        Dim y As Double = RS.Nodes(Me.Railroad_On.Y).Position.Y - RS.Nodes(Me.Railroad_On.X).Position.Y
        Dim H As Double = Math.Sqrt(x ^ 2 + y ^ 2)
        Dim Prop As Double = Me.Position_On_Railroad / H
        Dim p As New Point
        p.X = RS.Nodes(Me.Railroad_On.X).Position.X + x * Prop - 2.5
        p.Y = RS.Nodes(Me.Railroad_On.X).Position.Y + y * Prop - 2.5
        Return p
    End Function

    Public Sub OnStaion(OrigenOrDestinacio As Integer)
        If OrigenOrDestinacio = 0 Then
            Me.Position_On_Railroad = 0
            Me.Velocity = 0
            MsgBox("Reached Station " & RS.Nodes(Me.Railroad_On.X).Position.ToString)
        Else
            Me.Position_On_Railroad = Me.Lenght_Of_Railroad
            Me.Velocity = 0
            MsgBox("Reached Station " & RS.Nodes(Me.Railroad_On.Y).Position.ToString)
        End If
    End Sub

End Class
