using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class Werkstation
    {
        public int ID { get; set; }
        public string naam { get; set; }

        public Werkstation(int id, string naam)
        {
            this.ID = id;
            this.naam = naam;
        }

    }
}
