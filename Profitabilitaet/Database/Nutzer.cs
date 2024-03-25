using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Profitabilitaet.Database;

public readonly record struct NutzerId(int Id)
{
    public static NutzerId Empty = default;
}

public class Nutzer
{
    public NutzerId Id { get; private set; }
    public Rolle Rolle { get; private set; }
    public string Vorname { get; private set; }
    public string Nachname { get; private set; }
    public string Plz { get; private set; }
    public string Ort { get; private set; }
    public string Strasse { get; private set; }
    public int Hausnummer { get; private set; }
    public Geschlecht Geschlecht { get; private set; }
    public string Telefonnummer { get; private set; }
    public DateTime Einstellungsdatum { get; private set; }
    public string Loginname { get; private set; }
    public string Passwort { get; private set; }
    
    public bool HatRolle(Rolle rolle)
    {
        return this.Rolle == rolle;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Nutzer nutzer)
            return false;

        return this.Id == nutzer.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
