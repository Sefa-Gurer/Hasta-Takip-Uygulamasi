--CREATE TABLE Poliklinikler(
--	PoliklinikID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
--	Poliklinik_adi varchar(50) NOT NULL
--)

--CREATE TABLE Unvanlar(
--	UnvanID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
--	Unvan_adi varchar(50) NOT NULL
--)

--CREATE TABLE Personeller(
--	PersonelID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
--	Personel_tc bigint NOT NULL UNIQUE CHECK(10000000000<=Personel_tc AND Personel_tc<=99999999999),
--	Personel_Ad varchar(50) NOT NULL,
--	Personel_Soyad varchar(50) NOT NULL,
--	Personel_Email varchar(225),
--	Personel_Tel bigint NOT NULL CHECK(5000000000<=Personel_Tel AND Personel_Tel<=6000000000),
--	Unvan int NOT NULL,
--	Poliklinik int NOT NULL,
--	CONSTRAINT fk_Unvan FOREIGN KEY (Unvan) REFERENCES Unvanlar(UnvanID),
--	CONSTRAINT fk_Poliklinik FOREIGN KEY (Poliklinik) REFERENCES Poliklinikler(PoliklinikID)
--)

--CREATE TABLE Sisteme_Giris_Bilgileri(
--	Personel int NOT NULL,
--	Sifre nvarchar(50) UNIQUE,
--	CONSTRAINT fk_Personel FOREIGN KEY (Personel) REFERENCES Personeller(PersonelID)
--)

--CREATE TABLE Sisteme_Giris_Zamani(
--	Personel int NOT NULL,
--	Giris_Zamani DateTime2,
--	CONSTRAINT fk_Giris FOREIGN KEY (Personel) REFERENCES Personeller(PersonelID)
--)
