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
        public Login()
        {
            InitializeComponent();
        }

        // Methode om de numpads naar een inlogcode te krijgen
        private string loginString;
        void LoginCodeGenerate(string givenValue)
        {
            loginString += givenValue;
            if(loginString.Length >= 4)
            {
                int loginCode = int.Parse(loginString);
            }
        }

        // Numpad Button functies
        private void Numpadx_Click(object sender, EventArgs e)
        {

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
    }
}
