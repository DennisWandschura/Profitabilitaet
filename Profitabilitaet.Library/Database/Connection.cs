using Microsoft.EntityFrameworkCore;
using Profitabilitaet.Library.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Profitabilitaet.Library.Database
{
    public class Connection : DbContext, IConnection
    {
        private DbSet<Nutzer> _nutzer { get; set; }
        private DbSet<Abteilung> _abteilungen{ get; set; }

        private DatabaseSettings _settings;

        public Connection(DatabaseSettings settings)
        {
            _settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL($"server={_settings.Address};Port={_settings.Port};database={_settings.Database};user={_settings.User};password={_settings.Password}");
        }

        public Task<IReadOnlyList<Nutzer>> GetNutzer(CancellationToken cancellationToken)
        {
            return _nutzer.ToReadOnlyListAsync(cancellationToken);
        }

        public Task<Nutzer?> GetNutzer(int id, CancellationToken cancellationToken)
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

        public Task<Abteilung?> GetAbteilung(int id, CancellationToken cancellationToken)
        {
            return _abteilungen.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
