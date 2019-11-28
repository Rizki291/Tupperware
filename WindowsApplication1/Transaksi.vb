Imports MySql.Data.MySqlClient
Public Class Transaksi
    Dim m As Integer = 0
    Public edt As Boolean = False
    Public nmbrg As String = ""
    Public poinbrg As Integer = 0
    Dim bayar As Integer = 0
    Private Sub Transaksi_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        m = 0
        Connection()
        idgenerator()
        Me.MaximizeBox = False
    End Sub
    Sub idgenerator()
        Static Generator As System.Random = New System.Random()
        tidtrans.Text = Generator.Next(100000, 99999999)
        tidtrans.ReadOnly = True
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        CariMember.Show()
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        If tjumlah.Text = "" Or tjumlah.Text = "0" Or TextBox1.Text = "" Or tkdbarang.Text = "" Or nmbrg = "" Then
            MsgBox("Masukan tidak boleh kosong")
        Else
            tidtrans.ReadOnly = True
            Button2.Enabled = False
            Dim s As String = Format(Date.Now(), "yyyy/MM/dd")
            With DataGridView1
                .Rows.Add(tidtrans.Text, tkdbarang.Text, nmbrg, tnokartu.Text, s, tharga.Text, tjumlah.Text, ltotal.Text)
            End With
            tkdbarang.Text = ""
            m += ltotal.Text
            lTotalHarga.Text = m
            tjumlah.Text = 0
            nmbrg = ""
        End If
    End Sub

    Sub Buttonw1()
        Dim strPrint As String
        Dim i As Integer = DataGridView1.RowCount
        Dim total As Integer = 0
        Dim total2 As Integer = 0
        With DataGridView1
            strPrint = "TUPPERWARE" & vbCrLf
            strPrint = strPrint & "No. Faktur : " & tidtrans.Text & vbCrLf
            strPrint = strPrint & "Tanggal : " & Format(Date.Now(), "yyyy/MM/dd") & vbCrLf
            strPrint = strPrint & "------------------------------" & vbCrLf
            For a As Integer = 0 To i - 1
                total2 = .Item(6, 0).Value * .Item(5, a).Value
                strPrint = strPrint & .Item(2, a).Value & vbCrLf
                strPrint = strPrint & .Item(6, a).Value & " x " & .Item(5, a).Value & "       " & total2 & vbCrLf
                total += total2
            Next
            'strPrint = strPrint & "Ciki     2   5000    10000" & vbCrLf
            'strPrint = strPrint & "Akua     3   1000     3000" & vbCrLf
            strPrint = strPrint & "------------------------------" & vbCrLf
            strPrint = strPrint & "Total:                " & total & vbCrLf
            strPrint = strPrint & "Tunai:                " & bayar & vbCrLf
            strPrint = strPrint & "------------------------------" & vbCrLf
            strPrint = strPrint & "Kembalian:            " & bayar - total & vbCrLf
        End With
        Printer.Print(strPrint)
    End Sub


    Private Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click
        tidtrans.ReadOnly = False
        Dim s As String = Format(Date.Now(), "yyyy/MM/dd") '02/09/2019
        Dim i As Integer = DataGridView1.RowCount
        Dim err As Integer = 0
        Dim ss As Boolean = False
        Dim total As Integer = 0
        Dim result As Integer = 0
        For a As Integer = 0 To i - 1
            If (ambildata(DataGridView1.Item(1, a).Value, DataGridView1.Item(6, a).Value) = True) Then
                ss = True
            End If
        Next

        If ss = False Then
            With DataGridView1
                Try
                    bayar = InputBox("Masukan Jumlah Tunai : ", "Bayar")
                    For a As Integer = 0 To i - 1
                        total += .Item(7, a).Value
                    Next
                    If (total - bayar <= 0) Then
                        MsgBox("Uang Kurang")
                    Else
                        
                        Crud2("insert into penjualan values('" & .Item(0, 0).Value & "','" & .Item(3, 0).Value & "','" & .Item(4, 0).Value & "','" & total & "')")
                        If (tnokartu.Text = "0") Then
                        Else
                            If (total >= 1000000 And total <= 1499999) Then
                                poinbrg = 20
                            ElseIf (total >= 1500000 And total <= 1999999) Then
                                poinbrg = 35
                            ElseIf (total >= 2000000 And total <= 2999999) Then
                                poinbrg = 60
                            ElseIf (total >= 3000000 And total <= 3999999) Then
                                poinbrg = 90
                            ElseIf (total >= 4000000 And total <= 4999999) Then
                                poinbrg = 120
                            ElseIf (total >= 5000000 And total <= 6499999) Then
                                poinbrg = 150
                            ElseIf (total >= 6500000 And total <= 7999999) Then
                                poinbrg = 200
                            ElseIf (total >= 8000000) Then
                                poinbrg = 250
                            End If
                            result = MessageBox.Show("Apakah pelanggan ingin melakukan cicilan?", "Cicil", MessageBoxButtons.YesNo)
                            If result = DialogResult.Yes Then
                                Crud2("insert into cicilan values('','" & .Item(0, 0).Value & "','" & Format(Date.Now, "yyyy-MM-dd") & "','" & total & "','" & total - bayar & "','Belum Lunas','" & .Item(3, 0).Value & "')")
                            End If
                            Crud2("update member set poin_member = poin_member + " & poinbrg & " where nokartu='" & .Item(3, 0).Value & "'")
                        End If
                        For a As Integer = 0 To i - 1
                            Crud("Transaksi Berhasil", "insert into penjualan_detail values('','" & .Item(1, a).Value & "','" & .Item(6, a).Value & "','" & .Item(7, a).Value & "','" & .Item(0, a).Value & "')")
                        Next

                        Buttonw1()
                    End If
                Catch ex As Exception

                End Try
            End With
        Else
            MsgBox("Maaf, Stok Barang Kurang")
        End If

        'MsgBox(s)
        m = 0
        lTotalHarga.Text = 0
        idgenerator()
        tkdbarang.Clear()
        tnokartu.Clear()
        tharga.Clear()
        tjumlah.Clear()
        Button2.Enabled = True
        ltotal.Text = ""
        DataGridView1.Rows.Clear()
        bayar = 0
    End Sub

    Private Sub tjumlah_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tjumlah.TextChanged
        If Val(TextBox1.Text) = 0 Then
            ltotal.Text = (Val(tharga.Text) * Val(tjumlah.Text))
        Else
            ltotal.Text = (Val(tharga.Text) * Val(TextBox1.Text) / 100) * Val(tjumlah.Text)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CariBarang.Show()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        For x As Integer = DataGridView1.Rows.Count - 1 To 0 Step -1
            If DataGridView1.Rows(x).Cells("HAPUS").Value Then
                m -= DataGridView1.Item(7, x).Value()
                lTotalHarga.Text = m
                DataGridView1.Rows.Remove(DataGridView1.Rows(x))
            End If
        Next
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        m = 0
        lTotalHarga.Text = 0
        idgenerator()
        Button2.Enabled = True
        tkdbarang.Clear()
        tnokartu.Text = 0
        tharga.Clear()
        TextBox1.ReadOnly = True
        tjumlah.Clear()
        ltotal.Text = ""
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If Val(TextBox1.Text) = 0 Then
            ltotal.Text = (Val(tharga.Text) * Val(tjumlah.Text))
        Else
            ltotal.Text = (Val(tharga.Text) * Val(TextBox1.Text) / 100) * Val(tjumlah.Text)
        End If

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub tjumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tjumlah.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
End Class