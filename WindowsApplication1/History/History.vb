Imports MySql.Data.MySqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class History
    Dim Bln As Integer = 0
    Dim thn As String = Format(Date.Now, "yyyy")
    Private Sub History_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.MaximizeBox = False
        Connection()
        Tampildata(DataGridView1, "penjualan")
        Bln = 2
        ComboBox1.SelectedIndex = 0
        test1()

    End Sub
    Sub test1()
        With DataGridView1
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "No. Transaksi"
            .Columns(1).HeaderCell.Value = "No. Kartu"
            .Columns(2).HeaderCell.Value = "Tanggal"
            .Columns(3).HeaderCell.Value = "Total Harga"

        End With
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim datestart As String = TextBox1.Text & "/" & Bln & "/1"
        Dim dateend As String = TextBox1.Text & "/" & Bln & "/31"
        'SELECT * from penjualan where START >= '' AND END <= ''
        DS = New DataSet
        If (Bln = 0) Then
            Query = "SELECT * from penjualan where tanggal >= '" & TextBox1.Text & "/01/01' AND tanggal <= '" & TextBox1.Text & "/12/31'"
        Else
            thn = TextBox1.Text
            Query = "SELECT * from penjualan where tanggal >= '" & datestart & "' AND tanggal <= '" & dateend & "'"
        End If
        DA = New MySqlDataAdapter(Query, CONN)
        DA.Fill(DS)
        DataGridView1.DataSource = DS.Tables(0)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.SelectedIndex
            Case 0
                Bln = 0
            Case 1
                Bln = 1
            Case 2
                Bln = 2
            Case 3
                Bln = 3
            Case 4
                Bln = 4
            Case 5
                Bln = 5
            Case 6
                Bln = 6
            Case 7
                Bln = 7
            Case 8
                Bln = 8
            Case 9
                Bln = 9
            Case 10
                Bln = 10
            Case 11
                Bln = 11
        End Select
    End Sub
    Sub refresh()
        Tampildata(DataGridView1, "penjualan")
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        If MsgBox("Yakin Hapus ?", vbInformation + vbYesNo) = vbYes Then
            Crud("Berhasil", "delete from penjualan where no_transaksi ='" & DataGridView1.Item(0, i).Value & "'")
            Crud2("Delete from penjualan_detail where id_transaksi = '" & DataGridView1.Item(0, i).Value & "'")
        End If
        refresh()
    End Sub
    
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Static Generator As System.Random = New System.Random()
            Dim Paragraph As New Paragraph
            Dim PdfFile As New Document(PageSize.A4, 40, 40, 40, 20) ' set pdf page size
            PdfFile.AddTitle("LAPORAN PENJUALAN TUPPERWARE")
            Dim Write As PdfWriter = PdfWriter.GetInstance(PdfFile, New FileStream("./Laporan-" & Bln & "-" & thn & ".pdf", FileMode.Create))
            PdfFile.Open()

            Dim pTitle As New Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
            Dim pdate As New Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
            Dim pTable As New Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)

            Paragraph = New Paragraph(New Chunk("LAPORAN PENJUALAN TUPPERWARE", pTitle))
            Paragraph.Alignment = Element.ALIGN_CENTER
            Paragraph.SpacingAfter = 7.0F
            PdfFile.Add(Paragraph)

            Paragraph = New Paragraph(New Chunk(Date.Now, pdate))
            Paragraph.Alignment = Element.ALIGN_RIGHT
            Paragraph.SpacingAfter = 5.0F
            PdfFile.Add(Paragraph)

            Dim PdfTable As New PdfPTable(DataGridView1.Columns.Count)
            PdfTable.TotalWidth = 500.0F
            PdfTable.LockedWidth = True

            Dim widths(0 To DataGridView1.Columns.Count - 1) As Single
            For i As Integer = 0 To DataGridView1.Columns.Count - 1
                widths(i) = 1.0F
            Next

            PdfTable.SetWidths(widths)
            PdfTable.HorizontalAlignment = 0
            PdfTable.SpacingBefore = 5.0F

            Dim pdfcell As PdfPCell = New PdfPCell

            For i As Integer = 0 To DataGridView1.Columns.Count - 1
                pdfcell = New PdfPCell(New Phrase(New Chunk(DataGridView1.Columns(i).HeaderText, pTable)))
                pdfcell.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                PdfTable.AddCell(pdfcell)
            Next

            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                For j As Integer = 0 To DataGridView1.Columns.Count - 1
                    pdfcell = New PdfPCell(New Phrase(DataGridView1(j, i).Value.ToString(), pTable))
                    PdfTable.HorizontalAlignment = PdfPCell.ALIGN_LEFT
                    PdfTable.AddCell(pdfcell)
                Next
            Next

            PdfFile.Add(PdfTable)
            PdfFile.Close()

            MessageBox.Show("PDF format success exported !", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Error:" & ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim i As Integer = DataGridView1.CurrentRow.Index
        With DataGridView1
            HistoryDetail.Label3.Text = .Item(0, i).Value
            HistoryDetail.nostuff = .Item(0, i).Value
            HistoryDetail.nokartu = .Item(1, i).Value
            HistoryDetail.Label7.Text = .Item(2, i).Value
            HistoryDetail.Label10.Text = .Item(3, i).Value
        End With
        HistoryDetail.Show()
    End Sub
End Class