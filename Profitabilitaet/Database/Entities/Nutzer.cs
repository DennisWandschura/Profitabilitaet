using System;

namespace Profitabilitaet.Database.Entities;

public readonly record struct NutzerId(int Value)
{
    public static NutzerId Empty = default;
}

public class Nutzer
{
    public NutzerId Id { get; private set; }
    public Rolle Rolle { get; set; }
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public string Plz { get; set; }
    public string Ort { get; set; }
    public string Strasse { get; set; }
    public int Hausnummer { get; set; }
    public Geschlecht Geschlecht { get; set; }
    public string Telefonnummer { get; set; }
    public DateTime Einstellungsdatum { get; set; }
    public string Loginname { get; set; }
    public string Passwort { get; set; }

    public bool IsAdmin { get => Rolle == Rolle.ADMIN; }
}
