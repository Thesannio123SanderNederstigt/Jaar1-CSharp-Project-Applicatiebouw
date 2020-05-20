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
    public class MedewerkerHandler
    {
        private readonly MedewerkerDAO Medewerker_db = new MedewerkerDAO();

        public List<Medewerker> GetMedewerkers()
        {
            try
            {
                return Medewerker_db.Get_All_Medewerkers();
            }
            catch(Exception)
            {
                List<Medewerker> fakemedewerkerlist = new List<Medewerker>();
                Medewerker fakemedewerker = new Medewerker(1, "error", "something went wrong", "oh no", 1, 1);
                return fakemedewerkerlist;
            }

        }

        public Medewerker GetMedewerker(int medewerkerID)
        {
            try
            {
                return Medewerker_db.GetById(medewerkerID);
            }
            catch(Exception)
            {
                Medewerker fakemedewerker = new Medewerker(1, "error", "something went wrong", "oh no", 1, 1);
                return fakemedewerker;
            }

        }
    }
}
