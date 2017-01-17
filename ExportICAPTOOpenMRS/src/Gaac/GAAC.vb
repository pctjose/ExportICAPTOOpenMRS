Public Class GAAC


    Public Shared Function getTipoSaidaGaac(ByVal saida As String) As Integer
        Select Case saida
            Case "Suspender Tarv", "Suspenso"
                Return 4
            Case "Transferido para"
                Return 1
            Case "Desistiu"
                Return 2
            Case "Obitou", "Obito", "Morte"
                Return 3
            Case "Desintegracao"
                Return 5
            Case Else
                Return 2
        End Select
    End Function
    Public Shared Function getTipoAfinidadeGaac(ByVal afinidade As String) As Integer
        Select Case afinidade
            Case "Amizade"
                Return 1
            Case "Familiar"
                Return 2
            Case "Igreja", "Mesquita"
                Return 3
            Case "Residencia"
                Return 5
            Case "Trabalho"
                Return 6
            Case Else
                Return 7
        End Select
    End Function

    Private Shared Sub ImportGaacMember(ByVal fonte As Connection, ByVal idGaacAccess As Integer, ByVal idGaacOpenMRS As Integer)
        Try
            Dim rs As New Recordset

            rs.Open("Select distinct nid,dataInscricao,dataSaida,motivo from t_gaac_actividades where nid is not null and numGAAC = " & idGaacAccess, fonte, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockReadOnly)

            If Not (rs.EOF And rs.BOF) Then

                rs.MoveFirst()

                While Not rs.EOF

                    Dim patientId As Integer = GetPatientOpenMRSIDByNID(rs.Fields.Item("nid").Value)

                    If patientId > 0 Then
                        Dim memberId As Integer = GaacUtils.insertGaacMemberByParam(idGaacOpenMRS, patientId, rs.Fields.Item("dataInscricao").Value)

                        If Not IsDBNull(rs.Fields.Item("dataSaida").Value) Then
                            GaacUtils.updateSaidaGaacMemberByParam(memberId, getTipoSaidaGaac(rs.Fields.Item("motivo").Value), rs.Fields.Item("dataSaida").Value)
                        End If
                    End If

                    rs.MoveNext()
                End While
                rs.Close()
            End If

        Catch ex As Exception
            MsgBox("Error Importing Gaac Member: " & ex.Message)

        End Try
    End Sub
    Public Shared Sub ImportGaac(ByVal fonte As Connection, ByVal location As Integer)
        Try
            Dim rs As New Recordset

            rs.Open("Select distinct numGAAC,datainicio,afinidade,dataDesintegracao,nidPontoFocal,observacao from t_gaac", fonte, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockReadOnly)

            If Not (rs.EOF And rs.BOF) Then

                rs.MoveFirst()

                While Not rs.EOF

                    Dim pontoFocal As Integer = GetPatientOpenMRSIDByNID(rs.Fields.Item("nidPontoFocal").Value)

                    If pontoFocal > 0 Then

                        Dim numGaac As Integer = rs.Fields.Item("numGAAC").Value
                        Dim afinity As String = rs.Fields.Item("afinidade").Value
                        Dim dataInicio As Date = rs.Fields.Item("datainicio").Value
                        Dim dataDesintegracao As Date = Nothing

                        If Not IsDBNull(rs.Fields.Item("dataDesintegracao").Value) Then
                            dataDesintegracao = rs.Fields.Item("dataDesintegracao").Value
                        End If

                        Dim gaacID As Integer = GaacUtils.insertGaacByParam(numGaac.ToString, numGaac & "-" & afinity, dataInicio, getTipoAfinidadeGaac(afinity), pontoFocal, location, dataDesintegracao)

                        ImportGaacMember(fonte, numGaac, gaacID)
                    End If

                    rs.MoveNext()
                End While
                rs.Close()
            End If

        Catch ex As Exception
            MsgBox("Error Importing Gaac: " & ex.Message)

        End Try
    End Sub
End Class
