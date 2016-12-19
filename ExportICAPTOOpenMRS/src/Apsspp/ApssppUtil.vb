Imports ADODB
Imports MySql.Data.MySqlClient
Public Class ApssppUtil
    Public Shared Sub importApssSeguimento(ByVal fonte As Connection, ByVal locationid As Int16)
        Dim patientID As Integer
        Dim encounter_id As Integer
        Dim nid As String
        Dim dataVisitaApss As Date
        
        Dim obs As Obs

        

        Dim dataArray As New ArrayList


        'Try
        Dim cmmFonte As New Command 'Acess
        Dim rs As New Recordset
        Dim cmmDestino As New MySqlCommand 'MySQL

        cmmFonte.CommandType = CommandTypeEnum.adCmdText
        cmmFonte.ActiveConnection = fonte
        If AllPatients Then
            cmmFonte.CommandText = "SELECT  nid,dataseguimento,pp1,pp2,pp3,pp4,pp5,pp6,pp7,apssTipovisita," & _
                                            "apssAdesao,apssActividade,apssproximavisita,apssdatavisita,recebeSms,aceitaSerContatado " & _
                                " FROM apsspp where nid is not null "
        Else
            cmmFonte.CommandText = "SELECT  nid,dataseguimento,pp1,pp2,pp3,pp4,pp5,pp6,pp7,apssTipovisita," & _
                                            "apssAdesao,apssActividade,apssproximavisita,apssdatavisita,recebeSms,aceitaSerContatado " & _
                                " FROM apsspp where nid is not null and nid in (" & whereQuery & ")"
        End If

        cmmDestino.CommandType = CommandType.Text
        cmmDestino.Connection = ConexaoOpenMRS3

        rs = cmmFonte.Execute

        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF

                nid = rs.Fields.Item("nid").Value

                If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "apssdatavisita")) Then
                    dataVisitaApss = rs.Fields.Item("apssdatavisita").Value
                Else
                    dataVisitaApss = rs.Fields.Item("dataseguimento").Value
                End If


                

                patientID = GetPatientOpenMRSIDByNID(nid) 'Get the openmrs patient_id using the NID

                If patientID > 0 Then

                    encounter_id = EncounterDAO.insertEncounterByParam(35, patientID, locationid, 132, dataVisitaApss, 10, 27)

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "apssActividade")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6314
                        Dim apssActividade As String = rs.Fields.Item("apssActividade").Value
                        If apssActividade = "Acolhimento" Then
                            obs.value_coded = 6312
                        ElseIf apssActividade = "Aconselhamento Pré-Tarv" Then
                            obs.value_coded = 6313
                        Else
                            obs.value_coded = 5488
                        End If
                        dataArray.Add(obs)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "apssTipovisita")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6315
                        Dim apssTipovisita As String = rs.Fields.Item("apssTipovisita").Value
                        If apssTipovisita = "Normal" Then
                            obs.value_coded = 1115
                        ElseIf apssTipovisita = "Faltoso" Or apssTipovisita = "Baixa Adesão" Then
                            obs.value_coded = 6311
                        Else
                            obs.value_coded = 1707
                        End If
                        dataArray.Add(obs)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "apssAdesao")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6223
                        Dim apssAdesao As String = rs.Fields.Item("apssAdesao").Value
                        If apssAdesao = "Risco" Then
                            obs.value_coded = 1749
                        ElseIf apssAdesao = "Baixa" Then
                            obs.value_coded = 1385
                        Else
                            obs.value_coded = 1383
                        End If
                        dataArray.Add(obs)
                    End If

                    If rs.Fields.Item("pp1").Value Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6317
                        obs.value_coded = 1065
                        
                        dataArray.Add(obs)
                    End If

                    If rs.Fields.Item("pp2").Value Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6318
                        obs.value_coded = 1065

                        dataArray.Add(obs)
                    End If

                    If rs.Fields.Item("pp3").Value Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6319
                        obs.value_coded = 1065

                        dataArray.Add(obs)
                    End If

                    If rs.Fields.Item("pp4").Value Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6320
                        obs.value_coded = 1065

                        dataArray.Add(obs)
                    End If

                    If rs.Fields.Item("pp5").Value Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 5271
                        obs.value_coded = 1065

                        dataArray.Add(obs)

                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6316
                        obs.value_coded = 1065

                        dataArray.Add(obs)
                    End If

                    If rs.Fields.Item("pp6").Value Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6321
                        obs.value_coded = 1065

                        dataArray.Add(obs)
                    End If

                    If rs.Fields.Item("pp7").Value Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6322
                        obs.value_coded = 1065

                        dataArray.Add(obs)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "apssproximavisita")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TDatetime
                        obs.concept_id = 6310
                        obs.value_datetime = rs.Fields.Item("apssproximavisita").Value
                        
                        dataArray.Add(obs)
                    End If
                   

                    For Each o As Obs In dataArray

                        ObsDAO.insertObs(o, False)

                    Next

                    dataArray.Clear()

                    If rs.Fields.Item("recebeSms").Value Or rs.Fields.Item("aceitaSerContatado").Value Then
                        encounter_id = EncounterDAO.insertEncounterByParam(34, patientID, locationid, 131, dataVisitaApss, 10, 27)

                        If rs.Fields.Item("aceitaSerContatado").Value Then
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = encounter_id
                            obs.obs_datetime = dataVisitaApss
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6306
                            obs.value_coded = 1065

                            ObsDAO.insertObs(obs, False)
                        End If

                        If rs.Fields.Item("recebeSms").Value Then
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = encounter_id
                            obs.obs_datetime = dataVisitaApss
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6309
                            obs.value_coded = 6307

                            ObsDAO.insertObs(obs, False)
                        End If

                    End If

                  

                    
                End If

                rs.MoveNext()
            End While
        End If
        rs.Close()
        'Catch ex As Exception
        '    MsgBox("Error Importing Seguimento: " & ex.Message)
        'End Try

    End Sub

    Public Shared Sub importApssInicial(ByVal fonte As Connection, ByVal locationid As Int16)
        Dim patientID As Integer
        Dim encounter_id As Integer
        Dim nid As String
        Dim dataVisitaApss As Date

        Dim obs As Obs

        'Try
        Dim cmmFonte As New Command 'Acess
        Dim rs As New Recordset
        Dim cmmDestino As New MySqlCommand 'MySQL

        cmmFonte.CommandType = CommandTypeEnum.adCmdText
        cmmFonte.ActiveConnection = fonte
        If AllPatients Then
            cmmFonte.CommandText = "SELECT  nid,dataabertura," & _
                                            "recebeSms,aceitaSerContatado " & _
                                " FROM apsspp_initial where nid is not null "
        Else
            cmmFonte.CommandText = "SELECT  nid,dataabertura," & _
                                            "recebeSms,aceitaSerContatado " & _
                                " FROM apsspp_initial where nid is not null and nid in (" & whereQuery & ")"
        End If

        cmmDestino.CommandType = CommandType.Text
        cmmDestino.Connection = ConexaoOpenMRS3

        rs = cmmFonte.Execute

        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF

                nid = rs.Fields.Item("nid").Value

                dataVisitaApss = rs.Fields.Item("dataabertura").Value

                
                patientID = GetPatientOpenMRSIDByNID(nid) 'Get the openmrs patient_id using the NID

                If patientID > 0 Then

                    encounter_id = EncounterDAO.insertEncounterByParam(34, patientID, locationid, 131, dataVisitaApss, 10, 27)

                    If rs.Fields.Item("aceitaSerContatado").Value Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6306
                        obs.value_coded = 1065

                        ObsDAO.insertObs(obs, False)
                    End If

                    If rs.Fields.Item("recebeSms").Value Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounter_id
                        obs.obs_datetime = dataVisitaApss
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6309
                        obs.value_coded = 6307

                        ObsDAO.insertObs(obs, False)
                    End If
                End If
        rs.MoveNext()
            End While
        End If
        rs.Close()
        'Catch ex As Exception
        '    MsgBox("Error Importing Seguimento: " & ex.Message)
        'End Try

    End Sub


End Class
