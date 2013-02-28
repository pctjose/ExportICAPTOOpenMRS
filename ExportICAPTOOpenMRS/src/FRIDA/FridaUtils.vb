Imports ADODB
Imports MySql.Data.MySqlClient
Public Class FridaUtils
    Public Shared Function getTipoTarvConceptID(ByVal tipo As String) As Integer
        Select Case tipo
            Case "Manter"
                Return 1257
            Case "Inicia", "Iniciar"
                Return 1256
            Case "Alterar"
                Return 1259
            Case "Transfer de", "Transferido de"
                Return 1369
            Case "Reiniciar"
                Return 1705
            Case "Transfer para", "Transferido para"
                Return 1706
            Case "Suspender", "Suspenso"
                Return 1709
            Case Else
                Return 1257
        End Select
    End Function
    Public Shared Function getRegimeTerapeuticoConceptID(ByVal regime As String) As Integer
        regime = regime.ToUpper
        Select Case regime
            Case "AZT+3TC+NFV"
                Return 1702
            Case "AZT+3TC+ABC"
                Return 817
            Case "AZT+3TC+EFV"
                Return 1703
            Case "AZT+3TC+NVP"
                Return 1651
            Case "AZT+DDI+NFV", "AZT+DDL+NFV"
                Return 1700
            Case "D4T+3TC+EFV"
                Return 1827
            Case "D4T+3TC+NVP"
                Return 792
            Case "D4T30+3TC+NVP"
                Return 792
            Case "D4T40+3TC+EFV", "D4T30+3TC+EFV"
                Return 1827
            Case "D4T40+3TC+NVP"
                Return 792
            Case "DAT30+3TC+ABC", "D4T30+3TC+ABC", "D4T40+3TC+ABC"
                Return 6102
            Case "D4T6+3TC+NVP(3DFC BABY)"
                Return 6110
            Case "D4T30+DDI+NVP", "D4T30+DDL+NVP"
                Return 6242
            Case Else
                Return 5424
        End Select
    End Function
    Public Shared Function getSaidaTarvConceptID(ByVal saida As String) As Integer
        saida = saida.ToUpper
        Select Case saida
            Case "ABANDONO"
                Return 1707
            Case "HIV NEGATIVO"
                Return 1704
            Case "MORTE", "OBITO", "OBITOU"
                Return 1366
            Case "PERDA DE SEGUIMENTO"
                Return 1707
            Case "SUSPENDER TARV"
                Return 1709
            Case "TRANSFERIDO PARA", "TRANSFERE PARA", "TRANSFER PARA"
                Return 1706
            Case Else
                Return 5622
        End Select
    End Function
    Public Shared Sub ImportFRIDA(ByVal fonte As Connection, ByVal locationid As Int16)
        Dim patientID As Integer
        'Dim regime As Integer
        Dim tipo As Integer
        Dim encounterid As Integer

        'Dim saldo As Integer
        'Dim quantidade As Integer
        Dim dataProxima As Date
        Dim dataArray As New ArrayList
        Dim obs As New Obs
        Dim encounterDate As Date

        Try

            Dim cmmFonte As New Command 'Acess
            Dim cmmDestino As New MySqlCommand 'MySQL
            Dim rs As New Recordset
            Dim count As Int16 = 0

            'cmmDestino.ActiveConnection = fonte
            cmmDestino.CommandType = CommandTypeEnum.adCmdText

            With cmmFonte
                .ActiveConnection = fonte
                .CommandType = CommandType.Text
                If AllPatients Then
                    .CommandText = " Select nid,datatarv,codregime,dias," & _
                                    " tipotarv,codmudanca,dataproxima,observacao," & _
                                    " QtdComp,QtdSaldo,dataoutroservico " & _
                                    " from t_tarv "
                Else
                    .CommandText = " Select nid,datatarv,codregime,dias," & _
                                    " tipotarv,codmudanca,dataproxima,observacao," & _
                                    " QtdComp,QtdSaldo,dataoutroservico " & _
                                    " from t_tarv where nid in (" & whereQuery & ")"
                End If

                rs = .Execute
                If Not (rs.EOF And rs.BOF) Then
                    cmmDestino.Connection = ConexaoOpenMRS1 'cone.conectar
                    cmmDestino.CommandType = CommandType.Text
                    rs.MoveFirst()
                    While Not rs.EOF

                        If Not IsDBNull(rs.Fields.Item("nid").Value) Then



                            patientID = GetPatientOpenMRSIDByNID(rs.Fields.Item("nid").Value) 'Get the openmrs patient_id using the NID

                            If patientID > 0 Then
                                encounterDate = rs.Fields.Item("datatarv").Value
                                If Not TemFridaNestaData(patientID, encounterDate) Then


                                    cmmDestino.CommandText = "Insert into encounter(encounter_type,patient_id,provider_id,location_id," & _
                                                            "form_id,encounter_datetime,creator,date_created,voided,uuid) values(18," & patientID & ",27," & locationid & "," & _
                                                            "117,'" & dataMySQL(encounterDate) & "',22,now(),0,uuid())"
                                    cmmDestino.ExecuteNonQuery()
                                    'Get The encounter id to user in obs table
                                    cmmDestino.CommandText = "Select max(encounter_id) from encounter"
                                    encounterid = cmmDestino.ExecuteScalar

                                    tipo = getTipoTarvConceptID(rs.Fields.Item("tipotarv").Value)

                                    If tipo = 1706 Or tipo = 1709 Then 'Saida Transferido para ou suspenso
                                        'InsertObsQuestion(patientID, 1255, 1708, locationid, encounterid, rs.Fields.Item("datatarv").Value)
                                        'InsertObsQuestion(patientID, 1708, tipo, locationid, encounterid, rs.Fields.Item("datatarv").Value)

                                        If True Then
                                            obs = New Obs
                                            obs.location_id = locationid
                                            obs.person_id = patientID
                                            obs.date_created = Now
                                            obs.voided = 0
                                            obs.encounter_id = encounterid
                                            obs.obs_datetime = encounterDate
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.concept_id = 1255
                                            obs.value_coded = 1708
                                            dataArray.Add(obs)
                                        End If
                                        If True Then
                                            obs = New Obs
                                            obs.location_id = locationid
                                            obs.person_id = patientID
                                            obs.date_created = Now
                                            obs.voided = 0
                                            obs.encounter_id = encounterid
                                            obs.obs_datetime = encounterDate
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.concept_id = 1708
                                            obs.value_coded = tipo
                                            dataArray.Add(obs)
                                        End If
                                    Else
                                        If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "QtdSaldo")) Then
                                            obs = New Obs
                                            obs.location_id = locationid
                                            obs.person_id = patientID
                                            obs.date_created = Now
                                            obs.voided = 0
                                            obs.encounter_id = encounterid
                                            obs.obs_datetime = encounterDate
                                            obs.data_Type = ObsDataType.TNumeric
                                            obs.concept_id = 1713
                                            obs.value_numeric = rs.Fields.Item("QtdSaldo").Value
                                            dataArray.Add(obs)
                                        End If
                                        If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "QtdComp")) Then
                                            obs = New Obs
                                            obs.location_id = locationid
                                            obs.person_id = patientID
                                            obs.date_created = Now
                                            obs.voided = 0
                                            obs.encounter_id = encounterid
                                            obs.obs_datetime = encounterDate
                                            obs.data_Type = ObsDataType.TNumeric
                                            obs.concept_id = 1715
                                            obs.value_numeric = rs.Fields.Item("QtdComp").Value
                                            dataArray.Add(obs)
                                        End If
                                        If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "QtdComp")) Then
                                            obs = New Obs
                                            obs.location_id = locationid
                                            obs.person_id = patientID
                                            obs.date_created = Now
                                            obs.voided = 0
                                            obs.encounter_id = encounterid
                                            obs.obs_datetime = encounterDate
                                            obs.data_Type = ObsDataType.TNumeric
                                            obs.concept_id = 1712
                                            obs.value_numeric = rs.Fields.Item("QtdComp").Value
                                            dataArray.Add(obs)
                                        End If

                                        If IsDBNull(rs.Fields.Item("dataProxima").Value) Then
                                            If Not IsDBNull(rs.Fields.Item("dias").Value) Then
                                                dataProxima = DateAdd(DateInterval.Day, rs.Fields.Item("dias").Value, rs.Fields.Item("datatarv").Value)
                                            Else
                                                dataProxima = DateAdd(DateInterval.Day, 30, encounterDate)
                                            End If
                                        Else
                                            dataProxima = rs.Fields.Item("dataProxima").Value
                                        End If

                                        If True Then
                                            obs = New Obs
                                            obs.location_id = locationid
                                            obs.person_id = patientID
                                            obs.date_created = Now
                                            obs.voided = 0
                                            obs.encounter_id = encounterid
                                            obs.obs_datetime = encounterDate
                                            obs.data_Type = ObsDataType.TDatetime
                                            obs.concept_id = 5096
                                            obs.value_datetime = dataProxima
                                            dataArray.Add(obs)
                                        End If

                                        If True Then
                                            obs = New Obs
                                            obs.location_id = locationid
                                            obs.person_id = patientID
                                            obs.date_created = Now
                                            obs.voided = 0
                                            obs.encounter_id = encounterid
                                            obs.obs_datetime = encounterDate
                                            obs.data_Type = ObsDataType.TText
                                            obs.concept_id = 1711
                                            obs.value_text = "1-0-1"
                                            dataArray.Add(obs)
                                        End If

                                        If True Then
                                            obs = New Obs
                                            obs.location_id = locationid
                                            obs.person_id = patientID
                                            obs.date_created = Now
                                            obs.voided = 0
                                            obs.encounter_id = encounterid
                                            obs.obs_datetime = encounterDate
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.concept_id = 1255
                                            obs.value_coded = tipo
                                            dataArray.Add(obs)
                                        End If

                                        If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "codregime")) Then
                                            obs = New Obs
                                            obs.location_id = locationid
                                            obs.person_id = patientID
                                            obs.date_created = Now
                                            obs.voided = 0
                                            obs.encounter_id = encounterid
                                            obs.obs_datetime = encounterDate
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.concept_id = 1088
                                            obs.value_coded = getRegimeTerapeuticoConceptID(rs.Fields.Item("codregime").Value)
                                            dataArray.Add(obs)
                                        End If

                                        If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "dataoutroservico")) Then
                                            obs = New Obs
                                            obs.location_id = locationid
                                            obs.person_id = patientID
                                            obs.date_created = Now
                                            obs.voided = 0
                                            obs.encounter_id = encounterid
                                            obs.obs_datetime = encounterDate
                                            obs.data_Type = ObsDataType.TDatetime
                                            obs.concept_id = 1190
                                            obs.value_datetime = rs.Fields.Item("dataoutroservico").Value
                                            dataArray.Add(obs)
                                        End If

                                    End If

                                    If dataArray.Count > 0 Then
                                        For Each o As Obs In dataArray
                                            ObsDAO.insertObs(o, False)
                                        Next
                                    End If

                                    dataArray.Clear()
                                End If
                            End If
                        End If
                rs.MoveNext()
                    End While
                '.Connection.close()
                '.Connection.Dispose()
                rs.Close()
                End If
            End With
            ImportEstadoPaciente(fonte, locationid)
            ImportHistoricoARV(fonte, locationid)
        Catch ex As Exception
            MsgBox("Erro ao FRIDA. " & ex.Message)
        End Try
    End Sub
    Public Shared Sub ImportHistoricoARV(ByVal fonte As Connection, ByVal locationid As Int16)
        Dim patientID As Integer

        'Dim tipo As Integer
        Dim encounter_id As Integer

        Dim saida As Integer
        

        Try

            Dim cmmFonte As New Command 'Acess
            Dim cmmDestino As New MySqlCommand 'MySQL
            Dim rs As New Recordset
            Dim count As Int16 = 0
            Dim dataSaida As Date

            'cmmDestino.ActiveConnection = fonte
            cmmDestino.CommandType = CommandTypeEnum.adCmdText

            With cmmFonte
                .ActiveConnection = fonte
                .CommandType = CommandType.Text
                .CommandText = "Select t_histestadopaciente.nid,t_histestadopaciente.codestado,t_histestadopaciente.dataestado from t_histestadopaciente,t_paciente " & _
                "where t_histestadopaciente.nid=t_paciente.nid and t_histestadopaciente.dataestado>=t_paciente.datainiciotarv and " & _
                "t_paciente.emtarv=True and t_histestadopaciente.codestado is not null and dataestado is not null"
                rs = .Execute
                If Not (rs.EOF And rs.BOF) Then
                    cmmDestino.Connection = ConexaoOpenMRS1 'cone.conectar
                    cmmDestino.CommandType = CommandType.Text
                    rs.MoveFirst()
                    While Not rs.EOF
                        patientID = GetPatientOpenMRSIDByNID(rs.Fields.Item("nid").Value) 'Get the openmrs patient_id using the NID
                        dataSaida = rs.Fields.Item("dataestado").Value
                        If patientID > 0 Then
                            If Not SaiuMesmaData(patientID, dataSaida) Then
                                saida = getSaidaTarvConceptID(rs.Fields.Item("codestado").Value)
                                If Not TemFridaNestaData(patientID, dataSaida) Then
                                    cmmDestino.CommandText = "Insert into encounter(encounter_type,patient_id,provider_id,location_id," & _
                                                                                            "form_id,encounter_datetime,creator,date_created,voided,uuid) values(18," & patientID & ",27," & locationid & "," & _
                                                                                            "117,'" & dataMySQL(rs.Fields.Item("dataestado").Value) & "',22,now(),0,uuid())"
                                    cmmDestino.ExecuteNonQuery()
                                    'Get The encounter id to user in obs table
                                    cmmDestino.CommandText = "Select max(encounter_id) from encounter"
                                    encounter_id = cmmDestino.ExecuteScalar
                                    InsertObsQuestion(patientID, 1255, 1708, locationid, encounter_id, dataSaida)
                                    InsertObsQuestion(patientID, 1708, saida, locationid, encounter_id, dataSaida)
                                    'Else
                                    '    encounter_id = getEncounterFridaID(patientID, dataSaida)
                                    '    cmmDestino.CommandText = "update obs set value_coded=1708 where encounter_id=" & encounter_id & " and " & _
                                    '    " person_id=" & patientID & " and concept_id=1255"
                                    '    cmmDestino.ExecuteNonQuery()
                                    '    InsertObsQuestion(patientID, 1708, saida, locationid, encounter_id, dataSaida)
                                End If

                            End If
                        End If
                        rs.MoveNext()
                    End While
                    '.Connection.close()
                    '.Connection.Dispose()
                    rs.Close()
                End If
            End With


        Catch ex As Exception
            MsgBox("Error Importing TARV History: " & ex.Message)
            'Nerros += 1
        End Try
    End Sub
    Public Shared Sub ImportEstadoPaciente(ByVal fonte As Connection, ByVal locationid As Int16)
        Dim patientID As Integer

        'Dim tipo As Integer
        Dim encounter_id As Integer

        Dim saida As Integer

        Try

            Dim cmmFonte As New Command 'Acess
            Dim cmmDestino As New MySqlCommand 'MySQL
            Dim rs As New Recordset
            Dim count As Int16 = 0
            Dim dataSaida As Date

            'cmmDestino.ActiveConnection = fonte
            cmmDestino.CommandType = CommandTypeEnum.adCmdText

            With cmmFonte
                .ActiveConnection = fonte
                .CommandType = CommandType.Text
                .CommandText = "SELECT distinct t_paciente.emtarv,t_tarv.nid, t_paciente.datasaidatarv, t_paciente.codestado " & _
                                " FROM t_paciente INNER JOIN t_tarv ON t_paciente.nid = t_tarv.nid " & _
                                " WHERE (((t_paciente.codestado) Is Not Null)) and datasaidatarv is not null"

                rs = .Execute
                If Not (rs.EOF And rs.BOF) Then
                    cmmDestino.Connection = ConexaoOpenMRS1 'cone.conectar
                    cmmDestino.CommandType = CommandType.Text
                    rs.MoveFirst()
                    While Not rs.EOF
                        If rs.Fields.Item("emtarv").Value Then
                            patientID = GetPatientOpenMRSIDByNID(rs.Fields.Item("nid").Value) 'Get the openmrs patient_id using the NID

                            If patientID > 0 Then


                                dataSaida = rs.Fields.Item("datasaidatarv").Value

                                If Not SaiuMesmaData(patientID, dataSaida) Then
                                    saida = getSaidaTarvConceptID(rs.Fields.Item("codestado").Value)
                                    If Not TemFridaNestaData(patientID, dataSaida) Then
                                        cmmDestino.CommandText = "Insert into encounter(encounter_type,patient_id,provider_id,location_id," & _
                                                                                                "form_id,encounter_datetime,creator,date_created,voided,uuid) values(18," & patientID & ",27," & locationid & "," & _
                                                                                                "117,'" & dataMySQL(rs.Fields.Item("datasaidatarv").Value) & "',22,now(),0,uuid())"
                                        cmmDestino.ExecuteNonQuery()
                                        'Get The encounter id to user in obs table
                                        cmmDestino.CommandText = "Select max(encounter_id) from encounter"
                                        encounter_id = cmmDestino.ExecuteScalar
                                        InsertObsQuestion(patientID, 1255, 1708, locationid, encounter_id, dataSaida)
                                        InsertObsQuestion(patientID, 1708, saida, locationid, encounter_id, dataSaida)
                                    Else
                                        dataSaida = DateAdd(DateInterval.Day, 1, dataSaida)
                                        cmmDestino.CommandText = "Insert into encounter(encounter_type,patient_id,provider_id,location_id," & _
                                                                                                "form_id,encounter_datetime,creator,date_created,voided,uuid) values(18," & patientID & ",27," & locationid & "," & _
                                                                                                "117,'" & dataMySQL(dataSaida) & "',22,now(),0,uuid())"
                                        cmmDestino.ExecuteNonQuery()
                                        'Get The encounter id to user in obs table
                                        cmmDestino.CommandText = "Select max(encounter_id) from encounter"
                                        encounter_id = cmmDestino.ExecuteScalar
                                        InsertObsQuestion(patientID, 1255, 1708, locationid, encounter_id, dataSaida)
                                        InsertObsQuestion(patientID, 1708, saida, locationid, encounter_id, dataSaida)

                                    End If

                                End If
                                End If

                            End If
                            rs.MoveNext()
                    End While
                    '.Connection.close()
                    '.Connection.Dispose()
                    rs.Close()
                End If
            End With


        Catch ex As Exception
            MsgBox("Error Importing TARV History: " & ex.Message)
            'Nerros += 1
        End Try
    End Sub
    Private Shared Function SaiuMesmaData(ByVal patientid As Integer, ByVal dataSaida As Date) As Boolean
        Dim strString As String
        Dim cmmSaida As New MySqlCommand 'MySQL
        Dim counter As Int16
        strString = "Select count(*) from encounter,obs where encounter.encounter_id=obs.encounter_id and " & _
        " encounter.voided=0 and obs.voided=0 and encounter.patient_id=" & patientid & " and encounter.encounter_datetime='" & dataMySQL(dataSaida) & "' and " & _
        " obs.concept_id=1255 and obs.value_coded=1708 and encounter.encounter_type=18 "
        cmmSaida.Connection = ConexaoOpenMRS3
        cmmSaida.CommandType = CommandType.Text
        cmmSaida.CommandText = strString
        counter = cmmSaida.ExecuteScalar
        If counter = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Shared Function TemFridaNestaData(ByVal patientid As Integer, ByVal dataSaida As Date) As Boolean
        Dim strString As String
        Dim cmmSaida As New MySqlCommand 'MySQL
        Dim counter As Int16
        strString = "Select count(*) from encounter where " & _
        " encounter.voided=0 and encounter.patient_id=" & patientid & " and encounter.encounter_datetime='" & dataMySQL(dataSaida) & "' and " & _
        " encounter.encounter_type=18"
        cmmSaida.Connection = ConexaoOpenMRS3
        cmmSaida.CommandType = CommandType.Text
        cmmSaida.CommandText = strString
        counter = cmmSaida.ExecuteScalar
        If counter = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Shared Function getEncounterFridaID(ByVal patientid As Integer, ByVal dataSaida As Date) As Integer
        Dim strString As String
        Dim cmmSaida As New MySqlCommand 'MySQL
        'Dim counter As Int16
        strString = "Select encounter.encounter_id from encounter where " & _
        " encounter.voided=0 and encounter.patient_id=" & patientid & " and encounter.encounter_datetime='" & dataMySQL(dataSaida) & "' and " & _
        " encounter.encounter_type=18"
        cmmSaida.Connection = ConexaoOpenMRS3
        cmmSaida.CommandType = CommandType.Text
        cmmSaida.CommandText = strString
        Return cmmSaida.ExecuteScalar
        
    End Function
End Class
