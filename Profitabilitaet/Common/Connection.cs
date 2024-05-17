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
    public static class Connection
    {
        public static DatabaseConnection Create(IServiceProvider serviceProvider)
        {
           var databaseSettings = (IOptions<DatabaseSettings>)serviceProvider.GetService(typeof(IOptions<DatabaseSettings>));
            //IOptions<DatabaseSettings> databaseSettings;
            return MySqlConnection.Create(databaseSettings.Value);
        }
    }
}
