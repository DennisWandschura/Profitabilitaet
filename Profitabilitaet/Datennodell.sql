CREATE TABLE abteilung(
	
);
CREATE TABLE nutzer(
	Id int,
	Rolle string,
	Vorname string,
	Nachname string,
	Plz int,
	Ort string,
	Strasse string,
	Hausnummer int,
	Geschlecht string,
	Telefonnummer string,
	Einstellungsdatum date
);
CREATE TABLE projekt(
	Id int,
	Name string,
	Description string,
	Leiter id,
	Auftragswert decimal(65,2),
	AngezahlterBetrag decimal(65,2),
	Beginn date,
	Ende date,
	IstStorniert boolean
);