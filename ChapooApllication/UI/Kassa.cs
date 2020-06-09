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

namespace UI
{
    public partial class Kassa : Form
    {
        string Type = Login.MedewerkerType;
        public static string Bestellingoverzicht = "";
        private static string menuKeuze = "";
        public void HidePanels()
        {
            pnl_Inkomstenoverzicht.Hide();
            pnl_KassaDinerMenuoverzicht.Hide();
            pnl_KassaDrankMenuoverzicht.Hide();
            pnl_KassaHoofdscherm.Hide();
            pnl_KassaKeuzeBestellingen.Hide();
            pnl_KassaLunchMenuoverizcht.Hide();
            pnl_KassaMenuoverzichtKeuze.Hide();
            pnl_KassaPersoneelsbeheer.Hide();
            pnl_KassaTafeloverzicht.Hide();
            pnl_KassaVoorraadKeuze.Hide();
            pnl_KassaVoorraadoverzichtDrank.Hide();
            pnl_KassaVoorraadoverzichtGerecht.Hide();
        }

        public void RefreshVoorraadGerecht(MenuItemService menuItemService)
        {
            List<MenuItem> menuItemList = menuItemService.Get_Gerechten_MenuItems();

            foreach (MenuItem menuItem in menuItemList)
            {
                ListViewItem listViewItem = new ListViewItem(menuItem.omschrijving);
                listViewItem.SubItems.Add(menuItem.aantalInVoorraad.ToString());
                listview_GerechtVoorraadOverzicht.Items.Add(listViewItem);
            }
        }
        public Kassa()
        {
            InitializeComponent();

            HidePanels();
            pnl_KassaHoofdscherm.Show();

            if (Type == "chef-kok")
            {
                pnl_KassaVoorraadoverzichtGerecht.Show();
            }
            else if(Type == "barmedewerker")
            {
                pnl_KassaVoorraadoverzichtDrank.Show();
            }

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

        private void btn_BestellingenHoofdscherm_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaKeuzeBestellingen.Show();
        }

        // Event handlers voor Menu Overzicht Keuze
        private void btnDrankMenu_MenuoverzichtKeuze_Click(object sender, EventArgs e)
        {
            menuKeuze = "DrankMenu";
            lbl_Overzicht.Text = "Drank Overzicht";
            HidePanels();
            pnl_KassaDrankMenuoverzicht.Show();
            lv_MenuOverzicht.Items.Clear();

            menuItemFiller(menuKeuze);
        }

        private void btnLunchMenu_MenuoverzichtKeuze_Click(object sender, EventArgs e)
        {
            menuKeuze = "LunchMenu";
            lbl_Overzicht.Text = "Lunch Overzicht";
            HidePanels();
            pnl_KassaDrankMenuoverzicht.Show();
            lv_MenuOverzicht.Items.Clear();

            menuItemFiller(menuKeuze);
        }

        private void btnDinerMenu_MenuoverzichtKeuze_Click(object sender, EventArgs e)
        {
            menuKeuze = "DinerMenu";
            lbl_Overzicht.Text = "Diner Overzicht";
            HidePanels();
            pnl_KassaDrankMenuoverzicht.Show();
            lv_MenuOverzicht.Items.Clear();

            menuItemFiller(menuKeuze);

        }

        public List<ListViewItem> menuItemFiller(string menuKeuze)
        {
            MenuItemService menuItemService = new MenuItemService();
            List<ListViewItem> listviewlist = new List<ListViewItem>();
            List<MenuItem> thisList = new List<MenuItem>();
            if (menuKeuze == "DrankMenu")
            {
                thisList = menuItemService.Get_Dranken_MenuItems();
            }
            else if (menuKeuze == "LunchMenu")
            {
                thisList = menuItemService.Get_LunchMenuKaart();
            }
            else if (menuKeuze == "DinerMenu")
            {
                thisList = menuItemService.Get_DinerMenuKaart();
            }
            foreach (MenuItem m in thisList)
            {
                ListViewItem listViewItem = new ListViewItem(m.ID.ToString());
                listViewItem.SubItems.Add(m.omschrijving);
                listViewItem.SubItems.Add(m.aantalInVoorraad.ToString());
                listViewItem.SubItems.Add(m.btw.ToString());
                listViewItem.SubItems.Add(m.categorie);
                listViewItem.SubItems.Add(m.menukaartsoort);
                listViewItem.SubItems.Add(m.prijs.ToString("€ 0.00"));

                lv_MenuOverzicht.Items.Add(listViewItem);
                listviewlist.Add(listViewItem);
            }

            return listviewlist;
        }

