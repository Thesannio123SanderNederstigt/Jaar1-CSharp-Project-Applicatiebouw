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
    public class VoorraadDAO : Connection
    {
        public List<Voorraad> Get_All_Voorraden()
        {
            string query = "SELECT aantal, ID, productID FROM Voorraad";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadVoorraden(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Voorraad> ReadVoorraden(DataTable dataTable)
        {
            List<Voorraad> voorraden = new List<Voorraad>();

            foreach(DataRow dr in dataTable.Rows)
            {
                int aantal = (int)dr["aantal"];
                int ID = (int)dr["ID"];
                int productID = (int)dr["productID"];

                Voorraad voorraad = new Voorraad(ID, aantal, productID);
                voorraden.Add(voorraad);
            }

            return voorraden;
        }

        public Voorraad GetById(int voorraadID)
        {
            string query = "SELECT aantal, ID, productID FROM Voorraad WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", voorraadID)};
            return ReadVoorraad(ExecuteSelectQuery(query, sqlParameters));
        }

        private Voorraad ReadVoorraad(DataTable dataTable)
        {
            Voorraad voorraad = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                int aantal = (int)dr["aantal"];
                int ID = (int)dr["ID"];
                int productID = (int)dr["productID"];

                voorraad = new Voorraad(ID, aantal, productID);
            }

            return voorraad;
        }
    }
}
