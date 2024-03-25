using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Database;

public readonly record struct AbteilungsId(int Id)
{
    public static AbteilungsId Empty = default;
}

public class Abteilung
{
    public AbteilungsId Id { get; private set; }
    public string Bezeichnung { get; private set; }
    public Nutzer Leiter { get; private set; }
    public decimal Etat { get; private set; }
    
    public override bool Equals(object? obj)
    {
        if (obj is not Abteilung abteilung)
            return false;

        return this.Id == abteilung.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
