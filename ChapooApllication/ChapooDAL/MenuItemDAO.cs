using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChapooModel;
using System.Data;
using System.Data.SqlClient;

namespace ChapooDAL
{
    public class MenuItemDAO : Connection
    {
        // Get all MenuItems
        public List<MenuItem> Get_All_MenuItems()
        {
            string query = "SELECT ID, menukaartID, omschrijving FROM MenuItem";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadMenuItems(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<MenuItem> ReadMenuItems(DataTable dataTable)
        {
            List<MenuItem> menuitems = new List<MenuItem>();

            foreach(DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                int menukaartID = (int)dr["menukaartID"];
                string omschrijving = (string)dr["omschrijving"];

                MenuItem menuitem = new MenuItem(ID, menukaartID, omschrijving);
                menuitems.Add(menuitem);
            }
            return menuitems;
        }

        //Get MenuItem by ID
        public MenuItem GetById(int menuitemID)
        {
            string query = "SELECT ID, menukaartID, omschrijving FROM MenuItem WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", menuitemID)};
            return ReadMenuItem(ExecuteSelectQuery(query, sqlParameters));
        }

        private MenuItem ReadMenuItem(DataTable dataTable)
        {
            MenuItem menuitem = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                int menukaartID = (int)dr["menukaartID"];
                string omschrijving = (string)dr["omschrijving"];

                menuitem = new MenuItem(ID, menukaartID, omschrijving);

            }

            return menuitem;
        }
    }
}
