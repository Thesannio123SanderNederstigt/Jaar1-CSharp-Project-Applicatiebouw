using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ChapooDAL;
using ChapooModel;

namespace ChapooLogic
{
   public  class KlantHandler
   {
        KlantDAO Klant_db = new KlantDAO();

        public List<Klant> GetKlanten()
        {
            return Klant_db.Get_All_Klanten();
        }

        public Klant GetKlant(int klantID)
        {
            return Klant_db.GetById(klantID);
        }
    }
}
