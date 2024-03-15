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

        [Fact]
        public async void GetAbteilungenTest()
        {
            var connection = new Profitabilitaet.Library.Database.Connection(_settings);
            var abteilungen = await connection.GetAbteilungen(CancellationToken.None);
            abteilungen.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async void GetAbteilungTest()
        {
            var connection = new Profitabilitaet.Library.Database.Connection(_settings);
            var abteilung = await connection.GetAbteilung(1, CancellationToken.None);
            abteilung.Should().NotBeNull();
        }

        [Theory]
        [InlineData(1, 105)]
        public async void AbteilungLeiterTest(int abteilungsId, int leiterId)
        {
            var connection = new Profitabilitaet.Library.Database.Connection(_settings);

            var nutzer = await connection.GetNutzer(leiterId, CancellationToken.None);
            nutzer.Should().NotBeNull();

            var abteilung = await connection.GetAbteilung(abteilungsId, CancellationToken.None);
            abteilung.Should().NotBeNull();
            abteilung.LeiterId.Should().Be(leiterId);
        }
    }
}