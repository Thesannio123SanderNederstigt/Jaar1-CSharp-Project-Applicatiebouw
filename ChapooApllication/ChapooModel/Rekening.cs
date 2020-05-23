using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class Rekening 
    {
        private List<Bestelling> bestellingen = new List<Bestelling>();
        public int ID { get; set; }
        public float fooi { get; set; }
        public string betaalwijze { get; set; }
        public bool betaalStatus { get;  set; }
        public string opmerking { get; set; }

        private Tafel tafel;


        public Rekening(int ID, float fooi, string betaalwijze, int tafelID, bool betaalStatus, string opmerking)
        {
            this.ID = ID;
            this.fooi = fooi;
            this.betaalwijze = betaalwijze;
            this.tafel.ID = tafelID;
            this.betaalStatus = betaalStatus;
            this.opmerking = opmerking;

        }

        /*public Rekening(int ID, float fooi, string betaalwijze, bool betaalStatus, string opmerking)
        {
            this.ID = ID;
            this.fooi = fooi;
            this.betaalwijze = betaalwijze;
            this.betaalStatus = betaalStatus;
            this.opmerking = opmerking;
        }*/
    }
}
