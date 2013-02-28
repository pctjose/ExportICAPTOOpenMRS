Imports ADODB
Imports MySql.Data.MySqlClient

Public Class TuberculoseRastreioUtils
    Public Shared Sub ImportTuberculoseReal(ByVal fonte As Connection, ByVal location As Int16)
        Dim cmmFonte As New Command
        Dim rs As New Recordset
        With cmmFonte
            .ActiveConnection = fonte
            .CommandType = CommandType.Text
            If AllPatients Then
                .CommandText = "Select distinct nid from t_questionariotb"
            Else
                .CommandText = "Select distinct nid from t_questionariotb where nid in (" & whereQuery & ")"

            End If

            rs = .Execute
            If Not (rs.EOF And rs.BOF) Then
                While Not rs.EOF
                    ImportTuberculose(fonte, location, rs.Fields.Item("nid").Value)
                    rs.MoveNext()
                End While

            End If
        End With
    End Sub

    Public Shared Sub ImportTuberculose(ByVal fonte As Connection, ByVal locationid As Int16, ByVal nid As String)
        Dim patientID As Integer

        Dim encounter_id As Integer



        Try

            Dim cmmFonte As New Command 'Acess
            Dim cmmDestino As New MySqlCommand 'MySQL
            Dim rs As New Recordset

            Dim count As Int16 = 0

            Dim dataInicial As Date
            Dim dataCorrente As Date

            Dim codOpcao As String


            Dim notSet As New ArrayList

            Dim obs As Obs


            'cmmDestino.ActiveConnection = fonte
            cmmDestino.CommandType = CommandTypeEnum.adCmdText

            With cmmFonte
                .ActiveConnection = fonte
                .CommandType = CommandType.Text
                .CommandText = "Select codopcao,estadoopcao,data,observacao from t_questionariotb where nid='" & nid & "' order by data"
                'rs = .Execute
                rs.Open("Select codopcao,estadoopcao,data,observacao from t_questionariotb where nid='" & nid & "' order by data", fonte, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockReadOnly)

                If Not (rs.EOF And rs.BOF) Then
                    cmmDestino.Connection = ConexaoOpenMRS1 'cone.conectar
                    cmmDestino.CommandType = CommandType.Text
                    rs.MoveFirst()
                    dataInicial = rs.Fields.Item("data").Value 'First Initial date

                    patientID = GetPatientOpenMRSIDByNID(nid) 'get Current OpenMRS Patient ID

                    If patientID > 0 Then

                        'End If

                        While Not rs.EOF


                            dataCorrente = rs.Fields.Item("data").Value

                            codOpcao = PatientUtils.verificaNulo(rs, "codopcao") 'rs.Fields.Item("codopcao").Value

                            codOpcao = codOpcao.ToUpper

                            If dataCorrente = dataInicial Then 'Resultados da mesma data, neste caso mesma consulta no openmrs
                                obs = New Obs
                                obs.obs_datetime = dataInicial
                                obs.location_id = locationid
                                obs.person_id = patientID
                                obs.date_created = Now
                                obs.voided = 0
                                Select Case codOpcao
                                    Case "ALGUÉM NA FAMÍLIA ESTÁ TRATANDO A TB?", "ALGUEM NA FAMILIA ESTA TRATANDO A TB?"
                                        If rs.Fields.Item("estadoopcao").Value Then
                                            obs.concept_id = 1766
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 1765
                                            notSet.Add(obs)
                                        End If

                                    Case "FEBRE POR MAIS DE 3 SEMANAS?"
                                        If rs.Fields.Item("estadoopcao").Value Then
                                            obs.concept_id = 1766
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 1763
                                            notSet.Add(obs)
                                        End If
                                    Case "PERDEU PESO (MAIS DE 3 KG NO ULTIMO MÊS)?"
                                        If rs.Fields.Item("estadoopcao").Value Then
                                            obs.concept_id = 1766
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 1764
                                            notSet.Add(obs)
                                        End If
                                    Case "SUORES Á NOITE POR MAIS DE 3 SEMANAS?"
                                        If rs.Fields.Item("estadoopcao").Value Then
                                            obs.concept_id = 1766
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 1762
                                            notSet.Add(obs)
                                        End If
                                    Case "TOSSE COM SANGUE?"
                                        If rs.Fields.Item("estadoopcao").Value Then
                                            obs.concept_id = 1766
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 1761
                                            notSet.Add(obs)
                                        End If
                                    Case "TOSSE POR MAIS DE 3 SEMANAS?"
                                        If rs.Fields.Item("estadoopcao").Value Then
                                            obs.concept_id = 1766
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 1760
                                            notSet.Add(obs)
                                        End If
                                End Select

                            Else
                                'Inserir no openmrs
                                'Insert encounter first:
                                cmmDestino.CommandText = "Insert into encounter(encounter_type,patient_id,provider_id,location_id," & _
                                                    "form_id,encounter_datetime,creator,date_created,voided,uuid) values(20," & patientID & ",27," & locationid & "," & _
                                                    "118,'" & dataMySQL(dataInicial) & "',22,now(),0,uuid())"
                                cmmDestino.ExecuteNonQuery()
                                'get last inserted encounter id, for use in obs's
                                cmmDestino.CommandText = "Select max(encounter_id) from encounter"
                                encounter_id = cmmDestino.ExecuteScalar



                                'Dim obsGroupId As Integer


                                If notSet.Count > 0 Then
                                    For Each o As Obs In notSet
                                        o.encounter_id = encounter_id
                                        ObsDAO.insertObs(o, False)
                                    Next
                                End If
                                dataInicial = dataCorrente 'Mudar o valor da data inicial ja e outra consulta
                                rs.MovePrevious() 'Posionar-se na consulta que nao coicidiu com a anterior
                                notSet.Clear()
                            End If
                            rs.MoveNext()
                        End While


                        ''Ultimas Consultas
                        'Inserir no openmrs
                        'Insert encounter first:
                        cmmDestino.CommandText = "Insert into encounter(encounter_type,patient_id,provider_id,location_id," & _
                                            "form_id,encounter_datetime,creator,date_created, voided, uuid) values(20," & patientID & ",27," & locationid & "," & _
                                            "62,'" & dataMySQL(dataInicial) & "',22,now(),0,uuid())"
                        cmmDestino.ExecuteNonQuery()
                        'get last inserted encounter id, for use in obs's
                        cmmDestino.CommandText = "Select max(encounter_id) from encounter"
                        encounter_id = cmmDestino.ExecuteScalar

                        If notSet.Count > 0 Then
                            For Each o As Obs In notSet
                                o.encounter_id = encounter_id
                                ObsDAO.insertObs(o, False)
                            Next
                        End If

                        notSet.Clear()
                        '.Connection.close()
                        '.Connection.Dispose()

                    End If
                End If
                rs.Close()
            End With

            'ImportHistoricoARV(fonte, locationid)
            'MsgBox("Done")
        Catch ex As Exception
            MsgBox("Error Importing Tuberculose encounters: " & ex.Message)
            'Nerros += 1
        End Try
    End Sub

    'Private Class TratamentoTB
    '    Public 
    'End Class
End Class
