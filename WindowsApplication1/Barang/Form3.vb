Imports MySql.Data.MySqlClient
Public Class Form3
    Public edt As Boolean = False
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.SelectedIndex = -1 Or TextBox3.Text = "" Or TextBox4.Text = "") Then
            MsgBox("Maaf Masukan Tidak Boleh Kosong")
        Else
            If (edt = True) Then
                Crud("Berhasil", "update barang set nama='" & TextBox2.Text & "', jenis='" & ComboBox1.Text & "', harga='" & TextBox3.Text & "', stok='" & TextBox4.Text & "' where kdbarang='" & TextBox1.Text & "' ")
                edt = False
                TextBox1.ReadOnly = False
                Me.Close()
                Form2.refresh()
            Else
                Crud("Berhasil", "insert into barang values('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & ComboBox1.Text & "', '" & TextBox3.Text & "', '" & TextBox4.Text & "')")
                Me.Close()
                Form2.refresh()
            End If
        End If
    End Sub

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
    End Sub
End Class