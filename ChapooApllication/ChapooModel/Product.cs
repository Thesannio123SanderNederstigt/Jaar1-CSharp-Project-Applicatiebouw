using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class Product
    {
        public int ID { get; set; }
        public string naam { get; set; }
        public string type { get; set; }
        public float prijs { get; set; }
        public DateTime besteldatum { get; set; }
        public int btw { get; set; }
        public int voorraadID { get; set; }

        public Product(int id, string naam, string type, float prijs, DateTime besteldatum, int btw, int voorraadID)
        {
            this.ID = id;
            this.naam = naam;
            this.type = type;
            this.prijs = prijs;
            this.besteldatum = besteldatum;
            this.btw = btw;
            this.voorraadID = voorraadID;
        }
    }
}
