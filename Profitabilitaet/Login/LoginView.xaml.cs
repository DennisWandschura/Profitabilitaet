using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Profitabilitaet.Common.Views;

/// <summary>
/// Interaction logic for LoginView.xaml
/// </summary>
public partial class LoginView : UserControl
{
    private readonly ViewModels.LoginViewModel _dataContext;
    public LoginView()
    {
        InitializeComponent();
        _dataContext = App.Current.Services.GetService<ViewModels.LoginViewModel>();
        DataContext = _dataContext;
    }

    private async void Login_PreviewEnterDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if(e.Key == System.Windows.Input.Key.Enter)
        {
            await _dataContext.LoginCommand.ExecuteAsync(null);
            e.Handled = true;
        }
    }
}
