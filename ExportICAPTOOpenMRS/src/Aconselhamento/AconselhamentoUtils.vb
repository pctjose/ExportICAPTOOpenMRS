Imports ADODB
Imports MySql.Data.MySqlClient
Public Class AconselhamentoUtils
    Public Shared Sub ImportAconselhamento(ByVal fonte As Connection, ByVal locationid As Int16)
        Dim patientID As Integer
        Dim encounter_id As Integer

        Dim criteriosMedicos As String
       
        Dim nrsessao As Int16
        Dim tipoactividade As String

        Dim nid As String

        Dim o As New Obs


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
                    .CommandText = "SELECT t_aconselhamento.idaconselhamento, t_aconselhamento.nid, " & _
                " t_aconselhamento.criteriosmedicos, t_aconselhamento.conceitos, " & _
                " t_aconselhamento.interessado, t_aconselhamento.confidente, " & _
                " t_aconselhamento.apareceregularmente, t_aconselhamento.riscopobreaderencia, " & _
                " t_aconselhamento.regimetratamento, t_aconselhamento.prontotarv, " & _
                " t_aconselhamento.datapronto, t_aconselhamento.obs, " & _
                " t_actividadeaconselhamento.data, t_actividadeaconselhamento.nrsessao, " & _
                " t_actividadeaconselhamento.tipoactividade, t_actividadeaconselhamento.apresentouconfidente " & _
                " FROM t_aconselhamento INNER JOIN t_actividadeaconselhamento ON " & _
                " t_aconselhamento.idaconselhamento = t_actividadeaconselhamento.idaconselhamento and " & _
                " t_aconselhamento.nid = t_actividadeaconselhamento.nid "
                Else
                    .CommandText = "SELECT t_aconselhamento.idaconselhamento, t_aconselhamento.nid, " & _
                " t_aconselhamento.criteriosmedicos, t_aconselhamento.conceitos, " & _
                " t_aconselhamento.interessado, t_aconselhamento.confidente, " & _
                " t_aconselhamento.apareceregularmente, t_aconselhamento.riscopobreaderencia, " & _
                " t_aconselhamento.regimetratamento, t_aconselhamento.prontotarv, " & _
                " t_aconselhamento.datapronto, t_aconselhamento.obs, " & _
                " t_actividadeaconselhamento.data, t_actividadeaconselhamento.nrsessao, " & _
                " t_actividadeaconselhamento.tipoactividade, t_actividadeaconselhamento.apresentouconfidente " & _
                " FROM t_aconselhamento INNER JOIN t_actividadeaconselhamento ON " & _
                " t_aconselhamento.idaconselhamento = t_actividadeaconselhamento.idaconselhamento and " & _
                " t_aconselhamento.nid = t_actividadeaconselhamento.nid " & _
                " where t_aconselhamento.nid in (" & whereQuery & ")"

                End If
                
                rs = .Execute
                If Not (rs.EOF And rs.BOF) Then
                    cmmDestino.Connection = ConexaoOpenMRS1 'cone.conectar
                    cmmDestino.CommandType = CommandType.Text
                    rs.MoveFirst()
                    While Not rs.EOF
                        nid = rs.Fields.Item("nid").Value
                        patientID = GetPatientOpenMRSIDByNID(rs.Fields.Item("nid").Value) 'Get the openmrs patient_id using the NID

                        If patientID > 0 Then

                            'End If
                            cmmDestino.CommandText = "Insert into encounter(encounter_type,patient_id,provider_id,location_id," & _
                                                    "form_id,encounter_datetime,creator,date_created,voided,uuid) values(19," & patientID & ",27," & locationid & "," & _
                                                    "115,'" & dataMySQL(rs.Fields.Item("data").Value) & "',22,now(),0,uuid())"
                            cmmDestino.ExecuteNonQuery()
                            'Get The encounter id to user in obs table
                            cmmDestino.CommandText = "Select max(encounter_id) from encounter"
                            encounter_id = cmmDestino.ExecuteScalar


                            o.date_created = Now
                            o.encounter_id = encounter_id
                            o.location_id = locationid
                            o.obs_datetime = rs.Fields.Item("data").Value
                            o.person_id = patientID
                            o.voided = 0

                            If Not IsDBNull(rs.Fields.Item("criteriosmedicos").Value) Then
                                criteriosMedicos = rs.Fields.Item("criteriosmedicos").Value
                                criteriosMedicos = criteriosMedicos.ToUpper
                                o.concept_id = 1248
                                o.data_Type = ObsDataType.TCoded
                                If criteriosMedicos = "SIM" Then
                                    o.value_coded = 1065
                                Else
                                    o.value_coded = 1066
                                End If
                                ObsDAO.insertObs(o, False)
                            End If

                            If rs.Fields.Item("conceitos").Value Then
                                o.concept_id = 1729
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1065
                                ObsDAO.insertObs(o, False)
                            Else
                                o.concept_id = 1729
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1066
                                ObsDAO.insertObs(o, False)
                            End If

                            If rs.Fields.Item("interessado").Value Then
                                o.concept_id = 1736
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1065
                                ObsDAO.insertObs(o, False)
                            Else
                                o.concept_id = 1736
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1066
                                ObsDAO.insertObs(o, False)
                            End If

                            If rs.Fields.Item("confidente").Value Then
                                o.concept_id = 1728
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1065
                                ObsDAO.insertObs(o, False)
                            Else
                                o.concept_id = 1728
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1066
                                ObsDAO.insertObs(o, False)
                            End If

                            If rs.Fields.Item("apareceregularmente").Value Then
                                o.concept_id = 1743
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1065
                                ObsDAO.insertObs(o, False)
                            Else
                                o.concept_id = 1743
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1066
                                ObsDAO.insertObs(o, False)
                            End If

                            If rs.Fields.Item("riscopobreaderencia").Value Then
                                o.concept_id = 1749
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1065
                                ObsDAO.insertObs(o, False)
                            Else
                                o.concept_id = 1749
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1066
                                ObsDAO.insertObs(o, False)
                            End If
                            If rs.Fields.Item("regimetratamento").Value Then
                                o.concept_id = 1752
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1065
                                ObsDAO.insertObs(o, False)
                            Else
                                o.concept_id = 1752
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1066
                                ObsDAO.insertObs(o, False)
                            End If

                            If rs.Fields.Item("prontotarv").Value Then
                                o.concept_id = 1756
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1065
                                If Not IsDBNull(rs.Fields.Item("datapronto").Value) Then
                                    o.obs_datetime = rs.Fields.Item("datapronto").Value
                                End If
                                ObsDAO.insertObs(o, False)
                            Else
                                o.concept_id = 1756
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1066
                                ObsDAO.insertObs(o, False)
                            End If

                            If rs.Fields.Item("apresentouconfidente").Value Then
                                o.concept_id = 1739
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1065
                                ObsDAO.insertObs(o, False)
                            Else
                                o.concept_id = 1739
                                o.data_Type = ObsDataType.TCoded
                                o.value_coded = 1066
                                ObsDAO.insertObs(o, False)
                            End If

                            If Not IsDBNull(rs.Fields.Item("tipoactividade").Value) Then
                                tipoactividade = rs.Fields.Item("tipoactividade").Value
                                tipoactividade = tipoactividade.ToUpper
                                o.concept_id = 1727
                                o.data_Type = ObsDataType.TCoded
                                If tipoactividade = "GRUPO" Then
                                    o.value_coded = 1725
                                Else
                                    o.value_coded = 1726
                                End If
                                ObsDAO.insertObs(o, False)
                            End If

                            If Not IsDBNull(rs.Fields.Item("nrsessao").Value) Then
                                nrsessao = rs.Fields.Item("nrsessao").Value

                                o.concept_id = 1724
                                o.data_Type = ObsDataType.TNumeric
                                o.value_numeric = nrsessao
                                ObsDAO.insertObs(o, False)
                            End If

                            If Not IsDBNull(rs.Fields.Item("obs").Value) Then
                                o.concept_id = 1757
                                o.data_Type = ObsDataType.TText
                                o.value_text = MySQLScape(rs.Fields.Item("obs").Value)
                                ObsDAO.insertObs(o, False)
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
            'MsgBox(nid)
            MsgBox("Error Importing Aconselhamento: " & ex.Message)
            'Nerros += 1
        End Try
    End Sub
End Class
