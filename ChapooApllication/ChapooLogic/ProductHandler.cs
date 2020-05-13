using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ChapooDAL;
using ChapooModel;

namespace ChapooLogic
{
    public class ProductHandler
    {
        ProductDAO Product_db = new ProductDAO();

        public List<Product> GetProduct()
        {
            return Product_db.Db.Get_All_Producten();
        }
    }
}
