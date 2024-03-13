using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Profitabilitaet.Common.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
