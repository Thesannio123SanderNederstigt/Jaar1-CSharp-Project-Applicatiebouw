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
    public class BestellingHandler
    {
        private readonly BestellingDAO Bestelling_db = new BestellingDAO();
        private readonly TafelDAO Tafel_db = new TafelDAO();
        private readonly RekeningDAO Rekening_db = new RekeningDAO();
        private readonly MedewerkerDAO Medewerker_db = new MedewerkerDAO();
        private readonly MenuItemDAO MenuItem_db = new MenuItemDAO();


        public List<Bestelling> GetBestellingen()
        {
            try
            {
                return Bestelling_db.Get_All_Bestellingen();
            }
            catch(Exception)
            {
                List<Bestelling> fakeBestellingLijst = new List<Bestelling>();
                Bestelling fakeBestelling = new Bestelling(1,  1, DateTime.Now, true, 1, 1, 1, 1);
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
                Bestelling fakebestelling = new Bestelling(1, 1, DateTime.Now, false, 1, 1, 1, 1);
                return fakebestelling;
            }
        }

        //public int AddNewBestelling(int aantal, DateTime besteltijd, int tafelID, int rekeningID, int medewerkerID, int menuItemID)
        //{
        //    try
        //    {
        //        Rekening bestaanderekening = Rekening_db.GetByTafelId(tafelID);
        //        int rekeningnummer;
                                
        //        if(bestaanderekening == null)
        //        {
        //            Rekening_db.AddNewRekening(fooi, betaalwijze, tafelID, betaalstatus);
        //            rekeningnummer = Rekening_db.GetNewlyAddedRekening(tafelID);

        //            return Bestelling_db.AddBestelling(aantal, besteltijd, tafelID, rekeningnummer, medewerkerID, menuItemID);
        //        }
        //        else
        //        {
        //            rekeningnummer = rekeningID;
        //        }

        //        return Bestelling_db.AddBestelling(aantal, besteltijd, tafelID, rekeningnummer, medewerkerID, menuItemID);
        //    }
        //    catch
        //    {
        //        int unaltered = 0;
        //        return unaltered;
        //    }
        //}
        

        public Tafel GetSingleTafel(int tafelID)
        {
            try
            {
                return Tafel_db.GetById(tafelID);
            }
            catch(Exception)
            {
                Tafel faketafel = new Tafel(11, true, 1);
                return faketafel;
            }

        }

        public MenuItem GetSingleMenuItem(int menuItemID)
        {
            try
            {
                return MenuItem_db.GetById(menuItemID);
            }
            catch(Exception)
            {
                MenuItem fakemenuitem = new MenuItem(1,  1,  "Fake news!");
                return fakemenuitem;
            }

        }

        public Rekening GetSingleRekening(int rekeningID)
        {
            try
            {
                return Rekening_db.GetById(rekeningID);
            }
            catch(Exception)
            {
                Rekening fakerekening = new Rekening(1,1, "error", 11, false);
                return fakerekening;
            }

        }

        public Medewerker GetSingleMedewerker(int medewerkerID)
        {
            try
            {
                return Medewerker_db.GetById(medewerkerID);
            }
            catch(Exception)
            {
                Medewerker fakemedewerker = new Medewerker(1, "error", "something went wrong", "oh no", 1, 1);
                return fakemedewerker;
            }

        }
    }
}