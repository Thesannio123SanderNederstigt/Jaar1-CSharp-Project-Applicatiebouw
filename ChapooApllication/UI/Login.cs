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
    public partial class Login : Form
    {
        // method to hide all other panels
        void HidePanels()
        {
            pnl_LoginWarning.Hide();
        }
        public Login()
        {
            InitializeComponent();
            HidePanels();

        }

        // Methode om de numpads naar een inlogcode te krijgen
        private string loginString = "";
        void LoginCodeGenerate(string givenValue)
        {
            if (loginString.Length >= 1 && givenValue == "X")
            {
                loginString = loginString.Remove(loginString.Length - 1, 1);
            } else if(loginString.Length == 0 && givenValue == "X")
            {
                loginString = "";
            }
            else
            {
                loginString += givenValue;
            }
            lbl_CurrentLogincode.Text = loginString;


            if (loginString.Length == 4)
            {
                int loginCode = int.Parse(loginString);
                MedewerkerService medewerkerservice = new MedewerkerService();
                Medewerker medewerker = medewerkerservice.GetByLogincode(loginCode);

                if (medewerker.type == "eigenaar")
                {
                    Kassa kassa = new Kassa();
                    kassa.Show();
                }if(medewerker.type == "chef-kok")
                {
                    Keuken keuken = new Keuken();
                    keuken.Show();
                }if(medewerker.type == "bediening")
                {
                    Handheld handheld = new Handheld();
                    handheld.Show();
                } if(medewerker.type == "barmedewerker")
                {
                    Bar bar = new Bar();
                    bar.Show();
                }
                else if(medewerker == null)
                {
                    pnl_LoginWarning.Show();
                }

                loginString = "";
                lbl_CurrentLogincode.Text = medewerker.voornaam;
            }
        }

        // Numpad Button functies
        private void Numpadx_Click(object sender, EventArgs e)
        {
            LoginCodeGenerate("X");
        }
        private void Numpad0_Click(object sender, EventArgs e)
        {
            LoginCodeGenerate(Numpad0.Text);
        }

        private void Numpad1_Click(object sender, EventArgs e)
        {
            LoginCodeGenerate(Numpad1.Text);
        }

        private void Numpad2_Click(object sender, EventArgs e)
        {
            LoginCodeGenerate(Numpad2.Text);
        }

        private void Numpad3_Click(object sender, EventArgs e)
        {
            LoginCodeGenerate(Numpad3.Text);
        }

        private void Numpad4_Click(object sender, EventArgs e)
        {
            LoginCodeGenerate(Numpad4.Text);
        }

        private void Numpad5_Click(object sender, EventArgs e)
        {
            LoginCodeGenerate(Numpad5.Text); ;
        }

        private void Numpad6_Click(object sender, EventArgs e)
        {
            LoginCodeGenerate(Numpad6.Text);
        }

        private void Numpad7_Click(object sender, EventArgs e)
        {
            LoginCodeGenerate(Numpad7.Text);
        }

        private void Numpad8_Click(object sender, EventArgs e)
        {
            LoginCodeGenerate(Numpad8.Text);
        }

        private void Numpad9_Click(object sender, EventArgs e)
        {
            LoginCodeGenerate(Numpad9.Text);
        }

        private void btn_LoginWarningOK_Click(object sender, EventArgs e)
        {
            HidePanels();
        }
    }
}
