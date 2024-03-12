using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Common.Models
{
    public class Nutzer {
        private Rolle rolle { get; }
        private String Vorname { get; }
        private String Nachname { get; }
        private int Plz { get; }
        private String Ort { get; }
        private int Id { get; }

        private int Strasse
        {
            get => default;
            set
            {
            }
        }

        private int Hausnummer
        {
            get => default;
            set
            {
            }
        }

        private Geschlecht Geschlecht
        {
            get => default;
            set
            {
            }
        }

        private String Telefonnummer
        {
            get => default;
            set
            {
            }
        }

        private DateOnly Einstellungsdatum
        {
            get => default;
            set
            {
            }
        }

        public Boolean HatRolle(Rolle rolle)
        {
            return false;
        }
    }
}
