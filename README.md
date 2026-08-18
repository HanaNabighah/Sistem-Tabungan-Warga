# Sistem-Tabungan-Warga

Aplikasi pengelolaan tabungan warga berbasis VB.NET dan MySQL.

## Tujuan
Sistem ini dikembangkan untuk membantu pengelolaan tabungan warga secara terstruktur, mulai dari pencatatan transaksi bulanan hingga rekapitulasi tabungan tahunan.

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

## Yang Saya Pelajari
- Perancangan dan pengelolaan database
- Penggunaan SQL untuk pengolahan data
- Integrasi aplikasi dengan database MySQL
- Perancangan alur sistem berdasarkan kebutuhan pengguna
- Pembuatan laporan berdasarkan data transaksi
  
## Konfigurasi Database

Aplikasi menggunakan MySQL sebagai database.

Sebelum menjalankan aplikasi, sesuaikan connection string pada file `Form1.vb` dengan konfigurasi MySQL pada komputer masing-masing.

Contoh:

```vb
Dim strConn As String = "Server=localhost;Port=3306;Database=db_tabungan;Uid=root;Pwd=;"
