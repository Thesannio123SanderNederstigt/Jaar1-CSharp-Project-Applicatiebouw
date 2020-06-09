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
                List<Bestelling> bestellingslijst = Bestelling_db.Get_All_Bestellingen();

                if(bestellingslijst == null)
                {
                    throw new ArgumentNullException();
                }
                return bestellingslijst;
            }
            catch(Exception e)
            {
                List<Bestelling> fakeBestellingLijst = new List<Bestelling>();
                Bestelling fakeBestelling = new Bestelling(1, DateTime.Now, true, 1, e.ToString());
                fakeBestellingLijst.Add(fakeBestelling);
                return fakeBestellingLijst;
            }

        }

        public Bestelling GetSingleBestelling(int bestellingID)
        {
            try
            {
                Bestelling bestelling = Bestelling_db.GetById(bestellingID);

                if(bestelling == null)
                {
                    throw new ArgumentNullException();
                }
                return bestelling;
            }
            catch(Exception e)
            {
                Bestelling fakebestelling = new Bestelling(1, DateTime.Now, false, 1, e.ToString());
                return fakebestelling;
            }
        }

        public List<Bestelling> GetOrders(string minMax)
        {
            try
            {
                List<Bestelling> bestellingslijst = Bestelling_db.GetCurrentOrders(minMax);

                if (bestellingslijst == null)
                {
                    throw new ArgumentNullException();
                }
                return bestellingslijst;
            }
            catch(Exception e)
            {
                List<Bestelling> fakeBestellingLijst = new List<Bestelling>();
                Bestelling fakeBestelling = new Bestelling(1, DateTime.Now, true, 1, e.ToString());
                fakeBestellingLijst.Add(fakeBestelling);
                return fakeBestellingLijst;
            }
        }

        public List<Bestelling> GetDrinkOrders(string minMax)
        {
            try
            {
                List<Bestelling> bestellingslijst = Bestelling_db.GetCurrentDrinkOrders(minMax);

                if (bestellingslijst == null)
                {
                    throw new ArgumentNullException();
                }
                return bestellingslijst;
            }
            catch(Exception e)
            {
                List<Bestelling> fakeBestellingLijst = new List<Bestelling>();
                Bestelling fakeBestelling = new Bestelling(1, DateTime.Now, true, 1, e.ToString());
                fakeBestellingLijst.Add(fakeBestelling);
                return fakeBestellingLijst;
            }
        }

        public List<Bestelling> GetBestellingListView(int bestellingID)
        {
            try
            {
                List<Bestelling> bestellingslijst = Bestelling_db.GetBestellingMenuItems(bestellingID);

                if (bestellingslijst == null)
                {
                    throw new ArgumentNullException();
                }
                return bestellingslijst;
            }
            catch (Exception e)
            {
                List<Bestelling> fakeBestellingLijst = new List<Bestelling>();
                Bestelling fakeBestelling = new Bestelling(1, DateTime.Now, true, 1, e.ToString());
                fakeBestellingLijst.Add(fakeBestelling);
                return fakeBestellingLijst;
            }
        }

        public List<Bestelling> GetBestellingOpmerking(int bestellingID)
        {
            try
            {
                List<Bestelling> bestellingslijst = Bestelling_db.GetBestellingOpmerking(bestellingID);

                if (bestellingslijst == null)
                {
                    throw new ArgumentNullException();
                }
                return bestellingslijst;
            }
            catch (Exception e)
            {
                List<Bestelling> fakeBestellingLijst = new List<Bestelling>();
                Bestelling fakeBestelling = new Bestelling(1, DateTime.Now, true, 1, e.ToString());
                fakeBestellingLijst.Add(fakeBestelling);
                return fakeBestellingLijst;
            }
        }

        public string UpdateBestellingMenuItems(int BestellingMenuItemID)
        {
            try
            {
                Bestelling_db.UpdateBestellingMenuItem(BestellingMenuItemID);
                return "Menu Bestellingitem succesvol bijgewerkt!";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string UpdateBestelling(int BestellingID)
        {
            try
            {
                Bestelling_db.UpdateBestelling(BestellingID);
                return "Bestelling status succesvol gewijzigd!";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string AddNewBestelling(DateTime besteltijd, bool status, int tafelID, string opmerking)
        {
            try
            {
                Bestelling_db.AddBestelling(besteltijd, status, tafelID, opmerking);
                return "Bestelling succesvol toegevoegd!";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string EditBestelling(int BestellingID, DateTime besteltijd, bool status, int tafelID, string opmerking)
        {
            try
            {
                Bestelling_db.EditBestelling(BestellingID, besteltijd, status, tafelID, opmerking);
                return "Bestelling met succes aangepast!";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string DeleteBestelling(int BestellingID)
        {
            try
            {
                Bestelling_db.DeleteBestelling(BestellingID);
                return "Bestelling succesvol verwijderd!";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }
    }
}