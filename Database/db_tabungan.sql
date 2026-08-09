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
('R01', 'Ibu eva'),
('R02', 'Ibu Lina'),
('R03', 'Ibu Yuni'),
('R04', 'Ibu Ika'),
('R05', 'Ibu Ina'),
('R06', 'Ibu Hasna'),
('R07', 'Ibu Ria '),
('R08', 'Ibu Rita '),
('R09', 'Ibu Aliya'),
('R10', 'Ibu Dewi'),
('R11', 'Ibu Anita'),
('R12', 'Ibu Ismi'),
('R13', 'Ibu Alina'),
('R14', 'Ibu Erika');

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


