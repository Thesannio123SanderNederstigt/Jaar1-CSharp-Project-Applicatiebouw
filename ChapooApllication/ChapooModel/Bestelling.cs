﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class Bestelling
    {
        private List<MenuItem> menuItemlist = new List<MenuItem>();
        public int ID { get; set; }
        public DateTime besteltijd { get; set; }
        public bool status { get; set; }
        public int tafelID { get; set; }
        public int bestellingmenuitemID { get; set; }
        public string opmerking { get; set; }
        public string omschrijving { get; set; }
        public int aantal { get; set; }
        public string kaartsoort { get; set; }
        public Bestelling(int ID, DateTime besteltijd, bool status, int tafelID, string opmerking)
        {
            this.ID = ID;
            this.besteltijd = besteltijd;
            this.status = status;
            this.tafelID = tafelID;
            this.opmerking = opmerking;
        }

        public Bestelling(int ID, DateTime besteltijd, bool status, int tafelID, string opmerking, string kaartsoort)
        {
            this.ID = ID;
            this.besteltijd = besteltijd;
            this.status = status;
            this.tafelID = tafelID;
            this.opmerking = opmerking;
            this.kaartsoort = kaartsoort;
        }

        public Bestelling(string omschrijving, string opmerking, bool status, int aantal, int bestellingID, int bestellingmenuitemID)
        {
            this.omschrijving = omschrijving;
            this.opmerking = opmerking;
            this.status = status;
            this.ID = bestellingID;
            this.bestellingmenuitemID = bestellingmenuitemID;
            this.aantal = aantal;
        }

        public Bestelling(string omschrijving, string opmerking, bool status, int aantal, int bestellingID, int bestellingmenuitemID, string kaartsoort)
        {
            this.omschrijving = omschrijving;
            this.opmerking = opmerking;
            this.status = status;
            this.ID = bestellingID;
            this.bestellingmenuitemID = bestellingmenuitemID;
            this.aantal = aantal;
            this.kaartsoort = kaartsoort;

        }

        //gemaakt om alleen de bestelling ID terug te krijgen
        public Bestelling(int bestellingID)
        {
            this.ID = bestellingID;
        }
    }
}
