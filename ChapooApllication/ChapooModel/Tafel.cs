using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class Tafel
    {
        public int ID { get; set; }
        public bool status { get; set; }
        public int medewerkerID { get; set; }

        public Tafel(int id, bool status, int medewerkerid)
        {
            this.ID = id;
            this.status = status;
            this.medewerkerID = medewerkerid;
        }
    }
}
