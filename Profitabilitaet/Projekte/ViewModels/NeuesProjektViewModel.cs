using CommunityToolkit.Mvvm.Input;
using Profitabilitaet.Database.Connection;
using Profitabilitaet.Database.Entities;
using Profitabilitaet.Projekte.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Profitabilitaet.Projekte.ViewModels;

internal partial class NeuesProjektViewModel : ObservableObject, IDataErrorInfo
{
    private readonly DatabaseConnection _connection;
    private readonly NeuesProjektView _view;
    [ObservableProperty]
    private DateTime _beginn = DateTime.Today;
    private DateTime _ende = DateTime.Today;
    
    public string Bezeichnung { get; set; }
    public Nutzer? Leiter { get; set; }
    public decimal Auftragswert { get; set; }
    public decimal AngezahlterBetrag { get; set; }
    public DateTime Ende
    {
        get => _ende;
        set
        {
            _ende = value;
        }
    }

    public IReadOnlyList<Nutzer> Mitarbeiter { get; init; }

    public NeuesProjektViewModel(IReadOnlyList<Nutzer> mitarbeiter, DatabaseConnection connection, NeuesProjektView view)
    {
        Mitarbeiter = mitarbeiter;
        _connection = connection;
        _view = view;
    }

    private bool HasValidData()
    {
        return !string.IsNullOrEmpty(Bezeichnung) &&
               Auftragswert >= 0.0m && AngezahlterBetrag >= 0.0m && Auftragswert >= AngezahlterBetrag &&
               Ende >= Beginn;
    }

    [RelayCommand]
    private async Task OnSpeichern()
    {
        if (!HasValidData())
        {
            MessageBox.Show($"Die Projektdaten sind nicht alle korrekt.\n{GetInvalidFieldsString()}", "Falsche Projektdaten", MessageBoxButton.OK);
            return;
        }
        
        var projekt = new Projekt();
        projekt.Bezeichnung = Bezeichnung;
        projekt.Leiter = Leiter;
        projekt.Auftragswert = Auftragswert;
        projekt.AngezahlterBetrag = AngezahlterBetrag;
        projekt.Beginn = Beginn;
        projekt.Ende = Ende;
            
        _connection.Projekte.Add(projekt);
        await _connection.SaveChangesAsync();

        _view.DialogResult = true;
        _view.Close();
    }
    
    #region IDataErrorInfo

    public string Error => string.Empty;

    public string this[string columnName]
    {
        get
        {
            string result = string.Empty;

            switch (columnName)
            {
                case "Ende":
                    if (Beginn > _ende)
                    {
                        result = "Ende des Projekts muss am gleichen Tag oder nach Beginn sein!";
                    }
                    break;
                case "Auftragswert":
                    if (Auftragswert < 0.0m)
                    {
                        result = "Der Auftragswert muss größer als 0 sein!";
                    }
                    break;
                case "AngezahlterBetrag":
                    if (AngezahlterBetrag < 0.0m)
                    {
                        result = "Der AngezahlterBetrag darf nicht negativ sein!";
                    }
                    else if (AngezahlterBetrag > Auftragswert)
                    {
                        result = "Der AngezahlterBetrag darf größer als der Auftragswert sein!";
                    }
                    break;
            }

            return result;
        }
    }
    #endregion
    
    private string GetInvalidFieldsString()
    {
        List<string> fields = [];

        if (Beginn > _ende)
        {
            fields.Add("Ende des Projekts muss am gleichen Tag oder nach Beginn sein!");
        }
        if (Auftragswert < 0.0m)
        {
            fields.Add("Der Auftragswert muss größer als 0 sein!");
        }
        if (AngezahlterBetrag < 0.0m)
        {
            fields.Add( "Der AngezahlterBetrag darf nicht negativ sein!");
        }
        if (AngezahlterBetrag > Auftragswert)
        {
            fields.Add( "Der AngezahlterBetrag darf größer als der Auftragswert sein!");
        }

        return fields.Count == 0 ? string.Empty : (new StringBuilder()).AppendJoin('\n', fields).ToString();
    }
}
