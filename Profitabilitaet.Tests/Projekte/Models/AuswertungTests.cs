using Profitabilitaet.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Tests.Projekte.Models
{
    public class AuswertungTests
    {
        const string _path = "./auswertung.xlsx";

        private readonly List<Projekt> _projekte =
            [
                new Projekt{ Bezeichnung = "Test 1", Auftragswert = 100.50M, Buchungen = [ new Buchung { Anteil = 2 } ] },
                new Projekt{ Bezeichnung = "Test 2", Auftragswert = 250.0M, Buchungen = [ new Buchung { Anteil = 1 }, new Buchung { Anteil = 1 }] },
            ];

        [Fact]
        public async Task CreateAsync()
        {
            var auswertung = new Profitabilitaet.Projekte.Models.Auswertung(_path, _projekte);
            await auswertung.CreateAsync();
        }
    }
}
