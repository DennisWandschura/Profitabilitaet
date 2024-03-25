using FluentAssertions;
using Profitabilitaet.Database;

namespace Profitabilitaet.Tests
{
    public class SqliteConnectionTests
    {
        private readonly Profitabilitaet.Config.DatabaseSettings _settings = new Profitabilitaet.Config.DatabaseSettings
        {
            Database = "profitabilitaet.db"
        };

        [Fact]
        public async void GetAlleNutzerTest()
        {
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(CancellationToken.None);
            nutzer.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async void GetNutzerTest()
        {
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(new NutzerId(100), CancellationToken.None);
            nutzer.Should().NotBeNull();
        }

        [Fact]
        public async void GetNutzerLoginTest()
        {
            string loginName = "admin";
            string passwort = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918";
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(loginName, passwort, CancellationToken.None);
            nutzer.Should().NotBeNull();
        }

        [Fact]
        public async void GetAbteilungenTest()
        {
            var connection = CreateConnection();
            var abteilungen = await connection.GetAbteilungen(CancellationToken.None);
            abteilungen.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async void GetAbteilungTest()
        {
            var abteilungsId = new AbteilungsId(1);
            var connection = CreateConnection();
            var abteilung = await connection.GetAbteilung(abteilungsId, CancellationToken.None);
            abteilung.Should().NotBeNull();
            abteilung.Leiter.Should().NotBeNull();
            
            var nutzer = await connection.GetNutzer(abteilung.Leiter.Id, CancellationToken.None);
            nutzer.Should().NotBeNull();

            abteilung.Leiter.Should().Be(nutzer);
        }

        // [Theory]
        // [InlineData(1, new NutzerId(105))]
        // public async void AbteilungLeiterTest(int abteilungsId, NutzerId leiterId)
        // {
        //     var connection = CreateConnection();
        //     
        //     var nutzer = await connection.GetNutzer(leiterId, CancellationToken.None);
        //     nutzer.Should().NotBeNull();
        //     
        //     var abteilung = await connection.GetAbteilung(abteilungsId, CancellationToken.None);
        //     abteilung.Should().NotBeNull();
        //     abteilung.LeiterId.Should().Be(leiterId);
        //     Assert.Fail();
        // }

        private SqliteConnection CreateConnection()
        {
            return new SqliteConnection(_settings);
        }
    }
}