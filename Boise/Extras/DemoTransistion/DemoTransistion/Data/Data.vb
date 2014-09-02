Imports System.Collections.ObjectModel
Public Class Data

    Private Shared _objPerson As Person
    Private Shared _objPeople As ObservableCollection(Of Person)
    Private Shared _objBothSexes As ObservableCollection(Of Person)

    Public Shared ReadOnly Property Person() As Person
        Get
            Return _objPerson
        End Get
    End Property

    Public Shared ReadOnly Property People() As ObservableCollection(Of Person)
        Get
            Return _objPeople
        End Get
    End Property

    Public Shared ReadOnly Property BothSexes() As ObservableCollection(Of Person)
        Get
            Return _objBothSexes
        End Get
    End Property

    Shared Sub New()

        _objPerson = New Person With {.Birthday = #12/25/1960#, .FirstName = "Jack", .IsActive = True, .LastName = "Bauer", .Profession = "Agent", .Thumbnail = "jackbauer.jpg"}
        '
        '
        _objPeople = New ObservableCollection(Of Person)

        _objPeople.Add(_objPerson)
        _objPeople.Add(New Person With {.Birthday = #4/6/1961#, .FirstName = "Tony", .IsActive = True, .LastName = "Almeida", .Profession = "Enemy Agent", .Thumbnail = "tonyalmeida.jpg"})
        _objPeople.Add(New Person With {.FirstName = "Jane", .IsActive = False, .LastName = "Doe", .Profession = "Citizen"})
        _objPeople.Add(New Person With {.FirstName = "James", .IsActive = False, .LastName = "Bond", .Profession = "SAS"})
        _objPeople.Add(New Person With {.FirstName = "Bill", .IsActive = False, .LastName = "Buchanan", .Profession = "Director CTU"})
        _objPeople.Add(New Person With {.FirstName = "David", .IsActive = False, .LastName = "Palmer", .Profession = "POTUS"})
        _objPeople.Add(New Person With {.FirstName = "David", .IsActive = False, .LastName = "Palmer", .Profession = "POTUS"})
        _objPeople.Add(New Person With {.FirstName = "David", .IsActive = False, .LastName = "Palmer", .Profession = "POTUS"})
        _objPeople.Add(New Person With {.FirstName = "David", .IsActive = False, .LastName = "Palmer", .Profession = "POTUS"})
        _objPeople.Add(New Person With {.FirstName = "David", .IsActive = False, .LastName = "Palmer", .Profession = "POTUS"})
        _objPeople.Add(New Person With {.FirstName = "David", .IsActive = False, .LastName = "Palmer", .Profession = "POTUS"})

        _objBothSexes = New ObservableCollection(Of Person)
        _objBothSexes.Add(New Man With {.Birthday = #4/6/1961#, .FirstName = "Tony", .IsActive = True, .LastName = "Almeida", .Profession = "Enemy Agent", .Thumbnail = "tonyalmeida.jpg"})
        _objBothSexes.Add(New Woman With {.FirstName = "Jane", .IsActive = False, .LastName = "Doe", .Profession = "Citizen"})
        _objBothSexes.Add(New Man With {.FirstName = "James", .IsActive = False, .LastName = "Bond", .Profession = "SAS"})
        _objBothSexes.Add(New Man With {.FirstName = "Bill", .IsActive = False, .LastName = "Buchanan", .Profession = "Director CTU"})

    End Sub

End Class
