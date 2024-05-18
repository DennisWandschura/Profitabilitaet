using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Profitabilitaet.Common.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Profitabilitaet.Common.ViewModels;

internal partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private UserControl _currentView = new Views.LoginView();
    [ObservableProperty]
    private Visibility _logoutVisibility;
    private readonly LoggedInUser _loggedInUser;

    public Action? CloseAction { get; set; }

    public ShellViewModel(LoggedInUser loggedInUser)
    {
        LogoutVisibility = Visibility.Hidden;
        WeakReferenceMessenger.Default.Register<LoggedInUserChangedMessage>(this, OnLoggedInUserChangedMessage);
        this._loggedInUser = loggedInUser;
    }

    private void OnLoggedInUserChangedMessage(object recipient, LoggedInUserChangedMessage message)
    {
        if(message.Value is not null)
        {
            LogoutVisibility = Visibility.Visible;
            CurrentView = new Views.MainView();
        }
        else
        {
            LogoutVisibility = Visibility.Hidden;
            CurrentView = new Views.LoginView();
        }
    }

    [RelayCommand]
    private void Close()
    {
        CloseAction?.Invoke();
    }

    [RelayCommand]
    private void Logout()
    {
        _loggedInUser.Nutzer = null;
    }
}
