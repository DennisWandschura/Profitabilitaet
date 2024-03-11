using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Common.Models;

public class MitarbeiterNummer
{
    public int Nummer { get; init; }

    public MitarbeiterNummer(int nummer)
    {
        if (nummer > 9999)
            throw new ArgumentOutOfRangeException(nameof(nummer), "Nummer darf nur maximal 4-stellig sein!");

        Nummer = nummer;
    }
}
