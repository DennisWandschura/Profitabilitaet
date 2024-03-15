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
                Address = "localhost",
                Port = 3306,
                User = "Profitabilitaet",
                Password = "Gruppe1",
                Database = "Profitabilitaet"
            };

            var connection = new Profitabilitaet.Library.Database.Connection(settings);
            connection.nutzer.Count().Should().Be(0);
        }
    }
}