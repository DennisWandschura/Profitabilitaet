﻿using ProfitabilitaetBackend.Entities;

namespace ProfitabilitaetBackend.Connection;

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
    public Task<Buchung?> GetBuchung(BuchungId id, CancellationToken cancellationToken);
}
