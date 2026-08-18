CREATE DATABASE db_tabungan;
USE db_tabungan;

CREATE TABLE setting (
    ID INT PRIMARY KEY,
    TahunAktif INT NOT NULL,
    BulanAktif VARCHAR(20) NOT NULL
);

INSERT INTO setting (ID, TahunAktif, BulanAktif)
VALUES (1, 2026, 'Agustus');

CREATE TABLE TabunganThn2026 (
    No VARCHAR(10) NOT NULL PRIMARY KEY,
    Nama VARCHAR(100) NOT NULL,
    Januari DECIMAL(12,2) NOT NULL DEFAULT 0,
    Februari DECIMAL(12,2) NOT NULL DEFAULT 0,
    Maret DECIMAL(12,2) NOT NULL DEFAULT 0,
    April DECIMAL(12,2) NOT NULL DEFAULT 0,
    Mei DECIMAL(12,2) NOT NULL DEFAULT 0,
    Juni DECIMAL(12,2) NOT NULL DEFAULT 0,
    Juli DECIMAL(12,2) NOT NULL DEFAULT 0,
    Agustus DECIMAL(12,2) NOT NULL DEFAULT 0,
    September DECIMAL(12,2) NOT NULL DEFAULT 0,
    Oktober DECIMAL(12,2) NOT NULL DEFAULT 0,
    November DECIMAL(12,2) NOT NULL DEFAULT 0,
    Desember DECIMAL(12,2) NOT NULL DEFAULT 0,
    Total DECIMAL(12,2)
        GENERATED ALWAYS AS (
            Januari + Februari + Maret + April +
            Mei + Juni + Juli + Agustus +
            September + Oktober + November + Desember
        ) STORED
);

INSERT INTO TabunganThn2026 (No, Nama) VALUES
('R01', 'Ibu Etik Suprawati'),
('R02', 'Ibu Indah'),
('R03', 'Ibu Yanuarita Riana D'),
('R04', 'Ibu Sutikha'),
('R05', 'Ibu inayah'),
('R06', 'Ibu Nurhasanah'),
('R07', 'Ibu Ria Susanti'),
('R08', 'Ibu Rita Mastuti'),
('R09', 'Ibu RM Manurung'),
('R10', 'Ibu Desnita'),
('R11', 'Ibu Supriyanti'),
('R12', 'Ibu Isyaroh'),
('R13', 'Ibu Dauri'),
('R14', 'Ibu Turini'),
('R15', 'Ibu Vikra'),
('R16', 'Ibu Tiarama Ida'),
('R17', 'Ibu Hellen'),
('R18', 'Ibu HJ. Winerti'),
('R19', 'Ibu Nadia'),
('R20', 'Ibu Ikah');

CREATE TABLE Tanggal2026 (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Bulan VARCHAR(20) NOT NULL,
    Tanggal DATE
);

INSERT INTO Tanggal2026 (Bulan, Tanggal) VALUES
('Januari', NULL),
('Februari', NULL),
('Maret', NULL),
('April', NULL),
('Mei', NULL),
('Juni', NULL),
('Juli', NULL),
('Agustus', NULL),
('September', NULL),
('Oktober', NULL),
('November', NULL),
('Desember', NULL);


