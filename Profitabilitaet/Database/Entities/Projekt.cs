using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

    public ICollection<Buchung> Buchungen { get; private set; } =
            new ObservableCollection<Buchung>();

    public decimal Profitabilitaet => 
        Buchungen.Count == 0 ?
        Auftragswert : 
        Auftragswert / Buchungen.Sum(x => x.Anteil);
}
