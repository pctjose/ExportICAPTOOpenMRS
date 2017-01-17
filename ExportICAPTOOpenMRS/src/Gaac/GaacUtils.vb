Imports MySql.Data.MySqlClient
Public Class GaacUtils
    Public Shared Function insertGaacByParam(ByVal identifier As String, ByVal name As String, ByVal datainicio As Date, ByVal afinidade As Integer, ByVal pontoFocal As Integer, ByVal location As Integer, Optional ByVal dataDesintegracao As Date = Nothing) As Integer
        Dim cmmD As New MySqlCommand



        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            If dataDesintegracao = Nothing Then
                .CommandText = "insert into gaac(name,gaac_identifier,start_date,focal_patient_id,affinity_type,location_id,creator,date_created,uuid,changed_by,date_changed,end_date) " & _
                                " values('" & name & "','" & identifier & "','" & dataMySQL(datainicio) & "'," & pontoFocal & ", " & afinidade & "," & location & "," & _
                                " 22,now(),uuid(),null,null,null)"
            Else
                .CommandText = "insert into gaac(name,gaac_identifier,start_date,focal_patient_id,affinity_type,location_id,crumbled,date_crumbled,creator,date_created,uuid,changed_by,date_changed,end_date) " & _
                                " values('" & name & "','" & identifier & "','" & dataMySQL(datainicio) & "'," & pontoFocal & ", " & afinidade & "," & location & ",1,'" & dataMySQL(dataDesintegracao) & "'," & _
                                " 22,now(),uuid(),null,null,null)"

            End If

            .ExecuteNonQuery()


            .CommandText = "Select max(gaac_id) from gaac"

            Return .ExecuteScalar

        End With
    End Function
    Public Shared Function insertGaacMemberByParam(ByVal gaacId As Integer, ByVal memberId As Integer, ByVal startDate As Date) As Integer
        Dim cmmD As New MySqlCommand

        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3


            .CommandText = "insert into gaac_member(gaac_id,member_id,start_date,creator,date_created,uuid) " & _
                            " values(" & gaacId & "," & memberId & ",'" & dataMySQL(startDate) & "'," & _
                            " 22,now(),uuid())"



            .ExecuteNonQuery()


            .CommandText = "Select max(gaac_member_id) from gaac_member"

            Return .ExecuteScalar

        End With
    End Function
   
    Public Shared Sub updateSaidaGaacMemberByParam(ByVal gaacMemberID As Integer, ByVal motivo As Integer, ByVal dataSaida As Date)
        Dim cmmD As New MySqlCommand

        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3


            .CommandText = "update  gaac_member set leaving=1,end_date='" & dataMySQL(dataSaida) & "',reason_leaving_type=" & motivo & " where gaac_member_id= " & gaacMemberID

            .ExecuteNonQuery()

        End With
    End Sub
End Class
