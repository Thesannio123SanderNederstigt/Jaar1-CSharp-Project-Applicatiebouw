using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChapooDAL;
using ChapooModel;

namespace ChapooLogic
{
    public class RekeningService
    {
        private readonly RekeningDAO Rekening_db = new RekeningDAO();

        public List<Rekening> GetRekeningen()
        {
            try
            {
                List<Rekening> rekeninglijst = Rekening_db.Get_All_Rekeningen();

                if(rekeninglijst == null)
                {
                    throw new ArgumentNullException();
                }
                return rekeninglijst;
            }
            catch(ArgumentNullException e)
            {
                List<Rekening> fakerekeninglist = new List<Rekening>();
                Rekening fakerekening = new Rekening(1, 1, e.ToString(), 1, true, "error");
                fakerekeninglist.Add(fakerekening);
                return fakerekeninglist;
            }

        }

        public Rekening GetSingleRekening(int RekeningID)
        {
            try
            {
                Rekening rekening = Rekening_db.GetById(RekeningID);

                if(rekening == null)
                {
                    throw new ArgumentNullException();
                }
                return rekening;
            }
            catch(ArgumentNullException e)
            {
                Rekening fakerekening = new Rekening(1, 1, e.ToString(), 1, true, "error");
                return fakerekening;
            }
        }

        public Rekening GetRekeningByTafelId(Tafel tafel)
        {
            try
            {
                Rekening rekening = Rekening_db.GetByTafelId(tafel.ID);

                if(rekening == null)
                {
                    throw new ArgumentNullException();
                }
                return rekening;
            }
            catch(ArgumentNullException e)
            {
                Rekening fakerekening = new Rekening(1, 1, e.ToString(), 1, true, "error");
                return fakerekening;
            }
        }

        public string AddNewRekening(float fooi, string betaalwijze, bool betaalStatus, string opmerking, int tafelID)
        {
            try
            {
                return Rekening_db.AddRekening(fooi, betaalwijze, betaalStatus, opmerking, tafelID);
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string EditRekening(int ID, float fooi, string betaalwijze, bool betaalStatus, string opmerking)
        {
            try
            {
                return Rekening_db.EditRekening(ID, fooi, betaalwijze, betaalStatus, opmerking);
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }
        
        public string DeleteRekening(int RekeningID) 
        {
            try
            {
                return Rekening_db.DeleteRekening(RekeningID);
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        
        
        }

    }
}
