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
    public class MenuItemService
    {
        private readonly MenuItemDAO MenuItem_db = new MenuItemDAO();

        //methoden voor de menu Items
        public List<MenuItem> GetMenuItems()
        {
            try
            {
                return MenuItem_db.Get_All_MenuItems();
            }
            catch(Exception)
            {
                List<MenuItem> fakemenuitemlist = new List<MenuItem>();
                MenuItem fakemenuitem = new MenuItem(1,"error", "an error occured", 0.0f, 0, 0, "error");
                fakemenuitemlist.Add(fakemenuitem);
                return fakemenuitemlist;
            }
        }

        public MenuItem GetSingleMenuItem(int menuItemID)
        {
            try
            {
                return MenuItem_db.GetById(menuItemID);
            }
            catch  (Exception)
            {
                MenuItem fakemenuitem = new MenuItem(1, "error", "an error occured", 0.0f, 0, 0, "error");
                return fakemenuitem;
            }
        }
        
        public MenuItem ReadMenuItem(string MenuKaartSoort, string SoortType)
        {
            try
            {
                return MenuItem_db.Db_GetMenuItem(MenuKaartSoort, SoortType);
            }
            catch(Exception)
            {
                MenuItem fakemenuitem = new MenuItem(1, "error", "an error occured", 0.0f, 0, 0, "error");
                return fakemenuitem;
            }

        }
    }
}
