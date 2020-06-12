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
    public class RekeningDAO : Connection
    {
        public List<Rekening> Get_All_Rekeningen(int TafelID)
        {
            string query = "SELECT ID, fooi, betaalwijze, tafelID, betaalstatus, opmerking FROM Rekening WHERE tafelID = @id AND betaalstatus = 0";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", TafelID)};
            return ReadRekeningen(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Rekening> ReadRekeningen(DataTable dataTable)
        {
            List<Rekening> rekeningen = new List<Rekening>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                float fooi = Convert.ToSingle(dr["fooi"]);
                string betaalwijze = (string)dr["betaalwijze"];
                int tafelID = (int)dr["tafelID"];
                bool betaalstatus = (bool)dr["betaalstatus"];
                string opmerking = (string)dr["opmerking"];


                Rekening rekening = new Rekening(ID, fooi, betaalwijze, tafelID, betaalstatus, opmerking);
                
                rekeningen.Add(rekening);
            }

            return rekeningen;
        }

        public List<Rekening> GetRekeningItems(int tafelID)
        {
            string query = "SELECT MenuItem.omschrijving, Bestelling_menuItem.aantal, MenuItem.prijs, menuItem.btw FROM Rekening JOIN Bestelling ON Rekening.tafelID = Bestelling.tafelID JOIN Bestelling_MenuItem ON Bestelling.ID = Bestelling_MenuItem.bestellingID JOIN MenuItem ON Bestelling_MenuItem.menuItemID = MenuItem.ID WHERE Rekening.tafelID = @id AND Bestelling.[status] = 1;";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", tafelID) };
            return ReadMenuItems(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Rekening> ReadMenuItems(DataTable dataTable)
        {
            List<Rekening> rekeningen = new List<Rekening>();

            foreach (DataRow dr in dataTable.Rows)
            {
                string omschrijving = (string)dr["omschrijving"];
                int aantal = (int)dr["aantal"];
                double prijs = (double)dr["prijs"];
                int btw = (int)dr["btw"];


                Rekening rekening = new Rekening(omschrijving, aantal, prijs, btw);

                rekeningen.Add(rekening);
            }

            return rekeningen;
        }


        public Rekening GetById(int tafelID)
        {
            string query = "SELECT ID, fooi, betaalwijze, tafelID, betaalstatus, opmerking FROM Rekening WHERE tafelID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", tafelID) };
            return ReadRekening(ExecuteSelectQuery(query, sqlParameters));
        }

        private Rekening ReadRekening(DataTable dataTable)
        {
            Rekening rekening = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                float fooi = Convert.ToSingle(dr["fooi"]);
                string betaalwijze = (string)dr["betaalwijze"];
                int tafelID = (int)dr["tafelID"];
                bool betaalstatus = (bool)dr["betaalstatus"];
                string opmerking = (string)dr["opmerking"];

                rekening = new Rekening(ID, fooi, betaalwijze, tafelID, betaalstatus, opmerking);
            }

            return rekening;
        }

        public Rekening GetByTafelId(int TafelID)
        {
            string query = "SELECT ID, fooi, betaalwijze, tafelID, betaalstatus, opmerking FROM Rekening WHERE tafelID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", TafelID) };
            return ReadRekening(ExecuteSelectQuery(query, sqlParameters));
        }

        public Rekening GetFalseStatus(int TafelID)
        {
            string query = "SELECT ID, fooi, betaalwijze, tafelID, betaalstatus, opmerking FROM Rekening WHERE tafelID = @id AND betaalstatus = 0";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", TafelID) };
            return ReadRekening(ExecuteSelectQuery(query, sqlParameters));
        }

        public string AddRekening(int fooi, string betaalwijze, int tafelID, bool betaalstatus, string opmerking)
        {
            string query = "INSERT INTO Rekening (fooi, betaalwijze, tafelID, betaalstatus, opmerking) VALUES (@fooi, @betaalwijze, @tafelID, @betaalstatus, @opmerking)";
            SqlParameter[] sqlParameters = new SqlParameter[] {new SqlParameter("@fooi", fooi), new SqlParameter("@betaalwijze", betaalwijze),
            new SqlParameter("@tafelID", tafelID), new SqlParameter("@betaalStatus", betaalstatus), new SqlParameter("@opmerking", opmerking)};
            ExecuteEditQuery(query, sqlParameters);
            return "Rekening succesvol toegevoegd!";
        }

        public string EditRekening(int rekeningID, float fooi, string betaalwijze, int tafelID, bool betaalstatus, string opmerking)
        {
            string query = "UPDATE Rekening SET fooi = @fooi, betaalwijze = @betaalwijze, tafelID = @tafelID, betaalstatus = @betaalStatus, " +
                "opmerking = @opmerking WHERE ID = @ID";
            SqlParameter[] sqlParameters = new SqlParameter[] {new SqlParameter("@fooi", fooi), new SqlParameter("@betaalwijze", betaalwijze),
            new SqlParameter("@tafelID", tafelID), new SqlParameter("@betaalStatus", betaalstatus), new SqlParameter("@opmerking", opmerking), new SqlParameter("@ID", rekeningID)};
            ExecuteEditQuery(query, sqlParameters);
            return "Rekening met succes gewijzigd!";
        }

        public string DeleteBestellingen(int tafelID)
        {
            string query = "DELETE FROM Bestelling WHERE bestelling.[status] = 1 AND tafelID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@ID", tafelID) };
            ExecuteEditQuery(query, sqlParameters);
            return "Rekening succevol verwijderd!";
        }

        public string CheckRekening(int ID)
        {
            string query =$"IF NOT EXISTS(SELECT * FROM Rekening WHERE tafelID='{ID}' AND betaalstatus='False') \n"+
                          $"INSERT INTO Rekening(tafelID, betaalstatus) Select '{ID}', 'False'" ;
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
            return "RekeningWijzigen";
        }

        public double GetBedrag(int rekeningID)
        {
            string query = "SELECT CASE WHEN SUM(MenuItem.prijs * Bestelling_MenuItem.Aantal) IS NULL " +
                "THEN '' ELSE SUM(MenuItem.prijs * Bestelling_MenuItem.Aantal) END AS [totaalprijs] " +
                "FROM Rekening JOIN Bestelling ON Rekening.tafelID = Bestelling.tafelID " +
                "JOIN Bestelling_MenuItem ON Bestelling.ID = Bestelling_MenuItem.bestellingID " +
                "JOIN MenuItem ON Bestelling_MenuItem.menuItemID = MenuItem.ID WHERE Rekening.ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", rekeningID) };
            return ReadBedrag(ExecuteSelectQuery(query, sqlParameters));
        }

        private double ReadBedrag(DataTable dataTable)
        {
            double prijs = 0;

            foreach(DataRow dr in dataTable.Rows)
            {
                prijs = (double)dr["totaalprijs"];
            }
;
            return prijs;
        }


        public int GetMaxBTW(int rekeningID)
        {
            string query = "SELECT CASE WHEN MAX(menuItem.btw) IS NULL THEN '' ELSE MAX(menuItem.btw) END AS [btw] FROM Rekening JOIN Bestelling " + 
                "ON Rekening.tafelID = Bestelling.tafelID JOIN Bestelling_MenuItem ON Bestelling.ID = Bestelling_MenuItem.bestellingID " + 
                "JOIN MenuItem ON Bestelling_MenuItem.menuItemID = MenuItem.ID WHERE Rekening.ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", rekeningID)};
            return ReadBTW(ExecuteSelectQuery(query, sqlParameters));
        }

        private int ReadBTW(DataTable dataTable)
        {
            int btw = 0;
            
            foreach(DataRow dr in dataTable.Rows)
            {
                btw = (int)dr["btw"];
            }

            return btw;
        }

        public Rekening GetTafelByID(int TafelID)
        {
            string query = "SELECT ID, fooi, betaalwijze, tafelID, betaalstatus, opmerking FROM Rekening WHERE tafelID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", TafelID) };
            return ReadRekening(ExecuteSelectQuery(query, sqlParameters));
        }

    }
}
