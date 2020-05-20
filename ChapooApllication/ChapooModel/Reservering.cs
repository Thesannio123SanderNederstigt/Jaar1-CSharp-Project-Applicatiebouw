using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class Reservering
    {
        public int ID { get; set; }
        public DateTime reserveringstijd { get; set; }
        public int klantID { get; set; }
        public int tafelID { get; set; }

        public Reservering(int id, DateTime reserveringstijd, int klantid, int tafelid)
        {
            this.ID = id;
            this.reserveringstijd = reserveringstijd;
            this.klantID = klantid;
            this.tafelID = tafelid;
        }
    }
}
