﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Profitabilitaet.src
{
    public class Projekt
    {
        private String Name { get; }
        private String Description { get; }
        private Nutzer Leiter { get; }
        private int Id { get; }

        private Decimal Auftragswert
        {
            get => default;
            set
            {
            }
        }

        private Decimal AngezahlterBetrag
        {
            get => default;
            set
            {
            }
        }

        private DateOnly Beginn
        {
            get => default;
            set
            {
            }
        }

        private DateOnly Ende
        {
            get => default;
            set
            {
            }
        }

        private Boolean IstStorniert
        {
            get => default;
            set
            {
            }
        }
    }
}
