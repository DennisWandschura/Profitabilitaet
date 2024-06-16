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
    public Geschlecht[] Geschlechter { get; } = [Geschlecht.Maennlich, Geschlecht.Weiblich, Geschlecht.Divers];
    public Rolle[] Rollen { get; } = [Rolle.NUTZER, Rolle.ABTEILUNGSLEITER, Rolle.ADMIN];

    public ObservableCollection<Nutzer>? Mitarbeiter { get; init; }

    [ObservableProperty]
    private Nutzer? _selectedUser;

    private readonly LoggedInUser _loggedInUser;
    private readonly DatabaseConnection _connection;

    public Visibility EditVisibility { get; init; }
    [ObservableProperty]
    private Visibility _editButtonVisibility;
    [ObservableProperty]
    private Visibility _saveButtonVisibility;
    [ObservableProperty]
    private Visibility _editControlsVisibility;
    [ObservableProperty]
    private Visibility _viewControlsVisibility;
    [ObservableProperty]
    private Visibility _newUserButtonVisibility;
    [ObservableProperty]
    private Visibility _selectedUserViewVisibility;


    public MainViewViewModel(LoggedInUser loggedInUser, DatabaseConnection connection)
    {
        _loggedInUser = loggedInUser;
        _connection = connection;

        EditVisibility = GetVisibilityEditView(_loggedInUser.Rolle);
        EditButtonVisibility = Visibility.Visible;
        SaveButtonVisibility = Visibility.Hidden;
        EditControlsVisibility = Visibility.Hidden;
        ViewControlsVisibility = Visibility.Visible;
        SelectedUserViewVisibility = Visibility.Hidden;

        Mitarbeiter = connection.Nutzer;
    }

    partial void OnSelectedUserChanged(Nutzer? value)
    {
        SelectedUserViewVisibility = value is not null ?
            Visibility.Visible : Visibility.Hidden;
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

    [RelayCommand]
    private void OnEdit()
    {
        ShowEditControls();
    }

    [RelayCommand]
    private async Task OnSave()
    {
        HideEditControls();

        if (SelectedUser is not null)
        {
            await SelectedUser.UpdateAsync(_connection);
        }
    }

    [RelayCommand]
    private void OnCancel()
    {
        HideEditControls();
        
        SelectedUser?.Cancel(_connection);
    }

    [RelayCommand]
    private void OnNewUser()
    {
    }
    
    private void ShowEditControls()
    {
        EditControlsVisibility = Visibility.Visible;
        ViewControlsVisibility = Visibility.Hidden;

        EditButtonVisibility = Visibility.Hidden;
        SaveButtonVisibility = Visibility.Visible;
    }

    private void HideEditControls()
    {
        EditControlsVisibility = Visibility.Hidden;
        ViewControlsVisibility = Visibility.Visible;

        EditButtonVisibility = Visibility.Visible;
        SaveButtonVisibility = Visibility.Hidden;
    }
}
