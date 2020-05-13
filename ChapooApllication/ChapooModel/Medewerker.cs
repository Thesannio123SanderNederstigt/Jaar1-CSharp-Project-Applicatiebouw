using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class Medewerker
    {
        public int ID { get; set; }
        public string voornaam { get; set; }
        public string achternaam { get; set; }
        public string type { get; set; }
        public int inlogcode { get; set; }
        public int werkstationID { get; set; }
    }
}
