using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.Common.Models;

public class LoggedInUserChangedMessage : ValueChangedMessage<Profitabilitaet.Common.Models.Nutzer>
{
    public LoggedInUserChangedMessage(Profitabilitaet.Common.Models.Nutzer user) : base(user)
    {
    }
}
