Imports ADODB
Imports MySql.Data.MySqlClient
Public Class UpdatePatientProgram
    Public Shared Sub UpdateCuidadoTratamento(ByVal fonte As Connection, ByVal locationid As Int16)
        Dim patientID As Integer

        Dim nid As String
        Dim dataAberturaProcesso As Date
        Dim transfoutraUS As String
        Dim patientProgramID As Integer
        Dim patientStateId, patientState2 As Integer
        Dim dataEstado As Date
        Dim dataInicioTarv As Date

        'Try
        Dim cmmFonte As New Command 'Acess
        Dim rs As New Recordset
        Dim cmmDestino As New MySqlCommand 'MySQL

        cmmFonte.CommandType = CommandTypeEnum.adCmdText
        cmmFonte.ActiveConnection = fonte
        If AllPatients Then
            cmmFonte.CommandText = "SELECT  nid,dataabertura," & _
                                            "emtarv,datainiciotarv,datasaidatarv,codestado,transfoutraUS " & _
                                " FROM t_paciente where nid is not null "
        Else
            cmmFonte.CommandText = "SELECT  nid,dataabertura," & _
                                            "emtarv,datainiciotarv,datasaidatarv,codestado,transfoutraUS " & _
                                " FROM t_paciente where nid is not null and nid in (" & whereQuery & ")"
        End If

        cmmDestino.CommandType = CommandType.Text
        cmmDestino.Connection = ConexaoOpenMRS3

        rs = cmmFonte.Execute

        If Not (rs.EOF And rs.BOF) Then
            rs.MoveFirst()
            While Not rs.EOF

                nid = rs.Fields.Item("nid").Value
                dataAberturaProcesso = rs.Fields.Item("dataabertura").Value
                transfoutraUS = PatientUtils.verificaNulo(rs, "transfoutraUS")


                patientID = GetPatientOpenMRSIDByNID(nid) 'Get the openmrs patient_id using the NID

                If patientID > 0 Then



                    If rs.Fields.Item("emtarv").Value Then
                        dataInicioTarv = rs.Fields.Item("datainiciotarv").Value
                        If transfoutraUS = "-1" Then
                            If dataAberturaProcesso = dataInicioTarv Then
                                patientProgramID = PatientProgamDAO.insertPatientProgramByParam(patientID, 2, dataInicioTarv, locationid)
                                patientStateId = PatientProgamDAO.insertPatientStateByParam(patientProgramID, 29, dataInicioTarv)
                            Else
                                Dim patientProgramCuidado As Integer = PatientProgamDAO.insertPatientProgramByParam(patientID, 1, dataAberturaProcesso, locationid)
                                Dim patientStateIdCuidado As Integer = PatientProgamDAO.insertPatientStateByParam(patientProgramCuidado, 28, dataAberturaProcesso)

                                PatientProgamDAO.endPatientProgram(patientProgramCuidado, dataInicioTarv)
                                PatientProgamDAO.endPatientState(patientStateIdCuidado, dataInicioTarv)

                                PatientProgamDAO.insertPatientStateByParam(patientProgramCuidado, 4, dataInicioTarv)

                                patientProgramID = PatientProgamDAO.insertPatientProgramByParam(patientID, 2, dataInicioTarv, locationid)
                                patientStateId = PatientProgamDAO.insertPatientStateByParam(patientProgramID, 6, dataInicioTarv)

                            End If

                        Else
                            Dim patientProgramCuidado As Integer = PatientProgamDAO.insertPatientProgramByParam(patientID, 1, dataAberturaProcesso, locationid)
                            Dim patientStateIdCuidado As Integer = PatientProgamDAO.insertPatientStateByParam(patientProgramCuidado, 1, dataAberturaProcesso)

                            PatientProgamDAO.endPatientProgram(patientProgramCuidado, dataInicioTarv)
                            PatientProgamDAO.endPatientState(patientStateIdCuidado, dataInicioTarv)

                            PatientProgamDAO.insertPatientStateByParam(patientProgramCuidado, 4, dataInicioTarv)

                            patientProgramID = PatientProgamDAO.insertPatientProgramByParam(patientID, 2, dataInicioTarv, locationid)
                            patientStateId = PatientProgamDAO.insertPatientStateByParam(patientProgramID, 6, dataInicioTarv)

                        End If

                        If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "codestado")) Then

                            patientState2 = getEstadoId(rs.Fields.Item("codestado").Value, True)
                            If patientState2 > 0 Then
                                dataEstado = rs.Fields.Item("datasaidatarv").Value
                                If patientState2 = 10 Then
                                    PatientProgamDAO.endPatientProgram(patientProgramID, dataEstado)
                                    PatientProgamDAO.endPatientState(patientStateId, dataEstado)
                                    PatientProgamDAO.insertPatientStateByParam(patientProgramID, patientState2, dataEstado)
                                Else
                                    PatientProgamDAO.endPatientState(patientStateId, dataEstado)
                                    PatientProgamDAO.insertPatientStateByParam(patientProgramID, patientState2, dataEstado)
                                End If
                            End If
                        End If

                    Else
                        If transfoutraUS = "-1" Then
                            patientProgramID = PatientProgamDAO.insertPatientProgramByParam(patientID, 1, dataAberturaProcesso, locationid)
                            patientStateId = PatientProgamDAO.insertPatientStateByParam(patientProgramID, 28, dataAberturaProcesso)
                        Else
                            patientProgramID = PatientProgamDAO.insertPatientProgramByParam(patientID, 1, dataAberturaProcesso, locationid)
                            patientStateId = PatientProgamDAO.insertPatientStateByParam(patientProgramID, 1, dataAberturaProcesso)
                        End If

                        If Not String.IsNullOrEmpty(PatientUtils.verificaNulo(rs, "codestado")) Then

                            patientState2 = getEstadoId(rs.Fields.Item("codestado").Value, False)
                            If patientState2 > 0 Then
                                dataEstado = rs.Fields.Item("datasaidatarv").Value
                                If patientState2 = 5 Then
                                    PatientProgamDAO.endPatientProgram(patientProgramID, dataEstado)
                                    PatientProgamDAO.endPatientState(patientStateId, dataEstado)
                                    PatientProgamDAO.insertPatientStateByParam(patientProgramID, patientState2, dataEstado)
                                Else
                                    PatientProgamDAO.endPatientState(patientStateId, dataEstado)
                                    PatientProgamDAO.insertPatientStateByParam(patientProgramID, patientState2, dataEstado)
                                End If
                            End If
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
    Private Shared Function getEstadoId(ByVal estado As String, ByVal emtarv As Boolean) As Integer
        If emtarv Then
            Select Case estado
                Case "Abandono", "ABANDONO"
                    Return 9
                Case "Morte", "MORTE", "Obito", "Obitou"
                    Return 10
                Case "Suspender Tarv", "Suspender", "Suspenso"
                    Return 8
                Case "Transferido para", "Transfer para"
                    Return 7
                Case Else
                    Return 0
            End Select
        Else
            Select Case estado
                Case "Abandono", "ABANDONO"
                    Return 2
                Case "Morte", "MORTE", "Obito", "Obitou"
                    Return 5
                Case "Iniciar", "Inicio"
                    Return 4
                Case "Transferido para", "Transfer para"
                    Return 3
                Case Else
                    Return 0
            End Select
        End If
    End Function
End Class
