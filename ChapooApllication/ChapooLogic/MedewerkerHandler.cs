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

        public List<Medewerker> GetKlant()
        {
            return Medewerker_db.Db.Get_All_Medewerkers();
        }
    }
}
