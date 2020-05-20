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

        public List<Reservering> GetReserveringen()
        {
            try
            {
                return Reservering_db.Get_all_Reserveringen();
            }
            catch(Exception)
            {
                List<Reservering> fakeReserveringen = new List<Reservering>();
                Reservering fakeReservering = new Reservering(1, DateTime.Now, 1, 1);
                fakeReserveringen.Add(fakeReservering);

                return fakeReserveringen;
            }

        }

        public Reservering GetSingleReservering(int ReserveringID)
        {
            try
            {
                return Reservering_db.GetById(ReserveringID);
            }
            catch(Exception)
            {
                Reservering fakereservering = new Reservering(1, DateTime.Now, 1, 1);
                return fakereservering;
            }
        }
    }
}
