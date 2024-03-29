using FluentAssertions;
using ProfitabilitaetBackend;
using ProfitabilitaetBackend.Config;
using ProfitabilitaetBackend.Connection;

namespace Profitabilitaet.Tests
{
    public class SqliteConnectionTests
    {
        private readonly DatabaseSettings _settings = new DatabaseSettings
        {
            Database = "profitabilitaet.db"
        };

        [Fact]
        public async Task GetAlleNutzer_ReturnsNutzer()
        {
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(CancellationToken.None);
            nutzer.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetNutzer_ReturnsNutzer()
        {
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(new NutzerId(100), CancellationToken.None);
            nutzer.Should().NotBeNull();
        }
        
        [Fact]
        public async Task GetNutzer_ReturnsNull()
        {
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(new NutzerId(0), CancellationToken.None);
            nutzer.Should().BeNull();
        }

        [Fact]
        public async Task GetNutzerLogin_ReturnsNutzer()
        {
            string loginName = "admin";
            string passwort = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918";
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(loginName, passwort, CancellationToken.None);
            nutzer.Should().NotBeNull();
        }
        
        [Fact]
        public async Task GetNutzerLogin_ReturnsNull()
        {
            string loginName = "0";
            string passwort = "00";
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(loginName, passwort, CancellationToken.None);
            nutzer.Should().BeNull();
        }

        [Fact]
        public async Task GetAbteilungen_ReturnsAbteilungen()
        {
            var connection = CreateConnection();
            var abteilungen = await connection.GetAbteilungen(CancellationToken.None);
            abteilungen.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetAbteilung_ReturnsAbteilung()
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
        
        [Fact]
        public async Task GetAbteilung_ReturnsNull()
        {
            var abteilungsId = AbteilungsId.Empty;
            var connection = CreateConnection();
            var abteilung = await connection.GetAbteilung(abteilungsId, CancellationToken.None);
            abteilung.Should().BeNull();
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

        private DatabaseConnection CreateConnection()
        {
            return SqliteConnection.Create(_settings);
        }
    }
}