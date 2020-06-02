using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class MenuItem
    {
        public int ID { get; set; }
        public string menukaartsoort { get; set; }
        public string omschrijving { get; set; }
        public float prijs { get; set; }
        public int btw { get; set; }
        public int aantalInVoorraad { get; set; }
        public string categorie { get;  set; }

        public MenuItem(int ID, string menukaartsoort, string omschrijving, float prijs, int btw, int aantalInVoorraad, string categorie)
        {
            this.ID = ID;
            this.menukaartsoort = menukaartsoort;
            this.omschrijving = omschrijving;
            this.prijs = prijs;
            this.btw = btw;
            this.aantalInVoorraad = aantalInVoorraad;
            this.categorie = categorie;
        }
    }
}
