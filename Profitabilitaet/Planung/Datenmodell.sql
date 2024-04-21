CREATE TABLE nutzer(
	Id int NOT NULL AUTO_INCREMENT,
	Rolle VARCHAR(8) NOT NULL,
	Vorname VARCHAR(255) NOT NULL,
	Nachname VARCHAR(255) NOT NULL,
	Plz VARCHAR(8),
	Ort VARCHAR(255),
	Strasse VARCHAR(255),
	Hausnummer int,
	Geschlecht VARCHAR(16),
	Telefonnummer VARCHAR(64),
	Einstellungsdatum date NOT NULL,
	Loginname VARCHAR(255) NOT NULL,
	Passwort VARCHAR(255) NOT NULL,
	UNIQUE(Id),
	PRIMARY KEY(Id)
);
CREATE TABLE abteilung(
	Id int NOT NULL AUTO_INCREMENT,
	Bezeichnung VARCHAR(255) NOT NULL,
	LeiterId int NOT NULL,
	Etat decimal(16,2) NOT NULL,
	UNIQUE(Id),
	PRIMARY KEY(Id),
	FOREIGN KEY(LeiterId) REFERENCES nutzer(Id)
);
CREATE TABLE projekt(
	Id int NOT NULL AUTO_INCREMENT,
	Name VARCHAR(255) NOT NULL,
	Bezeichnung VARCHAR(255),
	LeiterId int,
	Auftragswert decimal(16,2) NOT NULL,
	AngezahlterBetrag decimal(16,2),
	Beginn date NOT NULL,
	Ende date NOT NULL,
	IstStorniert BOOLEAN NOT NULL,
	UNIQUE(Id),
	PRIMARY KEY(Id),
    FOREIGN KEY(LeiterId) REFERENCES nutzer(Id)
);
CREATE TABLE buchung(
	ID int NOT NULL AUTO_INCREMENT,
	Anteil int not null,
	Jahr int not null,
	Woche int not null,
	Mitarbeiter int not null,
	Projekt int not null,
	UNIQUE(Id),
	PRIMARY KEY(Id),
	FOREIGN KEY (Mitarbeiter) REFERENCES nutzer(Id),
	FOREIGN KEY (Projekt) REFERENCES projekt(Id)
);