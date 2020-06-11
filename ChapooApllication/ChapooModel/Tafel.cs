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

        public int TafelID { get; set; }
        public int TafelStatus { get; set; }

        private Medewerker medewerker;

        public Tafel(int id, bool status, int medewerkerid)
        {
            this.ID = id;
            this.status = status;
            this.medewerkerID = medewerkerid;
        }
        public Tafel(int tafelID, int TafelStatus)
        {
            this.TafelID = tafelID;
            this.TafelStatus = TafelStatus;
        }
        
    }
}
