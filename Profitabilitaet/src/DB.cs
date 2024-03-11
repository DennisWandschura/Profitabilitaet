using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.Interop;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.src{
    public class DB{
        private static DB instanz = null;

        private DB(){}

        public static DB get(){
            if ((instanz == null){
                instanz = new DB();
            }
            return instanz;
        }
    }

    public List<Nutzer> loadAllNutzer(){
        
    }

    public List<Projekt> getAllProjekte(){

    }
}