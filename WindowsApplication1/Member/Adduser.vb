Imports MySql.Data.MySqlClient
Public Class Adduser
    Public edt As Boolean = False
    Private Sub Adduser_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (edt = True) Then
            Crud("Berhasil", "update member set nama='" & nama.Text & "', alamat='" & alamat.Text & "', noHp='" & nohp.Text & "', lvl_member='" & cblevel.Text & "' where nokartu='" & noKartu.Text & "' ")
            edt = False
            noKartu.ReadOnly = False
            Me.Close()
            MemberList.refresh()
        Else
            'MsgBox("insert into member values('" & noKartu.Text & "', '" & nama.Text & "', '" & alamat.Text & "', '" & nohp.Text & "', '" & cblevel.Text & "', '0' )")
            Crud("Berhasil", "insert into member values('" & noKartu.Text & "', '" & nama.Text & "', '" & alamat.Text & "', '" & nohp.Text & "', '0', '0' )")
            Me.Close()
            MemberList.refresh()
        End If
    End Sub

    
End Class