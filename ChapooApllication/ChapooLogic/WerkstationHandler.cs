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
    public class WerkstationHandler
    {
        WerkstationDAO Werkstation_db = new WerkstationDAO();

        public List<Werkstation> GetWerkstation()
        {
            return Werkstation_db.Db.Get_All_Werkstations();
        }
    }
}
