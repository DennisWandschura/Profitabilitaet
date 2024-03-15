using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Common.Models;

public class Nutzer {
    public int Id { get; set; }
    public Library.Database.Rolle Rolle { get; set; }
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public int Plz { get; }
    public string Ort { get; }
    public string Strasse { get; }
    public int Hausnummer { get; }
    public Library.Database.Geschlecht Geschlecht { get; }
    public string Telefonnummer { get; }
    public DateOnly Einstellungsdatum { get; }

    public bool HatRolle(Library.Database.Rolle rolle)
    {
        return false;
    }
}
