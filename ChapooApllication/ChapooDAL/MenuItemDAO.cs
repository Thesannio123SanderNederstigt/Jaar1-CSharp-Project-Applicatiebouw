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
            string query = "SELECT ID, menukaartsoort, categorie, prijs, btw, omschrijving, aantalvoorraad FROM MenuItem";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadMenuItems(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<MenuItem> Get_Gerechten_MenuItems()
        {
            string query = "SELECT ID, menukaartsoort, categorie, prijs, btw, omschrijving, aantalvoorraad FROM MenuItem WHERE NOT menukaartsoort = 'Dranken'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadMenuItems(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<MenuItem> Get_Dranken_MenuItems()
        {
            string query = "SELECT ID, menukaartsoort, categorie, prijs, btw, omschrijving, aantalvoorraad FROM MenuItem WHERE menukaartsoort = 'Dranken'";
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
                // float naar float blijkt niet te werken, dus we hebben de float geconvert naar een single floating point number
                float prijs = Convert.ToSingle( dr["prijs"]);
                int btw = (int)dr["btw"];
                string omschrijving = (string)dr["omschrijving"];
                int aantalvoorraad = (int)dr["aantalvoorraad"];


                MenuItem menuitem = new MenuItem(ID, menukaartsoort, omschrijving, prijs, btw, aantalvoorraad, categorie);
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

        public string AddMenuItem(int ID, string omschrijving, int inVoorraad, int BTW, string categorie, string menuSoort, float prijs)
        {
            string query = "INSERT INTO menuItem VALUES(@ID, @omschrijving, @inVoorraad, @BTW, @categorie, @menuSoort, @prijs)";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@ID",ID), new SqlParameter("@omschrijving", omschrijving),
            new SqlParameter("@inVoorraad", inVoorraad),  new SqlParameter("@BTW", BTW),
            new SqlParameter("@categorie", categorie),  new SqlParameter("@menuSoort", menuSoort),  new SqlParameter("@prijs", prijs) };
            ExecuteEditQuery(query, sqlParameters);

            return "MenuItem met succes toegevoegd";
        }

        public MenuItem Db_GetMenuItem(string MenuKaartSoort, string SoortType)
        {
            string query = "SELECT * FROM MenuItem Where menukaartsoort = @MenuKaartSoort and categorie = @SoortType";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@MenuKaartSoort", MenuKaartSoort),
                new SqlParameter("@SoortType", SoortType) };
            return ReadMenuItem(ExecuteSelectQuery(query, sqlParameters));
        }
        // Edit with product and aantal
        public string EditMenuItem(string product, int aantal)
        {
            string query = "UPDATE MenuItem SET aantalvoorraad = @aantal WHERE omschrijving = @product";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@product", product), new SqlParameter("@aantal", aantal) };
            ExecuteEditQuery(query, sqlParameters);
            return "Menu item met succes aangepast!";
        }

        public string DeleteMenuItemByID(int ID)
        {
            string query = "DELETE FROM menuItem WHERE ID = @id";

            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", ID)};
            ExecuteEditQuery(query, sqlParameters);
            return "Menu item succesvol verwijderd!";
        }

        // Delete with product and aantal
        public string DeleteMenuItem(string product, int aantal)
        {
            string query = "DELETE FROM menuItem WHERE omschrijving = @product AND aantalvoorraad = @aantal";
            
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@product", product), new SqlParameter("@aantal", aantal) };
            ExecuteEditQuery(query, sqlParameters);
            return "Menu item succesvol verwijderd!";
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