        // Event Handlers voor VoorraadOverzichtKeuze Scherm
        private void btn_DrankVoorraadOverzichtKeuze_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            listView_DrankVoorraadOverzicht.Show();

            MenuItemService menuItemService = new MenuItemService();
            List<MenuItem> menuItemList = menuItemService.Get_Dranken_MenuItems();

            listView_DrankVoorraadOverzicht.Items.Clear();

            foreach (MenuItem menuItem in menuItemList)
            {
                ListViewItem listViewItem = new ListViewItem(menuItem.omschrijving);
                listViewItem.SubItems.Add(menuItem.aantalInVoorraad.ToString());

                listView_DrankVoorraadOverzicht.Items.Add(listViewItem);
            }
            pnl_KassaVoorraadoverzichtDrank.Show();
        }
        
        private void btn_GerechtVoorraadKeuzeOverzicht_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            listview_GerechtVoorraadOverzicht.Items.Clear();
            pnl_KassaVoorraadoverzichtGerecht.Show();
            
            MenuItemService menuItemService = new MenuItemService();
            RefreshVoorraadGerecht(menuItemService);
 
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

        private void btnWijzigen_GerechtVoorraadOverzicht_Click(object sender, EventArgs e)
        {
            string productNaam = txtProduct_GerechtVoorraadoverzicht.Text;
            int aantal = int.Parse(txtAantal_GerechtVoorraadoverzicht.Text);
            MenuItemService menuItemService = new MenuItemService();
            menuItemService.EditMenuItem(productNaam, aantal);

            txtProduct_GerechtVoorraadoverzicht.Clear();
            txtAantal_GerechtVoorraadoverzicht.Clear();
            listview_GerechtVoorraadOverzicht.Items.Clear();

            RefreshVoorraadGerecht(menuItemService);
            pnl_KassaVoorraadoverzichtGerecht.Show();

        }

        private void btnVerwijderen_GerechtvoorraadOverzicht_Click(object sender, EventArgs e)
        {
            string productNaam = txtProduct_GerechtVoorraadoverzicht.Text;
            int aantal = int.Parse(txtAantal_GerechtVoorraadoverzicht.Text);

            MenuItemService menuItemService = new MenuItemService();
            menuItemService.DeleteMenuItem(productNaam, aantal);

            txtProduct_GerechtVoorraadoverzicht.Clear();
            txtAantal_GerechtVoorraadoverzicht.Clear();
            listview_GerechtVoorraadOverzicht.Items.Clear();

            RefreshVoorraadGerecht(menuItemService);
            pnl_KassaVoorraadoverzichtGerecht.Show();

        }

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

        private void btnToevoegen_DinerMenuOverzicht_Click(object sender, EventArgs e)
        {
            MenuItemService menuItemService = new MenuItemService();
            int id = int.Parse(txtID_DinerMenuOverzicht.Text);
            string omschrijving = txtOmschrijving_DinerMenuOverzicht.Text;
            int inVoorraad = int.Parse(txtInVoorraad_DinermenuOverzicht.Text);
            int BTW = int.Parse(txtBTW_DinerMenuOverzicht.Text);
            string categorie = txtCategorie_DinerMenuOverzicht.Text;
            string menuSoort = txtMenuSoort_DinerMenuOverzicht.Text;
            float prijs = float.Parse(txtPrijs_DinerMenuOverzicht.Text);

            menuItemService.AddMenuItem(id, omschrijving, inVoorraad, BTW, categorie, menuSoort, prijs);
            pnl_KassaDinerMenuoverzicht.Show();
        }

        private void btnOpslaan_DinerMenuOverzicht_Click(object sender, EventArgs e)
        {
            listView_DinerMenuOverzicht.Items.Clear();
            MenuItemService menuItemService = new MenuItemService();
            int ID = int.Parse(txtID_DinerMenuOverzicht.Text);
            string omschrijving = txtOmschrijving_DinerMenuOverzicht.Text;
            int inVoorraad = int.Parse(txtInVoorraad_DinermenuOverzicht.Text);
            int BTW = int.Parse(txtBTW_DinerMenuOverzicht.Text);
            string categorie = txtCategorie_DinerMenuOverzicht.Text;
            string menuSoort = txtMenuSoort_DinerMenuOverzicht.Text;
            float prijs = float.Parse(txtPrijs_DinerMenuOverzicht.Text);
            menuItemService.EditAllMenuItem(ID, omschrijving, inVoorraad, BTW, categorie, menuSoort, prijs);
          
        }

