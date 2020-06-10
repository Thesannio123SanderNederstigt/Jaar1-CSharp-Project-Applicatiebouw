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
   public class Bestelling_MenuItemService
    {
        Bestelling_MenuItemDAO Bestelling_MenuItem_db = new Bestelling_MenuItemDAO();

        public List<Bestelling_MenuItem> GetBestellingMenuItem(int BestellingID)
        {
            return Bestelling_MenuItem_db.Get_Bestelling_MenuItem(BestellingID);
        }
        public List<Bestelling_MenuItem> GetBestelling(int BestellingID)
        {
            return Bestelling_MenuItem_db.Get_Bestelling(BestellingID);
        }
        public void CreateBestellingMenuItem(Bestelling_MenuItem bestelling_MenuItem)
        {
            Bestelling_MenuItem_db.Add_New_Bestelling_MenuItem(bestelling_MenuItem);
        }
        public void DeleteBestellingMenuItem(int bestelling_MenuItem)
        {
            Bestelling_MenuItem_db.Remove_Bestelling_MenuItem(bestelling_MenuItem);
        }
        public void SetNewAantal (int ID, int aantal)
        {
            Bestelling_MenuItem_db.Set_New_Aantal(ID, aantal);
        }
        public void DeleteMenuItem(int MenuItemID)
        {
            Bestelling_MenuItem_db.Remove_MenuItem(MenuItemID);
        }
        public void TESTCreateBestellingMenuItem(int MenuItemID, int ID, int Aantal)
        {
            Bestelling_MenuItem_db.New_Bestelling_MenuItem(MenuItemID, ID, Aantal);
        }
    }
}
