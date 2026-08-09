Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.Drawing.Printing

Public Class Form1
    Dim strConn As String = "Server=localhost;Port=3307;Database=db_tabungan;Uid=root;Pwd=;"
    Dim conn As New MySqlConnection(strConn)

    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim rd As MySqlDataReader
#Region "Koneksi"
    Private Sub BukaKoneksi()

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

    End Sub
    Private Sub TutupKoneksi()

        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If

    End Sub
#End Region

    Private Function GetNamaTabel() As String

        If IsNumeric(cboTahun.Text) Then
            Return "TabunganThn" & cboTahun.Text
        End If

        Return ""

    End Function

    Private Function GetNomorBulan(ByVal bulan As String) As Integer

        Select Case bulan

            Case "Januari"
                Return 1

            Case "Februari"
                Return 2

            Case "Maret"
                Return 3

            Case "April"
                Return 4

            Case "Mei"
                Return 5

            Case "Juni"
                Return 6

            Case "Juli"
                Return 7

            Case "Agustus"
                Return 8

            Case "September"
                Return 9

            Case "Oktober"
                Return 10

            Case "November"
                Return 11

            Case "Desember"
                Return 12

        End Select

        Return 0

    End Function
    Private Function GetTotalSQL() As String

        Select Case cboBulanAktif.Text

            Case "Januari"
                Return "COALESCE(Januari,0)"

            Case "Februari"
                Return "COALESCE(Januari,0)+COALESCE(Februari,0)"

            Case "Maret"
                Return "COALESCE(Januari,0)+COALESCE(Februari,0)+COALESCE(Maret,0)"

            Case "April"
                Return "COALESCE(Januari,0)+COALESCE(Februari,0)+COALESCE(Maret,0)+COALESCE(April,0)"

            Case "Mei"
                Return "COALESCE(Januari,0)+COALESCE(Februari,0)+COALESCE(Maret,0)+COALESCE(April,0)+COALESCE(Mei,0)"

            Case "Juni"
                Return "COALESCE(Januari,0)+COALESCE(Februari,0)+COALESCE(Maret,0)+COALESCE(April,0)+COALESCE(Mei,0)+COALESCE(Juni,0)"

            Case "Juli"
                Return "COALESCE(Januari,0)+COALESCE(Februari,0)+COALESCE(Maret,0)+COALESCE(April,0)+COALESCE(Mei,0)+COALESCE(Juni,0)+COALESCE(Juli,0)"

            Case "Agustus"
                Return "COALESCE(Januari,0)+COALESCE(Februari,0)+COALESCE(Maret,0)+COALESCE(April,0)+COALESCE(Mei,0)+COALESCE(Juni,0)+COALESCE(Juli,0)+COALESCE(Agustus,0)"

            Case "September"
                Return "COALESCE(Januari,0)+COALESCE(Februari,0)+COALESCE(Maret,0)+COALESCE(April,0)+COALESCE(Mei,0)+COALESCE(Juni,0)+COALESCE(Juli,0)+COALESCE(Agustus,0)+COALESCE(September,0)"

            Case "Oktober"
                Return "COALESCE(Januari,0)+COALESCE(Februari,0)+COALESCE(Maret,0)+COALESCE(April,0)+COALESCE(Mei,0)+COALESCE(Juni,0)+COALESCE(Juli,0)+COALESCE(Agustus,0)+COALESCE(September,0)+COALESCE(Oktober,0)"

            Case "November"
                Return "COALESCE(Januari,0)+COALESCE(Februari,0)+COALESCE(Maret,0)+COALESCE(April,0)+COALESCE(Mei,0)+COALESCE(Juni,0)+COALESCE(Juli,0)+COALESCE(Agustus,0)+COALESCE(September,0)+COALESCE(Oktober,0)+COALESCE(November,0)"

            Case "Desember"
                Return "COALESCE(Januari,0)+COALESCE(Februari,0)+COALESCE(Maret,0)+COALESCE(April,0)+COALESCE(Mei,0)+COALESCE(Juni,0)+COALESCE(Juli,0)+COALESCE(Agustus,0)+COALESCE(September,0)+COALESCE(Oktober,0)+COALESCE(November,0)+COALESCE(Desember,0)"

        End Select

        Return ""

    End Function

    Private Sub LoadSetting()

        Try

            BukaKoneksi()

            Dim sql As String =
            "SELECT TahunAktif, BulanAktif " &
            "FROM setting WHERE ID=1"

            cmd = New MySqlCommand(sql, conn)

            rd = cmd.ExecuteReader()

            If rd.Read() Then

                cboTahun.Text = rd("TahunAktif").ToString()
                cboBulanAktif.Text = rd("BulanAktif").ToString()

            End If

            rd.Close()
            TutupKoneksi()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub
    Private Function TahunBolehEdit() As Boolean

        If cboTahun.Text = "" Then
            MessageBox.Show("Pilih tahun terlebih dahulu.")
            Return False
        End If

        If cboTahun.Text <> AmbilTahunAktif() Then

            MessageBox.Show(
            "Tahun " & cboTahun.Text &
            " merupakan arsip dan tidak dapat diubah.")

            Return False

        End If

        Return True

    End Function
    Private Function AmbilTahunAktif() As String

        Dim tahun As String = ""

        Try

            BukaKoneksi()

            Dim sql As String =
            "SELECT TahunAktif FROM setting WHERE ID=1"

            cmd = New MySqlCommand(sql, conn)

            Dim hasil = cmd.ExecuteScalar()

            If hasil IsNot Nothing Then
                tahun = hasil.ToString()
            End If

            TutupKoneksi()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

        Return tahun

    End Function

    Private Function BulanBolehEdit() As Boolean

        If cboBulan.Text = "" Then

            MessageBox.Show("Pilih bulan terlebih dahulu.")

            Return False

        End If


        If cboBulan.Text <> cboBulanAktif.Text Then

            MessageBox.Show(
            "Bulan " & cboBulan.Text &
            " sudah tidak aktif dan hanya dapat dilihat.")

            Return False

        End If


        Return True

    End Function


    Private Sub SimpanData()

        If TahunBolehEdit() = False Then Exit Sub

        If BulanBolehEdit() = False Then Exit Sub


        If txtNoRumah.Text = "" Then

            MessageBox.Show("No rumah belum diisi.")

            Exit Sub

        End If


        If txtJumlah.Text = "" Then

            MessageBox.Show("Jumlah belum diisi.")

            Exit Sub

        End If


        Dim jumlah As Decimal

        If Decimal.TryParse(txtJumlah.Text, jumlah) = False Then

            MessageBox.Show("Jumlah harus berupa angka.")

            Exit Sub

        End If


        Try

            BukaKoneksi()

            Dim sql As String =
            "UPDATE " &
            GetNamaTabel() &
            " SET " &
            cboBulan.Text &
            "=@Jumlah, " &
            "Total=" &
            GetTotalSQL() &
            " WHERE No=@Rumah"

            cmd = New MySqlCommand(sql, conn)

            cmd.Parameters.AddWithValue("@Jumlah", jumlah)
            cmd.Parameters.AddWithValue("@Rumah", txtNoRumah.Text)

            cmd.ExecuteNonQuery()

            TutupKoneksi()


            'Simpan tanggal Aktif
            SimpanTanggalAktif()


            MessageBox.Show("Data berhasil disimpan.")

            LoadData()


        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(ex.Message)

        End Try

    End Sub


    Private Sub CariNama()

        Try

            BukaKoneksi()

            cmd = New MySqlCommand(
            "SELECT Nama FROM " &
            GetNamaTabel() &
            " WHERE No=@Rumah", conn)

            cmd.Parameters.AddWithValue("@Rumah", txtNoRumah.Text)

            rd = cmd.ExecuteReader()

            If rd.Read() Then

                txtNama.Text = rd("Nama").ToString()

            Else

                txtNama.Clear()

            End If

            rd.Close()

            TutupKoneksi()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub
    Private Sub CariRumah()

        Try

            BukaKoneksi()

            cmd = New MySqlCommand(
            "SELECT No FROM " &
            GetNamaTabel() &
            " WHERE Nama=@Nama", conn)

            cmd.Parameters.AddWithValue("@Nama", txtNama.Text)

            rd = cmd.ExecuteReader()

            If rd.Read() Then

                txtNoRumah.Text = rd("NoRumah").ToString()

            Else

                txtNoRumah.Clear()

            End If

            rd.Close()

            TutupKoneksi()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub
    Private Sub EditData()

        If TahunBolehEdit() = False Then Exit Sub

        If BulanBolehEdit() = False Then Exit Sub


        If txtNoRumah.Text = "" Then

            MessageBox.Show("No rumah belum diisi.")

            Exit Sub

        End If


        If txtJumlah.Text = "" Then

            MessageBox.Show("Jumlah belum diisi.")

            Exit Sub

        End If

        Dim jumlah As Decimal

        If Decimal.TryParse(txtJumlah.Text, jumlah) = False Then

            MessageBox.Show("Jumlah harus berupa angka.")

            Exit Sub

        End If

        Try

            BukaKoneksi()

            Dim sql As String =
            "UPDATE " &
            GetNamaTabel() &
            " SET " &
            cboBulan.Text &
            "=@Jumlah, " &
            "Total=" &
            GetTotalSQL() &
            " WHERE No=@Rumah"

            cmd = New MySqlCommand(sql, conn)

            cmd.Parameters.AddWithValue("@Jumlah", jumlah)
            cmd.Parameters.AddWithValue("@Rumah", txtNoRumah.Text)

            cmd.ExecuteNonQuery()

            TutupKoneksi()


            MessageBox.Show("Data berhasil diedit.")

            LoadData()


        Catch ex As Exception

            TutupKoneksi()

            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub SimpanTanggalAktif()

        Dim namaTabelTanggal As String =
        "Tanggal" & cboTahun.Text

        Try

            BukaKoneksi()

            Dim sql As String =
            "UPDATE " & namaTabelTanggal &
            " SET Tanggal=@Tanggal " &
            " WHERE Bulan=@Bulan"

            cmd = New MySqlCommand(sql, conn)

            cmd.Parameters.AddWithValue(
            "@Tanggal",
            dtpTanggal.Value.Date)

            cmd.Parameters.AddWithValue(
            "@Bulan",
            cboBulanAktif.Text)

            cmd.ExecuteNonQuery()

            TutupKoneksi()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub TampilNominalBulan()

        If txtNoRumah.Text = "" Then Exit Sub

        If cboBulan.Text = "" Then Exit Sub


        Try

            BukaKoneksi()

            Dim sql As String =
            "SELECT " & cboBulan.Text &
            " FROM " &
            GetNamaTabel() &
            " WHERE No=@Rumah"

            cmd = New MySqlCommand(sql, conn)

            cmd.Parameters.AddWithValue(
            "@Rumah",
            txtNoRumah.Text)

            Dim hasil = cmd.ExecuteScalar()

            If hasil IsNot Nothing AndAlso
           Not IsDBNull(hasil) Then

                txtJumlah.Text =
                Convert.ToDecimal(hasil).ToString("N0")

            Else

                txtJumlah.Clear()

            End If

            TutupKoneksi()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub
    Private Sub TampilkanTotal()

        If txtNoRumah.Text = "" Then

            MessageBox.Show("Masukkan No Rumah terlebih dahulu.")

            Exit Sub

        End If

        Try

            BukaKoneksi()

            Dim sql As String =
            "SELECT Nama, Total FROM " &
            GetNamaTabel() &
            " WHERE No=@Rumah"

            cmd = New MySqlCommand(sql, conn)

            cmd.Parameters.AddWithValue(
            "@Rumah",
            txtNoRumah.Text)

            rd = cmd.ExecuteReader()

            If rd.Read() Then

                Dim nama As String =
                rd("Nama").ToString()

                Dim total As Decimal =
                Convert.ToDecimal(rd("Total"))

                MessageBox.Show(
                "Nama : " & nama & vbCrLf &
                "No Rumah : " & txtNoRumah.Text & vbCrLf &
                "Tahun : " & cboTahun.Text & vbCrLf &
                "Sampai Bulan : " & cboBulanAktif.Text & vbCrLf &
                "Total Tabungan : Rp " &
                total.ToString("N0"),
                "Total Tabungan")

            Else

                MessageBox.Show("Data tidak ditemukan.")

            End If

            rd.Close()

            TutupKoneksi()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub
    Private Sub LoadTanggalAktif()

        Dim namaTabelTanggal As String =
        "Tanggal" & cboTahun.Text

        Try

            BukaKoneksi()

            Dim sql As String =
            "SELECT Tanggal FROM " &
            namaTabelTanggal &
            " WHERE Bulan=@Bulan"

            cmd = New MySqlCommand(sql, conn)

            cmd.Parameters.AddWithValue(
            "@Bulan",
            cboBulan.Text)

            Dim hasil = cmd.ExecuteScalar()

            If hasil IsNot Nothing AndAlso
           Not IsDBNull(hasil) Then

                dtpTanggal.Value =
                Convert.ToDateTime(hasil)

            End If

            TutupKoneksi()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Function GetTanggalBulan(ByVal bulan As String) As String

        Dim tanggal As String = ""

        Dim namaTabelTanggal As String =
        "Tanggal" & cboTahun.Text

        Try

            BukaKoneksi()

            Dim sql As String =
            "SELECT Tanggal FROM " &
            namaTabelTanggal &
            " WHERE Bulan=@Bulan"

            cmd = New MySqlCommand(sql, conn)

            cmd.Parameters.AddWithValue(
            "@Bulan",
            bulan)

            Dim hasil = cmd.ExecuteScalar()

            If hasil IsNot Nothing AndAlso
           Not IsDBNull(hasil) Then

                tanggal =
                Convert.ToDateTime(hasil).
                ToString("dd-MM-yyyy")

            End If

            TutupKoneksi()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

        Return tanggal

    End Function
    Private Sub AturHeaderTanggal()

        dgvTabungan.Columns("Januari").HeaderText =
        "Januari" & vbCrLf &
        GetTanggalBulan("Januari")

        dgvTabungan.Columns("Februari").HeaderText =
        "Februari" & vbCrLf &
        GetTanggalBulan("Februari")

        dgvTabungan.Columns("Maret").HeaderText =
        "Maret" & vbCrLf &
        GetTanggalBulan("Maret")

        dgvTabungan.Columns("April").HeaderText =
        "April" & vbCrLf &
        GetTanggalBulan("April")

        dgvTabungan.Columns("Mei").HeaderText =
        "Mei" & vbCrLf &
        GetTanggalBulan("Mei")

        dgvTabungan.Columns("Juni").HeaderText =
        "Juni" & vbCrLf &
        GetTanggalBulan("Juni")

        dgvTabungan.Columns("Juli").HeaderText =
        "Juli" & vbCrLf &
        GetTanggalBulan("Juli")

        dgvTabungan.Columns("Agustus").HeaderText =
        "Agustus" & vbCrLf &
        GetTanggalBulan("Agustus")

        dgvTabungan.Columns("September").HeaderText =
        "September" & vbCrLf &
        GetTanggalBulan("September")

        dgvTabungan.Columns("Oktober").HeaderText =
        "Oktober" & vbCrLf &
        GetTanggalBulan("Oktober")

        dgvTabungan.Columns("November").HeaderText =
        "November" & vbCrLf &
        GetTanggalBulan("November")

        dgvTabungan.Columns("Desember").HeaderText =
        "Desember" & vbCrLf &
        GetTanggalBulan("Desember")


    End Sub
    Private Sub ResetForm()

        txtNoRumah.Clear()
        txtNama.Clear()
        txtJumlah.Clear()

        cboBulan.SelectedIndex = -1

    End Sub

    Private Sub LoadData()

        Try

            BukaKoneksi()

            Dim sql As String =
                "SELECT * FROM " & GetNamaTabel()

            Dim da As New MySqlDataAdapter(sql, conn)

            Dim dt As New DataTable

            da.Fill(dt)

            dgvTabungan.DataSource = dt

            TutupKoneksi()

            AturHeaderTanggal()
        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub
    Private Sub LoadBulan()

        cboBulan.Items.Clear()

        cboBulan.Items.Add("Januari")
        cboBulan.Items.Add("Februari")
        cboBulan.Items.Add("Maret")
        cboBulan.Items.Add("April")
        cboBulan.Items.Add("Mei")
        cboBulan.Items.Add("Juni")
        cboBulan.Items.Add("Juli")
        cboBulan.Items.Add("Agustus")
        cboBulan.Items.Add("September")
        cboBulan.Items.Add("Oktober")
        cboBulan.Items.Add("November")
        cboBulan.Items.Add("Desember")

    End Sub
    Private Sub LoadBulanAktif()

        cboBulanAktif.Items.Clear()

        cboBulanAktif.Items.Add("Januari")
        cboBulanAktif.Items.Add("Februari")
        cboBulanAktif.Items.Add("Maret")
        cboBulanAktif.Items.Add("April")
        cboBulanAktif.Items.Add("Mei")
        cboBulanAktif.Items.Add("Juni")
        cboBulanAktif.Items.Add("Juli")
        cboBulanAktif.Items.Add("Agustus")
        cboBulanAktif.Items.Add("September")
        cboBulanAktif.Items.Add("Oktober")
        cboBulanAktif.Items.Add("November")
        cboBulanAktif.Items.Add("Desember")

    End Sub
    Private Sub LoadTahun()

        cboTahun.Items.Clear()

        cboTahun.Items.Add("2026")

    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadBulan()

        LoadTahun()

        LoadSetting()

        LoadBulanAktif()

        LoadTanggalAktif()

        LoadData()

    End Sub

    Private Sub txtNoRumah_Leave(sender As Object, e As EventArgs) Handles txtNoRumah.Leave

        CariNama()

    End Sub

    Private Sub txtNama_Leave(sender As Object, e As EventArgs) Handles txtNama.Leave

        CariRumah()

    End Sub
    Private Sub cboBulan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBulan.SelectedIndexChanged

        TampilNominalBulan()
        LoadTanggalAktif()
    End Sub
    Private Sub cboTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTahun.SelectedIndexChanged

        If cboTahun.Text = "" Then Exit Sub

        LoadData()

        If cboBulan.Text <> "" Then
            LoadTanggalAktif()
        End If

    End Sub
    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click

        SimpanData()

    End Sub
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        EditData()

    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click

        ResetForm()

    End Sub
    Private Sub btnTotal_Click(sender As Object, e As EventArgs) Handles btnTotal.Click

        TampilkanTotal()

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If dgvTabungan.Rows.Count = 0 Then
                MessageBox.Show("Tidak ada data untuk dicetak.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Cek apakah Microsoft Print to PDF tersedia
            Dim pdfPrinter As String = ""
            For Each printer As String In PrinterSettings.InstalledPrinters
                If printer.Contains("Microsoft Print to PDF") Then
                    pdfPrinter = printer
                    Exit For
                End If
            Next

            If pdfPrinter = "" Then
                MessageBox.Show("Microsoft Print to PDF tidak ditemukan. Pastikan fitur ini sudah diinstall di Windows.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Buat PrintDocument dengan setting PDF
            Dim printDoc As New PrintDocument()
            printDoc.DefaultPageSettings.Landscape = True
            printDoc.PrinterSettings.PrinterName = pdfPrinter

            AddHandler printDoc.PrintPage, AddressOf PrintDataGrid

            ' Tampilkan PrintPreview
            Dim preview As New PrintPreviewDialog()
            preview.Document = printDoc
            preview.WindowState = FormWindowState.Maximized

            ' Set judul preview
            preview.Text = "Preview - Laporan Tabungan " & cboTahun.Text

            ' Tampilkan preview
            preview.ShowDialog()

            ' Setelah preview ditutup, cek apakah user mengklik Print
            ' Jika user klik Print di preview, otomatis akan save ke PDF

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintDataGrid(sender As Object, e As PrintPageEventArgs)
        Dim g As Graphics = e.Graphics

        ' Ambil ukuran kertas
        Dim pageWidth As Integer = e.PageBounds.Width
        Dim pageHeight As Integer = e.PageBounds.Height

        ' Font
        Dim titleFont As New Font("Arial", 14, FontStyle.Bold)
        Dim headerFont As New Font("Arial", 8, FontStyle.Bold)
        Dim dataFont As New Font("Arial", 8, FontStyle.Regular)
        Dim footerFont As New Font("Arial", 8, FontStyle.Regular)

        ' Margin
        Dim marginKiri As Integer = 30
        Dim marginAtas As Integer = 40
        Dim marginBawah As Integer = 50

        Dim y As Integer = marginAtas
        Dim x As Integer = marginKiri
        Dim lebarTersedia As Integer = pageWidth - (marginKiri * 2)

        ' === JUDUL ===
        g.DrawString("LAPORAN TABUNGAN WARGA", titleFont, Brushes.Black, x, y)
        y += 35

        g.DrawString("RT 006 / RW 040 Blok J12", New Font("Arial", 12, FontStyle.Regular), Brushes.Black, x, y)
        y += 30

        g.DrawString("Tahun " & cboTahun.Text, New Font("Arial", 12, FontStyle.Bold), Brushes.Black, x, y)
        y += 40

        ' === ATUR LEBAR KOLOM ===
        Dim colWidths As New Dictionary(Of String, Integer)()
        Dim totalLebar As Integer = 0

        For Each col As DataGridViewColumn In dgvTabungan.Columns
            If col.Name = "No" Then
                colWidths(col.Name) = 100
            ElseIf col.Name = "Nama" Then
                colWidths(col.Name) = 200
            ElseIf col.Name = "Total" Then
                colWidths(col.Name) = 140
            Else
                ' Kolom bulan
                colWidths(col.Name) = 100
            End If
            totalLebar += colWidths(col.Name)
        Next

        ' Jika total lebar melebihi lebar tersedia, sesuaikan
        If totalLebar > lebarTersedia Then
            Dim faktor As Double = lebarTersedia / totalLebar
            For Each key As String In colWidths.Keys.ToList()
                colWidths(key) = CInt(colWidths(key) * faktor)
            Next
        End If

        ' === HEADER TABEL ===
        Dim currentX As Integer = x

        For i As Integer = 0 To dgvTabungan.Columns.Count - 1
            Dim colName As String = dgvTabungan.Columns(i).Name
            Dim width As Integer = colWidths(colName)

            Dim rect As New Rectangle(currentX, y, width, 25)
            g.FillRectangle(Brushes.LightGray, rect)
            g.DrawRectangle(Pens.Black, rect)

            Dim headerText As String = dgvTabungan.Columns(i).HeaderText
            If headerText.Contains(vbCrLf) Then
                headerText = headerText.Split(vbCrLf)(0)
            End If

            Dim format As New StringFormat()
            format.Alignment = StringAlignment.Center
            format.LineAlignment = StringAlignment.Center
            g.DrawString(headerText, headerFont, Brushes.Black, rect, format)

            currentX += width
        Next

        y += 25

        ' === DATA ===
        Dim tinggiBaris As Integer = 20

        For Each row As DataGridViewRow In dgvTabungan.Rows
            If row.IsNewRow Then Continue For

            ' Cek apakah masih muat
            Dim tinggiDibutuhkan As Integer = y + tinggiBaris + 80 + marginBawah
            If tinggiDibutuhkan > pageHeight Then
                TambahFooter(g, x, y, pageWidth)
                e.HasMorePages = True
                Return
            End If

            currentX = x

            For i As Integer = 0 To dgvTabungan.Columns.Count - 1
                Dim colName As String = dgvTabungan.Columns(i).Name
                Dim width As Integer = colWidths(colName)

                Dim rect As New Rectangle(currentX, y, width, tinggiBaris)
                g.DrawRectangle(Pens.Black, rect)

                Dim value As String = ""
                If row.Cells(i).Value IsNot Nothing Then
                    If IsNumeric(row.Cells(i).Value) Then
                        Dim decValue As Decimal = Convert.ToDecimal(row.Cells(i).Value)
                        If decValue = 0 Then
                            value = "-"
                        Else
                            value = decValue.ToString("N0")
                        End If
                    Else
                        value = row.Cells(i).Value.ToString()
                    End If
                End If

                Dim format As New StringFormat()
                format.Alignment = StringAlignment.Center
                format.LineAlignment = StringAlignment.Center
                g.DrawString(value, dataFont, Brushes.Black, rect, format)

                currentX += width
            Next

            y += tinggiBaris
        Next

        ' === FOOTER ===
        TambahFooter(g, x, y, pageWidth)

        e.HasMorePages = False
    End Sub

    Private Sub TambahFooter(g As Graphics, x As Integer, y As Integer, pageWidth As Integer)
        Dim footerFont As New Font("Arial", 8, FontStyle.Regular)

        y += 20
        g.DrawString("Dicetak: " & DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss"), footerFont, Brushes.Black, x, y)

        ' TANDA TANGAN (di kanan)
        y += 40
        Dim signatureX As Integer = pageWidth - 200
        g.DrawString("Mengetahui,", footerFont, Brushes.Black, signatureX, y)

        y += 40
        g.DrawString("(______________)", New Font("Arial", 10, FontStyle.Bold), Brushes.Black, signatureX, y)

        y += 20
        g.DrawString("Ketua RT 006", footerFont, Brushes.Black, signatureX, y)
    End Sub
End Class
