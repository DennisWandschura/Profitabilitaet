using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Profitabilitaet.Common.Models;

public class LoggedInUserChangedMessage : ValueChangedMessage<Database.Entities.Nutzer?>
{
    public LoggedInUserChangedMessage(Database.Entities.Nutzer? user) : base(user)
    {
    }
}
