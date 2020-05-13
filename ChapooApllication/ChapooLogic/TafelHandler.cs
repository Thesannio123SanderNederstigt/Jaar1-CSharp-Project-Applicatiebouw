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
    public class TafelHandler
    {
        TafelDAO Tafel_db = new TafelDAO();

        public List<Tafel> GetTafel()
        {
            return Tafel_db.Db.Get_All_Tafels();
        }
    }
}
