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
        public DbSet<Nutzer> nutzer { get; set; }

        private DatabaseSettings _settings;

        public Connection(DatabaseSettings settings)
        {
            _settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL($"server={_settings.Address};Port={_settings.Port};database={_settings.Database};user={_settings.User};password={_settings.Password}");
        }

        public Task<IEnumerable<Nutzer>> GetNutzer()
        {
            throw new NotImplementedException();
        }

        public Task<Nutzer> GetNutzer(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Projekt> GetProjekt(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Projekt>> GetProjekte()
        {
            throw new NotImplementedException();
        }
    }
}
