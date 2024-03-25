using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Profitabilitaet.Database;

internal interface IConnection
{
    public Task<IReadOnlyList<Nutzer>> GetNutzer(CancellationToken cancellationToken);
    public Task<Nutzer?> GetNutzer(NutzerId id, CancellationToken cancellationToken);

    public Task<Nutzer?> GetNutzer(string loginName, string passwort, CancellationToken cancellationToken);

    public Task<IReadOnlyList<Projekt>> GetProjekte(CancellationToken cancellationToken);
    public Task<Projekt?> GetProjekt(int id, CancellationToken cancellationToken);

    public Task<IReadOnlyList<Abteilung>> GetAbteilungen(CancellationToken cancellationToken);
    public Task<Abteilung?> GetAbteilung(AbteilungsId id, CancellationToken cancellationToken);
}
