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

        private void lBox_Bestelling1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();

        }

        private void btn_Tafel1More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
        }

        private void lBox_Bestelling2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
        }

        private void lBox_Bestelling3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
        }

        private void lBox_Bestelling4_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
        }

        private void lBox_Bestelling5_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
        }

        private void lBox_Bestelling6_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
        }

        private void lBox_Bestelling7_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
        }

        private void lBox_Bestelling8_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
        }

        private void lbl_Bestelling1_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_Bestelling2_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_Bestelling3_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_Bestelling4_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_Bestelling5_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_Bestelling6_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_Bestelling7_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_Bestelling8_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
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

        private void lBox_AFBestelling1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
        }

        private void btn_Tafel1AFMore_Click(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
        }

        private void lBox_AFBestelling2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
        }

        private void lBox_AFBestelling3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
        }

        private void lBox_AFBestelling4_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
        }

        private void lBox_AFBestelling5_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
        }

        private void lBox_AFBestelling6_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
        }

        private void lBox_AFBestelling7_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
        }

        private void lBox_AFBestelling8_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
        }

        private void lbl_AFBestelling1_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_AFBestelling2_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_AFBestelling3_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_AFBestelling4_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_AFBestelling5_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_AFBestelling6_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_AFBestelling7_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
        }

        private void lbl_AFBestelling8_Click(object sender, EventArgs e)
        {
            //kleur van het label aanpassen, doorgeven als globaal geselecteerde variabele (meegeven aan de code voor de gereed voor serveren knop) om te verwerken
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
