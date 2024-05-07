using CommunityToolkit.Mvvm.ComponentModel;
using Profitabilitaet.Common.Models;
using Profitabilitaet.Database.Connection;
using Profitabilitaet.Database.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Profitabilitaet.Mitarbeiter.ViewModels;

internal partial class MainViewViewModel : ObservableObject
{
    [ObservableProperty]
    private IReadOnlyList<Nutzer>? _mitarbeiter;

    private LoggedInUser _loggedInUser;
    private readonly Common.Connection _connection;

    public MainViewViewModel(LoggedInUser loggedInUser, Common.Connection connection)
    {
        _loggedInUser = loggedInUser;
        _connection = connection;

        LoadUsers();
    }

    private async Task LoadUsers()
    {
        var connection =_connection.Create();
        Mitarbeiter = await connection.GetNutzer(CancellationToken.None);
    }
}
