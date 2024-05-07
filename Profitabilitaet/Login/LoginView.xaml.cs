using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Profitabilitaet.Common.Views;

/// <summary>
/// Interaction logic for LoginView.xaml
/// </summary>
public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<ViewModels.LoginViewModel>();
    }
}
