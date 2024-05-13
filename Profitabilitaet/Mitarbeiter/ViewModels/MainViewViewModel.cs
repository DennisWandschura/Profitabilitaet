using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Profitabilitaet.Common.Models;
using Profitabilitaet.Database.Connection;
using Profitabilitaet.Database.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Profitabilitaet.Mitarbeiter.ViewModels;

internal partial class MainViewViewModel : ObservableObject
{
    [ObservableProperty]
    private IReadOnlyList<Nutzer>? _mitarbeiter;

    [ObservableProperty]
    private Nutzer? _selectedUser;

    private readonly LoggedInUser _loggedInUser;
    private readonly Common.Connection _connection;

    public Visibility EditVisibility { get; init; }
    [ObservableProperty]
    private Visibility _editButtonVisibility;
    [ObservableProperty]
    private Visibility _saveButtonVisibility;
    [ObservableProperty]
    private Visibility _editControlsVisibility;
    [ObservableProperty]
    private Visibility _viewControlsVisibility;
    public Geschlecht[] Geschlechter { get; } = [Geschlecht.Maennlich, Geschlecht.Weiblich, Geschlecht.Divers];
    public Rolle[] Rollen { get; } = [Rolle.NUTZER, Rolle.ABTEILUNGSLEITER, Rolle.ADMIN];

    public MainViewViewModel(LoggedInUser loggedInUser, Common.Connection connection)
    {
        _loggedInUser = loggedInUser;
        _connection = connection;

        EditVisibility = GetVisibilityEditView(_loggedInUser.Rolle);
        EditButtonVisibility = Visibility.Visible;
        SaveButtonVisibility = Visibility.Hidden;
        EditControlsVisibility = Visibility.Hidden;
        ViewControlsVisibility = Visibility.Visible;

        LoadUsers();
    }

    private Visibility GetVisibilityEditView(Rolle rolle)
    {
        return rolle switch
        {
            Rolle.NUTZER => Visibility.Collapsed,
            Rolle.ABTEILUNGSLEITER => Visibility.Visible,
            Rolle.ADMIN => Visibility.Visible,
            _ => Visibility.Collapsed
        };
    }

    private Visibility GetVisibilityDisplayView(Rolle rolle)
    {
        return rolle switch
        {
            Rolle.NUTZER => Visibility.Visible,
            Rolle.ABTEILUNGSLEITER => Visibility.Collapsed,
            Rolle.ADMIN => Visibility.Collapsed,
            _ => Visibility.Visible
        };
    }

    private async Task LoadUsers()
    {
        using var connection =_connection.Create();
        Mitarbeiter = await connection.GetNutzer(CancellationToken.None);
    }

    [RelayCommand]
    private void OnEdit()
    {
        EditControlsVisibility = Visibility.Visible;
        ViewControlsVisibility = Visibility.Hidden;

        EditButtonVisibility = Visibility.Hidden;
        SaveButtonVisibility = Visibility.Visible;
    }

    [RelayCommand]
    private void OnSave()
    {
        EditControlsVisibility = Visibility.Hidden;
        ViewControlsVisibility = Visibility.Visible;

        EditButtonVisibility = Visibility.Visible;
        SaveButtonVisibility = Visibility.Hidden;

        if (SelectedUser is not null)
        {
            using var connection = _connection.Create();
            connection.Update(SelectedUser);
            connection.SaveChanges();
        }
    }
}
