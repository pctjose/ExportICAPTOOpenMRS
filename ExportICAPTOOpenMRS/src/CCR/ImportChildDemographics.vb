Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices.Marshal
Imports MySql.Data.MySqlClient
Public Class ImportChildDemographics
    Public Shared Sub ImportDemographics(ByVal oSheet As Excel.Worksheet, ByVal pacientes As Int16)
        Dim nome As String = ""
        Dim nid As String = ""
        Dim sexo As String = ""
        Dim dataNascimento As Date = Nothing
        Dim localidade As String = ""
        Dim bairro As String = ""

        For linha = 2 To pacientes
            For coluna = 1 To 6
                Select Case coluna
                    Case 1
                        If Not oSheet.Cells(linha, coluna).Value = Nothing Then
                            nome = oSheet.Cells(linha, coluna).Value.ToString()
                        End If
                    Case 2
                        If Not oSheet.Cells(linha, coluna).Value = Nothing Then
                            nid = oSheet.Cells(linha, coluna).Value.ToString()
                        End If
                    Case 3
                        If Not oSheet.Cells(linha, coluna).Value = Nothing Then
                            sexo = oSheet.Cells(linha, coluna).Value.ToString()
                        End If
                    Case 4
                        If Not oSheet.Cells(linha, coluna).Value = Nothing Then
                            dataNascimento = oSheet.Cells(linha, coluna).Value.ToString()
                        End If
                    Case 5
                        If Not oSheet.Cells(linha, coluna).Value = Nothing Then
                            localidade = oSheet.Cells(linha, coluna).Value.ToString()
                        End If
                    Case 6
                        If Not oSheet.Cells(linha, coluna).Value = Nothing Then
                            bairro = oSheet.Cells(linha, coluna).Value.ToString()
                        End If

                End Select

            Next
            If sexo = "Feminino" Then
                sexo = "F"
            Else
                sexo = "M"
            End If
            Dim nomes(6) As String

            Dim apelido As String = ""
            Dim nomeMeio As String = ""
            Dim primeiroNome As String = ""

            nome = nome.Trim
            nomes = nome.Split(" ")
            If nomes.Count >= 3 Then
                primeiroNome = nomes(0)
                nomeMeio = nomes(1)
                apelido = nomes(2)
            Else
                primeiroNome = nomes(0)
                nomeMeio = ""
                apelido = nomes(1)
            End If

            nome = ""
            nid = ""
            sexo = ""
            dataNascimento = Nothing
            localidade = ""
            bairro = ""

        Next
    End Sub
    Public Shared Function FormatNIDCCR(ByVal nid As String) As String
        Dim partesNID(3) As String
        partesNID = nid.Split("/")

        Dim NidUS As String = ""
        Dim parteAno As String = ""
        Dim ordem As String = ""

        Dim ano As String = ""
        Dim categoria As String = ""

        If partesNID.Count = 3 Then
            NidUS = partesNID(0)
            ordem = partesNID(1)
            parteAno = partesNID(2)
        Else
            ordem = partesNID(0)
            parteAno = partesNID(1)

            If ordem.Length = 3 Then
                ordem = ordem.Insert(0, "0")
            ElseIf ordem.Length = 2 Then
                ordem = ordem.Insert(0, "00")
            ElseIf ordem.Length = 1 Then
                ordem = ordem.Insert(0, "000")
            End If

            ano = parteAno.Substring(0, 2)
            categoria = parteAno.Substring(parteAno.Length - 2, 2)
            categoria = categoria.ToUpper

        End If

        
        Return ""

    End Function
End Class
