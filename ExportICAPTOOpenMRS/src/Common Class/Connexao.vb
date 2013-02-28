Imports MySql.Data.MySqlClient
Public Class Connexao
    Public Shared Function conectarOpenMRS() As MySqlConnection
        Dim conn As MySqlConnection = New MySqlConnection()
        Try

            conn.ConnectionString = My.Settings.ConnectionStringOpenMRS '"Database=dm_#misau;user=root;password=dm2007misau;Server=localhost;port=3306"
            conn.Open()
            Return conn
        Catch ex As Exception
            Try
                conn.Close()
                conn.ConnectionString = My.Settings.ConnectionStringOpenMRS
                conn.Open()
                Return conn
            Catch e As Exception
                MsgBox("Houve erro de connexao, Veja se o MySQL esta a correr." & _
                vbNewLine & "E as portas estão correctamente configuradas." & vbNewLine & "Dica: Veja se o Firewall não bloqueia a porta 3306 do MySQL." & vbNewLine & ex.Message, MsgBoxStyle.Exclamation + MsgBoxStyle.ApplicationModal, "Erro de Conexão")
            End Try

            End
        End Try
    End Function
    
End Class
