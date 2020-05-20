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
            string query = "SELECT ID, fooi, betaalwijze, tafelID, betaalstatus FROM Rekening";
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
                Rekening rekening = new Rekening(ID, fooi, betaalwijze, tafelID, betaalstatus);
                
                rekeningen.Add(rekening);
            }

            return rekeningen;
        }



        public Rekening GetById(int rekeningID)
        {
            string query = "SELECT ID, fooi, betaalwijze, tafelID, betaalstatus FROM Rekening WHERE ID = @id";
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

                rekening = new Rekening(ID, fooi, betaalwijze, tafelID, betaalstatus);
            }

            return rekening;
        }


    }
}
