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
using Profitabilitaet.Projekte.Models;

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
    private Visibility _projectDetailsVisibility;

    public ProjekteViewModel(Connection connection, LoggedInUser loggedInUser)
    {
        _connection = connection;
        _loggedInUser = loggedInUser;

        EditControlsAdminVisibility = Visibility.Hidden;
        EditControlsNutzerVisibility = Visibility.Hidden;

        ViewControlsAdminVisibility = Visibility.Visible;
        ViewControlsNutzerVisibility = Visibility.Visible;

        EditButtonVisibility = Visibility.Hidden;
        SaveButtonVisibility = Visibility.Hidden;

        ProjectDetailsVisibility = Visibility.Hidden;

        LoadData();
    }

    private async Task LoadData()
    {
        try
        {
            using var connection = _connection.Create();
            Mitarbeiter = await connection.GetNutzer(CancellationToken.None);
            Projekte = await connection.GetProjekte(CancellationToken.None);
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message, "Fehler!");
        }
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

    partial void OnSelectedProjectChanged(Projekt? value)
    {
        ProjectDetailsVisibility = value is null ? Visibility.Hidden : Visibility.Visible;
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
    private async Task OnNeueBuchung()
    {
        if(SelectedProject is null)
        {
            return;
        }

        var neueBuchungView = new Views.NeueBuchungView(SelectedProject, _connection)
        {
            Topmost = true
        };
        neueBuchungView.ShowDialog();

        if (neueBuchungView.DialogResult == true)
        { 
            using var connection = _connection.Create();
            SelectedProject.Buchungen = await connection.GetBuchungen(SelectedProject.Id);
        }
    }

    [RelayCommand]
    private async Task OnAuswertung()
    {
        if (Projekte is null)
        {
            return;
        }

        try
        {
            var auswertung = new Auswertung($"./auswertung_{DateTime.Now:yyyyMMdd}.xlsx", Projekte);
            await auswertung.CreateAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Fehler beim erstellen der Auswertung!");
        }
    }
}
