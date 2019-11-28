
Public Class MainMenu

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Connection()
        Intro.MdiParent = Me
        Intro.Show()
        Intro.Dock = DockStyle.Fill
        Crud2("DELETE FROM penjualan WHERE tanggal < DATE_SUB(NOW(), INTERVAL 6 MONTH)")
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        CloseAll()
        Form2.MdiParent = Me
        Form2.Show()
        Form2.Dock = DockStyle.Fill
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        CloseAll()
        Transaksi.MdiParent = Me
        Transaksi.Show()
        Transaksi.Dock = DockStyle.Fill
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        CloseAll()
        MemberList.MdiParent = Me
        MemberList.Show()
        MemberList.Dock = DockStyle.Fill
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        CloseAll()
        Historyperdays.MdiParent = Me
        Historyperdays.Show()
        Historyperdays.Dock = DockStyle.Fill
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        CloseAll()
        Intro.MdiParent = Me
        Intro.Show()
        Intro.Dock = DockStyle.Fill
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        CloseAll()
        History.MdiParent = Me
        History.Show()
        History.Dock = DockStyle.Fill
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        CloseAll()
        Cicilan.MdiParent = Me
        Cicilan.Show()
        Cicilan.Dock = DockStyle.Fill
    End Sub
End Class
