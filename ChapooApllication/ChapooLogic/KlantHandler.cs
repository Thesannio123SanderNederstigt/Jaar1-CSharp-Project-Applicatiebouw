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

        public List<Klant> GetKlant()
        {
            return Klant_db.Db.Get_All_Klanten();
        }
   }
}
