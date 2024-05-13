using System;

namespace Profitabilitaet.Database.Entities;

public readonly record struct ProjektId(int Value)
{
    public static ProjektId Empty = default;
}

public class Projekt
{
    public ProjektId Id { get; set; }
    public string Bezeichnung { get; set; }
    public Nutzer? Leiter { get; set; }
    public decimal Auftragswert { get; set; }
    public decimal AngezahlterBetrag { get; set; }
    public DateOnly Beginn { get; set; }
    public DateOnly Ende { get; set; }
    public bool IstStorniert { get; set; }
    // public List<BuchungArbeitszeit> Buchungen { get; }
}
