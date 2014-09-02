
Partial Public Class Window1
    Inherits System.Windows.Window

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub chkUseHardwareLightFixtures_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles chkUseHardwareLightFixtures.Click
        If Me.chkUseHardwareLightFixtures.IsChecked.Value = True Then
            Dim obj As Style = TryCast(Me.FindResource("hardwareStopLightStyle"), Style)
            If obj IsNot Nothing Then
                Me.NorthLight.Style = obj
                Me.SouthLight.Style = obj
                Me.EastLight.Style = obj
                Me.WestLight.Style = obj
            End If
        Else
            Me.NorthLight.Style = Nothing
            Me.SouthLight.Style = Nothing
            Me.EastLight.Style = Nothing
            Me.WestLight.Style = Nothing
        End If
    End Sub
End Class
