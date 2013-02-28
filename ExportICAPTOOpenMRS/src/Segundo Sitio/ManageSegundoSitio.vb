Imports ADODB
Imports MySql.Data.MySqlClient
Public Class ManageSegundoSitio
    Public Shared Sub UpdateSegundoSitio(ByVal fonte As Connection, ByVal locationID As Int16)
        Dim cmmFonte As New Command
        Dim rs As New Recordset

        cmmFonte.ActiveConnection = fonte
        cmmFonte.CommandType = CommandType.Text

        If AllPatients Then
            Select Case locationID
                Case 3

                    cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro in ('Nauela','Mugema','Mohiua')"
                    rs = cmmFonte.Execute
                    If Not (rs.EOF And rs.BOF) Then
                        While Not rs.EOF
                            updateLocation(rs.Fields.Item("nid").Value, 9)
                            rs.MoveNext()
                        End While
                    End If
                Case 4
                    cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro='Mogulama 2ºSitio'"
                    rs = cmmFonte.Execute
                    If Not (rs.EOF And rs.BOF) Then
                        While Not rs.EOF
                            updateLocation(rs.Fields.Item("nid").Value, 10)
                            rs.MoveNext()
                        End While
                    End If
                    'Case 5
                    '    cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro in ('Macuse','Macuse1','Macuse 1','Mixixine','Mexixine','Furquia','Mbaua','Mbaúa','Supinho','Maquivale')"
                    '    rs = cmmFonte.Execute
                    '    If Not (rs.EOF And rs.BOF) Then
                    '        While Not rs.EOF
                    '            updateLocation(rs.Fields.Item("nid").Value, 17)
                    '            rs.MoveNext()
                    '        End While
                    '    End If
                Case 6
                    cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro in ('Alto Ligonha','Miraly','Muiane','Murrupula','Namirreco')"
                    rs = cmmFonte.Execute
                    If Not (rs.EOF And rs.BOF) Then
                        While Not rs.EOF
                            updateLocation(rs.Fields.Item("nid").Value, 25)
                            rs.MoveNext()
                        End While
                    End If
                    'rs.Close()
                    'cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro='Moneia'"
                    'rs = cmmFonte.Execute
                    'If Not (rs.EOF And rs.BOF) Then
                    '    While Not rs.EOF
                    '        updateLocation(rs.Fields.Item("nid").Value, 26)
                    '        rs.MoveNext()
                    '    End While
                    'End If
                    'rs.Close()
                    'cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro='Uapé'"
                    'rs = cmmFonte.Execute
                    'If Not (rs.EOF And rs.BOF) Then
                    '    While Not rs.EOF
                    '        updateLocation(rs.Fields.Item("nid").Value, 27)
                    '        rs.MoveNext()
                    '    End While
                    'End If
                Case 7
                    cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro in ('Gonhane','Abreu','Amadeu','Chirimane','Ilova')"
                    rs = cmmFonte.Execute
                    If Not (rs.EOF And rs.BOF) Then
                        While Not rs.EOF
                            updateLocation(rs.Fields.Item("nid").Value, 24)
                            rs.MoveNext()
                        End While
                    End If
                Case 8
                    cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro in ('Namagoa','Nacugulune')"
                    rs = cmmFonte.Execute
                    If Not (rs.EOF And rs.BOF) Then
                        While Not rs.EOF
                            updateLocation(rs.Fields.Item("nid").Value, 28)
                            rs.MoveNext()
                        End While
                    End If
            End Select
        Else
            Select Case locationID
                Case 3

                    cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro in ('Nauela','Mugema','Mohiua') and nid in (" & whereQuery & ")"
                    rs = cmmFonte.Execute
                    If Not (rs.EOF And rs.BOF) Then
                        While Not rs.EOF
                            updateLocation(rs.Fields.Item("nid").Value, 9)
                            rs.MoveNext()
                        End While
                    End If
                Case 4
                    cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro='Mogulama 2ºSitio' and nid in (" & whereQuery & ")"
                    rs = cmmFonte.Execute
                    If Not (rs.EOF And rs.BOF) Then
                        While Not rs.EOF
                            updateLocation(rs.Fields.Item("nid").Value, 10)
                            rs.MoveNext()
                        End While
                    End If
                    'Case 5
                    '    cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro in ('Macuse','Macuse1','Macuse 1','Mixixine','Mexixine','Furquia','Mbaua','Mbaúa','Supinho','Maquivale') and nid in (" & whereQuery & ")"
                    '    rs = cmmFonte.Execute
                    '    If Not (rs.EOF And rs.BOF) Then
                    '        While Not rs.EOF
                    '            updateLocation(rs.Fields.Item("nid").Value, 17)
                    '            rs.MoveNext()
                    '        End While
                    '    End If
                Case 6
                    cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro in ('Alto Ligonha','Miraly','Muiane','Murrupula','Namirreco') and nid in (" & whereQuery & ")"
                    rs = cmmFonte.Execute
                    If Not (rs.EOF And rs.BOF) Then
                        While Not rs.EOF
                            updateLocation(rs.Fields.Item("nid").Value, 25)
                            rs.MoveNext()
                        End While
                    End If
                    'rs.Close()
                    'cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro='Moneia' and nid in (" & whereQuery & ")"
                    'rs = cmmFonte.Execute
                    'If Not (rs.EOF And rs.BOF) Then
                    '    While Not rs.EOF
                    '        updateLocation(rs.Fields.Item("nid").Value, 26)
                    '        rs.MoveNext()
                    '    End While
                    'End If
                    'rs.Close()
                    'cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro='Uapé' and nid in (" & whereQuery & ")"
                    'rs = cmmFonte.Execute
                    'If Not (rs.EOF And rs.BOF) Then
                    '    While Not rs.EOF
                    '        updateLocation(rs.Fields.Item("nid").Value, 27)
                    '        rs.MoveNext()
                    '    End While
                    'End If
                Case 7
                    cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro in ('Gonhane','Abreu','Amadeu','Chirimane','Ilova') and nid in (" & whereQuery & ")"
                    rs = cmmFonte.Execute
                    If Not (rs.EOF And rs.BOF) Then
                        While Not rs.EOF
                            updateLocation(rs.Fields.Item("nid").Value, 24)
                            rs.MoveNext()
                        End While
                    End If
                Case 8
                    cmmFonte.CommandText = "Select distinct nid from t_paciente where codbairro in ('Namagoa','Nacugulune') and nid in (" & whereQuery & ")"
                    rs = cmmFonte.Execute
                    If Not (rs.EOF And rs.BOF) Then
                        While Not rs.EOF
                            updateLocation(rs.Fields.Item("nid").Value, 28)
                            rs.MoveNext()
                        End While
                    End If
            End Select

        End If
        
        rs.Close()
    End Sub

    Private Shared Sub updateLocation(ByVal nid As String, ByVal newLocation As Int16)
        Dim patientID As Integer
        Dim comandoS As New MySqlCommand
        patientID = GetPatientOpenMRSIDByNID(nid)
        If patientID > 0 Then
            With comandoS
                .Connection = ConexaoOpenMRS1
                .CommandType = CommandType.Text
                .CommandText = "Update encounter set location_id=" & newLocation & " where patient_id=" & patientID
                .ExecuteNonQuery()
                .CommandText = "Update obs set location_id=" & newLocation & " where person_id=" & patientID
                .ExecuteNonQuery()
                .CommandText = "Update patient_identifier set location_id=" & newLocation & " where patient_id=" & patientID
                .ExecuteNonQuery()
            End With
        End If
    End Sub
End Class
