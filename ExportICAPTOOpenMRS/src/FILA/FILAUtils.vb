Imports ADODB
Imports MySql.Data.MySqlClient
Public Class FILAUtils
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

        Select Case regime
            Case "TDF+3TC+EFV"
                Return 6324
            Case "TDF+3TC+LPV/r"
                Return 6108
            Case "TDF+3TC+NVP"
                Return 6243
            Case "TDF+3TC+RAL+DRV/r"
                Return 6329
            Case "TDF+ABC+LPV/r"
                Return 6234
            Case "TDF+AZT+3TC+LPV/r"
                Return 6107
            Case "AZT+3TC+NFV"
                Return 1702
            Case "AZT+3TC+ABC", "AZT 60+3TC+ABC", "AZT 60+3TC+ABC (2DFC + ABC)"
                Return 817
            Case "AZT+3TC+EFV", "AZT 60+3TC+EFV (2DFC + EFV)"
                Return 1703
            Case "AZT+3TC+NVP", "AZT60+3TC+NVP (3DFC)"
                Return 1651
            Case "AZT+DDI+NFV", "AZT+DDL+NFV"
                Return 1700
            Case "D4T+3TC+EFV", "d4T+3TC+ EFV"
                Return 1827
            Case "D4T+3TC+NVP"
                Return 792
            Case "D4T30+3TC+NVP"
                Return 792
            Case "D4T40+3TC+EFV", "D4T30+3TC+EFV"
                Return 1827
            Case "D4T40+3TC+NVP", "d4t20+3tc+nvp"
                Return 792
            Case "DAT30+3TC+ABC", "D4T30+3TC+ABC", "D4T40+3TC+ABC", "d4T+3TC+ABC"
                Return 6102
            Case "D4T6+3TC+NVP(3DFC BABY)", "d4T+3TC+NVP (3DFC Baby )"
                Return 6110
            Case "D4T30+DDI+NVP", "D4T30+DDL+NVP", "d4T40+Ddl+NVP"
                Return 6242
            Case "ABC+3TC + LPV200/r50", "ABC+3TC+LPV", "ABC+3TC+LPV200/r50"
                Return 1311
            Case "ABC+3TC+EFV"
                Return 6104
            Case "ABC+3TC+LPV/r"
                Return 1313
            Case "ABC+3TC+NVP"
                Return 6105
            Case "AZT 60+3TC+LPV/r(2DFC+LPV/r)", "AZT+3TC+LPV/r"
                Return 6100
            Case "AZT+3TC+ABC+LPV/r"
                Return 6326
            Case "AZT+3TC+ddI+LPV/r"
                Return 6233
            Case "AZT+ddI+LPV/r"
                Return 6109
            Case "d4t+3TC+ABC+EFV"
                Return 6327
            Case "d4t+3TC+ABC+LPV/r"
                Return 6325
            Case "d4t+3TC+LPV/r"
                Return 6103
            Case "d4T+3TC+LPV/r(2DFC Baby+LPV/r)"
                Return 6113
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
    Public Shared Function getCodMudancaConceptID(ByVal mudanca As String) As Integer

        Select Case mudanca
            Case "Anemia (A)"
                Return 3
            Case "Ausência de Eficácia Inicial"
                Return 1789
            Case "Falencia terapeutica (FT)", "Falha Terapêutica Laboratorial", "Falha Terapêutica Clínica"
                Return 1790
            Case "Gravidez (Gr)"
                Return 1982
            Case "Intolerância"
                Return 987
            Case "Tb", "Tuberculose (TB)"
                Return 1264
            Case Else
                Return 5622
        End Select
    End Function
    Public Shared Sub ImportFILA(ByVal fonte As Connection, ByVal locationid As Int16)
        Dim patientID As Integer
        Dim encounterid As Integer
        Dim dataProxima As Date
        Dim dataArray As New ArrayList
        Dim obs As New Obs
        Dim encounterDate As Date

        Try

            Dim cmmFonte As New Command 'Acess
            'Dim cmmDestino As New MySqlCommand 'MySQL
            Dim rs As New Recordset
            Dim count As Int16 = 0



            With cmmFonte
                .ActiveConnection = fonte
                .CommandType = CommandType.Text
                If AllPatients Then
                    .CommandText = "Select t_tarv.nid,datatarv,t_tarv.codregime,QtdComp,dose,dataproxima from t_paciente inner join t_tarv on t_paciente.nid=t_tarv.nid where t_tarv.nid is not null and t_tarv.datatarv is not null"

                Else
                    .CommandText = "Select t_tarv.nid,datatarv,t_tarv.codregime,QtdComp,dose,dataproxima from t_paciente inner join t_tarv on t_paciente.nid=t_tarv.nid where t_tarv.nid is not null and t_tarv.datatarv is not null and t_tarv.nid in (" & whereQuery & ")"

                End If

                rs = .Execute
                If Not (rs.EOF And rs.BOF) Then

                    rs.MoveFirst()
                    While Not rs.EOF

                        patientID = GetPatientOpenMRSIDByNID(rs.Fields.Item("nid").Value) 'Get the openmrs patient_id using the NID

                        If patientID > 0 Then
                            encounterDate = rs.Fields.Item("datatarv").Value

                            encounterid = EncounterDAO.insertEncounterByParam(18, patientID, locationid, 130, encounterDate, 8, 27)

                            If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "QtdComp")) Then
                                Dim qty As Integer = rs.Fields.Item("QtdComp").Value
                                If qty > 0 And qty <= 180 Then
                                    obs = New Obs
                                    obs.location_id = locationid
                                    obs.person_id = patientID
                                    obs.date_created = Now
                                    obs.voided = 0
                                    obs.encounter_id = encounterid
                                    obs.obs_datetime = encounterDate
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.concept_id = 1715
                                    obs.value_numeric = qty
                                    dataArray.Add(obs)
                                End If

                            End If


                            If IsDBNull(rs.Fields.Item("dataProxima").Value) Then
                                If Not IsDBNull(rs.Fields.Item("dias").Value) Then
                                    dataProxima = DateAdd(DateInterval.Day, rs.Fields.Item("dias").Value, encounterDate)
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


                            If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "dose")) Then

                                obs = New Obs
                                obs.location_id = locationid
                                obs.person_id = patientID
                                obs.date_created = Now
                                obs.voided = 0
                                obs.encounter_id = encounterid
                                obs.obs_datetime = encounterDate
                                obs.data_Type = ObsDataType.TText
                                obs.concept_id = 1711
                                obs.value_text = rs.Fields.Item("dose").Value
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



                            For Each o As Obs In dataArray
                                ObsDAO.insertObs(o, False)
                            Next


                            dataArray.Clear()
                        End If


                        rs.MoveNext()
                    End While
                    rs.Close()
                End If
            End With
            'ImportEstadoPaciente(fonte, locationid)
            'ImportHistoricoARV(fonte, locationid)
        Catch ex As Exception
            MsgBox("Erro ao FILA. " & ex.Message)
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
