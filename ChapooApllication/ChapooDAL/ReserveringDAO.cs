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
    public class ReserveringDAO : Connection
    {
       
        private List<Reservering> ReadReserveringen(DataTable dataTable)
        {
            List<Reservering> reserveringen = new List<Reservering>();
            
            foreach(DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                DateTime reserveringtijd = (DateTime)dr["reserveringtijd"];
                int klantID = (int)dr["klantID"];
                int tafelID = (int)dr["tafelID"];

                Reservering reservering = new Reservering(ID, reserveringtijd, klantID, tafelID);
                reserveringen.Add(reservering);

            }
            return reserveringen;
        }

        private Reservering ReadReservering(DataTable dataTable)
        {
            Reservering reservering = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                DateTime reserveringtijd = (DateTime)dr["reserveringtijd"];
                int klantID = (int)dr["klantID"];
                int tafelID = (int)dr["tafelID"];

                reservering = new Reservering(ID, reserveringtijd, klantID, tafelID);
            }
            return reservering;
        }

        //Get all Reserveringen
        public List<Reservering> Get_all_Reserveringen()
        {
            string query = "SELECT ID, reserveringtijd, klantID, tafelID FROM Reservering";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadReserveringen(ExecuteSelectQuery(query, sqlParameters));
        }

        // Get Reservering by ID
        public Reservering GetById(int reserveringID)
        {
            string query = "SELECT ID, reserveringtijd, klantID, tafelID FROM Reservering WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", reserveringID)  };
            return ReadReservering(ExecuteSelectQuery(query, sqlParameters));
        }
    }
}
