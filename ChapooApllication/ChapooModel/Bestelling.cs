using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class Bestelling
    {
        private List<MenuItem> menuitemlist = new List<MenuItem>();
        public int ID { get; set; }
        public DateTime besteltijd { get; set; }
        public bool status { get; set; }
        public int tafelID { get; set; }
        public int rekeningID { get; set; }
        public string opmerking { get; set; }


        public Bestelling(int ID, DateTime besteltijd, bool status, int tafelID, int rekeningID, string opmerking)
        {
            this.ID = ID;
            this.besteltijd = besteltijd;
            this.status = status;
            this.tafelID = tafelID;
            this.rekeningID = rekeningID;
            this.opmerking = opmerking;
        }
    }
}
