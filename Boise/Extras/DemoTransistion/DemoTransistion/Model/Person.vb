Imports System.ComponentModel

Public Class Person
    Implements INotifyPropertyChanged

#Region " Declarations "

    Private _strFirstName As String = String.Empty
    Private _strLastName As String = String.Empty
    Private _datBirthday As Nullable(Of Date)
    Private _strProfession As String = String.Empty
    Private _bolIsActive As Boolean = False
    Private _strThumbnail As String = String.Empty

#End Region

#Region " Events "

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region

#Region " Properties "

    Public Property FirstName() As String
        Get
            Return _strFirstName
        End Get
        Set(ByVal Value As String)
            _strFirstName = Value
            OnPropertyChanged("FirstName")
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return _strLastName
        End Get
        Set(ByVal Value As String)
            _strLastName = Value
            OnPropertyChanged("LastName")
        End Set
    End Property

    Public Property Birthday() As Nullable(Of Date)
        Get
            Return _datBirthday
        End Get
        Set(ByVal Value As Nullable(Of Date))
            _datBirthday = Value
            OnPropertyChanged("Birthday")
        End Set
    End Property

    Public Property Profession() As String
        Get
            Return _strProfession
        End Get
        Set(ByVal Value As String)
            _strProfession = value
            OnPropertyChanged("Profession")
        End Set
    End Property

    Public Property IsActive() As Boolean
        Get
            Return _bolIsActive
        End Get
        Set(ByVal Value As Boolean)
            _bolIsActive = Value
            OnPropertyChanged("IsActive")
        End Set
    End Property

    Public Property Thumbnail() As String
        Get
            Return _strThumbnail
        End Get
        Set(ByVal Value As String)
            _strThumbnail = Value
            OnPropertyChanged("Thumbnail")
        End Set
    End Property

#End Region

#Region " Constructors "

    Public Sub New()

    End Sub

#End Region

#Region " Methods "

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)

        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If

    End Sub

    'Note:  ToString is NOT dynamically updated in the UI like an INotifyPropertyChanged property
    Public Overrides Function ToString() As String
        Return String.Concat(_strFirstName, " ", _strLastName)
    End Function

#End Region

End Class
