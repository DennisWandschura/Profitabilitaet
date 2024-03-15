using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Library.Database;

internal interface IConnection
{
    public Task<IEnumerable<Nutzer>> GetNutzer();
    public Task<Nutzer> GetNutzer(int id);

    public Task<IEnumerable<Projekt>> GetProjekte();
    public Task<Projekt> GetProjekt(int id);
}
