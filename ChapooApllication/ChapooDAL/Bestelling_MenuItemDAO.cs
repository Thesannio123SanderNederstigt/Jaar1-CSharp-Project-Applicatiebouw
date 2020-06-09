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
    public class Bestelling_MenuItemDAO : Connection
    {
        public void Add_New_Bestelling_MenuItem(Bestelling_MenuItem bestelling_MenuItem)
        {
            string query = $"Insert into [Bestelling_MenuItem] ( menuItemID, bestellingID, [status], aantal) values " +
                $"Values ({bestelling_MenuItem.MenuItemID}, {bestelling_MenuItem.Bestelling.ID}, False , {bestelling_MenuItem.Aantal}) ";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }

        public List<Bestelling_MenuItem> Get_Bestelling_MenuItem(int BestellingID)
        {
            string query = $"Select [menuItemId], [aantal], [bestellingID], From Bestelling_MenuItem Where [bestellingID] = '{BestellingID}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public void Remove_Bestelling_MenuItem(int Bestelling_MenuItemID)
        {
            string query = $"Delete From [Bestelling_MenuItem] Where [ID] = '{Bestelling_MenuItemID}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }

        public void Set_New_Aantal (int Aantal, int ID)
        {
            string query = $"Update [Bestelling_MenuItem] set aantal = '{Aantal}' Where [ID] = '{ID}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }

        private List<Bestelling_MenuItem> ReadTables(DataTable dataTable)
        {
            List<Bestelling_MenuItem> bestelling_MenuItems = new List<Bestelling_MenuItem>();

            foreach(DataRow dr in dataTable.Rows)
            {
                Bestelling_MenuItem bestelling_MenuItem = new Bestelling_MenuItem()
                {
                    MenuItemID = (int)dr["menuItemID"],
                    BestellingID = (int)dr["BestellingID"],
                    Aantal = (int)dr["aantal"],
                    Status = (bool)dr["status"],
                    Opmerking = (string)dr["opmerking"]

                };
                bestelling_MenuItems.Add(bestelling_MenuItem);
                
            }
            return bestelling_MenuItems;
        }
    }
}
