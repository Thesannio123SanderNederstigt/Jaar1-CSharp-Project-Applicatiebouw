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
    public enum User
    {
        Eigenaar,
        ChefKok,
        Barpersoneel,
        Bediening
    }
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        // Methode om de numpads naar een inlogcode te krijgen
        private string loginString = "";
        public static string Username = "";
        //public static string MedewerkerType = "";
        public static User user;

        void LoginCodeGenerate(string givenValue)
        {
            if (txt_LoginScherm.Text.Length >= 1 && givenValue == "X")
            {
                txt_LoginScherm.Text = txt_LoginScherm.Text.Remove(txt_LoginScherm.Text.Length - 1, 1);
            }
            else if (txt_LoginScherm.Text.Length == 0 && givenValue == "X")
            {
                txt_LoginScherm.Text = "";
            }
            else
            {
                txt_LoginScherm.Text += givenValue;
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
            txt_LoginScherm.Clear();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            loginString = txt_LoginScherm.Text;
            // Tonen van juiste Form op basis van inlogcode + functie van gebruiker
            if (loginString.Length == 4)
            {
                int loginCode;
                if (!int.TryParse(txt_LoginScherm.Text, out loginCode))
                {
                    EnableWarning();
                }
                else
                {
                    loginCode = int.Parse(loginString);
                }
                MedewerkerService medewerkerservice = new MedewerkerService();
                Medewerker medewerker = medewerkerservice.GetByLogincode(loginCode);
                Username = $"{medewerker.voornaam} {medewerker.achternaam}";

                if (medewerker.type == "eigenaar")
                {
                    user = User.Eigenaar;
                    //MedewerkerType = medewerker.type;
                    Kassa kassa = new Kassa();
                    kassa.Show();
                    this.Visible = false;
                }
                else if (medewerker.type == "chef-kok")
                {
                    user = User.ChefKok;
                    //MedewerkerType = medewerker.type;
                    Keuken keuken = new Keuken();
                    keuken.Show();
                    this.Visible = false;
                }
                else if (medewerker.type == "bediening")
                {
                    user = User.Bediening;
                    //MedewerkerType = medewerker.type;
                    Bediening bediening = new Bediening();
                    bediening.Show();
                    this.Visible = false;
                }
                else if (medewerker.type == "barmedewerker")
                {
                    user = User.Barpersoneel;
                    //MedewerkerType = medewerker.type;
                    Keuken bar = new Keuken();
                    bar.Show();
                    this.Visible = false;
                }
                else
                {
                    Console.WriteLine(loginString);
                    EnableWarning();
                }
                loginString = "";
                lbl_CurrentLogincode.Text = loginString;
            }
            else if (loginString.Length != 4)
            {
                EnableWarning();
                Console.WriteLine(loginString);
                loginString = "";
                lbl_CurrentLogincode.Text = loginString;
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Environment.Exit(0);
            }
        }
    }
}
