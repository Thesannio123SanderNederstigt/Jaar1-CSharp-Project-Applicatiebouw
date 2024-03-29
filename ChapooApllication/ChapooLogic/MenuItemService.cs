﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ChapooDAL;
using ChapooModel;

namespace ChapooLogic
{
    public class MenuItemService
    {
        private readonly MenuItemDAO MenuItem_db = new MenuItemDAO();

    

        //methoden voor de menu Items
        public List<MenuItem> GetMenuItems()
        {
            try
            {
                List<MenuItem> menuitemlijst = MenuItem_db.Get_All_MenuItems();

                if(menuitemlijst == null)
                {
                    throw new ArgumentNullException();
                }
                return menuitemlijst;
            }
            catch(Exception e)
            {
                List<MenuItem> fakemenuitemlist = new List<MenuItem>();
                MenuItem fakemenuitem = new MenuItem(1, e.ToString(), "error", 0.0f, 0, 0, "error");
                fakemenuitemlist.Add(fakemenuitem);
                return fakemenuitemlist;
            }
        }
        public List<MenuItem> Get_LunchMenuKaart()
        {
            try
            {
                List<MenuItem> menuitemlijst = MenuItem_db.Get_LunchMenuKaart();

                if (menuitemlijst == null)
                {
                    throw new ArgumentNullException();
                }
                return menuitemlijst;
            }
            catch (Exception e)
            {
                List<MenuItem> fakemenuitemlist = new List<MenuItem>();
                MenuItem fakemenuitem = new MenuItem(1, e.ToString(), "an error occured", 0.0f, 0, 0, "error");
                fakemenuitemlist.Add(fakemenuitem);
                return fakemenuitemlist;
            }
        }

        public List<MenuItem> Get_DinerMenuKaart()
        {
            try
            {
                List<MenuItem> menuitemlijst = MenuItem_db.Get_DinerMenuKaart_MenuItems();

                if (menuitemlijst == null)
                {
                    throw new ArgumentNullException();
                }
                return menuitemlijst;
            }
            catch (Exception e)
            {
                List<MenuItem> fakemenuitemlist = new List<MenuItem>();
                MenuItem fakemenuitem = new MenuItem(1, e.ToString(), "an error occured", 0.0f, 0, 0, "error");
                fakemenuitemlist.Add(fakemenuitem);
                return fakemenuitemlist;
            }
        }

        public List<MenuItem> Get_Gerechten_MenuItems()
        {
            try
            {
                List<MenuItem> menuitemlijst = MenuItem_db.Get_Gerechten_MenuItems();

                if(menuitemlijst == null)
                {
                    throw new ArgumentNullException();
                }
                return menuitemlijst;
            }
            catch(Exception e)
            {
                List<MenuItem> fakemenuitemlist = new List<MenuItem>();
                MenuItem fakemenuitem = new MenuItem(1, e.ToString(), "an error occured", 0.0f, 0, 0, "error");
                fakemenuitemlist.Add(fakemenuitem);
                return fakemenuitemlist;
            }
        }

        public List<MenuItem> Get_Dranken_MenuItems()
        {
            try
            {
                List<MenuItem> menuitemlijst = MenuItem_db.Get_Dranken_MenuItems();

                if(menuitemlijst == null)
                {
                    throw new ArgumentNullException();
                }
                return menuitemlijst;
            }
            catch(Exception e)
            {
                List<MenuItem> fakemenuitemlist = new List<MenuItem>();
                MenuItem fakemenuitem = new MenuItem(1, e.ToString(), "an error occured", 0.0f, 0, 0, "error");
                fakemenuitemlist.Add(fakemenuitem);
                return fakemenuitemlist;
            }
        }

        public MenuItem GetSingleMenuItem(int menuItemID)
        {
            try
            {
                MenuItem menuItem = MenuItem_db.GetById(menuItemID);

                if(menuItem == null)
                {
                    throw new ArgumentNullException();
                }
                return menuItem;
            }
            catch(Exception e)
            {
                MenuItem fakemenuitem = new MenuItem(1, e.ToString(), "an error occured", 0.0f, 0, 0, "error");
                return fakemenuitem;
            }
        }

        public string EditAllMenuItem(int ID, string omschrijving, int inVoorraad, int BTW, string categorie, string menuSoort, float prijs)
        {
            try
            {
                MenuItem_db.EditAllMenuItem(ID, omschrijving, inVoorraad, BTW, categorie, menuSoort, prijs);
                return "Menu item met succes aangepast!";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string EditMenuItem(string product, int aantal)
        {
            try
            {
                MenuItem_db.EditMenuItem(product, aantal);
                return "Menu item met succes aangepast!";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }

        public string AddMenuItem(string omschrijving, int inVoorraad, int BTW, string categorie, string menuSoort, float prijs)
        {
            try
            {
                MenuItem_db.AddMenuItem(omschrijving,inVoorraad,BTW, categorie, menuSoort, prijs);
                return "Menu item succesvol toegevoegd!";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public string DeleteMenuItem(string product, int aantal)
        {
            try
            {

                MenuItem_db.DeleteMenuItem(product, aantal);
                return "Menu item succesvol verwijderd!";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }
       

        public MenuItem ReadMenuItem(string MenuKaartSoort, string SoortType)
        {
            try
            {
                MenuItem menuItem = MenuItem_db.Db_GetMenuItem(MenuKaartSoort, SoortType);
                if(menuItem == null)
                {
                    throw new ArgumentNullException();
                }
                return menuItem;
            }
            catch(Exception e)
            {
                MenuItem fakemenuitem = new MenuItem(1, e.ToString(), "an error occured", 0.0f, 0, 0, "error");
                return fakemenuitem;
            }
        }


        // test om soort en cat menuitem te krijgen
        public List<MenuItem> GetItems(string MenuKaartSoort, string Categorie)
        {
            try
            {
                List<MenuItem> menuitemlijst = MenuItem_db.GetByCat(MenuKaartSoort, Categorie);

                if (menuitemlijst == null)
                {
                    throw new ArgumentNullException();
                }
                return menuitemlijst;
            }
            catch (Exception e)
            {
                List<MenuItem> fakemenuitemlist = new List<MenuItem>();
                MenuItem fakemenuitem = new MenuItem(1, e.ToString(), "an error occured", 0.0f, 0, 0, "error");
                fakemenuitemlist.Add(fakemenuitem);
                return fakemenuitemlist;
            }
        }
    }
}
