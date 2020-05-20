using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapooModel
{
    public class Medewerker
    {
        public int id { get; set; }
        public string voornaam { get; set; }
        public string achternaam { get; set; }
        public string type { get; set; }
        public int inlogcode { get; set; }
        public int werkstationID { get; set; }

        public Medewerker(int id, string voornaam, string achternaam, string type, int inlogcode, int werkstationid)
        {
            this.id = id;
            this.voornaam = voornaam;
            this.achternaam = achternaam;
            this.type = type;
            this.inlogcode = inlogcode;
            this.werkstationID = werkstationid;
        }
    }
}
