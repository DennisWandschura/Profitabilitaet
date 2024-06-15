using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Profitabilitaet.Database.Connection;

namespace Profitabilitaet.Database.Entities;

public readonly record struct ProjektId(int Value)
{
    public static ProjektId Empty = default;
}

public partial class Projekt : ObservableObject
{
    public ProjektId Id { get; set; }
    [ObservableProperty]
    private string _bezeichnung;
    public Nutzer? Leiter { get; set; }
    public decimal Auftragswert { get; set; }
    public decimal AngezahlterBetrag { get; set; }
    public DateTime Beginn { get; set; }
    public DateTime Ende { get; set; }
    public bool IstStorniert { get; set; }

    public ICollection<Buchung> Buchungen { get; private set; } =
            new ObservableCollection<Buchung>();

    public decimal Profitabilitaet => 
        Buchungen.Count == 0 ?
        Auftragswert : 
        Auftragswert / Buchungen.Sum(x => x.Anteil);

    public Task UpdateAsync(DatabaseConnection connection)
    {
        connection.Update(this);
        return connection.SaveChangesAsync();
    }
    
    public async Task CancelAsync(DatabaseConnection connection)
    {
        var entity = connection.Entry(this);
        
        var leiterId =(NutzerId)entity.Property("LeiterId").OriginalValue;
        
        Bezeichnung = (string)entity.Property("Bezeichnung").OriginalValue;
        Leiter = await connection.GetNutzer(leiterId);
        Auftragswert = (decimal)entity.Property("Auftragswert").OriginalValue;
        AngezahlterBetrag = (decimal)entity.Property("AngezahlterBetrag").OriginalValue;
        Beginn = (DateTime)entity.Property("Beginn").OriginalValue;
        Ende = (DateTime)entity.Property("Ende").OriginalValue;
        IstStorniert = (bool)entity.Property("IstStorniert").OriginalValue;
    }
}
