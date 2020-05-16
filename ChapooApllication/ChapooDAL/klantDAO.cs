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
    public class KlantDAO : Connection
    {
        public List<Klant> Get_All_Klanten()
        {
            string query = "SELECT ID, opmerking, tafelID FROM Klant";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadKlanten(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Klant> ReadKlanten(DataTable dataTable)
        {
            List<Klant> klanten = new List<Klant>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                string Opmerking = (string)dr["opmerking"];
                int tafelID = (int)dr["tafelID"];

                Klant klant = new Klant(ID, Opmerking, tafelID);

                klanten.Add(klant);
            }

            return klanten;
        }

        public Klant GetById(int klantID)
        {
            string query = "SELECT ID, opmerking, tafelID FROM Klant WHERE ID = @Id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@Id", klantID) };
            return ReadKlant(ExecuteSelectQuery(query, sqlParameters));
        }

        public Klant ReadKlant(DataTable dataTable)
        {
            Klant klant = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                string Opmerking = (string)dr["opmerking"];
                int tafelID = (int)dr["tafelID"];

                klant = new Klant(ID, Opmerking, tafelID);
            }

            return klant;
        }
    }
}
