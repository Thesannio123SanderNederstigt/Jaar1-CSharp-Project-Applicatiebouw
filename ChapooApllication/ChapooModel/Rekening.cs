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

        public int tafelID { get; set; }

        public string omschrijving { get; set; }
        public int aantal { get; set; }
        public double prijs { get; set; }
        public int btw { get; set; }



        public Rekening(int ID, float fooi, string betaalwijze, int tafelID, bool betaalStatus, string opmerking)
        {
            this.ID = ID;
            this.fooi = fooi;
            this.betaalwijze = betaalwijze;
            this.tafelID = tafelID;
            this.betaalStatus = betaalStatus;
            this.opmerking = opmerking;

        }

        public Rekening(string omschrijving, int aantal, double prijs, int btw)
        {
            this.omschrijving = omschrijving;
            this.aantal = aantal;
            this.prijs = prijs;
            this.btw = btw;
        }
    }
}
