Public Class Encounter
    Private _encounterId As Integer
    Private _encounterType As Integer
    Private _patientId As Integer
    Private _locationId As Integer
    Private _formId As Integer
    Private _encounterDatetime As Date
    Private _creator As Integer
    Private _dateCreated As Date
    Private _uuid As String
    Private _visitId As Integer
    Public Property encounterId() As Integer
        Get
            Return _encounterId
        End Get
        Set(ByVal value As Integer)
            _encounterId = value
        End Set
    End Property
    Public Property encounterType() As Integer
        Get
            Return _encounterType
        End Get
        Set(ByVal value As Integer)
            _encounterType = value
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
    Public Property locationId() As Integer
        Get
            Return _locationId
        End Get
        Set(ByVal value As Integer)
            _locationId = value
        End Set
    End Property
    Public Property formId() As Integer
        Get
            Return _formId
        End Get
        Set(ByVal value As Integer)
            _formId = value
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
    Public Property visitId() As Integer
        Get
            Return _visitId
        End Get
        Set(ByVal value As Integer)
            _visitId = value
        End Set
    End Property
    Public Property encounterDatetime() As Date
        Get
            Return _encounterDatetime
        End Get
        Set(ByVal value As Date)
            _encounterDatetime = value
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
    Public Property uuid() As Date
        Get
            Return _uuid
        End Get
        Set(ByVal value As Date)
            _uuid = value
        End Set
    End Property
End Class
