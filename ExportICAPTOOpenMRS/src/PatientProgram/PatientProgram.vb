Public Class PatientProgram
    Private _patientProgramId As Integer
    Private _patientId As Integer
    Private _programId As Integer
    Private _dateEnrolled As Date
    Private _dateCompleted As Date
    Private _creator As Integer
    Private _dateCreated As Date
    Private _uuid As String
    Private _locationId As Integer
    Public Property patientProgramId() As Integer
        Get
            Return _patientProgramId
        End Get
        Set(ByVal value As Integer)
            _patientProgramId = value
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
    Public Property programId() As Integer
        Get
            Return _programId
        End Get
        Set(ByVal value As Integer)
            _programId = value
        End Set
    End Property
    Public Property dateEnrolled() As Date
        Get
            Return _dateEnrolled
        End Get
        Set(ByVal value As Date)
            _dateEnrolled = value
        End Set
    End Property
    Public Property dateCompleted() As Date
        Get
            Return _dateCompleted
        End Get
        Set(ByVal value As Date)
            _dateCompleted = value
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
    Public Property locationId() As Integer
        Get
            Return _locationId
        End Get
        Set(ByVal value As Integer)
            _locationId = value
        End Set
    End Property
End Class
