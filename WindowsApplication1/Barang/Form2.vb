Imports MySql.Data.MySqlClient
Public Class Form2
    
    Private Sub Form2_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Connection()
        Tampildata(DataGridView1, "barang")
        test1()
        Me.MaximizeBox = False
    End Sub
    Sub test1()
        With DataGridView1
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "No."
            .Columns(1).HeaderCell.Value = "Nama Barang"
            .Columns(2).HeaderCell.Value = "Jenis Barang"
            .Columns(3).HeaderCell.Value = "Harga Barang"
            .Columns(4).HeaderCell.Value = "Stok Barang"
        End With
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        'Form3.MdiParent = Form1
        Form3.Show()
    End Sub

    Sub refresh()
        Tampildata(DataGridView1, "barang")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim i As Integer
        Form3.edt = True
        i = DataGridView1.CurrentRow.Index
        Form3.Show()
        Form3.TextBox1.ReadOnly = True
        Form3.TextBox1.Text = DataGridView1.Item(0, i).Value
        Form3.TextBox2.Text = DataGridView1.Item(1, i).Value
        Form3.ComboBox1.Text = DataGridView1.Item(2, i).Value
        Form3.TextBox3.Text = DataGridView1.Item(3, i).Value
        Form3.TextBox4.Text = DataGridView1.Item(4, i).Value
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        If MsgBox("Yakin Hapus ?", vbInformation + vbYesNo) = vbYes Then
            Crud("Berhasil", "delete from barang where kdbarang='" & DataGridView1.Item(0, i).Value & "'")
        End If
        refresh()

    End Sub
End Class