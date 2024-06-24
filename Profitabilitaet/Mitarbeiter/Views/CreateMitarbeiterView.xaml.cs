using System.Windows;
using System.Windows.Input;
using Profitabilitaet.Database.Connection;
using Profitabilitaet.Mitarbeiter.ViewModels;

namespace Profitabilitaet.Mitarbeiter.Views
{
    /// <summary>
    /// Interaktionslogik für CreateMitarbeiter.xaml
    /// </summary>
    public partial class CreateMitarbeiterView : Window
    {
        public CreateMitarbeiterView(DatabaseConnection connection)
        {
            InitializeComponent();
            DataContext = new CreateMitarbeiterViewModel(connection, this);
        }
        
        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
