using Microsoft.EntityFrameworkCore;
using ProfitabilitaetBackend.Config;

namespace ProfitabilitaetBackend.Connection
{
    public static class MySqlConnection
    {
        public static DatabaseConnection Create(DatabaseSettings settings)
        {
            return new DatabaseConnection(settings, OnConfiguring);
        }

        private static void OnConfiguring(DbContextOptionsBuilder builder, DatabaseSettings settings)
        {
            builder.UseMySQL($"server={settings.Address};Port={settings.Port};database={settings.Database};user={settings.User};password={settings.Password}");
        }
    }
}
