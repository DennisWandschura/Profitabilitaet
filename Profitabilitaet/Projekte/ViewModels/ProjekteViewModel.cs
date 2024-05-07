using Profitabilitaet.Common.Models;
using System;
using System.Collections.Generic;
using Profitabilitaet.Database.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using Profitabilitaet.Common;
using System.Threading;

namespace Profitabilitaet.Projekte.ViewModels;

internal partial class ProjekteViewModel : ObservableObject
{
    [ObservableProperty]
    Database.Entities.Projekt? _selectedProject;

    [ObservableProperty]
    IReadOnlyList<Database.Entities.Projekt>? _projekte;
    private readonly Connection _connection;

    public ProjekteViewModel(Common.Connection connection)
    {
        this._connection = connection;
        LoadProjects();
    }

    private async Task LoadProjects()
    {
        Projekte = await _connection.Create().GetProjekte(CancellationToken.None);
    }

    partial void OnSelectedProjectChanged(Database.Entities.Projekt? value)
    {

    }
}
