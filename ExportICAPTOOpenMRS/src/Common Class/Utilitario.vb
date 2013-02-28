Imports MySql.Data.MySqlClient
Imports System
Imports System.Data
Imports MySql.Data.Types
Imports System.IO
Imports System.Text
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports System.Runtime.InteropServices.Marshal
Imports ADODB
'Imports CrystalDecisions.Windows

'CLASE PARA CARGAR UN REPORTE BASADO 

Module Utilitario
    Public ConexaoOpenMRS1 As New MySqlConnection
    Public ConexaoOpenMRS2 As New MySqlConnection
    Public ConexaoOpenMRS3 As New MySqlConnection
    Public ConexaoOpenMRSReporting1 As New MySqlConnection
    Public ConexaoOpenMRSReporting2 As New MySqlConnection
    Public ConexaoOpenMRSReporting3 As New MySqlConnection
    Public ResultSet As MySqlDataReader
    Public Parametro As New MySqlParameter
    Public userProvinciaID As Int16
    Public imagemFromDataBase As Boolean
    'Public cone As Connexao = New Connexao()
    Public conn As MySqlConnection
    Public cmm As MySqlCommand = New MySqlCommand()
    'Public utilizador As UserVO
    Public rs1 As System.Data.Common.DbProviderFactories
    Public inicializarFicha As Boolean
    Public RecourdCount As Integer = 0
    Public ICAPConection As New Connection
    Public AllPatients As Boolean
    Public whereQuery As String
    Public Sub centerForm(ByVal formulario As Form)
        'formulario.Left = (frmPrincipal.Width - formulario.Width) / 2
        'formulario.Top = (frmPrincipal.Height - formulario.Height) / 6

        Dim scr As Screen

        scr = Screen.FromControl(formulario)
        Dim rt As New Rectangle()
        rt = scr.Bounds

        formulario.Top = (rt.Height - formulario.Height) / 4
        formulario.Left = (rt.Width - formulario.Width) / 2

    End Sub
    Public Sub FechaResultSet(ByVal res As MySqlDataReader)
        If Not res.IsClosed Then
            res.Close()
            res = Nothing
            GC.Collect()
        End If
    End Sub
    Public Sub ReabreConexao(ByVal cnn As MySqlConnection)
        If cnn.State = ConnectionState.Broken Or cnn.State = ConnectionState.Closed Then
            cnn.Open()
        End If
    End Sub
    
   
    Public Enum Escolaridade As Short
        Sem_Escolaridade = 1
        Elementar = 2
        Basico = 3
        Medio = 4
        Superior = 5
    End Enum

    Public Enum ObsDataType As Short
        TBoolean = 0
        TCoded = 1
        TDrug = 2
        TDatetime = 3
        TNumeric = 4
        TModifier = 5
        TText = 6
    End Enum

    Public Enum TipologiaMaterial As Int16
        Convencional = 1
        Pre_Fabricado = 2
        Tradicional = 3
        Misto = 4
    End Enum

    Public Sub EncherCombo(ByVal cbo As ComboBox, ByVal CampoNome As String, ByVal tabela As String, Optional ByVal campoID As String = "", Optional ByVal campoCriterio As String = "", Optional ByVal sinal As String = "=", Optional ByVal criterio As String = "")
        'define o comando SQL para selecionar os dados da tabela funcionarios
        Dim strSQL As String
        Dim da As MySqlDataAdapter
        Dim ds As New DataSet
        Dim dt As New DataTable 'Criacao de uma tabela
        Dim dr As DataRow
        Dim drNovaRow As DataRow
        ReabreConexao(ConexaoOpenMRSReporting3)

        If Not (campoID = Nothing) Then
            If campoCriterio = Nothing Then
                strSQL = "Select distinct " & campoID & "," & CampoNome & " from " & tabela & " order by 2"
            Else
                strSQL = "Select distinct " & campoID & "," & CampoNome & " from " & tabela & " where " & campoCriterio & sinal & "'" & criterio & "' order by 2"
            End If

            da = New MySqlDataAdapter(strSQL, ConexaoOpenMRSReporting3)

            'preenche o dataset com os dados da tabela Seleccionada
            da.Fill(ds, tabela)

            dt.Columns.Add(CampoNome, GetType(System.String)) ' Cria um campo com o nome camponome introduzido
            dt.Columns.Add(campoID, GetType(System.String)) 'cria um outro campo com o nome campoID introduzido

            'percorre a tabela introduzida definida e preenche com dados da tabela da base de dados 
            For Each dr In ds.Tables(tabela).Rows()
                drNovaRow = dt.NewRow()
                drNovaRow(CampoNome) = dr(CampoNome)
                drNovaRow(campoID) = dr(campoID)
                dt.Rows.Add(drNovaRow)
            Next

            With cbo
                .DataSource = dt
                .DisplayMember = CampoNome
                .ValueMember = campoID
                'cbo.SelectedIndex = 0
            End With
        Else
            If campoCriterio = Nothing Then
                strSQL = "Select distinct " & CampoNome & " from " & tabela & " order by 1"
            Else
                strSQL = "Select distinct " & CampoNome & " from " & tabela & " where " & campoCriterio & "= " & criterio & " order by 1"
                'strSQL = "Select " & CampoNome & " from " & tabela & " where " & campoCriterio & sinal & "'" & criterio & "'"
            End If

            da = New MySqlDataAdapter(strSQL, ConexaoOpenMRSReporting3)

            'preenche o dataset com os dados da tabela Seleccionada
            da.Fill(ds, tabela)

            dt.Columns.Add(CampoNome, GetType(System.String)) ' Cria um campo com o nome camponome introduzido

            'percorre a tabela introduzida definida e preenche com dados da tabela da base de dados 
            For Each dr In ds.Tables(tabela).Rows()
                drNovaRow = dt.NewRow()
                drNovaRow(CampoNome) = dr(CampoNome)
                dt.Rows.Add(drNovaRow)
            Next

            With cbo
                .DataSource = dt
                .DisplayMember = CampoNome
                .ValueMember = CampoNome
                'cbo.SelectedIndex = 0
            End With
        End If

    End Sub
    Public Sub EncherCombo1(ByVal cbo As ComboBox, ByVal CampoNome As String, ByVal tabela As String, Optional ByVal campoID As String = "", Optional ByVal Criterio As String = "")
        'define o comando SQL para selecionar os dados da tabela funcionarios
        Dim strSQL As String
        Dim da As MySqlDataAdapter
        Dim ds As New DataSet
        Dim dt As New DataTable 'Criacao de uma tabela
        Dim dr As DataRow
        Dim drNovaRow As DataRow
        ReabreConexao(ConexaoOpenMRSReporting3)

        If Not (campoID = Nothing) Then
            If Criterio = Nothing Then
                strSQL = "Select distinct " & campoID & "," & CampoNome & " from " & tabela & " order by 2"
            Else
                strSQL = "Select distinct " & campoID & "," & CampoNome & " from " & tabela & " where " & Criterio & " order by 2"
            End If

            da = New MySqlDataAdapter(strSQL, ConexaoOpenMRSReporting3)

            'preenche o dataset com os dados da tabela Seleccionada
            da.Fill(ds, tabela)

            dt.Columns.Add(CampoNome, GetType(System.String)) ' Cria um campo com o nome camponome introduzido
            dt.Columns.Add(campoID, GetType(System.String)) 'cria um outro campo com o nome campoID introduzido

            'percorre a tabela introduzida definida e preenche com dados da tabela da base de dados 
            For Each dr In ds.Tables(tabela).Rows()
                drNovaRow = dt.NewRow()
                drNovaRow(CampoNome) = dr(CampoNome)
                drNovaRow(campoID) = dr(campoID)
                dt.Rows.Add(drNovaRow)
            Next

            With cbo
                .DataSource = dt
                .DisplayMember = CampoNome
                .ValueMember = campoID
                'cbo.SelectedIndex = 0
            End With
        Else
            If Criterio = Nothing Then
                strSQL = "Select distinct " & CampoNome & " from " & tabela & " order by 1"
            Else
                strSQL = "Select distinct " & CampoNome & " from " & tabela & " where " & Criterio & " order by 1"
            End If

            da = New MySqlDataAdapter(strSQL, ConexaoOpenMRSReporting3)

            'preenche o dataset com os dados da tabela Seleccionada
            da.Fill(ds, tabela)

            dt.Columns.Add(CampoNome, GetType(System.String)) ' Cria um campo com o nome camponome introduzido

            'percorre a tabela introduzida definida e preenche com dados da tabela da base de dados 
            For Each dr In ds.Tables(tabela).Rows()
                drNovaRow = dt.NewRow()
                drNovaRow(CampoNome) = dr(CampoNome)
                dt.Rows.Add(drNovaRow)
            Next

            With cbo
                .DataSource = dt
                .DisplayMember = CampoNome
                .ValueMember = CampoNome
                'cbo.SelectedIndex = 0
            End With
        End If

    End Sub

    Public Sub EncherComboComSQL(ByVal cbo As ComboBox, ByVal CampoNome As String, ByVal sqlQuery As String, Optional ByVal campoID As String = "")
        'define o comando SQL para selecionar os dados da tabela funcionarios
        Dim strSQL As String
        Dim da As MySqlDataAdapter
        Dim ds As New DataSet
        Dim dt As New DataTable 'Criacao de uma tabela
        Dim dr As DataRow
        Dim drNovaRow As DataRow
        ReabreConexao(ConexaoOpenMRSReporting3)

        If Not (campoID = Nothing) Then

            strSQL = "Select distinct " & campoID & "," & CampoNome & sqlQuery


            da = New MySqlDataAdapter(strSQL, ConexaoOpenMRSReporting3)

            'preenche o dataset com os dados da tabela Seleccionada
            da.Fill(ds, "tabela")

            dt.Columns.Add("Nome", GetType(System.String)) ' Cria um campo com o nome camponome introduzido
            dt.Columns.Add("ID", GetType(System.String)) 'cria um outro campo com o nome campoID introduzido

            'percorre a tabela introduzida definida e preenche com dados da tabela da base de dados 
            For Each dr In ds.Tables("tabela").Rows()
                drNovaRow = dt.NewRow()
                drNovaRow("Nome") = dr("Nome")
                drNovaRow("ID") = dr("ID")
                dt.Rows.Add(drNovaRow)
            Next

            With cbo
                .DataSource = dt
                .DisplayMember = "Nome"
                .ValueMember = "ID"
                'cbo.SelectedIndex = 0
            End With
        Else

            strSQL = "Select distinct " & CampoNome & sqlQuery


            da = New MySqlDataAdapter(strSQL, ConexaoOpenMRSReporting3)

            'preenche o dataset com os dados da tabela Seleccionada
            da.Fill(ds, "tabela")

            dt.Columns.Add("Nome", GetType(System.String)) ' Cria um campo com o nome camponome introduzido

            'percorre a tabela introduzida definida e preenche com dados da tabela da base de dados 
            For Each dr In ds.Tables("tabela").Rows()
                drNovaRow = dt.NewRow()
                drNovaRow("Nome") = dr("Nome")
                dt.Rows.Add(drNovaRow)
            Next

            With cbo
                .DataSource = dt
                .DisplayMember = "Nome"
                .ValueMember = "Nome"
                'cbo.SelectedIndex = 0
            End With
        End If

    End Sub

    Public Function TabelaDinamica(ByVal strSQL As String) As DataSet
        'define o comando SQL para selecionar os dados da tabela funcionarios
        'Dim strSQL As String
        Dim myAdapter As New MySqlDataAdapter
        Dim myData As New DataSet
        Dim cmd As New MySqlCommand
        RecourdCount = 0
        ReabreConexao(ConexaoOpenMRSReporting3)
        Try
            With cmd
                .Connection = ConexaoOpenMRSReporting3 'cone.conectar
                .CommandType = CommandType.Text
                .CommandText = strSQL
                myAdapter.SelectCommand = cmd
                RecourdCount = myAdapter.Fill(myData)
                '.Connection.close()
                '.Connection().Dispose()
                'myData.WriteXmlSchema("C:\DataSources\OpenMRS\CD4.xml")
                Return myData
            End With
        Catch ex As Exception
            MsgBox("Houve Erro na TabelaDinamica." & ex.Message)
            RecourdCount = 0
            '.Connection.close()
            '.Connection.Dispose()
            Return New DataSet
        End Try


    End Function


    Public Function TabelaDinamicaOpenMRSDATABASE(ByVal strSQL As String) As DataSet
        'define o comando SQL para selecionar os dados da tabela funcionarios
        'Dim strSQL As String
        Dim myAdapter As New MySqlDataAdapter
        Dim myData As New DataSet
        Dim cmd As New MySqlCommand
        RecourdCount = 0
        ReabreConexao(ConexaoOpenMRS3)

        Try
            With cmd
                .Connection = ConexaoOpenMRS3 'cone.conectar
                .CommandType = CommandType.Text
                .CommandText = strSQL


                myAdapter.SelectCommand = cmd
                RecourdCount = myAdapter.Fill(myData)
                '.Connection.close()
                '.Connection().Dispose()

                'myData.WriteXmlSchema("C:\DataSources\OpenMRS\PacienteFridaSemInicioTarv.xml")
                Return myData
            End With
        Catch ex As Exception
            MsgBox("Houve Erro na TabelaDinamica OpenMRS." & ex.Message)
            RecourdCount = 0
            '.Connection.close()
            '.Connection.Dispose()
            Return New DataSet
        End Try
    End Function

    Public Function verificaTextBox(ByVal txtbox As TextBox, ByVal lbl As Label) As Boolean
        If Trim(txtbox.Text) = Nothing Then
            MsgBox("O campo " & lbl.Text & " Deve ser preenchido.")
            txtbox.Focus()
            Return False
        End If
        Return True
    End Function
    Public Sub proibirLetras(ByVal c As System.Windows.Forms.KeyPressEventArgs, ByVal t As String)
        Dim i As Int16 = 0
        Dim j As Int16 = 1
        'MsgBox(Asc(c.KeyChar))
        If ((Asc(c.KeyChar) <> 8) And (Asc(c.KeyChar) <> 46) And (Asc(c.KeyChar) <> 44)) Then
            'If Asc(e.KeyChar) <> 46 Then
            If Asc(c.KeyChar) < Asc("0") Or Asc(c.KeyChar) > Asc("9") Then
                c.KeyChar = ""
            End If
        Else
            For i = 0 To t.Length - 1
                If t.Chars(i) = "." Then
                    j = j + 1
                    If j > 1 Then
                        If Asc(c.KeyChar) = 46 Then
                            c.KeyChar = ""
                        End If

                    End If
                End If
            Next
        End If
        'End If
    End Sub
    Public Sub proibirLetrasInt(ByVal c As System.Windows.Forms.KeyPressEventArgs, ByVal t As String)
        Dim i As Int16 = 0
        Dim j As Int16 = 1

        If ((Asc(c.KeyChar) <> 8) And (Asc(c.KeyChar) <> 44)) Then
            'If Asc(e.KeyChar) <> 46 Then
            If Asc(c.KeyChar) < Asc("0") Or Asc(c.KeyChar) > Asc("9") Then
                c.KeyChar = ""
            End If
        End If
    End Sub
    Public Function numeroValido(ByVal num As String) As Boolean
        Dim i As Int64
        Dim k, j As Int64
        If num <> Nothing Then
            If num.StartsWith(",") Then Return False
            For i = 0 To num.Length - 2
                If ((num.Chars(i) = ",") And (num.Chars(i + 1) = ",")) Or ((num.Chars(i) = ",") And (num.Chars(i + 1) = ".")) Or ((num.Chars(i) = ".") And (num.Chars(i + 1) = ",")) Then
                    Return False
                End If
            Next
            If num.EndsWith(".") Or num.EndsWith(",") Then Return False
            k = num.LastIndexOf(".")
            j = num.LastIndexOf(",")

            If k > 0 And j > 0 And j > k Then Return False
        End If
        Return True
    End Function
    Public Function VerificaRS(ByVal rs As MySqlDataReader, ByVal i As Int16) As String

        If rs.IsDBNull(i) Then
            Return ""
        Else
            Return rs.GetString(i)
        End If
    End Function
    Public Function VerificaRI(ByVal rs As MySqlDataReader, ByVal i As Int16) As Int32
        If rs.IsDBNull(i) Then
            Return 0
        Else
            Return rs.GetInt32(i)
        End If
    End Function
    Public Function VerificaDBL(ByVal rs As MySqlDataReader, ByVal i As Int16) As Double
        If rs.IsDBNull(i) Then
            Return 0.0
        Else
            Return rs.GetDouble(i)
        End If
    End Function
    Public Function VerificaRDate(ByVal rs As MySqlDataReader, ByVal i As Int16) As MySqlDateTime
        If rs.IsDBNull(i) Then
            Return New MySqlDateTime(Now.Year & "/" & Now.Month & "/" & Now.Day)
        Else
            Return rs.GetMySqlDateTime(i)
        End If
    End Function

    Public Sub preencheDataGrid(ByVal SQL As String, ByVal grid As DataGridView)
        Try
            ' Especifica a string de conexão acessando o banco de dados Northiwnd no
            ' MSDE na maquina local
            conn = ConexaoOpenMRS1 'cone.conectar()
            'Dim connectionString As String = _
            '"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Northwind;User ID=sa;password=;Data Source=MACORATI\VSDOTNET"

            'exemplo de string de conexão para o SQL Server na maquina local acessando Northwind
            ' "Integrated Security=SSPI;Persist Security Info=False;" + _
            ' "Initial Catalog=Northwind;Data Source=localhost"

            'Cria um novo data adapter baseado na consulta definida
            'Dim adapter As New SqlDataAdapter()


            Dim dataAdapter As New MySqlDataAdapter(SQL, conn)
            'Dim dataAdapter As New SqlDataAdapter(selectCommand, connectionString)


            ' Cria um command builder para gerar os SQL para atualizar, incluir e
            ' deletar baseados no selectCommand. 

            Dim commandBuilder As New MySqlCommandBuilder(dataAdapter)
            'Dim commandBuilder As New SqlCommandBuilder(dataAdapter)

            ' Preenche um novo data table e vincula ao BindingSource.
            Dim table As New DataTable()
            Dim bdsrc As New BindingSource()
            table.Locale = System.Globalization.CultureInfo.InvariantCulture
            dataAdapter.Fill(table)
            bdsrc.DataSource = table
            'bdsrc.DataMember = table.Columns(0).ColumnName
            'BindingSource1.DataSource = table

            ' Redimensiona as colunas do DataGridView
            'grid.AutoResizeColumns( _
            'DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader)
            grid.MultiSelect = False
            grid.RowHeadersVisible = False
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            grid.DataSource = bdsrc.DataSource
            'grid.DataMember = bdsrc.DataMember

            conn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try

    End Sub
    Public Function itemExiste(ByVal lst As ListView, ByVal elem As String) As Boolean
        If lst.Items.Count <= 0 Then Return False
        Dim i As Int32
        Dim ite As New ListViewItem
        For i = 0 To lst.Items.Count - 1
            ite = lst.Items(i)
            If ite.Text = elem Then Return True
        Next
    End Function
    Public Function provincia(ByVal CodP As String) As String
        Dim s As String = ""
        With cmm
            .Connection = ConexaoOpenMRS3 'cone.conectar
            .CommandType = CommandType.Text
            .CommandText = "select nome from provincia where codigo='" & CodP & "'"
            s = .ExecuteScalar
            '.Connection.close()
            '.Connection.Dispose()
            Return s
        End With
    End Function
    Public Function distrito(ByVal codD As String) As String
        Dim s As String = ""
        With cmm
            .Connection = ConexaoOpenMRS3 'cone.conectar
            .CommandType = CommandType.Text
            .CommandText = "select nome from distrito where codigo='" & codD & "'"
            s = .ExecuteScalar
            '.Connection.close()
            '.Connection.Dispose()
            Return s
        End With
    End Function
    Public Function PostoAdmin(ByVal codPosto As String) As String
        Dim s As String = ""
        With cmm
            .Connection = ConexaoOpenMRS3 'cone.conectar
            .CommandType = CommandType.Text
            .CommandText = "select nome from localidade where codigo='" & codPosto & "'"
            s = .ExecuteScalar
            '.Connection.close()
            '.Connection.Dispose()
            Return s
        End With
    End Function
    Public Function UnidadeS(ByVal codUS As String) As String
        Dim s As String = ""
        With cmm
            .Connection = ConexaoOpenMRS3 'cone.conectar
            .CommandType = CommandType.Text
            .CommandText = "select nomeus from unidadesanitaria where unidadeid='" & codUS & "'"
            s = .ExecuteScalar
            '.Connection.close()
            '.Connection.Dispose()
            Return s
        End With
    End Function
    Public Function Trabalhador(ByVal bi As String) As String
        Dim rs As MySqlDataReader
        Dim s As String = ""
        With cmm
            .Connection = ConexaoOpenMRS3 'cone.conectar
            .CommandType = CommandType.Text
            .CommandText = "select trabalhador.nome from trabalhador,unidadeSanitaria where trabalhador.bi=unidadesanitaria.executor and " & _
            "unidadeSanitaria.unidadeID='" & bi & "'"
            rs = .ExecuteReader

            If rs.HasRows Then
                While rs.Read
                    s = rs.GetString(0)
                End While
            End If
            FechaResultSet(rs)
            '.Connection.close()
            '.Connection.Dispose()
            Return s
        End With
    End Function
    Public Function MySQLScape(ByVal str As String) As String
        Dim help As String
        Dim i, j As Integer
        help = str
        j = 0
        If help <> Nothing Then
            For i = 0 To help.Length - 1
                If help.Chars(i) = "\" Or help.Chars(i) = "/" Or help.Chars(i) = "'" Then
                    str = str.Insert(i + j, "\")
                    j = j + 1
                End If
            Next
        End If
        Return str
    End Function
    Public Sub loadFigura(ByVal ope As OpenFileDialog, ByVal pic As PictureBox)
        Dim ext As String
        With ope
            .FileName = ""
            .Filter = "All Files|*.*|Bitmaps|*.bmp|JPEGs (*.jpg)|*.jpg|GIFs(*.gif)|*.gif|FireFox(*.png)|*.png"
            .FilterIndex = 1
            .DefaultExt = "jpg"
            .Multiselect = False
            .CheckFileExists = True
            .CheckPathExists = True
            .ShowDialog()
            If Not String.IsNullOrEmpty(.FileName) Then
                ext = .FileName.Substring(.FileName.Length - 4, 4).ToLower
                If ext = ".jpg" Or ext = ".gif" Or ext = ".png" Or ext = ".bmp" Then
                    pic.Image = Image.FromFile(ope.FileName)
                    imagemFromDataBase = False
                End If
            End If
        End With
    End Sub
    Public Function OpenDataBase(ByVal ope As OpenFileDialog) As String
        Dim ext As String
        With ope
            .FileName = ""
            .Filter = "MS Access|*.mdb"
            .FilterIndex = 1
            .DefaultExt = "mdb"
            .Multiselect = False
            .CheckFileExists = True
            .CheckPathExists = True
            .ShowDialog()
            If Not String.IsNullOrEmpty(.FileName) Then
                ext = .FileName.Substring(.FileName.Length - 4, 4).ToLower
                If ext = ".mdb" Then
                    Return .FileName
                End If
            End If
            Return ""
        End With
    End Function
    Public Function OpenExcelFile(ByVal ope As OpenFileDialog) As String
        Dim ext As String
        With ope
            .FileName = ""
            .Filter = "Excel Files|*.xlsx"
            .FilterIndex = 1
            .DefaultExt = "xlsx"
            .Multiselect = False
            .CheckFileExists = True
            .CheckPathExists = True
            .ShowDialog()
            If Not String.IsNullOrEmpty(.FileName) Then
                ext = .FileName.Substring(.FileName.Length - 5, 5).ToLower
                If ext = ".xlsx" Then
                    Return .FileName
                End If
            End If
            Return ""
        End With
    End Function

    Public Function dataMySQL(ByVal dt As Date) As String

        If dt <> Nothing Then
            Dim dia, mes, ano, hora, minutos As Integer
            Dim dt1 As String
            Dim d, m As String
            dia = dt.Day
            mes = dt.Month
            ano = dt.Year
            hora = dt.Hour
            minutos = dt.Minute
            If dia < 10 Then
                d = "0" & dia
            Else
                d = dia
            End If
            If mes < 10 Then
                m = "0" & mes
            Else
                m = mes
            End If
            dt1 = ano & "-" & m & "-" & d
            If hora > 0 Then
                dt1 &= " " & hora & ":" & minutos
            End If
            Return dt1
        Else
            Return "0000-00-00"
        End If

    End Function
    Public Sub imageSave(ByVal picLocal As String, ByVal id As String, ByVal tabela As String)

        'Note aqui considero que as minha tabela tem dois campos id e imagem
        Try
            'Localizacao da imagem a gravar na base de dados
            'Dim local As String = pic.ImageLocation

            'Abrir a imagem para leitura
            Dim Fs As FileStream = New FileStream(picLocal, FileMode.Open, FileAccess.Read)

            'Criar um array de bytes para guardar a imagem
            Dim imagem As Byte()
            'Redimensionar o array com o comprimento da imagem
            ReDim imagem(Fs.Length)

            ' Ler a imagem o colocar no array pois blob é um array de bytes
            Fs.Read(imagem, 0, System.Convert.ToInt64(Fs.Length))

            'Fechar o File Stream
            Fs.Close()

            Dim sql As String = "INSERT INTO " & tabela & " (ID, imagem) VALUES (?id,?foto)"

            '?id sao parametros para no caso Java sao os PreparedStatement
            With cmm
                .Connection = ConexaoOpenMRSReporting3 'cone.conectar
                .CommandType = CommandType.Text
                .CommandText = sql
                .Prepare() 'Preparar para insercao de valores para os parametros
                If .Parameters.Contains("?id") Then
                    .Parameters.RemoveAt("?id")
                End If
                If .Parameters.Contains("?foto") Then
                    .Parameters.RemoveAt("?foto")
                End If
                .Parameters.AddWithValue("?id", id)
                .Parameters.AddWithValue("?foto", imagem)
                .ExecuteNonQuery()
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub imageSave1(ByVal PictureBox1 As PictureBox, ByVal id As String, ByVal tabela As String)


        Dim ms As New MemoryStream
        PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
        Dim arrImage() As Byte = ms.GetBuffer

        ms.Close()



        'Dim sql As String = "INSERT INTO " & tabela & " (ID, imagem) VALUES (?id,?foto)"

        Dim SqlComando As New MySqlCommand() 'sql, conexao 'cone.conectar)

        With SqlComando
            Try
                .Connection = ConexaoOpenMRSReporting3 'cone.conectar
                .CommandType = CommandType.Text

                '.CommandText = "Start Transaction"
                '.ExecuteNonQuery()

                .CommandText = "INSERT INTO " & tabela & " (ID,imagem) VALUES (?id,?foto)"
                .Parameters.Add(New MySqlParameter("?id", MySqlDbType.String, 50)).Value = id
                .Parameters.Add(New MySqlParameter("?foto", MySqlDbType.LongBlob)).Value = arrImage
                .ExecuteNonQuery()

                '.CommandText = "commit"
                '.ExecuteNonQuery()

                '.Connection.close()
                '.Connection.Dispose()


            Catch ex As MySqlException
                '.CommandText = "RollBack"
                '.ExecuteNonQuery()
                MsgBox(ex.ErrorCode & "  " & ex.Message)
            Catch ex As Exception
                '.CommandText = "RollBack"
                '.ExecuteNonQuery()
                MsgBox(ex.Message)
            End Try
        End With

    End Sub

    Public Sub loadPicFromDB(ByVal tabela As String, ByVal id As String, ByVal lstPictures As ListBox, ByVal picbox As PictureBox)
        Dim da As MySqlDataAdapter

        Dim dsPictures As DataSet

        Try
            Dim sql As String = "Select * from " & tabela & " where id='" & id & "'"

            Dim MySqlComando As New MySqlCommand(sql, ConexaoOpenMRSReporting3)

            da = New MySqlDataAdapter(MySqlComando)

            dsPictures = New DataSet

            da.Fill(dsPictures)

            With lstPictures
                .DataSource = dsPictures.Tables(0)
                .DisplayMember = "id"
            End With
            If lstPictures.Items.Count > 0 Then
                lstPictures.SelectedIndex = 0
                Dim arrPicture() As Byte = _
             CType(dsPictures.Tables(0).Rows(lstPictures.SelectedIndex)("imagem"), Byte())
                Dim ms As New MemoryStream(arrPicture)

                With picbox
                    .Image = Image.FromStream(ms)
                    imagemFromDataBase = True
                End With
                ms.Close()

            End If

        Catch exMySQL As MySqlException
            MsgBox(exMySQL.Message)
        Catch exc As Exception
            MsgBox(exc.Message)
        End Try

    End Sub

    Public Sub LoadPicFromDBToPicBox(ByVal pic As PictureBox, ByVal id As String, ByVal tabela As String)
        With cmm
            .Connection = ConexaoOpenMRSReporting3 'cone.conectar
            .CommandType = CommandType.Text
            .CommandText = "Select imagem from " & tabela & " where id=?id"
            .Prepare()
            If .Parameters.Contains("?id") Then
                '.Parameters.Insert(0, id)
                .Parameters.RemoveAt("?id")
            End If
            .Parameters.AddWithValue("?id", id)
            Dim TamanhoImagem As Integer

            Dim imagem As Byte()

            imagem = .ExecuteScalar
            If imagem Is Nothing Then Exit Sub
            If imagem.Length <= 0 Then Exit Sub
            TamanhoImagem = imagem.GetUpperBound(0)

            Dim imagemPara As String = Directory.GetCurrentDirectory & "\imagemFromDB.jpg"

            Dim fs1 As FileStream = New FileStream(imagemPara, FileMode.OpenOrCreate, FileAccess.Write)
            fs1.Write(imagem, 0, TamanhoImagem)
            fs1.Close()
            pic.ImageLocation = imagemPara
            pic.Load()
            '.Connection.close()
            '.Connection.Dispose()
        End With
    End Sub
    Public Function indexSeleccionadoListview(ByVal list As ListView) As Integer
        Dim i As Integer
        For i = 0 To list.Items.Count - 1
            If list.Items(i).Selected Then
                Return i
            End If
        Next
    End Function
    Public Sub preencherLista(ByVal lista As ListView, ByVal rs As MySqlDataReader)
        lista.Items.Clear()
        Dim item1 As ListViewItem
        Dim j As Integer = rs.FieldCount
        Dim i As Int32

        While rs.Read
            item1 = New ListViewItem(rs.GetString(0))
            For i = 1 To j - 1
                If Not rs.IsDBNull(i) Then
                    item1.SubItems.Add(rs.GetString(i))
                Else
                    item1.SubItems.Add("")
                End If
            Next
            lista.Items.Add(item1)
        End While
        rs.Close()
    End Sub
    Public Function dataMicrosoft(ByVal dataMysql As MySql.Data.Types.MySqlDateTime) As Date
        If String.IsNullOrEmpty(dataMysql) Then Return New Date
        Dim dia As Int16 = dataMysql.Day
        Dim mes As Int16 = dataMysql.Month
        Dim ano As Int16 = dataMysql.Year
        If dia = 0 Or ano = 0 Or mes = 0 Then Return New Date
        dataMicrosoft = New Date(ano, mes, dia)

    End Function
    Public Function getReportPath() As String
        Dim reportPath As String = Application.StartupPath
        reportPath = reportPath.ToLower
        reportPath = reportPath.Substring(0, reportPath.Length - 9)
        Return reportPath & "reports\"
    End Function
    Public Function GerarParametros(ByVal ParamArray matriz() As String) As ParameterFields
        Dim c As Long, p1, p2 As String, l As Integer
        Dim parametros As New ParameterFields
        For c = 0 To matriz.Length - 1
            l = InStr(matriz(c), ";")
            If l > 0 Then
                p1 = Mid(matriz(c), 1, l - 1)
                p2 = Mid(matriz(c), l + 1, Len(matriz(c)) - l)
                Dim parametro As New ParameterField
                Dim dVal As New ParameterDiscreteValue
                parametro.ParameterFieldName = p1
                dVal.Value = p2
                parametro.CurrentValues.Add(dVal)
                parametros.Add(parametro)
            End If
        Next
        Return (parametros)
    End Function
    Public Sub PreencherDepartamentos(ByVal us As ComboBox, ByVal cboDept As ComboBox)
        'Dim str As String
        Dim nivel As Int16

        Dim str1 As String
        If us.Items.Count > 0 Then
            str1 = us.SelectedValue.ToString
        Else
            str1 = ""
        End If
        If str1 <> "" Then
            If Not str1.StartsWith("System") Then
                nivel = CShort(str1.Substring(8, 2))
            End If

        End If

        If nivel = 7 Then
            If us.Items.Count > 0 Then
                EncherCombo(cboDept, "DepartamentoNome", "Departamento", "DepartamentoID")
            End If
        Else
            cboDept.DataSource = Nothing
            cboDept.Items.Clear()
        End If
    End Sub

    Public Sub createDataSetOnDirectory(ByVal fileName As String, ByVal strQuery As String)
        Dim myData As New DataSet
        Dim Cmd As New MySqlCommand
        Dim myAdapter As New MySqlDataAdapter
        With Cmd
            .Connection = ConexaoOpenMRSReporting3 'cone.conectar
            .CommandType = CommandType.Text
            .CommandText = strQuery
            myAdapter.SelectCommand = Cmd
            myAdapter.Fill(myData)
            'myData.WriteXml("C:\" & fileName & ".xml", XmlWriteMode.WriteSchema)
        End With

    End Sub
    Public Sub fechaConexoes()
        GC.Collect()
        If ConexaoOpenMRSReporting3.State = ConnectionState.Open Then
            ConexaoOpenMRSReporting3.Close()
            ConexaoOpenMRSReporting3.Dispose()
        End If
        If ConexaoOpenMRSReporting2.State = ConnectionState.Open Then
            ConexaoOpenMRSReporting2.Close()
            ConexaoOpenMRSReporting2.Dispose()
        End If
        If ConexaoOpenMRSReporting1.State = ConnectionState.Open Then
            ConexaoOpenMRSReporting1.Close()
            ConexaoOpenMRSReporting1.Dispose()
        End If
        If ConexaoOpenMRS1.State = ConnectionState.Open Then
            ConexaoOpenMRS1.Close()
            ConexaoOpenMRS1.Dispose()
        End If
        If ConexaoOpenMRS2.State = ConnectionState.Open Then
            ConexaoOpenMRS2.Close()
            ConexaoOpenMRS2.Dispose()
        End If
        If ConexaoOpenMRS3.State = ConnectionState.Open Then
            ConexaoOpenMRS3.Close()
            ConexaoOpenMRS3.Dispose()
        End If
        If ICAPConection.State = ConnectionState.Open Then
            ICAPConection.Close()
        End If
    End Sub

    Public Function validarHoras(ByVal txt As String) As Boolean
        Try
            Dim horas As Int16 = txt.Substring(0, 2)
            Dim minutos As Int16 = txt.Substring(3, 2)
            Dim horaValida As Boolean = (horas >= 0 And horas <= 23)
            Dim minutosValidos As Boolean = (minutos >= 0 And minutos <= 59)

            If txt.IndexOf(":") <> 2 Then Return False
            Return (horaValida And minutosValidos)
        Catch ex As Exception
            Return False
        End Try

    End Function


    Public Function matchTime(ByVal valor As Int16) As Int16

        Select Case valor
            Case 1
                Return 13
            Case 2
                Return 14
            Case 3
                Return 15
            Case 4
                Return 16
            Case 5
                Return 17
            Case 6
                Return 18
            Case 7
                Return 19
            Case 8
                Return 20
            Case 9
                Return 21
            Case 10
                Return 22
            Case 11
                Return 23
        End Select

    End Function
    Public Function replaceWithReal(ByVal strData As String) As String
        Dim valor As Int16 = strData.Substring(11, 1)
        strData = strData.Remove(11, 1)
        strData = strData.Insert(11, matchTime(valor))
        Return strData
    End Function

    Public Sub changeServicoOnChangeDepartamento(ByVal cboDepartamento As ComboBox, ByVal cboServico As ComboBox, ByVal cboUS As ComboBox)
        Try
            Dim sqlQuery As String = " from Sector,depto_servico where sector.sectorid=depto_servico.sectorid and " & _
            " depto_servico.departamentoid=" & cboDepartamento.SelectedValue & " and depto_servico.unidadesanitaria='" & cboUS.SelectedValue & "'"
            EncherComboComSQL(cboServico, "sector.SectorNome as Nome", sqlQuery, "sector.SectorID as ID")
            If cboServico.Items.Count <= 0 Then
                EncherCombo(cboServico, "SectorNome", "Sector", "SectorID")
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Public Function getProvinciaTituloReport(ByVal util As UtilizadorVO) As String

    '    If utilizador.GetSetCodigoSPM = Constantes.SMHMAPUTOCOD Then
    '        Return "HOSPITAL CENTRAL DE MAPUTO"
    '    End If
    '    If utilizador.GetSetCodigoSPM = Constantes.SMHBEIRACOD Then
    '        Return "HOSPITAL CENTRAL DA BEIRA"
    '    End If
    '    If utilizador.GetSetCodigoSPM = Constantes.SMHNAMPULACOD Then
    '        Return "HOSPITAL CENTRAL DE NAMPULA"
    '    End If
    '    Return "PROVINCIA DE " & util.GetSetfuncionario.GetSetSpm.getSetProvincia.GetSetNome.ToUpper
    'End Function
    'Private Sub writeDataToExcellCells(ByVal dt_datatable As DataTable, ByVal objCells As Excel.Range)
    '    Dim iRow As Integer
    '    Dim iCol As Integer
    '    For iCol = 0 To dt_datatable.Columns.Count - 1
    '        objCells(1, iCol + 1) = dt_datatable.Columns(iCol).ToString

    '    Next
    '    For iRow = 0 To dt_datatable.Rows.Count - 1
    '        For iCol = 0 To dt_datatable.Columns.Count - 1
    '            objCells(iRow + 2, iCol + 1) = dt_datatable.Rows(iRow)(iCol)
    '        Next
    '    Next
    'End Sub
    'Public Sub ExportDataToExcelFile(ByVal ds As DataSet, ByVal fileTo As String)
    '    Dim objExcel As Excel.Application
    '    Dim objBooks As Excel.Workbooks
    '    Dim objBook As Excel.Workbook
    '    'Dim objSheets As Excel.Worksheets
    '    Dim objSheet As Excel.Worksheet
    '    Dim objRange As Excel.Range
    '    Dim dir As String

    '    fileTo = fileTo.Replace("/", "\")
    '    If Not (fileTo.EndsWith(".xls") Or fileTo.EndsWith(".xlsx")) Then
    '        MsgBox("Tem que exportar para um ficheiro Excel (.xls ou .xlsx)")
    '        Exit Sub
    '    End If
    '    dir = fileTo.Substring(0, fileTo.LastIndexOf("\"))
    '    If Not My.Computer.FileSystem.DirectoryExists(dir) Then
    '        My.Computer.FileSystem.CreateDirectory(dir)
    '    End If
    '    Try
    '        objExcel = New Excel.Application
    '        objExcel.Visible = False
    '        objExcel.DisplayAlerts = False
    '        objBook = CType(objExcel.Workbooks.Add(), Excel.Workbook)
    '        objBook.SaveAs(fileTo, Excel.XlFileFormat.xlWorkbookNormal, AccessMode:=Excel.XlSaveAsAccessMode.xlNoChange)
    '        objBooks = objExcel.Workbooks
    '        objSheet = CType(objBooks(1).Sheets.Item(1), Excel.Worksheet)
    '        objSheet.Name = "ExcelExport"
    '        objSheet.Range("A1", "Z1").Font.Bold = True
    '        'objSheet.Range("A1", "Z1").Font.Background( = Color.DarkGreen
    '        objSheet.Range("A1", "Z1").EntireColumn.AutoFit()
    '        objRange = objSheet.Cells
    '        writeDataToExcellCells(ds.Tables(0), objRange)

    '        'objSheet.SaveAs(fileTo)
    '        objBook.SaveAs(fileTo, Excel.XlFileFormat.xlWorkbookNormal, AccessMode:=Excel.XlSaveAsAccessMode.xlNoChange)
    '    Catch ex As Exception
    '        MsgBox("Error: " & ex.Message)
    '    Finally
    '        Try
    '            objExcel.Quit()
    '            ReleaseComObject(objExcel)
    '            ReleaseComObject(objBooks)
    '            ReleaseComObject(objBook)
    '            'ReleaseComObject(objSheets)
    '            ReleaseComObject(objSheet)
    '            ReleaseComObject(objRange)

    '            objExcel = Nothing
    '            objBooks = Nothing
    '            objBook = Nothing
    '            'objSheets = Nothing
    '            objSheet = Nothing
    '            objRange = Nothing
    '        Catch ex As Exception
    '            MsgBox("Erro na libertacao de recursos usados para exportacao")
    '        End Try

    '    End Try

    'End Sub
    Public Function GetPatientOpenMRSIDByNID(ByVal nid As String) As Integer
        
        Dim cmd As New MySqlCommand

        ReabreConexao(ConexaoOpenMRS3)
        Try
            With cmd
                .Connection = ConexaoOpenMRS3 'cone.conectar
                .CommandType = CommandType.Text
                .CommandText = "Select patient.patient_id from patient,patient_identifier " & _
                                " where patient_identifier.patient_id=patient.patient_id and identifier_type=2 and " & _
                                " identifier='" & nid & "'"
                
                Return .ExecuteScalar
            End With
        Catch ex As Exception
            MsgBox("Houve Erro ao Carregar Paciente a partir do NID" & ex.Message)
            
            Return 0
        End Try


    End Function

    Public Function GetOpenMRSProvider(ByVal accessID As Integer, ByVal locationID As Integer) As Integer

        Dim cmd As New MySqlCommand
        Dim provider As String

        ReabreConexao(ConexaoOpenMRS3)
        Try
            With cmd
                .Connection = ConexaoOpenMRS3 'cone.conectar
                .CommandType = CommandType.Text
                .CommandText = "Select openmrs_id from temp_export_provider " & _
                                " where access_id=" & accessID & " and " & _
                                " location_id=" & locationID & ""

                provider = .ExecuteScalar
            End With
            If Not String.IsNullOrEmpty(provider) Then
                Return provider
            Else
                Return 27
            End If
        Catch ex As Exception
            MsgBox("Houve Erro ao Carregar Paciente a partir do NID" & ex.Message)

            Return 0
        End Try


    End Function
    Public Sub InsertObsQuestion(ByVal personid As Integer, ByVal concept_id As Integer, ByVal value_coded As Integer, ByVal location As Integer, ByVal encounter_id As Integer, ByVal dataCreate As Date)

        Dim cmd As New MySqlCommand

        ReabreConexao(ConexaoOpenMRS3)
        'Try
        With cmd
            .Connection = ConexaoOpenMRS3 'cone.conectar
            .CommandType = CommandType.Text
            .CommandText = "insert into obs(person_id,concept_id,value_coded,encounter_id,obs_datetime,location_id,creator,date_created,uuid) " & _
                            " values(" & personid & "," & concept_id & "," & value_coded & ", " & encounter_id & ",'" & dataMySQL(dataCreate) & "'," & location & "," & _
                            " 22,now(),uuid())"

            .ExecuteNonQuery()
        End With
        'Catch ex As Exception
        'MsgBox("Houve erro ao inserir Observacao question: " & ex.Message)
        'End Try
    End Sub

    Public Sub InsertObsNumeric(ByVal personid As Integer, ByVal concept_id As Integer, ByVal numeric As Integer, ByVal location As Integer, ByVal encounter_id As Integer, ByVal dataCreate As Date)

        Dim cmd As New MySqlCommand

        ReabreConexao(ConexaoOpenMRS3)
        Try
            With cmd
                .Connection = ConexaoOpenMRS3 'cone.conectar
                .CommandType = CommandType.Text
                .CommandText = "insert into obs(person_id,concept_id,value_numeric,encounter_id,obs_datetime,location_id,creator,date_created) " & _
                                " values(" & personid & "," & concept_id & "," & numeric & ", " & encounter_id & ",'" & dataMySQL(dataCreate) & "'," & location & "," & _
                                " 22,now())"

                .ExecuteNonQuery()
            End With
        Catch ex As Exception
            MsgBox("Houve erro ao inserir Observacao question" & ex.Message)
        End Try
    End Sub
    Public Sub InsertObsText(ByVal personid As Integer, ByVal concept_id As Integer, ByVal text As String, ByVal location As Integer, ByVal encounter_id As Integer, ByVal dataCreate As Date)

        Dim cmd As New MySqlCommand

        ReabreConexao(ConexaoOpenMRS3)
        Try
            With cmd
                .Connection = ConexaoOpenMRS3 'cone.conectar
                .CommandType = CommandType.Text
                .CommandText = "insert into obs(person_id,concept_id,value_text,encounter_id,obs_datetime,location_id,creator,date_created) " & _
                                " values(" & personid & "," & concept_id & ",'" & text & "', " & encounter_id & ",'" & dataMySQL(dataCreate) & "'," & location & "," & _
                                " 22,now())"

                .ExecuteNonQuery()
            End With
        Catch ex As Exception
            MsgBox("Houve erro ao inserir Observacao question" & ex.Message)
        End Try
    End Sub
    Public Sub InsertObsDateTime(ByVal personid As Integer, ByVal concept_id As Integer, ByVal datetime As Date, ByVal location As Integer, ByVal encounter_id As Integer, ByVal dataCreate As Date)

        Dim cmd As New MySqlCommand

        ReabreConexao(ConexaoOpenMRS3)
        Try
            With cmd
                .Connection = ConexaoOpenMRS3 'cone.conectar
                .CommandType = CommandType.Text
                .CommandText = "insert into obs(person_id,concept_id,value_datetime,encounter_id,obs_datetime,location_id,creator,date_created) " & _
                                " values(" & personid & "," & concept_id & ",'" & dataMySQL(datetime) & "', " & encounter_id & ",'" & dataMySQL(dataCreate) & "'," & location & "," & _
                                " 22,now())"

                .ExecuteNonQuery()
            End With
        Catch ex As Exception
            MsgBox("Houve erro ao inserir Observacao question" & ex.Message)
        End Try
    End Sub
    Public Sub InsertObsBoolean(ByVal personid As Integer, ByVal concept_id As Integer, ByVal bool As Boolean, ByVal location As Integer, ByVal encounter_id As Integer, ByVal dataCreate As Date)

        Dim cmd As New MySqlCommand

        ReabreConexao(ConexaoOpenMRS3)
        Try
            With cmd
                .Connection = ConexaoOpenMRS3 'cone.conectar
                .CommandType = CommandType.Text
                .CommandText = "insert into obs(person_id,concept_id,value_boolean,encounter_id,obs_datetime,location_id,creator,date_created) " & _
                                " values(" & personid & "," & concept_id & "," & bool & ", " & encounter_id & ",'" & dataMySQL(dataCreate) & "'," & location & "," & _
                                " 22,now())"

                .ExecuteNonQuery()
            End With
        Catch ex As Exception
            MsgBox("Houve erro ao inserir Observacao question" & ex.Message)
        End Try
    End Sub

End Module
