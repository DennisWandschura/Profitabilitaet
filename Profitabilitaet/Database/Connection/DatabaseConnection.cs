using Microsoft.EntityFrameworkCore;
using Profitabilitaet.Config;
using Profitabilitaet.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Profitabilitaet.Database.Connection;

public class DatabaseConnection : DbContext, IConnection
{
    private readonly DbSet<Nutzer> _nutzer;
    private readonly DbSet<Abteilung> _abteilungen;
    private readonly DbSet<Projekt> _projekte;
    private readonly DbSet<Buchung> _buchungen;
    private readonly DatabaseSettings _settings;
    private readonly Action<DbContextOptionsBuilder, DatabaseSettings> _onConfiguring;

    public DatabaseConnection(DatabaseSettings settings, Action<DbContextOptionsBuilder, DatabaseSettings> onConfiguring)
    {
        _settings = settings;
        _onConfiguring = onConfiguring;
        _nutzer = Set<Nutzer>();
        _abteilungen = Set<Abteilung>();
        _projekte = Set<Projekt>();
        _buchungen = Set<Buchung>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _onConfiguring(optionsBuilder, _settings);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new NutzerEntityTypeConfiguration().Configure(modelBuilder.Entity<Nutzer>());
        new AbteilungEntityTypeConfiguration().Configure(modelBuilder.Entity<Abteilung>());
        new ProjektEntityTypeConfiguration().Configure(modelBuilder.Entity<Projekt>());
        new BuchungEntityTypeConfiguration().Configure(modelBuilder.Entity<Buchung>());
    }
    
    public Task<IReadOnlyList<Nutzer>> GetNutzer(CancellationToken cancellationToken)
    {
        return _nutzer.ToReadOnlyListAsync(cancellationToken);
    }

    public Task<Nutzer?> GetNutzer(NutzerId id, CancellationToken cancellationToken)
    {
        return _nutzer.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Nutzer?> GetNutzer(string loginName, string passwort, CancellationToken cancellationToken)
    {
        return _nutzer.Where(x => x.Loginname == loginName && x.Passwort == passwort).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<Projekt?> GetProjekt(ProjektId id, CancellationToken cancellationToken)
    {
        return _projekte.Where(x => x.Id == id)
            .Include(x => x.Leiter)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<IReadOnlyList<Projekt>> GetProjekte(CancellationToken cancellationToken)
    {
        return _projekte.Include(x => x.Leiter).ToReadOnlyListAsync(cancellationToken);
    }

    public Task<IReadOnlyList<Abteilung>> GetAbteilungen(CancellationToken cancellationToken)
    {
        return _abteilungen.Include(x => x.Leiter).ToReadOnlyListAsync();
    }

    public Task<Abteilung?> GetAbteilung(AbteilungsId id, CancellationToken cancellationToken)
    {
        return _abteilungen.Where(x => x.Id == id)
            .Include(x => x.Leiter)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<IReadOnlyList<Buchung>> GetBuchungen(CancellationToken cancellationToken)
    {
        return _buchungen.ToReadOnlyListAsync();
    }

    public Task<Buchung?> GetBuchung(BuchungId id, CancellationToken cancellationToken)
    {
       return _buchungen.Where(x => x.Id == id)
            .Include(x => x.Mitarbeiter)
            .Include(x => x.Projekt)
            .FirstOrDefaultAsync(cancellationToken);
    }
}

public abstract class ApplicationDbContextBase : DbContext
{
    protected ApplicationDbContextBase(DbContextOptions contextOptions)
        : base(contextOptions)
    {
    }
}

public sealed class ApplicationDbContext1 : ApplicationDbContextBase
{
    public ApplicationDbContext1(DbContextOptions<ApplicationDbContext1> contextOptions)
        : base(contextOptions)
    {
    }
}
