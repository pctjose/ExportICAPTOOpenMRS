Imports MySql.Data.MySqlClient
Public Class ProcessoParteB
    Public Shared Sub importProcessoBAdulto(ByVal nid As String, ByVal locationid As Int16, ByVal patientID As Integer)
        Dim observacoes As New ArrayList
        Dim sinaisVitais As New ArrayList
        Dim abdominalSet1677 As New ArrayList
        Dim extremidadeSet1683 As New ArrayList
        Dim examesCardiologico As New ArrayList
        Dim genitais As New ArrayList
        Dim examesNeurologicos1681 As New ArrayList
        Dim examesPulmonares1675 As New ArrayList
        Dim examesPele1674 As New ArrayList

        Dim cmmFonte As New Command 'Acess
        Dim Comando As New Command
        'Dim patientID As Integer
        Dim encounterID As Integer
        'Dim nid As String
        Dim rs As New Recordset
        Dim obs As New Obs
        'Dim dataAbertura As Date
        Dim obsSet As Obs
        Dim obsGroupId As Integer
        Dim codObs As String
        Dim estadoObs As String
        Dim valorObs As String
        Dim observacaoObs As String
        Dim dataObs As Date
        Dim haDados As Boolean = False
        Dim dataAbertura As Date
        Dim providerID As String = ""
        'Dim obs As New Obs

        Dim cmmDestino As New MySqlCommand
        cmmDestino.Connection = ConexaoOpenMRS3
        cmmDestino.CommandType = CommandType.Text
        'Dim rs As New Recordset
        cmmFonte.ActiveConnection = ICAPConection
        cmmFonte.CommandType = CommandTypeEnum.adCmdText
        cmmFonte.CommandText = "SELECT t_observacaopaciente.codobservacao, t_observacaopaciente.codestado, " & _
                                "t_observacaopaciente.data, t_observacaopaciente.valor,t_observacaodata.medico,t_observacaopaciente.observacao " & _
                                "FROM t_observacaopaciente,t_observacaodata,t_paciente  " & _
                                " where t_paciente.nid=t_observacaodata.nid and t_observacaodata.nid=t_observacaopaciente.nid and " & _
                                "      t_observacaodata.data=t_observacaopaciente.data and t_observacaopaciente.nid='" & nid & "'"
        rs = cmmFonte.Execute


        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            dataAbertura = rs.Fields.Item("data").Value
            providerID = PatientUtils.verificaNulo(rs, "medico")
            haDados = True
            While Not rs.EOF
                codObs = PatientUtils.verificaNulo(rs, "codobservacao")
                estadoObs = PatientUtils.verificaNulo(rs, "codestado")
                valorObs = PatientUtils.verificaNulo(rs, "valor")
                observacaoObs = PatientUtils.verificaNulo(rs, "observacao")
                If Not IsDBNull(rs.Fields.Item("data").Value) Then
                    dataObs = rs.Fields.Item("data").Value
                End If
                Select Case codObs
                    Case "Abdómen", "Abdomen"
                        If Not String.IsNullOrEmpty(estadoObs) Then
                            Select Case estadoObs
                                Case "Ascite", "ascite"
                                    obs.concept_id = 1125
                                    obs.value_coded = 581
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    abdominalSet1677.Add(obs)
                                Case "Globoso", "globoso", "Globuloso"
                                    obs.concept_id = 1125
                                    obs.value_coded = 1397
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    abdominalSet1677.Add(obs)
                                Case "Meteorismo", "meteorismo"
                                    obs.concept_id = 1125
                                    obs.value_coded = 1398
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    abdominalSet1677.Add(obs)
                                Case "Normal", "normal"
                                    obs.concept_id = 1125
                                    obs.value_coded = 1115
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    abdominalSet1677.Add(obs)
                                Case Else
                                    obs.concept_id = 1396
                                    obs.value_text = IIf(Not String.IsNullOrEmpty(observacaoObs), observacaoObs, "Outro")
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TText
                                    abdominalSet1677.Add(obs)
                            End Select
                        End If
                    Case "Aparelho Articular", "aparelho articular"
                        If Not String.IsNullOrEmpty(estadoObs) Then
                            Select Case estadoObs
                                Case "Normal", "normal"
                                    obs.concept_id = 1127
                                    obs.value_coded = 1115
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    extremidadeSet1683.Add(obs)
                                Case "Rigidez", "rigidez"
                                    obs.concept_id = 1127
                                    obs.value_coded = 1404
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    extremidadeSet1683.Add(obs)
                                Case "Tumefacções", "Tumefaccoes"
                                    obs.concept_id = 1127
                                    obs.value_coded = 1403
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    extremidadeSet1683.Add(obs)
                                Case Else
                                    obs.concept_id = 1127
                                    obs.value_coded = 5622
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    extremidadeSet1683.Add(obs)

                                    Dim other As New Obs
                                    other.concept_id = 1682
                                    other.data_Type = ObsDataType.TText
                                    other.obs_datetime = dataObs
                                    other.value_text = IIf(Not String.IsNullOrEmpty(observacaoObs), observacaoObs, "Outro")
                                    extremidadeSet1683.Add(other)

                            End Select
                        End If

                    Case "Cardiológico - Auscultação", "Cardiologico - Auscultacao"
                        If Not String.IsNullOrEmpty(estadoObs) Then
                            Select Case estadoObs
                                Case "Arritmias", "arritmias"
                                    obs.concept_id = 1124
                                    obs.value_coded = 1395
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    examesCardiologico.Add(obs)
                                Case "F.C", "FC"
                                    obs.concept_id = 5087
                                    If valorObs.EndsWith("/min") Then
                                        valorObs = valorObs.Remove(valorObs.IndexOf("/"))
                                    End If
                                    obs.value_numeric = IIf(Not String.IsNullOrEmpty(valorObs), valorObs, 0)
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TNumeric
                                    examesCardiologico.Add(obs)
                                Case "Normal", "normal"
                                    obs.concept_id = 1124
                                    obs.value_coded = 1115
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    examesCardiologico.Add(obs)
                                Case "Sopros", "sopros"
                                    obs.concept_id = 1124
                                    obs.value_coded = 562
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    examesCardiologico.Add(obs)
                                Case Else
                                    obs.concept_id = 1124
                                    obs.value_coded = 1116
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    examesCardiologico.Add(obs)
                            End Select
                        End If
                    Case "Estado Geral"
                        If Not String.IsNullOrEmpty(estadoObs) Then
                            Select Case estadoObs
                                Case "Bom", "bom"
                                    obs.concept_id = 1382
                                    obs.value_coded = 1383
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    sinaisVitais.Add(obs)
                                Case "Mau", "mau"
                                    obs.concept_id = 1382
                                    obs.value_coded = 1385
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    sinaisVitais.Add(obs)
                                Case "Moderado", "moderado"
                                    obs.concept_id = 1382
                                    obs.value_coded = 1384
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    sinaisVitais.Add(obs)
                            End Select
                        End If
                    Case "Estado Hidratação", "Estado Hidratacao"
                        If Not String.IsNullOrEmpty(estadoObs) Then
                            Select Case estadoObs
                                Case "Desidratado", "desidratado"
                                    obs.concept_id = 1425
                                    obs.value_coded = 1066
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    observacoes.Add(obs)
                                Case "Hidratado", "hidratado"
                                    obs.concept_id = 1425
                                    obs.value_coded = 1065
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    observacoes.Add(obs)
                            End Select
                        End If
                    Case "Genitais"
                        If Not String.IsNullOrEmpty(estadoObs) Then
                            Select Case estadoObs
                                Case "Condilomas", "condilomas"
                                    obs.concept_id = 1126
                                    obs.value_coded = 1400
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    genitais.Add(obs)
                                Case "Normais", "normais"
                                    obs.concept_id = 1126
                                    obs.value_coded = 1115
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    genitais.Add(obs)
                                Case "Secreções", "Secrecoes"
                                    obs.concept_id = 1126
                                    obs.value_coded = 1399
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    genitais.Add(obs)
                                Case "Úlceras", "Ulceras"
                                    obs.concept_id = 1126
                                    obs.value_coded = 1602
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    genitais.Add(obs)
                            End Select
                        End If
                    Case "Mucosas"
                        If Not String.IsNullOrEmpty(estadoObs) Then
                            Select Case estadoObs
                                Case "Anictéricas", "Anictericas"
                                    obs.concept_id = 1415
                                    obs.value_coded = 1420
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    sinaisVitais.Add(obs)
                                Case "conjuntivas", "Conjuntivas"
                                    obs.concept_id = 1415
                                    obs.value_coded = 1418
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    sinaisVitais.Add(obs)
                                Case "coradas", "Coradas"
                                    obs.concept_id = 1415
                                    obs.value_coded = 1416
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    sinaisVitais.Add(obs)
                                Case "descoradas", "Descoradas"
                                    obs.concept_id = 1415
                                    obs.value_coded = 1417
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    sinaisVitais.Add(obs)
                                Case "Ictéricas", "ictéricas"
                                    obs.concept_id = 1415
                                    obs.value_coded = 1419
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    sinaisVitais.Add(obs)
                            End Select
                        End If

                    Case "Neurológico", "Neurologico"
                        If Not String.IsNullOrEmpty(estadoObs) Then
                            Select Case estadoObs
                                Case "Neuropatia periférica"
                                    obs.concept_id = 1129
                                    obs.value_coded = 821
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    examesNeurologicos1681.Add(obs)
                                Case "Normal", "normal"
                                    obs.concept_id = 1129
                                    obs.value_coded = 1115
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    examesNeurologicos1681.Add(obs)
                                Case "Parésias", "Paresias"
                                    obs.concept_id = 1129
                                    obs.value_coded = 1401
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    examesNeurologicos1681.Add(obs)
                                Case "Rigidez na Nuca"
                                    obs.concept_id = 1129
                                    obs.value_coded = 5170
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    examesNeurologicos1681.Add(obs)
                            End Select
                        End If
                    Case "Pele", "pele"
                        If Not String.IsNullOrEmpty(estadoObs) Then
                            Select Case estadoObs
                                Case "Integra"
                                    obs.concept_id = 1120
                                    obs.value_coded = 1421
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    examesPele1674.Add(obs)
                                Case "Normal", "normal"
                                    obs.concept_id = 1120
                                    obs.value_coded = 1422
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    examesPele1674.Add(obs)
                            End Select
                        End If
                    Case "Pulmonar - Respiracao", "Pulmonar - Respiração"
                        If Not String.IsNullOrEmpty(estadoObs) Then
                            Select Case estadoObs
                                Case "Dispneia"
                                    obs.concept_id = 1427
                                    obs.value_coded = 5960
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    examesPulmonares1675.Add(obs)
                                Case "Normal", "normal"
                                    obs.concept_id = 1427
                                    obs.value_coded = 1115
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TCoded
                                    examesPulmonares1675.Add(obs)
                                Case "fr", "FR", "F.R"
                                    If Not String.IsNullOrEmpty(valorObs) Then
                                        obs.concept_id = 5242
                                        If valorObs.EndsWith("/min") Then
                                            valorObs = valorObs.Remove(valorObs.IndexOf("/"))
                                        End If
                                        obs.value_numeric = IIf(Not String.IsNullOrEmpty(valorObs), valorObs, 0)
                                        obs.obs_datetime = dataObs
                                        obs.data_Type = ObsDataType.TNumeric
                                        examesPulmonares1675.Add(obs)
                                    End If
                            End Select
                        End If
                    Case "Pulmonar - Auscultação"
                        If Not String.IsNullOrEmpty(valorObs) Then
                            If valorObs.EndsWith("/min") Then
                                valorObs = valorObs.Remove(valorObs.IndexOf("/"))
                            End If
                            valorObs = valorObs.Replace(",", ".")
                            If IsNumeric(valorObs) Then
                                obs.concept_id = 5242
                                obs.value_numeric = valorObs 'IIf(Not String.IsNullOrEmpty(valorObs), valorObs, 0)
                                obs.obs_datetime = dataObs
                                obs.data_Type = ObsDataType.TNumeric
                                examesPulmonares1675.Add(obs)
                            End If
                        End If
                    Case "Altura", "altura", "Estatura"
                        If Not String.IsNullOrEmpty(valorObs) Then
                            valorObs = valorObs.Replace(",", ".")
                            valorObs = valorObs.Replace("o", "0")
                            valorObs = valorObs.Replace(";", ".")
                            If valorObs.Contains(".") Then
                                If valorObs.IndexOf(".") <> valorObs.LastIndexOf(".") Then
                                    valorObs = valorObs.Remove(valorObs.IndexOf("."), 1)
                                End If
                            End If
                            valorObs = CDbl(valorObs)
                            If valorObs <= 2 Then
                                valorObs = CDbl(valorObs) * 100
                            ElseIf valorObs > 400 And valorObs < 3000 Then
                                valorObs = valorObs / 10
                            ElseIf valorObs >= 3 And valorObs <= 20 Then
                                valorObs = valorObs * 10
                            End If
                            obs.concept_id = 5090
                            obs.value_numeric = valorObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TNumeric
                            sinaisVitais.Add(obs)
                        End If
                    Case "Peso", "peso", "Peso-Criança", "Peso-Crianca"
                        If Not String.IsNullOrEmpty(valorObs) Then
                            If Not String.IsNullOrEmpty(valorObs) Then
                                valorObs = valorObs.Replace(",", ".")
                                If valorObs.Contains(".") Then
                                    If valorObs.IndexOf(".") <> valorObs.LastIndexOf(".") Then
                                        valorObs = valorObs.Remove(valorObs.IndexOf("."), 1)
                                    End If
                                End If
                                valorObs = CDbl(valorObs)
                                If valorObs > 150 Then
                                    valorObs = valorObs / 10
                                End If
                                obs.concept_id = 5089
                                obs.value_numeric = valorObs
                                obs.obs_datetime = dataObs
                                obs.data_Type = ObsDataType.TNumeric
                                sinaisVitais.Add(obs)
                            End If
                        End If
                    Case "Temperatura", "temperatura", "Te", "te"
                        If Not String.IsNullOrEmpty(valorObs) Then
                            valorObs = valorObs.Replace(";", ".")
                            valorObs = valorObs.Replace(",", ".")
                            valorObs = valorObs.Replace("/", ".")
                            obs.concept_id = 5088
                            obs.value_numeric = valorObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TNumeric
                            sinaisVitais.Add(obs)
                        End If

                    Case "PC", "pc", "Pc", "pC"
                        If Not String.IsNullOrEmpty(valorObs) Then
                            obs.concept_id = 5314
                            obs.value_numeric = valorObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TNumeric
                            observacoes.Add(obs)
                        End If
                    Case "Tensão Arterial", "Tensao Arterial", "Tensão-Arterial"
                        If Not String.IsNullOrEmpty(valorObs) Then
                            valorObs = valorObs.Replace(",", ".")
                            If valorObs.Contains("/") And (Not valorObs.StartsWith("/")) And (Not valorObs.EndsWith("/")) Then
                                Dim inferior As Int16
                                Dim superior As Int16

                                inferior = valorObs.Substring(0, valorObs.IndexOf("/"))
                                superior = valorObs.Substring(valorObs.IndexOf("/") + 1)
                                Dim tempObsInferior As New Obs
                                Dim tempObsSuperior As New Obs

                                tempObsInferior.concept_id = 5085
                                tempObsInferior.value_numeric = inferior
                                tempObsInferior.obs_datetime = dataObs
                                tempObsInferior.data_Type = ObsDataType.TNumeric
                                sinaisVitais.Add(tempObsInferior)

                                tempObsSuperior.concept_id = 5086
                                tempObsSuperior.value_numeric = inferior
                                tempObsSuperior.obs_datetime = dataObs
                                tempObsSuperior.data_Type = ObsDataType.TNumeric
                                sinaisVitais.Add(tempObsSuperior)
                            ElseIf valorObs.Contains(",") And (Not valorObs.StartsWith(",")) And (Not valorObs.EndsWith(",")) Then
                                Dim inferior As Int16
                                Dim superior As Int16

                                inferior = valorObs.Substring(0, valorObs.IndexOf(","))
                                superior = valorObs.Substring(valorObs.IndexOf(",") + 1)
                                Dim tempObsInferior As New Obs
                                Dim tempObsSuperior As New Obs

                                tempObsInferior.concept_id = 5085
                                tempObsInferior.value_numeric = inferior
                                tempObsInferior.obs_datetime = dataObs
                                tempObsInferior.data_Type = ObsDataType.TNumeric
                                sinaisVitais.Add(tempObsInferior)

                                tempObsSuperior.concept_id = 5086
                                tempObsSuperior.value_numeric = inferior
                                tempObsSuperior.obs_datetime = dataObs
                                tempObsSuperior.data_Type = ObsDataType.TNumeric
                                sinaisVitais.Add(tempObsSuperior)
                            Else

                                If Not String.IsNullOrEmpty(estadoObs) Then
                                    Select Case estadoObs
                                        Case "Inferior"
                                            Dim tempObsD As New Obs
                                            tempObsD.concept_id = 5085
                                            tempObsD.value_numeric = valorObs
                                            tempObsD.obs_datetime = dataObs
                                            tempObsD.data_Type = ObsDataType.TNumeric
                                            sinaisVitais.Add(tempObsD)
                                        Case "Superior"
                                            Dim tempObsS As New Obs
                                            tempObsS.concept_id = 5086
                                            tempObsS.value_numeric = valorObs
                                            tempObsS.obs_datetime = dataObs
                                            tempObsS.data_Type = ObsDataType.TNumeric
                                            observacoes.Add(tempObsS)
                                    End Select
                                End If
                            End If
                        End If
                    Case "Axilas aumentados"
                        If String.IsNullOrEmpty(observacaoObs) Then
                            obs.concept_id = 5112
                            obs.value_coded = 1065
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TCoded
                            observacoes.Add(obs)
                        ElseIf observacaoObs = "S" Or observacaoObs = "Sim" Or observacaoObs = "sim" Or observacaoObs = "SIM" Then
                            obs.concept_id = 5112
                            obs.value_coded = 1065
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TCoded
                            observacoes.Add(obs)
                        Else
                            obs.concept_id = 5112
                            obs.value_coded = 1066
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TCoded
                            observacoes.Add(obs)
                        End If
                        
                    Case "Cervicais aumentados"
                        If String.IsNullOrEmpty(observacaoObs) Then
                            obs.concept_id = 643
                            obs.value_coded = 1065
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TCoded
                            observacoes.Add(obs)
                        ElseIf observacaoObs = "S" Or observacaoObs = "Sim" Or observacaoObs = "sim" Or observacaoObs = "SIM" Then
                            obs.concept_id = 643
                            obs.value_coded = 1065
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TCoded
                            observacoes.Add(obs)
                        Else
                            obs.concept_id = 643
                            obs.value_coded = 1066
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TCoded
                            observacoes.Add(obs)
                        End If
                    Case "Inguinais aumentadados"
                        If String.IsNullOrEmpty(observacaoObs) Then
                            obs.concept_id = 506
                            obs.value_coded = 1065
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TCoded
                            observacoes.Add(obs)
                        ElseIf observacaoObs = "S" Or observacaoObs = "Sim" Or observacaoObs = "sim" Or observacaoObs = "SIM" Then
                            obs.concept_id = 506
                            obs.value_coded = 1065
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TCoded
                            observacoes.Add(obs)
                        Else
                            obs.concept_id = 506
                            obs.value_coded = 1066
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TCoded
                            observacoes.Add(obs)
                        End If
                    Case "Outros aumentados"
                        If String.IsNullOrEmpty(observacaoObs) Then
                            obs.concept_id = 1426
                            obs.value_coded = 1065
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TCoded
                            observacoes.Add(obs)
                        ElseIf observacaoObs = "S" Or observacaoObs = "Sim" Or observacaoObs = "sim" Or observacaoObs = "SIM" Then
                            obs.concept_id = 1426
                            obs.value_coded = 1065
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TCoded
                            observacoes.Add(obs)
                        Else
                            obs.concept_id = 1426
                            obs.value_coded = 1066
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TCoded
                            observacoes.Add(obs)
                        End If
                    Case "Cavidade orofaríngea"
                        obs.concept_id = 1672
                        obs.value_coded = 5244
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        sinaisVitais.Add(obs)
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            Dim temObs As New Obs
                            temObs.concept_id = 1424
                            temObs.value_text = observacaoObs
                            temObs.obs_datetime = dataObs
                            temObs.data_Type = ObsDataType.TText
                            sinaisVitais.Add(temObs)
                        End If
                    Case "Esplenomegáglia", "Esplenomegália"
                        obs.concept_id = 1125
                        obs.value_coded = 5009
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        abdominalSet1677.Add(obs)
                    Case "Hepatomegália"
                        obs.concept_id = 1125
                        obs.value_coded = 5008
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        abdominalSet1677.Add(obs)
                        
                    Case "IMC"
                        If Not String.IsNullOrEmpty(valorObs) Then
                            If valorObs.Contains("%") Then
                                valorObs = valorObs.Remove(valorObs.IndexOf("%"))
                            End If
                            valorObs = valorObs.Replace(",", ".")
                            Try
                                Dim valor As Double = CDbl(valorObs)
                                If valor < 100 Then
                                    obs.concept_id = 1342
                                    obs.value_numeric = valorObs
                                    obs.obs_datetime = dataObs
                                    obs.data_Type = ObsDataType.TNumeric
                                    sinaisVitais.Add(obs)
                                End If
                            Catch ex As Exception

                            End Try
                            
                        End If

                End Select
                obs = New Obs
                rs.MoveNext()
            End While
            rs.Close()
        End If


        If haDados Then

            'patientID = GetPatientOpenMRSIDByNID(nid)
            If patientID > 0 Then
                Dim provider As Integer
                If Not String.IsNullOrEmpty(providerID) Then
                    provider = GetOpenMRSProvider(providerID, locationid)
                Else
                    provider = 27
                End If
                'cmmDestino.CommandText = "Insert into encounter(encounter_type,patient_id,provider_id,location_id," & _
                '                                "form_id,encounter_datetime,creator,date_created,voided,uuid) values(1," & patientID & "," & provider & "," & locationid & "," & _
                '                                "100,'" & dataMySQL(dataAbertura) & "',22,now(),0,uuid())"
                'cmmDestino.ExecuteNonQuery()

                'cmmDestino.CommandText = "Select max(encounter_id) from encounter"
                'encounterID = cmmDestino.ExecuteScalar

                encounterID = EncounterDAO.insertEncounterByParam(1, patientID, locationid, 100, dataAbertura, 12, provider)

                obsSet = New Obs

                obsSet.location_id = locationid
                obsSet.person_id = patientID
                obsSet.date_created = Now
                obsSet.voided = 0
                obsSet.encounter_id = encounterID
                obsSet.obs_datetime = dataAbertura

                If sinaisVitais.Count > 0 Then
                    obsSet.concept_id = 1673
                    obsGroupId = ObsDAO.insertSet(obsSet)
                    For Each o As Obs In sinaisVitais
                        o.obs_group_id = obsGroupId
                        o.encounter_id = encounterID
                        o.location_id = locationid
                        o.person_id = patientID
                        If String.IsNullOrEmpty(o.obs_datetime) Then o.obs_datetime = dataAbertura
                        ObsDAO.insertObs(o, True)
                    Next
                End If

                If abdominalSet1677.Count > 0 Then
                    obsSet.concept_id = 1677
                    obsGroupId = ObsDAO.insertSet(obsSet)
                    For Each o As Obs In abdominalSet1677
                        o.obs_group_id = obsGroupId
                        o.encounter_id = encounterID
                        o.location_id = locationid
                        o.person_id = patientID
                        If String.IsNullOrEmpty(o.obs_datetime) Then o.obs_datetime = dataAbertura
                        ObsDAO.insertObs(o, True)
                    Next
                End If

                If extremidadeSet1683.Count > 0 Then
                    obsSet.concept_id = 1683
                    obsGroupId = ObsDAO.insertSet(obsSet)
                    For Each o As Obs In extremidadeSet1683
                        o.obs_group_id = obsGroupId
                        o.encounter_id = encounterID
                        o.location_id = locationid
                        o.person_id = patientID
                        If String.IsNullOrEmpty(o.obs_datetime) Then o.obs_datetime = dataAbertura
                        ObsDAO.insertObs(o, True)
                    Next
                End If

                If examesCardiologico.Count > 0 Then
                    obsSet.concept_id = 1676
                    obsGroupId = ObsDAO.insertSet(obsSet)
                    For Each o As Obs In examesCardiologico
                        o.obs_group_id = obsGroupId
                        o.encounter_id = encounterID
                        o.location_id = locationid
                        o.person_id = patientID
                        If String.IsNullOrEmpty(o.obs_datetime) Then o.obs_datetime = dataAbertura
                        ObsDAO.insertObs(o, True)
                    Next
                End If

                If genitais.Count > 0 Then
                    obsSet.concept_id = 1679
                    obsGroupId = ObsDAO.insertSet(obsSet)
                    For Each o As Obs In genitais
                        o.obs_group_id = obsGroupId
                        o.encounter_id = encounterID
                        o.location_id = locationid
                        o.person_id = patientID
                        If String.IsNullOrEmpty(o.obs_datetime) Then o.obs_datetime = dataAbertura
                        ObsDAO.insertObs(o, True)
                    Next
                End If
                If examesNeurologicos1681.Count > 0 Then
                    obsSet.concept_id = 1681
                    obsGroupId = ObsDAO.insertSet(obsSet)
                    For Each o As Obs In examesNeurologicos1681
                        o.obs_group_id = obsGroupId
                        o.encounter_id = encounterID
                        o.location_id = locationid
                        o.person_id = patientID
                        If String.IsNullOrEmpty(o.obs_datetime) Then o.obs_datetime = dataAbertura
                        ObsDAO.insertObs(o, True)
                    Next
                End If

                If examesPulmonares1675.Count > 0 Then
                    obsSet.concept_id = 1675
                    obsGroupId = ObsDAO.insertSet(obsSet)
                    For Each o As Obs In examesPulmonares1675
                        o.obs_group_id = obsGroupId
                        o.encounter_id = encounterID
                        o.location_id = locationid
                        o.person_id = patientID
                        If String.IsNullOrEmpty(o.obs_datetime) Then o.obs_datetime = dataAbertura
                        ObsDAO.insertObs(o, True)
                    Next
                End If

                If examesPele1674.Count > 0 Then
                    obsSet.concept_id = 1674
                    obsGroupId = ObsDAO.insertSet(obsSet)
                    For Each o As Obs In examesPele1674
                        o.obs_group_id = obsGroupId
                        o.encounter_id = encounterID
                        o.location_id = locationid
                        o.person_id = patientID
                        If String.IsNullOrEmpty(o.obs_datetime) Then o.obs_datetime = dataAbertura
                        ObsDAO.insertObs(o, True)
                    Next
                End If

                If observacoes.Count > 0 Then
                    For Each o As Obs In observacoes
                        o.encounter_id = encounterID
                        o.location_id = locationid
                        o.person_id = patientID
                        ' o.obs_datetime = dataAbertura
                        ObsDAO.insertObs(o, False)
                    Next
                End If


            End If
        End If

    End Sub

    Public Shared Sub importProcessoBCrianca(ByVal nid As String, ByVal locationid As Int16, ByVal patientID As Integer)
        Dim observacoes As New ArrayList
        

        Dim cmmFonte As New Command 'Acess
        Dim Comando As New Command
        'Dim patientID As Integer
        Dim encounterID As Integer
        'Dim nid As String
        Dim rs As New Recordset
        Dim obs As New Obs
        'Dim dataAbertura As Date
        Dim obsSet As Obs
        Dim obsGroupId As Integer
        Dim codObs As String
        Dim estadoObs As String
        Dim valorObs As String
        Dim dataObs As Date
        Dim observacaoObs As String
        Dim haDados As Boolean = False
        Dim dataAbertura As Date
        Dim providerID As String = ""
        'Dim obs As New Obs

        Dim cmmDestino As New MySqlCommand
        cmmDestino.Connection = ConexaoOpenMRS3
        cmmDestino.CommandType = CommandType.Text
        'Dim rs As New Recordset
        cmmFonte.ActiveConnection = ICAPConection
        cmmFonte.CommandType = CommandTypeEnum.adCmdText
        cmmFonte.CommandText = "SELECT t_observacaopaciente.codobservacao, t_observacaopaciente.codestado, " & _
                                "t_observacaopaciente.data, t_observacaopaciente.valor,t_observacaodata.medico,t_observacaopaciente.observacao " & _
                                "FROM t_observacaopaciente,t_observacaodata,t_paciente  " & _
                                " where t_paciente.nid=t_observacaodata.nid and t_observacaodata.nid=t_observacaopaciente.nid and " & _
                                "      t_observacaodata.data=t_observacaopaciente.data and t_observacaopaciente.nid='" & nid & "'"
        rs = cmmFonte.Execute


        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            dataAbertura = rs.Fields.Item("data").Value
            providerID = PatientUtils.verificaNulo(rs, "medico")
            haDados = True
            While Not rs.EOF
                codObs = PatientUtils.verificaNulo(rs, "codobservacao")
                estadoObs = PatientUtils.verificaNulo(rs, "codestado")
                valorObs = PatientUtils.verificaNulo(rs, "valor")
                observacaoObs = PatientUtils.verificaNulo(rs, "observacao")
                If Not IsDBNull(rs.Fields.Item("data").Value) Then
                    dataObs = rs.Fields.Item("data").Value
                End If
                If String.IsNullOrEmpty(valorObs) Then
                    valorObs = valorObs.Replace(" ", "")
                End If

                codObs = codObs.ToUpper

                Select Case codObs
                    Case "AC: SOPROS"
                        obs.concept_id = 562
                        obs.value_coded = 1065
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)

                    Case "ANEMIA"
                        obs.concept_id = 3
                        obs.value_coded = 1065
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)

                    Case "AP - FERVORES"
                        obs.concept_id = 1548
                        obs.value_coded = 1065
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)
                    Case "AP - MV"
                        obs.concept_id = 1545
                        obs.value_coded = 1115
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)
                    Case "AP: RONCOS"
                        obs.concept_id = 1549
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            observacaoObs = observacaoObs.ToUpper
                            If observacaoObs = "S" Or observacaoObs = "SIM" Then
                                obs.value_coded = 1065
                            ElseIf observacaoObs = "N" Or observacaoObs = "NAO" Or observacaoObs = "NÃO" Then
                                obs.value_coded = 1066
                            Else
                                obs.value_coded = 1065
                            End If
                        Else
                            obs.value_coded = 1065
                        End If
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)
                    Case "AP: SOPRO ANFÓRICO"
                        obs.concept_id = 1551
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            observacaoObs = observacaoObs.ToUpper
                            If observacaoObs = "S" Or observacaoObs = "SIM" Then
                                obs.value_coded = 1065
                            ElseIf observacaoObs = "N" Or observacaoObs = "NAO" Or observacaoObs = "NÃO" Then
                                obs.value_coded = 1066
                            Else
                                obs.value_coded = 1065
                            End If
                        Else
                            obs.value_coded = 1065
                        End If
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)
                    Case "AP: SOPRO TUBÁRICO"
                        obs.concept_id = 1550
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            observacaoObs = observacaoObs.ToUpper
                            If observacaoObs = "S" Or observacaoObs = "SIM" Then
                                obs.value_coded = 1065
                            ElseIf observacaoObs = "N" Or observacaoObs = "NAO" Or observacaoObs = "NÃO" Then
                                obs.value_coded = 1066
                            Else
                                obs.value_coded = 1065
                            End If
                        Else
                            obs.value_coded = 1065
                        End If
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)
                    Case "CANDIDÍASE DA OROFARÍNGE"
                        obs.concept_id = 5334
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            observacaoObs = observacaoObs.ToUpper
                            If observacaoObs = "S" Or observacaoObs = "SIM" Then
                                obs.value_coded = 1065
                            ElseIf observacaoObs = "N" Or observacaoObs = "NAO" Or observacaoObs = "NÃO" Then
                                obs.value_coded = 1066
                            Else
                                obs.value_coded = 1065
                            End If
                        Else
                            obs.value_coded = 1065
                        End If
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)
                    Case "DERMATITE GENERALIZADA"
                        obs.concept_id = 119
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            observacaoObs = observacaoObs.ToUpper
                            If observacaoObs = "S" Or observacaoObs = "SIM" Then
                                obs.value_coded = 1065
                            ElseIf observacaoObs = "N" Or observacaoObs = "NAO" Or observacaoObs = "NÃO" Then
                                obs.value_coded = 1066
                            Else
                                obs.value_coded = 1065
                            End If
                        Else
                            obs.value_coded = 1065
                        End If
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)
                    Case "DISPNEIA"
                        obs.concept_id = 5960
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            observacaoObs = observacaoObs.ToUpper
                            If observacaoObs = "S" Or observacaoObs = "SIM" Then
                                obs.value_coded = 1065
                            ElseIf observacaoObs = "N" Or observacaoObs = "NAO" Or observacaoObs = "NÃO" Then
                                obs.value_coded = 1066
                            Else
                                obs.value_coded = 1065
                            End If
                        Else
                            obs.value_coded = 1065
                        End If
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)
                    Case "ESPLENOMEGÁLIA", "ESPLENOMEGÁGLIA"
                        obs.concept_id = 5195
                        Try
                            obs.value_numeric = CDbl(observacaoObs)
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TNumeric
                            observacoes.Add(obs)
                        Catch ex As Exception

                        End Try
                        
                    Case "ESTATURA", "ALTURA"
                        If Not String.IsNullOrEmpty(valorObs) Then
                            valorObs = valorObs.Replace(",", ".")
                            valorObs = valorObs.Replace("o", "0")
                            valorObs = valorObs.Replace(";", ".")
                            If valorObs.Contains(".") Then
                                If valorObs.IndexOf(".") <> valorObs.LastIndexOf(".") Then
                                    valorObs = valorObs.Remove(valorObs.IndexOf("."), 1)
                                End If
                            End If
                            valorObs = CDbl(valorObs)
                            If valorObs <= 2 Then
                                valorObs = CDbl(valorObs) * 100
                            ElseIf valorObs > 400 And valorObs < 3000 Then
                                valorObs = valorObs / 10
                            ElseIf valorObs >= 3 And valorObs <= 20 Then
                                valorObs = valorObs * 10
                            End If
                            obs.concept_id = 5090
                            obs.value_numeric = valorObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TNumeric
                            observacoes.Add(obs)
                        End If
                    Case "HEPATOMEGÁLIA"
                        obs.concept_id = 5153
                        Try
                            obs.value_numeric = CDbl(observacaoObs)
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TNumeric
                            observacoes.Add(obs)
                        Catch ex As Exception

                        End Try
                        
                    Case "ICTERÍCIA"
                        obs.concept_id = 1419
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            observacaoObs = observacaoObs.ToUpper
                            If observacaoObs = "S" Or observacaoObs = "SIM" Then
                                obs.value_coded = 1065
                            ElseIf observacaoObs = "N" Or observacaoObs = "NAO" Or observacaoObs = "NÃO" Then
                                obs.value_coded = 1066
                            Else
                                obs.value_coded = 1065
                            End If
                        Else
                            obs.value_coded = 1065
                        End If
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)

                    Case "LINFADENOPATIA GENERALIZADA"
                        obs.concept_id = 161
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            observacaoObs = observacaoObs.ToUpper
                            If observacaoObs = "S" Or observacaoObs = "SIM" Then
                                obs.value_coded = 1065
                            ElseIf observacaoObs = "N" Or observacaoObs = "NAO" Or observacaoObs = "NÃO" Then
                                obs.value_coded = 1066
                            Else
                                obs.value_coded = 1065
                            End If
                        Else
                            obs.value_coded = 1065
                        End If
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)
                    Case "MALARIA", "MALÁRIA"
                        obs.concept_id = 123
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            observacaoObs = observacaoObs.ToUpper
                            If observacaoObs = "S" Or observacaoObs = "SIM" Then
                                obs.value_coded = 1065
                            ElseIf observacaoObs = "N" Or observacaoObs = "NAO" Or observacaoObs = "NÃO" Then
                                obs.value_coded = 1066
                            Else
                                obs.value_coded = 1065
                            End If
                        Else
                            obs.value_coded = 1065
                        End If
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)

                    Case "MEMBROS INFERIORES"
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            obs.concept_id = 1556
                            obs.value_text = observacaoObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TText
                            observacoes.Add(obs)
                        End If
                    Case "MEMBROS SUPERIORES"
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            obs.concept_id = 1555
                            obs.value_text = observacaoObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TText
                            observacoes.Add(obs)
                        End If
                    Case "ORL"
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            obs.concept_id = 1542
                            obs.value_text = observacaoObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TText
                            observacoes.Add(obs)
                        End If
                    Case "OUTRAS MASSAS"
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            obs.concept_id = 1553
                            obs.value_text = observacaoObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TText
                            observacoes.Add(obs)
                        End If
                    Case "PARÓTIDAS AUMENTADAS"
                        obs.concept_id = 1540
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            observacaoObs = observacaoObs.ToUpper
                            If observacaoObs = "S" Or observacaoObs = "SIM" Then
                                obs.value_coded = 1065
                            ElseIf observacaoObs = "N" Or observacaoObs = "NAO" Or observacaoObs = "NÃO" Then
                                obs.value_coded = 1066
                            Else
                                obs.value_coded = 1065
                            End If
                        Else
                            obs.value_coded = 1065
                        End If
                        obs.obs_datetime = dataObs
                        obs.data_Type = ObsDataType.TCoded
                        observacoes.Add(obs)
                    Case "PC"
                        If Not String.IsNullOrEmpty(valorObs) Then
                            valorObs = valorObs.Replace(",", ".")
                            obs.concept_id = 5314
                            obs.value_numeric = valorObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TNumeric
                            observacoes.Add(obs)
                        End If
                    Case "PERÍNEO"
                        If Not String.IsNullOrEmpty(observacaoObs) Then
                            obs.concept_id = 1554
                            obs.value_text = observacaoObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TText
                            observacoes.Add(obs)
                        ElseIf Not String.IsNullOrEmpty(valorObs) Then
                            obs.concept_id = 1554
                            obs.value_text = valorObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TText
                            observacoes.Add(obs)
                        End If
                    Case "PESO", "PESO-CRIANÇA", "PESO-CRIANCA"
                        If Not String.IsNullOrEmpty(valorObs) Then

                            valorObs = valorObs.Replace(",", ".")
                            If valorObs.Contains(".") Then
                                If valorObs.IndexOf(".") <> valorObs.LastIndexOf(".") Then
                                    valorObs = valorObs.Remove(valorObs.IndexOf("."), 1)
                                End If
                            End If
                            valorObs = CDbl(valorObs)
                            If valorObs > 150 Then
                                valorObs = valorObs / 10
                            End If
                            obs.concept_id = 5089
                            obs.value_numeric = valorObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TNumeric
                            observacoes.Add(obs)

                        End If
                    Case "TEMPERATURA", "TE"
                        If Not String.IsNullOrEmpty(valorObs) Then
                            obs.concept_id = 5088
                            obs.value_numeric = valorObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TNumeric
                            observacoes.Add(obs)
                        End If
                End Select
                obs = New Obs
                rs.MoveNext()
            End While
            rs.Close()
        End If


        If haDados Then

            'patientID = GetPatientOpenMRSIDByNID(nid)
            If patientID > 0 Then
                Dim provider As Int16
                If Not String.IsNullOrEmpty(providerID) Then
                    provider = GetOpenMRSProvider(providerID, locationid)
                Else
                    provider = 27
                End If
                'cmmDestino.CommandText = "Insert into encounter(encounter_type,patient_id,provider_id,location_id," & _
                '                                "form_id,encounter_datetime,creator,date_created,uuid) values(3," & patientID & "," & provider & "," & locationid & "," & _
                '                                "109,'" & dataMySQL(dataAbertura) & "',22,now(),uuid())"
                'cmmDestino.ExecuteNonQuery()

                'cmmDestino.CommandText = "Select max(encounter_id) from encounter"
                'encounterID = cmmDestino.ExecuteScalar

                encounterID = EncounterDAO.insertEncounterByParam(3, patientID, locationid, 109, dataAbertura, 12, provider)

                obsSet = New Obs

                obsSet.location_id = locationid
                obsSet.person_id = patientID
                obsSet.date_created = Now
                obsSet.voided = 0
                obsSet.encounter_id = encounterID
                obsSet.obs_datetime = dataAbertura

                If observacoes.Count > 0 Then
                    obsSet.concept_id = 1622
                    obsGroupId = ObsDAO.insertSet(obsSet)
                    For Each o As Obs In observacoes
                        o.obs_group_id = obsGroupId
                        o.encounter_id = encounterID
                        o.location_id = locationid
                        o.person_id = patientID
                        'o.obs_datetime = dataAbertura
                        ObsDAO.insertObs(o, True)
                    Next
                End If
            End If
        End If

    End Sub
End Class
