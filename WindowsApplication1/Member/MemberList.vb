Imports MySql.Data.MySqlClient
Public Class MemberList

    Private Sub MemberList_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False
        Connection()
        Tampildata(DataGridView1, "member")
        test1()

    End Sub
    Sub test1()
        With DataGridView1
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "No. Kartu"
            .Columns(1).HeaderCell.Value = "Nama Member"
            .Columns(2).HeaderCell.Value = "Alamat Member"
            .Columns(3).HeaderCell.Value = "Nomor HP"
            .Columns(4).HeaderCell.Value = "Tingkatan Member"
            .Columns(5).HeaderCell.Value = "Poin Member"
        End With
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Adduser.Show()
    End Sub
    Sub refresh()
        Tampildata(DataGridView1, "member")
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        If (DataGridView1.Item(0, i).Value = 0) Then
            MsgBox("Maaf Data Ini Tidak Dapat Diubah")
        Else
            Adduser.edt = True
            Adduser.Show()
            Adduser.noKartu.ReadOnly = True
            Adduser.noKartu.Text = DataGridView1.Item(0, i).Value
            Adduser.nama.Text = DataGridView1.Item(1, i).Value
            Adduser.alamat.Text = DataGridView1.Item(2, i).Value
            Adduser.nohp.Text = DataGridView1.Item(3, i).Value
            Adduser.cblevel.Text = DataGridView1.Item(4, i).Value
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        If MsgBox("Yakin Hapus ?", vbInformation + vbYesNo) = vbYes Then
            If (DataGridView1.Item(0, i).Value = 0) Then
                MsgBox("Maaf Data Ini Tidak Dapat Dihapus")
            Else
                Crud("Berhasil Menghapus Member", "delete from member where nokartu='" & DataGridView1.Item(0, i).Value & "'")
            End If
        End If
            refresh()
    End Sub
End Class