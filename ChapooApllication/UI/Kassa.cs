using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ChapooModel;
using ChapooLogic;
using MenuItem = ChapooModel.MenuItem;
using System.Drawing.Drawing2D;

namespace UI
{
    public enum MenuKeuze
    {
        Drank,
        Lunch,
        Diner,
        Gerechten
    }
    public enum ViewKeuze
    {
        Voorraad,
        MenuKaart,
        Personeelsbeheer
    }
    
    public enum ButtonActie
    {
        Verwijderen,
        Wijzigen,
        Toevoegen
    }
    public partial class Kassa : Form
    {

        //string Type = Login.MedewerkerType;
        public static string Bestellingoverzicht = "";
        User user = Login.user;
        private static MenuKeuze menu;
        private static ViewKeuze view;
        private static ButtonActie actie;
        private static string Message;
        MedewerkerService medewerkerService = new MedewerkerService();

        public void HidePanels()
        {
            pnl_Inkomstenoverzicht.Hide();
            pnl_KassaDinerMenuoverzicht.Hide();
            pnl_MenuOverzicht.Hide();
            pnl_KassaHoofdscherm.Hide();
            pnl_KassaKeuzeBestellingen.Hide();
            pnl_KassaLunchMenuoverizcht.Hide();
            pnl_KassaMenuoverzichtKeuze.Hide();
            pnl_KassaPersoneelsbeheer.Hide();
            pnl_KassaTafeloverzicht.Hide();
            pnl_KassaVoorraadKeuze.Hide();
            pnl_KassaVoorraadoverzicht.Hide();
            pnl_KassaVoorraadoverzichtGerecht.Hide();
        }

<<<<<<< HEAD

        
        public void RefreshPersoneelsbeheer(MedewerkerService medewerkerService)
=======
        public void RefreshText()
        {
            foreach(Control panel in this.Controls)
            {
                foreach(Control control in panel.Controls)
                {                    
                   if(control.GetType() == typeof(TextBox))
                   {
                        control.Controls.Clear();
                   }
                }          
            }
        }

        public void RefreshVoorraadGerecht(MenuItemService menuItemService)
>>>>>>> e629fdf331b66a83cc98047b676f94f2b24f2af2
        {
            HidePanels();
            listView_Personeelsbeheer.Items.Clear();
            List<Medewerker> medewerkerList = medewerkerService.GetMedewerkers();
            foreach (Medewerker medewerker in medewerkerList)
            {
                ListViewItem listviewItem = new ListViewItem(medewerker.ID.ToString());
                listviewItem.SubItems.Add(medewerker.voornaam);
                listviewItem.SubItems.Add(medewerker.achternaam);
                listviewItem.SubItems.Add(medewerker.type);
                listviewItem.SubItems.Add(medewerker.inlogcode.ToString());
                listView_Personeelsbeheer.Items.Add(listviewItem);
            }
            pnl_KassaPersoneelsbeheer.Show();
        }
        public Kassa()
        {
            InitializeComponent();
            if (user == User.ChefKok)
            {
                HidePanels();
                pnl_KassaVoorraadoverzicht.Show();
                menuItemFiller();
            }
            else if (user == User.Barpersoneel)
            {
                HidePanels();
                pnl_KassaVoorraadoverzicht.Show();
                menuItemFiller();
            }
            else
            {
                HidePanels();
                pnl_KassaHoofdscherm.Show();
            }
<<<<<<< HEAD


=======
>>>>>>> e629fdf331b66a83cc98047b676f94f2b24f2af2
        }
        private void pictureBx_Uitloggen_Kassa_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            //Login.ActiveForm.Visible = true;

            Login login = new Login();
            login.Show();
        }

