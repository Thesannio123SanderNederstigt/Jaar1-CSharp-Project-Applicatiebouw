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
            string query = "SELECT ID, besteltijd, status, tafelID, rekeningID, menuItemID FROM Bestelling";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadBestellingen (ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Bestelling> ReadBestellingen(DataTable dataTable)
        {

            List<Bestelling> bestellingen = new List<Bestelling>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                DateTime besteltijd = (DateTime)dr["besteltijd"];
                bool status = (bool)dr["status"];
                int tafelID = (int)dr["tafelID"];
                int rekeningID = (int)dr["rekeningID"];
                string opmerking = (string)dr["opmerking"];

                Bestelling bestelling = new Bestelling(ID, besteltijd, status, tafelID, rekeningID, opmerking);
                bestellingen.Add(bestelling);
            }

            return bestellingen;
        }


        public List<Bestelling> GetCurrentOrders(string minMax)
        {
            string query = $"SELECT Bestelling.tafelID, {minMax}(Bestelling.besteltijd) AS [besteltijd], Bestelling.ID, Bestelling.[status], " + 
                "Bestelling.rekeningID, Bestelling.opmerking, MenuItem.menukaartsoort AS [kaartsoort] FROM Bestelling_MenuItem " +
                "JOIN Bestelling ON Bestelling_MenuItem.bestellingID = Bestelling.ID JOIN MenuItem ON Bestelling_MenuItem.menuItemID = MenuItem.ID " +
                "GROUP BY Bestelling.tafelID, Bestelling.ID, besteltijd, Bestelling.[status], Bestelling.rekeningID, Bestelling.opmerking, MenuItem.menukaartsoort;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadBestellingenKaart(ExecuteSelectQuery(query, sqlParameters));
        }


        private List<Bestelling> ReadBestellingenKaart(DataTable dataTable)
        {

            List<Bestelling> bestellingen = new List<Bestelling>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                DateTime besteltijd = (DateTime)dr["besteltijd"];
                bool status = (bool)dr["status"];
                int tafelID = (int)dr["tafelID"];
                int rekeningID = (int)dr["rekeningID"];
                string opmerking = (string)dr["opmerking"];
                string kaartsoort = (string)dr["kaartsoort"];

                Bestelling bestelling = new Bestelling(ID, besteltijd, status, tafelID, rekeningID, opmerking, kaartsoort);
                bestellingen.Add(bestelling);
            }

            return bestellingen;
        }

        public List<Bestelling> GetBestellingMenuItems(int tafelID)
        {
            string query = "SELECT MenuItem.omschrijving, COUNT(MenuItem.omschrijving) AS [aantal], Bestelling.ID, rekeningID, Bestelling_MenuItem.[status], " +
                "MenuItem.menukaartsoort AS [kaartsoort] FROM Bestelling_MenuItem JOIN Bestelling ON Bestelling_MenuItem.bestellingID = Bestelling.ID " +
                "JOIN MenuItem ON Bestelling_MenuItem.menuItemID = MenuItem.ID WHERE Bestelling.tafelID = @id " +
               "GROUP BY Bestelling_MenuItem.[status], MenuItem.omschrijving, Bestelling.ID, rekeningID, MenuItem.menukaartsoort";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", tafelID) };
            return ReadSpecialBestelling(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Bestelling> ReadSpecialBestelling(DataTable dataTable)
        {
            List<Bestelling> bestellingen = new List<Bestelling>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                bool status = (bool)dr["status"];
                int aantal = (int)dr["aantal"];
                int rekeningID = (int)dr["rekeningID"];
                string opmerking = null;
                string omschrijving = (string)dr["omschrijving"];
                string kaartsoort = (string)dr["kaartsoort"];

                Bestelling bestelling = new Bestelling(omschrijving, opmerking, status, aantal, ID, rekeningID, kaartsoort);
                bestellingen.Add(bestelling);
            }

            return bestellingen;
        }

        public List<Bestelling> GetBestellingOpmerking(int tafelID)
        {
            string query = "SELECT MenuItem.omschrijving, CASE WHEN Bestelling_MenuItem.opmerking IS NULL THEN '' ELSE Bestelling_MenuItem.opmerking " +
                "END AS [opmerking], COUNT(MenuItem.omschrijving) AS [Aantal], Bestelling.ID AS [BestellingID], Bestelling_MenuItem.ID AS [BestellingMenuItemID], " +
                "Bestelling_MenuItem.[status], MenuItem.menukaartsoort AS [kaartsoort] FROM Bestelling_MenuItem JOIN Bestelling ON Bestelling_MenuItem.bestellingID = Bestelling.ID " + 
                "JOIN MenuItem ON Bestelling_MenuItem.menuItemID = MenuItem.ID WHERE Bestelling.tafelID = @id GROUP BY MenuItem.omschrijving, " +
                "Bestelling_MenuItem.opmerking, Bestelling.ID, Bestelling_MenuItem.ID, Bestelling_MenuItem.[status], MenuItem.menukaartsoort ORDER BY omschrijving";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", tafelID) };
            return ReadAnotherSpecialBestelling(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Bestelling> ReadAnotherSpecialBestelling(DataTable dataTable)
        {
            List<Bestelling> bestellingen = new List<Bestelling>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["BestellingID"];
                bool status = (bool)dr["status"];
                int aantal = (int)dr["Aantal"];
                int rekeningID = (int)dr["BestellingMenuItemID"];
                string opmerking = (string)dr["opmerking"];
                string omschrijving = (string)dr["omschrijving"];
                string kaartsoort = (string)dr["kaartsoort"];

                Bestelling bestelling = new Bestelling(omschrijving, opmerking, status, aantal, ID, rekeningID, kaartsoort);
                bestellingen.Add(bestelling);
            }

            return bestellingen;
        }

        public string UpdateBestellingMenuItem(int BestellingMenuItemID, string opmerking)
        {
            string query = "UPDATE Bestelling_MenuItem SET [status] = 1, opmerking = @opmerking WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", BestellingMenuItemID), new SqlParameter("@opmerking", opmerking) };
            ExecuteEditQuery(query, sqlParameters);
            return "Menu Bestellingitem succesvol bijgewerkt!";
        }

        public string UpdateBestelling(int BestellingID)
        {
            string query = "UPDATE Bestelling SET [status] = 1 WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", BestellingID) };
            ExecuteEditQuery(query, sqlParameters);
            return "Bestelling status succesvol gewijzigd!";
        }


        // Get Bestelling by ID
        public Bestelling GetById(int bestellingID)
        {
            string query = "SELECT ID, besteltijd, status, tafelID, rekeningID, menuItemID FROM Bestelling WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[]  { new SqlParameter("@id", bestellingID) };
            return ReadBestelling(ExecuteSelectQuery(query, sqlParameters));
        }



        private Bestelling ReadBestelling(DataTable dataTable)
        {
            Bestelling bestelling = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                DateTime besteltijd = (DateTime)dr["besteltijd"];
                bool status = (bool)dr["status"];
                int tafelID = (int)dr["tafelID"];
                int rekeningID = (int)dr["rekeningID"];
                string opmerking = (string)dr["opmerking"];

                bestelling = new Bestelling(ID, besteltijd, status, tafelID, rekeningID, opmerking);
            }

            return bestelling;
        }

        public string AddBestelling(DateTime besteltijd, bool status, int tafelID, int rekeningID, string opmerking)
        {
            string query = "INSERT Bestelling(besteltijd, status, tafelID, rekeningID, opmerking) VALUES (@Besteltijd, @Status, @TafelID, @RekeningID, @Opmerking)";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@Besteltijd", besteltijd), new SqlParameter("@Status", status), 
                new SqlParameter("@TafelID", tafelID), new SqlParameter("@RekeningID", rekeningID), new SqlParameter("@Opmerking", opmerking) };
            ExecuteEditQuery(query, sqlParameters);
            return "Bestelling succesvol toegevoegd!";
        }

        public string EditBestelling(int BestellingID, DateTime besteltijd, bool status, int tafelID, int rekeningID, string opmerking)
        {
            string query = "UPDATE Bestelling SET besteltijd = @Besteltijd, status = @Status, tafelID = @TafelID, rekeningID = @RekeningID, opmerking = @Opmerking WHERE ID = @Id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@Id", BestellingID), new SqlParameter("@Besteltijd", besteltijd), 
                new SqlParameter("@Status", status), new SqlParameter("@TafelID", tafelID), new SqlParameter("@RekeningID", rekeningID), new SqlParameter("@Opmerking", opmerking) };
            ExecuteEditQuery(query, sqlParameters);
            return "Bestelling met succes aangepast!";
        }

        public string DeleteBestelling(int BestellingID)
        {
            string query = "DELETE FROM Bestelling WHERE ID = @Id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@Id", BestellingID) };
            ExecuteEditQuery(query, sqlParameters);
            return "Bestelling succesvol verwijderd!";
        }

        //private Bestelling LeesBestellingID(DataTable dataTable)
        //{
        //    Bestelling bestelling = new Bestelling();
        //    foreach(DataRow dr in dataTable.Rows)
        //    {
        //        bestelling.ID = (int)dr[""];
        //    }


        //    return bestelling;
        //




    }
}