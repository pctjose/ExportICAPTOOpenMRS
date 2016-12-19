Imports MySql.Data.MySqlClient
Public Class PatientProgamDAO
    Public Shared Function insertPatientProgram(ByVal pg As PatientProgram) As Integer
        Dim cmmD As New MySqlCommand
        


        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            pg.uuid = Guid.NewGuid.ToString

            .CommandText = "insert into patient_program(patient_id,program_id,date_enrolled,creator,date_created,uuid,location_id) " & _
                                " values(" & pg.patientId & "," & pg.programId & ",'" & dataMySQL(pg.dateEnrolled) & "', " & _
                                " 22,now(),'" & pg.uuid & "'," & pg.locationId & ")"
            .ExecuteNonQuery()
            .CommandText = "Select max(patient_program_id) from patient_program"
            

            Return .ExecuteScalar

        End With
    End Function
    Public Shared Function insertPatientProgramByParam(ByVal patientId As Integer, ByVal programId As Integer, ByVal dateEnrolled As Date, ByVal locationId As Integer) As Integer
        Dim cmmD As New MySqlCommand



        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            .CommandText = "insert into patient_program(patient_id,program_id,date_enrolled,creator,date_created,uuid,location_id) " & _
                                " values(" & patientId & "," & programId & ",'" & dataMySQL(dateEnrolled) & "', " & _
                                " 22,now(),uuid()," & locationId & ")"
            .ExecuteNonQuery()
            .CommandText = "Select max(patient_program_id) from patient_program"

            Return .ExecuteScalar

        End With
    End Function
    Public Shared Function getPatientProgramID(ByVal patientId As Integer, ByVal programId As Integer) As Integer
        Dim cmmD As New MySqlCommand
        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3
            
            .CommandText = "Select max(patient_program_id)  from patient_program where patient_id=" & patientId & " and program_id=" & programId

            Return .ExecuteScalar

        End With
    End Function
    Public Shared Sub endPatientProgram(ByVal patientProgramId As Integer, ByVal endDate As Date)
        Dim cmmD As New MySqlCommand

        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            .CommandText = "update patient_program set date_completed= '" & dataMySQL(endDate) & "' where patient_program_id= " & patientProgramId

            .ExecuteNonQuery()

        End With
    End Sub
    Public Shared Function insertPatientState(ByVal ps As PatientState) As Integer
        Dim cmmD As New MySqlCommand



        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            ps.uuid = Guid.NewGuid.ToString

            .CommandText = "insert into patient_state(patient_program_id,state,start_date,creator,date_created,uuid) " & _
                                " values(" & ps.patientProgramId & "," & ps.state & ",'" & dataMySQL(ps.startDate) & "', " & _
                                " 22,now(),'" & ps.uuid & "')"
            .ExecuteNonQuery()
            .CommandText = "Select max(patient_state_id) from patient_state"


            Return .ExecuteScalar

        End With
    End Function
    Public Shared Function insertPatientStateByParam(ByVal patientProgramId As Integer, ByVal stateId As Integer, ByVal startDate As Date) As Integer
        Dim cmmD As New MySqlCommand



        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            .CommandText = "insert into patient_state(patient_program_id,state,start_date,creator,date_created,uuid) " & _
                                " values(" & patientProgramId & "," & stateId & ",'" & dataMySQL(startDate) & "', " & _
                                " 22,now(),uuid())"
            .ExecuteNonQuery()
            .CommandText = "Select max(patient_state_id) from patient_state"


            Return .ExecuteScalar

        End With
    End Function

    Public Shared Function getPatientStateByProgramID(ByVal patientProgram As Integer, ByVal stateID As Integer) As Integer
        Dim cmmD As New MySqlCommand
        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            .CommandText = "Select max(patient_state_id)  from patient_state where patient_program_id=" & patientProgram & " and state=" & stateID

            Return .ExecuteScalar

        End With
    End Function

    Public Shared Sub endPatientState(ByVal patientState As Integer, ByVal endDate As Date)
        Dim cmmD As New MySqlCommand

        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            .CommandText = "update patient_state set end_date= '" & dataMySQL(endDate) & "' where patient_state_id= " & patientState

            .ExecuteNonQuery()

        End With
    End Sub
End Class
