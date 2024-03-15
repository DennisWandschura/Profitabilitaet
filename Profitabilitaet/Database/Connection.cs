using Microsoft.EntityFrameworkCore;
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
        private Settings _settings;

        public Connection(Settings settings)
        {
            _settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL($"server=localhost;database=library;user=user;password=password");
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
