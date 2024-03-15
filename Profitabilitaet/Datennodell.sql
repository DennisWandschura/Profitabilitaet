CREATE TABLE abteilung(
	
);
CREATE TABLE nutzer(
	Id int NOT NULL,
	Rolle VARCHAR(8),
	Vorname VARCHAR(255),
	Nachname VARCHAR(255),
	Plz int,
	Ort VARCHAR(255),
	Strasse VARCHAR(255),
	Hausnummer int,
	Geschlecht VARCHAR(16),
	Telefonnummer VARCHAR(64),
	Einstellungsdatum date,
	Loginname VARCHAR(255) NOT NULL,
	Passwort VARCHAR(255) NOT NULL,
	UNIQUE(Id),
	PRIMARY KEY(Id)

);
CREATE TABLE projekt(
	Id int NOT NULL,
	Name VARCHAR(255),
	Description VARCHAR(255),
	Leiter int,
	Auftragswert decimal(65,2),
	AngezahlterBetrag decimal(65,2),
	Beginn date,
	Ende date,
	IstStorniert BOOLEAN,
	UNIQUE(Id),
	PRIMARY KEY(Id)
);