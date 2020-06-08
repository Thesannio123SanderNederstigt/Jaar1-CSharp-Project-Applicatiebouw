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
            string query = "SELECT ID, voornaam, achternaam, type, inlogcode FROM Medewerker";
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
                string type = (string)dr["type"];
                int inlogcode = (int)dr["inlogcode"];

                Medewerker medewerker = new Medewerker(ID, voornaam, achternaam, type, inlogcode);

                medewerkers.Add(medewerker);
            }

            return medewerkers;
        }

        public Medewerker GetById(int medewerkerID)
        {
            string query = "SELECT ID, voornaam, achternaam, type, inlogcode FROM Medewerker WHERE ID = @Id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@Id", medewerkerID) };
            return ReadMedewerker(ExecuteSelectQuery(query, sqlParameters));
        }


        public Medewerker GetByLogincode(int logincode)
        {
            string query = "SELECT ID, voornaam, achternaam, type, inlogcode FROM [Medewerker] WHERE inlogcode = @logincode";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@logincode", logincode) };
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
                string type = (string)dr["type"];
                int inlogcode = (int)dr["inlogcode"];

                medewerker = new Medewerker(ID, voornaam, achternaam, type, inlogcode);
            }

            return medewerker;
        }

        public string AddNewMedewerker(int medewerkerID, string voornaam, string achternaam, string type, int inlogcode)
        {
            string query = "INSERT INTO Medewerker(ID, voornaam, achternaam, type, inlogcode)VALUES(@medewerkerID,@voornaam, @achternaam, @type, @inlogcode)";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", medewerkerID), new SqlParameter("@voornaam",voornaam), new SqlParameter("@type", achternaam), new SqlParameter("@type", type), new SqlParameter("@inlogcode", inlogcode) };
            ExecuteEditQuery(query, sqlParameters);
            return "Succesvol een nieuwe medewerker toegevoegd!";
        }

        public string UpdateMedewerker(int medewerkerID, string voornaam, string achternaam, string type, int inlogcode)
        {
            string query = "UPDATE medewerker SET voornaam = @voornaam, achternaam = @achternaam, type = @type, inlogcode = @inlogcode WHERE ID = @medewerkerID";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@medewerkerid", medewerkerID), new SqlParameter("@voornaam", voornaam),
            new SqlParameter("@achternaam", achternaam), new SqlParameter("@type", type), new SqlParameter("@inlogcode", inlogcode)};
            ExecuteEditQuery(query, sqlParameters);
            return "Succesvol update van medewerker uitgevoerd!";
        }
        public string DeleteMedewerker(int medewerkerID)
        {
            string query = "DELETE * FROM Medewerker WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] {new SqlParameter ("@id", medewerkerID)};
            ExecuteEditQuery(query, sqlParameters);
            return "Medewerker succesvol verwijderd!";
        }


    }
}
