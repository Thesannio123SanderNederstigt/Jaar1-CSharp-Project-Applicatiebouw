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
        MedewerkerDAO Medewerker_db = new MedewerkerDAO();

        public List<Medewerker> GetMedewerkers()
        {
            return Medewerker_db.Get_All_Medewerkers();
        }

        public Medewerker GetMedewerker(int medewerkerID)
        {
            return Medewerker_db.GetById(medewerkerID);
        }
    }
}
