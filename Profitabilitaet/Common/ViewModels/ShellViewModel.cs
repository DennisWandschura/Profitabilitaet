using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Profitabilitaet.Common.Models;
using System;
using System.Windows.Controls;

namespace Profitabilitaet.Common.ViewModels;

internal partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private UserControl _currentView = new Views.LoginView();

    public Action? CloseAction { get; set; }

    public ShellViewModel()
    {
        WeakReferenceMessenger.Default.Register<LoggedInUserChangedMessage>(this, OnLoggedInUserChangedMessage);
    }

    private void OnLoggedInUserChangedMessage(object recipient, LoggedInUserChangedMessage message)
    {
        CurrentView = new Views.MainView();
    }

    [RelayCommand]
    private void Close()
    {
        CloseAction?.Invoke();
    }
}
