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

    public class TafelService
    {
        private readonly TafelDAO Tafel_db = new TafelDAO();

        public List<Tafel> GetAllTafels()
        {
            try
            {
                List<Tafel> tafellijst = Tafel_db.Get_All_Tafels();

                if(tafellijst == null)
                {
                    throw new ArgumentNullException();
                }
                return tafellijst;
            }
            catch(Exception e)
            {
                string error = e.ToString();

                List<Tafel> fakeTafelLijst = new List<Tafel>();
                Tafel fakeTafel = new Tafel(error.Length, true, 1);
                fakeTafelLijst.Add(fakeTafel);
                return fakeTafelLijst;
            }
            
        }

        public Tafel GetSingleTafel(int tafelID)
        {
            try
            {
                Tafel tafel = Tafel_db.GetById(tafelID);
                if(tafel ==null)
                {
                    throw new ArgumentNullException();
                }
                return tafel;
            }
            catch(Exception e)
            { 

                string error = e.ToString();
                Tafel fakeTafel = new Tafel(error.Length, true, 1);
                return fakeTafel;
            }
        }

        //deze is onnodig want dit geeft dezelfde lijst met tafels terug, maar dan met een enkele eigenschap (de status) die al in de Get_All_tafels ook wordt meegegeven...
        public List<Tafel> GetTafelStatus(bool status)
        {
            try
            {
                List<Tafel> tafellijst = Tafel_db.GetTafelByStatus(status);

                if (tafellijst == null)
                {
                    throw new ArgumentNullException();
                }
                return tafellijst;
            }
            catch(Exception e)
            {
                string error = e.ToString();
                List<Tafel> fakeTafelLijst = new List<Tafel>();
                Tafel fakeTafel = new Tafel(error.Length, true, 1);
                fakeTafelLijst.Add(fakeTafel);
                return fakeTafelLijst;
            }
        } 

        // test gillian 
        public List<Tafel> GetTafeltest()
        {
            return Tafel_db.Get_All_Tafels();
        }

        public List<Tafel> GeTafelStatus(bool status)
        {
            return Tafel_db.GetTafelByStatus(status);
        }

        public Tafel ID_Status(int ID)
        {
            return Tafel_db.Get_By_ID(ID);
        }

        public List<Tafel> GetTafel()
        {
            return Tafel_db.GetTafels();
        }
    }
}
