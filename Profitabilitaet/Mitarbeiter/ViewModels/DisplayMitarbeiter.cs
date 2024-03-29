using System;
using ProfitabilitaetBackend;

namespace Profitabilitaet.Mitarbeiter.ViewModels;

internal class DisplayMitarbeiter
{
    public string Vorname { get; } = "Peter";
    public string Nachname { get; } = "Ross";
    public string Strasse { get; } = "Teststrasse";
    public int Hausnummer { get; } = 10;
    public int Plz { get; } = 60529;
    public string Ort { get; } = "Frankfurt";
    public Geschlecht Geschlecht { get; } = Geschlecht.Maennlich;
    public string Telefonnummer { get; } = "069/12345";
    public DateOnly Einstellungsdatum { get; } = new DateOnly(2024, 01, 01);
    public Rolle Rolle { get; } = Rolle.NUTZER;
}
