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
        public List<Rekening> Get_All_Rekeningen()
        {
            string query = "SELECT ID, fooi, betaalwijze, tafelID, betaalstatus, opmerking FROM Rekening";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadRekeningen(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Rekening> ReadRekeningen(DataTable dataTable)
        {
            List<Rekening> rekeningen = new List<Rekening>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                float fooi = (float)dr["fooi"];
                string betaalwijze = (string)dr["betaalwijze"];
                int tafelID = (int)dr["tafelID"];
                bool betaalstatus = (bool)dr["betaalstatus"];
                string opmerking = (string)dr["opmerking"];

                Rekening rekening = new Rekening(ID, fooi, betaalwijze, tafelID, betaalstatus, opmerking);
                
                rekeningen.Add(rekening);
            }

            return rekeningen;
        }



        public Rekening GetById(int rekeningID)
        {
            string query = "SELECT ID, fooi, betaalwijze, tafelID, betaalstatus, opmerking FROM Rekening WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", rekeningID) };
            return ReadRekening(ExecuteSelectQuery(query, sqlParameters));
        }

        private Rekening ReadRekening(DataTable dataTable)
        {
            Rekening rekening = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                float fooi = (float)dr["fooi"];
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

        public string AddRekening(float fooi, string betaalwijze, bool betaalStatus, string opmerking, int tafelID)
        {
            string query = "INSERT INTO [Rekening] VALUES (@fooi, @betaalwijze, @betaalStatus, @opmerking, @tafelID)";
            SqlParameter[] sqlParameters = new SqlParameter[] {new SqlParameter("@fooi", fooi), new SqlParameter("@betaalwijze", betaalwijze),
            new SqlParameter("@betaalStatus", betaalStatus), new SqlParameter("@opmerking", opmerking), new SqlParameter("@tafelID", tafelID)};
            ExecuteEditQuery(query, sqlParameters);
            return "Rekening succesvol toegevoegd!";

        }

        public string EditRekening(int ID, float fooi, string betaalwijze, bool betaalStatus, string opmerking)
        {
            string query = "UPDATE Rekening SET (fooi = @fooi, betaalwijze = @betaalwijze, betaalStatus = @betaalStatus, " +
                "opmerking = @opmerking WHERE ID = @ID";
            SqlParameter[] sqlParameters = new SqlParameter[] {new SqlParameter("@fooi", fooi), new SqlParameter("@betaalwijze", betaalwijze),
            new SqlParameter("@betaalStatus", betaalStatus), new SqlParameter("@opmerking", opmerking), new SqlParameter("@ID", ID)};
            ExecuteEditQuery(query, sqlParameters);
            return "Rekening met succes gewijzigd!";
        }

        public string DeleteRekening(int RekeningID)
        {
            string query = "DELETE * FROM Rekening WHERE ID = @ID";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@ID", RekeningID) };
            ExecuteEditQuery(query, sqlParameters);
            return "Rekening succevol verwijderd!";
        }

    }
}
