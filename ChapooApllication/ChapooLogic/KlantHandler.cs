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
            try
            {
                return Klant_db.Get_All_Klanten();
            }
            catch(Exception)
            {
                List<Klant> fakeklantenlist = new List<Klant>();
                Klant fakeklant = new Klant(1, "error", 1);
                fakeklantenlist.Add(fakeklant);
                return fakeklantenlist;
            }

        }

        public Klant GetKlant(int klantID)
        {
            try
            {
                return Klant_db.GetById(klantID);
            }
            catch(Exception)
            {
                Klant fakeklant = new Klant(1, "error", 1);
                return fakeklant;
            }

        }
    }
}
