using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Common.Models;

public class LoggedInUserChangedMessage : ValueChangedMessage<Database.Nutzer>
{
    public LoggedInUserChangedMessage(Database.Nutzer user) : base(user)
    {
    }
}
