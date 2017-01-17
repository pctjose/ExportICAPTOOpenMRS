Imports ADODB
Imports MySql.Data.MySqlClient
Public Class frmMain
    Dim cnnStringOpenMRS As String
    

    Private Function getOpenMRSLocationID(ByVal i As Int16) As Int16
        Select Case i
            Case 0
                Return 6
            Case 1
                Return 4
            Case 2
                Return 7
            Case 3
                Return 8
            Case 4
                Return 3
            Case 5
                Return 5
            Case 6
                Return 17
            Case 7
                Return 398
            Case 8
                Return 400
            Case 9
                Return 399
        End Select
    End Function

    Private Sub cmdBrowser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowser.Click
        Me.txtLocation.Text = OpenDataBase(Me.OpenFileDialog1)
    End Sub

    Private Sub cmdFechar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFechar.Click
        'frmExportCCR.Show()
        'buildStringWhereQuey()
        fechaConexoes()
        Me.Close()

    End Sub

    Private Sub cmdICAP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdICAP.Click
        If Not Me.txtLocation.Text = "" Then
            Try
                Dim cnn As New Connection
                Dim cmm As New Command
                Dim rs As New Recordset
                cnn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                "Data Source=" & Me.txtLocation.Text
                cnn.Open()
                MsgBox("ICAP Connection made succefully")
                cnn.Close()
            Catch ex As Exception
                MsgBox("There where error on ICAP Connection: " & ex.Message)
            End Try
        Else
            MsgBox("Browser the data base before ...")
        End If
    End Sub

    Private Sub cmdOpenMRS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenMRS.Click
        If Me.txtServerHost.Text = "" Then
            MsgBox("Specify the OpenMRS server Host...")
            Me.txtServerHost.Focus()
            Exit Sub
        End If
        If Me.txtUserName.Text = Nothing Then
            MsgBox("Specify the OpenMRS server User Name...")
            Me.txtUserName.Focus()
            Exit Sub
        End If
        If Me.txtPassword.Text = Nothing Then
            MsgBox("Specify the OpenMRS server Password...")
            Me.txtPassword.Focus()
            Exit Sub
        End If
        If Me.txtPort.Text = Nothing Then
            MsgBox("Specify the OpenMRS server Port...")
            Me.txtPort.Focus()
            Exit Sub
        End If

        Dim conn As MySqlConnection = New MySqlConnection()
        Try
            If Me.txtDataBase.Text = Nothing Then
                cnnStringOpenMRS = "Database=openmrs;Uid=" & Me.txtUserName.Text & ";Pwd=" & Me.txtPassword.Text & ";Server=" & Me.txtServerHost.Text & ";port=" & Me.txtPort.Text & ";default command timeout=100;Connection Timeout=20"
            Else
                cnnStringOpenMRS = "Database=" & Me.txtDataBase.Text & ";Uid=" & Me.txtUserName.Text & ";Pwd=" & Me.txtPassword.Text & ";Server=" & Me.txtServerHost.Text & ";port=" & Me.txtPort.Text & ";default command timeout=100;Connection Timeout=20"
            End If

            conn.ConnectionString = cnnStringOpenMRS '"Database=dm_#misau;user=root;password=dm2007misau;Server=localhost;port=3306"
            conn.Open()
            MsgBox("OpenMRS Connection made successfully...")
            conn.Close()
        Catch ex As Exception
            MsgBox("There where error on OpenMRS Connection: " & ex.Message)
        End Try

    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cboLocation.SelectedIndex = 0
    End Sub

    Private Sub cmdImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImport.Click
        

        If Me.txtLocation.Text = Nothing Then
            MsgBox("Browser the data base before ...")
            Me.cmdBrowser.Focus()
            Exit Sub
        End If

        If Me.txtServerHost.Text = "" Then
            MsgBox("Specify the OpenMRS server Host...")
            Me.txtServerHost.Focus()
            Exit Sub
        End If
        If Me.txtUserName.Text = Nothing Then
            MsgBox("Specify the OpenMRS server User Name...")
            Me.txtUserName.Focus()
            Exit Sub
        End If
        If Me.txtPassword.Text = Nothing Then
            MsgBox("Specify the OpenMRS server Password...")
            Me.txtPassword.Focus()
            Exit Sub
        End If
        If Me.txtPort.Text = Nothing Then
            MsgBox("Specify the OpenMRS server Port...")
            Me.txtPort.Focus()
            Exit Sub
        End If
        If Me.chkUnlock.Checked Then
            AllPatients = True
        Else
            If Me.txtNID.Text = Nothing Then
                MsgBox("You must specify at least one valid patient NID")
                Me.txtNID.Focus()
                Exit Sub
            End If
            AllPatients = False
            whereQuery = buildStringWhereQuey()
        End If

        Try

            ICAPConection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                    "Data Source=" & Me.txtLocation.Text
            ICAPConection.Open()

            If Me.txtDataBase.Text = Nothing Then
                cnnStringOpenMRS = "Database=openmrs;Uid=" & Me.txtUserName.Text & ";Pwd=" & Me.txtPassword.Text & ";Server=" & Me.txtServerHost.Text & ";port=" & Me.txtPort.Text & ";default command timeout=100;Connection Timeout=20"
            Else
                cnnStringOpenMRS = "Database=" & Me.txtDataBase.Text & ";Uid=" & Me.txtUserName.Text & ";Pwd=" & Me.txtPassword.Text & ";Server=" & Me.txtServerHost.Text & ";port=" & Me.txtPort.Text & ";default command timeout=100;Connection Timeout=20"
            End If
            ConexaoOpenMRS1.ConnectionString = cnnStringOpenMRS
            ConexaoOpenMRS2.ConnectionString = cnnStringOpenMRS
            ConexaoOpenMRS3.ConnectionString = cnnStringOpenMRS
            ConexaoOpenMRS1.Open()
            ConexaoOpenMRS2.Open()
            ConexaoOpenMRS3.Open()
        Catch ex As Exception
            MsgBox("Error during connection: " & ex.Message)
            Exit Sub
        End Try
        
        If Me.chkLock.Checked Then
            Dim cmm22 As New MySqlCommand
            Dim patients As Integer
            cmm22.Connection = ConexaoOpenMRS1
            cmm22.CommandText = "Select count(*) from patient"
            patients = cmm22.ExecuteScalar
            If patients > 0 Then
                MsgBox("The import need empty (no patients) OpenMRS data base ... couldn't continue, try to empty your OpenMRS data base or Uncheck 'Lock OpenMRS Append'.")
                fechaConexoes()
                Exit Sub
            End If
        End If

        Cursor = Cursors.WaitCursor
        Me.Timer1.Enabled = True
        'Me.lblTempo.Text = Timer1.ToString
        Dim LocationID As Int16 = getOpenMRSLocationID(Me.cboLocation.SelectedIndex)

        encherTempo("Dados demograficos")

        Me.Progress.Value = 10
        Me.lblMessage.Text = "Importing Patients..."
        Me.StatusStrip1.Refresh()
        PatientUtils.ImportPatients(ICAPConection, LocationID)
        Me.Progress.Value = 20

        encherDataFim()

        encherTempo("Processos")

        Me.lblMessage.Text = "Importing Process..."
        Me.StatusStrip1.Refresh()
        Dim autil As New ProcessoUtils
        autil.importAdulto(LocationID)
        autil.importCrianca(LocationID)
        UpdatePatientProgram.UpdateCuidadoTratamento(ICAPConection, LocationID)
        Me.Progress.Value = 45

        encherDataFim()

        encherTempo("Consultas de Seguimento")

        Me.lblMessage.Text = "Importing Seguimento..."
        Me.StatusStrip1.Refresh()
        SeguimentoUtils.importSeguimento(ICAPConection, LocationID)
        Me.Progress.Value = 70

        encherDataFim()

        encherTempo("Consultas de Fila")

        Me.lblMessage.Text = "Importing Fila..."
        Me.StatusStrip1.Refresh()
        FILAUtils.ImportFILA(ICAPConection, LocationID)
        Me.Progress.Value = 80

        encherDataFim()

        encherTempo("Laboratorio")

        Me.lblMessage.Text = "Importing Laboratorio..."
        Me.StatusStrip1.Refresh()
        LabUtils.ImportLabReal(ICAPConection, LocationID)
        Me.Progress.Value = 90

        encherDataFim()

        encherTempo("Aconselhamento Pre-Tarv")

        Me.lblMessage.Text = "Importing Aconselhamento..."
        Me.StatusStrip1.Refresh()
        AconselhamentoUtils.ImportAconselhamento(ICAPConection, LocationID)
        Me.Progress.Value = 92

        encherDataFim()


        encherTempo("Aconselhamento: APSS/PP")

        Me.lblMessage.Text = "Importing Aconselhamento: APSS/PP ..."
        Me.StatusStrip1.Refresh()
        AconselhamentoUtils.ImportAconselhamento(ICAPConection, LocationID)
        ApssppUtil.importApssInicial(ICAPConection, LocationID)
        ApssppUtil.importApssSeguimento(ICAPConection, LocationID)
        Me.Progress.Value = 95

        encherDataFim()


        encherTempo("Rastreio de Tuberculose")

        Me.lblMessage.Text = "Importing Rastreio de Tuberculose..."
        Me.StatusStrip1.Refresh()
        TuberculoseRastreioUtils.ImportTuberculoseReal(ICAPConection, LocationID)

        encherDataFim()


        encherTempo("Gaac")

        Me.lblMessage.Text = "Importing Gaac..."
        Me.StatusStrip1.Refresh()
        GAAC.ImportGaac(ICAPConection, LocationID)
        Me.Progress.Value = 100

        encherDataFim()

        'encherTempo("Tratamento de Tuberculose")

        'Me.lblMessage.Text = "Importing Tratamento de Tuberculose..."
        'Me.StatusStrip1.Refresh()
        'TuberculoseTratamento.ImportTuberculoseTratamento(ICAPConection, LocationID)
        'Me.Progress.Value = 95

        'encherDataFim()

        'encherTempo("Actualizando Segundo Sitio")

        'Me.lblMessage.Text = "Updating Secondary Site..."
        'Me.StatusStrip1.Refresh()
        'ManageSegundoSitio.UpdateSegundoSitio(ICAPConection, LocationID)
        'Me.Progress.Value = 100

        'encherDataFim()

        Me.lblMessage.Text = "Importação Terminada (Finishied)"
        Me.StatusStrip1.Refresh()
        Me.Timer1.Enabled = False
        fechaConexoes()
        Cursor = Cursors.Default
    End Sub

    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    'Me.lblTempo.Text = DateTime.Now.ToLongTimeString
    '    Me.Refresh()
    '    Me.p()
    'End Sub
    Private Sub encherTempo(ByVal strMessage As String)
        Dim lstItem As ListViewItem
        lstItem = New ListViewItem(strMessage)
        lstItem.SubItems.Add(DateTime.Now.ToLongTimeString)
        Me.ListView1.Items.Add(lstItem)
        Me.ListView1.Refresh()
    End Sub
    Private Sub encherDataFim()
        Dim lstItem As ListViewItem
        lstItem = Me.ListView1.Items(Me.ListView1.Items.Count - 1)
        'MsgBox(Now.TimeOfDay.ToString)
        lstItem.SubItems.Add(DateTime.Now.ToLongTimeString)
        Me.ListView1.Items.RemoveAt(Me.ListView1.Items.Count - 1)
        Me.ListView1.Items.Add(lstItem)
        Me.ListView1.Refresh()
    End Sub

    
    Private Sub chkUnlock_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUnlock.CheckedChanged
        If Me.chkUnlock.Checked Then
            Me.txtNID.Text = ""
            Me.txtNID.ReadOnly = True
        Else
            Me.txtNID.ReadOnly = False
        End If
    End Sub
    Private Function buildStringWhereQuey() As String
        Dim str As String = ""
        Dim nids() As String
        nids = Me.txtNID.Text.Split(",")
        For Each nid As String In nids
            str &= "'" & nid & "',"
        Next
        If str.EndsWith(",") Then
            str = str.Remove(str.LastIndexOf(","))
        End If
        'MsgBox(str)

        Return str
    End Function
End Class
