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

        public void CreateBestellingMenuItem(Bestelling_MenuItem bestelling_MenuItem)
        {
            Bestelling_MenuItem_db.Add_New_Bestelling_MenuItem(bestelling_MenuItem);
        }
    }
}
