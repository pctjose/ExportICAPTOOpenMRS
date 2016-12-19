Public Class EncounterProvider
    Private _encounterProviderId As Integer
    Private _encounterId As Integer
    Private _providerId As Integer
    Private _encounterRoleId As Integer
    Private _creator As Integer
    Private _dateCreated As Date
    Private _uuid As String
    Public Property encounterProviderId() As Integer
        Get
            Return _encounterProviderId
        End Get
        Set(ByVal value As Integer)
            _encounterProviderId = value
        End Set
    End Property
    Public Property encounterId() As Integer
        Get
            Return _encounterId
        End Get
        Set(ByVal value As Integer)
            _encounterId = value
        End Set
    End Property
    Public Property providerId() As Integer
        Get
            Return _providerId
        End Get
        Set(ByVal value As Integer)
            _providerId = value
        End Set
    End Property
    Public Property encounterRoleId() As Integer
        Get
            Return _encounterRoleId
        End Get
        Set(ByVal value As Integer)
            _encounterRoleId = value
        End Set
    End Property
    Public Property creator() As Integer
        Get
            Return _creator
        End Get
        Set(ByVal value As Integer)
            _creator = value
        End Set
    End Property
    Public Property dateCreated() As Date
        Get
            Return _dateCreated
        End Get
        Set(ByVal value As Date)
            _dateCreated = value
        End Set
    End Property
    Public Property uuid() As String
        Get
            Return _uuid
        End Get
        Set(ByVal value As String)
            _uuid = value
        End Set
    End Property

End Class
