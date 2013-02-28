Public Class Obs
    Private _obs_id As Integer
    Private _person_id As Integer
    Private _concept_id As Integer
    Private _encounter_id As Integer
    Private _obs_datetime As Date
    Private _location_id As Integer
    Private _obs_group_id As Integer
    'Private _value_boolean As Boolean
    Private _value_coded As Integer
    Private _value_drug As Integer
    Private _value_datetime As Date
    Private _value_numeric As Double
    Private _value_modifier As String
    Private _value_text As String

    Private _date_created As Date
    Private _voided As Boolean
    Private _data_Type As Int16

    Private _uuid As String

    Public Property UUID() As String
        Get
            Return _uuid
        End Get
        Set(ByVal value As String)
            _uuid = value
        End Set
    End Property

    Public Property obs_id() As Integer
        Get
            Return _obs_id
        End Get
        Set(ByVal value As Integer)
            _obs_id = value
        End Set
    End Property

    Public Property person_id() As Integer
        Get
            Return _person_id
        End Get
        Set(ByVal value As Integer)
            _person_id = value
        End Set
    End Property
    Public Property concept_id() As Integer
        Get
            Return _concept_id
        End Get
        Set(ByVal value As Integer)
            _concept_id = value
        End Set
    End Property
    Public Property encounter_id() As Integer
        Get
            Return _encounter_id
        End Get
        Set(ByVal value As Integer)
            _encounter_id = value
        End Set
    End Property
    Public Property obs_datetime() As Date
        Get
            Return _obs_datetime
        End Get
        Set(ByVal value As Date)
            _obs_datetime = value
        End Set
    End Property
    Public Property location_id() As Integer
        Get
            Return _location_id
        End Get
        Set(ByVal value As Integer)
            _location_id = value
        End Set
    End Property
    Public Property obs_group_id() As Integer
        Get
            Return _obs_group_id
        End Get
        Set(ByVal value As Integer)
            _obs_group_id = value
        End Set
    End Property
    'Public Property value_boolean() As Boolean
    '    Get
    '        Return _value_boolean
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _value_boolean = value
    '    End Set
    'End Property

    Public Property value_coded() As Integer
        Get
            Return _value_coded
        End Get
        Set(ByVal value As Integer)
            _value_coded = value
        End Set
    End Property
    Public Property value_drug() As Integer
        Get
            Return _value_drug
        End Get
        Set(ByVal value As Integer)
            _value_drug = value
        End Set
    End Property
    Public Property value_datetime() As Date
        Get
            Return _value_datetime
        End Get
        Set(ByVal value As Date)
            _value_datetime = value
        End Set
    End Property

    Public Property value_numeric() As Double
        Get
            Return _value_numeric
        End Get
        Set(ByVal value As Double)
            _value_numeric = value
        End Set
    End Property
    Public Property value_modifier() As String
        Get
            Return _value_modifier
        End Get
        Set(ByVal value As String)
            _value_modifier = value
        End Set
    End Property
    Public Property value_text() As String
        Get
            Return _value_text
        End Get
        Set(ByVal value As String)
            _value_text = value
        End Set
    End Property
    Public Property date_created() As Date
        Get
            Return _date_created
        End Get
        Set(ByVal value As Date)
            _date_created = value
        End Set
    End Property
    Public Property voided() As Boolean
        Get
            Return _voided
        End Get
        Set(ByVal value As Boolean)
            _voided = value
        End Set
    End Property
    Public Property data_Type() As Int16
        Get
            Return _data_Type
        End Get
        Set(ByVal value As Int16)
            _data_Type = value
        End Set
    End Property
End Class
