using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.src{
    public class Nutzer{
        private List<Rolle> rollen;
        public String vorname { get; private set; }
        public String nachname { get; private set; }
        public int plz { get; private set; }
        public String ort { get; private set; }

        public Nutzer(List<Rolle> rollen, String vorname, String nachname, int plz, String ort) {
            this.rollen = rollen;
            this.vorname = vorname;
            this.nachname = nachname;
            this.plz = plz;
            this.ort = ort;
        }

        public Nutzer() {
            this.rollen = new List<Rolle> {Rolle.BAISIS_NUTZER};
            this.vorname = "Max";
            this.nachname = "Muster";
            this.plz = 0;
            this.ort = "Musterhausen";
        }

        public Boolean hasRolle(Rolle rolle)
        {
            foreach (var item in rollen)
            {
                if (item.Equals(rolle)) {
                    return true;
                }
            }
            return false;
        }

        public void addRolle(Rolle rolle)
        {
            if (!hasRolle(rolle)) {
                rollen.Add(rolle);
            }
        }

        public Boolean removeRolle(Rolle rolle) {
            if (hasRolle(rolle))
            {
                rollen.Remove(rolle);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Nutzer getBaseUser() {
            return new Nutzer();
        }

        public static Nutzer getAdmin() {
            Nutzer admin = getBaseUser();
            admin.addRolle(Rolle.ADMIN);
            return admin;
        }

        public static Nutzer getProjektleiter() {
            Nutzer projektleiter = getBaseUser();
            projektleiter.addRolle(Rolle.PROJEKTLEITER);
            return projektleiter;
        }
    }
}
