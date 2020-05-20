
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
    public class WerkstationDAO : Connection
    {
        public List<Werkstation> Get_All_Werkstations()
        {
            string query = "SELECT ID, naam FROM Werkstation";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadWerkstations(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Werkstation> ReadWerkstations(DataTable dataTable)
        {
            List<Werkstation> werkstations = new List<Werkstation>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                string naam = (string)dr["naam"];


                Werkstation werkstation = new Werkstation(ID,naam);

                werkstations.Add(werkstation);
            }

            return werkstations;
        }

        public Werkstation GetById(int werkstationID)
        {
            string query = "SELECT ID, naam FROM Werkstation WHERE ID=@Id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@Id", werkstationID) };
            return ReadWerkstation(ExecuteSelectQuery(query, sqlParameters));
        }



        public Werkstation ReadWerkstation(DataTable dataTable)
        {
            Werkstation werkstation = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                string naam = (string)dr["naam"];
                werkstation = new Werkstation(ID, naam);
            }

            return werkstation;
        }



    }
}
