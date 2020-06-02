  using ChapooLogic;
using ChapooModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class Keuken : Form
    {
        public void HidePanels()
        {
            //pnl_KeukenBarStart().Hide();
            pnl_BinnenkomendeBestellingen.Hide();
            pnl_TafelBinnenkomendeBestelling.Hide();
            pnl_AfgerondeBestellingen.Hide();
            pnl_TafelAfgerondeBestelling.Hide();
        }

        public Keuken()
        {
            InitializeComponent();
            HidePanels();
            Select8Orders();
        }

        private void Select8Orders()
        {
            for (int i = 1; i < 9; i++)
            {
                BestellingService bestelservice = new BestellingService();
                List<Bestelling> bestellinglist = bestelservice.GetEightOrders();
                ListViewItem li = new ListViewItem();

                string listviewname = "lv_Tafel";

                foreach (Bestelling b in bestellinglist)
                {
                    listviewname += listviewname + i;

                    //invullen in de juiste listview

                    /* moet nog aangepast worden naar juiste DB teruggave
                    li.SubItems.Add(b.omschrijving);
                    li.SubItems.Add(b.aantal);
                    */

                    // TODO: kijken hoe we de listviews oproepen dmv string/loop
                    
                }


            }

        }

        //eventhandlers voor op het startscherm panel (pnl_KeukenBarStart)
        private void pictureBx_Uitloggen_Keuken_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            Login login = new Login();
            login.Show();
        }

        private void btn_StartInkomende_Click(object sender, EventArgs e)
        {
            pnl_KeukenBarStart.Hide();
            pnl_BinnenkomendeBestellingen.Show();
        }

        private void btn_StartAfgeronde_Click(object sender, EventArgs e)
        {
            pnl_KeukenBarStart.Hide();
            pnl_AfgerondeBestellingen.Show();
        }

        //eventhandlers/methoden op/binnen het pnl_BinnenkomendeBestellingen
        private void pictureBx_KeukenBarStartscherm_Keuken_Click(object sender, EventArgs e)
        {
            pnl_BinnenkomendeBestellingen.Hide();
            pnl_KeukenBarStart.Show();
        }


        // Listviews binnenkomende bestellingen
        private void lv_Tafel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling1.ForeColor = Color.Orange;
        }


        private void btn_Tafel1More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();

        }


        private void lv_Tafel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling2.ForeColor = Color.Orange;

        }

        private void lv_Tafel3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling3.ForeColor = Color.Orange;

        }

        private void lv_Tafel4_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling4.ForeColor = Color.Orange;

        }

        private void lv_Tafel5_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling5.ForeColor = Color.Orange;

        }

        private void lv_Tafel6_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling6.ForeColor = Color.Orange;

        }

        private void lv_Tafel7_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling7.ForeColor = Color.Orange;

        }

        private void lv_Tafel8_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling8.ForeColor = Color.Orange;

        }


        // Groupboxes voor binnengekomen bestelingen
        private void gBox_Tafel1_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_Tafel2_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_Tafel3_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_Tafel4_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_Tafel5_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_Tafel6_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_Tafel7_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_Tafel8_Enter(object sender, EventArgs e)
        {

        }


        private void btn_GereedBestelling_Click(object sender, EventArgs e)
        {
            //verwerken van gereed melden geselecteerde bestelling (welk label is geselecteerd)
        }

        private void btn_AfgerondeBestelling_Click(object sender, EventArgs e)
        {
            pnl_BinnenkomendeBestellingen.Hide();
            pnl_AfgerondeBestellingen.Show();
        }

        private void btn_VerwijderBestelling_Click(object sender, EventArgs e)
        {
            //verwijderen van geselecteerde bestelling (welk label is geselecteerd)
        }


        //eventhandlers/methoden voor het tafel panel van de inkomende bestellingen (pnl_TafelBinnenkomendeBestelling)
        private void pictureBx_TerugInkomendeBestellingen_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Hide();
        }

        private void btnGereedMenuItem_Click_1(object sender, EventArgs e)
        {
            //bestelling items gereed melden
            //...

            pnl_TafelBinnenkomendeBestelling.Hide();
        }

        //eventhandlers/methoden die op het Afgerondenbestellingen panel staan
        private void pictureBxAF_KeukenBarStartscherm_Keuken_Click(object sender, EventArgs e)
        {
            pnl_AfgerondeBestellingen.Hide();
            pnl_KeukenBarStart.Show();
        }

        // Listviews Afgeronde bestellingen
        private void lv_AfBestelling1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling1.ForeColor = Color.Orange;
        }

        private void lv_AfBestelling2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling2.ForeColor = Color.Orange;

        }

        private void lv_AfBestelling3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling3.ForeColor = Color.Orange;

        }

        private void lv_AfBestelling4_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling4.ForeColor = Color.Orange;

        }

        private void lv_AfBestelling5_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling5.ForeColor = Color.Orange;

        }

        private void lv_AfBestelling6_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling6.ForeColor = Color.Orange;

        }

        private void lv_AfBestelling7_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling7.ForeColor = Color.Orange;

        }

        private void lv_AfBestelling8_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling8.ForeColor = Color.Orange;

        }

        // Groupboxes voor afgeronde bestellingen
        private void gBox_AFTafel1_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_AFTafel2_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_AFTafel3_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_AFTafel4_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_AFTafel5_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_AFTafel6_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_AFTafel7_Enter(object sender, EventArgs e)
        {

        }

        private void gBox_AFTafel8_Enter(object sender, EventArgs e)
        {

        }

        private void btn_Binnenkomendestelling_Click(object sender, EventArgs e)
        {
            pnl_AfgerondeBestellingen.Hide();
            pnl_BinnenkomendeBestellingen.Show();
        }

        private void btn_VerwijderAfgerondeBestelling_Click(object sender, EventArgs e)
        {
            //verwijderen van geselecteerde bestelling (welk label is geselecteerd)
        }


        //eventhandler/methode die op het afgeronde tafel bestelling panel (pnl_TafelAfgerondeBestelling) staat
        private void pictureBx_TerugAfgerondeBestellingen_Click(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Hide();
        }

        private void Keuken_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Environment.Exit(0);
            }
        }

        // TODO: Optioneel nog een panel toevoegen
        private void btn_StartVoorraad_Click(object sender, EventArgs e)
        {
            // Hide en Show de goede panels
            HidePanels();

        }


    }
}
