using Microsoft.EntityFrameworkCore.ChangeTracking;
using Profitabilitaet.Database.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Profitabilitaet.Database.Connection;

internal interface IConnection
{
    public Task<IReadOnlyList<Nutzer>> GetNutzer();
    public Task<Nutzer?> GetNutzer(NutzerId id);
    public Task<Nutzer?> GetNutzer(string loginName, string passwort);
    public ValueTask<EntityEntry<Nutzer>> AddNutzer(Nutzer nutzer);

    public Task<IReadOnlyList<Projekt>> GetProjekte();
    public Task<Projekt?> GetProjekt(ProjektId id);
    public ValueTask<EntityEntry<Projekt>> AddProjekt(Projekt projekt);

    public Task<IReadOnlyList<Abteilung>> GetAbteilungen();
    public Task<Abteilung?> GetAbteilung(AbteilungsId id);

    public Task<IReadOnlyList<Buchung>> GetBuchungen();
    public Task<List<Buchung>> GetBuchungen(ProjektId projektId);
    public Task<IReadOnlyList<Buchung>> GetBuchungen(NutzerId nutzerId);
    public Task<Buchung?> GetBuchung(BuchungId id);
    public ValueTask<EntityEntry<Buchung>> AddBuchung(Buchung buchung);
}
