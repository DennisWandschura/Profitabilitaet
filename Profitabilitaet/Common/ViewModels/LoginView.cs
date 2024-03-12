using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Profitabilitaet.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Profitabilitaet.Common.ViewModels
{
    internal partial class LoginView : ObservableObject
    {
        public string Login { get; set; } = string.Empty;
        public PasswordBox PasswordBox { get; set; } = new();
        private readonly LoggedInUser _loggedInUser;

        public LoginView(LoggedInUser loggedInUser)
        {
            _loggedInUser = loggedInUser;
            PasswordBox.PasswordChar = '*';
        }

        [RelayCommand]
        public void OnLogin()
        {
            var password = PasswordBox.Password;
            Nutzer nutzer = new Nutzer
            {
                Rolle = Rolle.ADMIN,
                Vorname = "Tom",
                Nachname = "Schneider"
            };

            WeakReferenceMessenger.Default.Send(new LoggedInUserChangedMessage(nutzer));
        }
    }
}
