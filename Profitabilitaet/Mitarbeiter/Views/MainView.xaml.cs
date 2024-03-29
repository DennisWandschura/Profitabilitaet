using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Profitabilitaet.Mitarbeiter.Views;

/// <summary>
/// Interaktionslogik für MainView.xaml
/// </summary>
public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<ViewModels.MainView>();
    }
}
