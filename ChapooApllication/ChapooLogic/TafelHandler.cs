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

    public class TafelHandler
    {
        private readonly TafelDAO Tafel_db = new TafelDAO();

        public List<Tafel> GetTafels()
        {
            try
            {
                return Tafel_db.Get_All_Tafels();
            }
            catch(Exception)
            {
                List<Tafel> fakeTafelLijst = new List<Tafel>();
                Tafel fakeTafel = new Tafel(11, true, 1);
                fakeTafelLijst.Add(fakeTafel);
                return fakeTafelLijst;
            }
            
        }

        public Tafel GetSingleTafel(int tafelID)
        {
            try
            {
                return Tafel_db.GetById(tafelID);
            }
            catch(Exception)
            {
                Tafel fakeTafel = new Tafel(11, true, 1);
                return fakeTafel;
            }
        }

        //deze is onnodig want dit geeft dezelfde lijst met tafels terug, maar dan met een enkele eigenschap (de status) die al in de Get_All_tafels ook wordt meegegeven...
        public List<Tafel> GetTafelStatus(bool status)
        {
            try
            {
                return Tafel_db.GetTafelByStatus(status);
            }
            catch(Exception)
            {
                List<Tafel> fakeTafelLijst = new List<Tafel>();
                Tafel fakeTafel = new Tafel(11, true, 1);
                fakeTafelLijst.Add(fakeTafel);
                return fakeTafelLijst;
            }
        } 
    }
}
