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
            ShowPanel("DrankPNL");
        }
        private void HidePanels()
        {
            
            TafelNummerPNL.Hide();
            BestellingPNL.Hide();
            TafelPNL.Hide();
            WijzigenPNL.Hide();
            LunchPNL.Hide();
            DinerPNL.Hide();
            OverzichtPNL.Hide();
            

        }
        private void ShowPanel(string PanelName)
        {
            HidePanels();

            if(PanelName == "TafelPNL")
            {
                TafelPNL.Show();
            }

            else if(PanelName == "TafelNummerPNL")
            {
                TafelNummerPNL.Show();
            }

            else if (PanelName == "BestellingPNL")
            {

            }

            else if (PanelName == "LunchPNL")
            {
                // Laat panel x zien
                LunchPNL.Show();

                // Lijst maken en vullen voor Voorgerecht
                List<ChapooModel.MenuItem> VList = MenuItemService.GetItems("Lunch", "Voorgerecht");

                ListViewLunchV.Items.Clear();

                foreach (ChapooModel.MenuItem M in VList)
                {
                    ListViewItem List = new ListViewItem(M.ID.ToString());
                    List.Tag = M;
                    List.SubItems.Add(M.omschrijving);
                    List.SubItems.Add(M.aantalInVoorraad.ToString());

                    ListViewLunchV.Items.Add(List);
                }

                // Lijst maken en vullen voor Hoofdgerecht
                List<ChapooModel.MenuItem> HList = MenuItemService.GetItems("Lunch", "Hoofdgerecht");

                ListViewLunchH.Items.Clear();

                foreach (ChapooModel.MenuItem M in HList)
                {
                    ListViewItem List = new ListViewItem(M.ID.ToString());
                    List.Tag = M;
                    List.SubItems.Add(M.omschrijving);
                    List.SubItems.Add(M.aantalInVoorraad.ToString());

                    ListViewLunchH.Items.Add(List);
                }

                // Lijst maken en vullen voor Hoofdgerecht
                List<ChapooModel.MenuItem> NList = MenuItemService.GetItems("Lunch", "Nagerecht");

                ListViewLunchN.Items.Clear();

                foreach (ChapooModel.MenuItem M in NList)
                {
                    ListViewItem List = new ListViewItem(M.ID.ToString());
                    List.Tag = M;
                    List.SubItems.Add(M.omschrijving);
                    List.SubItems.Add(M.aantalInVoorraad.ToString());

                    ListViewLunchN.Items.Add(List);
                }

            }

            else if (PanelName == "DinerPNL")
            {
                DinerPNL.Show();
                // Lijst maken en vullen voor Voorgerecht
                List<ChapooModel.MenuItem> VList = MenuItemService.GetItems("Diner", "Voorgerecht");

                ListViewDinerV.Items.Clear();

                foreach (ChapooModel.MenuItem M in VList)
                {
                    ListViewItem List = new ListViewItem(M.ID.ToString());
                    List.Tag = M;
                    List.SubItems.Add(M.omschrijving);
                    List.SubItems.Add(M.aantalInVoorraad.ToString());

                    ListViewDinerV.Items.Add(List);
                }

                // Lijst maken en vullen voor Tussengerecht
                List<ChapooModel.MenuItem> TList = MenuItemService.GetItems("Diner", "Tussengerecht");

                ListViewDinerT.Items.Clear();

                foreach (ChapooModel.MenuItem M in TList)
                {
                    ListViewItem List = new ListViewItem(M.ID.ToString());
                    List.Tag = M;
                    List.SubItems.Add(M.omschrijving);
                    List.SubItems.Add(M.aantalInVoorraad.ToString());

                    ListViewDinerT.Items.Add(List);
                }

                // Lijst maken en vullen voor Hoofdgerecht
                List<ChapooModel.MenuItem> HList = MenuItemService.GetItems("Diner", "Hoofdgerecht");

                ListViewDinerH.Items.Clear();

                foreach (ChapooModel.MenuItem M in HList)
                {
                    ListViewItem List = new ListViewItem(M.ID.ToString());
                    List.Tag = M;
                    List.SubItems.Add(M.omschrijving);
                    List.SubItems.Add(M.aantalInVoorraad.ToString());

                    ListViewDinerH.Items.Add(List);
                }

                // Lijst maken en vullen voor Nagerecht
                List<ChapooModel.MenuItem> NList = MenuItemService.GetItems("Diner", "Nagerecht");

                ListViewDinerN.Items.Clear();

                foreach (ChapooModel.MenuItem M in NList)
                {
                    ListViewItem List = new ListViewItem(M.ID.ToString());
                    List.Tag = M;
                    List.SubItems.Add(M.omschrijving);
                    List.SubItems.Add(M.aantalInVoorraad.ToString());

                    ListViewDinerN.Items.Add(List);
                }
            }

            else if (PanelName == "DrankPNL")
            {
                DrankPNL.Show();

                // Lijst maken en vullen voor Nagerecht
                List<ChapooModel.MenuItem> FrisList = MenuItemService.GetItems("Dranken", "Frisdrank");

                ListViewDrankFris.Items.Clear();

                foreach (ChapooModel.MenuItem M in FrisList)
                {
                    ListViewItem List = new ListViewItem(M.ID.ToString());
                    List.Tag = M;
                    List.SubItems.Add(M.omschrijving);
                    List.SubItems.Add(M.aantalInVoorraad.ToString());

                    ListViewDrankFris.Items.Add(List);
                }

                // Lijst maken en vullen voor Nagerecht
                List<ChapooModel.MenuItem> TapList = MenuItemService.GetItems("Dranken", "Tap");

                ListViewDrankTap.Items.Clear();

                foreach (ChapooModel.MenuItem M in TapList)
                {
                    ListViewItem List = new ListViewItem(M.ID.ToString());
                    List.Tag = M;
                    List.SubItems.Add(M.omschrijving);
                    List.SubItems.Add(M.aantalInVoorraad.ToString());

                    ListViewDrankTap.Items.Add(List);
                }

                // Lijst maken en vullen voor Nagerecht
                List<ChapooModel.MenuItem> GedList = MenuItemService.GetItems("Dranken", "Gedistilleerd");

                ListViewDrankGed.Items.Clear();

                foreach (ChapooModel.MenuItem M in GedList)
                {
                    ListViewItem List = new ListViewItem(M.ID.ToString());
                    List.Tag = M;
                    List.SubItems.Add(M.omschrijving);
                    List.SubItems.Add(M.aantalInVoorraad.ToString());

                    ListViewDrankGed.Items.Add(List);
                }

                // Lijst maken en vullen voor Nagerecht
                List<ChapooModel.MenuItem> WijnList = MenuItemService.GetItems("Dranken", "Wijn");

                ListViewDrankWijn.Items.Clear();

                foreach (ChapooModel.MenuItem M in WijnList)
                {
                    ListViewItem List = new ListViewItem(M.ID.ToString());
                    List.Tag = M;
                    List.SubItems.Add(M.omschrijving);
                    List.SubItems.Add(M.aantalInVoorraad.ToString());

                    ListViewDrankWijn.Items.Add(List);
                }

                // Lijst maken en vullen voor Nagerecht
                List<ChapooModel.MenuItem> TheeList = MenuItemService.GetItems("Dranken", "Thee");

                ListViewDrankThee.Items.Clear();

                foreach (ChapooModel.MenuItem M in TheeList)
                {
                    ListViewItem List = new ListViewItem(M.ID.ToString());
                    List.Tag = M;
                    List.SubItems.Add(M.omschrijving);
                    List.SubItems.Add(M.aantalInVoorraad.ToString());

                    ListViewDrankThee.Items.Add(List);
                }
            }

            else if (PanelName == "OverzichtPNL")
            {

            }

            else if (PanelName == "WijzigenPNL")
            {

            }
        }

        private bool isNeer;
        private void timerLV_Tick(object sender, EventArgs e) //LUNCH VOORGERECHT
        {
            if (isNeer)
            {
                LunchVoorBTN.Image = Resources.Collapse_Arrow_20px;
                LVDropdown.Height += 10;
                if (LVDropdown.Size == LVDropdown.MaximumSize)
                {
                    timerLV.Stop();
                    isNeer = false;
                    LHDropdown.Height -= 10;
                    LHDropdown.Size = LVDropdown.MinimumSize;
                    LNDropdown.Height -= 10;
                    LNDropdown.Size = LVDropdown.MinimumSize;

                }
            }
            else
            {
                LunchVoorBTN.Image = Resources.Expand_Arrow_20px;
                LVDropdown.Height -= 10;
                if (LVDropdown.Size == LVDropdown.MinimumSize)
                {
                    timerLV.Stop();
                    isNeer = true;
                }
            }
        }
        private void LunchVoorBTN_Click(object sender, EventArgs e)
        {
            timerLV.Start(); timerLH.Stop();
        }
        //
        private void timerLH_Tick(object sender, EventArgs e) //LUNCH HOOFDGERECHT
        {
            if (isNeer)
            {
                button15.Image = Resources.Collapse_Arrow_20px;
                LHDropdown.Height += 10;
                if (LHDropdown.Size == LHDropdown.MaximumSize)
                {
                    timerLH.Stop();
                    isNeer = false;
                    LVDropdown.Height -= 10;
                    LVDropdown.Size = LHDropdown.MinimumSize;
                    LNDropdown.Height -= 10;
                    LNDropdown.Size = LHDropdown.MinimumSize;
                }
            }
            else
            {
                button15.Image = Resources.Expand_Arrow_20px;
                LHDropdown.Height -= 10;
                if (LHDropdown.Size == LHDropdown.MinimumSize)
                {
                    timerLH.Stop();
                    isNeer = true;
                }
            }
        }
        private void LunchHoofBTN_Click(object sender, EventArgs e)
        {
            timerLH.Start();
        }
        //
        private void timerLN_Tick(object sender, EventArgs e) //LUNCH NAGERECHT
        {
            if (isNeer)
            {
                button16.Image = Resources.Collapse_Arrow_20px;
                LNDropdown.Height += 10;
                if (LNDropdown.Size == LNDropdown.MaximumSize)
                {
                    timerLN.Stop();
                    isNeer = false;
                    LHDropdown.Height -= 10;
                    LHDropdown.Size = LNDropdown.MinimumSize;
                    LVDropdown.Height -= 10;
                    LVDropdown.Size = LNDropdown.MinimumSize;
                }
            }
            else
            {
                button16.Image = Resources.Expand_Arrow_20px;
                LNDropdown.Height -= 10;
                if (LNDropdown.Size == LNDropdown.MinimumSize)
                {
                    timerLN.Stop();
                    isNeer = true;
                }
            }
        }
        private void LunchNaBTN_Click(object sender, EventArgs e)
        {
            timerLN.Start();
        }
        // moet nog gedaan worden 

        private void timerDFris_Tick(object sender, EventArgs e) // FRISDRANK DRANK
        {
            if (isNeer)
            {
                DrankFrisBTN.Image = Resources.Collapse_Arrow_20px;
                DFrisDropdown.Height += 10;
                if (DFrisDropdown.Size == DFrisDropdown.MaximumSize)
                {
                    timerDFris.Stop();
                    isNeer = false;
                    DTapDropdown.Height -= 10;
                    DTapDropdown.Size = DFrisDropdown.MinimumSize;
                    DGedDropdown.Height -= 10;
                    DGedDropdown.Size = DFrisDropdown.MinimumSize;
                    DWijnDropdown.Height -= 10;
                    DWijnDropdown.Size = DFrisDropdown.MinimumSize;
                    DTheeDropdown.Height -= 10;
                    DTheeDropdown.Size = DFrisDropdown.MinimumSize;
                }
            }
            else
            {
                DrankFrisBTN.Image = Resources.Expand_Arrow_20px;
                DFrisDropdown.Height -= 10;
                if (DFrisDropdown.Size == DFrisDropdown.MinimumSize)
                {
                    timerDFris.Stop();
                    isNeer = true;
                }
            }
        }
        private void DrankFrisBTN_Click(object sender, EventArgs e)
        {
            timerDFris.Start();
        }
        //
        private void timerDTap_Tick(object sender, EventArgs e) // TAP DRANK 
        {
            if (isNeer)
            {
                DrankTapBTN.Image = Resources.Collapse_Arrow_20px;
                DTapDropdown.Height += 10;
                if (DTapDropdown.Size == DTapDropdown.MaximumSize)
                {
                    timerDTap.Stop();
                    isNeer = false;
                    DFrisDropdown.Height -= 10;
                    DFrisDropdown.Size = DTapDropdown.MinimumSize;
                    DGedDropdown.Height -= 10;
                    DGedDropdown.Size = DTapDropdown.MinimumSize;
                    DWijnDropdown.Height -= 10;
                    DWijnDropdown.Size = DTapDropdown.MinimumSize;
                    DTheeDropdown.Height -= 10;
                    DTheeDropdown.Size = DTapDropdown.MinimumSize;
                }
            }
            else
            {
                DrankTapBTN.Image = Resources.Expand_Arrow_20px;
                DTapDropdown.Height -= 10;
                if (DTapDropdown.Size == DTapDropdown.MinimumSize)
                {
                    timerDTap.Stop();
                    isNeer = true;
                }
            }
        }
        private void DrankTapBTN_Click(object sender, EventArgs e)
        {
            timerDTap.Start();
        }
        //
        private void timerDGed_Tick(object sender, EventArgs e) //GEDISTILLEERD DRANK
        {
            if (isNeer)
            {
                DrankGedBTN.Image = Resources.Collapse_Arrow_20px;
                DGedDropdown.Height += 10;
                if (DGedDropdown.Size == DGedDropdown.MaximumSize)
                {
                    timerDGed.Stop();
                    isNeer = false;
                    DFrisDropdown.Height -= 10;
                    DFrisDropdown.Size = DGedDropdown.MinimumSize;
                    DTapDropdown.Height -= 10;
                    DTapDropdown.Size = DGedDropdown.MinimumSize;
                    DWijnDropdown.Height -= 10;
                    DWijnDropdown.Size = DGedDropdown.MinimumSize;
                    DTheeDropdown.Height -= 10;
                    DTheeDropdown.Size = DGedDropdown.MinimumSize;
                }
            }
            else
            {
                DrankGedBTN.Image = Resources.Expand_Arrow_20px;
                DGedDropdown.Height -= 10;
                if (DGedDropdown.Size == DGedDropdown.MinimumSize)
                {
                    timerDGed.Stop();
                    isNeer = true;
                }
            }
        }

        private void DrankGedBTN_Click(object sender, EventArgs e)
        {
            timerDGed.Start();
        }
        //
        private void timerDWijn_Tick(object sender, EventArgs e) //WIJN DRANK
        {
            if (isNeer)
            {
                DrankWijnBTN.Image = Resources.Collapse_Arrow_20px;
                DWijnDropdown.Height += 10;
                if (DWijnDropdown.Size == DWijnDropdown.MaximumSize)
                {
                    timerDWijn.Stop();
                    isNeer = false;
                    DFrisDropdown.Height -= 10;
                    DFrisDropdown.Size = DWijnDropdown.MinimumSize;
                    DTapDropdown.Height -= 10;
                    DTapDropdown.Size = DWijnDropdown.MinimumSize;
                    DGedDropdown.Height -= 10;
                    DGedDropdown.Size = DWijnDropdown.MinimumSize;
                    DTheeDropdown.Height -= 10;
                    DTheeDropdown.Size = DWijnDropdown.MinimumSize;
                }
            }
            else
            {
                DrankWijnBTN.Image = Resources.Expand_Arrow_20px;
                DWijnDropdown.Height -= 10;
                if (DWijnDropdown.Size == DWijnDropdown.MinimumSize)
                {
                    timerDWijn.Stop();
                    isNeer = true;
                }
            }
        }
        private void DrankWijnBTN_Click(object sender, EventArgs e)
        {
            timerDWijn.Start();
        }
        //

        private void timerDThee_Tick(object sender, EventArgs e) // KOFFIE/THEE DRANK
        {
            if (isNeer)
            {
                DrankTheeBTN.Image = Resources.Collapse_Arrow_20px;
                DTheeDropdown.Height += 10;
                if (DTheeDropdown.Size == DTheeDropdown.MaximumSize)
                {
                    timerDThee.Stop();
                    isNeer = false;
                    DFrisDropdown.Height -= 10;
                    DFrisDropdown.Size = DTheeDropdown.MinimumSize;
                    DTapDropdown.Height -= 10;
                    DTapDropdown.Size = DTheeDropdown.MinimumSize;
                    DGedDropdown.Height -= 10;
                    DGedDropdown.Size = DTheeDropdown.MinimumSize;
                    DWijnDropdown.Height -= 10;
                    DWijnDropdown.Size = DTheeDropdown.MinimumSize;
                }
            }
            else
            {
                DrankTheeBTN.Image = Resources.Expand_Arrow_20px;
                DTheeDropdown.Height -= 10;
                if (DTheeDropdown.Size == DTheeDropdown.MinimumSize)
                {
                    timerDThee.Stop();
                    isNeer = true;
                }
            }
        }
        private void DrankTheeBTN_Click(object sender, EventArgs e)
        {
            timerDThee.Start();
        }
        //

        private void timerDV_Tick(object sender, EventArgs e) // VOORGERECHT DINER
        {
            if (isNeer)
            {
                DinerVoorBTN.Image = Resources.Collapse_Arrow_20px;
                DVDropdown.Height += 10;
                if (DVDropdown.Size == DVDropdown.MaximumSize)
                {
                    timerDV.Stop();
                    isNeer = false;
                    DTDropdown.Height -= 10;
                    DTDropdown.Size = DVDropdown.MinimumSize;
                    DHDropdown.Height -= 10;
                    DHDropdown.Size = DVDropdown.MinimumSize;
                    DNDropdown.Height -= 10;
                    DNDropdown.Size = DVDropdown.MinimumSize;
                }
            }
            else
            {
                DinerVoorBTN.Image = Resources.Expand_Arrow_20px;
                DVDropdown.Height -= 10;
                if (DVDropdown.Size == DVDropdown.MinimumSize)
                {
                    timerDV.Stop();
                    isNeer = true;
                }
            }
        }
        private void DinerVoorBTN_Click(object sender, EventArgs e)
        {
            timerDV.Start();
        }

        private void timerDT_Tick(object sender, EventArgs e) // TUSSENGERECHT DINER
        {
            if (isNeer)
            {
                DinerTussenBTN.Image = Resources.Collapse_Arrow_20px;
                DTDropdown.Height += 10;
                if (DTDropdown.Size == DTDropdown.MaximumSize)
                {
                    timerDT.Stop();
                    isNeer = false;
                    DVDropdown.Height -= 10;
                    DVDropdown.Size = DTDropdown.MinimumSize;
                    DHDropdown.Height -= 10;
                    DHDropdown.Size = DTDropdown.MinimumSize;
                    DNDropdown.Height -= 10;
                    DNDropdown.Size = DTDropdown.MinimumSize;
                }
            }
            else
            {
                DinerTussenBTN.Image = Resources.Expand_Arrow_20px;
                DTDropdown.Height -= 10;
                if (DTDropdown.Size == DTDropdown.MinimumSize)
                {
                    timerDT.Stop();
                    isNeer = true;
                }
            }
        }
        private void DinerTussenBTN_Click(object sender, EventArgs e)
        {
            timerDT.Start();
        }
        //
        private void timerDH_Tick(object sender, EventArgs e) // HOOFDGERECHT DINER
        {
            if (isNeer)
            {
                DinerHoofdBTN.Image = Resources.Collapse_Arrow_20px;
                DHDropdown.Height += 10;
                if (DHDropdown.Size == DHDropdown.MaximumSize)
                {
                    timerDH.Stop();
                    isNeer = false;
                    DVDropdown.Height -= 10;
                    DVDropdown.Size = DHDropdown.MinimumSize;
                    DTDropdown.Height -= 10;
                    DTDropdown.Size = DHDropdown.MinimumSize;
                    DNDropdown.Height -= 10;
                    DNDropdown.Size = DHDropdown.MinimumSize;
                }
            }
            else
            {
                DinerHoofdBTN.Image = Resources.Expand_Arrow_20px;
                DHDropdown.Height -= 10;
                if (DHDropdown.Size == DHDropdown.MinimumSize)
                {
                    timerDH.Stop();
                    isNeer = true;
                }
            }
        }
        private void DinerHoofdBTN_Click(object sender, EventArgs e)
        {
            timerDH.Start();
        }
        private void timerDN_Tick(object sender, EventArgs e) // NAGERECHT DINER 
        {
            if (isNeer)
            {
                DinerNaBTN.Image = Resources.Collapse_Arrow_20px;
                DNDropdown.Height += 10;
                if (DNDropdown.Size == DNDropdown.MaximumSize)
                {
                    timerDN.Stop();
                    isNeer = false;
                    DVDropdown.Height -= 10;
                    DVDropdown.Size = DNDropdown.MinimumSize;
                    DTDropdown.Height -= 10;
                    DTDropdown.Size = DNDropdown.MinimumSize;
                    DHDropdown.Height -= 10;
                    DHDropdown.Size = DNDropdown.MinimumSize;
                }
            }
            else
            {
                DinerNaBTN.Image = Resources.Expand_Arrow_20px;
                DNDropdown.Height -= 10;
                if (DNDropdown.Size == DNDropdown.MinimumSize)
                {
                    timerDN.Stop();
                    isNeer = true;
                }
            }

        }
        private void DinerNaBTN_Click(object sender, EventArgs e)
        {
            timerDN.Start();
        }
        //

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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
    }
}
