using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Database;

[Table("abteilung")]
public class Abteilung
{
    public int Id { get; set; }
    public string Bezeichnung { get; set; }
    public Nutzer Leiter { get; set; }
    public int LeiterId { get; set; }
    public decimal Etat { get; set; }
}
