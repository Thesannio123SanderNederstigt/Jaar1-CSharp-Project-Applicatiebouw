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
    public class MedewerkerService
    {
        private readonly MedewerkerDAO Medewerker_db = new MedewerkerDAO();

        public List<Medewerker> GetMedewerkers()
        {
            try
            {
                List<Medewerker> medewerkerlist = Medewerker_db.Get_All_Medewerkers();

                if(medewerkerlist == null)
                {
                    throw new ArgumentNullException();
                }

                return medewerkerlist;
            }
            catch(Exception e)
            {
                List<Medewerker> fakemedewerkerlist = new List<Medewerker>();

                Medewerker fakemedewerker = new Medewerker(99999999, e.ToString(), "something went wrong", "oh no", 9999);

                fakemedewerkerlist.Add(fakemedewerker);

                return fakemedewerkerlist;
            }

        }

        public Medewerker GetMedewerker(int medewerkerID)
        {
            try
            {
                Medewerker medewerker = Medewerker_db.GetById(medewerkerID);

                if (medewerker == null)
                {
                    throw new ArgumentNullException();
                }
                return medewerker;
            }
            catch(Exception e)
            {
                Medewerker fakemedewerker = new Medewerker(9999999, e.ToString(), "something went wrong", "oh no", 9999);
                return fakemedewerker;
            }
        }

        public Medewerker GetByLogincode(int loginCode)
        {
            try
            {
                Medewerker medewerker = Medewerker_db.GetByLogincode(loginCode);

                if (medewerker == null)
                {

                    throw new ArgumentNullException();
                }
                return medewerker;

            }
            catch (Exception e)
            {
                Medewerker fakeMedewerker = new Medewerker(1, e.ToString(), "news:", "nonExisting", 9999);
                return fakeMedewerker;
            }
        }

        public string AddNewMedewerker(int medewerkerID, string voornaam, string achternaam, string type, int inlogcode)
        {
            try
            {
                Medewerker_db.AddNewMedewerker(medewerkerID, voornaam, achternaam, type, inlogcode);
                return "Succesvol een nieuwe medewerker toegevoegd!";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string DeleteMedewerker(int medewerkerID)
        {
            try
            {
                Medewerker_db.DeleteMedewerker(medewerkerID);
                return "Medewerker succesvol verwijderd!";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }


    }
}
