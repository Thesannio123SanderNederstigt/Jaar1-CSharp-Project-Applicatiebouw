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
    public class BestellingDAO : Connection
    {
        // Get all Bestellingen
        public List<Bestelling> Get_All_Bestellingen()
        {
            string query = "SELECT ID, aantal, besteltijd, status, tafelID, rekeningID, medewerkerID, menuItemID FROM Bestelling";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadBestellingen (ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Bestelling> ReadBestellingen(DataTable dataTable)
        {
            List<Bestelling> bestellingen = new List<Bestelling>();

            foreach(DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                int aantal = (int)dr["aantal"];
                DateTime besteltijd = (DateTime)dr["besteltijd"];
                bool status = (bool)dr["status"];
                int tafelID = (int)dr["tafelID"];
                int rekeningID = (int)dr["rekeningID"];
                int medewerkerID = (int)dr["medewerkerID"];
                int menuItemID = (int)dr["menuItemID"];

                Bestelling bestelling = new Bestelling(ID, aantal, besteltijd, status, tafelID, rekeningID, medewerkerID, menuItemID);
                bestellingen.Add(bestelling);
            }

            return bestellingen;
        }

        

        // Get Bestelling by ID
        public Bestelling GetById(int bestellingID)
        {
            string query = "SELECT ID, aantal, besteltijd, status, tafelID, rekeningID, medewerkerID, menuItemID FROM Bestelling WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[]  { new SqlParameter("@id", bestellingID) };
            return ReadBestelling(ExecuteSelectQuery(query, sqlParameters));
        }



        private Bestelling ReadBestelling(DataTable dataTable)
        {
            Bestelling bestelling = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                int aantal = (int)dr["aantal"];
                DateTime besteltijd = (DateTime)dr["besteltijd"];
                bool status = (bool)dr["status"];
                int tafelID = (int)dr["tafelID"];
                int rekeningID = (int)dr["rekeningID"];
                int medewerkerID = (int)dr["medewerkerID"];
                int menuItemID = (int)dr["menuItemID"];

                bestelling = new Bestelling(ID, aantal, besteltijd, status, tafelID, rekeningID, medewerkerID, menuItemID);
            }

            return bestelling;
        }

        //bij de methode voor het inschrijven van een bestelling ook een gerecht aanmaken in de database (tabel 'Gerecht')

    }
}