Imports ADODB
Imports MySql.Data.MySqlClient
Public Class TuberculoseTratamento
    Public Shared Sub ImportTuberculoseTratamento(ByVal fonte As Connection, ByVal locationid As Int16)
        Dim patientID As Integer

        Dim encounter_id As Integer

        Try

            Dim cmmFonte As New Command 'Acess
            Dim cmmDestino As New MySqlCommand 'MySQL
            Dim rs As New Recordset
            Dim dataInicial As Date
            Dim dataCorrente As Date
            Dim notSet As New ArrayList
            Dim obs As Obs

            If AllPatients Then
                rs.Open("Select distinct nid,data,datafim from t_tratamentotb", fonte, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockReadOnly)
            Else
                rs.Open("Select distinct nid,data,datafim from t_tratamentotb where nid in (" & whereQuery & ")", fonte, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockReadOnly)
            End If


            If Not (rs.EOF And rs.BOF) Then
                cmmDestino.Connection = ConexaoOpenMRS1 'cone.conectar
                cmmDestino.CommandType = CommandType.Text
                rs.MoveFirst()

                While Not rs.EOF

                    patientID = GetPatientOpenMRSIDByNID(rs.Fields.Item("nid").Value) 'get Current OpenMRS Patient ID
                    If patientID > 0 Then
                        'End If
                        If Not IsDBNull(rs.Fields.Item("data").Value) Then


                            dataInicial = rs.Fields.Item("data").Value 'First Initial date

                            If Not IsDBNull(rs.Fields.Item("datafim").Value) Then
                                dataCorrente = rs.Fields.Item("datafim").Value
                            Else
                                dataCorrente = Nothing
                            End If

                            cmmDestino.CommandText = "Insert into encounter(encounter_type,patient_id,provider_id,location_id," & _
                                                    "form_id,encounter_datetime,creator,date_created,voided,uuid) values(26," & patientID & ",27," & locationid & "," & _
                                                    "120,'" & dataMySQL(dataInicial) & "',22,now(),0,uuid())"
                            cmmDestino.ExecuteNonQuery()

                            cmmDestino.CommandText = "Select max(encounter_id) from encounter"

                            encounter_id = cmmDestino.ExecuteScalar

                            obs = New Obs
                            obs.concept_id = 1113
                            obs.data_Type = ObsDataType.TDatetime
                            obs.value_datetime = dataInicial
                            obs.obs_datetime = dataInicial
                            notSet.Add(obs)

                            obs = New Obs
                            obs.concept_id = 1268
                            obs.data_Type = ObsDataType.TCoded
                            obs.value_coded = 1256
                            obs.obs_datetime = dataInicial
                            notSet.Add(obs)

                            If Not dataCorrente = Nothing Then
                                obs = New Obs
                                obs.concept_id = 1269
                                obs.data_Type = ObsDataType.TCoded
                                obs.obs_datetime = dataCorrente
                                obs.value_coded = 1267
                                notSet.Add(obs)

                                obs = New Obs

                                obs.concept_id = 6120
                                obs.data_Type = ObsDataType.TDatetime
                                obs.obs_datetime = dataCorrente
                                obs.value_datetime = dataCorrente

                                notSet.Add(obs)
                            End If

                            If notSet.Count > 0 Then
                                For Each o As Obs In notSet
                                    o.location_id = locationid
                                    o.person_id = patientID
                                    o.date_created = Now
                                    o.voided = 0
                                    o.encounter_id = encounter_id
                                    ObsDAO.insertObs(o, False)
                                Next
                            End If
                        End If
                    End If
                    notSet.Clear()
                    rs.MoveNext()
                End While
            rs.Close()
            End If

        Catch ex As Exception
            MsgBox("Error Importing Treatment of Tuberculosis: " & ex.Message)
        End Try
    End Sub
End Class