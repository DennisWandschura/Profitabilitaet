using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Profitabilitaet.Common.Models;

public class Abteilung
{
    public int Id { get; }
    public string Bezeichnung { get; }
    public Nutzer Leiter { get; }
    public decimal Etat { get; }
}