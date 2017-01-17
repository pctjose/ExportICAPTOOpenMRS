﻿Imports ADODB
Imports MySql.Data.MySqlClient
Public Class SeguimentoUtils
    Public Shared Function importTratamento(ByVal fonte As Connection, ByVal IdSeguimento As Integer) As ArrayList

        Dim seguimentos As New ArrayList
        Dim cmmFonte As New Command 'Acess
        Dim rs As New Recordset
        cmmFonte.ActiveConnection = fonte
        cmmFonte.CommandType = CommandTypeEnum.adCmdText
        cmmFonte.CommandText = "Select codtratamento,data,datafimtratamento from t_tratamentoseguimento where idseguimento=" & IdSeguimento & ""
        rs = cmmFonte.Execute
        Dim codTratamento As String
        Dim dataTratamento As Date
        Dim fimTratamento As Date
        Dim obs As New Obs

        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF
                codTratamento = PatientUtils.verificaNulo(rs, "codtratamento")
                If Not IsDBNull(rs.Fields.Item("data").Value) Then
                    dataTratamento = rs.Fields.Item("data").Value
                End If

                If Not IsDBNull(rs.Fields.Item("datafimtratamento").Value) Then
                    fimTratamento = rs.Fields.Item("datafimtratamento").Value
                End If
                'codTratamento = codTratamento.ToUpper

                Select Case codTratamento
                    Case "4DFC,SF+AF+MTV"
                        'Falta Implementar isto
                    Case "4DTC"
                        'Falta Implementar isto
                    Case "7A3+8K9"
                        'Falta Implementar isto
                    Case "A.A.Salicilico", "Acido Salicico", "Acído Salicilico"
                        'Falta Implementar isto
                    Case "Aciclovir", "aciclovir Pomada", "Aciclovir Pomada"
                        obs.value_coded = 732
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Acido Ascorbico"
                        'Falta Implementar isto
                    Case "Acido benzoico", "Ácido Benzoico"
                        'Falta Implementar isto
                    Case "Acido folico"
                        obs.value_coded = 257
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Acido Noldixico", "Acido Nalidicico", "Ácido Naldicico", "Acido Nalidixico", "Ácido Nalidixo"
                        obs.value_coded = 268
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Albundazol", "Albendazol", "Albentazol"
                        obs.value_coded = 941
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Amiloride", "Amilorida", "AMILORIDA"
                        'Falta Implementar isto
                    Case "Aminofilina"
                        obs.value_coded = 928
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Amoxicilina"
                        obs.value_coded = 265
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Amoxicilina/Clavanox"
                        obs.value_coded = 450
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Plumpy Nut", "Plumpy´Nut", "ATPU"
                        obs.value_coded = 1884
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Ampicilina"
                        obs.value_coded = 269
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Amptriptilina", "Amitriptilina"
                        obs.value_coded = 931
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Artesanato", "Artesanate", "Artesunate"
                        'Falta Implementar isto
                    Case "Aspirina", "AAS"
                        obs.value_coded = 88
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Atropina"
                        'Falta Implementar isto
                    Case "Bacitracina"
                        'Falta Implementar isto
                    Case "Baixa"
                        'Falta Implementar isto
                    Case "Betametazona Creme", "Betametazona Pomada", "Betamitazone", "Betametazona"
                        obs.value_coded = 786
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Bezantinica", "Benzatina", "Benzatinica", "Penicelina Bezantinica", "Penicilina Bezantinica"
                        obs.value_coded = 292
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Buscopa", "Buscopam", "Buscopan"
                        obs.value_coded = 917
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Butelescopilamina", "Bultescoplamina"
                        'Falta Implementar isto
                    Case "Butilenofilamina"
                        'Falta Implementar isto
                    Case "CAF"
                        'Falta Implementar isto
                    Case "Canamicina", "Kanamicina"
                        obs.value_coded = 356
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)

                    Case "Carbamezapina", "Carpamezapina"
                        obs.value_coded = 920
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)


                    Case "Ceftroaxona", "Ceftriaxona"
                        obs.value_coded = 496
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "CSB"
                        obs.value_coded = 2151
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)

                    Case "Nevirapina (NVP)", "Nevirapina", "NVP"
                        obs.value_coded = 631
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)

                    Case "Zidovudina (AZT)", "Zidovudina", "AZT"
                        obs.value_coded = 797
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)

                    Case "Cimetidina"
                        'Falta Implementar isto

                    Case "Ciprofloxacina", "Ciproflaxina", "ciprofloxacino"
                        obs.value_coded = 740
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Cloranfenicol"
                        obs.value_coded = 266
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Clorfeneramina", "Clorofelinamina", "Clorofinamina", "Clorofenamina", "clorfenamina", "Clorifeminamina"
                        obs.value_coded = 913
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Clotimazol Ovulo", "Clotrimazol", "Clotrimazol Crème", "Clotrimazol Creme", "Clotrimazol Pomada"
                        obs.value_coded = 960
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Coartem"
                        'Falta Implementar isto
                    Case "Complexo B"
                        obs.value_coded = 329
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Corretagem"
                        'Falta Implementar isto
                    Case "Cotrimoxazol xarope", "Cotrimoxazol", "CTZ"
                        obs.value_coded = 916
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Dexametazol Crème", "Dexametazol Creme", "Dexametazol"
                        obs.value_coded = 358
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Diazepan", "Diazepam"
                        obs.value_coded = 247
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Diclofinac", "Diclofenac", "Diclofenaco"
                        obs.value_coded = 436
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Difenoxilato", "Difenoxilate com Atropina"
                        obs.value_coded = 921
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Doxaciclina", "Doxicilina", "Doxacilina"
                        obs.value_coded = 95
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Endometicina", "Indometicina"
                        obs.value_coded = 263
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Eritromicina", "Eritromicona"
                        obs.value_coded = 272
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Etambutol"
                        obs.value_coded = 745
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)

                    Case "F100", "F 100", "F75", "F 75", "fulconazol", "Fluconazole", "Fluconazol"
                        obs.value_coded = 747
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Fansidar"
                        obs.value_coded = 87
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Fenobarbital"
                        obs.value_coded = 238
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "fenox", "Fenoximetil", "Fenox Metil", "Fenoximetilpenicilina"
                        'Falta Implementar isto
                    Case "Fentoina"
                        'Falta Implementar isto
                    Case "Flucloxacilina"
                        obs.value_coded = 447
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Furosamida", "FUROSEMIDA", "Furosamina", "Furosemida"
                        obs.value_coded = 99
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Ganciclovir"
                        'Falta Implementar isto
                    Case "Gentamicina"
                        obs.value_coded = 100
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Grisofulvina", "Gliciovulvina", "Griceofulvina", "Gristo Fluvirror"
                        'Falta Implementar isto
                    Case "Hexacloreto de Benzeno"
                        'Falta Implementar isto
                    Case "Hidrocloromediazida", "HIDROCLOROTIAZIDA", "Hidroclorometiazida"
                        obs.value_coded = 1243
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Hidroxido Aluminio", "Hidrixido de Aluminio"
                        obs.value_coded = 446
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Ibuprofeno", "Ibubrofeno"
                        obs.value_coded = 912
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Indometacine", "Indometacina", "Idometacina", "Inclometaina"
                        obs.value_coded = 263
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "INH", "Isoniazida", "Izoniasida", "Isoniasida"
                        obs.value_coded = 656
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Ketoconazol", "Keteconazol"
                        obs.value_coded = 926
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Lactato de Ringer"
                        'Falta Implementar isto
                    Case "Loperamida"
                        obs.value_coded = 429
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Menbendazol", "Mebendazol"
                        obs.value_coded = 244
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Metoclopramida", "Metodopramida"
                        obs.value_coded = 751
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Metronindazol", "Metronidazol"
                        obs.value_coded = 237
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Miconazol"
                        obs.value_coded = 918
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Mistura Oral"
                        'Falta Implementar isto
                    Case "Multivitamina", "Multivitaminas Xarope", "Multivitamina Xarope", "Multivitaminas"
                        obs.value_coded = 461
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Nerobium", "Neurobium", "Neurobium Inj.", "Neurobium Oral"
                        'Falta Implementar isto
                    Case "Nifedipine"
                        obs.value_coded = 250
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Nistantina Creme", "Nistantina óvula", "Nistantina Ovulo", "Nistantina Suspensão", "Nistantina", "Nistatina", "Nistatina Oral", "Nistantina Oral"
                        obs.value_coded = 919
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Oftalmicas"
                        'Falta Implementar isto
                    Case "p ritmica"
                        'Falta Implementar isto
                    Case "Paracetamol", "Paracetamos"
                        obs.value_coded = 89
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Parasiquatel", "Parasiquantim", "Praziquantil", "Paraziquantil"
                        'Falta Implementar isto
                    Case "Parizinamida"
                        'Falta Implementar isto
                    Case "Penicilina", "Penicilina G", "Penicilina procaina", "Penicelina Procaina"
                        obs.value_coded = 784
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Pirodoxina", "PIRIDOXINA", "Piridoxina"
                        obs.value_coded = 766
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Pirazinamida", "Pirazinamida"
                        obs.value_coded = 5829
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "predisolona", "Predizilona", "prednisolona"
                        obs.value_coded = 765
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Prodifilina"
                        'Falta Implementar isto
                    Case "Profloxacina"
                        'Falta Implementar isto
                    
                    Case "Quadriterme", "Quadriderme"
                        obs.value_coded = 744
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "QNNO", "Quinina", "Quinino"
                        obs.value_coded = 243
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Retinol"
                        obs.value_coded = 1720
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Rifanpicina", "Rifampicina"
                        obs.value_coded = 767
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Sal ferroso", "Sal feroso", "Sal Ferroso/Ác Fólico"
                        obs.value_coded = 256
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Salbutanol"
                        obs.value_coded = 798
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "SFAF"
                        'Falta Implementar isto
                    Case "Sherz"
                        obs.value_coded = 2057
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "SRO", "S.O.R.O", "SORO"
                        obs.value_coded = 5244
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Sulfadiazina", "Sulfadoxina/Pirimetamina"
                        obs.value_coded = 938
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Sulfamitaxazol", "SULFAMETOXAZOL", "Sulfametoxazol"
                        obs.value_coded = 916
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Tetraciclina", "Tetraxiclina"
                        obs.value_coded = 270
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Tiabendazol", "Tiabendazol Pomada"
                        'Falta Implementar isto
                    Case "Tuberculose"
                        obs.value_coded = 58
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Vitamina A"
                        obs.value_coded = 1720
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Vitamina B"
                        obs.value_coded = 329
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                    Case "Vitamina B12"
                        'Falta Implementar isto
                    Case "Vitamina C"
                        'Falta Implementar isto
                    Case "Zidovudine"
                        obs.value_coded = 797
                        obs.obs_datetime = dataTratamento
                        seguimentos.Add(obs)
                End Select
                obs = New Obs
                rs.MoveNext()
            End While
        End If
        Return seguimentos
    End Function

    Public Shared Function importInfeccoesOportunisticas(ByVal fonte As Connection, ByVal IdSeguimento As Integer) As ArrayList
        Dim infeccoes As New ArrayList
        Dim cmmFonte As New Command 'Acess
        Dim rs As New Recordset
        cmmFonte.ActiveConnection = fonte
        cmmFonte.CommandType = CommandTypeEnum.adCmdText
        cmmFonte.CommandText = "SELECT t_infeccoesoportunisticaseguimento.codigoio, t_infeccoesoportunisticaseguimento.estadiooms, t_infeccoesoportunisticaseguimento.data, t_paciente.tipopaciente " & _
                                "FROM (t_paciente INNER JOIN t_seguimento ON t_paciente.nid = t_seguimento.nid) INNER JOIN t_infeccoesoportunisticaseguimento ON t_seguimento.idseguimento = t_infeccoesoportunisticaseguimento.idseguimento " & _
                                " where t_infeccoesoportunisticaseguimento.idseguimento=" & IdSeguimento & ""
        rs = cmmFonte.Execute
        Dim codInfeccao As String
        Dim estadioOms As String
        Dim tipoPaciente As String
        Dim dataInfeccao As Date
        Dim obs As New Obs

        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF
                codInfeccao = PatientUtils.verificaNulo(rs, "codigoio")
                tipoPaciente = PatientUtils.verificaNulo(rs, "tipopaciente")
                estadioOms = PatientUtils.verificaNulo(rs, "estadiooms")
                If Not IsDBNull(rs.Fields.Item("data").Value) Then
                    dataInfeccao = rs.Fields.Item("data").Value
                End If

                If tipoPaciente = "Adulto" Then
                    Select Case estadioOms
                        Case "I"
                            obs.value_coded = 5327
                            obs.concept_id = 1564
                            obs.obs_datetime = dataInfeccao
                            infeccoes.Add(obs)
                        Case "II"
                            Select Case codInfeccao
                                Case "Herpes Zoster nos últimos 5 anos"
                                    obs.value_coded = 5329
                                    obs.concept_id = 1565
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Infecções respiratórias recorrentes"
                                    obs.value_coded = 5012
                                    obs.concept_id = 1565
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Manifestações mucocutânicas menores"
                                    obs.value_coded = 5330
                                    obs.concept_id = 1565
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Perda de Peso<10%"
                                    obs.value_coded = 5332
                                    obs.concept_id = 1565
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                            End Select
                        Case "III"
                            Select Case codInfeccao
                                Case "Candidíase Oral"
                                    obs.concept_id = 1566
                                    obs.value_coded = 5334
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Diarreia crónica > 1 mês"
                                    obs.concept_id = 1566
                                    obs.value_coded = 5018
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Febre > 1 mês"
                                    obs.concept_id = 1566
                                    obs.value_coded = 5027
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Infecções Bacteriana severas"
                                    obs.concept_id = 1566
                                    obs.value_coded = 5333
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Leucoplasia pilosa"
                                    obs.concept_id = 1566
                                    obs.value_coded = 5337
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Na cama < 50% do tempo"
                                    obs.concept_id = 1566
                                    obs.value_coded = 1567
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Perda de Peso > 10%"
                                    obs.concept_id = 1566
                                    obs.value_coded = 5339
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Tuberculose pulmonar"
                                    obs.concept_id = 1566
                                    obs.value_coded = 5338
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Vulvovaginite candidiásica > 1mês"
                                    obs.concept_id = 1566
                                    obs.value_coded = 298
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                            End Select
                        Case "IV"
                            Select Case codInfeccao
                                Case "Candidíase esofágica"
                                    obs.concept_id = 1569
                                    obs.value_coded = 5340
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Carcinoma invasivo do colo do útero"
                                    obs.concept_id = 1569
                                    obs.value_coded = 1570
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Citomegalovirose"
                                    obs.concept_id = 1569
                                    obs.value_coded = 5035
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Criptococose extrapulmonar"
                                    obs.concept_id = 1569
                                    obs.value_coded = 5033
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Criptosporidiose, isosporidiase"
                                    obs.concept_id = 1569
                                    obs.value_coded = 5034
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Demência / Encefalopatia"
                                    obs.concept_id = 1569
                                    obs.value_coded = 5345
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Herpes simples > 1 mês ou visceral"
                                    obs.concept_id = 1569
                                    obs.value_coded = 5344
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Leucoencefalite multifocal progressiva"
                                    obs.concept_id = 1569
                                    obs.value_coded = 5046
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Linfoma"
                                    obs.concept_id = 1569
                                    obs.value_coded = 5041
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Micobacteriose atípica"
                                    obs.concept_id = 1569
                                    obs.value_coded = 5043
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Na cama > 50% do tempo"
                                    obs.concept_id = 1569
                                    obs.value_coded = 1568
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Sarcoma de Kaposi"
                                    obs.concept_id = 1569
                                    obs.value_coded = 507
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Sindroma caquético"
                                    obs.concept_id = 1569
                                    obs.value_coded = 823
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "toxoplasmose cerebral"
                                    obs.concept_id = 1569
                                    obs.value_coded = 5355
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Tuberculose extrapulmonar"
                                    obs.concept_id = 1569
                                    obs.value_coded = 5042
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                            End Select

                    End Select
                ElseIf tipoPaciente = "Crianca" Or tipoPaciente = "Criança" Then
                    Select Case estadioOms
                        Case "I"
                            Select Case codInfeccao
                                Case "Assintomático", "Assintomatico"
                                    obs.concept_id = 1558
                                    obs.value_coded = 5327
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Hepato-esplenomegalia"
                                    obs.concept_id = 1561
                                    obs.value_coded = 825
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Linfadenopatia generalizada", "Linfadenopatia generalizada persistente"
                                    obs.concept_id = 1558
                                    obs.value_coded = 5328
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)

                            End Select
                        Case "II"
                            Select Case codInfeccao
                                Case "Aumento das parótidas inexplicado"
                                    obs.concept_id = 1561
                                    obs.value_coded = 1210
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Candidíase oral (> 2 episódios)"
                                    obs.concept_id = 1561
                                    obs.value_coded = 5334
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Diarreia Crónica > 1 mês"
                                    obs.concept_id = 1561
                                    obs.value_coded = 5018
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Eritema gengival linear"
                                    obs.concept_id = 1561
                                    obs.value_coded = 2064
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Febre > 1 mês"
                                    obs.concept_id = 1561
                                    obs.value_coded = 5027
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "HEM persistente inexplicada"
                                    obs.concept_id = 1561
                                    obs.value_coded = 825
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Herpes Zoster"
                                    obs.concept_id = 1561
                                    obs.value_coded = 5329
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Infecção viral extensa", "Infecções ungueais fúngicas", "Molusco contagioso extenso", "Prurigo"
                                    obs.concept_id = 1561
                                    obs.value_coded = 1212
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Infecções bacterianas graves de repetição(>2)"
                                    obs.concept_id = 1561
                                    obs.value_coded = 5030
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "IVRS"
                                    obs.concept_id = 1561
                                    obs.value_coded = 5012
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Parotidite crónica"
                                    obs.concept_id = 1561
                                    obs.value_coded = 1210
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Perda de Peso > 10%"
                                    obs.concept_id = 1561
                                    obs.value_coded = 5339
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Pneumonia Bacteriana (>2/1 ano)"
                                    obs.concept_id = 1561
                                    obs.value_coded = 1215
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "TP"
                                    obs.concept_id = 1561
                                    obs.value_coded = 42
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Ulcerações orais recurrentes"
                                    obs.concept_id = 1561
                                    obs.value_coded = 2065
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                            End Select
                        Case "III"
                            Select Case codInfeccao
                                Case "Anemia, neutropenia, Trombocitopenia inexplicadas"
                                    obs.concept_id = 1562
                                    obs.value_coded = 1217
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Candidíase no esófago"
                                    obs.concept_id = 1562
                                    obs.value_coded = 5340
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Candidíase oral persistente"
                                    obs.concept_id = 1562
                                    obs.value_coded = 5334
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Diarreia persistente inexplicada (>14d)"
                                    obs.concept_id = 1562
                                    obs.value_coded = 5018
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Doença pulmonar crónica incluindo bronquietasias"
                                    obs.concept_id = 1562
                                    obs.value_coded = 1295
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Encefalopatia por HIV"
                                    obs.concept_id = 1562
                                    obs.value_coded = 5046
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Falência de Crescimento ou Mal nutrição grave"
                                    obs.concept_id = 1562
                                    obs.value_coded = 5050
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Febre persistente inexplicada (>1m)"
                                    obs.concept_id = 1562
                                    obs.value_coded = 5027
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Gengivite/periodontite ulcerativa necrotizante"
                                    obs.concept_id = 1562
                                    obs.value_coded = 126
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "HSV > 1 mês ou visceral"
                                    obs.concept_id = 1562
                                    obs.value_coded = 5344
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Leucoplasia oral pilosa"
                                    obs.concept_id = 1562
                                    obs.value_coded = 5337
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Linfoma"
                                    obs.concept_id = 1562
                                    obs.value_coded = 5041
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "LIP", "Pneumonia intersticial linfóide (LIP) sintomática"
                                    obs.concept_id = 1562
                                    obs.value_coded = 5024
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Malnutrição moderada inexplicada"
                                    obs.concept_id = 1562
                                    obs.value_coded = 68
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Pcp", "PCP"
                                    obs.concept_id = 1562
                                    obs.value_coded = 882
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Pneumonia bacteriana grave de repetição"
                                    obs.concept_id = 1562
                                    obs.value_coded = 1215
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Sarcoma de Kaposi"
                                    obs.concept_id = 2066
                                    obs.value_coded = 507
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Septicétima Recurrente"
                                    obs.concept_id = 1562
                                    obs.value_coded = 5354
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "TB extrapulmonar"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5042
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Tuberculose ganglionar/pulmonar"
                                    obs.concept_id = 1562
                                    obs.value_coded = 42
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                            End Select
                        Case "IV"
                            Select Case codInfeccao
                                Case "Candidiase esofágica (ou traqueal/pulmonar)"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5340
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Cardiomiopatia associada ao HIV sintomática"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5025
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Criptococose extrapulmonar"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5033
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Criptosporidiose crónica"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5034
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Encefalopatia por HIV"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5345
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Fístula recto-vaginal associada ao HIV"
                                    obs.concept_id = 2066
                                    obs.value_coded = 1218
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Infecção crónica pelo herpes simples vírus"
                                    obs.concept_id = 2066
                                    obs.value_coded = 1216
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Infecção micobactéria não tuberculosa disseminada"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5044
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Infecção por Citomegalovirus"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5035
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Infecções bacterianas severas de repetição"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5333
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Isosporiose crónica"
                                    obs.concept_id = 2066
                                    obs.value_coded = 1570
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Leucoencefalopatia multifocal progressiva"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5046
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Linfoma não Hodgkin"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5041
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Malnutrição grave ou perda de peso severa"
                                    obs.concept_id = 2066
                                    obs.value_coded = 1844
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Micose disseminada (Histoplasma, etc)"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5350
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Nefropatia associada ao HIV sintomática"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5025
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "PCP"
                                    obs.concept_id = 2066
                                    obs.value_coded = 882
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Sarcoma de Kaposi"
                                    obs.concept_id = 2066
                                    obs.value_coded = 507
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Toxoplasmose do SNC"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5355
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                                Case "Tuberculose extrapulmonar disseminada"
                                    obs.concept_id = 2066
                                    obs.value_coded = 5042
                                    obs.obs_datetime = dataInfeccao
                                    infeccoes.Add(obs)
                            End Select
                    End Select

                End If
                obs = New Obs
                rs.MoveNext()
            End While
        End If
        Return infeccoes
    End Function

    Public Shared Function importObservacoes(ByVal fonte As Connection, ByVal nid As String, ByVal dataSeguimento As Date) As ArrayList
        Dim observacoes As New ArrayList
        Dim cmmFonte As New Command 'Acessg
        Dim rs As New Recordset
        cmmFonte.ActiveConnection = fonte
        'MsgBox(dataSeguimento.Month)
        cmmFonte.CommandType = CommandTypeEnum.adCmdText
        cmmFonte.CommandText = "SELECT t_observacaopaciente.codobservacao, t_observacaopaciente.codestado, " & _
                                "t_observacaopaciente.data, t_observacaopaciente.valor " & _
                                "FROM t_observacaopaciente " & _
                                " where t_observacaopaciente.nid='" & nid & "' and t_observacaopaciente.data=cdate('" & dataSeguimento & "') and " & _
                                " t_observacaopaciente.data not in (Select t_observacaodata.data from t_observacaodata where nid='" & nid & "' )"
        rs = cmmFonte.Execute
        Dim codObs As String
        Dim estadoObs As String
        Dim valorObs As String
        Dim dataObs As Date
        Dim obs As New Obs

        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF
                codObs = PatientUtils.verificaNulo(rs, "codobservacao")
                estadoObs = PatientUtils.verificaNulo(rs, "codestado")
                valorObs = PatientUtils.verificaNulo(rs, "valor")
                If Not IsDBNull(rs.Fields.Item("data").Value) Then
                    dataObs = rs.Fields.Item("data").Value
                End If
                If Not String.IsNullOrEmpty(valorObs) Then
                    valorObs = valorObs.Replace(" ", "")
                End If

                Select Case codObs
                    Case "Altura", "altura"
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
                    Case "Peso", "peso", "Peso-Criança", "Peso-Crianca"
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
                    Case "Temperatura", "temperatura", "Te", "te"
                        If Not String.IsNullOrEmpty(valorObs) Then
                            valorObs = valorObs.Replace(";", ".")
                            valorObs = valorObs.Replace(",", ".")
                            valorObs = valorObs.Replace("/", ".")
                            obs.concept_id = 5088
                            obs.value_numeric = valorObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TNumeric
                            observacoes.Add(obs)
                        End If
                    Case "PC", "pc", "Pc", "pC"
                        If Not String.IsNullOrEmpty(valorObs) Then
                            obs.concept_id = 5314
                            obs.value_numeric = valorObs
                            obs.obs_datetime = dataObs
                            obs.data_Type = ObsDataType.TNumeric
                            observacoes.Add(obs)
                        End If
                    Case "Tensão Arterial", "Tensao Arterial"
                        If Not String.IsNullOrEmpty(valorObs) Then
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
                                observacoes.Add(tempObsInferior)

                                tempObsSuperior.concept_id = 5086
                                tempObsSuperior.value_numeric = inferior
                                tempObsSuperior.obs_datetime = dataObs
                                tempObsSuperior.data_Type = ObsDataType.TNumeric
                                observacoes.Add(tempObsSuperior)
                            Else
                                'If estadoObs = "Inferior" Then
                                '    obs.concept_id = 5085
                                '    obs.value_numeric = valorObs
                                '    obs.obs_datetime = dataObs
                                '    obs.data_Type = ObsDataType.TNumeric
                                '    sinaisVitais.Add(obs)
                                'ElseIf estadoObs = "Superior" Then
                                '    obs.concept_id = 5086
                                '    obs.value_numeric = valorObs
                                '    obs.obs_datetime = dataObs
                                '    obs.data_Type = ObsDataType.TNumeric
                                '    sinaisVitais.Add(obs)
                                'End If

                                Dim tempObsD As New Obs
                                Dim tempObsS As New Obs

                                tempObsD.concept_id = 5085
                                tempObsD.value_numeric = valorObs
                                tempObsD.obs_datetime = dataObs
                                tempObsD.data_Type = ObsDataType.TNumeric
                                observacoes.Add(tempObsD)

                                tempObsS.concept_id = 5086
                                tempObsS.value_numeric = valorObs
                                tempObsS.obs_datetime = dataObs
                                tempObsS.data_Type = ObsDataType.TNumeric
                                observacoes.Add(tempObsS)
                            End If

                        End If
                End Select
                obs = New Obs
                rs.MoveNext()
            End While
        End If
        Return observacoes
    End Function

    Public Shared Function importDiagnostico(ByVal fonte As Connection, ByVal IdSeguimento As Integer) As ArrayList
        Dim diagnosticos As New ArrayList
        Dim cmmFonte As New Command 'Acess
        Dim rs As New Recordset
        cmmFonte.ActiveConnection = fonte
        cmmFonte.CommandType = CommandTypeEnum.adCmdText
        cmmFonte.CommandText = "SELECT t_diagnosticoseguimento.coddiagnostico, t_diagnosticoseguimento.observacao, t_diagnosticoseguimento.data " & _
                                "FROM t_diagnosticoseguimento " & _
                                " where t_diagnosticoseguimento.idseguimento=" & IdSeguimento & ""
        rs = cmmFonte.Execute
        Dim codDiag As String

        Dim dataDiag As Date
        Dim obs As New Obs

        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF
                codDiag = PatientUtils.verificaNulo(rs, "coddiagnostico")
                If Not String.IsNullOrEmpty(codDiag) Then
                    obs.data_Type = ObsDataType.TCoded
                    obs.concept_id = 1406

                    Select Case codDiag
                        Case "ADPM"
                            obs.value_coded = 8607
                        Case "ANEMIA"
                            obs.value_coded = 3
                        Case "CANDIDÍASE ORAL"
                            obs.value_coded = 5334
                        Case "DERMATITE ATOPICA"
                            obs.value_coded = 11385
                        Case "DERMATITE SEBORREICA"
                            obs.value_coded = 11389
                        Case "DESNUTRIÇÃO AGUDA GRAVE (DAG)"
                            obs.value_coded = 1844
                        Case "DESNUTRIÇÃO AGUDA MODERADA (DAM)"
                            obs.value_coded = 68
                        Case "Diarreia"
                            obs.value_coded = 16
                        Case "ENCEFALOPATIA POR HIV"
                            obs.value_coded = 6990
                        Case "ESTOMATITE HERPETICA"
                            obs.value_coded = 10939
                        Case "EXTRAPULMONAR", "TB - EXTRAPULMONAR"
                            obs.value_coded = 5042
                        Case "FALENCIA DE CRESCIMENTO"
                            obs.value_coded = 5050
                        Case "FILHO DE MAE HIV+"
                            obs.value_coded = 1586
                        Case "GEA"
                            obs.value_coded = 197
                        Case "GIARDIASE"
                            obs.value_coded = 6496
                        Case "HERPES ZOSTER"
                            obs.value_coded = 836
                        Case "HIV/SIDA"
                            obs.value_coded = 894
                        Case "IVRS"
                            obs.value_coded = 10588
                        Case "KW"
                            obs.value_coded = 8600
                        Case "KW-MARASMATICO"
                            obs.value_coded = 8602
                        Case "LIP"
                            obs.value_coded = 10942
                        Case "Malaria"
                            obs.value_coded = 123
                        Case "MARASMO"
                            obs.value_coded = 8601
                        Case "OTITE"
                            obs.value_coded = 218
                        Case "PAROTIDITE CRONICA"
                            obs.value_coded = 1540
                        Case "PCP"
                            obs.value_coded = 7187
                        Case "PIODERMITE"
                            obs.value_coded = 11354
                        Case "PNEUMONIA (BPN/PN)"
                            obs.value_coded = 43
                        Case "PRURIGO"
                            obs.value_coded = 879
                        Case "SARCOMA KAPOSI (SK)"
                            obs.value_coded = 507
                        Case "SINDROMES NEUROLÓGICOS"
                            obs.value_coded = 8398
                        Case "TB", "TB DISSEMINADA"
                            obs.value_coded = 5042
                        Case "TB PULMONAR BK NEG", "TB PULMONAR BK POS"
                            obs.value_coded = 42
                        Case "TINEA CAPITIS"
                            obs.value_coded = 7047
                        Case Else
                            obs.concept_id = 1649
                            obs.data_Type = ObsDataType.TText
                            obs.value_text = codDiag
                    End Select

                    If Not IsDBNull(rs.Fields.Item("data").Value) Then
                        dataDiag = rs.Fields.Item("data").Value
                    End If

                    obs.obs_datetime = dataDiag

                    diagnosticos.Add(obs)

                    obs = New Obs
                End If
                rs.MoveNext()
            End While
        End If
        Return diagnosticos
    End Function


    Public Shared Function ImportTuberculoseTratamento(ByVal fonte As Connection, ByVal IdSeguimento As Integer) As ArrayList
        
        Try

            Dim cmmFonte As New Command 'Acess

            Dim rs As New Recordset
            Dim dataInicial As Date
            Dim dataCorrente As Date
            Dim notSet As New ArrayList
            Dim obs As Obs


            rs.Open("Select distinct data,datafim from t_tratamentotb where idseguimento = " & IdSeguimento, fonte, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockReadOnly)



            If Not (rs.EOF And rs.BOF) Then
                
                rs.MoveFirst()

                While Not rs.EOF

                    If Not IsDBNull(rs.Fields.Item("data").Value) Then

                        dataInicial = rs.Fields.Item("data").Value

                        obs = New Obs
                        obs.concept_id = 1113
                        obs.data_Type = ObsDataType.TDatetime
                        obs.value_datetime = dataInicial
                        obs.obs_datetime = dataInicial
                        notSet.Add(obs)

                        If Not IsDBNull(rs.Fields.Item("datafim").Value) Then

                            dataCorrente = rs.Fields.Item("datafim").Value

                            obs = New Obs

                            obs.concept_id = 6120
                            obs.data_Type = ObsDataType.TDatetime
                            obs.obs_datetime = dataCorrente
                            obs.value_datetime = dataCorrente

                            notSet.Add(obs)

                        End If

                    End If

                    rs.MoveNext()
                End While
                rs.Close()
            End If
            Return notSet

        Catch ex As Exception
            MsgBox("Error Importing Treatment of Tuberculosis: " & ex.Message)
            Return New ArrayList
        End Try
    End Function





    Public Shared Sub importSeguimento(ByVal fonte As Connection, ByVal locationid As Int16)
        Dim patientID As Integer
        Dim encounter_id As Integer
        Dim nid As String

        Dim dataSeguimento As Date
        Dim idSeguimento As Integer
        'encounterID As Integer
        Dim estadioOms As String
        Dim dataProxima As Date
        Dim gravidaz As String
        Dim tipoPaciente, tiposeguimento As String
        Dim obs As New Obs

        Dim tratamentos As New ArrayList
        Dim infeccoes As New ArrayList
        Dim observacoes As New ArrayList
        Dim diagnosticos As New ArrayList
        Dim insertNextEncounter As Boolean


        'Try
        Dim cmmFonte As New Command 'Acess
        Dim rs As New Recordset
        'Dim cmmDestino As New MySqlCommand 'MySQL

        cmmFonte.CommandType = CommandTypeEnum.adCmdText
        cmmFonte.ActiveConnection = fonte
        If AllPatients Then
            cmmFonte.CommandText = "SELECT  Q_seguimento.idseguimento, Q_seguimento.nid, Q_seguimento.dataseguimento, Q_seguimento.estadiooms," & _
                                            "Q_seguimento.dataproximaconsulta,Q_seguimento.aleitamentomaterno,Q_seguimento.idadedesmame,Q_seguimento.aderente, " & _
                                            "Q_seguimento.tarvregime,Q_seguimento.interrompermotivo,Q_seguimento.Gravidez,Q_seguimento.screeningtb,Q_seguimento.screeningIts, " & _
                                            "Q_seguimento.aconselhado,Q_seguimento.referidoSectorClinico,Q_seguimento.gravida,Q_seguimento.dum,Q_seguimento.dpp,Q_seguimento.PTV_TARV, " & _
                                            "Q_seguimento.sintomasTb,Q_seguimento.dataParto,Q_seguimento.lactante,Q_seguimento.temperatura,Q_seguimento.tensaoInferior,Q_seguimento.tensaosuperior,Q_seguimento.tipopaciente, " & _
                                            "Q_seguimento.peso,Q_seguimento.altura,Q_seguimento.perimetroBraquial,Q_seguimento.imc,Q_seguimento.imcPorIdade,Q_seguimento.pesoPorEstatura,Q_seguimento.diagnosticoNutricional, " & _
                                            "Q_seguimento.tratamentoNutricional,Q_seguimento.densPsicoMotor,Q_seguimento.investigacaoTb,Q_seguimento.genexpert,Q_seguimento.datainiciotarv,Q_seguimento.tiposeguimento, " & _
                                            "Q_seguimento.tipotarv,Q_seguimento.codmudanca,Q_seguimento.codregime,Q_seguimento.motivoInicioTarv " & _
                                " FROM Q_seguimento order by Q_seguimento.nid "
        Else

            cmmFonte.CommandText = "SELECT  Q_seguimento.idseguimento, Q_seguimento.nid, Q_seguimento.dataseguimento, Q_seguimento.estadiooms," & _
                                            "Q_seguimento.dataproximaconsulta,Q_seguimento.aleitamentomaterno,Q_seguimento.idadedesmame,Q_seguimento.aderente, " & _
                                            "Q_seguimento.tarvregime,Q_seguimento.interrompermotivo,Q_seguimento.Gravidez,Q_seguimento.screeningtb,Q_seguimento.screeningIts, " & _
                                            "Q_seguimento.aconselhado,Q_seguimento.referidoSectorClinico,Q_seguimento.gravida,Q_seguimento.dum,Q_seguimento.dpp,Q_seguimento.PTV_TARV, " & _
                                            "Q_seguimento.sintomasTb,Q_seguimento.dataParto,Q_seguimento.lactante,Q_seguimento.temperatura,Q_seguimento.tensaoInferior,Q_seguimento.tensaosuperior,Q_seguimento.tipopaciente, " & _
                                            "Q_seguimento.peso,Q_seguimento.altura,Q_seguimento.perimetroBraquial,Q_seguimento.imc,Q_seguimento.imcPorIdade,Q_seguimento.pesoPorEstatura,Q_seguimento.diagnosticoNutricional, " & _
                                            "Q_seguimento.tratamentoNutricional,Q_seguimento.densPsicoMotor,Q_seguimento.investigacaoTb,Q_seguimento.genexpert,Q_seguimento.datainiciotarv,Q_seguimento.tiposeguimento, " & _
                                            "Q_seguimento.tipotarv,Q_seguimento.codmudanca,Q_seguimento.codregime,Q_seguimento.motivoInicioTarv " & _
                                " FROM Q_seguimento where nid in (" & whereQuery & ") order by Q_seguimento.nid "


           
        End If

        'cmmDestino.CommandType = CommandType.Text
        'cmmDestino.Connection = ConexaoOpenMRS3

        rs = cmmFonte.Execute

        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            
            While Not rs.EOF
                nid = rs.Fields.Item("nid").Value
                patientID = GetPatientOpenMRSIDByNID(nid) 'Get the openmrs patient_id using the NID

                If patientID > 0 Then

                    dataSeguimento = rs.Fields.Item("dataseguimento").Value
                    idSeguimento = rs.Fields.Item("idseguimento").Value
                    estadioOms = PatientUtils.verificaNulo(rs, "estadiooms")
                    gravidaz = PatientUtils.verificaNulo(rs, "Gravidez")

                    If Not IsDBNull(rs.Fields.Item("dataproximaconsulta").Value) Then
                        dataProxima = rs.Fields.Item("dataproximaconsulta").Value
                        insertNextEncounter = True
                    Else
                        dataProxima = Nothing
                        insertNextEncounter = False
                    End If

                    If Not IsDBNull(rs.Fields.Item("tipopaciente").Value) Then
                        tipoPaciente = rs.Fields.Item("tipopaciente").Value
                    Else
                        tipoPaciente = "Adulto"
                    End If

                    If Not IsDBNull(rs.Fields.Item("tiposeguimento").Value) Then
                        tiposeguimento = rs.Fields.Item("tiposeguimento").Value
                    Else
                        tiposeguimento = "Seguinte"
                    End If

                    If tipoPaciente = "Adulto" Or tipoPaciente = "adulto" Then

                        If tiposeguimento = "Seguinte" Then
                            encounter_id = EncounterDAO.insertEncounterByParam(6, patientID, locationid, 126, dataSeguimento, 3, 27)
                        Else
                            encounter_id = EncounterDAO.insertEncounterByParam(6, patientID, locationid, 126, dataSeguimento, 5, 27)
                        End If

                    ElseIf tipoPaciente = "Crianca" Or tipoPaciente = "Criança" Then
                        If tiposeguimento = "Seguinte" Then
                            encounter_id = EncounterDAO.insertEncounterByParam(9, patientID, locationid, 127, dataSeguimento, 3, 27)
                        Else
                            encounter_id = EncounterDAO.insertEncounterByParam(9, patientID, locationid, 127, dataSeguimento, 5, 27)
                        End If

                    End If

                    obs.date_created = Now.Date
                    obs.encounter_id = encounter_id
                    obs.location_id = locationid
                    obs.obs_datetime = dataSeguimento
                    obs.person_id = patientID
                    obs.voided = False

                    If Not String.IsNullOrEmpty(estadioOms) Then

                        obs.concept_id = 5356
                        obs.data_Type = ObsDataType.TCoded
                        If estadioOms = "I" Then
                            obs.value_coded = 1204
                        ElseIf estadioOms = "II" Then
                            obs.value_coded = 1205
                        ElseIf estadioOms = "III" Then
                            obs.value_coded = 1206
                        Else
                            obs.value_coded = 1207
                        End If
                        ObsDAO.insertObs(obs, False)

                    End If
                    If Not String.IsNullOrEmpty(gravidaz) Then
                        obs.value_numeric = gravidaz
                        obs.data_Type = ObsDataType.TNumeric
                        obs.concept_id = 5992
                        ObsDAO.insertObs(obs, False)
                    End If
                    If insertNextEncounter Then
                        obs.value_datetime = dataProxima
                        obs.data_Type = ObsDataType.TDatetime
                        obs.concept_id = 1410
                        ObsDAO.insertObs(obs, False)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "gravida")) Then
                        If rs.Fields.Item("gravida").Value = "-1" Then
                            obs.value_coded = 44
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 1982
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If
                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "aderente")) Then
                        If rs.Fields.Item("aderente").Value = "-1" Then
                            obs.value_coded = 1065
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 1714
                            ObsDAO.insertObs(obs, False)
                        ElseIf rs.Fields.Item("aderente").Value = "0" Then
                            obs.value_coded = 1066
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 1714
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "tensaoInferior")) Then
                        Dim tensaoInferior = rs.Fields.Item("tensaoInferior").Value
                        If tensaoInferior >= 58 And tensaoInferior <= 140 Then
                            obs.value_numeric = tensaoInferior
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 5086
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "tensaosuperior")) Then
                        Dim tensaosuperior = rs.Fields.Item("tensaosuperior").Value
                        If tensaosuperior >= 100 And tensaosuperior <= 260 Then
                            obs.value_numeric = tensaosuperior
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 5085
                            ObsDAO.insertObs(obs, False)
                        ElseIf tensaosuperior.ToString.Length = 5 Then
                            Dim tSup As Integer = tensaosuperior.ToString.Substring(0, 3)
                            Dim tInf As Integer = tensaosuperior.ToString.Substring(3, 2)

                            obs.value_numeric = tInf
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 5086
                            ObsDAO.insertObs(obs, False)

                            obs.value_numeric = tSup
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 5085
                            ObsDAO.insertObs(obs, False)
                        ElseIf tensaosuperior.ToString.Length = 6 Then
                            Dim tSup As Integer = tensaosuperior.ToString.Substring(0, 3)
                            Dim tInf As Integer = tensaosuperior.ToString.Substring(3, 3)

                            obs.value_numeric = tInf
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 5086
                            ObsDAO.insertObs(obs, False)

                            obs.value_numeric = tSup
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 5085
                            ObsDAO.insertObs(obs, False)

                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "temperatura")) Then
                        Dim temperatura = rs.Fields.Item("temperatura").Value
                        If temperatura >= 36 And temperatura <= 40 Then
                            obs.value_numeric = temperatura
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 5088
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "peso")) Then
                        Dim peso = rs.Fields.Item("peso").Value
                        If peso <= 250 Then
                            obs.value_numeric = peso
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 5089
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "altura")) Then
                        Dim altura = rs.Fields.Item("altura").Value
                        If altura < 20 Then
                            If altura.ToString.Contains(".") Then
                                altura = CInt(altura.ToString.Replace(".", ""))
                            End If
                        End If


                        If altura >= 50 And altura <= 200 Then
                            obs.value_numeric = altura
                            obs.data_Type = ObsDataType.TNumeric
                            obs.concept_id = 5090
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "perimetroBraquial")) Then
                        obs.value_numeric = rs.Fields.Item("perimetroBraquial").Value
                        obs.data_Type = ObsDataType.TNumeric
                        obs.concept_id = 1343
                        ObsDAO.insertObs(obs, False)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "imcPorIdade")) Then

                        Dim imcPorIdade As String = rs.Fields.Item("imcPorIdade").Value
                        Select Case imcPorIdade
                            Case "<-3 D"
                                obs.value_coded = 1844
                            Case "≥ -1 DP"
                                obs.value_coded = 1115
                            Case "≥ –2 e < –1 DP"
                                obs.value_coded = 6335
                            Case Else
                                obs.value_coded = 68
                        End Select

                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6336
                        ObsDAO.insertObs(obs, False)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "pesoPorEstatura")) Then
                        Dim pesoPorEstatura As String = rs.Fields.Item("pesoPorEstatura").Value
                        Select Case pesoPorEstatura
                            Case "<-3 D"
                                obs.value_coded = 1844
                            Case "≥ -1 DP"
                                obs.value_coded = 1115
                            Case "≥ –2 e < –1 DP"
                                obs.value_coded = 6335
                            Case Else
                                obs.value_coded = 68
                        End Select

                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 6336
                        ObsDAO.insertObs(obs, False)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "diagnosticoNutricional")) Then
                        Dim diagnosticoNutricional As String = rs.Fields.Item("diagnosticoNutricional").Value
                        If diagnosticoNutricional.Length > 3 Then
                            Select Case diagnosticoNutricional
                                Case "<-3 D"
                                    obs.value_coded = 1844
                                Case "≥ -1 DP"
                                    obs.value_coded = 1115
                                Case "≥ –2 e < –1 DP"
                                    obs.value_coded = 6335
                                Case Else
                                    obs.value_coded = 68
                            End Select

                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6336
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "tratamentoNutricional")) Then
                        Dim tratamentoNutricional As String = rs.Fields.Item("tratamentoNutricional").Value


                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 2152
                        obs.value_coded = 1065
                        ObsDAO.insertObs(obs, False)

                        Select Case tratamentoNutricional
                            Case "ATPU", "Plumpy Nut"
                                obs.value_coded = 1884
                            Case "CSB", "Cesta Basica"
                                obs.value_coded = 2151
                            Case "Papa Enriquecida", "Papa"
                                obs.value_coded = 2150
                            Case Else
                                obs.value_coded = 5622
                        End Select

                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 2152
                        ObsDAO.insertObs(obs, False)


                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "densPsicoMotor")) Then
                        If rs.Fields.Item("densPsicoMotor").Value Then
                            obs.value_coded = 1115
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 1882
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "investigacaoTb")) Then
                        If rs.Fields.Item("investigacaoTb").Value = "Negativo" Then
                            obs.value_coded = 664
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6277
                            ObsDAO.insertObs(obs, False)
                        ElseIf rs.Fields.Item("investigacaoTb").Value = "Positivo" Then
                            obs.value_coded = 703
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6277
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "datainiciotarv")) Then

                        obs.value_datetime = rs.Fields.Item("datainiciotarv").Value
                        obs.data_Type = ObsDataType.TDatetime
                        obs.concept_id = 1190
                        ObsDAO.insertObs(obs, False)

                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "sintomasTb")) Then
                        If rs.Fields.Item("sintomasTb").Value = "0" Then
                            obs.value_coded = 1066
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6257
                            ObsDAO.insertObs(obs, False)
                        ElseIf rs.Fields.Item("sintomasTb").Value = "-1" Then
                            obs.value_coded = 1065
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6257
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "screeningIts")) Then
                        If rs.Fields.Item("screeningIts").Value = "0" Then
                            obs.value_coded = 1066
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6258
                            ObsDAO.insertObs(obs, False)
                        ElseIf rs.Fields.Item("screeningIts").Value = "-1" Then
                            obs.value_coded = 1065
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6258
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "aconselhado")) Then
                        If rs.Fields.Item("aconselhado").Value = "0" Then
                            obs.value_coded = 1066
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 1714
                            ObsDAO.insertObs(obs, False)
                        ElseIf rs.Fields.Item("aconselhado").Value = "-1" Then
                            obs.value_coded = 1065
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 1714
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "referidoSectorClinico")) Then
                        Dim referidoSectorClinico As String = rs.Fields.Item("referidoSectorClinico").Value
                        Select Case referidoSectorClinico
                            Case "Apoio Psicosocial-AP", "Apoio Psicosocial"
                                obs.value_coded = 2044
                                obs.data_Type = ObsDataType.TCoded
                                obs.concept_id = 1272
                                ObsDAO.insertObs(obs, False)
                            Case "TB"
                                obs.value_coded = 5487
                                obs.data_Type = ObsDataType.TCoded
                                obs.concept_id = 1272
                                ObsDAO.insertObs(obs, False)
                            Case "Planeamento Familiar-PF"
                                obs.value_coded = 1940
                                obs.data_Type = ObsDataType.TCoded
                                obs.concept_id = 1272
                                ObsDAO.insertObs(obs, False)
                            Case "Internamento-I"
                                obs.value_coded = 5485
                                obs.data_Type = ObsDataType.TCoded
                                obs.concept_id = 1272
                                ObsDAO.insertObs(obs, False)
                            Case "Consulta Medica"
                                obs.value_coded = 1798
                                obs.data_Type = ObsDataType.TCoded
                                obs.concept_id = 1272
                                ObsDAO.insertObs(obs, False)
                            Case Else
                                obs.value_coded = 5622
                                obs.data_Type = ObsDataType.TCoded
                                obs.concept_id = 1272
                                ObsDAO.insertObs(obs, False)
                        End Select


                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "dum")) Then
                        obs.value_datetime = rs.Fields.Item("dum").Value
                        obs.data_Type = ObsDataType.TDatetime
                        obs.concept_id = 1465
                        ObsDAO.insertObs(obs, False)

                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "dpp")) Then
                        obs.value_datetime = rs.Fields.Item("dpp").Value
                        obs.data_Type = ObsDataType.TDatetime
                        obs.concept_id = 1600
                        ObsDAO.insertObs(obs, False)

                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "PTV_TARV")) Then
                        If rs.Fields.Item("PTV_TARV").Value = "PTV" Then
                            obs.value_coded = 1598
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6286
                            ObsDAO.insertObs(obs, False)
                        ElseIf rs.Fields.Item("PTV_TARV").Value = "TARV" Then
                            obs.value_coded = 6276
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6286
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If


                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "lactante")) Then
                        If rs.Fields.Item("lactante").Value Then
                            obs.value_coded = 1065
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6332
                            ObsDAO.insertObs(obs, False)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "codregime")) Then
                        obs.value_coded = FILAUtils.getRegimeTerapeuticoConceptID(rs.Fields.Item("codregime").Value)
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1087
                        ObsDAO.insertObs(obs, False)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "tipotarv")) Then
                        obs.value_coded = FILAUtils.getTipoTarvConceptID(rs.Fields.Item("tipotarv").Value)
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1255
                        ObsDAO.insertObs(obs, False)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "codmudanca")) Then
                        obs.value_coded = FILAUtils.getCodMudancaConceptID(rs.Fields.Item("codmudanca").Value)
                        obs.data_Type = ObsDataType.TCoded
                        obs.concept_id = 1792
                        ObsDAO.insertObs(obs, False)
                    End If

                    If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "motivoInicioTarv")) Then
                        Dim motivoInicio As String = rs.Fields.Item("motivoInicioTarv").Value

                        If motivoInicio = "Criterios Clinicos/Imunologicos" Then
                            obs.value_coded = 6333
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6334
                            ObsDAO.insertObs(obs, False)
                        ElseIf motivoInicio = "Grávida" Or motivoInicio = "Gravida" Then
                            obs.value_coded = 6331
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6334
                            ObsDAO.insertObs(obs, False)
                        ElseIf motivoInicio = "Lactante" Then
                            obs.value_coded = 6332
                            obs.data_Type = ObsDataType.TCoded
                            obs.concept_id = 6334
                            ObsDAO.insertObs(obs, False)
                        End If
                        
                    End If



                    tratamentos = importTratamento(fonte, idSeguimento)
                    For Each o As Obs In tratamentos
                        o.date_created = Now.Date
                        o.encounter_id = encounter_id
                        o.location_id = locationid
                        o.person_id = patientID
                        o.voided = False
                        o.concept_id = 1719
                        o.data_Type = ObsDataType.TCoded
                        ObsDAO.insertObs(o, False)
                        If o.value_coded = 916 Then
                            o.concept_id = 6121
                            o.value_coded = 1065
                            ObsDAO.insertObs(o, False)
                        End If

                        If o.value_coded = 656 Then
                            o.concept_id = 6122
                            o.value_coded = 1065
                            ObsDAO.insertObs(o, False)
                        End If
                    Next

                    infeccoes = importInfeccoesOportunisticas(fonte, idSeguimento)

                    For Each o As Obs In infeccoes
                        o.date_created = Now.Date
                        o.encounter_id = encounter_id
                        o.location_id = locationid
                        o.person_id = patientID
                        o.voided = False
                        o.data_Type = ObsDataType.TCoded
                        'MsgBox(o.concept_id & " / " & o.value_coded)
                        ObsDAO.insertObs(o, False)
                    Next

                    observacoes = importObservacoes(fonte, nid, dataSeguimento)
                    For Each o As Obs In observacoes
                        o.date_created = Now.Date
                        o.encounter_id = encounter_id
                        o.location_id = locationid
                        o.person_id = patientID
                        o.voided = False
                        o.data_Type = ObsDataType.TNumeric
                        ObsDAO.insertObs(o, False)
                    Next

                    diagnosticos = importDiagnostico(fonte, idSeguimento)

                    For Each o As Obs In diagnosticos
                        o.date_created = Now.Date
                        o.encounter_id = encounter_id
                        o.location_id = locationid
                        o.person_id = patientID
                        o.voided = False
                        ObsDAO.insertObs(o, False)
                    Next

                    diagnosticos.Clear()

                    diagnosticos = ImportTuberculoseTratamento(fonte, idSeguimento)

                    For Each o As Obs In diagnosticos
                        o.date_created = Now.Date
                        o.encounter_id = encounter_id
                        o.location_id = locationid
                        o.person_id = patientID
                        o.voided = False
                        ObsDAO.insertObs(o, False)
                        If o.concept_id = 1113 Then
                            Dim patientProgramTB As Integer = PatientProgamDAO.insertPatientProgramByParam(patientID, 5, o.value_datetime, locationid)
                            Dim patientStateTB As Integer = PatientProgamDAO.insertPatientStateByParam(patientProgramTB, 16, o.value_datetime)
                        End If
                        If o.concept_id = 6120 Then
                            Dim openPatientID As Integer = PatientProgamDAO.getPatientProgramID(patientID, 5)
                            Dim stateID As Integer = PatientProgamDAO.getPatientStateByProgramID(openPatientID, 16)
                            PatientProgamDAO.endPatientProgram(openPatientID, o.value_datetime)
                            PatientProgamDAO.endPatientState(stateID, o.value_datetime)
                            PatientProgamDAO.insertPatientStateByParam(openPatientID, 24, o.value_datetime)
                        End If
                    Next

                End If
                tratamentos.Clear()
                infeccoes.Clear()
                observacoes.Clear()
                diagnosticos.Clear()

                rs.MoveNext()
            End While
        End If
        'Catch ex As Exception
        '    MsgBox("Error Importing Seguimento: " & ex.Message)
        'End Try

    End Sub


End Class
