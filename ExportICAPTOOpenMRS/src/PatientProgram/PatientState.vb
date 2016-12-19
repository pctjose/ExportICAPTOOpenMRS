Public Class PatientState
    Private _patientStateId As Integer
    Private _patientProgramId As Integer
    Private _state As Integer
    Private _startDate As Date
    Private _endDate As Date
    Private _creator As Integer
    Private _dateCreated As Date
    Private _uuid As String
    Public Property patientStateId() As Integer
        Get
            Return _patientStateId
        End Get
        Set(ByVal value As Integer)
            _patientStateId = value
        End Set
    End Property
    Public Property patientProgramId() As Integer
        Get
            Return _patientProgramId
        End Get
        Set(ByVal value As Integer)
            _patientProgramId = value
        End Set
    End Property
    Public Property state() As Integer
        Get
            Return _state
        End Get
        Set(ByVal value As Integer)
            _state = value
        End Set
    End Property
    Public Property startDate() As Date
        Get
            Return _startDate
        End Get
        Set(ByVal value As Date)
            _startDate = value
        End Set
    End Property
    Public Property endDate() As Date
        Get
            Return _endDate
        End Get
        Set(ByVal value As Date)
            _endDate = value
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
