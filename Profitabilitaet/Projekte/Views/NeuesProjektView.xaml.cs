using Profitabilitaet.Database.Connection;
using Profitabilitaet.Database.Entities;
using Profitabilitaet.Projekte.ViewModels;
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

namespace Profitabilitaet.Projekte.Views;

/// <summary>
/// Interaction logic for NeuesProjektView.xaml
/// </summary>
public partial class NeuesProjektView : Window
{
    public NeuesProjektView(IReadOnlyList<Nutzer> mitarbeiter, DatabaseConnection connection)
    {
        InitializeComponent();
        DataContext = new NeuesProjektViewModel(mitarbeiter, connection, this);
    }

    private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
    {
        DialogResult = false;
        this.Close();
    }
}
