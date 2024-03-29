using Microsoft.EntityFrameworkCore;
using ProfitabilitaetBackend.Config;

namespace ProfitabilitaetBackend.Connection
{
    public static class SqliteConnection
    {
        public static DatabaseConnection Create(DatabaseSettings settings)
        {
            return new DatabaseConnection(settings, OnConfiguring);
        }
        
        private static void OnConfiguring(DbContextOptionsBuilder builder, DatabaseSettings settings)
        {
            builder.UseSqlite($"Data Source={settings.Database}");
        }
    }
}
