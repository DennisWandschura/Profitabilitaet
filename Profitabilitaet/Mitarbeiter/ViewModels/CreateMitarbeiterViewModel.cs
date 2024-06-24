using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Profitabilitaet.Database.Connection;
using Profitabilitaet.Database.Entities;
using Profitabilitaet.Mitarbeiter.Views;

namespace Profitabilitaet.Mitarbeiter.ViewModels;

public partial class CreateMitarbeiterViewModel : ObservableObject
{
    public Geschlecht[] Geschlechter { get; } = [Geschlecht.Maennlich, Geschlecht.Weiblich, Geschlecht.Divers];
    public Rolle[] Rollen { get; } = [Rolle.NUTZER, Rolle.ABTEILUNGSLEITER, Rolle.ADMIN];
    
    private readonly DatabaseConnection _connection;
    private readonly CreateMitarbeiterView _view;
    public Database.Entities.Nutzer Mitarbeiter { get; set; } = new();
    
    public CreateMitarbeiterViewModel(DatabaseConnection connection, CreateMitarbeiterView view)
    {
        _connection = connection;
        _view = view;
        Mitarbeiter.Einstellungsdatum = DateTime.Today;
    }
    
    [RelayCommand]
    private async Task OnSpeichern()
    {
        _connection.Nutzer.Add(Mitarbeiter);
        await _connection.SaveChangesAsync();

        _view.DialogResult = true;
        _view.Close();
    }
    
    private bool HasValidData()
    {
        /*
         	Id int NOT NULL AUTO_INCREMENT,
	Rolle VARCHAR(24) NOT NULL,
	Vorname VARCHAR(255) NOT NULL,
	Nachname VARCHAR(255) NOT NULL,
	Plz VARCHAR(8),
	Ort VARCHAR(255),
	Strasse VARCHAR(255),
	Hausnummer int,
	Geschlecht VARCHAR(16),
	Telefonnummer VARCHAR(64),
	Einstellungsdatum date NOT NULL,
	Loginname VARCHAR(255) NOT NULL,
	Passwort VARCHAR(255) NOT NULL,
         */
        
        return !string.IsNullOrEmpty(Mitarbeiter.Vorname) &&
               !string.IsNullOrEmpty(Mitarbeiter.Vorname) &&
               !string.IsNullOrEmpty(Mitarbeiter.Loginname) &&
               !string.IsNullOrEmpty(Mitarbeiter.Passwort);
    }
}
