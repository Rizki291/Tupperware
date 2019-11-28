Imports MySql.Data.MySqlClient
Public Class CariMember

    Private Sub CariMember_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Connection()
        Tampildata(DataGridView1, "member")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim i As Integer
        Transaksi.edt = True
        i = DataGridView1.CurrentRow.Index
        'Transaksi.Show()
        Transaksi.tnokartu.Text = DataGridView1.Item(0, i).Value
        If (DataGridView1.Item(0, i).Value = "0") Then
            Transaksi.TextBox1.Text = 0
            Transaksi.TextBox1.ReadOnly = True
        Else
            Transaksi.TextBox1.ReadOnly = False
        End If
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        DS = New DataSet
        Query = "select * from member where nama like '%" & TextBox1.Text & "%'"
        DA = New MySqlDataAdapter(Query, CONN)
        DA.Fill(DS)
        DataGridView1.DataSource = DS.Tables(0)
    End Sub
End Class