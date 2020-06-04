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
    public partial class Bediening : Form
    {
        private MenuItemService MenuItemService = new MenuItemService();
        private RekeningService RekeningService = new RekeningService();
        private TafelService TafelService = new TafelService();
        private BestellingService BestellingService = new BestellingService();

        private void BTNReturn_Click(object sender, EventArgs e)
        {
            
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ben je zeker dat je wilt afmelden?\n" +
                                "      Er wordt niks opgeslagen", "Afmelden", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public Bediening()
        {
            InitializeComponent();     
        }

        private void Bediening_Load(object sender, EventArgs e)
        {

            TijdLBL.Text = DateTime.Now.ToShortDateString();
            DatumLBL.Text = DateTime.Now.ToShortTimeString();
            ShowPanel("TafelPNL");
        }
        private void HidePanels()
        {
            DrankPNL.Hide();
            TafelNummerPNL.Hide();
            BestellingPNL.Hide();
            WijzigenPNL.Hide();
            LunchPNL.Hide();
            DinerPNL.Hide();
            OverzichtPNL.Hide();
            

        }
        private void ShowPanel(string PanelName)
        {
            HidePanels();

            if (PanelName == "TafelPNL")
            {
                TafelPNL.Show();
            }

            else if (PanelName == "TafelNummerPNL")
            {

            }

            else if (PanelName == "BestellingPNL")
            {

            }

            else if (PanelName == "OverzichtPNL")
            {

            }

            else if (PanelName == "WijzigenPNL")
            {

            }
        }
        //
        public void PlusClick(Label Plus) //Void voor alle plus knoppen 
        {
            int AantalLBL = int.Parse(Plus.Text);
            AantalLBL++;
            Plus.Text = AantalLBL.ToString();
        }

        public void MinClick(Label Min) //void voor alle min knoppen
        {
            int AantalLBL = int.Parse(Min.Text);
            if (AantalLBL > 0)
            {
                AantalLBL--;
                Min.Text = AantalLBL.ToString();
            }
        }

        public void VulListView(string MenuSoort, string Categorie, ListView ListViewNaam)
        {
            ListViewNaam.Items.Clear();

            List<ChapooModel.MenuItem> ViewList = MenuItemService.GetItems($"{MenuSoort}", $"{Categorie}");

            foreach (ChapooModel.MenuItem M in ViewList)
            {
                ListViewItem List = new ListViewItem(M.ID.ToString());
                List.Tag = M;
                List.SubItems.Add(M.omschrijving);
                List.SubItems.Add(M.aantalInVoorraad.ToString());

                ListViewNaam.Items.Add(List);
            }
        }
        private bool isNeer;
        public void TimerClick(Timer TimerNaam, Button BTNNaam, Panel DropDownNaam)
        {
            if (isNeer)
            {
                BTNNaam.Image = Resources.Collapse_Arrow_20px;
                DropDownNaam.Height += 10;
                if (DropDownNaam.Size == DropDownNaam.MaximumSize)
                {
                    TimerNaam.Stop();
                    isNeer = false;
                }
            }
            else
            {
                BTNNaam.Image = Resources.Expand_Arrow_20px;
                DropDownNaam.Height -= 10;
                if (DropDownNaam.Size == DropDownNaam.MinimumSize)
                {
                    TimerNaam.Stop();
                    isNeer = true;
                }
            }
        }
        private void BTNLunch_Click(object sender, EventArgs e)
        {
            LunchPNL.Show();
            TafelNummerPNL.Hide();
            BestellingPNL.Hide();
            TafelPNL.Hide();
        }

        private void BTNDiner_Click(object sender, EventArgs e)
        {
            DinerPNL.Show();
            TafelNummerPNL.Hide();
            BestellingPNL.Hide();
        }

        private void BTNDrank_Click(object sender, EventArgs e)
        {
            DrankPNL.Show();
            TafelNummerPNL.Hide();
            BestellingPNL.Hide();
        }

        private void BTNOverzicht_Click(object sender, EventArgs e)
        {
            OverzichtPNL.Show();
            TafelNummerPNL.Hide();
            BestellingPNL.Hide();
        }
        //Dit is om alle menu lijsten te vullen en de menu te openen
        private void timerDN_Tick(object sender, EventArgs e) // NAGERECHT DINER 
        {
            TimerClick(timerDN, DinerNaBTN, DNDropdown);
        }


        private void DinerNaBTN_Click(object sender, EventArgs e)
        {
            timerDN.Start();
            VulListView("Diner", "nagerecht", ListViewDinerN);
        }
        //
        private void timerDV_Tick(object sender, EventArgs e) // VOORGERECHT DINER
        {
            TimerClick(timerDV, DinerVoorBTN, DVDropdown);
        }
        private void DinerVoorBTN_Click(object sender, EventArgs e)
        {
            timerDV.Start();
            VulListView("Diner", "voorgerecht", ListViewDinerV);
        }
        //
        private void timerDT_Tick(object sender, EventArgs e) // TUSSENGERECHT DINER
        {
            TimerClick(timerDT, DinerTussenBTN, DTDropdown);
        }
        private void DinerTussenBTN_Click(object sender, EventArgs e)
        {
            timerDT.Start();
            VulListView("Diner", "tussengerecht", ListViewDinerT);
        }
        //
        private void timerDH_Tick(object sender, EventArgs e) // HOOFDGERECHT DINER
        {
            TimerClick(timerDH, DinerHoofdBTN, DHDropdown);
        }
        private void DinerHoofdBTN_Click(object sender, EventArgs e)
        {
            timerDH.Start();
            VulListView("Diner", "hoofdgerecht", ListViewDinerH);
        }
        //
        private void timerLV_Tick(object sender, EventArgs e) //LUNCH VOORGERECHT
        {
            TimerClick(timerLV, LunchVoorBTN, LVDropdown);
        }
        private void LunchVoorBTN_Click(object sender, EventArgs e)
        {
            timerLV.Start();
            VulListView("Lunch","Voorgerecht", ListViewLunchV); 
        }
        //
        private void timerLH_Tick(object sender, EventArgs e) //LUNCH HOOFDGERECHT
        {
            TimerClick(timerLH, LunchHoofBTN, LHDropdown);
        }
        private void LunchHoofBTN_Click(object sender, EventArgs e)
        {
            timerLH.Start();
            VulListView("Lunch", "Hoofdgerecht", ListViewLunchH);
           
        }
        //
        private void timerLN_Tick(object sender, EventArgs e) //LUNCH NAGERECHT
        {
            TimerClick(timerLN, LunchNaBTN, LNDropdown);
        }
        private void LunchNaBTN_Click(object sender, EventArgs e)
        {
            timerLN.Start();
            VulListView("Lunch", "Nagerecht", ListViewLunchN);
        }

        private void timerDFris_Tick(object sender, EventArgs e) // FRISDRANK DRANK
        {
            TimerClick(timerDFris, DrankFrisBTN, DFrisDropdown);
        }
        private void DrankFrisBTN_Click(object sender, EventArgs e)
        {
            timerDFris.Start();
            VulListView("Dranken", "Frisdrank", ListViewDrankFris);
        }
        //
        private void timerDTap_Tick(object sender, EventArgs e) // TAP DRANK 
        {
            TimerClick(timerDTap, DrankTapBTN, DTapDropdown);
        }
        private void DrankTapBTN_Click(object sender, EventArgs e)
        {
            timerDTap.Start();
            VulListView("Dranken", "Tap", ListViewDrankTap);
        }
        //
        private void timerDGed_Tick(object sender, EventArgs e) //GEDISTILLEERD DRANK
        {
            TimerClick(timerDGed, DrankGedBTN, DGedDropdown);
        }
        private void DrankGedBTN_Click(object sender, EventArgs e)
        {
            timerDGed.Start();
            VulListView("Dranken", "Gedistilleerd", ListViewDrankGed);
        }
        //
        private void timerDWijn_Tick(object sender, EventArgs e) //WIJN DRANK
        {
            TimerClick(timerDWijn, DrankWijnBTN, DWijnDropdown);
        }
        private void DrankWijnBTN_Click(object sender, EventArgs e)
        {
            timerDWijn.Start();
            VulListView("Dranken", "Wijn", ListViewDrankWijn);
        }
        //
        private void timerDThee_Tick(object sender, EventArgs e) // KOFFIE/THEE DRANK
        {
            TimerClick(timerDThee, DrankTheeBTN, DTheeDropdown);
        }
        private void DrankTheeBTN_Click(object sender, EventArgs e)
        {
            timerDThee.Start();
            VulListView("Dranken", "Thee", ListViewDrankThee);
        }
        // Alle plus en min knoppen voor de 3 panels
        private void BTNPlusDrank_Click(object sender, EventArgs e)
        {
            PlusClick(ALBLDrank);
        }
        private void BTNMinDrank_Click(object sender, EventArgs e)
        {
            MinClick(ALBLDrank);
        }
        private void BTNPlusLunch_Click(object sender, EventArgs e)
        {
            PlusClick(ALBLLunch);
        }
        private void BTNMinLunch_Click(object sender, EventArgs e)
        {
            MinClick(ALBLLunch);
        }
        private void BTNMinDiner_Click(object sender, EventArgs e)
        {
            MinClick(ALBLDiner);
        }
        private void BTNPlusDiner_Click(object sender, EventArgs e)
        {
            PlusClick(ALBLDiner);
        }
        private void BTNPlusWijzigen_Click(object sender, EventArgs e)
        {
            PlusClick(ALBLWijzigen);
        }
        private void BTNMinWijzigen_Click(object sender, EventArgs e)
        {
            MinClick(ALBLWijzigen);
        }
        // Per ongeluk geklikt 
        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {

        }

        private void listView6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnT1_Click(object sender, EventArgs e)
        {

            TafelNummerPNL.Show();
            TafelPNL.Hide();
        }

        private void btnT2_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
        }

        private void btnT3_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
        }

        private void btnT4_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
        }

        private void btnT5_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
        }

        private void btnT6_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
        }

        private void btnT7_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
        }

        private void btnT8_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
        }

        private void btnT9_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
        }
        private void btnT10_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
        }
        private void panel11_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void TijdLBL_Click(object sender, EventArgs e)
        {

        }

        private void BTNBestellen_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Hide();
            BestellingPNL.Show();

        }
        private void BTNRekening_Click(object sender, EventArgs e)
        {
      
        }

        private void button33_Click(object sender, EventArgs e)
        {

        }

        private void OverzichtPNL_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
