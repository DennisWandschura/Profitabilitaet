using Microsoft.EntityFrameworkCore;
using ProfitabilitaetBackend.Config;

namespace ProfitabilitaetBackend.Connection;

public class DatabaseConnection : DbContext, IConnection
{
    private DbSet<Nutzer> _nutzer { get; set; }
    private DbSet<Abteilung> _abteilungen { get; set; }
    private readonly DatabaseSettings _settings;
    private readonly Action<DbContextOptionsBuilder, DatabaseSettings> _onConfiguring;

    public DatabaseConnection(DatabaseSettings settings, Action<DbContextOptionsBuilder, DatabaseSettings> onConfiguring)
    {
        _settings = settings;
        _onConfiguring= onConfiguring;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_settings.Database}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new NutzerEntityTypeConfiguration().Configure(modelBuilder.Entity<Nutzer>());
        new AbteilungEntityTypeConfiguration().Configure(modelBuilder.Entity<Abteilung>());
        new ProjektEntityTypeConfiguration().Configure(modelBuilder.Entity<Projekt>());
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

    public Task<Projekt?> GetProjekt(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Projekt>> GetProjekte(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Abteilung>> GetAbteilungen(CancellationToken cancellationToken)
    {
        return _abteilungen.ToReadOnlyListAsync();
    }

    public Task<Abteilung?> GetAbteilung(AbteilungsId id, CancellationToken cancellationToken)
    {
        return _abteilungen.Where(x => x.Id == id)
            .Include(x => x.Leiter)
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
