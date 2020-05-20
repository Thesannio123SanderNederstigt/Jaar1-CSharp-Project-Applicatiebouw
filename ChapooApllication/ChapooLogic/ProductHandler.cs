using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ChapooDAL;
using ChapooModel;

namespace ChapooLogic
{
    public class ProductHandler
    {
        private readonly ProductDAO Product_db = new ProductDAO();
        private readonly MenuItemDAO MenuItem_db = new MenuItemDAO();
        private readonly MenuKaartDAO MenuKaart_db = new MenuKaartDAO();
        private readonly VoorraadDAO Voorraad_db = new VoorraadDAO();

        public List<Product> GetProducten()
        {
            try
            {
                return Product_db.Get_All_Products();
            }
            catch(Exception)
            {
                List<Product> fakeproductlist = new List<Product>();
                Product fakeproduct = new Product(1, "error", "fout", 1.2f, DateTime.Now, 1, 1);
                fakeproductlist.Add(fakeproduct);
                return fakeproductlist;

            }

        }

        //methoden voor de producten
        public Product GetSingleProduct(int productID)
        {
            try
            {
                return Product_db.GetById(productID);
            }
            catch(Exception)
            {
                Product fakeproduct = new Product(1, "error", "fout", 1.2f, DateTime.Now, 1, 1);
                return fakeproduct;
            }

        }

        //methoden voor de menu Items
        public List<MenuItem> GetMenuItems()
        {
            try
            {
                return MenuItem_db.Get_All_MenuItems();
            }
            catch(Exception)
            {
                List<MenuItem> fakemenuitemlist = new List<MenuItem>();
                MenuItem fakemenuitem = new MenuItem(1,1, "an error occured");
                fakemenuitemlist.Add(fakemenuitem);
                return fakemenuitemlist;
            }
        }

        public MenuItem GetSingleMenuItem(int menuItemID)
        {
            try
            {
                return MenuItem_db.GetById(menuItemID);
            }
            catch(Exception)
            {
                MenuItem fakemenuitem = new MenuItem(1, 1, "an error occured");
                return fakemenuitem;
            }
        }

        //methoden voor de Menu Kaarten
        public List<MenuKaart> GetMenuKaarten()
        {
            try
            {
                return MenuKaart_db.Get_All_MenuKaarten();
            }
            catch(Exception)
            {
                List<MenuKaart> fakemenukaartlist = new List<MenuKaart>();
                MenuKaart fakemenukaart = new MenuKaart(1, "error");
                fakemenukaartlist.Add(fakemenukaart);
                return fakemenukaartlist;
            }
        }

        public MenuKaart GetSingleMenuKaart(int menuKaartID)
        {
            try
            {
                return MenuKaart_db.GetById(menuKaartID);
            }
            catch(Exception)
            {
                MenuKaart fakemenukaart = new MenuKaart(1, "error");
                return fakemenukaart;
            }
        }

        //methoden voor de voorraad
        public List<Voorraad> GetVoorraden()
        {
            try
            {
                return Voorraad_db.Get_All_Voorraden();
            }
            catch(Exception)
            {
                List<Voorraad> fakevoorraadlist = new List<Voorraad>();
                Voorraad fakevoorraad = new Voorraad(0, 0, 0);
                fakevoorraadlist.Add(fakevoorraad);
                return fakevoorraadlist;
            }
        }

        public Voorraad GetSingleVoorraad(int voorraadID)
        {
            try
            {
                return Voorraad_db.GetById(voorraadID);
            }
            catch(Exception)
            {
                Voorraad fakevoorraad = new Voorraad(0, 0, 0);
                return fakevoorraad;
            }
        }
        

    }
}
