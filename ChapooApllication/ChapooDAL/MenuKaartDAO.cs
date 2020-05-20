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
    public class MenuKaartDAO : Connection
    {
        public List<MenuKaart> Get_All_MenuKaarten()
        {
            string query = "SELECT ID, naam FROM [MenuKaart]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadMenuKaarten(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<MenuKaart> ReadMenuKaarten (DataTable datatable)
        {
            List<MenuKaart> menukaarten = new List<MenuKaart>();

            foreach(DataRow dr in datatable.Rows)
            {
                int ID = (int)dr["ID"];
                string naam = (string)dr["naam"];

                MenuKaart menuKaart = new MenuKaart(ID, naam);
                menukaarten.Add(menuKaart);
            }
            return menukaarten;
        }


        public MenuKaart GetById(int menukaartID)
        {
            string query = "SELECT ID, naam FROM MenuKaart WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", menukaartID) };
            return ReadMenuKaart(ExecuteSelectQuery(query, sqlParameters));
        }

        private MenuKaart ReadMenuKaart(DataTable dataTable)
        {
            MenuKaart menukaart = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                string naam = (string)dr["naam"];
                menukaart = new MenuKaart(ID, naam);
            }
            return menukaart;
        }
    }
}
