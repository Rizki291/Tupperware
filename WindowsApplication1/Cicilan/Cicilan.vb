Imports MySql.Data.MySqlClient
Public Class Cicilan

    Private Sub Cicilan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tampildata(DataGridView1, "Cicilan")
        Me.MaximizeBox = False
        test1()
    End Sub
    Sub test1()
        With DataGridView1
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "No."
            .Columns(1).HeaderCell.Value = "No. Transaksi"
            .Columns(2).HeaderCell.Value = "Tanggal Transaksi"
            .Columns(3).HeaderCell.Value = "Total Bayar"
            .Columns(4).HeaderCell.Value = "Cicilan Sisa"
            .Columns(5).HeaderCell.Value = "Status Lunas"
            .Columns(6).HeaderCell.Value = "Nomer Member"
        End With
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim i As Integer = DataGridView1.CurrentRow.Index
        If (i = -1) Then
            MsgBox("Pilih Data Terlebih Dahulu")
        Else
            With DataGridView1
                If (DataGridView1.Item(5, i).Value = "Lunas") Then
                    MsgBox("Cicilan Sudah Lunas")
                Else
                    EditCicilan.Show()
                    EditCicilan.m = .Item(0, i).Value
                    EditCicilan.Label4.Text = .Item(4, i).Value
                End If
            End With
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim i As Integer = DataGridView1.CurrentRow.Index
        Dim result As Integer = MessageBox.Show("Anda Yakin Ingin Menghapus ?", "Hapus", MessageBoxButtons.YesNo)
        If result = DialogResult.No Then

        ElseIf result = DialogResult.Yes Then
            If (DataGridView1.Item(5, i).Value = "Lunas") Then
                Crud("Berhasil Dihapus", "Delete from Cicilan where IDcicilan=" & DataGridView1.Item(0, i).Value)
            Else
                MsgBox("Cicilan Belum Lunas")
            End If
        End If
        Tampildata(DataGridView1, "Cicilan")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        DS = New DataSet
        Query = "select * from cicilan where nama like '%" & TextBox1.Text & "%'"
        DA = New MySqlDataAdapter(Query, CONN)
        DA.Fill(DS)
        DataGridView1.DataSource = DS.Tables(0)
    End Sub
End Class