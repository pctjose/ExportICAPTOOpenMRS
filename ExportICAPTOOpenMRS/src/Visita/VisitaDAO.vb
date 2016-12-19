Imports MySql.Data.MySqlClient
Public Class VisitaDAO
    Public Shared Function insertVisita(ByVal visit As Visita) As Integer
        Dim cmmD As New MySqlCommand
        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            visit.UUID = Guid.NewGuid.ToString

            .CommandText = "insert into visit(patient_id,visit_type_id,date_started,date_stopped,location_id,creator,date_created,uuid) " & _
                                " values(" & visit.patientId & "," & visit.visitTypeId & ",'" & dataMySQL(visit.dateStarted) & "','" & dataMySQL(visit.dateStoped) & " 23:59:59" & "', " & visit.locationId & "," & _
                                " 22,now(),'" & visit.UUID & "')"
            .ExecuteNonQuery()
            .CommandText = "Select max(visit_id) from visit"
            Return .ExecuteScalar

        End With
    End Function

    Public Shared Function insertVisitaByParam(ByVal patientId As Integer, ByVal visitType As Integer, ByVal startDate As Date, ByVal stopDate As Date, ByVal location As Integer) As Integer
        Dim cmmD As New MySqlCommand

        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            .CommandText = "insert into visit(patient_id,visit_type_id,date_started,date_stopped,location_id,creator,date_created,uuid) " & _
                                " values(" & patientId & "," & visitType & ",'" & dataMySQL(startDate) & "','" & dataMySQL(stopDate) & " 23:59:59" & "', " & location & "," & _
                                " 22,now(),'" & Guid.NewGuid.ToString & "')"
            .ExecuteNonQuery()
            .CommandText = "Select max(visit_id) from visit"
            Return .ExecuteScalar

        End With
    End Function

    Public Shared Function getVisitaIdByPatientAndStartDate(ByVal patientId As Integer, ByVal startDate As Date) As Integer
        Dim cmmD As New MySqlCommand
        Dim visitId As String
        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            .CommandText = "select visit_id from visit where patient_id=" & patientId & " and date_started='" & dataMySQL(startDate) & "' and voided=0"

            visitId = .ExecuteScalar
            If Not String.IsNullOrEmpty(visitId) Then
                Return visitId
            Else
                Return 0
            End If

        End With
    End Function
End Class
