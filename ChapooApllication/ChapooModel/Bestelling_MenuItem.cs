using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class Bestelling_MenuItem
    {
        public Bestelling Bestelling { get; set; }
        public int ID { get; set; }
        public MenuItem MenuItem { get; set; }
        public bool Status { get; set; }
        public string Opmerking { get; set; }
        public int MenuItemID { get; set; }
        public int  BestellingID {get; set;}
        public int Aantal { get; set; }
        public string Omschrijving { get; set; }
    }
}
