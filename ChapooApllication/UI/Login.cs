using ChapooLogic;
using ChapooModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        // Methode om de numpads naar een inlogcode te krijgen
        private string loginString = "";
        public static string Username = "";

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
            DisableWarning();
        }

        public void EnableWarning()
        {
            pnl_LoginWarning.Visible = true;
            pnl_LoginWarning.Enabled = true;
            pnl_Login.Enabled = false;
        }

        public void DisableWarning()
        {
            pnl_LoginWarning.Visible = false;
            pnl_LoginWarning.Enabled = false;
            pnl_Login.Enabled = true;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            // Tonen van juiste Form op basis van inlogcode + functie van gebruiker
            if (loginString.Length == 4)
            {
                int loginCode = int.Parse(loginString);

                MedewerkerService medewerkerservice = new MedewerkerService();
                Medewerker medewerker = medewerkerservice.GetByLogincode(loginCode);
                Username = $"{medewerker.voornaam} {medewerker.achternaam}";

                if (medewerker.type == "eigenaar")
                {
                    Kassa kassa = new Kassa();
                    kassa.Show();
                    //this.Visible = false;
                }
                else if (medewerker.type == "chef-kok")
                {
                    Keuken keuken = new Keuken();
                    keuken.Show();
                    //this.Visible = false;
                }
                else if (medewerker.type == "bediening")
                {
                    Handheld handheld = new Handheld();
                    handheld.Show();
                    //this.Visible = false;
                }
                else if (medewerker.type == "barmedewerker")
                {
                    Bar bar = new Bar();
                    bar.Show();
                    //this.Visible = false;
                }
                else
                {
                    EnableWarning();
                }
                loginString = "";
                lbl_CurrentLogincode.Text = loginString;
            }
            else if(loginString.Length != 4)
            {
                EnableWarning();
                loginString = "";
                lbl_CurrentLogincode.Text = loginString;
            }
        }
    }
}
