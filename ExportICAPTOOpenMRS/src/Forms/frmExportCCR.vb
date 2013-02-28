Public Class frmExportCCR

    Private Sub cmdBrowser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowser.Click
        Me.txtLocation.Text = OpenExcelFile(Me.OpenFileDialog1)
    End Sub
End Class