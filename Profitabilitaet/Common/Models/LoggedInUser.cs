using Profitabilitaet.Database.Entities;

namespace Profitabilitaet.Common.Models
{
    internal class LoggedInUser
    {
        public Nutzer Nutzer { get; set; }
        public Rolle Rolle { get => Nutzer.Rolle; }
    }
}
