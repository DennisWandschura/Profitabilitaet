using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Profitabilitaet.Database.Connection;
using Profitabilitaet.Database.Entities;
using Profitabilitaet.Mitarbeiter.Views;

namespace Profitabilitaet.Mitarbeiter.ViewModels;

public partial class CreateMitarbeiterViewModel : ObservableObject, IDataErrorInfo
{
    public Geschlecht[] Geschlechter { get; } = [Geschlecht.Maennlich, Geschlecht.Weiblich, Geschlecht.Divers];
    public Rolle[] Rollen { get; } = [Rolle.NUTZER, Rolle.ABTEILUNGSLEITER, Rolle.ADMIN];

    private readonly DatabaseConnection _connection;
    private readonly CreateMitarbeiterView _view;
    public Database.Entities.Nutzer Mitarbeiter { get; set; } = new();
    public string Vorname
    {
        get => Mitarbeiter.Vorname;
        set => Mitarbeiter.Vorname = value;
    }
    public string Nachname
    {
        get => Mitarbeiter.Nachname;
        set => Mitarbeiter.Nachname = value;
    }
    public string Loginname
    {
        get => Mitarbeiter.Loginname;
        set => Mitarbeiter.Loginname = value;
    }
    public string Passwort
    {
        get => Mitarbeiter.Passwort;
        set => Mitarbeiter.Passwort = value;
    }

    public CreateMitarbeiterViewModel(DatabaseConnection connection, CreateMitarbeiterView view)
    {
        _connection = connection;
        _view = view;
        Mitarbeiter.Einstellungsdatum = DateTime.Today;
    }

    [RelayCommand]
    private async Task OnSpeichern()
    {
        if (!HasValidData())
        {
            MessageBox.Show($"Die Mitarbeiterdaten sind nicht alle korrekt!\n{GetInvalidFieldsString()}", "Falsche Mitarbeiterdaten",
                MessageBoxButton.OK);
            return;
        }

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
               !string.IsNullOrEmpty(Mitarbeiter.Nachname) &&
               !string.IsNullOrEmpty(Mitarbeiter.Loginname) &&
               !string.IsNullOrEmpty(Mitarbeiter.Passwort);
    }

    #region IDataErrorInfo

    public string Error => string.Empty;

    public string this[string columnName]
    {
        get
        {
            var result = string.Empty;

            switch (columnName)
            {
                case "Vorname":
                    if (string.IsNullOrEmpty(Vorname))
                    {
                        result = "Bitte geben Sie einen Vornamen ein!";
                    }
                    break;
                case "Nachname":
                    if (string.IsNullOrEmpty(Nachname))
                    {
                        result = "Bitte geben Sie einen Nachname ein!";
                    }
                    break;
                case "Loginname":
                    if (string.IsNullOrEmpty(Loginname))
                    {
                        result = "Bitte geben Sie einen Loginnamen ein!";
                    }
                    break;
                case "Passwort":
                    if (string.IsNullOrEmpty(Passwort))
                    {
                        result = "Bitte geben Sie ein Passwort ein!";
                    }
                    break;
            }

            return result;
        }
    }

    #endregion

    private string GetInvalidFieldsString()
    {
        List<string> fields = [];

        if (string.IsNullOrEmpty(Mitarbeiter.Vorname))
        {
            fields.Add("Bitte geben Sie einen Vornamen ein!");
        }
        if (string.IsNullOrEmpty(Nachname))
        {
            fields.Add("Bitte geben Sie einen Nachname ein!");
        }
        if (string.IsNullOrEmpty(Loginname))
        {
            fields.Add( "Bitte geben Sie einen Loginnamen ein!");
        }
        if (string.IsNullOrEmpty(Passwort))
        {
            fields.Add( "Bitte geben Sie ein Passwort ein!");
        }
        
        return fields.Count == 0 ? string.Empty : (new StringBuilder()).AppendJoin('\n', fields).ToString();
    }
}