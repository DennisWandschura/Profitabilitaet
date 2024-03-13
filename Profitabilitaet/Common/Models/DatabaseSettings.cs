using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Common.Models
{
    internal class DatabaseSettings
    {
        public const string Name = "DatabaseSettings";
        public string Address { get; set; }
        public int Port { get; set; }
    }
}
