Public Class HistoryDetail
    Public nokartu As String = ""
    Public nostuff As String = ""
    Private Sub HistoryDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        namamember(Label4, nokartu)
        Tampildatahistory(DataGridView1, nostuff)
        test1()
    End Sub

    Sub test1()
        Try
            With DataGridView1
                .RowHeadersVisible = False
                .Columns(0).HeaderCell.Value = "ID Detail"
                .Columns(1).HeaderCell.Value = "Kode Barang"
                .Columns(2).HeaderCell.Value = "Qty"
                .Columns(3).HeaderCell.Value = "Total Harga"
                .Columns(4).HeaderCell.Value = "ID Transaksi"
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try   
    End Sub
End Class