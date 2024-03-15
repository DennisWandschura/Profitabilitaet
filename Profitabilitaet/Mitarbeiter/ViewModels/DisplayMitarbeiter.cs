using Profitabilitaet.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Mitarbeiter.ViewModels;

internal class DisplayMitarbeiter
{
    public string Vorname { get; } = "Peter";
    public string Nachname { get; } = "Ross";
    public string Strasse { get; } = "Teststrasse";
    public int Hausnummer { get; } = 10;
    public int Plz { get; } = 60529;
    public string Ort { get; } = "Frankfurt";
    public Library.Database.Geschlecht Geschlecht { get; } = Library.Database.Geschlecht.Maennlich;
    public string Telefonnummer { get; } = "069/12345";
    public DateOnly Einstellungsdatum { get; } = new DateOnly(2024, 01, 01);
    public Library.Database.Rolle Rolle { get; } = Library.Database.Rolle.NUTZER;
}
