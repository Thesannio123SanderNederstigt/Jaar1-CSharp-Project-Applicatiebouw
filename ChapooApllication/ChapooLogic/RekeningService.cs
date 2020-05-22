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
                return Rekening_db.Get_All_Rekeningen();
            }
            catch(Exception)
            {
                List<Rekening> fakerekeninglist = new List<Rekening>();
                Tafel tafel = new Tafel(1, false, 1);
                Rekening fakerekening = new Rekening(1, 1, "error", true, "error", tafel);
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
                Tafel tafel = new Tafel(1, false, 1);
                Rekening fakerekening = new Rekening(1, 1, "error", true, "error", tafel);
                return fakerekening;
            }
        }

        public Rekening GetRekeningByTafelId(Tafel tafel)
        {
            try
            {
                return Rekening_db.GetByTafelId(tafel.ID);
            }
            catch(Exception)
            {
                Rekening fakerekening = new Rekening(1, 1, "error", true, "error", tafel);
                return fakerekening;
            }
        }

        public void AddNewRekening(float fooi, string betaalwijze, bool betaalStatus, string opmerking)
        {
            try
            {
                Rekening_db.AddRekening(fooi, betaalwijze, betaalStatus, opmerking);
            }
            catch(Exception)
            {
                throw new Exception($"The Rekening wasn't created because an error occurred");
            }
        }

        public void EditRekening(int ID, float fooi, string betaalwijze, bool betaalStatus, string opmerking)
        {
            try
            {
                Rekening_db.EditRekening(ID, fooi, betaalwijze, betaalStatus, opmerking);
            }
            catch(Exception)
            {
                throw new Exception($"The Rekening wasn't altered because an error occurred");
            }
        }
        
        public void DeleteRekening(int RekeningID) 
        {
            try
            {
                Rekening_db.DeleteRekening(RekeningID);
            }
            catch(Exception)
            {
                throw new Exception($"The Rekening wasn't deleted because an error occurred");
            }
        
        
        }

    }
}
