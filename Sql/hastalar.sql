﻿--CREATE TABLE Hastalar(
--	Hasta_tc bigint NOT NULL PRIMARY KEY CHECK(10000000000<=Hasta_tc AND Hasta_tc<=99999999999),
--	Hasta_Ad varchar(50) NOT NULL,
--	Hasta_Soyad varchar(50) NOT NULL,
--	Hasta_Email varchar(225),
--	Hasta_Tel bigint NOT NULL CHECK(5000000000<=Hasta_Tel AND Hasta_Tel<=6000000000),
--	Hasta_Dogum_Tarihi date,
--	Kan_Grubu VARCHAR(5) NOT NULL CHECK (Kan_Grubu IN ('A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', '0+', '0-'))
--)

--CREATE TABLE Tahlil_Aralikleri(
--	Sonuc_Tipi nvarchar(50)	NOT NULL PRIMARY KEY,
--	Sonuc_Max float NOT NULL,
--	Sonuc_Min float NOT NULL
--)

--CREATE TABLE Tahliller(
--	tc bigint NOT NULL,
--	Sonuc_Tipi nvarchar(50) NOT NULL CHECK (Sonuc_Tipi IN ('DEM�R', 'GLUKOZ','�NS�L�N','KOLESTEROL','TSH','�RE')),
--	Sonuc int,
--	Tarih datetime NOT NULL,
--	CONSTRAINT fk_tc FOREIGN KEY (tc) REFERENCES Hastalar(Hasta_tc)
--)

--CREATE TABLE Radyolojik_Goruntuler(
--	tc bigint NOT NULL,
--	Sonuc_Tipi nvarchar(50) NOT NULL CHECK (Sonuc_Tipi IN ('MR', 'R�NTGEN')),
--	Sonuc image,
--	Tarih datetime NOT NULL,
--	CONSTRAINT fk_tc2 FOREIGN KEY (tc) REFERENCES Hastalar(Hasta_tc)
--) 