using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profitabilitaet.Common.Models;
using System;
using System.IO;
using System.Windows;
using ProfitabilitaetBackend.Config;

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

        // Viewmodels
        services.AddTransient<Mitarbeiter.ViewModels.MainView>();
        services.AddTransient<Common.ViewModels.LoginViewModel>();

        return services.BuildServiceProvider();
    }
}
