Imports MySql.Data.MySqlClient
Module Main
    Public CONN As MySqlConnection
    Public DA As MySqlDataAdapter
    Public DS As DataSet
    Public DS2 As DataSet
    Public DR As MySqlDataReader
    Public connString As String
    Public Query As String
    Public CMD As MySqlCommand
    Public DT As DataTable
    'Main Connection
    Sub Connection()
        connString = "server=localhost;uid=root;password=;database=tupperware"
        CONN = New MySqlConnection(connString)
        Try
            If CONN.State = ConnectionState.Closed Then
                CONN.Open()
                'MsgBox("Koneksi Berhasil")
            End If
        Catch ex As Exception
            MsgBox("Koneksi Gagal" & Err.Description)
        End Try
    End Sub
    Function ambildata(ByVal id As String, ByVal stok As Integer) As Boolean
        Dim bool As Boolean = False
        CMD = New MySqlCommand("Select stok from Barang where kdbarang='" & id & "'", CONN)
        DR = CMD.ExecuteReader
        If (DR.HasRows) Then
            While DR.Read
                bool = False
                If (stok >= DR.Item("stok")) Then
                    bool = True
                    'MsgBox("stok kurang")
                End If
            End While
        End If
        DR.Close()
        Return bool
    End Function
    Sub Tampildata(ByVal dgv As DataGridView, ByVal tabel As String)
        Try
            DS = New DataSet
            Query = "select * from " & tabel
            DA = New MySqlDataAdapter(Query, CONN)
            DA.Fill(DS)
            dgv.DataSource = DS.Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub Tampildata2(ByVal dgv As DataGridView, ByVal tabel As String)
        Try
            DS = New DataSet
            Query = "select * from " & tabel & " where tanggal='" & Format(Date.Now, "yyyy-MM-dd") & "'"
            DA = New MySqlDataAdapter(Query, CONN)
            DA.Fill(DS)
            dgv.DataSource = DS.Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub Tampildatahistory(ByVal dgv As DataGridView, ByVal tabel As String)
        Try
            DS = New DataSet
            Query = "select * from penjualan_detail where id_transaksi= '" & tabel & "'"
            DA = New MySqlDataAdapter(Query, CONN)
            DA.Fill(DS)
            dgv.DataSource = DS.Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub namamember(ByVal lbl As Label, ByVal str As String)
        Try
            DS2 = New DataSet
            CMD = New MySqlCommand("select * from member where nokartu='" & str & "'", CONN)
            DA = New MySqlDataAdapter(CMD)
            DA.Fill(DS2)
            lbl.Text = DS2.Tables(0).Rows(0).Item(1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub Tampildatamember(ByVal dgv As DataGridView, ByVal tabel As String)
        Try
            DS = New DataSet
            Query = "select * from " & tabel & " where nokartu !=0"
            DA = New MySqlDataAdapter(Query, CONN)
            DA.Fill(DS)
            dgv.DataSource = DS.Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    'All Crud must be here
    Sub Crud(ByVal isi As String, ByVal query As String)
        Try
            CMD = New MySqlCommand(query, CONN)
            CMD.ExecuteNonQuery()
            MsgBox(isi)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub Crud2(ByVal query As String)
        Try
            CMD = New MySqlCommand(query, CONN)
            CMD.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    'Close ALL Sub Forms
    Sub CloseAll()
        For Each frmApproval As Form In MainMenu.MdiChildren
            frmApproval.Close()
        Next
    End Sub

End Module
