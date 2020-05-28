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
            string query = "SELECT ID, menukaartsoort, categorie, prijs, btw, omschrijving, aantalInVoorraad FROM MenuItem";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadMenuItems(ExecuteSelectQuery(query, sqlParameters));
        }


        private List<MenuItem> ReadMenuItems(DataTable dataTable)
        {
            List<MenuItem> menuitems = new List<MenuItem>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                string menukaartsoort = (string)dr["menukaartsoort"];
                string categorie = (string)dr["categorie"];
                float prijs = (float)dr["prijs"];
                int btw = (int)dr["btw"];
                string omschrijving = (string)dr["omschrijving"];
                int aantalInVoorraad = (int)dr["aantalInVoorraad"];


                MenuItem menuitem = new MenuItem(ID, menukaartsoort, omschrijving, prijs, btw, aantalInVoorraad, categorie);
                menuitems.Add(menuitem);
            }
            return menuitems;
        }

        //Get MenuItem by ID
        public MenuItem GetById(int menuitemID)
        {
            string query = "SELECT ID, menukaartsoort, categorie, prijs, btw, omschrijving, aantalInVoorraad FROM MenuItem WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", menuitemID)};
            return ReadMenuItem(ExecuteSelectQuery(query, sqlParameters));
        }

        public MenuItem Db_GetMenuItem(string MenuKaartSoort, string SoortType)
        {
            string query = "SELECT * FROM MenuItem Where menukaartsoort = @MenuKaartSoort and categorie = @SoortType";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@MenuKaartSoort", MenuKaartSoort),
                new SqlParameter("@SoortType", SoortType) };
            return ReadMenuItem(ExecuteSelectQuery(query, sqlParameters));
        }

        private MenuItem ReadMenuItem(DataTable dataTable)
        {
            MenuItem menuitem = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                string menukaartsoort = (string)dr["menukaartsoort"];
                string omschrijving = (string)dr["omschrijving"];
                float prijs = (float)dr["prijs"];
                string categorie = (string)dr["categorie"];
                int btw = (int)dr["btw"];
                int aantalInVoorraad = (int)dr["aantalInVoorraad"];

                menuitem = new MenuItem(ID, menukaartsoort, omschrijving, prijs, btw, aantalInVoorraad, categorie);

            }

            return menuitem;
        }
    }
}
