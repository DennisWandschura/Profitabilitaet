using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Profitabilitaet.Mitarbeiter.Views
{
    /// <summary>
    /// Interaktionslogik für DisplayMitarbeiter.xaml
    /// </summary>
    public partial class DisplayMitarbeiter : UserControl
    {
        public DisplayMitarbeiter()
        {
            InitializeComponent();
            //DataContext = App.Current.Services.GetService<ViewModels.DisplayMitarbeiterViewModel>();
        }
    }
}
