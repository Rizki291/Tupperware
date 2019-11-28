Imports MySql.Data.MySqlClient
Public Class Login

    Private Sub Login_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        TextBox2.PasswordChar = "*"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call Connection()
        CMD = New MySqlCommand
        CMD.Connection = CONN
        Query = "select * from user where username='" & TextBox1.Text & "' and password='" & TextBox2.Text & "'"
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()
        DR = CMD.ExecuteReader()
        DR.Read()
        If Not DR.HasRows Then
            Call Connection()
            MsgBox("Login Gagal")
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox1.Focus()
        Else
            MsgBox("Login Berhasil")
            Me.Visible = False
            MainMenu.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class