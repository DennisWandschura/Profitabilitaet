using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Common.Models
{
    public class Nutzer {
        public int Id { get; }
        public Rolle Rolle { get; }
        public String Vorname { get; }
        public String Nachname { get; }
        public int Plz { get; }
        public String Ort { get; }
        public string Strasse { get; }
        public int Hausnummer { get; }
        public Geschlecht Geschlecht { get; }
        public String Telefonnummer { get; }
        public DateOnly Einstellungsdatum { get; }

        public Boolean HatRolle(Rolle rolle)
        {
            return false;
        }
    }
}
