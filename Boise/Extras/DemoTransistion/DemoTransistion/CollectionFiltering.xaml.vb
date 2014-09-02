Partial Public Class CollectionFiltering
    
    Private Sub FilterMen_Filter(ByVal sender As System.Object, ByVal e As System.Windows.Data.FilterEventArgs)
        If TypeOf e.Item Is Man Then
            e.Accepted = True
        Else
            e.Accepted = False
        End If
    End Sub

    Private Sub FilterWomen_Filter(ByVal sender As System.Object, ByVal e As System.Windows.Data.FilterEventArgs)
        If TypeOf e.Item Is Woman Then
            e.Accepted = True
        Else
            e.Accepted = False
        End If
    End Sub
End Class
