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
            catch(Exception e)
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
            catch(Exception e)
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
            catch(Exception e)
            {
                Rekening fakerekening = new Rekening(1, 1, e.ToString(), 1, true, "error");
                return fakerekening;
            }
        }

        public string AddNewRekening(float fooi, string betaalwijze, bool betaalStatus, string opmerking, int tafelID)
        {
            try
            {
                Rekening_db.AddRekening(fooi, betaalwijze, betaalStatus, opmerking, tafelID);
                return "Rekening succesvol toegevoegd!";
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
                Rekening_db.EditRekening(ID, fooi, betaalwijze, betaalStatus, opmerking);
                return "Rekening met succes gewijzigd!";
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
                Rekening_db.DeleteRekening(RekeningID);
                return "Rekening succevol verwijderd!";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public void CheckRekeningItem(int ID)
        {
            Rekening_db.CheckRekening(ID);
        }

        //public string GetTafelByID(int ID)
        //{
        //    return Rekening_db.GetTafelByID(ID);
        //}

    }
}
