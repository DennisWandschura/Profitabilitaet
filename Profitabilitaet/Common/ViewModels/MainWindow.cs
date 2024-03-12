using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Profitabilitaet.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Profitabilitaet.Common.ViewModels;

internal partial class MainWindow : ObservableObject
{
    [ObservableProperty]
    private UserControl _currentView = new Views.LoginView();

    public MainWindow()
    {
        WeakReferenceMessenger.Default.Register<LoggedInUserChangedMessage>(this, OnLoggedInUserChangedMessage);
    }

    private void OnLoggedInUserChangedMessage(object recipient, LoggedInUserChangedMessage message)
    {
        CurrentView = new Views.MainView();
    }
}
