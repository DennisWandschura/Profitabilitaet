using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Library.Database;

internal interface IConnection
{
    public Task<IReadOnlyList<Nutzer>> GetNutzer(CancellationToken cancellationToken);
    public Task<Nutzer?> GetNutzer(int id, CancellationToken cancellationToken);

    public Task<IReadOnlyList<Projekt>> GetProjekte(CancellationToken cancellationToken);
    public Task<Projekt?> GetProjekt(int id, CancellationToken cancellationToken);

    public Task<IReadOnlyList<Abteilung>> GetAbteilungen(CancellationToken cancellationToken);
    public Task<Abteilung?> GetAbteilung(int id, CancellationToken cancellationToken);
}
