using CommunityToolkit.Mvvm.Input;
using Profitabilitaet.Database.Connection;
using Profitabilitaet.Database.Entities;
using Profitabilitaet.Projekte.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Projekte.ViewModels;

internal partial class NeuesProjektViewModel
{
    private readonly DatabaseConnection _connection;
    private readonly NeuesProjektView _view;

    public Projekt Projekt { get; init; } = new()
    {
        Bezeichnung = string.Empty,
        Beginn = DateTime.Now,
        Ende = DateTime.Now
    };

    public IReadOnlyList<Nutzer> Mitarbeiter { get; init; }

    public NeuesProjektViewModel(IReadOnlyList<Nutzer> mitarbeiter, DatabaseConnection connection, NeuesProjektView view)
    {
        Mitarbeiter = mitarbeiter;
        _connection = connection;
        _view = view;
    }

    [RelayCommand]
    private async Task OnSpeichern()
    {
        _connection.Projekte.Add(Projekt);
        await _connection.SaveChangesAsync();

        _view.DialogResult = true;
        _view.Close();
    }
}
