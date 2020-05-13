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
    public class ReserveringHandler
    {
        ReserveringDAO Reservering_db = new ReserveringDAO();

        public List<Reservering> GetReservering()
        {
            return Reservering_db.Db.Get_All_Reserveringen();
        }
    }
}
