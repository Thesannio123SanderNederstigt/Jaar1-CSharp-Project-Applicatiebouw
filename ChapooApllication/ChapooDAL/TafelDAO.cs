﻿using System;
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

        private List<Tafel> ReadTafels(DataTable dataTable)
        {
            List<Tafel> tafels = new List<Tafel>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                bool status = (bool)dr["status"];
                int medewerkerID = (int)dr["medewerkerID"];
                Tafel tafel = new Tafel(ID, status, medewerkerID);
                tafels.Add(tafel);
            }

            return tafels;
        }

        //Get all Tables 
        public List<Tafel> Get_All_Tafels()
        {
            string query = "SELECT ID, [status] FROM [Tafel]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTafels(ExecuteSelectQuery(query, sqlParameters));
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
