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
using Profitabilitaet.Database.Connection;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Profitabilitaet.Projekte.ViewModels;

internal partial class ProjekteViewModel : ObservableObject
{
    [ObservableProperty]
    Projekt? _selectedProject;

    public ObservableCollection<Projekt>? Projekte => _connection.Projekte;
    public ObservableCollection<Nutzer>? Mitarbeiter => _connection.Nutzer;

    private readonly DatabaseConnection _connection;
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
    private bool _canEditIstStorniert = false;
    [ObservableProperty]
    private Visibility _projectDetailsVisibility;
    [ObservableProperty]
    private Visibility _newProjectVisibility;

    public ProjekteViewModel(DatabaseConnection connection, LoggedInUser loggedInUser)
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

        NewProjectVisibility = loggedInUser.Rolle != Rolle.NUTZER ? Visibility.Visible : Visibility.Collapsed;
    }

    [RelayCommand]
    private void OnEdit()
    {
        SetEditVisibilities();
    }

    [RelayCommand]
    private Task OnSave()
    {
        SetViewVisibilities();

        return SelectedProject?.UpdateAsync(_connection) ?? Task.CompletedTask;
    }

    [RelayCommand]
    private Task OnCancel()
    {
        SetViewVisibilities();

        return SelectedProject?.CancelAsync(_connection) ?? Task.CompletedTask;
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
    private void OnNeueBuchung()
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
    }

    [RelayCommand]
    private async Task OnAuswertung()
    {
        if (Projekte is null)
        {
            MessageBox.Show("Es sind keine Projekte geladen.", "Fehler beim erstellen der Auswertung!");
            return;
        }

        try
        {
            var path = $"./auswertung_{DateTime.Now:yyyyMMdd}.xlsx";
            path = Path.GetFullPath(path);

            await Auswertung.CreateAsync(path, Projekte.ToList());

            MessageBox.Show($"Die Auswertung wurde unter '{path}' gespeicher.", "Auswertung erstellt", MessageBoxButton.OK);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Fehler beim erstellen der Auswertung!");
        }
    }

    [RelayCommand]
    private async Task OnNeuesProjekt()
    {
        var nutzerListe =await  _connection.GetNutzer();
        var neueBuchungView = new Views.NeuesProjektView(nutzerListe, _connection)
        {
            Topmost = true
        };
        neueBuchungView.ShowDialog();
    }
}
