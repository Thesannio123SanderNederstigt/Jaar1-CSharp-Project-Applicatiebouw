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
    public class ProductDAO : Connection
    {
        public List<Product> Get_All_Products()
        {
            string query = "SELECT ID, naam, [type], prijs, besteldatum, btw, voorraadID FROM Product";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadProducts(ExecuteSelectQuery(query, sqlParameters));
        }


        private List<Product> ReadProducts(DataTable dataTable)
        {
            List<Product> products = new List<Product>();

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                string naam = (string)dr["naam"];
                string type = (string)dr["type"];
                float prijs = (float)dr["prijs"];
                DateTime besteldatum = (DateTime)dr["besteldatum"];
                int btw = (int)dr["btw"];
                int voorraadID = (int)dr["voorraadID"];

                Product product = new Product(ID, naam, type, prijs, besteldatum, btw, voorraadID);
                products.Add(product);
            }
            return products;
        }

        public Product GetById(int productID)
        {
            string query = "SELECT ID, naam, [type], prijs, besteldatum, btw, voorraadID FROM Product WHERE ID = @id";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@id", productID)};
            return ReadProduct(ExecuteSelectQuery(query, sqlParameters));
        }

        private Product ReadProduct(DataTable dataTable)
        {
            Product product = null;

            foreach (DataRow dr in dataTable.Rows)
            {
                int ID = (int)dr["ID"];
                string naam = (string)dr["naam"];
                string type = (string)dr["type"];
                float prijs = (float)dr["prijs"];
                DateTime besteldatum = (DateTime)dr["besteldatum"];
                int btw = (int)dr["btw"];
                int voorraadID = (int)dr["voorraadID"];

                product = new Product(ID, naam, type, prijs, besteldatum, btw, voorraadID);
            }
            return product;
        }
    }
}
