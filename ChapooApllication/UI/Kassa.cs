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

            //pnl_KassaHoofdscherm.Hide();

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
            HidePanels();
        }

        // methode to hide al panels
 

        private void btn_MenuOverzichtHoofdscherm_Kassa_Click(object sender, EventArgs e)
        {
            pnl_KassaMenuoverzichtKeuze.Show();
            pnl_KassaHoofdscherm.Hide();
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
    }
}
