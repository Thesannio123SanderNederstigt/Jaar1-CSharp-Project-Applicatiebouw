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

        public List<Rekening> GetRekeningen(int tafelID)
        {
            try
            {
                List<Rekening> rekeninglijst = Rekening_db.Get_All_Rekeningen(tafelID);

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

        public Rekening GetSingleRekening(int TafelID)
        {
            try
            {
                Rekening rekening = Rekening_db.GetById(TafelID);

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

        public List<Rekening> GetMenuitems(int tafelID)
        {
            try
            {
                List<Rekening> rekeninglijst = Rekening_db.GetRekeningItems(tafelID);

                if (rekeninglijst == null)
                {
                    throw new ArgumentNullException();
                }
                return rekeninglijst;
            }
            catch (Exception e)
            {
                List<Rekening> fakerekeninglist = new List<Rekening>();
                Rekening fakerekening = new Rekening(e.ToString(), 1, 0.0f, 1);
                fakerekeninglist.Add(fakerekening);
                return fakerekeninglist;
            }
        }

        public Rekening GetLopendeRekening(int TafelID)
        {
            try
            {
                Rekening rekening = Rekening_db.GetFalseStatus(TafelID);

                if (rekening == null)
                {
                    throw new ArgumentNullException();
                }
                return rekening;
            }
            catch (Exception e)
            {
                Rekening fakerekening = new Rekening(1, 1, e.ToString(), 1, true, "error");
                return fakerekening;
            }
        }

        public double GetTotaalBedrag(int rekeningID)
        {
            try
            {
                double bedrag = Rekening_db.GetBedrag(rekeningID);

                return bedrag;
            
            }
            catch(Exception e)
            {
                return double.Parse(e.ToString());
            }
        }

        public int GetUpperBTW(int rekeningID)
        {
            try
            {
                int btw = Rekening_db.GetMaxBTW(rekeningID);

                return btw;
            }
            catch(Exception e)
            {
                return int.Parse(e.ToString());
            }
        }

        public string AddNewRekening(int fooi, string betaalwijze,  int tafelID, bool betaalstatus, string opmerking)
        {
            try
            {
                Rekening_db.AddRekening(fooi, betaalwijze, tafelID, betaalstatus, opmerking);
                return "Rekening succesvol toegevoegd!";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string EditRekening(int rekeningID, float fooi, string betaalwijze, int tafelID, bool betaalStatus, string opmerking)
        {
            try
            {
                Rekening_db.EditRekening(rekeningID, fooi, betaalwijze, tafelID, betaalStatus, opmerking);
                return "Rekening met succes gewijzigd!";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }
        
        public string DeleteBestellingen(int tafelID) 
        {
            try
            {
                Rekening_db.DeleteBestellingen(tafelID);
                return "Bestellingen succevol verwijderd!";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string CheckRekeningItem(int ID)
        {
            try
            {
                Rekening_db.CheckRekening(ID);
                return "Rekeing met succes gechecked!";
            }
            catch(Exception e)
            {
                return e.ToString();
            }

        }
    }
}
