using CommunityToolkit.Mvvm.ComponentModel;
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

    private LoggedInUser _loggedInUser;
    private readonly Common.Connection _connection;

    public Visibility EditViewVisibility { get; init; }
    public Visibility DisplayViewVisibility { get; init; }
    public Geschlecht[] Geschlechter { get; } = [Geschlecht.Maennlich, Geschlecht.Weiblich, Geschlecht.Divers];
    public Rolle[] Rollen { get; } = [Rolle.NUTZER, Rolle.ABTEILUNGSLEITER, Rolle.ADMIN];

    public MainViewViewModel(LoggedInUser loggedInUser, Common.Connection connection)
    {
        _loggedInUser = loggedInUser;
        _connection = connection;

        EditViewVisibility = GetVisibilityEditView(_loggedInUser.Rolle);
        DisplayViewVisibility = GetVisibilityDisplayView(_loggedInUser.Rolle);

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
        var connection =_connection.Create();
        Mitarbeiter = await connection.GetNutzer(CancellationToken.None);
    }
}
