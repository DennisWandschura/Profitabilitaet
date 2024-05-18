using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Options;
using Profitabilitaet.Common.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Profitabilitaet.Config;
using Profitabilitaet.Database.Connection;
using System.Windows;

namespace Profitabilitaet.Common.ViewModels;

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
    public async Task OnLogin()
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
            MessageBox.Show("Fehler: " + ex.Message);
        }

        CanLogin = true;
    }

    static string Sha256String(string randomString)
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
