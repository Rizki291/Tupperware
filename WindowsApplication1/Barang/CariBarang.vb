Imports MySql.Data.MySqlClient
Public Class CariBarang

    Private Sub CariBarang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Connection()
        Tampildata(DataGridView1, "barang")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim i As Integer
        Transaksi.edt = True
        i = DataGridView1.CurrentRow.Index
        'Transaksi.Show()
        Transaksi.nmbrg = DataGridView1.Item(2, i).Value
        Transaksi.tkdbarang.Text = DataGridView1.Item(0, i).Value
        Transaksi.tharga.Text = DataGridView1.Item(3, i).Value
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        DS = New DataSet
        Query = "select * from barang where nama like '%" & TextBox1.Text & "%'"
        DA = New MySqlDataAdapter(Query, CONN)
        DA.Fill(DS)
        DataGridView1.DataSource = DS.Tables(0)
    End Sub
End Class