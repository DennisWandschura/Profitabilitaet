using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Database;

[Table("nutzer")]
public class Nutzer
{
    public int Id { get; set; }
    public string Rolle { get; set; }
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public int Plz { get; set; }
    public string Ort { get; set; }
    public string Strasse { get; set; }
    public int Hausnummer { get; set; }
    public string Geschlecht { get; set; }
    public string Telefonnummer { get; set; }
    public DateTime Einstellungsdatum { get; set; }
    public string Loginname { get; set; }
    public string Passwort { get; set; }
}
