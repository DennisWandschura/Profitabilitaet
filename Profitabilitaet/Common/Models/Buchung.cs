using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Profitabilitaet.Common.Models;

public class BuchungArbeitszeit
{
    public Database.Nutzer Mitarbeiter { get; }
    public Projekt Projekt { get; }
    public int Anteil { get; }
    public int Woche { get; }
    public int Jahr { get; }
}