using Profitabilitaet.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Profitabilitaet.Projekte.Views
{
    /// <summary>
    /// Interaktionslogik für NeueBuchungView.xaml
    /// </summary>
    public partial class NeueBuchungView : Window
    {
        public NeueBuchungView(Projekt projekt, Common.Connection connection)
        {
            InitializeComponent();
            DataContext = new ViewModels.NeueBuchungViewModel(projekt, connection, this);
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
