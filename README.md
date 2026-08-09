# Sistem-Tabungan-Warga

Aplikasi pengelolaan tabungan warga berbasis VB.NET dan MySQL.

## Fitur

- Pencatatan tabungan warga
- Pencarian data warga
- Pengelolaan tabungan berdasarkan bulan
- Perhitungan total tabungan
- Rekap tabungan tahunan
- Print Preview laporan
- Export laporan ke PDF

## Teknologi

- VB.NET
- Windows Forms
- MySQL
- SQL

## Konfigurasi Database

Aplikasi menggunakan MySQL sebagai database.

Sebelum menjalankan aplikasi, sesuaikan connection string pada file `Form1.vb` dengan konfigurasi MySQL pada komputer masing-masing.

Contoh:

```vb
Dim strConn As String = "Server=localhost;Port=3306;Database=db_tabungan;Uid=root;Pwd=;"
