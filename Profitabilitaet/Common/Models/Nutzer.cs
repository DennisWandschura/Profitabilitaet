using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profitabilitaet.Library;
using Profitabilitaet.Library.Database;

namespace Profitabilitaet.Common.Models;

public record Nutzer(int Id, Rolle Rolle, string Vorname, string Nachname, int Plz, string Ort, string Strasse, int Hausnummer, Geschlecht Geschlecht, string Telefonnummer,
    DateOnly Einstellungsdatum)
{
    public Nutzer(Library.Database.Nutzer nutzer):this(nutzer.Id, nutzer.Rolle.ToEnum<Rolle>(), nutzer.Vorname, nutzer.Nachname,
        nutzer.Plz, nutzer.Ort, nutzer.Strasse, nutzer.Hausnummer, nutzer.Geschlecht.ToEnum<Geschlecht>(), nutzer.Telefonnummer, nutzer.Einstellungsdatum.ToDateOnly())
    {

    }

    public bool HatRolle(Rolle rolle)
    {
        return this.Rolle == rolle;
    }
}
