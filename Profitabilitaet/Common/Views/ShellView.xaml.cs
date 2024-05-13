using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Profitabilitaet.Common.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
            var viewModel = App.Current.Services.GetService<ViewModels.ShellViewModel>();

            viewModel.CloseAction += () =>
                {
                    this.Close();
                };

            DataContext = viewModel;
        }
    }
}
