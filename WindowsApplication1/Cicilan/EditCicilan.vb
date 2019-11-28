Public Class EditCicilan
    Public m As Integer = 0
    
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Dim total As Integer = 0
            total = Label4.Text - TextBox1.Text
            If (total <= 0) Then
                total = 0
                Crud2("Update Cicilan set Lunas='Lunas' where IDcicilan=" & m)
            End If
            Crud("Pembayaran Berhasil", "Update Cicilan set CicilanSisa='" & total & "' where IDcicilan=" & m)
        Catch ex As Exception
            MsgBox("Inputkan Data Terlebih Dahulu")
        End Try
        Tampildata(Cicilan.DataGridView1, "Cicilan")
        Me.Close()
    End Sub
End Class