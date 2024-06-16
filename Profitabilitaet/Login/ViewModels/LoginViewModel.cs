using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Profitabilitaet.Common.Models;
using Profitabilitaet.Database.Connection;

namespace Profitabilitaet.Login.ViewModels;

internal partial class LoginViewModel : ObservableObject
{
    public string BenutzerName { get; set; } = string.Empty;
    public PasswordBox PasswordBox { get; set; } = new();
    private readonly LoggedInUser _loggedInUser;
    private readonly DatabaseConnection _connection;
    [ObservableProperty]
    private bool _canLogin = true;
    [ObservableProperty]
    private string _loginStatusText = string.Empty;

    public LoginViewModel(LoggedInUser loggedInUser, DatabaseConnection connection)
    {
        _loggedInUser = loggedInUser;
        PasswordBox.PasswordChar = '*';
        _connection = connection;
    }

    [RelayCommand]
    private async Task OnLogin()
    {
        CanLogin = false;
        LoginStatusText = "Einloggen...";
        var password = Sha256String(PasswordBox.Password);

        try
        {
            var dbNutzer = await _connection.GetNutzer(BenutzerName, password);
            if (dbNutzer is not null)
            {
                await _connection.OnLogin();
                _loggedInUser.Nutzer = dbNutzer;
            }
            else
            {
                LoginStatusText = "Unbekannter Benutzer";
            }
        }
        catch (Exception ex)
        {
            LoginStatusText = string.Empty;
            MessageBox.Show("Fehler: " + ex.Message, "Fehler beim Einloggen");
        }

        CanLogin = true;
    }

    private static string Sha256String(string randomString)
    {
        using var crypt = SHA256.Create();
        var hash = new System.Text.StringBuilder();
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
        foreach (byte theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }
        return hash.ToString();
    }
}
