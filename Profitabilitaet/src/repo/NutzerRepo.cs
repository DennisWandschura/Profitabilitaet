using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Profitabilitaet.src.repo{
    public class NutzerRepo{
        private List<Nutzer> nutzerliste;
        
        public NutzerRepo(){
            nutzerliste = DB.get().loadAllNutzer();
        }

        public int getMaxID(){

        }

        public Nutzer GetNutzerByID(int id){
            if (id < 0){
                throw new Exception("ID is not valid!");
            }
            foreach (var item in nutzerliste){
                if (item.id == id){
                    return item;
                }
            }
            return null;
        }
    }
}