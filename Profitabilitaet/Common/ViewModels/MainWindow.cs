using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Profitabilitaet.Common.ViewModels
{
    internal class MainWindow
    {
        public UserControl CurrentView { get; } = new Views.MainView();
    }
}
