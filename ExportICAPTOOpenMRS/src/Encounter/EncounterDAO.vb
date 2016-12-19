Imports MySql.Data.MySqlClient
Public Class EncounterDAO
    Public Shared Function insertEncounter(ByVal encounter As Encounter, Optional ByVal visitType As Integer = 3, Optional ByVal providerId As Integer = 1) As Integer
        Dim cmmD As New MySqlCommand
        Dim encounterVisitId As Integer
        Dim returnEncounterId As Integer
        encounterVisitId = VisitaDAO.getVisitaIdByPatientAndStartDate(encounter.patientId, encounter.encounterDatetime)

        If encounterVisitId = 0 Then
            encounterVisitId = VisitaDAO.insertVisitaByParam(encounter.patientId, visitType, encounter.encounterDatetime, encounter.encounterDatetime, encounter.locationId)
        End If


        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            encounter.uuid = Guid.NewGuid.ToString

            .CommandText = "insert into encounter(encounter_type,patient_id,location_id,form_id,encounter_datetime,creator,date_created,uuid,visit_id) " & _
                                " values(" & encounter.encounterType & "," & encounter.patientId & "," & encounter.locationId & "," & encounter.formId & ",'" & dataMySQL(encounter.encounterDatetime) & "', " & _
                                " 22,now(),'" & encounter.uuid & "'," & encounterVisitId & ")"
            .ExecuteNonQuery()
            .CommandText = "Select max(encounter_id) from encounter"
            returnEncounterId = .ExecuteScalar

            .CommandText = "insert into encounter_provider(encounter_id,provider_id,encounter_role_id,creator,date_created,uuid) " & _
                            " values(" & returnEncounterId & "," & providerId & ",1,22,now(),uuid()"
            .ExecuteNonQuery()

            Return returnEncounterId

        End With
    End Function
    Public Shared Function insertEncounterByParam(ByVal encounterType As Integer, ByVal patientId As Integer, ByVal locationId As Integer, ByVal formId As Integer, ByVal encounterDate As Date, Optional ByVal visitType As Integer = 3, Optional ByVal providerId As Integer = 1) As Integer
        Dim cmmD As New MySqlCommand
        Dim encounterVisitId As Integer
        Dim returnEncounterId As Integer
        encounterVisitId = VisitaDAO.getVisitaIdByPatientAndStartDate(patientId, encounterDate)

        If encounterVisitId = 0 Then
            encounterVisitId = VisitaDAO.insertVisitaByParam(patientId, visitType, encounterDate, encounterDate, locationId)
        End If


        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            .CommandText = "insert into encounter(encounter_type,patient_id,location_id,form_id,encounter_datetime,creator,date_created,uuid,visit_id) " & _
                                " values(" & encounterType & "," & patientId & "," & locationId & "," & formId & ",'" & dataMySQL(encounterDate) & "', " & _
                                " 22,now(),'" & Guid.NewGuid.ToString & "'," & encounterVisitId & ")"
            .ExecuteNonQuery()
            .CommandText = "Select max(encounter_id) from encounter"
            returnEncounterId = .ExecuteScalar

            .CommandText = "insert into encounter_provider(encounter_id,provider_id,encounter_role_id,creator,date_created,uuid) " & _
                            " values(" & returnEncounterId & "," & providerId & ",1,22,now(),uuid()"
            .ExecuteNonQuery()

            Return returnEncounterId

        End With
    End Function
End Class
