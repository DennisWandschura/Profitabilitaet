using FluentAssertions;

namespace Profitabilitaet.Tests
{
    public class DatabaseTests
    {
        [Fact]
        public void ConnectionTest()
        {
            var settings = new Profitabilitaet.Library.Config.DatabaseSettings
            {
                Address = "localhos",
                Port = 3306,
                User = "Profitabilitaet",
                Password = "Gruppe1",
                Database = "Test" 
            };

            var connection = new Profitabilitaet.Library.Database.Connection(settings);
            connection.benutzer.Count().Should().Be(0);
        }
    }
}