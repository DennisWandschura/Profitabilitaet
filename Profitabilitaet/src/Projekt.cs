using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.src
{
    public class Projekt{
        public String name { get; }
        public String description { get; }
        public Nutzer leiter { get; }

        private List<Nutzer> mitarbeiter;
        public int id {get; }

        public Projekt(string name, string description, Nutzer leiter){
            this.name = name;
            this.description = description;
            this.leiter = leiter;
            this.mitarbeiter = new List<Nutzer>();
            this.id = -1;
        }

        public Projekt(string name, string description, Nutzer leiter, int id){
            this.name = name;
            this.description = description;
            this.leiter = leiter;
            this.mitarbeiter = new List<Nutzer>();
            this.id = id;
        }
    }
}
