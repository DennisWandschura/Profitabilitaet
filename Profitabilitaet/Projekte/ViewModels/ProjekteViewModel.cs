using Profitabilitaet.Common.Models;
using System;
using System.Collections.Generic;
using ProfitabilitaetBackend.Entities;

namespace Profitabilitaet.Projekte.ViewModels
{
    internal class ProjekteViewModel
    {
        public string Name { get; } = "Projekt A";
        public string Description { get; } = "Eine tolle Beschreibung des Projekts.";
        public Nutzer? Leiter { get; } = null;
        public decimal Auftragswert { get; } = 1500.0M;
        public decimal AngezahlterBetrag { get; } = 500.95M;
        public DateOnly Beginn { get; } = new DateOnly(2024, 01, 01);
        public DateOnly Ende { get; } = new DateOnly(2024, 02, 01);
        public bool IstStorniert { get; set; } = false;
        public List<BuchungArbeitszeit> Buchungen { get; } = new();

        public ProjekteViewModel()
        {
        }
    }
}
