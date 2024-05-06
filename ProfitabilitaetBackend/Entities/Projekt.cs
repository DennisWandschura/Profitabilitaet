namespace ProfitabilitaetBackend.Entities;

public readonly record struct ProjektId(int Value)
{
    public static ProjektId Empty = default;
}

public class Projekt
{
    public ProjektId Id { get; private set; }
    public string Bezeichnung { get; private set; }
    public Nutzer? Leiter { get; private set; }
    public decimal Auftragswert { get; private set; }
    public decimal AngezahlterBetrag { get; private set; }
    public DateOnly Beginn { get; private set; }
    public DateOnly Ende { get; private set; }
    public bool IstStorniert { get; private set; }
    // public List<BuchungArbeitszeit> Buchungen { get; }
}
