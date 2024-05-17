﻿using Profitabilitaet.Database.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Profitabilitaet.Database.Connection;

internal interface IConnection
{
    public Task<IReadOnlyList<Nutzer>> GetNutzer(CancellationToken cancellationToken);
    public Task<Nutzer?> GetNutzer(NutzerId id, CancellationToken cancellationToken);

    public Task<Nutzer?> GetNutzer(string loginName, string passwort, CancellationToken cancellationToken);

    public Task<IReadOnlyList<Projekt>> GetProjekte(CancellationToken cancellationToken);
    public Task<Projekt?> GetProjekt(ProjektId id, CancellationToken cancellationToken);

    public Task<IReadOnlyList<Abteilung>> GetAbteilungen(CancellationToken cancellationToken);
    public Task<Abteilung?> GetAbteilung(AbteilungsId id, CancellationToken cancellationToken);

    public Task<IReadOnlyList<Buchung>> GetBuchungen(CancellationToken cancellationToken);
    public Task<List<Buchung>> GetBuchungen(ProjektId projektId);
    public Task<IReadOnlyList<Buchung>> GetBuchungen(NutzerId nutzerId);
    public Task<Buchung?> GetBuchung(BuchungId id, CancellationToken cancellationToken);
    public Task AddBuchung(Buchung buchung);
}
