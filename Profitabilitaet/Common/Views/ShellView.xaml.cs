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

            MyViewModel.CloseAction += () =>
                {
                    this.Close();
                };
        }
    }
}
