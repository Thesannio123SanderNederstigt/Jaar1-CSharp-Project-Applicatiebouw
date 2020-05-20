using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class MenuKaart
    {
        public int ID { get; set; }
        public string naam { get; set; }

        public MenuKaart(int id, string naam)
        {
            this.ID = id;
            this.naam = naam;
        }
    }
}
