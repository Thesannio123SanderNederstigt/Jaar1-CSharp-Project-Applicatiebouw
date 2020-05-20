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
        private WerkstationDAO Werkstation_db = new WerkstationDAO();

        public List<Werkstation> GetWerkstations()
        {
            return Werkstation_db.Get_All_Werkstations();
        }

        public Werkstation GetWerkstation(int werkstationID)
        {
            return Werkstation_db.GetById(werkstationID);
        }
    }
}
