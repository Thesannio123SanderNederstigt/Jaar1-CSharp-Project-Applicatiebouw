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
        }
    }
}
