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
        private bool isNeer;
        private Tafel  tafel; //zodat ik het niet bij elke btn# click moet zetten
        private List<Tafel> tafels; //voor de "TafelStatus" void en btn# click
        private List<Button> buttons; // voor de "TafelPNL" en "TafelStatus" voids
        private List<Bestelling_MenuItem> Bestelling_MenuItems;
        private List<ChapooModel.MenuItem> menuItems; //ChappoModel omdat ik anders de model laag niet in kan.
        public Bestelling Bestelling;
        public Rekening Rekening { get; set; }

        private Bestelling_MenuItemService bestelling_MenuItemService = new Bestelling_MenuItemService();

        private void OverzichtReturnBTN_Click(object sender, EventArgs e)
        {
            BestellingPNL.Show();
            OverzichtPNL.Hide();
        }
        private void TafelEXITPNL_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ben je zeker dat je wilt afmelden?", "Afmelden", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Visible = false;
            }
        }

        public Bediening()
        {
            InitializeComponent();
        }


        public void Date_Time(Label Date, Label Time)
        {
            Date.Text = DateTime.Now.ToShortDateString();
            Time.Text = DateTime.Now.ToShortTimeString();
        }

        private void Bediening_Load(object sender, EventArgs e)
        {
            ShowPanel("TafelPNL");
            Date_Time(TafelDateLBL, TafelTimeLBL);
            TafelPNL.Show();
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
        private void HidePanels()
        {
            DrankPNL.Hide();
            TafelNummerPNL.Hide();
            BestellingPNL.Hide();
            WijzigenPNL.Hide();
            LunchPNL.Hide();
            DinerPNL.Hide();
            OverzichtPNL.Hide();
            RekeningPNL.Hide();
            AfgerondPNL.Hide();
            AfrekenenPNL.Hide();
        }
        private void ShowPanel(string PanelName)
        {
            HidePanels();
            if (PanelName == "TafelPNL")
            {
                TafelPNL.Show();
            }
        }
        ListView list, list12, lis1t3, li1st4, li1st5;
        //Item(s) aan een bestelling toevoegen
        private void BTNVoegToeL_Click(object sender, EventArgs e)
        {
           
           BestellingToevoegen1(ListViewLunchV, ALBLLunch);
            BestellingToevoegen1(ListViewLunchN, ALBLLunch);



        }
        private void BTNVoegToeDrank_Click(object sender, EventArgs e)
        {
            //BestellingToevoegen1(ListViewDrankFris, ALBLDrank);
            //BestellingToevoegen2(ListViewDrankTap, ALBLDrank);
            //BestellingToevoegen3(ListViewDrankGed, ALBLDrank);
            //BestellingToevoegen4(ListViewDrankWijn, ALBLDrank);
            //BestellingToevoegen5(ListViewDrankThee, ALBLDrank);

        }
        private void BTNVoegToeDiner_Click(object sender, EventArgs e)
        {
            //BestellingToevoegen1(ListViewDinerV, ALBLDiner);
           // BestellingToevoegen2(ListViewDinerT, ALBLDiner);
           // BestellingToevoegen3(ListViewDinerH, ALBLDiner);
           // BestellingToevoegen4(ListViewDinerN, ALBLDiner);
            

        }
        //Om te kunnen selecteren wat je in een bestelling toevoegt
        private void BestellingToevoegen1(ListView listview1, Label label1)
        {
            ListView.SelectedListViewItemCollection selectedItems1 = listview1.SelectedItems;
            ListViewItem item1 = selectedItems1[0];
            ChapooModel.MenuItem menuItem = (ChapooModel.MenuItem)item1.Tag;


            Bestelling_MenuItem Item = new Bestelling_MenuItem()
            {
                MenuItem = menuItem,
                BestellingID = BestellingService.GetNieuuwsteID(),
                Aantal = int.Parse(label1.Text),
            };

            menuItems = MenuItemService.GetMenuItems();

            if (int.Parse(label1.Text) > 0)//hoger dan 1
            {
                if (menuItems[Item.Aantal].aantalInVoorraad >= Item.Aantal)
                {
                    bestelling_MenuItemService.TESTCreateBestellingMenuItem(menuItem.ID, BestellingService.GetNieuuwsteID(), int.Parse(label1.Text));
                    MessageBox.Show("Item(s) toegevoegd aan bestelling !");
                }
                else
                {
                    MessageBox.Show("Niet genoeg items op voorraad !");
                }
            }
            else
            {
                MessageBox.Show("Aantal is niet hoger dan 0 !!!");
            }
            label1.Text = 0.ToString();
        }
        
        //om de bestelling te controleren
        public void CheckBestelling()
        {
            int ID = Bestelling.ID;
            Bestelling_MenuItems = bestelling_MenuItemService.GetBestellingMenuItem(ID);
            menuItems = new List<ChapooModel.MenuItem>();

            foreach (Bestelling_MenuItem bestelling_MenuItem in Bestelling_MenuItems)
            {
                menuItems.Add(MenuItemService.GetMenuItemByID(bestelling_MenuItem.MenuItemID));
            }

            ListViewOverzicht.Items.Clear();
            for (int i = 0; i < Bestelling_MenuItems.Count; i++)
            {
                ListViewItem List = new ListViewItem(menuItems[i].ID.ToString());
                List.SubItems.Add($"{menuItems[i].omschrijving.ToString()}");
                List.SubItems.Add($"{Bestelling_MenuItems[i].Aantal.ToString()}");
                ListViewOverzicht.Items.Add(List);
            }
        }
        public void CheckBestellingLijst()
        {
            int ID = Bestelling.ID;
            ListViewOverzicht.Items.Clear();
            Bestelling_MenuItems = bestelling_MenuItemService.GetBestelling(ID);
            foreach (Bestelling_MenuItem B in Bestelling_MenuItems)
            {
                ListViewItem List = new ListViewItem(B.ID.ToString());
                List.Tag = B;
                List.SubItems.Add(B.Omschrijving);
                List.SubItems.Add(B.Aantal.ToString());

                ListViewOverzicht.Items.Add(List);
            }
        }

        //om de Listview in de Bestellingoverzicht te vullen 
        private void VulListViewOverzicht()
        {
            ListViewOverzicht.Items.Clear();
            for(int i = 0; i < Bestelling_MenuItems.Count; i++)
            {
                ListViewItem List = new ListViewItem(Bestelling_MenuItems[i].BestellingID.ToString());
                List.SubItems.Add($"{menuItems[i].omschrijving.ToString()}");
                List.SubItems.Add($"{Bestelling_MenuItems[i].Aantal.ToString()}");
                ListViewOverzicht.Items.Add(List);
            }

        }
        //Om de status van een tafel te krijgen
        private void TafelStatus() //om de tafel status terug te krijgen
        {
            tafels = TafelService.GetTafel();

            for (int i = 0; i < tafels.Count; i++)
            {
                if (tafels[i].status == false)
                {
                    buttons[i].BackColor = Color.Salmon;
                }
                else
                {
                    buttons[i].BackColor = Color.LightGreen;
                }
            }
        }
        //om een bestelling te plaatsen
        private void BTNBestellen_Click(object sender, EventArgs e)
        {           
            TafelNummerPNL.Hide();
            BestellingPNL.Show();
            Date_Time(BestellingDateLBL, BestellingTimeLBL);

            Bestelling = BestellingService.Create_Bestelling(tafel);
            tafel.status = false;
            if (tafel.status != false)
            {
                TafelService.Change_Status(tafel.ID);
            }
        }
        //Void voor alle plus knoppen 
        public void PlusClick(Label Plus)
        {
            int AantalLBL = int.Parse(Plus.Text);
            AantalLBL++;
            Plus.Text = AantalLBL.ToString();
        }
        //void voor alle min knoppen
        public void MinClick(Label Min)
        {
            int AantalLBL = int.Parse(Min.Text);
            if (AantalLBL > 0)
            {
                AantalLBL--;
                Min.Text = AantalLBL.ToString();
            }
        }
        //Dit is om alle lijsten te vullen
        public void VulListView(string MenuSoort, string Categorie, ListView ListViewNaam)
        {
            ListViewNaam.Items.Clear();

            menuItems = MenuItemService.GetItems($"{MenuSoort}", $"{Categorie}");

            foreach (ChapooModel.MenuItem M in menuItems)
            {
                ListViewItem List = new ListViewItem(M.ID.ToString());
                List.Tag = M;
                List.SubItems.Add(M.omschrijving);
                List.SubItems.Add(M.aantalInVoorraad.ToString());

                ListViewNaam.Items.Add(List);
            }
        }
        //Dit is om alle menu balken te openen
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
        //Om naar alle menu's te gaan
        private void BTNLunch_Click(object sender, EventArgs e)
        {
            LunchPNL.Show();
            TafelNummerPNL.Hide();
            BestellingPNL.Hide();
            TafelPNL.Hide();
            Date_Time(LunchDateLBL, LunchTimeLBL);
        }
        private void BTNDiner_Click(object sender, EventArgs e)
        {
            DinerPNL.Show();
            TafelNummerPNL.Hide();
            BestellingPNL.Hide();
            Date_Time(DinerDateLBL, DinerTimeLBL);
        }
        private void BTNDrank_Click(object sender, EventArgs e)
        {
            DrankPNL.Show();
            TafelNummerPNL.Hide();
            BestellingPNL.Hide();
            Date_Time(DrankDATELBL, DrankTIMELBL);
        }

        private void BTNOverzicht_Click(object sender, EventArgs e)
        {
            OverzichtPNL.Show();
            TafelNummerPNL.Hide();
            BestellingPNL.Hide();
            Date_Time(OverzichtDateLBL, OverzichtTimeLBL);
            CheckBestelling();


        }
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
            Button btnsender = (Button)sender;
            //if (btnsender == BTNVoegToeL)
            //{
            //    BestellingToevoegen2(ALBLLunch);
            //}
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
            Button btnsender = (Button)sender;
            //if (btnsender == BTNVoegToeL)
            //{
            //    BestellingToevoegen1(ALBLLunch);
            //}
        }
        //
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
        // Alle plus en min knoppen voor de Lunch, Diner, Drank, Wijzingen panels
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
        //tafel knoppen 
        private void btnT1_Click(object sender, EventArgs e)
        {
            tafel = tafels[0];
            TafelNummerPNL.Show();
            LBLTafelNummer.Text = "Tafel 1";
            TafelPNL.Hide();
        }

        private void btnT2_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
            tafel = tafels[1];
            LBLTafelNummer.Text = "Tafel 2";
        }

        private void btnT3_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
            tafel = tafels[2];
            LBLTafelNummer.Text = "Tafel 3";
        }

        private void btnT4_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
            tafel = tafels[3];
            LBLTafelNummer.Text = "Tafel 4";
        }
        private void btnT5_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
            tafel = tafels[4];
            LBLTafelNummer.Text = "Tafel 5";
        }
        private void btnT6_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
            tafel = tafels[5];
            LBLTafelNummer.Text = "Tafel 6";
        }
        private void btnT7_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
            tafel = tafels[6];
            LBLTafelNummer.Text = "Tafel 7";
        }
        private void btnT8_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
            tafel = tafels[7];
            LBLTafelNummer.Text = "Tafel 8";
        }
        private void btnT9_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
            tafel = tafels[8];
            LBLTafelNummer.Text = "Tafel 9";
        }
        private void btnT10_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Show();
            TafelPNL.Hide();
            tafel = tafels[9];
            LBLTafelNummer.Text = "Tafel 10";
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

        private void BTNBestellingVerwijderen_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Weet je zeker dat je de bestelling wilt verwijderen?\n" +
                                "Bestelling en inhoud worden niet opgeslagen", "Bestelling verwijderen ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void BTNItemAanpassen_Click(object sender, EventArgs e)
        {
            WijzigenPNL.Visible = true;
            OverzichtPNL.SendToBack();   
        }

        private void listView6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void panel11_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void TijdLBL_Click(object sender, EventArgs e)
        {

        }
        private void BTNRekening_Click(object sender, EventArgs e)
        {
            Date_Time(RekeningDateLBL, RekeningTimeLBL);
        }

        private void BTNBestellingLunch_Click(object sender, EventArgs e)
        {
            LunchPNL.Hide();
            OverzichtPNL.Show();
        }

        private void OverzichtPNL_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BTNBestellingDiner_Click(object sender, EventArgs e)
        {
            OverzichtPNL.Show();
            DinerPNL.Hide();
        }

        private void BTNBestellingDrank_Click(object sender, EventArgs e)
        {
            DrankPNL.Hide();
            OverzichtPNL.Show();
            
        }
        private void DinerTerugLBL_Click(object sender, EventArgs e)
        {
            DinerPNL.Hide();
            BestellingPNL.Show();
        }

        private void LunchTerugBTN_Click(object sender, EventArgs e)
        {
            LunchPNL.Hide();
            BestellingPNL.Show();
        }
        private void DrankTerugBTN_Click(object sender, EventArgs e)
        {
            DrankPNL.Hide();
            BestellingPNL.Show();
        }

        private void BestellingTerugBTN_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Weet je zeker dat je terug wilt gaan?\n" +
                                "Bestelling en inhoud worden niet opgeslagen", "Tafel overzicht", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void TafelNummerTerugBT_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Hide();
            TafelPNL.Show();
        }
    }
}
