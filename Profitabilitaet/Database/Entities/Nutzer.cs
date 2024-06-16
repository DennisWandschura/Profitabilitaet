using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Profitabilitaet.Database.Connection;

namespace Profitabilitaet.Database.Entities;

public readonly record struct NutzerId(int Value)
{
    public static NutzerId Empty = default;
}

public partial class Nutzer : ObservableObject
{
    public NutzerId Id { get; set; }
    [ObservableProperty] private Rolle _rolle;
    
    private string _vorname;
    public string Vorname
    {
        get => _vorname;
        set
        {
            SetProperty(ref _vorname, value);
            OnPropertyChanged(nameof(DisplayName));
        }
    }
    
  
    private string _nachname;
    public string Nachname
    {
        get => _nachname;
        set
        {
            SetProperty(ref _nachname, value);
            OnPropertyChanged(nameof(DisplayName));
        }
    }

    [ObservableProperty] private string _plz;
    [ObservableProperty] private string _ort;
    [ObservableProperty] private string _strasse;
    [ObservableProperty] private int _hausnummer;
    [ObservableProperty] private Geschlecht _geschlecht;
    [ObservableProperty] private string _telefonnummer;
    [ObservableProperty] private DateTime _einstellungsdatum;
    [ObservableProperty] private string _loginname;
    [ObservableProperty] private string _passwort;

    public bool IsAdmin { get => Rolle == Rolle.ADMIN; }

    public string DisplayName => $"{Vorname} {Nachname}";
    
    public Task UpdateAsync(DatabaseConnection connection)
    {
        connection.Update(this);
        return connection.SaveChangesAsync();
    }
    
    public void Cancel(DatabaseConnection connection)
    {
        var entity = connection.Entry(this);
        
        Rolle = (Rolle)entity.Property("Rolle").OriginalValue;
        Vorname = (string)entity.Property("Vorname").OriginalValue;
        Nachname = (string)entity.Property("Nachname").OriginalValue;
        Plz = (string)entity.Property("Plz").OriginalValue;
        Ort = (string)entity.Property("Ort").OriginalValue;
        Strasse = (string)entity.Property("Strasse").OriginalValue;
        Hausnummer = (int)entity.Property("Hausnummer").OriginalValue;
        Geschlecht = (Geschlecht)entity.Property("Geschlecht").OriginalValue;
        Telefonnummer = (string)entity.Property("Telefonnummer").OriginalValue;
        Einstellungsdatum = (DateTime)entity.Property("Einstellungsdatum").OriginalValue;
        Loginname = (string)entity.Property("Loginname").OriginalValue;
        Passwort = (string)entity.Property("Passwort").OriginalValue;
    }
}
