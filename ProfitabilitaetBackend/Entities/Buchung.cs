using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfitabilitaetBackend.Entities
{
    public readonly record struct BuchungId(int Value);

    public class Buchung
    {
        public BuchungId Id { get; set; }
        public int Anteil { get; set; }
        public int Jahr { get; set; }
        public int Woche { get; set; }
        public Nutzer Mitarbeiter { get; set; }
        public Projekt Projekt { get; set; }
    }
}
