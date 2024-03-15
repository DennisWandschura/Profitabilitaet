using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Library.Database;

public class Projekt
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Bezeichnung { get; set; }
    public Nutzer? Leiter { get; set; }
    public decimal? Auftragswert { get; set; }
    public decimal? AngezahlterBetrag { get; set; }
    public DateOnly Beginn { get; set; }
    public DateOnly Ende { get; set; }
    public bool IstStorniert { get; set; }
   // public List<BuchungArbeitszeit> Buchungen { get; }
}