        private void Kassa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Environment.Exit(0);
            }
        }

        // Event Handlers voor het Hoofdscherm
        private void btn_MenuOverzichtHoofdscherm_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaMenuoverzichtKeuze.Show();
        }

        private void btn_TafeloverzichtHoofdscherm_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaTafeloverzicht.Show();
        }

        private void btn_VoorraadHoofdscherm_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaVoorraadKeuze.Show();
        }

        private void btn_PersoneelsbeheerHoofdscherm_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            listView_Personeelsbeheer.Items.Clear();
            List<Medewerker> medewerkerList = medewerkerService.GetMedewerkers();
            foreach (Medewerker medewerker in medewerkerList)
            {
                ListViewItem listviewItem = new ListViewItem(medewerker.ID.ToString());
                listviewItem.SubItems.Add(medewerker.voornaam);
                listviewItem.SubItems.Add(medewerker.achternaam);
                listviewItem.SubItems.Add(medewerker.type);
                listviewItem.SubItems.Add(medewerker.inlogcode.ToString());
                listView_Personeelsbeheer.Items.Add(listviewItem);
            }        
            pnl_KassaPersoneelsbeheer.Show();



        }

        private void btn_BestellingenHoofdscherm_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaKeuzeBestellingen.Show();
        }

        // Event handlers voor Menu Overzicht Keuze
        private void btnDrankMenu_MenuoverzichtKeuze_Click(object sender, EventArgs e)
        {
            menu = MenuKeuze.Drank;
            view = ViewKeuze.MenuKaart;
            //lbl_Overzicht.Text = "Drank Overzicht";
            txtBoxMenuoverzicht.Text = "Drank Overzicht";
            HidePanels();
            pnl_MenuOverzicht.Show();
            lv_MenuOverzicht.Items.Clear();
            menuItemFiller();
        }

        private void btnLunchMenu_MenuoverzichtKeuze_Click(object sender, EventArgs e)
        {
            menu = MenuKeuze.Lunch;
            view = ViewKeuze.MenuKaart;
            //lbl_Overzicht.Text = "Lunch Overzicht";
            txtBoxMenuoverzicht.Text = "Lunch Overzicht";

            HidePanels();
            pnl_MenuOverzicht.Show();
            lv_MenuOverzicht.Items.Clear();
            menuItemFiller();
        }

        private void btnDinerMenu_MenuoverzichtKeuze_Click(object sender, EventArgs e)
        {
            menu = MenuKeuze.Diner;
            view = ViewKeuze.MenuKaart;
            //lbl_Overzicht.Text = "Diner Overzicht";
            txtBoxMenuoverzicht.Text = "Diner Overzicht";

            HidePanels();
            pnl_MenuOverzicht.Show();
            lv_MenuOverzicht.Items.Clear();
            menuItemFiller();

            RefreshText();
        }

        // Methode om de listviews te vullen op basis van de menu keuzes en voorraad of menukaart
        public List<MenuItem> menuItemFiller()
        {
            MenuItemService menuItemService = new MenuItemService();
            List<MenuItem> thisList = new List<MenuItem>();
            if(user == User.Barpersoneel)
            {
                menu = MenuKeuze.Drank;
            } 
            else if(user == User.ChefKok)
            {
                menu = MenuKeuze.Gerechten;
            }

            if (menu == MenuKeuze.Drank)
            {
                thisList = menuItemService.Get_Dranken_MenuItems();
            }
            else if (menu == MenuKeuze.Lunch)
            {
                thisList = menuItemService.Get_LunchMenuKaart();
            }
            else if (menu == MenuKeuze.Diner)
            {
                thisList = menuItemService.Get_DinerMenuKaart();
            } 
            else if(menu == MenuKeuze.Gerechten)
            {
              thisList = menuItemService.Get_Gerechten_MenuItems();
            }

            foreach (MenuItem m in thisList)
                {
                if (view == ViewKeuze.MenuKaart)
                {
                   
                    ListViewItem listViewItem = new ListViewItem(m.ID.ToString());
                    listViewItem.SubItems.Add(m.omschrijving);
                    listViewItem.SubItems.Add(m.aantalInVoorraad.ToString());
                    listViewItem.SubItems.Add(m.btw.ToString());
                    listViewItem.SubItems.Add(m.categorie);
                    listViewItem.SubItems.Add(m.menukaartsoort);
                    listViewItem.SubItems.Add(m.prijs.ToString("0.00"));

                    lv_MenuOverzicht.Items.Add(listViewItem);

                }
               else if (view == ViewKeuze.Voorraad || user == User.ChefKok || user == User.Barpersoneel)
                {
                    ListViewItem listViewItem = new ListViewItem(m.omschrijving);
                    listViewItem.SubItems.Add(m.aantalInVoorraad.ToString());

                    listView_VoorraadOverzicht.Items.Add(listViewItem);
                }
            }

            return thisList;
        }

        // Event Handlers voor VoorraadOverzichtKeuze Scherm
        private void btn_DrankVoorraadOverzichtKeuze_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            view = ViewKeuze.Voorraad;
            menu = MenuKeuze.Drank;
            txtBoxVoorraadoverzicht.Text = "Drankvoorraad overzicht";
            listView_VoorraadOverzicht.Items.Clear();
            listView_VoorraadOverzicht.Show();
            menuItemFiller();
            pnl_KassaVoorraadoverzicht.Show();
        }
        
        private void btn_GerechtVoorraadKeuzeOverzicht_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            view = ViewKeuze.Voorraad;
            menu = MenuKeuze.Gerechten;
            txtBoxVoorraadoverzicht.Text = "Gerechtvoorraad overzicht";
            listView_VoorraadOverzicht.Items.Clear();
            menuItemFiller();
            pnl_KassaVoorraadoverzicht.Show();

        }

        // Event Handlers voor Gerecht Voorraad Overzicht Scherm

        private void btnVooraad_GerechtVoorraadOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaVoorraadKeuze.Show();
        }

        private void btnBestellingen_GerechtVoorraadOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaKeuzeBestellingen.Show();
        }

        private void btnMenuOverzicht_GerechtVoorraadOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaMenuoverzichtKeuze.Show();
        }

        private void btnPersoneelsbeheer_GerechtvooraadOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaPersoneelsbeheer.Show();
        }

        // button voor wijzigen voorraad
        private void btnWijzigen_GerechtVoorraadOverzicht_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Wijzigen;
            Panel panelGerechtVoorraad = pnl_KassaVoorraadoverzicht;
            ButtonHelper(panelGerechtVoorraad);
            panelGerechtVoorraad.Show();
        }

        // button voor verwijderen voorraad
        private void btnVerwijderen_GerechtvoorraadOverzicht_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Verwijderen;
            Panel panelGerechtVoorraad = pnl_KassaVoorraadoverzicht;
            ButtonHelper(panelGerechtVoorraad);
            panelGerechtVoorraad.Show();

        }

        // de items van de gerechten voorraad resetten
        private void ClearGerechtenVoorraadOverzicht(MenuItemService service)
        {
            txtProduct_GerechtVoorraadoverzicht.Clear();
            txtAantal_GerechtVoorraadoverzicht.Clear();
            listview_GerechtVoorraadOverzicht.Items.Clear();
            //RefreshVoorraadGerecht(service);
            pnl_KassaVoorraadoverzichtGerecht.Show();
        }

        // methode om listview items te selecteren
        private void listview_GerechtVoorraadOverzicht_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listView_GerechtVoorraad = listview_GerechtVoorraadOverzicht.SelectedItems;

            if (listView_GerechtVoorraad.Count > 0)
            {
                int index = listview_GerechtVoorraadOverzicht.SelectedIndices[0];
                txtProduct_GerechtVoorraadoverzicht.Text = listview_GerechtVoorraadOverzicht.Items[index].SubItems[0].Text;
                txtAantal_GerechtVoorraadoverzicht.Text = listview_GerechtVoorraadOverzicht.Items[index].SubItems[1].Text.ToString();
            }
        }

        // Event handler voor Diner menu Overzicht Scherm
        private void btnVoorraad_DinerMenuOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaVoorraadKeuze.Show();
        }

        private void btnBestellingen_Dinermenuoverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaKeuzeBestellingen.Show();
        }

        private void btnMenuOverzicht_DinermenuOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaMenuoverzichtKeuze.Show();
        }

        private void btnPersoneelsbeheer_DinerMenuOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaPersoneelsbeheer.Show();
        }

        // methode om een menuitem aan de menukaart toe te voegen
        private void btnToevoegen_DinerMenuOverzicht_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Toevoegen;
            Panel panelDinerMenu = pnl_MenuOverzicht;
            ButtonHelper( panelDinerMenu);
            panelDinerMenu.Show();

        }

        // opslaan van de wijzigengen aan de menukaart items
        private void btnOpslaan_DinerMenuOverzicht_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Wijzigen;
            Panel panelDinerMenu = pnl_MenuOverzicht;
            ButtonHelper(panelDinerMenu);
            panelDinerMenu.Show();
        }

        // verwijderen van een menu item
        private void btnVerwijderen_DinerMenuOverzicht_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Verwijderen;
            Panel panelDinermenu = pnl_MenuOverzicht;
            ButtonHelper(panelDinermenu);
            panelDinermenu.Show();
        }

        private void listView_DinerMenuOverzicht_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listView_DinerMenu = listView_DinerMenuOverzicht.SelectedItems;

            if (listView_DinerMenu.Count > 0)
            {
                int index = listView_DinerMenuOverzicht.SelectedIndices[0];
                txtID_DinerMenuOverzicht.Text = listView_DinerMenuOverzicht.Items[index].SubItems[0].Text.ToString();
                txtOmschrijving_DinerMenuOverzicht.Text = listView_DinerMenuOverzicht.Items[index].SubItems[1].Text;
                txtInVoorraad_DinermenuOverzicht.Text = listView_DinerMenuOverzicht.Items[index].SubItems[2].Text.ToString();
                txtBTW_DinerMenuOverzicht.Text = listView_DinerMenuOverzicht.Items[index].SubItems[3].Text.ToString();
                txtCategorie_DinerMenuOverzicht.Text = listView_DinerMenuOverzicht.Items[index].SubItems[4].Text;
                txtMenuSoort_DinerMenuOverzicht.Text = listView_DinerMenuOverzicht.Items[index].SubItems[5].Text;
                txtPrijs_DinerMenuOverzicht.Text = listView_DinerMenuOverzicht.Items[index].SubItems[6].Text.ToString();
            }
        }

        private void pictureBx_TerugDinermenuOverzicht_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaMenuoverzichtKeuze.Show();
        }

        // Event Handlers voor KeuzeBestelling Scherm
        private void btn_BarKeuzeBestelling_Kassa_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Bestellingoverzicht = "bar";
            Keuken keuken = new Keuken();
            keuken.Show();
        }

        private void btn_KeukenKeuzeBestelling_Kassa_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Bestellingoverzicht = "kok";
            Keuken keuken = new Keuken();
            keuken.Show();
        }

        // Event Handlers voor Personeels Overzicht Scherm
        private void btn_VoorraadPersoneelsoverzicht_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaVoorraadKeuze.Show();
        }

        private void btn_BestellingenPersoneelsoverzicht_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaKeuzeBestellingen.Show();
        }
        private void btn_MenuoverzichtPersoneelsbeheer_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaMenuoverzichtKeuze.Show();
        }

        private void btn_PersoneelsbeheerPersoneelsoverzicht_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            listView_Personeelsbeheer.Items.Clear();

            MedewerkerService medewerkerService = new MedewerkerService();
            List<Medewerker> medewerkerList = medewerkerService.GetMedewerkers();
            foreach (Medewerker medewerker in medewerkerList)
            {
                ListViewItem listviewItem = new ListViewItem(medewerker.ID.ToString());
                listviewItem.SubItems.Add(medewerker.voornaam);
                listviewItem.SubItems.Add(medewerker.achternaam);
                listviewItem.SubItems.Add(medewerker.type);
                listviewItem.SubItems.Add(medewerker.inlogcode.ToString());

                listView_Personeelsbeheer.Items.Add(listviewItem);
            }
            pnl_KassaPersoneelsbeheer.Show();
        }

        private void btn_VerwijderenPersoneelsBeheer_Kassa_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Verwijderen;
            Panel panelPersoneel = pnl_KassaPersoneelsbeheer;
            
            ButtonHelper(panelPersoneel);
            RefreshPersoneelsbeheer(medewerkerService);
            panelPersoneel.Show();
        }
        // button om te wijzigen
        private void btn_OpslaanPersoneelsbeheer_Kassa_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Wijzigen;
            Panel panelPersoneel = pnl_KassaPersoneelsbeheer;
            ButtonHelper(panelPersoneel);
            RefreshPersoneelsbeheer(medewerkerService);
            panelPersoneel.Show();
        }

        private void btn_ToevoegenPersoneel_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Toevoegen;
            //listView_Personeelsbeheer.Items.Clear();
            Panel panelPersoneel = pnl_KassaPersoneelsbeheer;
            ButtonHelper(panelPersoneel);
            RefreshPersoneelsbeheer(medewerkerService);
            panelPersoneel.Show();
           
        }

        private void listView_Personeelsbeheer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listView_personeel = listView_Personeelsbeheer.SelectedItems;
            if(listView_personeel.Count > 0)
            {
                int index = listView_Personeelsbeheer.SelectedIndices[0];
                txt_PersoneelsbeheerID.Text = listView_Personeelsbeheer.Items[index].SubItems[0].Text.ToString();
                txt_VoornaamPersoneelsbeheer.Text = listView_Personeelsbeheer.Items[index].SubItems[1].Text;
                txt_AchternaamPersoneelsbeheer.Text = listView_Personeelsbeheer.Items[index].SubItems[2].Text;
                txt_TypePersoneelsbeheer.Text = listView_Personeelsbeheer.Items[index].SubItems[3].Text;
                txt_InlogcodePersoneelsbeheer.Text = listView_Personeelsbeheer.Items[index].SubItems[4].Text.ToString();
            }
        }


        // Event Handlers voor Lunch Menu Overzicht Scherm
        private void btn_VoorraadLunchmenuoverzicht_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaVoorraadKeuze.Show();
        }

        private void btn_BestellingenLunchmenuoverzicht_Kassan41_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaKeuzeBestellingen.Show();
        }

        private void btn_MenuOverzichtLunchmenuoverzicht_Kassan41_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaMenuoverzichtKeuze.Show();
        }

        private void btn_PersoneelsbeheerLunchmenuoverzicht_Kassan41_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaPersoneelsbeheer.Show();
        }


        private void btnToevoegen_Lunchmenuoverzicht_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Toevoegen;
            Panel panelLunchMenu = pnl_MenuOverzicht;
            ButtonHelper(panelLunchMenu);
            panelLunchMenu.Show();
        }

        private void btnOpslaan_Lunchmenuoverzicht_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Wijzigen;
            Panel panelLunchMenu = pnl_MenuOverzicht;
            ButtonHelper(panelLunchMenu);
            panelLunchMenu.Show();
        }

        private void btnVerwijderen_Lunchmenuoverzicht_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Verwijderen;
            Panel panelLunchMenu = pnl_MenuOverzicht;
            ButtonHelper(panelLunchMenu);
            panelLunchMenu.Show();

        }

        private void listView_LunchMenuOverzicht_Kassa_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listView_Lunchmenu = listView_LunchMenuOverzicht_Kassa.SelectedItems;
            if (listView_Lunchmenu.Count > 0)
            {
                int index = listView_LunchMenuOverzicht_Kassa.SelectedIndices[0];
                txtID_Lunchmenuoverzicht.Text = listView_LunchMenuOverzicht_Kassa.Items[index].SubItems[0].Text.ToString();
                txtOmschrijving_Lunchmenuoverzicht.Text = listView_LunchMenuOverzicht_Kassa.Items[index].SubItems[1].Text;
                txtInVoorraad_Lunchmenuoverzicht.Text = listView_LunchMenuOverzicht_Kassa.Items[index].SubItems[2].Text.ToString();
                txtBTW_Lunchmenuoverzicht.Text = listView_LunchMenuOverzicht_Kassa.Items[index].SubItems[3].Text.ToString();
                txtCategorie_Lunchmenuoverzicht.Text = listView_LunchMenuOverzicht_Kassa.Items[index].SubItems[4].Text;
                txtMenuSoort_Lunchmenuoverzicht.Text = listView_LunchMenuOverzicht_Kassa.Items[index].SubItems[5].Text;
                txtPrijs_Lunchmenuoverzicht.Text = listView_LunchMenuOverzicht_Kassa.Items[index].SubItems[6].Text.ToString();
            }
        }

        // Event Handlers voor Drank Voorraad Overzicht Scherm
        private void btnVoorraad_DrankVoorraadOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaVoorraadKeuze.Show();
        }

        private void btnBestellingen_DrankVoorraadOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaKeuzeBestellingen.Show();
        }

        private void btnMenuOverzicht_DrankVoorraadOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaMenuoverzichtKeuze.Show();
        }

        private void btnPersoneelsbeheer_DrankVoorraadOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaPersoneelsbeheer.Show();
        }

        private void btnWijzigen_DrankVoorraadOverzicht_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Wijzigen;
            Panel panelDrankVoorraad = pnl_KassaVoorraadoverzicht;
            ButtonHelper(panelDrankVoorraad);
            panelDrankVoorraad.Show();
        }

        private void btnVerwijderen_DrankVoorraadOverzicht_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Verwijderen;
            Panel panelDrankVoorraad = pnl_KassaVoorraadoverzicht;
            ButtonHelper(panelDrankVoorraad);
            panelDrankVoorraad.Show();

        }
        private void listView_DrankVoorraadOverzicht_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listView_Drankvoorraad = listView_VoorraadOverzicht.SelectedItems;

            if (listView_Drankvoorraad.Count > 0)
            {
                int index = listView_VoorraadOverzicht.SelectedIndices[0];
                txtProduct_VoorraadOverzicht.Text = listView_VoorraadOverzicht.Items[index].SubItems[0].Text;
                txtAantal_VoorraadOverzicht.Text = listView_VoorraadOverzicht.Items[index].SubItems[1].Text.ToString();
            }
        }

        // Event Handlers voor Drank Menu Overzicht Scherm
        private void btnVoorraad_DrankMenuOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaVoorraadKeuze.Show();
        }

        private void btnBestellingen_DrankMenuOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaKeuzeBestellingen.Show();
        }

        private void btnMenuOverzicht_DrankMenuOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaMenuoverzichtKeuze.Show();
        }

        private void btnPersoneelsbeheer_DrankMenuOverzicht_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaPersoneelsbeheer.Show();
        }

        private void btnToevoegen_DrankMenuOverzicht_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Toevoegen;
            Panel panelDrankMenu = pnl_MenuOverzicht;
            ButtonHelper(panelDrankMenu);
            panelDrankMenu.Show();
        }

        private void btnWijzigen_DrankMenuOverzicht_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Wijzigen;
            Panel panelDrankMenu = pnl_MenuOverzicht;
            ButtonHelper(panelDrankMenu);
            panelDrankMenu.Show();
        }

        private void btnVerwijderen_DrankMenuOverzicht_Click(object sender, EventArgs e)
        {
            actie = ButtonActie.Verwijderen;
            Panel panelDrankMenu = pnl_MenuOverzicht;
            ButtonHelper(panelDrankMenu);
            panelDrankMenu.Show();
        }

        // methode om items uit de voorraad of menusoort aan te passen
        private void ButtonHelper(Panel panel)
        {
            MenuItemService menuItemService = new MenuItemService();
            MedewerkerService medewerkerService = new MedewerkerService();
            Panel panelMenuOverzicht = pnl_MenuOverzicht;
            Panel panelVoorraadoverzicht = pnl_KassaVoorraadoverzicht;
            Panel panelPersoneelsbeheer = pnl_KassaPersoneelsbeheer;
            if (panel == panelMenuOverzicht)
            {
                int ID = int.Parse(txtID_MenuOverzicht.Text);
                string omschrijving = txtOmschrijving_MenuOverzicht.Text;
                int inVoorraad = int.Parse(txtInVoorraad_MenuOverzicht.Text);
                int BTW = int.Parse(txtBTW_MenuOverzicht.Text);
                string categorie = txtCategorie_MenuOverzicht.Text;
                string menuSoort = txtMenuSoort_MenuOverzicht.Text;
                float prijs = float.Parse(txtPrijs_MenuOverzicht.Text);
                if(actie == ButtonActie.Wijzigen)
                {
                    Message = menuItemService.EditAllMenuItem(ID,omschrijving, inVoorraad, BTW, categorie, menuSoort, prijs);

                } else if(actie == ButtonActie.Toevoegen)
                {
                    
                    Message = menuItemService.AddMenuItem(omschrijving, inVoorraad, BTW, categorie, menuSoort, prijs);
                } else if(actie == ButtonActie.Verwijderen)
                {
                    Message = menuItemService.DeleteMenuItem(omschrijving, inVoorraad);
                }
            }
            else if (panel == panelVoorraadoverzicht)
            {
                string product = txtProduct_VoorraadOverzicht.Text;
                int aantal = int.Parse(txtAantal_VoorraadOverzicht.Text);
                if(actie == ButtonActie.Wijzigen)
                {
                   Message = menuItemService.EditMenuItem(product, aantal);
                }
                else if(actie == ButtonActie.Verwijderen)
                {
                   Message = menuItemService.DeleteMenuItem(product, aantal);
                }
            }
            else if (panel == panelPersoneelsbeheer)
            {
                int ID = int.Parse(txt_PersoneelsbeheerID.Text);
                string voornaam = txt_VoornaamPersoneelsbeheer.Text;
                string achternaam = txt_AchternaamPersoneelsbeheer.Text;
                string type = txt_TypePersoneelsbeheer.Text;
                int inlogcode = int.Parse(txt_InlogcodePersoneelsbeheer.Text);
                if (actie == ButtonActie.Toevoegen)
                {
                   Message = medewerkerService.AddNewMedewerker(ID, voornaam, achternaam, type, inlogcode);
                }
                else if (actie == ButtonActie.Wijzigen)
                {
                  Message =  medewerkerService.UpdateMedewerker(ID, voornaam, achternaam, type, inlogcode);
                }
                else if (actie == ButtonActie.Verwijderen)
                {
                   Message = medewerkerService.DeleteMedewerker(ID);
                }
            }

            MessageBox.Show(Message);
            listView_Personeelsbeheer.Items.Clear();
            listView_VoorraadOverzicht.Items.Clear();
            lv_MenuOverzicht.Items.Clear();
            
            menuItemFiller();

        }

        private void listViewDrankmenuOverzicht_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listView_DrankMenu = lv_MenuOverzicht.SelectedItems;
            if(listView_DrankMenu.Count > 0)
            {
                int index = lv_MenuOverzicht.SelectedIndices[0];
                txtID_MenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[0].Text.ToString();
                txtOmschrijving_MenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[1].Text;
                txtInVoorraad_MenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[2].Text.ToString();
                txtBTW_MenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[3].Text.ToString();
                txtCategorie_MenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[4].Text;
                txtMenuSoort_MenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[5].Text;
                txtPrijs_MenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[6].Text.ToString();
            }
        }

        private void pictureBx_TerugTafeloverzicht_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaHoofdscherm.Show();
        }

        private void pictureBx_TerugVoorraadKeuzeOverzicht_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaHoofdscherm.Show();
        }

        private void pictureBx_TerugGerechtVoorraadOverzicht_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaVoorraadKeuze.Show();
        }

        private void pictureBx_TerugKeuzebestelling_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaHoofdscherm.Show();
        }

        private void pictureBx_TerugDrankvoorraadOverzicht_Kassa_Click(object sender, EventArgs e)
        {
            Console.WriteLine(user.ToString());
            if (user != User.Eigenaar)
            {
                this.Visible = false;
                Keuken keuken = new Keuken();
                keuken.Show();
            }
            HidePanels();
            pnl_KassaVoorraadKeuze.Show();
        }

        private void pictureBx_TerugMenuOverzichtKeuze_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaHoofdscherm.Show();
        }

        private void pictureBx_TerugLunchMenu_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaMenuoverzichtKeuze.Show();
        }

        private void pictureBox_Terug_DrankmenuoverzichtKassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaMenuoverzichtKeuze.Show();
        }

        private void pictureBx_TerugOverzichtPersoneel_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaHoofdscherm.Show();
        }


    }
}
