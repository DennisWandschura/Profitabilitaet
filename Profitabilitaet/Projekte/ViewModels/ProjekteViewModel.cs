using Profitabilitaet.Common.Models;
using System;
using System.Collections.Generic;
using Profitabilitaet.Database.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using Profitabilitaet.Common;
using System.Threading;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Profitabilitaet.Projekte.ViewModels;

internal partial class ProjekteViewModel : ObservableObject
{
    [ObservableProperty]
    Database.Entities.Projekt? _selectedProject;

    [ObservableProperty]
    IReadOnlyList<Database.Entities.Projekt>? _projekte;
    private readonly Connection _connection;
    private readonly LoggedInUser _loggedInUser;
    [ObservableProperty]
    private Visibility _editButtonVisibility;
    [ObservableProperty]
    private Visibility _saveButtonVisibility;

    [ObservableProperty]
    private Visibility _editControlsNutzerVisibility;
    [ObservableProperty]
    private Visibility _editControlsAdminVisibility;
    [ObservableProperty]
    private Visibility _viewControlsNutzerVisibility;
    [ObservableProperty]
    private Visibility _viewControlsAdminVisibility;
    [ObservableProperty]
    private IReadOnlyList<Nutzer>? _mitarbeiter;
    [ObservableProperty]
    private bool _canEditIstStorniert = false;

    [ObservableProperty]
    private ObservableCollection<Buchung>? _buchungen;

    public ProjekteViewModel(Connection connection, LoggedInUser loggedInUser)
    {
        _connection = connection;
        _loggedInUser = loggedInUser;

        EditControlsAdminVisibility = Visibility.Hidden;
        EditControlsNutzerVisibility = Visibility.Hidden;

        ViewControlsAdminVisibility = Visibility.Visible;
        ViewControlsNutzerVisibility = Visibility.Visible;

        EditButtonVisibility = Visibility.Visible;
        SaveButtonVisibility = Visibility.Hidden;

        LoadData();
    }

    private async Task LoadData()
    {
        using var connection = _connection.Create();
        Mitarbeiter = await connection.GetNutzer(CancellationToken.None);
        Projekte = await connection.GetProjekte(CancellationToken.None);
    }

    partial void OnSelectedProjectChanged(Database.Entities.Projekt? value)
    {
        //Select * From buchung Where ProjektId = value.Id;

        GetBuchungen(value);
    }

    private async Task GetBuchungen(Database.Entities.Projekt? value)
    {
        using var connection = _connection.Create();
        var buchungen = await connection.GetBuchungen(value.Id, CancellationToken.None);

        Buchungen = new ObservableCollection<Buchung>(buchungen);
    }

    [RelayCommand]
    private void OnEdit()
    {
        SetEditVisibilities();
    }

    [RelayCommand]
    private void OnSave()
    {
        SetViewVisibilities();

        if (SelectedProject is not null)
        {
            using var connection = _connection.Create();
            connection.Update(SelectedProject);
            connection.SaveChanges();
        }
    }

    [RelayCommand]
    private void OnCancel()
    {
        SetViewVisibilities();
    }

    private void SetEditVisibilities()
    {
        if (_loggedInUser.Rolle != Rolle.NUTZER)
        {
            EditControlsAdminVisibility = Visibility.Visible;
            ViewControlsAdminVisibility = Visibility.Hidden;
            CanEditIstStorniert = true;
        }

        EditControlsNutzerVisibility = Visibility.Visible;
        ViewControlsNutzerVisibility = Visibility.Hidden;

        EditButtonVisibility = Visibility.Hidden;
        SaveButtonVisibility = Visibility.Visible;
    }

    private void SetViewVisibilities()
    {
        EditControlsAdminVisibility = Visibility.Hidden;
        ViewControlsAdminVisibility = Visibility.Visible;

        EditControlsNutzerVisibility = Visibility.Hidden;
        ViewControlsNutzerVisibility = Visibility.Visible;

        EditButtonVisibility = Visibility.Visible;
        SaveButtonVisibility = Visibility.Hidden;

        CanEditIstStorniert = false;
    }

    [RelayCommand]
    private void OnNeueBuchung()
    {
        if(SelectedProject is null)
        {
            return;
        }

        var neueBuchungView = new Views.NeueBuchungView(SelectedProject, _connection);
        neueBuchungView.Topmost = true;
        neueBuchungView.ShowDialog();
    }
}
