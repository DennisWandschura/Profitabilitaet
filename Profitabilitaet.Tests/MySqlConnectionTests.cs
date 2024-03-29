using FluentAssertions;
using ProfitabilitaetBackend;
using ProfitabilitaetBackend.Config;
using ProfitabilitaetBackend.Connection;

namespace Profitabilitaet.Tests
{
    public class MySqlConnectionTests
    {
        private readonly DatabaseSettings _settings = new DatabaseSettings
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
        public async Task GetAbteilungTest()
        {
            var abteilungsId = new AbteilungsId(1);
            var connection = CreateConnection();
            var abteilung = await connection.GetAbteilung(abteilungsId, CancellationToken.None);
            abteilung.Should().NotBeNull();
        }

        [Theory]
        [InlineData(1, 105)]
        public void AbteilungLeiterTest(int abteilungsId, int leiterId)
        {
            Assert.Fail();
            // var connection = new Profitabilitaet.Database.MySqlConnection(_settings);
            //
            // var nutzer = await connection.GetNutzer(leiterId, CancellationToken.None);
            // nutzer.Should().NotBeNull();
            //
            // var abteilung = await connection.GetAbteilung(abteilungsId, CancellationToken.None);
            // abteilung.Should().NotBeNull();
            // abteilung.LeiterId.Should().Be(leiterId);
        }

        private DatabaseConnection CreateConnection()
        {
            return MySqlConnection.Create(_settings);
        }
    }
}