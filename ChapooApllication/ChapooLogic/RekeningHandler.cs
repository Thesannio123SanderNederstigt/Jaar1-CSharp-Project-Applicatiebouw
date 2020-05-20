using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChapooDAL;
using ChapooModel;

namespace ChapooLogic
{
    public class RekeningHandler
    {
        private readonly RekeningDAO Rekening_db = new RekeningDAO();

        public List<Rekening> GetRekeningen()
        {
            try
            {
                return Rekening_db.Get_All_Rekeningen();
            }
            catch(Exception)
            {
                List<Rekening> fakerekeninglist = new List<Rekening>();
                Rekening fakerekening = new Rekening(1, 1, "error", 11, true);
                fakerekeninglist.Add(fakerekening);
                return fakerekeninglist;
            }

        }

        public Rekening GetSingleRekening(int RekeningID)
        {
            try
            {
                return Rekening_db.GetById(RekeningID);
            }
            catch(Exception)
            {
                Rekening fakerekening = new Rekening(1, 1, "error", 11, true);
                return fakerekening;
            }
        }

        

    }
}