        private void btnVerwijderen_DinerMenuOverzicht_Click(object sender, EventArgs e)
        {
            listView_DinerMenuOverzicht.Items.Clear();
            MenuItemService menuItemService = new MenuItemService();
            string product = txtOmschrijving_DinerMenuOverzicht.Text;
            int aantal = int.Parse(txtInVoorraad_DinermenuOverzicht.Text);
            menuItemService.DeleteMenuItem(product, aantal);

            txtOmschrijving_DinerMenuOverzicht.Clear();
            txtInVoorraad_DinermenuOverzicht.Clear();
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
            Bestellingoverzicht = "bar";
            Keuken keuken = new Keuken();
            keuken.Show();
        }

        private void btn_KeukenKeuzeBestelling_Kassa_Click(object sender, EventArgs e)
        {
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
            // listView_Personeelsbeheer.Items.Clear();
            MedewerkerService medewerkerService = new MedewerkerService();
            int ID = int.Parse(txt_PersoneelsbeheerID.Text);
            medewerkerService.DeleteMedewerker(ID);
        }
        // button om te wijzigen
        private void btn_OpslaanPersoneelsbeheer_Kassa_Click(object sender, EventArgs e)
        {
            //listView_Personeelsbeheer.Items.Clear();
            MedewerkerService medewerkerService = new MedewerkerService();
            int ID = int.Parse(txt_PersoneelsbeheerID.Text);
            string voornaam = txt_VoornaamPersoneelsbeheer.Text;
            string achternaam = txt_AchternaamPersoneelsbeheer.Text;
            string type = txt_TypePersoneelsbeheer.Text;
            int inlogcode = int.Parse(txt_InlogcodePersoneelsbeheer.Text);
            medewerkerService.UpdateMedewerker(ID, voornaam, achternaam, type, inlogcode);
        }

        private void btn_ToevoegenPersoneel_Click(object sender, EventArgs e)
        {
            //listView_Personeelsbeheer.Items.Clear();
            MedewerkerService medewerkerService = new MedewerkerService();
            int ID = int.Parse(txt_PersoneelsbeheerID.Text);
            string voornaam = txt_VoornaamPersoneelsbeheer.Text;
            string achternaam = txt_AchternaamPersoneelsbeheer.Text;
            string type = txt_TypePersoneelsbeheer.Text;
            int inlogcode = int.Parse(txt_InlogcodePersoneelsbeheer.Text);
            medewerkerService.AddNewMedewerker(ID, voornaam, achternaam, type, inlogcode);
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
            // listView_LunchMenuOverzicht_Kassa.Items.Clear();
            MenuItemService menuItemService = new MenuItemService();
            int id = int.Parse(txtID_Lunchmenuoverzicht.Text);
            string omschrijving = txtOmschrijving_Lunchmenuoverzicht.Text;
            int inVoorraad = int.Parse(txtInVoorraad_Lunchmenuoverzicht.Text);
            int BTW = int.Parse(txtBTW_Lunchmenuoverzicht.Text);
            string categorie = txtCategorie_Lunchmenuoverzicht.Text;
            string menuSoort = txtMenuSoort_Lunchmenuoverzicht.Text;
            float prijs = float.Parse(txtPrijs_Lunchmenuoverzicht.Text);

            menuItemService.AddMenuItem(id, omschrijving, inVoorraad, BTW, categorie, menuSoort, prijs);
            pnl_KassaLunchMenuoverizcht.Show();
        }

        private void btnOpslaan_Lunchmenuoverzicht_Click(object sender, EventArgs e)
        {
            // listView_LunchMenuOverzicht_Kassa.Items.Clear();
            MenuItemService menuItemService = new MenuItemService();
            int ID = int.Parse(txtID_Lunchmenuoverzicht.Text);
            string omschrijving = txtOmschrijving_Lunchmenuoverzicht.Text;
            int inVoorraad = int.Parse(txtInVoorraad_Lunchmenuoverzicht.Text);
            int BTW = int.Parse(txtBTW_Lunchmenuoverzicht.Text);
            string categorie = txtCategorie_Lunchmenuoverzicht.Text;
            string menuSoort = txtMenuSoort_Lunchmenuoverzicht.Text;
            float prijs = float.Parse(txtPrijs_Lunchmenuoverzicht.Text);
            menuItemService.EditAllMenuItem(ID, omschrijving, inVoorraad, BTW, categorie, menuSoort, prijs);

        }

