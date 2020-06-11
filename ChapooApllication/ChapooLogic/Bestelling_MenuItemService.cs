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
            //try
            //{
            //    List<Bestelling_MenuItem> bestellingslijst = Bestelling_MenuItem_db.Get_Bestelling_MenuItem(BestellingID);

            //    if (bestellingslijst == null)
            //    {
            //        throw new ArgumentNullException();
            //    }
            //    return bestellingslijst;

            //}
            //catch(Exception e)
            //{
            //    List<Bestelling_MenuItem> fakebestellingmenuitemlist = new List<Bestelling_MenuItem>();
            //    Bestelling_MenuItem item = new Bestelling_MenuItem();
            //    fakebestellingmenuitemlist.Add(item);
            //    return fakebestellingmenuitemlist;
            //}

        }

        public List<Bestelling_MenuItem> GetBestelling(int BestellingID)
        {
            return Bestelling_MenuItem_db.Get_Bestelling(BestellingID);
            //try
            //{
            //    List<Bestelling_MenuItem> trueList = Bestelling_MenuItem_db.Get_Bestelling(BestellingID);
            //    if(trueList == null)
            //    {
            //        throw new ArgumentNullException();
            //    }
            //    return trueList;
            //}
            //catch(Exception e)
            //{
            //    List<Bestelling_MenuItem> fakeBestellingList = new List<Bestelling_MenuItem>();
            //    Bestelling_MenuItem fakeItem = new Bestelling_MenuItem();
            //    fakeBestellingList.Add(fakeItem);

            //    return fakeBestellingList;
            //}
        }

        public void CreateBestellingMenuItem(Bestelling_MenuItem bestelling_MenuItem)
        {
           Bestelling_MenuItem_db.Add_New_Bestelling_MenuItem(bestelling_MenuItem);
            //try
            //{
            //    Bestelling_MenuItem_db.Add_New_Bestelling_MenuItem(bestelling_MenuItem);
            //    return "Bestelling menuitem met succes aangemaakt!";
            //}
            //catch(Exception e)
            //{
            //    return e.ToString();
            //}

        }
        public void DeleteBestellingMenuItem(int bestelling_MenuItem)
        {
            Bestelling_MenuItem_db.Remove_Bestelling_MenuItem(bestelling_MenuItem);
            //try
            //{
            //    Bestelling_MenuItem_db.Remove_Bestelling_MenuItem(bestelling_MenuItem);
            //    return "Bestelling_MenuItem succesvol verwijderd!";
            //}
            //catch(Exception e)
            //{
            //    return e.ToString();
            //}
        }
        public void SetNewAantal (int ID, int aantal)
        {
            Bestelling_MenuItem_db.Set_New_Aantal(ID, aantal);
            //try
            //{
            //    Bestelling_MenuItem_db.Set_New_Aantal(ID, aantal);
            //    return "Aantal succesvol gewijzigd!";
            //}
            //catch  (Exception e)
            //{
            //    return e.ToString();
            //}

        }
        public void DeleteMenuItem(int MenuItemID)
        {
            Bestelling_MenuItem_db.Remove_MenuItem(MenuItemID);
            //try
            //{
            //    Bestelling_MenuItem_db.Remove_MenuItem(MenuItemID);
            //    return "menu Item met succes verwijderd!";
            //}
            //catch(Exception e)
            //{
            //    return e.ToString();
            //}

        }
        public void TESTCreateBestellingMenuItem(int MenuItemID, int ID, int Aantal)
        {
            Bestelling_MenuItem_db.New_Bestelling_MenuItem(MenuItemID, ID, Aantal);
            //try
            //{
            //    Bestelling_MenuItem_db.New_Bestelling_MenuItem(MenuItemID, ID, Aantal);
            //    return "Bestelling succesvol aangemaakt!";
            //}
            //catch(Exception e)
            //{
            //    return e.ToString();
            //}
        }
        public void DeleteBestelling(int bestellingID)
        {
            Bestelling_MenuItem_db.DeleteBestelling(bestellingID);
        }
        public void DeleteBestellingItem(int bestellingID)
        {
            Bestelling_MenuItem_db.DeleteBestellingItem(bestellingID);
        }
    }
}
