namespace Profitabilitaet.Database.Entities;

public readonly record struct AbteilungsId(int Value)
{
    public static AbteilungsId Empty = default;
}

public class Abteilung
{
    public AbteilungsId Id { get; private set; }
    public string Bezeichnung { get; private set; }
    public Nutzer Leiter { get; private set; }
    public decimal Etat { get; private set; }
}
