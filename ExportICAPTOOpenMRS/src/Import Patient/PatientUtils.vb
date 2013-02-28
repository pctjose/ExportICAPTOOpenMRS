Imports ADODB
Imports MySql.Data.MySqlClient
Public Class PatientUtils
    Public Shared Function verificaNulo(ByVal rs As Recordset, ByVal campo As String) As String
        If IsDBNull(rs.Fields.Item(campo).Value) Then
            Return ""
        Else
            Return rs.Fields.Item(campo).Value
        End If
    End Function
    Public Shared Function replaceAcento(ByVal frase As String) As String
        If Not frase = Nothing Then
            frase = frase.Replace("ú", "u")
            frase = frase.Replace("á", "a")
            frase = frase.Replace("é", "e")
            frase = frase.Replace("í", "i")
            frase = frase.Replace("ó", "o")
            frase = frase.Replace("ã", "a")
            frase = frase.Replace("õ", "o")
            frase = frase.Replace("ç", "c")
            frase = frase.Replace("'", "")
        End If

        Return frase
    End Function
    Public Shared Sub ImportPatients(ByVal fonte As Connection, ByVal locationid As Int16)
        Dim patientID As Integer
        Dim nid As String
        Dim nome As String
        Dim nome1 As String = ""
        Dim nome2 As String = ""
        Dim sexo As Char
        Dim bairro As String
        Dim celula As String
        Dim avenida As String
        Dim datanascimento As Date
        Dim apelido As String
        Dim distrito As String
        Dim telefone As String
        Dim identificacao As String
        Dim observacao As String

        Dim estado As String
        Dim dataSaida As Date

        Dim nomes(2) As String

        Dim dataEstimada As Boolean

        Dim obitou As Boolean

        Dim apelido1 As String = ""
        Dim apelido2 As String = ""
        Dim apelidos(2) As String


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
                    .CommandText = "SELECT t_paciente.nid, t_paciente.nome, t_paciente.sexo, " & _
                                " t_paciente.codbairro, t_paciente.celula, t_paciente.avenida, " & _
                                " t_paciente.datanasc, t_paciente.apelido, t_paciente.coddistrito, " & _
                                " t_adulto.telefone, t_paciente.identificacao,t_paciente.observacao, " & _
                                " t_paciente.codestado,t_paciente.datasaidatarv " & _
                                " FROM t_paciente LEFT JOIN t_adulto ON t_paciente.nid = t_adulto.nid "
                Else
                    .CommandText = "SELECT t_paciente.nid, t_paciente.nome, t_paciente.sexo, " & _
                                " t_paciente.codbairro, t_paciente.celula, t_paciente.avenida, " & _
                                " t_paciente.datanasc, t_paciente.apelido, t_paciente.coddistrito, " & _
                                " t_adulto.telefone, t_paciente.identificacao,t_paciente.observacao, " & _
                                " t_paciente.codestado,t_paciente.datasaidatarv " & _
                                " FROM t_paciente LEFT JOIN t_adulto ON t_paciente.nid = t_adulto.nid " & _
                                " where t_paciente.nid in (" & whereQuery & ")"
                End If
                rs = .Execute
                If Not (rs.EOF And rs.BOF) Then
                    cmmDestino.Connection = ConexaoOpenMRS1 'cone.conectar
                    cmmDestino.CommandType = CommandType.Text
                    rs.MoveFirst()
                    While Not rs.EOF

                        nid = verificaNulo(rs, "nid")

                        nome = replaceAcento(verificaNulo(rs, "nome"))

                        sexo = verificaNulo(rs, "sexo")

                        bairro = replaceAcento(verificaNulo(rs, "codbairro"))

                        celula = replaceAcento(verificaNulo(rs, "celula"))

                        avenida = replaceAcento(verificaNulo(rs, "avenida"))

                        apelido = replaceAcento(verificaNulo(rs, "apelido"))

                        distrito = replaceAcento(verificaNulo(rs, "coddistrito"))

                        telefone = verificaNulo(rs, "telefone")

                        identificacao = verificaNulo(rs, "identificacao")

                        estado = verificaNulo(rs, "codestado")

                        If Not IsDBNull(rs.Fields.Item("datasaidatarv").Value) Then
                            dataSaida = rs.Fields.Item("datasaidatarv").Value
                        End If

                        If Not IsDBNull(rs.Fields.Item("datanasc").Value) Then
                            datanascimento = rs.Fields.Item("datanasc").Value
                        End If

                        observacao = verificaNulo(rs, "observacao")

                        If Not String.IsNullOrEmpty(nome) Then
                            nomes = nome.Split(" ")
                            If nomes.Count >= 2 Then
                                nome1 = nomes(0)
                                nome2 = nomes(1)
                            Else
                                nome1 = nomes(0)
                                nome2 = ""
                            End If
                        End If

                        If Not String.IsNullOrEmpty(apelido) Then
                            apelidos = apelido.Split(" ")
                            If apelidos.Count >= 2 Then
                                apelido1 = apelidos(0)
                                apelido2 = apelidos(apelidos.Count - 1)
                            Else
                                apelido2 = apelidos(apelidos.Count - 1)
                                apelido1 = ""
                            End If
                        End If

                        nome2 &= " " & apelido1
                        apelido = apelido2

                        If String.IsNullOrEmpty(observacao) Then
                            dataEstimada = False
                        Else
                            If observacao.Contains("data de nascimento foi estimada") Then
                                dataEstimada = True
                            Else
                                dataEstimada = False
                            End If
                        End If

                        If String.IsNullOrEmpty(estado) Then
                            obitou = False
                        ElseIf estado = "Morte" Then
                            obitou = True
                        Else
                            obitou = False
                        End If


                        cmmDestino.CommandText = "Insert into person(gender,birthdate,birthdate_estimated,dead,creator,date_created,uuid)" & _
                                                " values('" & sexo & "','" & dataMySQL(datanascimento) & "'," & dataEstimada & ",0,22,now(),uuid())"
                        cmmDestino.ExecuteNonQuery()

                        cmmDestino.CommandText = "Select max(person_id) from person"
                        patientID = cmmDestino.ExecuteScalar

                        If obitou Then
                            cmmDestino.CommandText = "Update person set dead=1,death_date='" & dataMySQL(dataSaida) & "',cause_of_death=5622 where person_id=" & patientID & ""
                            cmmDestino.ExecuteNonQuery()
                        End If

                        cmmDestino.CommandText = "Insert into person_address(person_id,preferred,address1,state_province,country,creator,date_created,county_district,neighborhood_cell,subregion,uuid)" & _
                        " values(" & patientID & ",1,'" & MySQLScape(avenida) & "','Zambezia','Mozambique',22,now(),'" & MySQLScape(distrito) & "','" & MySQLScape(celula) & "','" & MySQLScape(bairro) & "',uuid())"
                        cmmDestino.ExecuteNonQuery()

                        If Not String.IsNullOrEmpty(telefone) Then
                            cmmDestino.CommandText = "Insert into person_attribute(person_id,value,person_attribute_type_id,creator,date_created,uuid)" & _
                            " values(" & patientID & ",'" & telefone & "',9,22,now(),uuid())"
                            cmmDestino.ExecuteNonQuery()
                        End If

                        cmmDestino.CommandText = "Insert into person_name(preferred,person_id,given_name,middle_name,family_name,creator,date_created,uuid)" & _
                        " values(1," & patientID & ",'" & nome1 & "','" & nome2 & "','" & apelido & "',22,now(),uuid())"
                        cmmDestino.ExecuteNonQuery()

                        cmmDestino.CommandText = "Insert into patient(patient_id,creator,date_created)" & _
                        " values(" & patientID & ",22,now())"
                        cmmDestino.ExecuteNonQuery()

                        cmmDestino.CommandText = "Insert into patient_identifier(patient_id,identifier,identifier_type,preferred,location_id,creator,date_created,uuid)" & _
                        " values(" & patientID & ",'" & nid & "',2,1," & locationid & ",22,now(),uuid())"
                        cmmDestino.ExecuteNonQuery()
                        If Not String.IsNullOrEmpty(identificacao) Then
                            cmmDestino.CommandText = "Insert into patient_identifier(patient_id,identifier,identifier_type,preferred,location_id,creator,date_created,uuid)" & _
                            " values(" & patientID & ",'" & identificacao & "',3,0," & locationid & ",22,now(),uuid())"
                            cmmDestino.ExecuteNonQuery()
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
            MsgBox("Error Importing Patients: " & ex.Message)
            'Nerros += 1
        End Try
    End Sub
    
End Class
