using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfitabilitaetBackend.Entities
{
    internal class Buchung
    {
        public int ID { get; set; }
        public int Anteil { get; set; }
        public int Jahr { get; set; }
        public int Woche { get; set; }
        public int MitarbeiterId { get; set; }
        public int ProjektId { get; set; }
    }
}
