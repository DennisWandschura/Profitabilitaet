using System;
using System.Collections.Generic;
using ProfitabilitaetBackend;

namespace Profitabilitaet.Common.Models;
public class Projekt
{
    public int Id { get; }
    public string Name { get; }
    public string Bezeichnung { get; }
    public Nutzer? Leiter { get; }
    public decimal Auftragswert { get; }
    public decimal AngezahlterBetrag { get; }
    public DateOnly Beginn { get; }
    public DateOnly Ende { get; }
    public bool IstStorniert { get; private set; }
    public List<BuchungArbeitszeit> Buchungen { get; }

    public bool Buchen(Nutzer mitarbeiter, int woche, int jahr, int stunden)
    {
        return false;
    }

    public bool Stornieren()
    {
        if (IstStorniert)
        {
            return false;
        }
        else
        {
            IstStorniert = true;
            return true;
        }
    }
}
