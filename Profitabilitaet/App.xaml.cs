using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profitabilitaet.Common.Models;
using System;
using System.IO;
using System.Windows;
using Profitabilitaet.Config;

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

    private static IConfiguration AddConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("./appsettings.json");

#if DEBUG
        builder.AddJsonFile("./appsettings.development.json", true, true);
#else
#endif

        return builder.Build();
    }

    private static IServiceProvider ConfigureServices()
    {
        var configuration = AddConfiguration();

        var services = new ServiceCollection();

        services.Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.Name));

        // Services
        services.AddSingleton<LoggedInUser>();
        services.AddSingleton<Common.Connection>();

        // Viewmodels
        services.AddTransient<Mitarbeiter.ViewModels.MainViewViewModel>();
        services.AddTransient<Common.ViewModels.LoginViewModel>();
        services.AddTransient<Projekte.ViewModels.ProjekteViewModel>();

        return services.BuildServiceProvider();
    }
}