        private void btnVerwijderen_Lunchmenuoverzicht_Click(object sender, EventArgs e)
        {
            // listView_LunchMenuOverzicht_Kassa.Items.Clear();
            string productNaam = txtOmschrijving_Lunchmenuoverzicht.Text;
            int aantal = int.Parse(txtInVoorraad_Lunchmenuoverzicht.Text);

            MenuItemService menuItemService = new MenuItemService();
            menuItemService.DeleteMenuItem(productNaam, aantal);

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
            string product = txtProduct_DrankVoorraadOverzicht.Text;
            int aantal = int.Parse(txtAantal_DrankVoorraadOverzicht.Text);

            MenuItemService menuItemService = new MenuItemService();
            menuItemService.EditMenuItem(product, aantal);
            pnl_KassaVoorraadoverzichtDrank.Show();
            listView_DrankVoorraadOverzicht.Show();
        }

        private void btnVerwijderen_DrankVoorraadOverzicht_Click(object sender, EventArgs e)
        {
            string product = txtProduct_DrankVoorraadOverzicht.Text;
            int aantal = int.Parse(txtAantal_DrankVoorraadOverzicht.Text);

            MenuItemService menuItemService = new MenuItemService();
            menuItemService.DeleteMenuItem(product, aantal);
            pnl_KassaVoorraadoverzichtDrank.Show();
            listView_DrankVoorraadOverzicht.Show();

        }
        private void listView_DrankVoorraadOverzicht_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listView_Drankvoorraad = listView_DrankVoorraadOverzicht.SelectedItems;

            if (listView_Drankvoorraad.Count > 0)
            {
                int index = listView_DrankVoorraadOverzicht.SelectedIndices[0];
                txtProduct_DrankVoorraadOverzicht.Text = listView_DrankVoorraadOverzicht.Items[index].SubItems[0].Text;
                txtAantal_DrankVoorraadOverzicht.Text = listView_DrankVoorraadOverzicht.Items[index].SubItems[1].Text.ToString();

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
            // listViewDrankmenuOverzicht.Items.Clear();
            MenuItemService menuItemService = new MenuItemService();
            int ID = int.Parse(txtID_DrankMenuOverzicht.Text);
            string omschrijving = txtOmschrijving_DrankMenuOverzicht.Text;
            int inVoorraad = int.Parse(txtInVoorraad_DrankMenuOverzicht.Text);
            int BTW = int.Parse(txtBTW_DrankMenuOverzicht.Text);
            string categorie = txtCategorie_DrankMenuOverzicht.Text;
            string menuSoort = txtMenuSoort_DrankMenuOverzicht.Text;
            float prijs = float.Parse(txtPrijs_DrankMenuOverzicht.Text);
            menuItemService.AddMenuItem(ID, omschrijving, inVoorraad, BTW, categorie, menuSoort, prijs);
        }

        private void btnWijzigen_DrankMenuOverzicht_Click(object sender, EventArgs e)
        {
            // listViewDrankmenuOverzicht.Items.Clear();
            MenuItemService menuItemService = new MenuItemService();
            int ID = int.Parse(txtID_DrankMenuOverzicht.Text);
            string omschrijving = txtOmschrijving_DrankMenuOverzicht.Text;
            int inVoorraad = int.Parse(txtInVoorraad_DrankMenuOverzicht.Text);
            int BTW = int.Parse(txtBTW_DrankMenuOverzicht.Text);
            string categorie = txtCategorie_DrankMenuOverzicht.Text;
            string menuSoort = txtMenuSoort_DrankMenuOverzicht.Text;
            float prijs = float.Parse(txtPrijs_DrankMenuOverzicht.Text);
            menuItemService.EditAllMenuItem(ID, omschrijving, inVoorraad, BTW, categorie, menuSoort, prijs);
        }

        private void btnVerwijderen_DrankMenuOverzicht_Click(object sender, EventArgs e)
        {
            string productNaam = txtOmschrijving_DrankMenuOverzicht.Text;
            int aantal = int.Parse(txtInVoorraad_DrankMenuOverzicht.Text);

            MenuItemService menuItemService = new MenuItemService();
            menuItemService.DeleteMenuItem(productNaam, aantal);
        }

        private void listViewDrankmenuOverzicht_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listView_DrankMenu = lv_MenuOverzicht.SelectedItems;
            if(listView_DrankMenu.Count > 0)
            {
                int index = lv_MenuOverzicht.SelectedIndices[0];
                txtID_DrankMenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[0].Text.ToString();
                txtOmschrijving_DrankMenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[1].Text;
                txtInVoorraad_DrankMenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[2].Text.ToString();
                txtBTW_DrankMenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[3].Text.ToString();
                txtCategorie_DrankMenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[4].Text;
                txtMenuSoort_DrankMenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[5].Text;
                txtPrijs_DrankMenuOverzicht.Text = lv_MenuOverzicht.Items[index].SubItems[6].Text.ToString();
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
