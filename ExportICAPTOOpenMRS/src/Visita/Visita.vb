Public Class Visita
    Private _visitId As Integer
    Private _patientId As Integer
    Private _visitTypeId As Integer
    Private _dateStarted As Date
    Private _dateStoped As Date
    Private _locationId As Integer
    Private _creator As Integer
    Private _dateCreated As Date
    Private _uuid As String

    Public Property UUID() As String
        Get
            Return _uuid
        End Get
        Set(ByVal value As String)
            _uuid = value
        End Set
    End Property
    Public Property visitId() As Integer
        Get
            Return _visitId
        End Get
        Set(ByVal value As Integer)
            _visitId = value
        End Set
    End Property
    Public Property patientId() As Integer
        Get
            Return _patientId
        End Get
        Set(ByVal value As Integer)
            _patientId = value
        End Set
    End Property
    Public Property visitTypeId() As Integer
        Get
            Return _visitTypeId
        End Get
        Set(ByVal value As Integer)
            _visitTypeId = value
        End Set
    End Property
    Public Property locationId() As Integer
        Get
            Return _locationId
        End Get
        Set(ByVal value As Integer)
            _locationId = value
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
    Public Property dateStarted() As Date
        Get
            Return _dateStarted
        End Get
        Set(ByVal value As Date)
            _dateStarted = value
        End Set
    End Property
    Public Property dateStoped() As Date
        Get
            Return _dateStoped
        End Get
        Set(ByVal value As Date)
            _dateStoped = value
        End Set
    End Property

End Class
