using FluentAssertions;
using Profitabilitaet.Database.Entities;
using Profitabilitaet.Config;
using Profitabilitaet.Database.Connection;

namespace Profitabilitaet.Tests
{
    public class MySqlConnectionTests
    {
        private readonly DatabaseSettings _settings = new DatabaseSettings
        {
            Address = "localhost",
            Port = 3306,
            User = "Profitabilitaet",
            Password = "Gruppe1",
            Database = "Profitabilitaet"
        };

        [Fact]
        public async void GetNutzer_ReturnsList()
        {
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(CancellationToken.None);
            nutzer.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async void GetNutzer_ReturnsNutzer()
        {
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(new NutzerId(100), CancellationToken.None);
            nutzer.Should().NotBeNull();
        }

        [Fact]
        public async void GetNutzer_ViaLogin_ReturnsNutzer()
        {
            string loginName = "admin";
            string passwort = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918";
            var connection = CreateConnection();
            var nutzer = await connection.GetNutzer(loginName, passwort, CancellationToken.None);
            nutzer.Should().NotBeNull();
        }

        [Fact]
        public async void GetAbteilungen_ReturnsList()
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
        }

        [Fact]
        public async Task GetProjekte_ReturnsList()
        {
            var connection = CreateConnection();
            var projects = await connection.GetProjekte(CancellationToken.None);
            projects.Should().HaveCountGreaterThan(0);
            projects[0].Buchungen.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData(77)]
        public async Task GetProjekt_ReturnsProjekt(int projId)
        {
            var connection = CreateConnection();

            var project = await connection.GetProjekt(new ProjektId(projId),CancellationToken.None);
            project.Should().NotBeNull();
            project.Leiter.Should().NotBeNull();
            project.Buchungen.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData(1, 105)]
        public async Task CheckAbteilungsLeiter_IsEqualToNutzer(int abteilungsId, int leiterId)
        {
            var connection = CreateConnection();

            var abteilung = await connection.GetAbteilung(new AbteilungsId(abteilungsId), CancellationToken.None);
            abteilung.Should().NotBeNull();
            abteilung.Leiter.Should().NotBeNull();

            var nutzer = await connection.GetNutzer(new NutzerId(leiterId), CancellationToken.None);
            nutzer.Should().NotBeNull();
            abteilung.Leiter.Should().Be(nutzer);
        }

        [Fact]
        public async Task GetBuchungen_ReturnsList()
        {
            var connection = CreateConnection();

            var buchungen = await connection.GetBuchungen(CancellationToken.None);
            buchungen.Should().NotBeNull();
            buchungen.Should().HaveCountGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetBuchung_ReturnsBuchung(int buchungsId)
        {
            var connection = CreateConnection();

            var buchung = await connection.GetBuchung(new BuchungId(buchungsId), CancellationToken.None);
            buchung.Should().NotBeNull();
            buchung.Mitarbeiter.Should().NotBeNull();
            buchung.Projekt.Should().NotBeNull();
        }

        private DatabaseConnection CreateConnection()
        {
            return MySqlConnection.Create(_settings);
        }
    }
}