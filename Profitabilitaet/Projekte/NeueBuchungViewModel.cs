using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Profitabilitaet.Database;
using Profitabilitaet.Database.Entities;

namespace Profitabilitaet.Projekte;

public partial class NeueBuchungViewModel : ObservableObject
{
    public Database.Entities.Projekt Projekt { get; init; }

    [ObservableProperty]
    private Nutzer? _selectedMitarbeiter;

    [ObservableProperty]
    private IReadOnlyList<Nutzer>? _mitarbeiterListe;

    [ObservableProperty]
    private int _anteil;
    [ObservableProperty]
    private int _jahr;
    [ObservableProperty]
    private int _woche;

    private readonly Common.Connection _connection;
    private IReadOnlyList<Buchung>? _nutzerBuchungen;

    public NeueBuchungViewModel(Projekt projekt, Common.Connection connection)
    {
        Projekt = projekt;
        _connection = connection;

        GetMitarbeiterListe();
    }

    private async Task GetMitarbeiterListe()
    {
        using var connection = _connection.Create();
        MitarbeiterListe = await connection.GetNutzer(CancellationToken.None);
    }

    partial void OnSelectedMitarbeiterChanged(Nutzer? value)
    {
        GetNutzerBuchungen(value);
    }

    private async Task GetNutzerBuchungen(Nutzer? value)
    {
        using var connection = _connection.Create();
        _nutzerBuchungen = await connection.GetBuchungen(value.Id, CancellationToken.None);
    }

    [RelayCommand]
    private void OnSpeichern()
    {

    }
}
