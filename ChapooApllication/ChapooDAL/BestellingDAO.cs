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
            string query = "SELECT ID, besteltijd, status, tafelID, menuItemID FROM Bestelling";
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
                string opmerking = (string)dr["opmerking"];

                Bestelling bestelling = new Bestelling(ID, besteltijd, status, tafelID, opmerking);
                bestellingen.Add(bestelling);
            }

            return bestellingen;
        }


        public List<Bestelling> GetCurrentOrders(string minMax)
        {
            string query = "SELECT Bestelling.tafelID, Bestelling.besteltijd AS [besteltijd], Bestelling.ID, Bestelling.[status], " +
                 "CASE WHEN Bestelling.opmerking IS NULL THEN '' ELSE Bestelling.opmerking END AS[opmerking] " +
                 "FROM Bestelling_MenuItem JOIN Bestelling ON Bestelling_MenuItem.bestellingID = Bestelling.ID JOIN MenuItem ON Bestelling_MenuItem.menuItemID = MenuItem.ID " +
                 "WHERE menukaartsoort != 'Dranken' GROUP BY Bestelling.tafelID, Bestelling.ID, besteltijd, Bestelling.[status], " +
                 $"Bestelling.opmerking ORDER BY besteltijd {minMax}";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadBestellingenKaart(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<Bestelling> GetCurrentDrinkOrders(string minMax)
        {
            string query = "SELECT Bestelling.tafelID, Bestelling.besteltijd AS [besteltijd], Bestelling.ID, Bestelling.[status], "+ 
                "CASE WHEN Bestelling.opmerking IS NULL THEN '' ELSE Bestelling.opmerking END AS[opmerking] " +
                "FROM Bestelling_MenuItem JOIN Bestelling ON Bestelling_MenuItem.bestellingID = Bestelling.ID JOIN MenuItem ON Bestelling_MenuItem.menuItemID = MenuItem.ID " +
                "WHERE menukaartsoort = 'Dranken' GROUP BY Bestelling.tafelID, Bestelling.ID, besteltijd, Bestelling.[status], " +  
                $"Bestelling.opmerking ORDER BY besteltijd {minMax}";
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
                string opmerking = (string)dr["opmerking"];

                Bestelling bestelling = new Bestelling(ID, besteltijd, status, tafelID, opmerking);
                bestellingen.Add(bestelling);
            }

            return bestellingen;
        }

        public List<Bestelling> GetBestellingMenuItems(int bestellingID)
        {
            string query = "SELECT MenuItem.omschrijving, Bestelling_MenuItem.aantal, Bestelling.ID, Bestelling_MenuItem.[status], " +
                "MenuItem.menukaartsoort AS [kaartsoort] FROM Bestelling_MenuItem JOIN Bestelling ON Bestelling_MenuItem.bestellingID = Bestelling.ID " +
                "JOIN MenuItem ON Bestelling_MenuItem.menuItemID = MenuItem.ID WHERE Bestelling.ID = @id  " +
                "GROUP BY Bestelling_MenuItem.[status], MenuItem.omschrijving, Bestelling_MenuItem.aantal, Bestelling.ID, MenuItem.menukaartsoort";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", bestellingID) };
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
                string opmerking = "";
                int bestellingmenuitemID = ID;
                string omschrijving = (string)dr["omschrijving"];
                string kaartsoort = (string)dr["kaartsoort"];

                Bestelling bestelling = new Bestelling(omschrijving, opmerking, status, aantal, ID, bestellingmenuitemID, kaartsoort);
                bestellingen.Add(bestelling);
            }

            return bestellingen;
        }

        public List<Bestelling> GetBestellingOpmerking(int bestellingID)
        {

            string query = "SELECT MenuItem.omschrijving, Bestelling_MenuItem.aantal AS [Aantal], Bestelling.ID AS [BestellingID], " + 
                "Bestelling_MenuItem.ID AS [BestellingMenuItemID], Bestelling_MenuItem.[status], MenuItem.menukaartsoort AS [kaartsoort], " +
                "CASE WHEN Bestelling.opmerking IS NULL THEN '' ELSE Bestelling.opmerking END AS [opmerking] FROM Bestelling_MenuItem "+ 
                "JOIN Bestelling ON Bestelling_MenuItem.bestellingID = Bestelling.ID " + 
                "JOIN MenuItem ON Bestelling_MenuItem.menuItemID = MenuItem.ID WHERE Bestelling.ID = @id " + 
                "GROUP BY MenuItem.omschrijving, Bestelling_MenuItem.aantal, Bestelling.ID, Bestelling_MenuItem.ID, " + 
                "Bestelling_MenuItem.[status], MenuItem.menukaartsoort, Bestelling.opmerking ORDER BY omschrijving";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", bestellingID) };
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
                int bestellingmenuitemID = (int)dr["BestellingMenuItemID"];
                string opmerking = (string)dr["opmerking"]; //zorgen dat dit de algemene opmerking van de bestelling ophaald! (is nu zo)
                string omschrijving = (string)dr["omschrijving"];
                string kaartsoort = (string)dr["kaartsoort"];

                Bestelling bestelling = new Bestelling(omschrijving, opmerking, status, aantal, ID, bestellingmenuitemID, kaartsoort);
                bestellingen.Add(bestelling);
            }

            return bestellingen;
        }

        //bijwerken bestelling_menuitem van een bestelling
        public string UpdateBestellingMenuItem(int BestellingMenuItemID)
        {
            string query = "UPDATE Bestelling_MenuItem SET [status] = 1 WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", BestellingMenuItemID) };
            ExecuteEditQuery(query, sqlParameters);
            return "Menu Bestellingitem succesvol bijgewerkt!";
        }

        public string UpdateBestellingMenuItems(int BestellingID)
        {
            string query = "UPDATE Bestelling_MenuItem SET [status] = 1 WHERE bestellingID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", BestellingID) };
            ExecuteEditQuery(query, sqlParameters);
            return "Menu Bestellingitem succesvol bijgewerkt!";
        }

        //bijwerken van een gehele bestelling
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
            string query = "SELECT ID, besteltijd, status, tafelID, CASE WHEN opmerking IS NULL THEN '' ELSE opmerking END AS [opmerking] FROM Bestelling WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", bestellingID) };
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
                string opmerking = (string)dr["opmerking"];

                bestelling = new Bestelling(ID, besteltijd, status, tafelID, opmerking);
            }
            return bestelling;
        }

        public string AddBestelling(DateTime besteltijd, bool status, int tafelID, string opmerking)
        {
            string query = "INSERT Bestelling(besteltijd, status, tafelID, opmerking) VALUES (@Besteltijd, @Status, @TafelID, @Opmerking)";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@Besteltijd", besteltijd), new SqlParameter("@Status", status), 
                new SqlParameter("@TafelID", tafelID), new SqlParameter("@Opmerking", opmerking) };
            ExecuteEditQuery(query, sqlParameters);
            return "Bestelling succesvol toegevoegd!";
        }

        public string EditBestelling(int BestellingID, DateTime besteltijd, bool status, int tafelID, string opmerking)
        {
            string query = "UPDATE Bestelling SET besteltijd = @Besteltijd, status = @Status, tafelID = @TafelID, opmerking = @Opmerking WHERE ID = @Id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@Id", BestellingID), new SqlParameter("@Besteltijd", besteltijd), 
                new SqlParameter("@Status", status), new SqlParameter("@TafelID", tafelID), new SqlParameter("@Opmerking", opmerking) };
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

        //gemaakt door gillian om een bestelling te maken 
        public Bestelling Create_Bestelling(Tafel tafel)
        {
            string query = $"Insert Into [Bestelling] (besteltijd, [status], tafelID)" +
                            $"Values ('{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff")}', 'False', '{tafel.TafelID}')" +
                            $"update Tafel set [status] = 'False' where ID = '{tafel.TafelID}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query,sqlParameters);

            string query2 = "Select top(1) [ID] from [Bestelling] order by [ID] desc";
            SqlParameter[] sqlParameters2 = new SqlParameter[0];
            return LeesBestellingID(ExecuteSelectQuery(query2, sqlParameters2));
        }

        //gemaakt om de nieuwste bestellingID mee te nemen bij het vullen van een bestelling
        public int NieuwsteBestelling()
        {
            string query = "Select top(1) [ID] as [NieuwesteID] from [Bestelling] order by [ID] desc";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);
            return (int)dataTable.Rows[0]["NieuwesteID"];
        }

        private Bestelling LeesBestellingID(DataTable dataTable)
        {
            Bestelling bestelling = null;
            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                bestelling = new Bestelling(ID);
            }
            return bestelling;
        }

    }
}