--CREATE TABLE Egitimci(
--	EgitimciID int IDENTITY(1,1) NOT NULL PRIMARY KEY,
--	Ad varchar(50) NOT NULL,
--	Soyad varchar(50) NOT NULL,
--	Email varchar(225),
--	Maas smallint CHECK(10000<=Maas AND Maas<=20000)
--)

CREATE TABLE Bolum(
	BolumID smallint NOT NULL PRIMARY KEY,
	BolumBaskaniID int NOT NULL UNIQUE,
	BolumAdi varchar(50) NOT NULL,
	CONSTRAINT fk_BolumBaskaniID FOREIGN KEY (BolumBaskaniID) REFERENCES Egitimci(EgitimciID)
)
