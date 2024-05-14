using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Profitabilitaet.Projekte.Views
{
    /// <summary>
    /// Interaktionslogik für Projekte.xaml
    /// </summary>
    public partial class ProjekteView : UserControl
    {
        public ProjekteView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<ViewModels.ProjekteViewModel>();
        }
    }
}
