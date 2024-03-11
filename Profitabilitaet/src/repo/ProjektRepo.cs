using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profitabilitaet.src.repo{
    public class ProjektRepo{
        private List<Projekt> projektliste;
        public ProjektRepo(){
            projektliste = DB.get().getAllProjekte();
        }

        public int getMaxID(){
            
        }

        public Projekt GetProjektByID(int id){
            if (id < 0){
                throw new Exception("ID is not valid!");
            }
            foreach (var item in projektliste){
                if (item.id == id){
                    return item;
                }
            }
            return null;
        }
    }
}