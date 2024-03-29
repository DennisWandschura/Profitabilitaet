using CommunityToolkit.Mvvm.Messaging.Messages;
using ProfitabilitaetBackend;

namespace Profitabilitaet.Common.Models;

public class LoggedInUserChangedMessage : ValueChangedMessage<Nutzer>
{
    public LoggedInUserChangedMessage(Nutzer user) : base(user)
    {
    }
}
