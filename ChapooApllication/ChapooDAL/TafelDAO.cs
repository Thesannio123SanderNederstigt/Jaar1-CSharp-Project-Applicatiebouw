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
    public class TafelDAO : Connection
    {
        //gillian
        public List<Tafel> GetTafels()
        {
            string query = "select T.ID as [tafelID],isnull( B.[tafelID], '0') as [tafelStatus]\n" +
                            "from Bestelling as B right join Tafel as T on b.tafelID = t.ID";
                            
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return LeesTafels(ExecuteSelectQuery(query, sqlParameters));
        }
        //gillian
        private List<Tafel> LeesTafels(DataTable dataTable)
        {
            List<Tafel> tafel = new List<Tafel>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int TafelID = (int)dr["tafelID"];
                int TafelStatus = (int)dr["tafelStatus"];
                Tafel tafels = new Tafel(TafelID, TafelStatus);
                tafel.Add(tafels);
            }
            return tafel;
        }

        private List<Tafel> ReadTafels(DataTable dataTable)
        {
            List<Tafel> tafels = new List<Tafel>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                bool status = (bool)dr["status"];
                int medewerkerID = (int)dr["medewerkerID"];
                Tafel tafel = new  Tafel(ID, status, medewerkerID);
                tafels.Add(tafel);
            }
            return tafels;
        }
        //Get tafel by ID
        public Tafel GetById(int tafelID)
        {
            string query = "SELECT ID, status FROM Tafel WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", tafelID) };
            return ReadTafel(ExecuteSelectQuery(query, sqlParameters));
        }

        //Get Table by Status 
        public List<Tafel> GetTafelByStatus(bool status)
        {
            string query = "SELECT ID, status FROM Tafel WHERE status = @status";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@status", status) };
            return ReadTafels(ExecuteSelectQuery(query, sqlParameters));
        }
        //verander tafel status
        public void Tafel_Status(int tafelID)
        {
            string query = $"Update [Tafel] Set [status] = 'False' Where [ID] = '{tafelID}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteEditQuery(query, sqlParameters);
        }

        private Tafel ReadTafel(DataTable dataTable)
        {
        Tafel tafel = null;

            foreach (DataRow dr in dataTable.Rows)
            {
            int ID = (int)dr["ID"];
            bool status = (bool)dr["status"];
            int medewerkerID = (int)dr["medewerkerID"];
            tafel = new Tafel(ID, status, medewerkerID);
            }
            return tafel;
        }
    }
}
