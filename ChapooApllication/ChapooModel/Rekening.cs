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
        public bool betaalStatus { get;  set; }
        public string opmerking { get; set; }

        private Tafel tafel;


        public Rekening(int ID, float fooi, string betaalwijze, bool betaalStatus, string opmerking, Tafel tafel)
        {
            this.ID = ID;
            this.fooi = fooi;
            this.betaalwijze = betaalwijze;
            this.betaalStatus = betaalStatus;
            this.opmerking = opmerking;
            this.tafel = tafel;
        }
    }
}
