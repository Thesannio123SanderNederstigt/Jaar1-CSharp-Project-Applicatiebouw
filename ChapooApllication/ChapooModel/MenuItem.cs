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
        public int menukaartID { get; set; }
        public string omschrijving { get; set; }

        public MenuItem(int id, int menukaartid, string omschrijving)
        {
            this.ID = id;
            this.menukaartID = menukaartid;
            this.omschrijving = omschrijving;
        }
    }
}
