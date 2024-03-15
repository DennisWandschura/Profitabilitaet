CREATE TABLE abteilung(
	Id int NOT NULL
);
CREATE TABLE nutzer(
	Id int NOT NULL,
	Rolle VARCHAR(8) NOT NULL,
	Vorname VARCHAR(255) NOT NULL,
	Nachname VARCHAR(255) NOT NULL,
	Plz int,
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
CREATE TABLE projekt(
	Id int NOT NULL,
	Name VARCHAR(255) NOT NULL,
	Bezeichnung VARCHAR(255),
	Leiter int,
	Auftragswert decimal(16,2),
	AngezahlterBetrag decimal(16,2),
	Beginn date NOT NULL,
	Ende date NOT NULL,
	IstStorniert BOOLEAN NOT NULL,
	UNIQUE(Id),
	PRIMARY KEY(Id)
);