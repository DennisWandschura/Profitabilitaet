namespace ProfitabilitaetBackend;

public readonly record struct NutzerId(int Value)
{
    public static NutzerId Empty = default;
}

public class Nutzer
{
    public NutzerId Id { get; private set; }
    public Rolle Rolle { get; private set; }
    public string Vorname { get; private set; }
    public string Nachname { get; private set; }
    public string Plz { get; private set; }
    public string Ort { get; private set; }
    public string Strasse { get; private set; }
    public int Hausnummer { get; private set; }
    public Geschlecht Geschlecht { get; private set; }
    public string Telefonnummer { get; private set; }
    public DateTime Einstellungsdatum { get; private set; }
    public string Loginname { get; private set; }
    public string Passwort { get; private set; }
    
    public bool HatRolle(Rolle rolle)
    {
        return this.Rolle == rolle;
    }
}
