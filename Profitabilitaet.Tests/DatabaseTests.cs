using FluentAssertions;

namespace Profitabilitaet.Tests
{
    public class DatabaseTests
    {
        private readonly Profitabilitaet.Library.Config.DatabaseSettings _settings = new Profitabilitaet.Library.Config.DatabaseSettings
        {
            Address = "localhost",
            Port = 3310,
            User = "Profitabilitaet",
            Password = "Gruppe1",
            Database = "Profitabilitaet"
        };

        [Fact]
        public async void GetAlleNutzerTest()
        {
            var connection = new Profitabilitaet.Library.Database.Connection(_settings);
            var nutzer = await connection.GetNutzer(CancellationToken.None);
            nutzer.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async void GetNutzerTest()
        {
            var connection = new Profitabilitaet.Library.Database.Connection(_settings);
            var nutzer = await connection.GetNutzer(100, CancellationToken.None);
            nutzer.Should().NotBeNull();
        }
    }
}