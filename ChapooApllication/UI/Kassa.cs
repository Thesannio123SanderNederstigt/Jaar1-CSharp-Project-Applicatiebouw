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

namespace UI
{
    public partial class Kassa : Form
    {

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
        public Kassa()
        {
            InitializeComponent();
            
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
            HidePanels();
            pnl_KassaDrankMenuoverzicht.Show();
        }

        private void btnLunchMenu_MenuoverzichtKeuze_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaLunchMenuoverizcht.Show();
        }

        private void btnDinerMenu_MenuoverzichtKeuze_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaDinerMenuoverzicht.Show();
        }

        // Event Handlers voor VoorraadOverzichtKeuze Scherm
        private void btn_DrankVoorraadOverzichtKeuze_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaVoorraadoverzichtDrank.Show();
            listView_DrankVoorraadOverzicht.Show();

            MenuItemService menuItemService = new MenuItemService();
            List<ChapooModel.MenuItem> menuItemList = menuItemService.Get_Dranken_MenuItems();

            listView_DrankVoorraadOverzicht.Items.Clear();

            foreach (ChapooModel.MenuItem menuItem in menuItemList)
            {
                ListViewItem listViewItem = new ListViewItem(menuItem.omschrijving);
                listViewItem.SubItems.Add(menuItem.aantalInVoorraad.ToString());

                listView_DrankVoorraadOverzicht.Items.Add(listViewItem);
            }
        }
        
        private void btn_GerechtVoorraadKeuzeOverzicht_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaVoorraadoverzichtGerecht.Show();
            listview_GerechtVoorraadOverzicht.Show();

            MenuItemService menuItemService = new MenuItemService();
            List<ChapooModel.MenuItem> menuItemList = menuItemService.Get_Gerechten_MenuItems();

            listview_GerechtVoorraadOverzicht.Items.Clear();

            foreach (ChapooModel.MenuItem menuItem in menuItemList)
            {
                ListViewItem listViewItem = new ListViewItem(menuItem.omschrijving);
                listViewItem.SubItems.Add(menuItem.aantalInVoorraad.ToString());
                listview_GerechtVoorraadOverzicht.Items.Add(listViewItem);
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
            
        }

        private void btnOpslaan_DinerMenuOverzicht_Click(object sender, EventArgs e)
        {

        }

        private void btnVerwijderen_DinerMenuOverzicht_Click(object sender, EventArgs e)
        {

        }

        private void listView_DinerMenuOverzicht_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBx_TerugDinermenuOverzicht_Kassa_Click(object sender, EventArgs e)
        {

        }

        // Event Handlers voor KeuzeBestelling Scherm
        private void btn_BarKeuzeBestelling_Kassa_Click(object sender, EventArgs e)
        {

        }

        private void btn_KeukenKeuzeBestelling_Kassa_Click(object sender, EventArgs e)
        {

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

        private void btn_PersoneelsbeheerPersoneelsoverzicht_Kassa_Click(object sender, EventArgs e)
        {
            HidePanels();
            pnl_KassaPersoneelsbeheer.Show();
        }

        private void btn_InkomstenPersoneelsbeheer_Kassa_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_VerwijderenPersoneelsBeheer_Kassa_Click(object sender, EventArgs e)
        {

        }

        private void btn_OpslaanPersoneelsbeheer_Kassa_Click(object sender, EventArgs e)
        {

        }

        private void btn_ToevoegenPersoneel_Click(object sender, EventArgs e)
        {

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

        }

        private void btnOpslaan_Lunchmenuoverzicht_Click(object sender, EventArgs e)
        {

        }

        private void btnVerwijderen_Lunchmenuoverzicht_Click(object sender, EventArgs e)
        {

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
                txtProduct_DrankVoorraadOverzicht.Text = listView_DrankVoorraadOverzicht.Items[index].SubItems[0].Text.ToString();
                txtAantal_DrankVoorraadOverzicht.Text = listView_DrankVoorraadOverzicht.Items[index].SubItems[1].Text;

            }
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
            ChapooLogic.MenuItemService menuItemService = new ChapooLogic.MenuItemService();
            menuItemService.EditMenuItem(productNaam, aantal);
            pnl_KassaVoorraadoverzichtGerecht.Show();
        }

        private void btnVerwijderen_GerechtvoorraadOverzicht_Click(object sender, EventArgs e)
        {

        }

        private void listview_GerechtVoorraadOverzicht_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listView_GerechtVoorraad = listview_GerechtVoorraadOverzicht.SelectedItems;

            if(listView_GerechtVoorraad.Count > 0)
            {
                int index = listview_GerechtVoorraadOverzicht.SelectedIndices[0];
                txtProduct_GerechtVoorraadoverzicht.Text = listview_GerechtVoorraadOverzicht.Items[index].SubItems[0].Text;
                txtAantal_GerechtVoorraadoverzicht.Text = listview_GerechtVoorraadOverzicht.Items[index].SubItems[1].Text;
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

        }

        private void btnWijzigen_DrankMenuOverzicht_Click(object sender, EventArgs e)
        {

        }

        private void btnVerwijderen_DrankMenuOverzicht_Click(object sender, EventArgs e)
        {

        }


    }
}
