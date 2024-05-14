using Microsoft.Extensions.Options;
using Profitabilitaet.Config;
using Profitabilitaet.Database.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Common
{
    public class Connection(IOptions<DatabaseSettings> databaseSettings)
    {
        private readonly DatabaseSettings _databaseSettings = databaseSettings.Value;

        public DatabaseConnection Create()
        {
            return MySqlConnection.Create(_databaseSettings);
        }
    }
}
