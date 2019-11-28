Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class Historyperdays

    Private Sub Historyperdays_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Tampildata2(DataGridView1, "penjualan")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        If MsgBox("Yakin Hapus ?", vbInformation + vbYesNo) = vbYes Then
            Crud("Berhasil", "delete from penjualan where no_transaksi ='" & DataGridView1.Item(0, i).Value & "'")
            Crud2("Delete from penjualan_detail where id_transaksi = '" & DataGridView1.Item(0, i).Value & "'")
        End If
        Refresh()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Static Generator As System.Random = New System.Random()
            Dim Paragraph As New Paragraph
            Dim PdfFile As New Document(PageSize.A4, 40, 40, 40, 20) ' set pdf page size
            PdfFile.AddTitle("LAPORAN PENJUALAN TUPPERWARE")
            Dim Write As PdfWriter = PdfWriter.GetInstance(PdfFile, New FileStream("./Laporan-" & Format(Date.Now, "dd-MM-yyyy") & ".pdf", FileMode.Create))
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