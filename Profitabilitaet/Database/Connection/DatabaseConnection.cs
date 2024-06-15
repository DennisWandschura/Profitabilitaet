using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Profitabilitaet.Common.Models;
using Profitabilitaet.Config;
using Profitabilitaet.Database.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Profitabilitaet.Database.Connection;

public class DatabaseConnection : DbContext, IConnection
{
    public ObservableCollection<Nutzer>? Nutzer { get; private set; }
    public ObservableCollection<Projekt>? Projekte { get; private set; }

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

    public async Task OnLogin()
    {
        await _abteilungen.LoadAsync();

        await _nutzer.LoadAsync();
        Nutzer = _nutzer.Local.ToObservableCollection();

        await _buchungen.LoadAsync();

        await _projekte.LoadAsync();
        Projekte = _projekte.Local.ToObservableCollection();
    }
    
    public Task<IReadOnlyList<Nutzer>> GetNutzer()
    {
        return _nutzer.ToReadOnlyListAsync();
    }

    public Task<Nutzer?> GetNutzer(NutzerId id)
    {
        return _nutzer.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public Task<Nutzer?> GetNutzer(string loginName, string passwort)
    {
        return _nutzer.Where(x => x.Loginname == loginName && x.Passwort == passwort).FirstOrDefaultAsync();
    }

    public ValueTask<EntityEntry<Nutzer>> AddNutzer(Nutzer nutzer)
    {
        return _nutzer.AddAsync(nutzer);
    }

    public Task<Projekt?> GetProjekt(ProjektId id)
    {
        return _projekte.Where(x => x.Id == id)
            .Include(x => x.Leiter)
            .Include(x => x.Buchungen)
            .FirstOrDefaultAsync();
    }

    public Task<IReadOnlyList<Projekt>> GetProjekte()
    {
        return _projekte
            .Include(x => x.Leiter)
            .Include(x => x.Buchungen)
            .ToReadOnlyListAsync();
    }

    public ValueTask<EntityEntry<Projekt>> AddProjekt(Projekt projekt)
    {
        return _projekte.AddAsync(projekt);
    }

    public Task<IReadOnlyList<Abteilung>> GetAbteilungen()
    {
        return _abteilungen.Include(x => x.Leiter).ToReadOnlyListAsync();
    }

    public Task<Abteilung?> GetAbteilung(AbteilungsId id)
    {
        return _abteilungen.Where(x => x.Id == id)
            .Include(x => x.Leiter)
            .FirstOrDefaultAsync();
    }

    public Task<IReadOnlyList<Buchung>> GetBuchungen()
    {
        return _buchungen
            .Include(x => x.Mitarbeiter)
            .Include(x => x.Projekt)
            .ToReadOnlyListAsync();
    }

    public Task<List<Buchung>> GetBuchungen(ProjektId projektId)
    {
        return _buchungen.FromSql($"SELECT * FROM buchung WHERE ProjektId={projektId.Value}")
            .Include(x => x.Mitarbeiter)
            .Include(x => x.Projekt)
            .ToListAsync();
    }

    public Task<IReadOnlyList<Buchung>> GetBuchungen(NutzerId nutzerId)
    {
        return _buchungen.FromSql($"SELECT * FROM buchung WHERE MitarbeiterId={nutzerId.Value}")
           .Include(x => x.Mitarbeiter)
           .Include(x => x.Projekt)
           .ToReadOnlyListAsync();
    }

    public Task<Buchung?> GetBuchung(BuchungId id)
    {
       return _buchungen.Where(x => x.Id == id)
            .Include(x => x.Mitarbeiter)
            .Include(x => x.Projekt)
            .FirstOrDefaultAsync();
    }

    public ValueTask<EntityEntry<Buchung>> AddBuchung(Buchung buchung)
    {
       return _buchungen.AddAsync(buchung);
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
