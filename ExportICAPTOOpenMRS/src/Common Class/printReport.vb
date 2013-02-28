Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
'Imports CrystalDecisions.Windows

'CLASE PARA CARGAR UN REPORTE BASADO EN SQL SERVER

Public Class printReport

    Private Shared loginfo As CrystalDecisions.Shared.ConnectionInfo
    Public Shared exportar_mail As Boolean
    Public rutaRpt As String
    Public Shared stillOpen As Boolean
    Public Shared custTitle As String

    Public Shared Sub conectar(ByVal servidor As String, ByVal basedatos As String, ByVal usuario As String, ByVal password As String)
        loginfo = New CrystalDecisions.Shared.ConnectionInfo
        loginfo.ServerName = servidor
        loginfo.DatabaseName = basedatos
        loginfo.UserID = usuario
        loginfo.Password = password

    End Sub
    Public Shared Sub conectar()
        loginfo = New CrystalDecisions.Shared.ConnectionInfo
        loginfo.ServerName = "localhost"
        loginfo.DatabaseName = "dm_#misau"
        loginfo.UserID = "root"
        loginfo.Password = "dm2007misau"
    End Sub
    Public Sub setLogon(ByVal rpt As ReportDocument)
        'Dim crTableLogonInfos As New TableLogOnInfos()
        Dim crTableLogonInfo As New TableLogOnInfo()
        Dim crConnectionInfo As New ConnectionInfo()
        Dim crTables As Tables
        Dim crTable As Table
        Dim subReportCount As Int16
        With crConnectionInfo
            .ServerName = "DMDATASORCE"
            .DatabaseName = "dm_#misau"
            .UserID = "root"
            .Password = "dm2007misau"
        End With
        crTables = rpt.Database.Tables
        For Each crTable In crTables
            crTableLogonInfo = crTable.LogOnInfo
            crTableLogonInfo.ConnectionInfo = crConnectionInfo
            crTable.ApplyLogOnInfo(crTableLogonInfo)
        Next
        For subReportCount = 0 To rpt.Subreports.Count - 1
            crTables = rpt.Subreports(subReportCount).Database.Tables
            For Each crTable In crTables
                crTableLogonInfo = crTable.LogOnInfo
                crTableLogonInfo.ConnectionInfo = crConnectionInfo
                crTable.ApplyLogOnInfo(crTableLogonInfo)
            Next
        Next
    End Sub
    Public Sub LogOnReportAndSubReports(ByVal repDoc As ReportDocument)
        Dim subReportDoc As New ReportDocument()
        Dim crSections As Sections
        Dim crSection As Section
        Dim crReportObjects As ReportObjects
        Dim crReportObject As ReportObject
        Dim CrsubReportObject As SubreportObject
        Dim crDatabase As Database
        Dim crTables As Tables
        Dim crTable As Table
        Dim crLogonInfo As TableLogOnInfo
        Dim crConnInfo As New ConnectionInfo()


        With crConnInfo
            .ServerName = "DMDATASORCE"
            .DatabaseName = "dm_#misau"
            .UserID = "root"
            .Password = "dm2007misau"

        End With

        crDatabase = repDoc.Database
        crTables = crDatabase.Tables
        For Each crTable In crTables
            crLogonInfo = crTable.LogOnInfo
            crLogonInfo.ConnectionInfo = crConnInfo
            crTable.ApplyLogOnInfo(crLogonInfo)
        Next
        'Falta alguma coisa aqui.
        crSections = repDoc.ReportDefinition.Sections
        For Each crSection In crSections
            crReportObjects = crSection.ReportObjects
            For Each crReportObject In crReportObjects
                If crReportObject.Kind = ReportObjectKind.SubreportObject Then
                    CrsubReportObject = CType(crReportObject, SubreportObject)
                    subReportDoc = CrsubReportObject.OpenSubreport(CrsubReportObject.SubreportName)
                    'SetDateRangeForOrders(subReportDoc)
                    crDatabase = subReportDoc.Database
                    crTables = crDatabase.Tables
                    For Each crTable In crTables
                        crLogonInfo = crTable.LogOnInfo
                        crLogonInfo.ConnectionInfo = crConnInfo
                        crTable.ApplyLogOnInfo(crLogonInfo)
                    Next
                End If
            Next
        Next
        'Dim pfs As ParameterFields = repDoc.ParameterFields
        'For Each param As ParameterField In repDoc.ParameterFields
        '    If param.Name = "Provincia" Then
        '        repDoc.SetParameterValue("Provincia", getProvinciaTituloReport(utilizador))
        '        'param.CurrentValues.Add(getProvinciaTituloReport(utilizador))
        '        Exit For
        '    End If
        'Next


    End Sub
    Public Sub LogOnReportAndSubReportsOpenMRS(ByVal repDoc As ReportDocument)
        Dim subReportDoc As New ReportDocument()
        Dim crSections As Sections
        Dim crSection As Section
        Dim crReportObjects As ReportObjects
        Dim crReportObject As ReportObject
        Dim CrsubReportObject As SubreportObject
        Dim crDatabase As Database
        Dim crTables As Tables
        Dim crTable As Table
        Dim crLogonInfo As TableLogOnInfo
        Dim crConnInfo As New ConnectionInfo()


        With crConnInfo
            .ServerName = My.Settings.ServerNameOpenMRS
            .DatabaseName = My.Settings.DataBaseNameOpenMRS ' "openmrs"
            .UserID = My.Settings.UserIDOpenMRS '"root"
            .Password = My.Settings.PasswordOpenMRS '"dm2007misau"

        End With

        crDatabase = repDoc.Database
        crTables = crDatabase.Tables
        For Each crTable In crTables
            crLogonInfo = crTable.LogOnInfo
            crLogonInfo.ConnectionInfo = crConnInfo
            crTable.ApplyLogOnInfo(crLogonInfo)
        Next
        'Falta alguma coisa aqui.
        crSections = repDoc.ReportDefinition.Sections
        For Each crSection In crSections
            crReportObjects = crSection.ReportObjects
            For Each crReportObject In crReportObjects
                If crReportObject.Kind = ReportObjectKind.SubreportObject Then
                    CrsubReportObject = CType(crReportObject, SubreportObject)
                    subReportDoc = CrsubReportObject.OpenSubreport(CrsubReportObject.SubreportName)
                    'SetDateRangeForOrders(subReportDoc)
                    crDatabase = subReportDoc.Database
                    crTables = crDatabase.Tables
                    For Each crTable In crTables
                        crLogonInfo = crTable.LogOnInfo
                        crLogonInfo.ConnectionInfo = crConnInfo
                        crTable.ApplyLogOnInfo(crLogonInfo)
                    Next
                End If
            Next
        Next
        'Dim pfs As ParameterFields = repDoc.ParameterFields
        'For Each param As ParameterField In repDoc.ParameterFields
        '    If param.Name = "Provincia" Then
        '        repDoc.SetParameterValue("Provincia", getProvinciaTituloReport(utilizador))
        '        'param.CurrentValues.Add(getProvinciaTituloReport(utilizador))
        '        Exit For
        '    End If
        'Next


    End Sub
    Public Sub LogOnReportAndSubReportsOpenMRSReporting(ByVal repDoc As ReportDocument)
        Dim subReportDoc As New ReportDocument()
        Dim crSections As Sections
        Dim crSection As Section
        Dim crReportObjects As ReportObjects
        Dim crReportObject As ReportObject
        Dim CrsubReportObject As SubreportObject
        Dim crDatabase As Database
        Dim crTables As Tables
        Dim crTable As Table
        Dim crLogonInfo As TableLogOnInfo
        Dim crConnInfo As New ConnectionInfo()


        'With crConnInfo
        '    .ServerName = My.Settings.ServerNameReporting
        '    .DatabaseName = My.Settings.DataBaseNameReporting ' "openmrs"
        '    .UserID = My.Settings.UserIDOpenMRS '"root"
        '    .Password = My.Settings.PasswordOpenMRS '"dm2007misau"

        'End With

        crDatabase = repDoc.Database
        crTables = crDatabase.Tables
        For Each crTable In crTables
            crLogonInfo = crTable.LogOnInfo
            crLogonInfo.ConnectionInfo = crConnInfo
            crTable.ApplyLogOnInfo(crLogonInfo)
        Next
        'Falta alguma coisa aqui.
        crSections = repDoc.ReportDefinition.Sections
        For Each crSection In crSections
            crReportObjects = crSection.ReportObjects
            For Each crReportObject In crReportObjects
                If crReportObject.Kind = ReportObjectKind.SubreportObject Then
                    CrsubReportObject = CType(crReportObject, SubreportObject)
                    subReportDoc = CrsubReportObject.OpenSubreport(CrsubReportObject.SubreportName)
                    'SetDateRangeForOrders(subReportDoc)
                    crDatabase = subReportDoc.Database
                    crTables = crDatabase.Tables
                    For Each crTable In crTables
                        crLogonInfo = crTable.LogOnInfo
                        crLogonInfo.ConnectionInfo = crConnInfo
                        crTable.ApplyLogOnInfo(crLogonInfo)
                    Next
                End If
            Next
        Next
        'Dim pfs As ParameterFields = repDoc.ParameterFields
        'For Each param As ParameterField In repDoc.ParameterFields
        '    If param.Name = "Provincia" Then
        '        repDoc.SetParameterValue("Provincia", getProvinciaTituloReport(utilizador))
        '        'param.CurrentValues.Add(getProvinciaTituloReport(utilizador))
        '        Exit For
        '    End If
        'Next


    End Sub


    'Public Sub teste()
    '    Dim parametros As New ParameterFields
    '    Dim numberParamField As ParameterField = New ParameterField()
    '    numberParamField.



    'End Sub

    Public Shared Function genpar(ByVal ParamArray matriz() As String) As ParameterFields
        Dim c As Long, p1, p2 As String, l As Integer
        Dim parametros As New ParameterFields
        For c = 0 To matriz.Length - 1
            l = InStr(matriz(c), ";")
            If l > 0 Then
                p1 = Mid(matriz(c), 1, l - 1)
                p2 = Mid(matriz(c), l + 1, Len(matriz(c)) - l)
                Dim parametro As New ParameterField
                Dim dVal As New ParameterDiscreteValue
                parametro.ParameterFieldName = p1
                dVal.Value = p2
                parametro.CurrentValues.Add(dVal)
                parametros.Add(parametro)
            End If
        Next
        Return (parametros)
    End Function
    Public Sub SetParamSubReportValue(ByVal rpt As ReportDocument, ByVal nomeSubRepor As String, ByVal ParamArray matriz() As String)
        Dim c As Integer, p1, p2 As String, l As Integer
        'Dim parametros As New ParameterFields

        For c = 0 To matriz.Length - 1
            l = InStr(matriz(c), ";")
            If l > 0 Then
                p1 = Mid(matriz(c), 1, l - 1)
                p2 = Mid(matriz(c), l + 1, Len(matriz(c)) - l)
                Dim parametro As New ParameterField
                Dim dVal As New ParameterDiscreteValue
                parametro.ParameterFieldName = p1
                dVal.Value = p2
                MsgBox(p1 & "  " & p2)
                MsgBox(rpt.Subreports.Count)
                'rpt.Subreports(0).SetParameterValue("UnidadeID", "1234")

                rpt.SetParameterValue("UnidadeID", "actividades")

                'parametro.CurrentValues.Add(dVal)
                'rpt.Subreports(nomeSubRepor).ParameterFields.Add(parametro)
                'rpt.Subreports(nomeSubRepor).ParameterFields(p1).DefaultValues.Add(p2)
                'rpt.Subreports(nomeSubRepor).DataDefinition.ParameterFields(c).CurrentValues.Add(p2)
                'parametros.Add(parametro)
            End If
        Next

    End Sub

    'Private Shared Function logonrpt(ByRef reporte As ReportDocument)
    '    Dim crtableLogoninfos As New TableLogOnInfos
    '    Dim crtableLogoninfo As New TableLogOnInfo
    '    Dim crConnectionInfo As New ConnectionInfo
    '    Dim CrTables As Tables
    '    Dim CrTable As Table
    '    conectar()
    '    crConnectionInfo = loginfo

    '    CrTables = reporte.Database.Tables
    '    For Each CrTable In CrTables
    '        'MsgBox(CrTable.Fields(1).Name)
    '        crtableLogoninfo = CrTable.LogOnInfo
    '        crtableLogoninfo.ConnectionInfo = crConnectionInfo
    '        CrTable.ApplyLogOnInfo(crtableLogoninfo)
    '    Next
    'End Function

    'Public Overloads Sub printrpt(ByVal nombrereporte As String, ByVal rpt As ReportDocument, ByVal forma As frmPrintReports, ByVal ParamArray par() As String)
    '    'Dim forma As New frmPrintReports
    '    'Dim rpt As New ReportDocument
    '    With forma.rptViewer
    '        If par.Length > 0 Then
    '            .ParameterFieldInfo = genpar(par)
    '        End If
    '        If rutaRpt.Trim.Length = 0 Then
    '            rpt.Load(nombrereporte, OpenReportMethod.OpenReportByDefault)
    '        ElseIf Mid(rutaRpt.Trim, rutaRpt.Trim.Length, 1) = "\" Then
    '            rpt.Load(rutaRpt & nombrereporte, OpenReportMethod.OpenReportByDefault)
    '        Else
    '            rpt.Load(rutaRpt & "\" & nombrereporte, OpenReportMethod.OpenReportByDefault)
    '        End If
    '        logonrpt(rpt)
    '        'Dim doc As New fichaUS

    '        'MsgBox(doc.DataDefinition.ParameterFields.Count)
    '        'MsgBox(rpt.DataDefinition.ParameterFields.Count)

    '        'Configurar aquí cualquier opción de exportación 
    '        Dim opt As New ExportOptions
    '        opt = rpt.ExportOptions
    '        'Configurar aquí cualquier opción de impresión 
    '        Dim prn As PrintOptions
    '        prn = rpt.PrintOptions
    '        .ReportSource = rpt
    '        Dim crParameterFields As ParameterFields = .ParameterFieldInfo
    '        MsgBox(.ParameterFieldInfo.Count)
    '        Dim crParameterField As ParameterField = crParameterFields.Item("", "")
    '        'Dim crParameterField As ParameterField = crParameterFields.Item("UnidadeID", "agua")
    '        Dim crParameterValues As ParameterValues = crParameterField.CurrentValues
    '        Dim crParameterDiscrete As ParameterDiscreteValue = New ParameterDiscreteValue()
    '        crParameterDiscrete.Value = "MC-DU1-D1-1-01"
    '        crParameterValues.Add(crParameterDiscrete)
    '        .ParameterFieldInfo = crParameterFields

    '        'Visualizar el reporte en una ventana nueva

    '        'forma.Text = custTitle
    '        'forma.Show()
    '    End With
    'End Sub


    Public Sub SetDateRangeForOrders(ByVal subname As ReportDocument)
        Dim myParameterFields As ParameterFields
        Dim myParametrDiscreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()
        myParameterFields = subname.ParameterFields
        myParametrDiscreteValue.Value = "MC-DU1-D1-1-01"
        Dim myParameterField As ParameterField = myParameterFields("UnidadeID", subname.Name)
        myParameterField.CurrentValues.Clear()
        myParameterField.CurrentValues.Add(myParametrDiscreteValue)
        myParameterFields.Add(myParameterField)
        subname.ParameterFields.Clear()
        subname.ParameterFields.Add(myParameterFields)
    End Sub

End Class
