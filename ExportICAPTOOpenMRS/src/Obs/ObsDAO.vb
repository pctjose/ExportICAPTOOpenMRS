Imports MySql.Data.MySqlClient
Public Class ObsDAO
    Public Shared Function insertSet(ByVal o As Obs) As Integer
        Dim cmmD As New MySqlCommand
        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            o.UUID = Guid.NewGuid.ToString

            .CommandText = "insert into obs(person_id,concept_id,encounter_id,obs_datetime,location_id,creator,date_created,uuid) " & _
                                " values(" & o.person_id & "," & o.concept_id & "," & o.encounter_id & ",'" & dataMySQL(o.obs_datetime) & "', " & o.location_id & "," & _
                                " 22,now(),'" & o.UUID & "')"
            .ExecuteNonQuery()
            .CommandText = "Select max(obs_id) from obs"
            Return .ExecuteScalar

        End With
    End Function
    Public Shared Sub insertObs(ByVal o As Obs, ByVal isInSet As Boolean)
        Dim cmmD As New MySqlCommand
        With cmmD
            .CommandType = CommandType.Text
            .Connection = ConexaoOpenMRS3

            o.UUID = Guid.NewGuid.ToString

            Select Case o.data_Type
                Case ObsDataType.TCoded
                    If isInSet Then
                        .CommandText = "insert into obs(person_id,concept_id,value_coded,encounter_id,obs_datetime,location_id,creator,date_created,obs_group_id,uuid) " & _
                                " values(" & o.person_id & "," & o.concept_id & "," & o.value_coded & ", " & o.encounter_id & ",'" & dataMySQL(o.obs_datetime) & "'," & o.location_id & "," & _
                                " 22,now()," & o.obs_group_id & ",'" & o.UUID & "')"
                    Else
                        .CommandText = "insert into obs(person_id,concept_id,value_coded,encounter_id,obs_datetime,location_id,creator,date_created,uuid) " & _
                                " values(" & o.person_id & "," & o.concept_id & "," & o.value_coded & ", " & o.encounter_id & ",'" & dataMySQL(o.obs_datetime) & "'," & o.location_id & "," & _
                                " 22,now(),'" & o.UUID & "')"

                    End If
                Case ObsDataType.TNumeric
                    If isInSet Then
                        .CommandText = "insert into obs(person_id,concept_id,value_numeric,encounter_id,obs_datetime,location_id,creator,date_created,obs_group_id,uuid) " & _
                                " values(" & o.person_id & "," & o.concept_id & "," & o.value_numeric & ", " & o.encounter_id & ",'" & dataMySQL(o.obs_datetime) & "'," & o.location_id & "," & _
                                " 22,now()," & o.obs_group_id & ",'" & o.UUID & "')"
                    Else
                        .CommandText = "insert into obs(person_id,concept_id,value_numeric,encounter_id,obs_datetime,location_id,creator,date_created,uuid) " & _
                                " values(" & o.person_id & "," & o.concept_id & "," & o.value_numeric & ", " & o.encounter_id & ",'" & dataMySQL(o.obs_datetime) & "'," & o.location_id & "," & _
                                " 22,now(),'" & o.UUID & "')"

                    End If
                Case ObsDataType.TText
                    Dim valorTexto As String = o.value_text
                    If Not (valorTexto = Nothing) Then
                        valorTexto = valorTexto.Replace("'", "")
                        valorTexto = valorTexto.Replace("\", "")
                    End If
                    If isInSet Then
                        .CommandText = "insert into obs(person_id,concept_id,value_text,encounter_id,obs_datetime,location_id,creator,date_created,obs_group_id,uuid) " & _
                                " values(" & o.person_id & "," & o.concept_id & ",'" & valorTexto & "', " & o.encounter_id & ",'" & dataMySQL(o.obs_datetime) & "'," & o.location_id & "," & _
                                " 22,now()," & o.obs_group_id & ",'" & o.UUID & "')"
                    Else
                        .CommandText = "insert into obs(person_id,concept_id,value_text,encounter_id,obs_datetime,location_id,creator,date_created,uuid) " & _
                                " values(" & o.person_id & "," & o.concept_id & ",'" & o.value_text & "', " & o.encounter_id & ",'" & dataMySQL(o.obs_datetime) & "'," & o.location_id & "," & _
                                " 22,now(),'" & o.UUID & "')"

                    End If
                Case ObsDataType.TDatetime
                    If isInSet Then
                        .CommandText = "insert into obs(person_id,concept_id,value_datetime,encounter_id,obs_datetime,location_id,creator,date_created,obs_group_id,uuid) " & _
                                " values(" & o.person_id & "," & o.concept_id & ",'" & dataMySQL(o.value_datetime) & "', " & o.encounter_id & ",'" & dataMySQL(o.obs_datetime) & "'," & o.location_id & "," & _
                                " 22,now()," & o.obs_group_id & ",'" & o.UUID & "')"
                    Else
                        .CommandText = "insert into obs(person_id,concept_id,value_datetime,encounter_id,obs_datetime,location_id,creator,date_created,uuid) " & _
                                " values(" & o.person_id & "," & o.concept_id & ",'" & dataMySQL(o.value_datetime) & "', " & o.encounter_id & ",'" & dataMySQL(o.obs_datetime) & "'," & o.location_id & "," & _
                                " 22,now(),'" & o.UUID & "')"

                    End If
                    'Case ObsDataType.TBoolean
                    '    If isInSet Then
                    '        .CommandText = "insert into obs(person_id,concept_id,value_numeric,encounter_id,obs_datetime,location_id,creator,date_created,obs_group_id) " & _
                    '                " values(" & o.person_id & "," & o.concept_id & "," & o.value_boolean & ", " & o.encounter_id & ",'" & dataMySQL(o.obs_datetime) & "'," & o.location_id & "," & _
                    '                " 22,now()," & o.obs_group_id & ")"
                    '    Else
                    '        .CommandText = "insert into obs(person_id,concept_id,value_numeric,encounter_id,obs_datetime,location_id,creator,date_created) " & _
                    '                " values(" & o.person_id & "," & o.concept_id & "," & o.value_boolean & ", " & o.encounter_id & ",'" & dataMySQL(o.obs_datetime) & "'," & o.location_id & "," & _
                    '                " 22,now())"

                    '    End If

            End Select
            '.Connection.Open()
            .ExecuteNonQuery()
        End With
    End Sub
End Class
