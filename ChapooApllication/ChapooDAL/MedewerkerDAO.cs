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
    public class MedewerkerDAO : Connection
    {
        public List<Medewerker> Get_All_Medewerkers()
        {
            string query = "SELECT ID, voornaam, achternaam, [type], werkstationID, inlogcode FROM Medewerker";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadMedewerkers(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Medewerker> ReadMedewerkers(DataTable dataTable)
        {
            List<Medewerker> medewerkers = new List<Medewerker>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                string voornaam = (string)dr["voornaam"];
                string achternaam = (string)dr["achternaam"];
                string type = (string)dr["[type]"];
                int werkstationID = (int)dr["werkstationID"];
                int inlogcode = (int)dr["inlogcode"];

                Medewerker medewerker = new Medewerker(ID, voornaam, achternaam, type, werkstationID, inlogcode);

                medewerkers.Add(medewerker);
            }

            return medewerkers;
        }

        public Medewerker GetById(int medewerkerID)
        {
            string query = "SELECT ID, voornaam, achternaam, [type], werkstationID, inlogcode FROM Medewerker WHERE ID = @Id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@Id", medewerkerID) };
            return ReadMedewerker(ExecuteSelectQuery(query, sqlParameters));
        }

        public Medewerker ReadMedewerker(DataTable dataTable)
        {
            Medewerker medewerker = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                string voornaam = (string)dr["voornaam"];
                string achternaam = (string)dr["achternaam"];
                string type = (string)dr["[type]"];
                int werkstationID = (int)dr["werkstationID"];
                int inlogcode = (int)dr["inlogcode"];

                medewerker = new Medewerker(ID, voornaam, achternaam, type, werkstationID, inlogcode);
            }

            return medewerker;
        }
    }
}
