using Microsoft.Extensions.DependencyInjection;
using Profitabilitaet.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Profitabilitaet;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public new static App Current => (App)Application.Current;

    public IServiceProvider Services { get; }

    public App()
    {
        Services = ConfigureServices();

        this.InitializeComponent();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Services
        services.AddSingleton<LoggedInUser>();

        // Viewmodels
        services.AddTransient<Mitarbeiter.ViewModels.MainView>();

        return services.BuildServiceProvider();
    }
}
