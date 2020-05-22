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
    public class BestellingService
    {
        private readonly BestellingDAO Bestelling_db = new BestellingDAO();

        public List<Bestelling> GetBestellingen()
        {
            try
            {
                return Bestelling_db.Get_All_Bestellingen();
            }
            catch(Exception)
            {
                List<Bestelling> fakeBestellingLijst = new List<Bestelling>();
                Bestelling fakeBestelling = new Bestelling(1, DateTime.Now, true, 1, 1, "error");
                fakeBestellingLijst.Add(fakeBestelling);
                return fakeBestellingLijst;
            }

        }

        public Bestelling GetSingleBestelling(int bestellingID)
        {
            try
            {
                return Bestelling_db.GetById(bestellingID);
            }
            catch (Exception)
            {
                Bestelling fakebestelling = new Bestelling(1, DateTime.Now, false, 1, 1, "error");
                return fakebestelling;
            }
        }

        public void AddNewBestelling(DateTime besteltijd, bool status, int tafelID, int rekeningID, string opmerking)
        {
            try
            {
                Bestelling_db.AddBestelling(besteltijd, status, tafelID, rekeningID, opmerking);
            }
            catch (Exception)
            {
                throw new Exception($"No bestelling was added :(");
            }
        }

        public void EditBestelling(int BestellingID, DateTime besteltijd, bool status, int tafelID, int rekeningID, string opmerking)
        {
            try
            {
                Bestelling_db.EditBestelling(BestellingID, besteltijd, status, tafelID, rekeningID, opmerking);
            }
            catch(Exception)
            {
                throw new Exception($"The bestelling wasn't altered because an error occurred");
            }
        }

        public void DeleteBestelling(int BestellingID)
        {
            try
            {
                Bestelling_db.DeleteBestelling(BestellingID);
            }
            catch(Exception)
            {
                throw new Exception($"The bestelling could not be deleted :(");
            }
        }
    }
}