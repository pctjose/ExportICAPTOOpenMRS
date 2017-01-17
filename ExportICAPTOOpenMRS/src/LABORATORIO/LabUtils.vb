Imports ADODB
Imports MySql.Data.MySqlClient
Public Class LabUtils
    Public Shared Sub ImportLabReal(ByVal fonte As Connection, ByVal location As Int16)
        Dim cmmFonte As New Command
        Dim rs As New Recordset
        With cmmFonte
            .ActiveConnection = fonte
            .CommandType = CommandType.Text
            If AllPatients Then
                .CommandText = "Select distinct t_paciente.nid from t_paciente inner join t_resultadoslaboratorio on t_paciente.nid=t_resultadoslaboratorio.nid"
            Else
                .CommandText = "Select distinct t_paciente.nid from t_paciente inner join t_resultadoslaboratorio on t_paciente.nid=t_resultadoslaboratorio.nid where nid in (" & whereQuery & ")"
            End If

            rs = .Execute
            If Not (rs.EOF And rs.BOF) Then
                While Not rs.EOF
                    ImportLabs(fonte, location, rs.Fields.Item("nid").Value)
                    rs.MoveNext()
                End While

            End If
        End With
    End Sub

    Private Shared Sub ImportLabs(ByVal fonte As Connection, ByVal locationid As Int16, ByVal nid As String)
        Dim patientID As Integer

        Dim encounter_id As Integer

        Dim obsSet As New Obs
        Dim obsGroupId As Integer

        Try


            Dim rs As New Recordset
            Dim count As Int16 = 0

            Dim dataInicial As Date
            Dim dataCorrente As Date

            Dim codExame As String
            Dim codParametro As String
            Dim resultado As Double


            Dim set1632 As New ArrayList
            Dim set1633 As New ArrayList
            Dim set1639 As New ArrayList

            Dim set1723 As New ArrayList

            Dim notSet As New ArrayList

            Dim obs As Obs


            rs.Open("Select codexame,dataresultado,codparametro,resultado,obs from t_resultadoslaboratorio where dataresultado is not null and nid='" & nid & "' order by dataresultado", fonte, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockReadOnly)

            If Not (rs.EOF And rs.BOF) Then

                rs.MoveFirst()

                dataInicial = rs.Fields.Item("dataresultado").Value 'First Initial date

                patientID = GetPatientOpenMRSIDByNID(nid) 'get Current OpenMRS Patient ID

                If patientID > 0 Then


                    While Not rs.EOF


                        dataCorrente = rs.Fields.Item("dataresultado").Value

                        codExame = rs.Fields.Item("codexame").Value

                        If Not IsDBNull(rs.Fields.Item("codparametro").Value) Then
                            codParametro = rs.Fields.Item("codparametro").Value
                        Else
                            codParametro = ""
                        End If

                        If Not IsDBNull(rs.Fields.Item("resultado").Value) Then
                            resultado = rs.Fields.Item("resultado").Value
                        Else
                            resultado = 0
                        End If

                        codExame = codExame.ToUpper

                        codParametro = codParametro.ToUpper

                        If dataCorrente = dataInicial Then 'Resultados da mesma data, neste caso mesma consulta no openmrs
                            obs = New Obs
                            obs.obs_datetime = dataInicial
                            obs.location_id = locationid
                            obs.person_id = patientID
                            obs.date_created = Now
                            obs.voided = 0
                            Select Case codExame
                                Case "ALBUMINA"
                                    obs.concept_id = 848
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                Case "ALT", "GTP"
                                    obs.concept_id = 654
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                Case "ÁSCARIS", "ASCARIS"
                                    obs.concept_id = 1635
                                    obs.data_Type = ObsDataType.TCoded
                                    obs.value_coded = 1530
                                    notSet.Add(obs)
                                Case "AST", "GOT"
                                    obs.concept_id = 653
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                Case "BACILOSCOPIA"
                                    If Not String.IsNullOrEmpty(codParametro) Then
                                        If codParametro = "ND" Then
                                            obs.concept_id = 307
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 1067
                                            notSet.Add(obs)
                                        ElseIf codParametro = "NEGATIVO" Then
                                            obs.concept_id = 307
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 664
                                            notSet.Add(obs)
                                        ElseIf codParametro = "POSITIVO" Or codParametro = "POSETIVO" Then
                                            obs.concept_id = 307
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 703
                                            notSet.Add(obs)
                                        End If
                                    End If
                                Case "BACTERIOLOGIA FEZES"
                                    'AIDA FALTA IMPLEMENTAR ISTO
                                Case "BILIRRUBINA DIREITA", "BILIRRUBINA D", "BILIRRUBINA C"
                                    obs.concept_id = 1297
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                Case "BILIRRUBINA T"
                                    obs.concept_id = 655
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                Case "CÁLCIO"
                                    'FALTA IMPLEMENTAR ISTO
                                Case "CARGA VIRAL"
                                    If Not IsDBNull(rs.Fields.Item("obs").Value) Then
                                        obs.concept_id = 856
                                        obs.data_Type = ObsDataType.TNumeric
                                        obs.value_numeric = -20
                                        set1632.Add(obs)
                                    Else
                                        If Not String.IsNullOrEmpty(codParametro) Then
                                            If codParametro = "CARGA VIRAL ABSOLUTA" Then
                                                obs.concept_id = 856
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1632.Add(obs)
                                            ElseIf codParametro = "CARGA VIRAL LOG" Then
                                                obs.concept_id = 1518
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1632.Add(obs)
                                            End If
                                        End If
                                    End If

                                Case "CD4"
                                    If Not String.IsNullOrEmpty(codParametro) Then
                                        If codParametro = "CD4 ABSOLUTO" Then
                                            obs.concept_id = 5497
                                            obs.data_Type = ObsDataType.TNumeric
                                            obs.value_numeric = resultado
                                            set1639.Add(obs)
                                        ElseIf codParametro = "CD4 PERCENTUAL" Then
                                            obs.concept_id = 730
                                            obs.data_Type = ObsDataType.TNumeric
                                            obs.value_numeric = resultado
                                            set1639.Add(obs)
                                        End If
                                    End If
                                Case "CÉLULAS EPITELIAIS"
                                    obs.concept_id = 1619
                                    obs.data_Type = ObsDataType.TCoded
                                    obs.value_coded = 666
                                    notSet.Add(obs)
                                Case "CILINDROS"
                                    obs.concept_id = 1619
                                    obs.data_Type = ObsDataType.TCoded
                                    obs.value_coded = 1524
                                    notSet.Add(obs)
                                Case "CL", "CLORITE"
                                    obs.concept_id = 1134
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                Case "CREATININA"
                                    obs.concept_id = 790
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                Case "E"
                                    obs.concept_id = 1024
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1723.Add(obs)
                                Case "E. HYSTOLÍTICA"
                                    obs.concept_id = 1635
                                    obs.data_Type = ObsDataType.TCoded
                                    obs.value_coded = 1529
                                    notSet.Add(obs)
                                Case "ECOGRAFIA"
                                    If Not String.IsNullOrEmpty(codParametro) Then
                                        If codParametro = "ND" Then
                                            obs.concept_id = 2054
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 1067
                                            notSet.Add(obs)
                                        ElseIf codParametro = "NEGATIVO" Then
                                            obs.concept_id = 2054
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 664
                                            notSet.Add(obs)
                                        Else
                                            obs.concept_id = 2054
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 703
                                            notSet.Add(obs)
                                        End If
                                    End If
                                Case "ERITROGRAMA"
                                    If Not String.IsNullOrEmpty(codParametro) Then
                                        Select Case codParametro
                                            Case "C. HB. GLOB. MÉDIA"
                                                obs.concept_id = 1017
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "ERITÓCITOS"
                                                'AINDA FALTA IMPLEMENTAR ISTO
                                            Case "HEMATRÓCRITO", "HEMATÓCRITO", "HEMATOCRITO", "HEMATROCRITO", "HTC"
                                                obs.concept_id = 1015
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "HEMOGLOBINA", "HEMOGLOBINA (HB)"
                                                obs.concept_id = 21
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "HG. GLOB. MÉDIA"
                                                obs.concept_id = 1018
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "VOL. GLOB. MÉDIO", "VGM"
                                                obs.concept_id = 851
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)

                                        End Select
                                    End If
                                Case "FA"
                                    obs.concept_id = 1521
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    notSet.Add(obs)
                                Case "FERRO"
                                    'FALTA IMPLEMENTAR ISTO
                                Case "GB"
                                    obs.concept_id = 678
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1723.Add(obs)
                                Case "GGT"
                                    obs.concept_id = 2077
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1723.Add(obs)
                                Case "GIARDIA"
                                    obs.concept_id = 1635
                                    obs.data_Type = ObsDataType.TCoded
                                    obs.value_coded = 1527
                                    notSet.Add(obs)
                                Case "GLC", "GLUCOSE", "GLICOSE"
                                    obs.concept_id = 887
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                Case "GLOBULINAS"
                                    obs.concept_id = 1520
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                    'Case "GLICEMIA"
                                Case "HEMOGLOBINA", "HGB", "HEMOGLOBINA (HB)"
                                    obs.concept_id = 21
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1723.Add(obs)
                                Case "HEMOGRAMA", "HGM"
                                    If Not String.IsNullOrEmpty(codParametro) Then
                                        Select Case codParametro
                                            Case "LINFÓCITOS ABSOLUTO", "LINFOCITOS ABSOLUTO"
                                                obs.concept_id = 952
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "LINFÓCITOS PERCENTUAL", "LINFOCITOS PERCENTUAL"
                                                obs.concept_id = 1021
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "NEUTRÓFILOS ABSOLUTO", "NEUTROFILOS ABSOLUTO"
                                                obs.concept_id = 1330
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "NEUTRÓFILOS PERCENTUAL", "NEUTROFILOS PERCENTUAL"
                                                obs.concept_id = 1022
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                        End Select
                                    End If
                                Case "HTC", "HCT"
                                    obs.concept_id = 1015
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1723.Add(obs)
                                Case "IGG ANTI-HIV"
                                    obs.concept_id = 1519
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1639.Add(obs)
                                Case "K", "POTASSIO", "POTÁSSIO"
                                    obs.concept_id = 1133
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                Case "LCR PATALÓGICO", "LCR PATOLOGICO"
                                    'FALTA IMPLEMENTAR ISTO
                                Case "LDH"
                                    obs.concept_id = 1014
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                Case "LEUCOGRAMA"
                                    If Not String.IsNullOrEmpty(codParametro) Then
                                        Select Case codParametro
                                            Case "BASÓFILOS", "BASOFILOS"
                                                obs.concept_id = 1025
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "EOSINÓFILOS", "EOSINOFILOS"
                                                obs.concept_id = 1024
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "LEUCÓCITOS", "LEUCOCITOS"
                                                obs.concept_id = 678
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "LINFÓCITOS", "LINFOCITOS", "L"
                                                obs.concept_id = 1021
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "LINFÓCITOS ABSOLUTO", "LINFOCITOS ABSOLUTO"
                                                obs.concept_id = 952
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "LINFÓCITOS PERCENTUAL", "LINFOCITOS PERCENTUAL"
                                                obs.concept_id = 1021
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "MONÓCITOS", "MONOCITOS"
                                                obs.concept_id = 1023
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "NEUTRÓFILOS", "NEUTROFILOS", "N"
                                                obs.concept_id = 1022
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "NEUTRÓFILOS ABSOLUTO", "NEUTROFILOS ABSOLUTO"
                                                obs.concept_id = 1330
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                            Case "NEUTRÓFILOS PERCENTUAL", "NEUTROFILOS PERCENTUAL"
                                                obs.concept_id = 1022
                                                obs.data_Type = ObsDataType.TNumeric
                                                obs.value_numeric = resultado
                                                set1723.Add(obs)
                                        End Select
                                    End If
                                Case "LINFÓCITOS", "LINFOCITOS"
                                    obs.concept_id = 1021
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1723.Add(obs)
                                Case "MCH"
                                    obs.concept_id = 1018
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1723.Add(obs)
                                Case "N"
                                    obs.concept_id = 857
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1723.Add(obs)
                                Case "NA"
                                    obs.concept_id = 1132
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                Case "PARACENTESE"
                                    If String.IsNullOrEmpty(codParametro) Then
                                        If codParametro = "ND" Then
                                            obs.concept_id = 1770
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 1067
                                            notSet.Add(obs)
                                        ElseIf codParametro = "NEGATIVO" Then
                                            obs.concept_id = 1770
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 664
                                            notSet.Add(obs)
                                        ElseIf codParametro = "POSITIVO" Or codParametro = "POSETIVO" Then
                                            obs.concept_id = 1770
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 703
                                            notSet.Add(obs)
                                        End If
                                    End If
                                Case "PIÓCITOS", "PIOCITOS"
                                    obs.concept_id = 1619
                                    obs.data_Type = ObsDataType.TCoded
                                    obs.value_coded = 1525
                                    notSet.Add(obs)
                                Case "PLAQUETAS", "PLQ"
                                    obs.concept_id = 729
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1723.Add(obs)
                                Case "PLASMODIUM"
                                    If Not String.IsNullOrEmpty(codParametro) Then
                                        If codParametro = "NEGATIVO" Then
                                            obs.concept_id = 32
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 664
                                            notSet.Add(obs)
                                        ElseIf codParametro = "POSITIVO" Or codParametro = "POSETIVO" Then
                                            obs.concept_id = 32
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 703
                                            notSet.Add(obs)
                                        ElseIf codParametro = "INDETERMINADO" Then
                                            obs.concept_id = 32
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 1138
                                            notSet.Add(obs)
                                        End If
                                    End If

                                Case "PROTEÍNAS TOTAIS", "PROTEINAS TOTAIS"
                                    obs.concept_id = 717
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                Case "RADIOGRAFIA"
                                    If String.IsNullOrEmpty(codParametro) Then
                                        If codParametro = "POSITIVO" Or codParametro = "POSETIVO" Then
                                            obs.concept_id = 12
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 703
                                            notSet.Add(obs)
                                        Else
                                            obs.concept_id = 12
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 664
                                            notSet.Add(obs)
                                        End If
                                    End If
                                Case "RPR"
                                    If Not String.IsNullOrEmpty(codParametro) Then
                                        If codParametro = "NEGATIVO" Then
                                            obs.concept_id = 1655
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 1229
                                            set1639.Add(obs)
                                        ElseIf codParametro = "POSITIVO" Or codParametro = "POSETIVO" Then
                                            obs.concept_id = 1655
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 1228
                                            set1639.Add(obs)
                                        End If
                                    End If
                                Case "RX TÓRAX", "RX TORAX"
                                    If Not String.IsNullOrEmpty(codParametro) Then
                                        Select Case codParametro
                                            Case "ADENOPATIAS AUMENTADAS"
                                                obs.concept_id = 12
                                                obs.data_Type = ObsDataType.TCoded
                                                obs.value_coded = 1645
                                                notSet.Add(obs)
                                            Case "CONDENSAÇÕES"
                                                obs.concept_id = 12
                                                obs.data_Type = ObsDataType.TCoded
                                                obs.value_coded = 1644
                                                notSet.Add(obs)
                                            Case "DERRAMES"
                                                obs.concept_id = 12
                                                obs.data_Type = ObsDataType.TCoded
                                                obs.value_coded = 1647
                                                notSet.Add(obs)
                                            Case "INFILTRADO RETICULAR"
                                                obs.concept_id = 12
                                                obs.data_Type = ObsDataType.TCoded
                                                obs.value_coded = 1643
                                                notSet.Add(obs)
                                            Case "INFILTRADO RETICULONODULAR"
                                                obs.concept_id = 12
                                                obs.data_Type = ObsDataType.TCoded
                                                obs.value_coded = 1641
                                                notSet.Add(obs)
                                            Case "MEDIASTINO ALARGADO"
                                                obs.concept_id = 12
                                                obs.data_Type = ObsDataType.TCoded
                                                obs.value_coded = 1646
                                                notSet.Add(obs)
                                            Case "OUTROS", "OUTRO"
                                                obs.concept_id = 12
                                                obs.data_Type = ObsDataType.TCoded
                                                obs.value_coded = 5622
                                                notSet.Add(obs)
                                            Case "SEM ALTERAÇÕES", "SEM ALTERACOES"
                                                obs.concept_id = 12
                                                obs.data_Type = ObsDataType.TCoded
                                                obs.value_coded = 1374
                                                notSet.Add(obs)
                                        End Select
                                    End If
                                Case "STRONGILOIDES"
                                    obs.concept_id = 1635
                                    obs.data_Type = ObsDataType.TCoded
                                    obs.value_coded = 1526
                                    notSet.Add(obs)
                                Case "TORACENTESE"
                                    If String.IsNullOrEmpty(codParametro) Then
                                        If codParametro = "ND" Then
                                            obs.concept_id = 1771
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 1067
                                            notSet.Add(obs)
                                        ElseIf codParametro = "NEGATIVO" Then
                                            obs.concept_id = 1771
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 664
                                            notSet.Add(obs)
                                        ElseIf codParametro = "POSITIVO" Then
                                            obs.concept_id = 1771
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 703
                                            notSet.Add(obs)
                                        End If
                                    End If
                                Case "TP"
                                    'FALTA IMPLEMENTAR ISTO
                                Case "TRICHURIA"
                                    obs.concept_id = 1635
                                    obs.data_Type = ObsDataType.TCoded
                                    obs.value_coded = 1528
                                    notSet.Add(obs)
                                Case "UREIA"
                                    obs.concept_id = 857
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1633.Add(obs)
                                Case "VS"
                                    obs.concept_id = 855
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1723.Add(obs)
                                Case "NEUTROFILOS", "NEUTRÓFILOS"
                                    obs.concept_id = 1022
                                    obs.data_Type = ObsDataType.TNumeric
                                    obs.value_numeric = resultado
                                    set1723.Add(obs)
                                Case "TESTE RAPIDO HIV"
                                    'FALTA IMPLEMENTAR ISTO
                                Case "PCR"
                                    If String.IsNullOrEmpty(codParametro) Then
                                        If codParametro = "POSITIVO" Or codParametro = "POSETIVO" Then
                                            obs.concept_id = 1030
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 703
                                            notSet.Add(obs)
                                        Else
                                            obs.concept_id = 1030
                                            obs.data_Type = ObsDataType.TCoded
                                            obs.value_coded = 664
                                            notSet.Add(obs)
                                        End If
                                    End If


                            End Select

                        Else


                            encounter_id = EncounterDAO.insertEncounterByParam(13, patientID, locationid, 106, dataInicial, 7, 27)

                            obsSet = New Obs

                            obsSet.location_id = locationid
                            obsSet.person_id = patientID
                            obsSet.date_created = Now
                            obsSet.voided = 0
                            obsSet.encounter_id = encounter_id
                            obsSet.obs_datetime = dataInicial



                            If set1632.Count > 0 Then
                                obsSet.concept_id = 1632
                                obsGroupId = ObsDAO.insertSet(obsSet)
                                For Each o As Obs In set1632
                                    o.obs_group_id = obsGroupId
                                    o.encounter_id = encounter_id
                                    ObsDAO.insertObs(o, True)
                                Next
                            End If
                            If set1633.Count > 0 Then
                                obsSet.concept_id = 1633
                                obsGroupId = ObsDAO.insertSet(obsSet)
                                For Each o As Obs In set1633
                                    o.obs_group_id = obsGroupId
                                    o.encounter_id = encounter_id
                                    ObsDAO.insertObs(o, True)
                                Next
                            End If
                            If set1639.Count > 0 Then
                                obsSet.concept_id = 1639
                                obsGroupId = ObsDAO.insertSet(obsSet)
                                For Each o As Obs In set1639
                                    o.obs_group_id = obsGroupId
                                    o.encounter_id = encounter_id
                                    ObsDAO.insertObs(o, True)
                                Next
                            End If

                            If set1723.Count > 0 Then
                                obsSet.concept_id = 1723
                                obsGroupId = ObsDAO.insertSet(obsSet)
                                For Each o As Obs In set1723
                                    o.obs_group_id = obsGroupId
                                    o.encounter_id = encounter_id
                                    ObsDAO.insertObs(o, True)
                                Next
                            End If

                            If notSet.Count > 0 Then
                                For Each o As Obs In notSet
                                    o.encounter_id = encounter_id
                                    ObsDAO.insertObs(o, False)
                                Next
                            End If
                            dataInicial = dataCorrente 'Mudar o valor da data inicial ja e outra consulta
                            rs.MovePrevious() 'Posionar-se na consulta que nao coicidiu com a anterior

                            set1632.Clear()
                            set1633.Clear()
                            set1639.Clear()

                            set1723.Clear()

                            notSet.Clear()
                        End If
                        codExame = ""
                        codParametro = ""
                        rs.MoveNext()

                    End While



                    encounter_id = EncounterDAO.insertEncounterByParam(13, patientID, locationid, 106, dataInicial, 7, 27)

                    obsSet = New Obs

                    obsSet.location_id = locationid
                    obsSet.person_id = patientID
                    obsSet.date_created = Now
                    obsSet.voided = 0
                    obsSet.encounter_id = encounter_id
                    obsSet.obs_datetime = dataInicial




                    If set1632.Count > 0 Then
                        obsSet.concept_id = 1632
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In set1632
                            o.obs_group_id = obsGroupId
                            o.encounter_id = encounter_id
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                    If set1633.Count > 0 Then
                        obsSet.concept_id = 1633
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In set1633
                            o.obs_group_id = obsGroupId
                            o.encounter_id = encounter_id
                            ObsDAO.insertObs(o, True)
                        Next
                    End If
                    If set1639.Count > 0 Then
                        obsSet.concept_id = 1639
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In set1639
                            o.obs_group_id = obsGroupId
                            o.encounter_id = encounter_id
                            ObsDAO.insertObs(o, True)
                        Next
                    End If

                    If set1723.Count > 0 Then
                        obsSet.concept_id = 1723
                        obsGroupId = ObsDAO.insertSet(obsSet)
                        For Each o As Obs In set1723
                            o.obs_group_id = obsGroupId
                            o.encounter_id = encounter_id
                            ObsDAO.insertObs(o, True)
                        Next
                    End If

                    If notSet.Count > 0 Then
                        For Each o As Obs In notSet
                            o.encounter_id = encounter_id
                            ObsDAO.insertObs(o, False)
                        Next
                    End If
                End If

                set1632.Clear()
                set1633.Clear()
                set1639.Clear()

                set1723.Clear()

                notSet.Clear()

                rs.Close()
            End If

        Catch ex As Exception
            MsgBox("Erro ao Importar Laboratorio. " & ex.Message)

        End Try
    End Sub

End Class
