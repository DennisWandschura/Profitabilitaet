using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Profitabilitaet.Database;
using Profitabilitaet.Database.Connection;
using Profitabilitaet.Database.Entities;
using Profitabilitaet.Projekte.Views;

namespace Profitabilitaet.Projekte.ViewModels;

internal partial class NeueBuchungViewModel : ObservableObject
{
    private const int MaxWochenAnteil = 40;

    public Database.Entities.Projekt Projekt { get; init; }

    [ObservableProperty]
    private Nutzer? _selectedMitarbeiter;

    [ObservableProperty]
    private IReadOnlyList<Nutzer>? _mitarbeiterListe;

    [ObservableProperty]
    private int _anteil;
    [ObservableProperty]
    private int _jahr;
    [ObservableProperty]
    private int _woche;

    private readonly DatabaseConnection _connection;
    private IReadOnlyList<Buchung>? _nutzerBuchungen;

    public int[] Anteile { get; }

    [ObservableProperty]
    private int[] _wochen;
    public int[] Jahre { get; }
    private readonly int _currentYear;
    private readonly NeueBuchungView _view;

    public NeueBuchungViewModel(Projekt projekt, DatabaseConnection connection, NeueBuchungView view)
    {
        Projekt = projekt;
        _connection = connection;

        Anteile = Enumerable.Range(1, 100).ToArray();
        Anteil = 1;

        var today = DateTime.Today;
        _currentYear = today.Year;

        Jahre = [today.Year, today.Year + 1];
        Jahr = _currentYear;

        //var currentWeek = GetWeekOfYear(DateTime.Today);
        //var numberOfWeeks = GetNumberOfWeeks(_currentYear);
        //var wochen = Enumerable.Range(currentWeek, GetNumberOfWeeks(_currentYear) - currentWeek).ToArray();
        //Wochen = wochen;
        //Woche = currentWeek;

        _view = view;

        GetMitarbeiterListe();
    }

    private async Task GetMitarbeiterListe()
    {
        MitarbeiterListe = await _connection.GetNutzer();
    }

    partial void OnSelectedMitarbeiterChanged(Nutzer? value)
    {
        GetNutzerBuchungen(value);
    }

    partial void OnJahrChanged(int value)
    {
        
        if (value == _currentYear)
        {
            var currentWeek = GetWeekOfYear(DateTime.Today);
            var wochen = Enumerable.Range(currentWeek, GetNumberOfWeeks(value) - currentWeek).ToArray();

            Wochen = wochen;
            Woche = currentWeek;
        }
        else
        {
            Wochen = Enumerable.Range(1, GetNumberOfWeeks(value)).ToArray();
            Woche = 1;
        }
    }

    private async Task GetNutzerBuchungen(Nutzer? value)
    {
        _nutzerBuchungen = await _connection.GetBuchungen(value.Id);
    }

    [RelayCommand]
    private async Task OnSpeichern()
    {
        if(SelectedMitarbeiter is null)
        {
            MessageBox.Show("Bitte wählen Sie einen Mitarbeiter aus!", "Fehler beim Buchen");
            return;
        }

        if (_nutzerBuchungen is null)
        {
            MessageBox.Show("Fehler beim Buchen");
            return;
        }

        var validationResult = IsBuchungValid();
        if (!validationResult.Item1)
        {
            MessageBox.Show(validationResult.Item2, "Fehler beim Buchen");
            _view.DialogResult = false;
        }
        else
        {
            var buchung = new Buchung
            {
                Anteil = this.Anteil,
                Jahr = this.Jahr,
                Mitarbeiter = SelectedMitarbeiter,
                Woche = this.Woche,
                Projekt = this.Projekt
            };
            Projekt.Buchungen.Add(buchung);
            await _connection.SaveChangesAsync();

            _view.DialogResult = true;
            _view.Close();
        }
    }

    private (bool, string) IsBuchungValid()
    {
        //Der Arbeitszeitanteil beträgt mindestens eine, maximal 40 Arbeitsstunden pro Woche
        var currentAnteil = _nutzerBuchungen.Where(x => x.Jahr == Jahr && x.Woche == Woche).Sum(x => x.Anteil);
        if(currentAnteil > MaxWochenAnteil)
        {
            return (false, $"Mitarbeiter darf nur maximal 40 Stunden arbeiten, mit der aktuellen Buchung wären es aber {currentAnteil}!");
        }

        return (true, string.Empty);
    }

    private static int GetWeekOfYear(DateTime date)
    {
        // Gets the Calendar instance associated with a CultureInfo.
        CultureInfo myCI = CultureInfo.CurrentCulture;
        Calendar myCal = myCI.Calendar;

        // Gets the DTFI properties required by GetWeekOfYear.
        CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
        DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

        return myCal.GetWeekOfYear(date, myCWR, myFirstDOW);
    }

    private static int GetNumberOfWeeks(int year)
    {
        // Gets the Calendar instance associated with a CultureInfo.
        CultureInfo myCI = CultureInfo.CurrentCulture;
        Calendar myCal = myCI.Calendar;

        // Gets the DTFI properties required by GetWeekOfYear.
        CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
        DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

        // Displays the total number of weeks in the current year.
        DateTime LastDay = new System.DateTime(year, 12, 31);
        return myCal.GetWeekOfYear(LastDay, myCWR, myFirstDOW) + 1;
    }
}
