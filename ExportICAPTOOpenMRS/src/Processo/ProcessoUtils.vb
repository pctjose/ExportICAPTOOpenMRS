Imports ADODB
Imports MySql.Data.MySqlClient

Public Class ProcessoUtils
    Public Shared Function importMae(ByVal idMae As Integer) As ArrayList
        Dim Comando As New Command
        Dim rs As New Recordset
        Dim obs As New Obs
        Dim nome As String
        Dim idade As String
        Dim vivo As String
        Dim doente As String
        Dim doenca As String
        Dim profissao As String
        Dim resultadoHIV As String
        Dim emtarv As String
        Dim maedata As New ArrayList
        'MATERNAL DATA CONCEPT SET ID 1607
        Comando.ActiveConnection = ICAPConection
        Comando.CommandType = CommandTypeEnum.adCmdText
        Comando.CommandText = "Select nome,idade,vivo,doente,doenca,codprofissao,resultadohiv,emtarv from t_mae where idmae=" & idMae & ""

        rs = Comando.Execute
        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            nome = PatientUtils.verificaNulo(rs, "nome")
            idade = PatientUtils.verificaNulo(rs, "idade")
            vivo = PatientUtils.verificaNulo(rs, "vivo")
            doente = PatientUtils.verificaNulo(rs, "doente")
            doenca = PatientUtils.verificaNulo(rs, "doenca")
            profissao = PatientUtils.verificaNulo(rs, "codprofissao")
            resultadoHIV = PatientUtils.verificaNulo(rs, "resultadohiv")
            emtarv = PatientUtils.verificaNulo(rs, "emtarv")

            If Not String.IsNullOrEmpty(nome) Then
                obs = New Obs
                obs.concept_id = 1477
                obs.value_text = nome
                obs.data_Type = ObsDataType.TText
                maedata.Add(obs)
            End If
            If Not String.IsNullOrEmpty(idade) Then
                obs = New Obs
                obs.concept_id = 1478
                obs.value_text = idade
                obs.data_Type = ObsDataType.TNumeric
                maedata.Add(obs)
            End If

            If Not String.IsNullOrEmpty(vivo) Then
                vivo = vivo.ToUpper
                obs = New Obs
                obs.concept_id = 1479
                obs.data_Type = ObsDataType.TCoded
                If vivo = "SIM" Or vivo = "S" Then
                    obs.value_coded = 1065
                ElseIf vivo = "NÃO" Or vivo = "NAO" Or vivo = "N" Then
                    obs.value_coded = 1066
                Else
                    obs.value_coded = 1457
                End If
                maedata.Add(obs)
            End If

            If Not String.IsNullOrEmpty(doente) Then

                obs = New Obs
                doente = doente.ToUpper
                obs.concept_id = 1480
                obs.data_Type = ObsDataType.TCoded
                If doente = "SIM" Or doente = "S" Then
                    obs.value_coded = 1065
                ElseIf doente = "NAO" Or doente = "NÃO" Or doente = "N" Then
                    obs.value_coded = 1066
                Else
                    obs.value_coded = 1457
                End If
                maedata.Add(obs)
           
            End If

            If Not String.IsNullOrEmpty(doenca) Then
                obs = New Obs
                obs.concept_id = 1481
                obs.value_text = doenca
                obs.data_Type = ObsDataType.TText
                maedata.Add(obs)
            End If

            If Not String.IsNullOrEmpty(profissao) Then
                obs = New Obs
                obs.concept_id = 1482
                obs.value_text = profissao
                obs.data_Type = ObsDataType.TText
                maedata.Add(obs)
            End If

            If Not String.IsNullOrEmpty(resultadoHIV) Then
                obs = New Obs
                obs.concept_id = 1483
                obs.data_Type = ObsDataType.TCoded
                resultadoHIV = resultadoHIV.ToUpper
                Select Case resultadoHIV
                    Case "SEM INFORMAÇÃO", "SEM INFORMACAO"
                        obs.value_coded = 1457
                    Case "POSITIVO", "POSETIVO"
                        obs.value_coded = 703
                    Case "NEGATIVO"
                        obs.value_coded = 664
                    Case "NAO FEZ", "NÃO FEZ"
                        obs.value_coded = 1118
                    Case "INDETERMINADO"
                        obs.value_coded = 1138
                    Case Else
                        obs.value_coded = 1457
                End Select
                maedata.Add(obs)
            End If

            If Not String.IsNullOrEmpty(emtarv) Then
                obs = New Obs
                emtarv = emtarv.ToUpper
                obs.concept_id = 1484
                obs.data_Type = ObsDataType.TCoded
                If emtarv = "SIM" Or emtarv = "S" Then
                    obs.value_coded = 1065
                ElseIf emtarv = "NAO" Or emtarv = "NÃO" Or emtarv = "N" Then
                    obs.value_coded = 1066
                Else
                    obs.value_coded = 1457
                End If
                maedata.Add(obs)
            End If

        End If
        rs.Close()
        Return maedata
    End Function
    Public Shared Function importPai(ByVal idPai As Integer) As ArrayList
        Dim Comando As New Command
        Dim rs As New Recordset
        Dim obs As New Obs
        Dim nome As String
        Dim idade As String
        Dim vivo As String
        Dim doente As String
        Dim doenca As String
        Dim profissao As String
        Dim resultadoHIV As String
        Dim emtarv As String
        Dim paidata As New ArrayList
        'MATERNAL DATA CONCEPT SET ID 1608
        Comando.ActiveConnection = ICAPConection
        Comando.CommandType = CommandTypeEnum.adCmdText
        Comando.CommandText = "Select nome,idade,vivo,doente,doenca,codprofissao,resultadohiv,emtarv from t_pai where idpai=" & idPai & ""

        rs = Comando.Execute
        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            nome = PatientUtils.verificaNulo(rs, "nome")
            idade = PatientUtils.verificaNulo(rs, "idade")
            vivo = PatientUtils.verificaNulo(rs, "vivo")
            doente = PatientUtils.verificaNulo(rs, "doente")
            doenca = PatientUtils.verificaNulo(rs, "doenca")
            profissao = PatientUtils.verificaNulo(rs, "codprofissao")
            resultadoHIV = PatientUtils.verificaNulo(rs, "resultadohiv")
            emtarv = PatientUtils.verificaNulo(rs, "emtarv")

            If Not String.IsNullOrEmpty(nome) Then
                obs = New Obs
                obs.concept_id = 1485
                obs.value_text = nome
                obs.data_Type = ObsDataType.TText
                paidata.Add(obs)
            End If
            If Not String.IsNullOrEmpty(idade) Then
                obs = New Obs
                obs.concept_id = 1486
                obs.value_text = idade
                obs.data_Type = ObsDataType.TNumeric
                paidata.Add(obs)
            End If

            If Not String.IsNullOrEmpty(vivo) Then
                obs = New Obs
                obs.concept_id = 1487
                vivo = vivo.ToUpper
                obs.data_Type = ObsDataType.TCoded
                If vivo = "SIM" Or vivo = "S" Then
                    obs.value_coded = 1065
                ElseIf vivo = "NAO" Or vivo = "NÃO" Or vivo = "N" Then
                    obs.value_coded = 1066
                Else
                    obs.value_coded = 1457
                End If
                paidata.Add(obs)
            
            End If

            If Not String.IsNullOrEmpty(doente) Then
                obs = New Obs
                obs.concept_id = 1488
                doente = doente.ToUpper
                obs.data_Type = ObsDataType.TCoded
                If doente = "SIM" Or doente = "S" Then
                    obs.value_coded = 1065
                ElseIf doente = "NAO" Or doente = "NÃO" Or doente = "N" Then
                    obs.value_coded = 1066
                Else
                    obs.value_coded = 1457
                End If
                paidata.Add(obs)
            End If

            If Not String.IsNullOrEmpty(doenca) Then
                obs = New Obs
                obs.concept_id = 1489
                obs.value_text = doenca
                obs.data_Type = ObsDataType.TText
                paidata.Add(obs)
            End If

            If Not String.IsNullOrEmpty(profissao) Then
                obs = New Obs
                obs.concept_id = 1490
                obs.value_text = profissao
                obs.data_Type = ObsDataType.TText
                paidata.Add(obs)
            End If

            If Not String.IsNullOrEmpty(resultadoHIV) Then
                obs = New Obs
                obs.concept_id = 1491
                obs.data_Type = ObsDataType.TCoded
                resultadoHIV = resultadoHIV.ToUpper
                Select Case resultadoHIV
                    Case "SEM INFORMAÇÃO", "SEM INFORMACAO"
                        obs.value_coded = 1457
                    Case "POSITIVO", "POSETIVO"
                        obs.value_coded = 703
                    Case "NEGATIVO"
                        obs.value_coded = 664
                    Case "NAO FEZ", "NÃO FEZ"
                        obs.value_coded = 1118
                    Case "INDETERMINADO"
                        obs.value_coded = 1138
                    Case Else
                        obs.value_coded = 1457
                End Select
                paidata.Add(obs)
            End If

            If Not String.IsNullOrEmpty(emtarv) Then
                obs = New Obs
                obs.concept_id = 1492
                obs.data_Type = ObsDataType.TCoded
                emtarv = emtarv.ToUpper
                If emtarv = "SIM" Or emtarv = "S" Then
                    obs.value_coded = 1065
                ElseIf emtarv = "NAO" Or emtarv = "NÃO" Or emtarv = "N" Then
                    obs.value_coded = 1066
                Else
                    obs.value_coded = 1457
                End If
                paidata.Add(obs)
           
            End If

        End If
        rs.Close()
        Return paidata
    End Function

    Public Shared Function importPessoaReferencia(ByVal nidDoente As String) As ArrayList
        Dim Comando As New Command
        Dim rs As New Recordset
        Dim obs As New Obs
        Dim nome As String
        Dim apelido As String
        Dim telefone As String
        
        Dim contactoData As New ArrayList
        'REFERAL PERSON CONCEPT SET ID 1609
        Comando.ActiveConnection = ICAPConection
        Comando.CommandType = CommandTypeEnum.adCmdText
        Comando.CommandText = "Select nome,apelido,telefone from t_contacto where nid='" & nidDoente & "'"

        rs = Comando.Execute
        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF
                nome = PatientUtils.replaceAcento(PatientUtils.verificaNulo(rs, "nome"))
                apelido = PatientUtils.replaceAcento(PatientUtils.verificaNulo(rs, "apelido"))
                telefone = PatientUtils.replaceAcento(PatientUtils.verificaNulo(rs, "telefone"))

                If Not String.IsNullOrEmpty(nome) Then
                    obs = New Obs
                    obs.concept_id = 1441
                    obs.value_text = nome
                    obs.data_Type = ObsDataType.TText
                    contactoData.Add(obs)
                End If

                If Not String.IsNullOrEmpty(apelido) Then
                    obs = New Obs
                    obs.concept_id = 1442
                    obs.value_text = apelido
                    obs.data_Type = ObsDataType.TText
                    contactoData.Add(obs)
                End If

                If Not String.IsNullOrEmpty(telefone) Then
                    obs = New Obs
                    obs.concept_id = 1611
                    obs.value_text = telefone
                    obs.data_Type = ObsDataType.TText
                    contactoData.Add(obs)
                End If
                rs.MoveNext()
            End While
        End If
        rs.Close()
        Return contactoData
    End Function

    Public Shared Function importAntecedentesClinicos(ByVal nidDoente As String) As ArrayList
        Dim Comando As New Command
        Dim rs As New Recordset
        Dim obs As Obs
        Dim codAntecendente As String

        Dim tipoPaciente As String

        Dim estado As String
        Dim antecedentes As New ArrayList

        Comando.ActiveConnection = ICAPConection
        Comando.CommandType = CommandTypeEnum.adCmdText
        Comando.CommandText = "SELECT t_antecedentesclinicospaciente.codantecendentes, " & _
                            " t_antecedentesclinicospaciente.datadiagnostico, " & _
                            " t_paciente.tipopaciente,t_antecedentesclinicospaciente.Estado,t_antecedentesclinicospaciente.observacao " & _
                            "FROM t_paciente INNER JOIN t_antecedentesclinicospaciente ON t_paciente.nid = t_antecedentesclinicospaciente.nid " & _
                            " where t_antecedentesclinicospaciente.nid = '" & nidDoente & "'"


        rs = Comando.Execute
        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF

                obs = New Obs

                codAntecendente = PatientUtils.verificaNulo(rs, "codantecendentes")

                tipoPaciente = PatientUtils.verificaNulo(rs, "tipopaciente")

                estado = PatientUtils.verificaNulo(rs, "Estado")
                estado = estado.ToUpper

                If String.IsNullOrEmpty(estado) Then
                    obs.value_coded = 1065
                ElseIf estado = "S" Or estado = "SIM" Or estado = "SM" Then
                    obs.value_coded = 1065
                ElseIf estado = "N" Or estado = "NÃO" Or estado = "NAO" Then
                    obs.value_coded = 1066
                Else
                    obs.value_coded = 1457
                End If

                obs.data_Type = ObsDataType.TCoded

                If Not IsDBNull(rs.Fields.Item("datadiagnostico").Value) Then
                    obs.obs_datetime = rs.Fields.Item("datadiagnostico").Value
                End If

                If tipoPaciente = "Adulto" Or tipoPaciente = "adulto" Or tipoPaciente = "ADULTO" Then
                    'obs.concept_id = 1670                   

                    Select Case codAntecendente
                        Case "Candidiase esofásica"
                            obs.concept_id = 5340
                            antecedentes.Add(obs)
                        Case "Candidiase oral"
                            obs.concept_id = 5334
                            antecedentes.Add(obs)
                        Case "Condiloma"
                            obs.concept_id = 1381
                            antecedentes.Add(obs)
                        Case "Corrimento", "DTS Corrimento"
                            obs.concept_id = 1379
                            antecedentes.Add(obs)
                        Case "Diarreia crónica"
                            obs.concept_id = 5018
                            antecedentes.Add(obs)
                        Case "Febre prolongada"
                            obs.concept_id = 5027
                            antecedentes.Add(obs)
                        Case "Herpes Zoster"
                            obs.concept_id = 836
                            antecedentes.Add(obs)
                        Case "Perda de peso mais de 10%"
                            obs.concept_id = 5339
                            antecedentes.Add(obs)
                        Case "Sarcoma de Kaposi"
                            obs.concept_id = 507
                            antecedentes.Add(obs)
                        Case "Tosse prolongada"
                            obs.concept_id = 1429
                            antecedentes.Add(obs)
                        Case "Tuberculose Extra Pulmonar"
                            obs.concept_id = 5042
                            antecedentes.Add(obs)
                        Case "Tuberculose Pulmonar"
                            obs.concept_id = 42
                            antecedentes.Add(obs)
                        Case "Úlcera"
                            obs.concept_id = 1380
                            antecedentes.Add(obs)
                        Case "Malaria"
                            obs.concept_id = 123
                            antecedentes.Add(obs)
                        Case Else
                            If Not IsDBNull(rs.Fields.Item("observacao").Value) Then
                                Dim obsOther As New Obs
                                obsOther.concept_id = 1628
                                obsOther.data_Type = ObsDataType.TText
                                obsOther.value_text = rs.Fields.Item("observacao").Value
                                If Not IsDBNull(rs.Fields.Item("datadiagnostico").Value) Then
                                    obsOther.obs_datetime = rs.Fields.Item("datadiagnostico").Value
                                End If
                                antecedentes.Add(obsOther)
                            End If
                    End Select
                Else
                    'obs.concept_id = 1610
                    Select Case codAntecendente
                        Case "Candidíase > 1 episódio"
                            obs.concept_id = 204
                            antecedentes.Add(obs)
                        Case "Candidíase esofágica"
                            obs.concept_id = 5340
                            antecedentes.Add(obs)
                        Case "Candidiase oral"
                            obs.concept_id = 5334
                            antecedentes.Add(obs)
                        Case "Diarreia > 1 mês"
                            obs.concept_id = 5018
                            antecedentes.Add(obs)
                        Case "Falencia de crescimento"
                            obs.concept_id = 5050
                            antecedentes.Add(obs)
                        Case "Febre > 1 mês"
                            obs.concept_id = 5027
                            antecedentes.Add(obs)
                        Case "Infecções de repetição"
                            obs.concept_id = 5030
                            antecedentes.Add(obs)
                        Case "Pneumonias graves ou de repetição"
                            obs.concept_id = 1215
                            antecedentes.Add(obs)
                        Case "Tosse > 1 mês"
                            obs.concept_id = 1429
                            antecedentes.Add(obs)
                        Case "Tuberculose Tratada"
                            obs.concept_id = 1765
                            antecedentes.Add(obs)
                        Case Else
                            If Not IsDBNull(rs.Fields.Item("observacao").Value) Then
                                Dim obsOther As New Obs
                                obsOther.concept_id = 1628
                                obsOther.data_Type = ObsDataType.TText
                                obsOther.value_text = rs.Fields.Item("observacao").Value
                                If Not IsDBNull(rs.Fields.Item("datadiagnostico").Value) Then
                                    obsOther.obs_datetime = rs.Fields.Item("datadiagnostico").Value
                                End If
                                antecedentes.Add(obsOther)
                            End If
                    End Select

                End If
                'obs = New Obs
                rs.MoveNext()
            End While
        End If
        rs.Close()
        Return antecedentes
    End Function

    Public Shared Function importCirurgias(ByVal nidDoente As String) As ArrayList
        Dim Comando As New Command
        Dim rs As New Recordset
        Dim obs As New Obs
        Dim cirurgia As String

        'Dim tipoPaciente As String

        Dim cirurgias As New ArrayList

        'ADULTO CONCEPT SET ID 1688

        Comando.ActiveConnection = ICAPConection
        Comando.CommandType = CommandTypeEnum.adCmdText
        Comando.CommandText = "SELECT t_paciente.tipopaciente, t_cirurgia.nid, t_cirurgia.designacao " & _
                            " FROM t_paciente INNER JOIN t_cirurgia ON t_paciente.nid = t_cirurgia.nid " & _
                            " where t_cirurgia.nid = '" & nidDoente & "' and t_cirurgia.designacao is not null"


        rs = Comando.Execute
        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF
                cirurgia = PatientUtils.verificaNulo(rs, "designacao")
                obs.data_Type = ObsDataType.TText
                obs.concept_id = 1473
                obs.value_text = cirurgia
                obs.voided = False
                cirurgias.Add(obs)
                obs = New Obs
                rs.MoveNext()
            End While
        End If
        rs.Close()
        Return cirurgias
    End Function
    Public Shared Function importDataTransfusao(ByVal nidDoente As String, ByVal dataEnc As Date) As Date
        Dim Comando As New Command
        Dim rs As New Recordset
        Dim obs As New Obs
        Comando.ActiveConnection = ICAPConection
        Comando.CommandType = CommandTypeEnum.adCmdText
        Comando.CommandText = "SELECT t_transfusao.data " & _
                            " FROM t_transfusao " & _
                            " where t_transfusao.nid = '" & nidDoente & "'"


        rs = Comando.Execute
        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            If Not IsDBNull(rs.Fields.Item("data").Value) Then
                Return rs.Fields.Item("data").Value
            Else
                Return dataEnc
            End If

        Else
            Return dataEnc
        End If
    End Function
    Public Shared Function importExposicaoTarvMae(ByVal nidDoente As String) As ArrayList
        Dim Comando As New Command
        Dim rs As New Recordset
        Dim obs As New Obs
        Dim tarv As String

        'Dim tipoPaciente As String

        Dim tarves As New ArrayList

        'CONCEPT SET ID 1612

        Comando.ActiveConnection = ICAPConection
        Comando.CommandType = CommandTypeEnum.adCmdText
        Comando.CommandText = "SELECT t_esposicaotarvmae.tarv " & _
                            " FROM t_esposicaotarvmae " & _
                            " where t_esposicaotarvmae.nid = '" & nidDoente & "'"


        rs = Comando.Execute
        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF
                tarv = PatientUtils.verificaNulo(rs, "tarv")
                If Not String.IsNullOrEmpty(tarv) Then
                    tarv = tarv.ToUpper
                    obs.concept_id = 1504
                    obs.data_Type = ObsDataType.TCoded
                    Select Case tarv
                        Case "NVP"
                            obs.value_coded = 631
                            tarves.Add(obs)
                        Case "AZT+NVP", "NVP+AZT", "BITERAPIA", "BITERRAPIA"
                            obs.value_coded = 1801
                            tarves.Add(obs)
                        Case "TRITERRAPIA", "TRITERAPIA", "AZT+3TC+NVP"
                            obs.value_coded = 1651
                            tarves.Add(obs)
                        Case Else

                    End Select

                End If
                obs = New Obs
                rs.MoveNext()
            End While
        End If
        rs.Close()
        Return tarves
    End Function
    Public Shared Function importExposicaoTarvNascenca(ByVal nidDoente As String) As ArrayList
        Dim Comando As New Command
        Dim rs As New Recordset
        Dim obs As New Obs
        Dim tarv As String

        'Dim tipoPaciente As String

        Dim tarves As New ArrayList

        'CONCEPT SET ID 1612

        Comando.ActiveConnection = ICAPConection
        Comando.CommandType = CommandTypeEnum.adCmdText
        Comando.CommandText = "SELECT t_esposicaotarvnascenca.tarv " & _
                            " FROM t_esposicaotarvnascenca " & _
                            " where t_esposicaotarvnascenca.nid = '" & nidDoente & "'"


        rs = Comando.Execute
        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF
                tarv = PatientUtils.verificaNulo(rs, "tarv")
                If Not String.IsNullOrEmpty(tarv) Then
                    tarv = tarv.ToUpper
                    obs.concept_id = 1503
                    obs.data_Type = ObsDataType.TCoded
                    Select Case tarv
                        Case "NVP"
                            obs.value_coded = 631
                            tarves.Add(obs)
                        Case "AZT+NVP", "NVP+AZT", "BITERAPIA", "BITERRAPIA"
                            obs.value_coded = 1801
                            tarves.Add(obs)
                        Case "TRITERRAPIA", "TRITERAPIA", "AZT+3TC+NVP", "TRI."
                            obs.value_coded = 1651
                            tarves.Add(obs)
                        Case Else

                    End Select

                End If
                obs = New Obs
                rs.MoveNext()
            End While
        End If
        rs.Close()
        Return tarves
    End Function
    Public Shared Function importProcessoFilhos(ByVal nidDoente As String) As ArrayList
        Dim Comando As New Command
        Dim rs As New Recordset
        Dim obs As New Obs
        Dim nprocesso As String

        'Dim tipoPaciente As String

        Dim processos As New ArrayList

        'CONCEPT SET ID 1659

        Comando.ActiveConnection = ICAPConection
        Comando.CommandType = CommandTypeEnum.adCmdText
        Comando.CommandText = "SELECT t_filho.nrprocesso " & _
                            " FROM t_filho " & _
                            " where t_filho.nid = '" & nidDoente & "' and t_filho.nrprocesso is not null"


        rs = Comando.Execute
        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF
                nprocesso = PatientUtils.verificaNulo(rs, "nrprocesso")
                If Not String.IsNullOrEmpty(nprocesso) Then
                    obs.concept_id = 1454
                    obs.data_Type = ObsDataType.TText
                    obs.value_text = nprocesso
                    processos.Add(obs)
                End If
                obs = New Obs
                rs.MoveNext()
            End While
        End If
        rs.Close()
        Return processos
    End Function

    Public Shared Function importInternamento(ByVal nidDoente As String) As ArrayList
        Dim Comando As New Command
        Dim rs As New Recordset
        Dim obs As New Obs

        Dim enfermaria As String
        Dim diagnostico As String
        Dim tratamento As String
        'Dim observacao As String


        'Dim tipoPaciente As String

        Dim internamentos As New ArrayList

        'CONCEPT SET ID 1659

        Comando.ActiveConnection = ICAPConection
        Comando.CommandType = CommandTypeEnum.adCmdText
        Comando.CommandText = "SELECT data,enfermaria,diagnostico,tratamento,observacao " & _
                            " FROM t_internamento " & _
                            " where t_internamento.nid = '" & nidDoente & "'"


        rs = Comando.Execute
        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF
                enfermaria = PatientUtils.verificaNulo(rs, "enfermaria")
                diagnostico = PatientUtils.verificaNulo(rs, "diagnostico")
                tratamento = PatientUtils.verificaNulo(rs, "tratamento")
                'observacao = PatientUtils.verificaNulo(rs, "observacao")

                If Not String.IsNullOrEmpty(enfermaria) Then
                    obs = New Obs
                    obs.concept_id = 1468
                    obs.data_Type = ObsDataType.TText
                    obs.value_text = enfermaria
                    internamentos.Add(obs)
                End If

                If Not String.IsNullOrEmpty(diagnostico) Then
                    obs = New Obs
                    obs.concept_id = 1469
                    obs.data_Type = ObsDataType.TText
                    obs.value_text = diagnostico
                    internamentos.Add(obs)
                End If

                If Not String.IsNullOrEmpty(tratamento) Then
                    obs = New Obs
                    obs.concept_id = 1471
                    obs.data_Type = ObsDataType.TText
                    obs.value_text = tratamento
                    internamentos.Add(obs)
                End If
                If Not IsDBNull(rs.Fields.Item("data").Value) Then
                    obs = New Obs
                    obs.concept_id = 1470
                    obs.data_Type = ObsDataType.TDatetime
                    obs.value_datetime = rs.Fields.Item("data").Value
                    internamentos.Add(obs)
                End If

                rs.MoveNext()
            End While
        End If
        rs.Close()
        Return internamentos
    End Function

    Private Function getNivelCoded(ByVal nivel As String) As Integer
        nivel = nivel.ToUpper
        Select Case nivel
            Case "MÉDIO", "MEDIO", "NÍVEL MEDIO", "NÍVEL MÉDIO", "TECNICO PROFISSINAL"
                Return 1444
            Case "NAO SABE LER", "NÃO SABE LER", "NÃO ESTUDOU", "NAO ESTUDOU"
                Return 1445
            Case "NÍVEL PRIMÁRIO", "NIVEL PRIMARIO", "NÍVEL PRIMARIO", "NIVEL PRIMÁRIO", "ALFABETIZACAO", "PRIMÁRIO", "PRIMARIO"
                Return 1446
            Case "NÍVEL SECUNDÁRIO", "NIVEL SECUNDARIO", "NÍVEL SECUNDARIO", "NIVEL SECUNDÁRIO", "BASICO", "BÁSICO", "SECUNDÁRIO"
                Return 1447
            Case "NÍVEL UNIVERSITÁRIO", "NIVEL UNIVERSITARIO", "NÍVEL UNIVERSITARIO", "NIVEL UNIVERSITÁRIO", "NIVEL SUPERIOR", "NÍVEL SUPERIOR", "UNIVERSIDADE", "LECENCIADA", "LICENCIADO", "LICENCIADA", "LECENCIADO", "U"
                Return 1448
            Case Else
                Return 5622
        End Select
    End Function
    Private Function getEstadoCivil(ByVal estado As String) As Integer
        estado = estado.ToUpper
        Select Case estado
            Case "C", "c"
                Return 5555
            Case "S", "s"
                Return 1057
            Case "V", "v"
                Return 1059
            Case "U", "u"
                Return 1060
            Case "o", "O"
                Return 5622
        End Select
    End Function
    Private Function getTARVCoded(ByVal ptv As String) As Integer
        ptv = ptv.ToUpper
        Select Case ptv
            Case "NVP", "NEVIRAPINA", "TARV.NVP"
                Return 631
            Case "AZT"
                Return 797
            Case "3TC"
                Return 628
            Case "D4T"
                Return 625
            Case "BITERAPIA", "BITERRAPIA", "AZT+NVP", "NVP+AZT"
                Return 1801
            Case "TRITERAPIA", "TRITERRAPIA", "AZT+3TC+NVP", "TRI."
                Return 1651
            Case "TRIOMUNE", "TRIUMUNE", "D4T+3TC+NVP", "TRIOMUNE 30", "TRIUMUNE 30", "D4T30+3TC+NVP", "D4T30,3TC,NVP", "D4T40,3TC,NVP", "T 30", "T30"
                Return 792
            Case "PTV"
                Return 1598
            Case "TARV"
                Return 6276
            Case Else
                Return 817
        End Select

    End Function
    Private Function getSituacaoHIVCoded(ByVal situacao As String) As Integer
        situacao = situacao.ToUpper
        Select Case situacao
            Case "POSITIVO"
                Return 1169
            Case "NEGATIVO"
                Return 1066
            Case "INDETERMINADO"
                Return 1138
            Case Else
                Return 1457
        End Select
    End Function

    Private Function getSexualidade(ByVal sexo As String) As Integer
        sexo = sexo.ToUpper
        Select Case sexo
            Case "1"
                Return 1376
            Case "2"
                Return 1377
            Case "3"
                Return 1378
        End Select
    End Function
    Private Function getProvenienciaCoded(ByVal prov As String) As Integer
        prov = prov.ToUpper
        If prov.StartsWith("CS") Or prov.StartsWith("C.S") Or prov.StartsWith("C. S") Then
            Return 1275
        ElseIf prov.StartsWith("TRA") Or prov.StartsWith("TANS") Then
            Return 1369
        ElseIf prov.StartsWith("HP") Or prov.StartsWith("H.C") Then
            Return 1984
        Else
            Select Case prov
                Case "CLINICA MOVEL", "CLÍNICA MOVEL", "CLÍNICA MÓVEL", "C.M"
                    Return 1386
                Case "LABORATÓRIO", "LABORATORIO"
                    Return 1387
                Case "PNCTL", "PNCT"
                    Return 1414
                Case "ENF", "ENFERMARIA"
                    Return 1595
                Case "C.P", "CLINICA PRIVADA", "CP", "C.P.", "Clinica Boa Esperanca", "Clinica 1", "Renascer"
                    Return 1386
                Case "C.EXT"
                    Return 1596
                Case "ATS", "GAS", "GATV", "UATS"
                    Return 1597
                Case "PTV", "Consulta Pre-Natal"
                    Return 1598
                Case "CCR"
                    Return 1872
                Case "CONTACTO"
                    Return 1932
                Case "HG/HR"
                    Return 1984
                Case "2a SITIO", "2º SITIO", "SATÉLITE", "SATÉLITES", "SATELITE", "SATELITES"
                    Return 1986
                Case "SAAJ"
                    Return 1987
                Case "CIRCUCIOSAO MASCULINA", "CIRCUSCISÃO MASCULINA"
                    Return 408
                Case "ATSC"
                    Return 6245
                Case "Banco de Socorro", "Maternidade"
                    Return 6304
                Case "SMI"
                    Return 6288
                Case "Pediatria"
                    Return 1044
                Case "ITS"
                    Return 174
                Case "Planeamento Familiar"
                    Return 5483
                Case "CD"
                    Return 1699
                Case Else
                    Return 5622
            End Select
        End If
        
    End Function

    Private Function getTipoAleitamentoCoded(ByVal aleitamento As String) As Integer
        aleitamento = aleitamento.ToUpper
        Select Case aleitamento
            Case "MATERNO"
                Return 5526
            Case "ARTIFICIAL"
                Return 5254
            Case "MISTO"
                Return 6046
        End Select
    End Function
    Private Function getAlergiaMedicamentoCoded(ByVal alergia As String) As Integer
        alergia = alergia.ToUpper
        Select Case alergia
            Case "SIM"
                Return 1065
            Case "NAO", "NÃO"
                Return 1066
            Case "NÃO SABE", "NAO SABE"
                Return 1067
        End Select
    End Function
    Private Function getYesNoCirurgiaCoded(ByVal yesno As String) As Integer
        yesno = yesno.ToUpper
        Select Case yesno
            Case "-1"
                Return 1472
            Case "0"
                Return 1066
            Case "-99"
                Return 1457
        End Select
    End Function
    Private Function getYesNoExposicaoTarvCoded(ByVal yesno As String) As Integer
        yesno = yesno.ToUpper
        Select Case yesno
            Case "-1"
                Return 1065
            Case "0"
                Return 1066
            Case "-99"
                Return 1457
        End Select
    End Function
    Private Function getYesNoTransfusaoCoded(ByVal yesno As String) As Integer
        yesno = yesno.ToUpper
        Select Case yesno
            Case "-1"
                Return 1063
            Case "0"
                Return 1066
            Case "-99"
                Return 1457
        End Select
    End Function
    Private Function getYesNoAleitamentoCoded(ByVal yesno As String) As Integer
        yesno = yesno.ToUpper
        Select Case yesno
            Case "-1"
                Return 5526
            Case "0"
                Return 1066
            Case "-99"
                Return 1457
        End Select
    End Function


    Public Sub importAdulto(ByVal locationid As Int16)
        Dim Comando As New Command
        Dim patientID As Integer
        Dim encounterID As Integer
        Dim nid As String
        Dim rs As New Recordset
        Dim obs As New Obs
        Dim dataAbertura As Date
        Dim obsSet As Obs
        Dim obsGroupId As Integer
        Dim accessProviderID As String
        Dim openMRSProviderID As Int16

        Dim cmmDestino As New MySqlCommand
        cmmDestino.Connection = ConexaoOpenMRS3
        cmmDestino.CommandType = CommandType.Text
        'Dim observacao As String

        Dim dataArray As New ArrayList
        Dim temArray As New ArrayList
        'Dim tipoPaciente As String

        Dim internamentos As New ArrayList

        'CONCEPT SET ID 1659

        Comando.ActiveConnection = ICAPConection

        Comando.CommandType = CommandTypeEnum.adCmdText
        If AllPatients Then
            Comando.CommandText = "SELECT t_paciente.dataabertura, t_paciente.nid, t_paciente.codproveniencia, " & _
        "t_paciente.designacaoprov,t_paciente.emtarv, t_paciente.datainiciotarv, t_paciente.codregime, " & _
        "t_paciente.codfuncionario, t_paciente.datadiagnostico, t_paciente.aconselhado, " & _
        "t_paciente.tipopaciente, t_paciente.datasaidatarv, t_paciente.cirurgias, " & _
        "t_paciente.transfusao, t_paciente.codestado, t_paciente.referidocd, " & _
        "t_paciente.situacaohiv, t_paciente.estadiooms, t_paciente.emtratamentotb, " & _
        "t_paciente.observacao,t_paciente.CodUniSan,t_paciente.Codigoproveniencia, " & _
        "t_paciente.DataCD, t_paciente.numerotarv, t_paciente.referidohdd, " & _
        "t_paciente.datareferidohdd, t_paciente.aceitabuscaactiva, t_paciente.dataaceitabuscaactiva, " & _
        "t_paciente.referidobuscaactiva, t_paciente.datareferenciabuscaactiva, t_paciente.destinopaciente, " & _
        "t_paciente.Educacaoprevencao, t_paciente.nlivroPreTarv, t_paciente.paginaPreTarv, " & _
        "t_paciente.linhaPreTarv, t_paciente.dataPreTarv, t_paciente.nlivroTarv, " & _
        "t_paciente.paginaTarv, t_paciente.linhaTarv, t_paciente.dataTarv2, " & _
        "t_paciente.transfOutraUs, t_paciente.dataElegibilidadeInicioTarv, t_paciente.apssDisponivel, " & _
        "t_paciente.apssFormaContacto, t_paciente.apssQuemInformouSeroestado, t_paciente.apssconheceestadoparceiro, " & _
        "t_paciente.dataprevistainiciotarv, t_paciente.nie, " & _
        "t_adulto.codprofissao, t_adulto.codnivel, " & _
        "t_adulto.nrconviventes, t_adulto.codestadocivil, t_adulto.nrconjuges, " & _
        "t_adulto.serologiaHivconjuge, t_adulto.Nrprocesso, t_adulto.outrosparceiros, " & _
        "t_adulto.nrfilhos, t_adulto.nrfilhostestados, t_adulto.nrfilhoshiv, " & _
        "t_adulto.tabaco, t_adulto.alcool, t_adulto.droga, " & _
        "t_adulto.nrparceiros, t_adulto.antecedentesgenelogicos, t_adulto.datamestruacao, " & _
        "t_adulto.aborto, t_adulto.ptv, t_adulto.ptvquais, " & _
        "t_adulto.gravida, t_adulto.semanagravidez, t_adulto.dataprevistoparto, " & _
        "t_adulto.puerpera, t_adulto.dataparto, t_adulto.tipoaleitamento, " & _
        "t_adulto.Alergiamedicamentos, t_adulto.Alergiasquais, t_adulto.Antecedentestarv, " & _
        "t_adulto.antecedentesquais, t_adulto.exposicaoacidental, t_adulto.tipoacidente, " & _
        "t_adulto.historiaactual, t_adulto.hipotesedediagnostico, t_adulto.codkarnosfsky, " & _
        "t_adulto.geleira, t_adulto.electricidade, t_adulto.sexualidade,recebeSms,aceitaSerContatado,idseguimento " & _
        "FROM t_paciente LEFT JOIN t_adulto ON t_paciente.nid = t_adulto.nid " & _
        "WHERE (((t_paciente.tipopaciente)='Adulto' or t_paciente.tipopaciente is null));"
        Else
            Comando.CommandText = "SELECT t_paciente.dataabertura, t_paciente.nid, t_paciente.codproveniencia, " & _
       "t_paciente.designacaoprov,t_paciente.emtarv, t_paciente.datainiciotarv, t_paciente.codregime, " & _
       "t_paciente.codfuncionario, t_paciente.datadiagnostico, t_paciente.aconselhado, " & _
       "t_paciente.tipopaciente, t_paciente.datasaidatarv, t_paciente.cirurgias, " & _
       "t_paciente.transfusao, t_paciente.codestado, t_paciente.referidocd, " & _
       "t_paciente.situacaohiv, t_paciente.estadiooms, t_paciente.emtratamentotb, " & _
       "t_paciente.observacao,t_paciente.CodUniSan,t_paciente.Codigoproveniencia, " & _
       "t_paciente.DataCD, t_paciente.numerotarv, t_paciente.referidohdd, " & _
       "t_paciente.datareferidohdd, t_paciente.aceitabuscaactiva, t_paciente.dataaceitabuscaactiva, " & _
       "t_paciente.referidobuscaactiva, t_paciente.datareferenciabuscaactiva, t_paciente.destinopaciente, " & _
       "t_paciente.Educacaoprevencao, t_paciente.nlivroPreTarv, t_paciente.paginaPreTarv, " & _
       "t_paciente.linhaPreTarv, t_paciente.dataPreTarv, t_paciente.nlivroTarv, " & _
       "t_paciente.paginaTarv, t_paciente.linhaTarv, t_paciente.dataTarv2, " & _
       "t_paciente.transfOutraUs, t_paciente.dataElegibilidadeInicioTarv, t_paciente.apssDisponivel, " & _
       "t_paciente.apssFormaContacto, t_paciente.apssQuemInformouSeroestado, t_paciente.apssconheceestadoparceiro, " & _
       "t_paciente.dataprevistainiciotarv, t_paciente.nie, " & _
       "t_adulto.codprofissao, t_adulto.codnivel, " & _
       "t_adulto.nrconviventes, t_adulto.codestadocivil, t_adulto.nrconjuges, " & _
       "t_adulto.serologiaHivconjuge, t_adulto.Nrprocesso, t_adulto.outrosparceiros, " & _
       "t_adulto.nrfilhos, t_adulto.nrfilhostestados, t_adulto.nrfilhoshiv, " & _
       "t_adulto.tabaco, t_adulto.alcool, t_adulto.droga, " & _
       "t_adulto.nrparceiros, t_adulto.antecedentesgenelogicos, t_adulto.datamestruacao, " & _
       "t_adulto.aborto, t_adulto.ptv, t_adulto.ptvquais, " & _
       "t_adulto.gravida, t_adulto.semanagravidez, t_adulto.dataprevistoparto, " & _
       "t_adulto.puerpera, t_adulto.dataparto, t_adulto.tipoaleitamento, " & _
       "t_adulto.Alergiamedicamentos, t_adulto.Alergiasquais, t_adulto.Antecedentestarv, " & _
       "t_adulto.antecedentesquais, t_adulto.exposicaoacidental, t_adulto.tipoacidente, " & _
       "t_adulto.historiaactual, t_adulto.hipotesedediagnostico, t_adulto.codkarnosfsky, " & _
       "t_adulto.geleira, t_adulto.electricidade, t_adulto.sexualidade,t_adulto.recebeSms,t_adulto.aceitaSerContatado,idseguimento " & _
       "FROM t_paciente LEFT JOIN t_adulto ON t_paciente.nid = t_adulto.nid " & _
       "WHERE (((t_paciente.tipopaciente)='Adulto' or t_paciente.tipopaciente is null)) and t_paciente.nid in (" & whereQuery & ");"
           
        End If
        
        rs = Comando.Execute
        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF
                nid = PatientUtils.verificaNulo(rs, "nid")

                accessProviderID = PatientUtils.verificaNulo(rs, "codfuncionario")

                patientID = GetPatientOpenMRSIDByNID(nid)
                If patientID > 0 Then

                    If Not String.IsNullOrEmpty(accessProviderID) Then
                        openMRSProviderID = GetOpenMRSProvider(accessProviderID, locationid)
                    Else
                        openMRSProviderID = 27
                    End If
                    dataAbertura = rs.Fields.Item("dataabertura").Value

                    ' cmmDestino.CommandText = "Insert into encounter(encounter_type,patient_id,provider_id,location_id," & _
                    '                            "form_id,encounter_datetime,creator,date_created,voided,uuid) values(5," & patientID & "," & openMRSProviderID & "," & locationid & "," & _
                    '                           "99,'" & dataMySQL(dataAbertura) & "',22,now(),0,uuid())"
                    'cmmDestino.ExecuteNonQuery()
                    '
                    'cmmDestino.CommandText = "Select max(encounter_id) from encounter"
                    'encounterID = cmmDestino.ExecuteScalar

                    encounterID = EncounterDAO.insertEncounterByParam(5, patientID, locationid, 99, dataAbertura, 12, openMRSProviderID)

                    obsSet = New Obs

                    obsSet.location_id = locationid
                    obsSet.person_id = patientID
                    obsSet.date_created = Now
                    obsSet.voided = 0
                    obsSet.encounter_id = encounterID
                    obsSet.obs_datetime = dataAbertura

                    dataArray = importPessoaReferencia(nid)

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1609
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            o.encounter_id = encounterID
                            o.location_id = locationid
                            o.person_id = patientID
                            o.obs_datetime = dataAbertura
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                    dataArray.Clear()
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "codprofissao")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TText
                        obs.concept_id = 1459
                        obs.value_text = rs.Fields.Item("codprofissao").Value
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "codnivel")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1443
                        obs.value_coded = getNivelCoded(rs.Fields.Item("codnivel").Value)
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "nrconviventes")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TNumeric
                        obs.concept_id = 1656
                        Try
                            obs.value_numeric = rs.Fields.Item("nrconviventes").Value
                        Catch ex As Exception
                            obs.value_numeric = 0
                        End Try

                        dataArray.Add(obs)
                    End If

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1657
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                    dataArray.Clear()

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "codestadocivil")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1054
                        obs.value_coded = getEstadoCivil(rs.Fields.Item("codestadocivil").Value)
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "nrconjuges")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TNumeric
                        obs.concept_id = 5557
                        Try
                            obs.value_numeric = rs.Fields.Item("nrconjuges").Value
                        Catch ex As Exception
                            obs.value_numeric = 0
                        End Try
                        dataArray.Add(obs)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "serologiaHivconjuge")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1449
                        If rs.Fields.Item("serologiaHivconjuge").Value = "Positivo" Or rs.Fields.Item("serologiaHivconjuge").Value = "Posetivo" Then
                            obs.value_coded = 1169
                        ElseIf rs.Fields.Item("serologiaHivconjuge").Value = "Negativo" Then
                            obs.value_coded = 1066
                        Else
                            obs.value_coded = 1457
                        End If
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "Nrprocesso")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TText
                        obs.concept_id = 1450
                        obs.value_text = rs.Fields.Item("Nrprocesso").Value
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "outrosparceiros")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TText
                        obs.concept_id = 1451
                        obs.value_text = rs.Fields.Item("outrosparceiros").Value
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "nrfilhos")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TNumeric
                        obs.concept_id = 5573

                        Try
                            obs.value_numeric = rs.Fields.Item("nrfilhos").Value
                        Catch ex As Exception
                            obs.value_numeric = 0
                        End Try

                        dataArray.Add(obs)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "nrfilhostestados")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TNumeric
                        obs.concept_id = 1452
                        obs.value_numeric = rs.Fields.Item("nrfilhostestados").Value
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "nrfilhoshiv")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TNumeric
                        obs.concept_id = 1453
                        obs.value_numeric = rs.Fields.Item("nrfilhoshiv").Value
                        dataArray.Add(obs)
                    End If
                    temArray = importProcessoFilhos(nid)
                    If temArray.Count > 0 Then
                        For Each obs1 As Obs In temArray
                            obs1.location_id = locationid
                            obs1.person_id = patientID
                            obs1.date_created = Now
                            obs1.encounter_id = encounterID
                            obs1.obs_datetime = dataAbertura
                            dataArray.Add(obs1)
                        Next
                    End If
                    temArray.Clear()
                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1659
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If

                    dataArray.Clear()

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "geleira")) Then
                        If rs.Fields.Item("geleira").Value Then
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = encounterID
                            obs.obs_datetime = dataAbertura
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 1455
                            obs.value_coded = 1065
                            dataArray.Add(obs)
                        End If

                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "electricidade")) Then
                        If rs.Fields.Item("electricidade").Value Then
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = encounterID
                            obs.obs_datetime = dataAbertura
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 5609
                            obs.value_coded = 1065
                            dataArray.Add(obs)
                        End If

                    End If

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1660
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If

                    dataArray.Clear()

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "codproveniencia")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1594
                        obs.value_coded = getProvenienciaCoded(rs.Fields.Item("codproveniencia").Value)
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "designacaoprov")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TText
                        obs.concept_id = 1626
                        obs.value_text = rs.Fields.Item("designacaoprov").Value
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "Codigoproveniencia")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TText
                        obs.concept_id = 1627
                        obs.value_text = rs.Fields.Item("Codigoproveniencia").Value
                        dataArray.Add(obs)
                    End If

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1625
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If

                    dataArray.Clear()


                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "situacaohiv")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = IIf(Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "datadiagnostico")), rs.Fields.Item("datadiagnostico").Value, dataAbertura)
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1634
                        obs.value_coded = getSituacaoHIVCoded(rs.Fields.Item("situacaohiv").Value)
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "datadiagnostico")) Then
                        obs = New Obs

                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = rs.Fields.Item("datadiagnostico").Value
                        obs.data_Type = ObsDataType.TDatetime
                        obs.concept_id = 6123
                        obs.value_datetime = rs.Fields.Item("datadiagnostico").Value

                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "aconselhado")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1463
                        If rs.Fields.Item("aconselhado").Value Then
                            obs.value_coded = 1065
                        Else
                            obs.value_coded = 1066
                        End If
                        dataArray.Add(obs)
                    End If

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1661
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If

                    dataArray.Clear()
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "tabaco")) Then


                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1388
                        If rs.Fields.Item("tabaco").Value Then
                            obs.value_coded = 1065
                        Else
                            obs.value_coded = 1066
                        End If

                        dataArray.Add(obs)

                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "alcool")) Then

                        If rs.Fields.Item("alcool").Value Then
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = encounterID
                            obs.obs_datetime = dataAbertura
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 1603

                            If rs.Fields.Item("alcool").Value Then
                                obs.value_coded = 1065
                            Else
                                obs.value_coded = 1066
                            End If

                            dataArray.Add(obs)
                        End If
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "droga")) Then
                        If rs.Fields.Item("droga").Value Then
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = encounterID
                            obs.obs_datetime = dataAbertura
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 105

                            If rs.Fields.Item("droga").Value Then
                                obs.value_coded = 1065
                            Else
                                obs.value_coded = 1066
                            End If

                            dataArray.Add(obs)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "sexualidade")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1375
                        obs.value_coded = getSexualidade(rs.Fields.Item("sexualidade").Value)
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "nrparceiros")) Then
                        obs = New Obs
                        Dim parceir As Int16 = rs.Fields.Item("nrparceiros").Value
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1666
                        If parceir = 0 Then
                            obs.value_coded = 1665
                        ElseIf parceir = 1 Then
                            obs.value_coded = 1662
                        ElseIf parceir <= 3 Then
                            obs.value_coded = 1663
                        Else
                            obs.value_coded = 1664
                        End If
                        dataArray.Add(obs)
                    End If

                    If dataArray.Count > 0 Then
                        For Each o As Obs In dataArray
                            ObsDAO.insertObs(o, False)
                        Next
                    End If

                    dataArray.Clear()

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "antecedentesgenelogicos")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TText
                        obs.concept_id = 1394
                        obs.value_text = rs.Fields.Item("antecedentesgenelogicos").Value
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "datamestruacao")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TDatetime
                        obs.concept_id = 1465
                        obs.value_datetime = rs.Fields.Item("datamestruacao").Value
                        dataArray.Add(obs)
                    End If

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1393
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                    dataArray.Clear()
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "aborto")) Then


                        If rs.Fields.Item("aborto").Value Then
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = encounterID
                            obs.obs_datetime = dataAbertura
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 1667
                            obs.value_coded = 50
                            dataArray.Add(obs)
                        End If
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "ptv")) Then

                        If rs.Fields.Item("ptv").Value Then
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = encounterID
                            obs.obs_datetime = dataAbertura
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 1466

                            obs.value_coded = 1065
                        End If


                        dataArray.Add(obs)

                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "ptvquais")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1504
                        obs.value_coded = getTARVCoded(rs.Fields.Item("ptvquais").Value)
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "gravida")) Then
                        If rs.Fields.Item("gravida").Value Then
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = encounterID
                            obs.obs_datetime = dataAbertura
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 1982
                            obs.value_coded = 44
                            dataArray.Add(obs)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "semanagravidez")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TNumeric
                        obs.concept_id = 1279
                        obs.value_numeric = rs.Fields.Item("semanagravidez").Value
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "dataprevistoparto")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TDatetime
                        obs.concept_id = 1600
                        obs.value_datetime = rs.Fields.Item("dataprevistoparto").Value
                        dataArray.Add(obs)
                    End If
                    'FALTA IMPLEMENTAR ISTO
                    'If rs.Fields.Item("puerpera").Value Then
                    '    obs = New Obs
                    '    obs.location_id = locationid
                    '    obs.person_id = patientID
                    '    obs.date_created = Now
                    '    obs.voided = 0
                    '    obs.encounter_id = encounterID
                    '    obs.obs_datetime = dataAbertura
                    '    obs.data_Type = ObsDataType.TCoded
                    '    obs.concept_id = 1982
                    '    obs.value_coded = 44
                    '    dataArray.Add(obs)
                    'End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "dataparto")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TDatetime
                        obs.concept_id = 5599
                        obs.value_datetime = rs.Fields.Item("dataparto").Value
                        dataArray.Add(obs)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "tipoaleitamento")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1151
                        obs.value_coded = getTipoAleitamentoCoded(rs.Fields.Item("tipoaleitamento").Value)
                        dataArray.Add(obs)
                    End If

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1668
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                    dataArray.Clear()



                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "Alergiamedicamentos")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1601
                        obs.value_coded = getAlergiaMedicamentoCoded(rs.Fields.Item("Alergiamedicamentos").Value)
                        dataArray.Add(obs)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "Alergiasquais")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TText
                        obs.concept_id = 1517
                        obs.value_text = rs.Fields.Item("Alergiasquais").Value
                        dataArray.Add(obs)
                    End If

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1669
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                    dataArray.Clear()
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "Antecedentestarv")) Then

                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1192
                        If rs.Fields.Item("Antecedentestarv").Value Then
                            obs.value_coded = 1065
                        Else
                            obs.value_coded = 1066
                        End If

                        ObsDAO.insertObs(obs, False)

                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "antecedentesquais")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1087
                        obs.value_coded = getTARVCoded(rs.Fields.Item("antecedentesquais").Value)
                        ObsDAO.insertObs(obs, False)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "exposicaoacidental")) Then
                        If rs.Fields.Item("exposicaoacidental").Value Then
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = encounterID
                            obs.obs_datetime = dataAbertura
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 1687
                            obs.value_coded = 1433
                            dataArray.Add(obs)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "tipoacidente")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TText
                        obs.concept_id = 1435
                        obs.value_text = rs.Fields.Item("tipoacidente").Value
                        dataArray.Add(obs)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "cirurgias")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1685
                        obs.value_coded = getYesNoCirurgiaCoded(rs.Fields.Item("cirurgias").Value)
                        dataArray.Add(obs)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "codkarnosfsky")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TNumeric
                        obs.concept_id = 5283
                        obs.value_numeric = rs.Fields.Item("codkarnosfsky").Value
                        dataArray.Add(obs)
                    End If

                    temArray = importCirurgias(nid)

                    If temArray.Count > 0 Then
                        For Each obs1 As Obs In temArray
                            obs1.location_id = locationid
                            obs1.person_id = patientID
                            obs1.date_created = Now
                            obs1.encounter_id = encounterID
                            obs1.obs_datetime = dataAbertura
                            dataArray.Add(obs1)
                        Next
                    End If

                    temArray.Clear()

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "transfusao")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = importDataTransfusao(nid, dataAbertura)
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1686
                        obs.value_coded = getYesNoTransfusaoCoded(rs.Fields.Item("transfusao").Value)
                        dataArray.Add(obs)
                    End If

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1688
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                    dataArray.Clear()

                    temArray = importAntecedentesClinicos(nid)
                    If temArray.Count > 0 Then
                        For Each obs1 As Obs In temArray
                            obs1.location_id = locationid
                            obs1.person_id = patientID
                            obs1.date_created = Now
                            obs1.encounter_id = encounterID
                            If obs1.obs_datetime = Nothing Then
                                obs1.obs_datetime = dataAbertura
                            End If
                            dataArray.Add(obs1)
                        Next
                    End If


                    If dataArray.Count > 0 Then
                        'obsSet.concept_id = 1688
                        'obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            'o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, False)
                        Next
                    End If
                    temArray.Clear()
                    dataArray.Clear()

                    temArray = importInternamento(nid)
                    If temArray.Count > 0 Then
                        For Each obs1 As Obs In temArray
                            obs1.location_id = locationid
                            obs1.person_id = patientID
                            obs1.date_created = Now
                            obs1.encounter_id = encounterID
                            obs1.obs_datetime = dataAbertura
                            dataArray.Add(obs1)
                        Next
                    End If


                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1606
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                End If
                temArray.Clear()
                dataArray.Clear()

                If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "nlivroPreTarv")) Then

                    Dim nLivro As Integer = rs.Fields.Item("nlivroPreTarv").Value

                    If nLivro <= 2 Then
                        If (Not IsDBNull(rs.Fields.Item("paginaPreTarv").Value)) And (Not IsDBNull(rs.Fields.Item("linhaPreTarv").Value)) And (Not IsDBNull(rs.Fields.Item("dataPreTarv").Value)) Then


                            Dim nPagina As Integer = rs.Fields.Item("paginaPreTarv").Value
                            Dim nLinha As Integer = rs.Fields.Item("linhaPreTarv").Value
                            Dim dataLivro As Date = rs.Fields.Item("dataPreTarv").Value

                            Dim livroTARVEncounterID As Integer = EncounterDAO.insertEncounterByParam(32, patientID, locationid, 128, dataLivro, 14, openMRSProviderID)

                            'Livro
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = livroTARVEncounterID
                            obs.obs_datetime = dataLivro
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6263
                            obs.value_coded = IIf(nLivro = 1, 6259, 6260)
                            dataArray.Add(obs)

                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = livroTARVEncounterID
                            obs.obs_datetime = dataLivro
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 6265
                            obs.value_numeric = nPagina
                            dataArray.Add(obs)

                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = livroTARVEncounterID
                            obs.obs_datetime = dataLivro
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 6267
                            obs.value_numeric = nLinha
                            dataArray.Add(obs)

                            For Each o As Obs In dataArray
                                ObsDAO.insertObs(o, False)
                            Next
                        End If
                    End If
                End If


                If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "nlivroTarv")) Then

                    Dim nLivro As Integer = rs.Fields.Item("nlivroTarv").Value

                    If nLivro <= 2 Then

                        If (Not IsDBNull(rs.Fields.Item("paginaTarv").Value)) And (Not IsDBNull(rs.Fields.Item("linhaTarv").Value)) And (Not IsDBNull(rs.Fields.Item("dataTarv2").Value)) Then


                            Dim nPagina As Integer = rs.Fields.Item("paginaTarv").Value
                            Dim nLinha As Integer = rs.Fields.Item("linhaTarv").Value
                            Dim dataLivro As Date = rs.Fields.Item("dataTarv2").Value

                            Dim livroTARVEncounterID As Integer = EncounterDAO.insertEncounterByParam(33, patientID, locationid, 129, dataLivro, 14, openMRSProviderID)

                            'Livro
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = livroTARVEncounterID
                            obs.obs_datetime = dataLivro
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6264
                            obs.value_coded = IIf(nLivro = 1, 6261, 6262)
                            dataArray.Add(obs)

                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = livroTARVEncounterID
                            obs.obs_datetime = dataLivro
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 6266
                            obs.value_numeric = nPagina
                            dataArray.Add(obs)

                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = livroTARVEncounterID
                            obs.obs_datetime = dataLivro
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 6268
                            obs.value_numeric = nLinha
                            dataArray.Add(obs)

                            For Each o As Obs In dataArray
                                ObsDAO.insertObs(o, False)
                            Next
                        End If
                    End If
                End If



                ProcessoParteB.importProcessoBAdulto(nid, locationid, patientID)
                rs.MoveNext()
            End While
        End If
        rs.Close()
    End Sub

    Public Sub importCrianca(ByVal locationid As Int16)
        Dim Comando As New Command
        Dim patientID As Integer
        Dim encounterID As Integer
        Dim nid As String
        Dim rs As New Recordset
        Dim obs As New Obs
        Dim dataAbertura As Date
        Dim obsSet As Obs
        Dim obsGroupId As Integer

        Dim cmmDestino As New MySqlCommand
        cmmDestino.Connection = ConexaoOpenMRS3
        cmmDestino.CommandType = CommandType.Text
        'Dim observacao As String

        Dim dataArray As New ArrayList
        Dim temArray As New ArrayList
        'Dim tipoPaciente As String

        Dim internamentos As New ArrayList
        Dim accessProviderID As String
        Dim openMRSProviderID As Int16

        'CONCEPT SET ID 1659

        Comando.ActiveConnection = ICAPConection

        Comando.CommandType = CommandTypeEnum.adCmdText
        If AllPatients Then
            Comando.CommandText = "SELECT t_paciente.dataabertura, t_paciente.nid, t_paciente.codproveniencia, " & _
        "t_paciente.designacaoprov,t_paciente.emtarv, t_paciente.datainiciotarv, t_paciente.codregime, " & _
        "t_paciente.codfuncionario, t_paciente.datadiagnostico, t_paciente.aconselhado, " & _
        "t_paciente.tipopaciente, t_paciente.datasaidatarv, t_paciente.cirurgias, " & _
        "t_paciente.transfusao, t_paciente.codestado, t_paciente.referidocd, " & _
        "t_paciente.situacaohiv, t_paciente.estadiooms, t_paciente.emtratamentotb, " & _
        "t_paciente.observacao,t_paciente.CodUniSan,t_paciente.Codigoproveniencia, " & _
        "t_paciente.DataCD, t_paciente.numerotarv, t_paciente.referidohdd, " & _
        "t_paciente.datareferidohdd, t_paciente.aceitabuscaactiva, t_paciente.dataaceitabuscaactiva, " & _
        "t_paciente.referidobuscaactiva, t_paciente.datareferenciabuscaactiva, t_paciente.destinopaciente, " & _
        "t_paciente.Educacaoprevencao, t_paciente.nlivroPreTarv, t_paciente.paginaPreTarv, " & _
        "t_paciente.linhaPreTarv, t_paciente.dataPreTarv, t_paciente.nlivroTarv, " & _
        "t_paciente.paginaTarv, t_paciente.linhaTarv, t_paciente.dataTarv2, " & _
        "t_paciente.transfOutraUs, t_paciente.dataElegibilidadeInicioTarv, t_paciente.apssDisponivel, " & _
        "t_paciente.apssFormaContacto, t_paciente.apssQuemInformouSeroestado, t_paciente.apssconheceestadoparceiro, " & _
        "t_paciente.dataprevistainiciotarv, t_paciente.nie, " & _
        "t_crianca.tipoparto, t_crianca.[local] as localnasc, " & _
        "t_crianca.termo, t_crianca.pesonascimento, t_crianca.exposicaotarvmae, " & _
        "t_crianca.exposicaotarvnascenca, t_crianca.patologianeonatal, t_crianca.injeccoes, " & _
        "t_crianca.escarificacoes, t_crianca.extracoesdentarias, t_crianca.aleitamentomaterno, " & _
        "t_crianca.aleitamentoexclusivo, t_crianca.idadedesmame, t_crianca.pavcompleto, " & _
        "t_crianca.idadecronologica, t_crianca.bailey, t_crianca.idmae, " & _
        "t_crianca.idpai, t_crianca.observacao, t_crianca.recebeSmsCrianca,t_crianca.telefoneCrianca " & _
        " FROM t_paciente LEFT JOIN t_crianca ON t_paciente.nid = t_crianca.nid " & _
        " WHERE (((t_paciente.tipopaciente)='Crianca' or (t_paciente.tipopaciente)='Criança'));"
        Else
            Comando.CommandText = "SELECT t_paciente.dataabertura, t_paciente.nid, t_paciente.codproveniencia, " & _
        "t_paciente.designacaoprov,t_paciente.emtarv, t_paciente.datainiciotarv, t_paciente.codregime, " & _
        "t_paciente.codfuncionario, t_paciente.datadiagnostico, t_paciente.aconselhado, " & _
        "t_paciente.tipopaciente, t_paciente.datasaidatarv, t_paciente.cirurgias, " & _
        "t_paciente.transfusao, t_paciente.codestado, t_paciente.referidocd, " & _
        "t_paciente.situacaohiv, t_paciente.estadiooms, t_paciente.emtratamentotb, " & _
        "t_paciente.observacao,t_paciente.CodUniSan,t_paciente.Codigoproveniencia, " & _
        "t_paciente.DataCD, t_paciente.numerotarv, t_paciente.referidohdd, " & _
        "t_paciente.datareferidohdd, t_paciente.aceitabuscaactiva, t_paciente.dataaceitabuscaactiva, " & _
        "t_paciente.referidobuscaactiva, t_paciente.datareferenciabuscaactiva, t_paciente.destinopaciente, " & _
        "t_paciente.Educacaoprevencao, t_paciente.nlivroPreTarv, t_paciente.paginaPreTarv, " & _
        "t_paciente.linhaPreTarv, t_paciente.dataPreTarv, t_paciente.nlivroTarv, " & _
        "t_paciente.paginaTarv, t_paciente.linhaTarv, t_paciente.dataTarv2, " & _
        "t_paciente.transfOutraUs, t_paciente.dataElegibilidadeInicioTarv, t_paciente.apssDisponivel, " & _
        "t_paciente.apssFormaContacto, t_paciente.apssQuemInformouSeroestado, t_paciente.apssconheceestadoparceiro, " & _
        "t_paciente.dataprevistainiciotarv, t_paciente.nie, " & _
        "t_crianca.tipoparto, t_crianca.[local] as localnasc, " & _
        "t_crianca.termo, t_crianca.pesonascimento, t_crianca.exposicaotarvmae, " & _
        "t_crianca.exposicaotarvnascenca, t_crianca.patologianeonatal, t_crianca.injeccoes, " & _
        "t_crianca.escarificacoes, t_crianca.extracoesdentarias, t_crianca.aleitamentomaterno, " & _
        "t_crianca.aleitamentoexclusivo, t_crianca.idadedesmame, t_crianca.pavcompleto, " & _
        "t_crianca.idadecronologica, t_crianca.bailey, t_crianca.idmae, " & _
        "t_crianca.idpai, t_crianca.observacao, t_crianca.recebeSmsCrianca,t_crianca.telefoneCrianca " & _
        " FROM t_paciente LEFT JOIN t_crianca ON t_paciente.nid = t_crianca.nid " & _
        " WHERE (((t_paciente.tipopaciente)='Crianca' or (t_paciente.tipopaciente)='Criança')) and t_paciente.nid in (" & whereQuery & ");"
        End If

        rs = Comando.Execute

        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF
                nid = PatientUtils.verificaNulo(rs, "nid")

                patientID = GetPatientOpenMRSIDByNID(nid)
                accessProviderID = PatientUtils.verificaNulo(rs, "codfuncionario")
                If patientID > 0 Then

                    If Not String.IsNullOrEmpty(accessProviderID) Then
                        openMRSProviderID = GetOpenMRSProvider(accessProviderID, locationid)
                    Else
                        openMRSProviderID = 27
                    End If

                    dataAbertura = rs.Fields.Item("dataabertura").Value
                   

                    encounterID = EncounterDAO.insertEncounterByParam(7, patientID, locationid, 108, dataAbertura, 12, openMRSProviderID)

                    obsSet = New Obs

                    obsSet.location_id = locationid
                    obsSet.person_id = patientID
                    obsSet.date_created = Now
                    obsSet.voided = 0
                    obsSet.encounter_id = encounterID
                    obsSet.obs_datetime = dataAbertura

                    dataArray = importPessoaReferencia(nid)

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1609
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            o.encounter_id = encounterID
                            o.location_id = locationid
                            o.person_id = patientID
                            o.obs_datetime = dataAbertura
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                    dataArray.Clear()
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "idmae")) Then
                        dataArray = importMae(rs.Fields.Item("idmae").Value)
                        If dataArray.Count > 0 Then
                            obsSet.concept_id = 1607
                            obsGroupId = ObsDAO.insertSet(obsSet)
                            For Each o As Obs In dataArray
                                o.obs_group_id = obsGroupId
                                o.encounter_id = encounterID
                                o.location_id = locationid
                                o.person_id = patientID
                                o.obs_datetime = dataAbertura
                                ObsDAO.insertObs(o, True)
                            Next
                        End If
                    End If
                    dataArray.Clear()
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "idpai")) Then
                        dataArray = importPai(rs.Fields.Item("idpai").Value)
                        If dataArray.Count > 0 Then
                            obsSet.concept_id = 1608
                            obsGroupId = ObsDAO.insertSet(obsSet)
                            For Each o As Obs In dataArray
                                o.obs_group_id = obsGroupId
                                o.encounter_id = encounterID
                                o.location_id = locationid
                                o.person_id = patientID
                                o.obs_datetime = dataAbertura
                                ObsDAO.insertObs(o, True)
                            Next
                        End If
                    End If
                    dataArray.Clear()

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "codproveniencia")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1594
                        obs.value_coded = getProvenienciaCoded(rs.Fields.Item("codproveniencia").Value)
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "designacaoprov")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TText
                        obs.concept_id = 1626
                        obs.value_text = rs.Fields.Item("designacaoprov").Value
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "Codigoproveniencia")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TText
                        obs.concept_id = 1627
                        obs.value_text = rs.Fields.Item("Codigoproveniencia").Value
                        dataArray.Add(obs)
                    End If

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1625
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                    dataArray.Clear()
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "situacaohiv")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = IIf(Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "datadiagnostico")), rs.Fields.Item("datadiagnostico").Value, dataAbertura)
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1634
                        obs.value_coded = getSituacaoHIVCoded(rs.Fields.Item("situacaohiv").Value)
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "aconselhado")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1463
                        If rs.Fields.Item("aconselhado").Value Then
                            obs.value_coded = 1065
                        Else
                            obs.value_coded = 1066
                        End If
                        dataArray.Add(obs)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "datadiagnostico")) Then
                        obs = New Obs

                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = rs.Fields.Item("datadiagnostico").Value
                        obs.data_Type = ObsDataType.TDatetime
                        obs.concept_id = 6123
                        obs.value_datetime = rs.Fields.Item("datadiagnostico").Value

                        dataArray.Add(obs)
                    End If

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1661
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If

                    dataArray.Clear()

                    temArray = importAntecedentesClinicos(nid)
                    If temArray.Count > 0 Then
                        For Each obs1 As Obs In temArray
                            obs1.location_id = locationid
                            obs1.person_id = patientID
                            obs1.date_created = Now
                            obs1.encounter_id = encounterID
                            If obs1.obs_datetime = Nothing Then
                                obs1.obs_datetime = dataAbertura
                            End If
                            dataArray.Add(obs1)
                        Next
                    End If

                    If dataArray.Count > 0 Then
                        For Each o As Obs In dataArray
                            ObsDAO.insertObs(o, False)
                        Next
                    End If
                    dataArray.Clear()
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "tipoparto")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 5630
                        If rs.Fields.Item("tipoparto").Value = "Vaginal" Or rs.Fields.Item("tipoparto").Value = "VAGINAL" Then
                            obs.value_coded = 1170
                        Else
                            obs.value_coded = 1171
                        End If
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "localnasc")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TText
                        obs.concept_id = 1505
                        obs.value_text = rs.Fields.Item("localnasc").Value
                        'dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "termo")) Then

                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1500
                        If rs.Fields.Item("termo").Value = "-1" Then
                            obs.value_coded = 1065
                        ElseIf rs.Fields.Item("termo").Value = "0" Then
                            obs.value_coded = 1066
                        Else
                            obs.value_coded = 1457
                        End If

                        dataArray.Add(obs)


                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "pesonascimento")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TNumeric
                        obs.concept_id = 5916
                        Try
                            'Verificar a conversao de dados antes de inserir
                            Dim valorPeso As String = rs.Fields.Item("pesonascimento").Value
                            valorPeso = valorPeso.Replace(",", ".")
                            obs.value_numeric = valorPeso
                        Catch ex As Exception
                            obs.value_numeric = 0
                        End Try

                        dataArray.Add(obs)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "exposicaotarvmae")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1501
                        obs.value_coded = getYesNoExposicaoTarvCoded(rs.Fields.Item("exposicaotarvmae").Value)
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "exposicaotarvnascenca")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1502
                        obs.value_coded = getYesNoExposicaoTarvCoded(rs.Fields.Item("exposicaotarvnascenca").Value)
                        dataArray.Add(obs)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "patologianeonatal")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TText
                        obs.concept_id = 1506
                        obs.value_text = rs.Fields.Item("patologianeonatal").Value
                        dataArray.Add(obs)
                    End If
                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1612
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                    dataArray.Clear()

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "injeccoes")) Then

                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1507
                        If rs.Fields.Item("injeccoes").Value = "-1" Then
                            obs.value_coded = 1065
                        ElseIf rs.Fields.Item("injeccoes").Value = "0" Then
                            obs.value_coded = 1066
                        Else
                            obs.value_coded = 1457
                        End If

                        dataArray.Add(obs)

                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "escarificacoes")) Then

                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1509
                        If rs.Fields.Item("escarificacoes").Value = "-1" Then
                            obs.value_coded = 1065
                        ElseIf rs.Fields.Item("escarificacoes").Value = "0" Then
                            obs.value_coded = 1066
                        Else
                            obs.value_coded = 1457
                        End If
                        dataArray.Add(obs)

                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "extracoesdentarias")) Then

                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1508
                        If rs.Fields.Item("extracoesdentarias").Value = "-1" Then
                            obs.value_coded = 1065
                        ElseIf rs.Fields.Item("extracoesdentarias").Value = "0" Then
                            obs.value_coded = 1066
                        Else
                            obs.value_coded = 1457
                        End If
                        dataArray.Add(obs)

                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "cirurgias")) Then

                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1472
                        If rs.Fields.Item("cirurgias").Value = "-1" Then
                            obs.value_coded = 1065
                        ElseIf rs.Fields.Item("cirurgias").Value = "0" Then
                            obs.value_coded = 1066
                        Else
                            obs.value_coded = 1457
                        End If
                        dataArray.Add(obs)

                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "transfusao")) Then

                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = importDataTransfusao(nid, dataAbertura)
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1063
                        If rs.Fields.Item("transfusao").Value = "-1" Then
                            obs.value_coded = 1065
                        ElseIf rs.Fields.Item("transfusao").Value = "0" Then
                            obs.value_coded = 1066
                        Else
                            obs.value_coded = 1457
                        End If
                        dataArray.Add(obs)

                    End If
                    If dataArray.Count > 0 Then

                        For Each o As Obs In dataArray
                            ObsDAO.insertObs(o, False)
                        Next
                    End If
                    dataArray.Clear()


                    temArray = importCirurgias(nid)
                    If temArray.Count > 0 Then
                        For Each obs1 As Obs In temArray
                            obs1.location_id = locationid
                            obs1.person_id = patientID
                            obs1.date_created = Now
                            obs1.encounter_id = encounterID
                            obs1.obs_datetime = dataAbertura
                            dataArray.Add(obs1)
                        Next
                    End If



                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1688
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If

                    temArray.Clear()
                    dataArray.Clear()

                    temArray = importInternamento(nid)
                    If temArray.Count > 0 Then
                        For Each obs1 As Obs In temArray
                            obs1.location_id = locationid
                            obs1.person_id = patientID
                            obs1.date_created = Now
                            obs1.encounter_id = encounterID
                            obs1.obs_datetime = dataAbertura
                            dataArray.Add(obs1)
                        Next
                    End If

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1606
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                    dataArray.Clear()
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "aleitamentomaterno")) Then

                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6061
                        If rs.Fields.Item("aleitamentomaterno").Value = "-1" Then
                            obs.value_coded = 1065
                        ElseIf rs.Fields.Item("aleitamentomaterno").Value = "0" Then
                            obs.value_coded = 1066
                        Else
                            obs.value_coded = 1457
                        End If
                        dataArray.Add(obs)

                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "aleitamentoexclusivo")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1613
                        obs.value_coded = getYesNoAleitamentoCoded(rs.Fields.Item("aleitamentoexclusivo").Value)
                        dataArray.Add(obs)

                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "idadedesmame")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TNumeric
                        obs.concept_id = 1510
                        obs.value_numeric = rs.Fields.Item("idadedesmame").Value
                        dataArray.Add(obs)

                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "pavcompleto")) Then

                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1511
                        If rs.Fields.Item("pavcompleto").Value = "-1" Then
                            obs.value_coded = 1065
                        ElseIf rs.Fields.Item("pavcompleto").Value = "0" Then
                            obs.value_coded = 1066
                        Else
                            obs.value_coded = 1457
                        End If
                        dataArray.Add(obs)
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "idadecronologica")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TNumeric
                        obs.concept_id = 1512
                        obs.value_numeric = rs.Fields.Item("idadecronologica").Value
                        dataArray.Add(obs)

                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "bailey")) Then
                        obs = New Obs
                        obs.location_id = locationid
                        obs.person_id = patientID
                        obs.date_created = Now
                        obs.voided = 0
                        obs.encounter_id = encounterID
                        obs.obs_datetime = dataAbertura
                        obs.data_Type = ObsDataType.TNumeric
                        obs.concept_id = 1514
                        obs.value_numeric = rs.Fields.Item("bailey").Value
                        dataArray.Add(obs)

                    End If

                    If dataArray.Count > 0 Then
                        obsSet.concept_id = 1614
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In dataArray
                            o.obs_group_id = obsGroupId
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                End If
                temArray.Clear()
                dataArray.Clear()

                If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "nlivroPreTarv")) Then

                    Dim nLivro As Integer = rs.Fields.Item("nlivroPreTarv").Value

                    If nLivro <= 2 Then
                        If (Not IsDBNull(rs.Fields.Item("paginaPreTarv").Value)) And (Not IsDBNull(rs.Fields.Item("linhaPreTarv").Value)) And (Not IsDBNull(rs.Fields.Item("dataPreTarv").Value)) Then


                            Dim nPagina As Integer = rs.Fields.Item("paginaPreTarv").Value
                            Dim nLinha As Integer = rs.Fields.Item("linhaPreTarv").Value
                            Dim dataLivro As Date = rs.Fields.Item("dataPreTarv").Value

                            Dim livroTARVEncounterID As Integer = EncounterDAO.insertEncounterByParam(32, patientID, locationid, 128, dataLivro, 14, openMRSProviderID)

                            'Livro
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = livroTARVEncounterID
                            obs.obs_datetime = dataLivro
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6263
                            obs.value_coded = IIf(nLivro = 1, 6259, 6260)
                            dataArray.Add(obs)

                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = livroTARVEncounterID
                            obs.obs_datetime = dataLivro
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 6265
                            obs.value_numeric = nPagina
                            dataArray.Add(obs)

                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = livroTARVEncounterID
                            obs.obs_datetime = dataLivro
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 6267
                            obs.value_numeric = nLinha
                            dataArray.Add(obs)

                            For Each o As Obs In dataArray
                                ObsDAO.insertObs(o, False)
                            Next
                        End If
                    End If
                End If


                If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "nlivroTarv")) Then

                    Dim nLivro As Integer = rs.Fields.Item("nlivroTarv").Value

                    If nLivro <= 2 Then
                        If (Not IsDBNull(rs.Fields.Item("paginaTarv").Value)) And (Not IsDBNull(rs.Fields.Item("linhaTarv").Value)) And (Not IsDBNull(rs.Fields.Item("dataTarv2").Value)) Then


                            Dim nPagina As Integer = rs.Fields.Item("paginaTarv").Value
                            Dim nLinha As Integer = rs.Fields.Item("linhaTarv").Value
                            Dim dataLivro As Date = rs.Fields.Item("dataTarv2").Value

                            Dim livroTARVEncounterID As Integer = EncounterDAO.insertEncounterByParam(33, patientID, locationid, 129, dataLivro, 14, openMRSProviderID)

                            'Livro
                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = livroTARVEncounterID
                            obs.obs_datetime = dataLivro
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6264
                            obs.value_coded = IIf(nLivro = 1, 6261, 6262)
                            dataArray.Add(obs)

                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = livroTARVEncounterID
                            obs.obs_datetime = dataLivro
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 6266
                            obs.value_numeric = nPagina
                            dataArray.Add(obs)

                            obs = New Obs
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            obs.encounter_id = livroTARVEncounterID
                            obs.obs_datetime = dataLivro
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 6268
                            obs.value_numeric = nLinha
                            dataArray.Add(obs)

                            For Each o As Obs In dataArray
                                ObsDAO.insertObs(o, False)
                            Next
                        End If
                    End If
                End If



                ProcessoParteB.importProcessoBCrianca(nid, locationid, patientID)
                rs.MoveNext()
            End While
        End If
        rs.Close()
    End Sub


End Class
