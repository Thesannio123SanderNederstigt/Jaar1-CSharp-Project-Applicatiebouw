using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class Bestelling
    {
        public int ID { get; set; }
        public int aantal { get; set; }
        public DateTime besteltijd { get; set; }
        public bool status { get; set; }
        public int tafelID { get; set; }
        public int rekeningID { get; set; }
        public int medewerkerID { get; set; }
        public int menuItemID { get; set; }


        public Bestelling(int ID, int aantal, DateTime besteltijd, bool status, int tafelID, int rekeningID,
            int medewerkerID, int menuItemID)
        {
            this.ID = ID;
            this.aantal = aantal;
            this.besteltijd = besteltijd;
            this.status = status;
            this.tafelID = tafelID;
            this.rekeningID = rekeningID;
            this.medewerkerID = medewerkerID;
            this.menuItemID = menuItemID;
        }
    }
}
