using ChapooLogic;
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
    public partial class Bediening : Form
    {
        private MenuItemService MenuItemService = new MenuItemService();
        private RekeningService RekeningService = new RekeningService();
        private TafelService TafelService = new TafelService();
        private BestellingService BestellingService = new BestellingService();
        public Bediening()
        {
            InitializeComponent();
            
        }

        private void Bediening_Load(object sender, EventArgs e)
        {
            lblB2.Text = DateTime.Now.ToShortDateString();
            lblB1.Text = DateTime.Now.ToShortTimeString();
            ShowPanel("TafelOverzicht");
        }
        private void ShowPanel(string PanelName)
        { 
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ben je zeker dat je wilt afmelden? Alles  ", "Afmelden", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                MessageBox.Show("Terug naar hoofscherm", "Bestelling opgeslagen", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Activity Not Deleted", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button12_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private bool isNeer;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isNeer)
            {
                LunchVoorBTN.Image = Resources.Collapse_Arrow_20px;
                panel11Dropdown.Height += 10;
                if (panel11Dropdown.Size == panel11Dropdown.MaximumSize)
                {
                    timer1.Stop();
                    isNeer = false;
                }
            }
            else
            {
                LunchVoorBTN.Image = Resources.Expand_Arrow_20px;
                panel11Dropdown.Height -= 10;
                if (panel11Dropdown.Size == panel11Dropdown.MinimumSize)
                {
                    timer1.Stop();
                    isNeer = true;
                }
            }
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isNeer)
            {
                button21.Image = Resources.Collapse_Arrow_20px;
                panel12.Height += 10;
                if (panel12.Size == panel12.MaximumSize)
                {
                    timer2.Stop();
                    isNeer = false;
                }
            }
            else
            {
                button21.Image = Resources.Expand_Arrow_20px;
                panel12.Height -= 10;
                if (panel12.Size == panel12.MinimumSize)
                {
                    timer2.Stop();
                    isNeer = true;
                }
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            

        }

        private void timer4_Tick(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            timer3.Start();
            
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnT1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void DinerPNL_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DrankPNL_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click_1(object sender, EventArgs e)
        {

        }

        private void DinerNBTN_Click(object sender, EventArgs e)
        {

        }

        private void DinerHBTN_Click(object sender, EventArgs e)
        {

        }

        private void DinerTBTN_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button29_Click(object sender, EventArgs e)
        {

        }

        private void button30_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {

        }
    }
}
