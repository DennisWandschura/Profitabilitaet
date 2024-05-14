using CommunityToolkit.Mvvm.Messaging;
using Google.Protobuf.WellKnownTypes;
using Profitabilitaet.Database.Entities;

namespace Profitabilitaet.Common.Models
{
    internal class LoggedInUser
    {
        public Database.Entities.Nutzer? Nutzer 
        {
            get => _nutzer;
            set
            {
                _nutzer = value;
                WeakReferenceMessenger.Default.Send(new LoggedInUserChangedMessage(value));
            }
        }
        public Rolle Rolle { get => Nutzer.Rolle; }
        private Database.Entities.Nutzer? _nutzer;

    }
}
