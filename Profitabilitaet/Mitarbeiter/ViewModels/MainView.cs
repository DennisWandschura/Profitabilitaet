using CommunityToolkit.Mvvm.Messaging;
using Profitabilitaet.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Mitarbeiter.ViewModels;

internal class MainView
{
    public List<string> Mitarbeiter { get; } = new List<string>
    {
        "Peter Ross",
        "Mitarbeiter B",
        "Mitarbeiter C",
        "Mitarbeiter D",
        "Mitarbeiter E",
    };

    private LoggedInUser _loggedInUser;

    public MainView(LoggedInUser loggedInUser)
    {
        _loggedInUser = loggedInUser;
    }
}
