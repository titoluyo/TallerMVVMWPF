' <summary>
' ========================================
' .NET Framework 3.0 Custom Control
' ========================================
'
' Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
'
' Step 1a) Using this custom control in a XAML file that exists in the current project.
' Add this XmlNamespace attribute to the root element of the markup file where it is 
' to be used:
'
'     xmlns:MyNamespace="clr-namespace:StopLightCustomControl"
'
'
' Step 1b) Using this custom control in a XAML file that exists in a different project.
' Add this XmlNamespace attribute to the root element of the markup file where it is 
' to be used:
'
'     xmlns:MyNamespace="clr-namespace:StopLightCustomControl;assembly=StopLightCustomControl"
'
' You will also need to add a project reference from the project where the XAML file lives
' to this project and Rebuild to avoid compilation errors:
'
'     Right click on the target project in the Solution Explorer and
'     "Add Reference"->"Projects"->[Browse to and select this project]
'
'
' Step 2)
' Go ahead and use your control in the XAML file. Note that Intellisense in the
' XML editor does not currently work on custom controls and its child elements.
'
'     <MyNamespace:StopLight/>
'
' </summary>

Public Class StopLight
    Inherits System.Windows.Controls.Control

    Public Shared ReadOnly LightProperty As DependencyProperty = DependencyProperty.Register("Light", GetType(Lights), GetType(StopLight), New PropertyMetadata(Lights.Green))

    Public Property Light() As Lights
        Get
            Return CType(GetValue(LightProperty), Lights)
        End Get
        Set(ByVal value As Lights)
            SetValue(LightProperty, value)
        End Set
    End Property

    Shared Sub New()
        'This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
        'This style is defined in themes\generic.xaml
        DefaultStyleKeyProperty.OverrideMetadata(GetType(StopLight), New FrameworkPropertyMetadata(GetType(StopLight)))
    End Sub

End Class
