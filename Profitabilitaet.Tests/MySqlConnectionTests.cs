using FluentAssertions;
using ProfitabilitaetBackend.Entities;
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
        public async void GetNutzer_ReturnsNutzerListe()
        {
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(CancellationToken.None);
            nutzer.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async void GetNutzerViaId_ReturnsSingleNutzer()
        {
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(new NutzerId(100), CancellationToken.None);
            nutzer.Should().NotBeNull();
        }

        [Fact]
        public async void GetNutzerViaLoginData_ReturnsSingleUser()
        {
            string loginName = "admin";
            string passwort = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918";
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(loginName, passwort, CancellationToken.None);
            nutzer.Should().NotBeNull();
        }

        [Fact]
        public async void GetAbteilungen_ReturnsAbteilungsList()
        {
            var connection = CreateConnection();
            var abteilungen = await connection.GetAbteilungen(CancellationToken.None);
            abteilungen.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetAbteilungViaId_ReturnsSingleAbteilung()
        {
            var abteilungsId = new AbteilungsId(1);
            var connection = CreateConnection();
            var abteilung = await connection.GetAbteilung(abteilungsId, CancellationToken.None);
            abteilung.Should().NotBeNull();
        }

        [Fact]
        public async Task GetProjekte_ReturnsProjektList()
        {
            var connection = CreateConnection();
            var projects = await connection.GetProjekte(CancellationToken.None);
            projects.Should().HaveCountGreaterThan(0);
        }

        [Theory]
        [InlineData(1, 105)]
        public async Task GetAbteilung_CompareAbteilungsLeiter_IsEqual(int abteilungsId, int leiterId)
        {
            var nutzerId = new NutzerId(leiterId);
            var connection = CreateConnection();

            var nutzer = await connection.GetNutzer(nutzerId, CancellationToken.None);
            nutzer.Should().NotBeNull();

            var abteilung = await connection.GetAbteilung(new AbteilungsId(abteilungsId), CancellationToken.None);
            abteilung.Should().NotBeNull();
            abteilung.Leiter.Should().Be(nutzer);
        }

        [Fact]
        public async Task GetBuchungen_ReturnsList()
        {
            var connection = CreateConnection();
            var projects = await connection.GetBuchungen(CancellationToken.None);
            projects.Should().HaveCountGreaterThan(0);
        }

        private DatabaseConnection CreateConnection()
        {
            return MySqlConnection.Create(_settings);
        }
    }
}