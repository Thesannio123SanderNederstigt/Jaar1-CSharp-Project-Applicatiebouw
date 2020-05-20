using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class Rekening
    {
        public int ID { get; set; }
        public float fooi { get; set; }
        public string betaalwijze { get; set; }
        public int tafelID { get; set; }
        public bool betaalStatus { get;  set; }

        public Rekening(int id, float fooi, string betaalwijze, int tafelID, bool betaalStatus)
        {
            this.ID = id;
            this.fooi = fooi;
            this.betaalwijze = betaalwijze;
            this.tafelID = tafelID;
            this.betaalStatus = betaalStatus;
        }
    }
}
