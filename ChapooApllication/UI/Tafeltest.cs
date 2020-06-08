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
using UI.Properties;

namespace UI
{
    public partial class Tafeltest : Form
    {
        private TafelService TafelService = new TafelService();
        private Tafel tafel;
        private List<Tafel> tafels;
        private List<Button> buttons;


        public Tafeltest()
        {
            InitializeComponent();
            buttons = new List<Button>();
            buttons.Add(btnT1);
            buttons.Add(btnT2);
            buttons.Add(btnT3);
            buttons.Add(btnT4);
            buttons.Add(btnT5);
            buttons.Add(btnT6);
            buttons.Add(btnT7);
            buttons.Add(btnT8);
            buttons.Add(btnT9);
            buttons.Add(btnT10);
            TafelStatus();
        }

        private void TafelStatus()
        {
            tafels = TafelService.GetTafel();

            for (int i = 0; i < tafels.Count; i++)
            {
                if (tafels[i].status == true)
                {
                    buttons[i].BackColor = Color.LightGreen;
                }
                else
                {
                    buttons[i].BackColor = Color.Salmon;
                }
            }
        }
        private void Tafel_Load(object sender, EventArgs e)
        {

        }

        private void TafelPNL_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnT1_Click_1(object sender, EventArgs e)
        {
            tafel = tafels[0];

            TafelPNL.Hide();

        }
        private void btnT2_Click(object sender, EventArgs e)
        {

            TafelPNL.Hide();
            tafel = tafels[1];
        }

        private void btnT3_Click_1(object sender, EventArgs e)
        {

            TafelPNL.Hide();
            tafel = tafels[2];
        }

        private void btnT4_Click_1(object sender, EventArgs e)
        {

            TafelPNL.Hide();
            tafel = tafels[3];
        }

        private void btnT5_Click_1(object sender, EventArgs e)
        {

            TafelPNL.Hide();
            tafel = tafels[4];
        }

        private void btnT6_Click_1(object sender, EventArgs e)
        {

            TafelPNL.Hide();
            tafel = tafels[5];
        }

        private void btnT7_Click_1(object sender, EventArgs e)
        {

            TafelPNL.Hide();
            tafel = tafels[6];
        }

        private void btnT8_Click_1(object sender, EventArgs e)
        {

            TafelPNL.Hide();
            tafel = tafels[7];
        }

        private void btnT9_Click_1(object sender, EventArgs e)
        {

            TafelPNL.Hide();
            tafel = tafels[8];
        }
        private void btnT10_Click_1(object sender, EventArgs e)
        {

            TafelPNL.Hide();
            tafel = tafels[9];
        }

    }
}
