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
        private Tafel tafel; //zodat ik het niet bij elke btn# click moet zetten
        private List<Tafel> tafels; //voor de "TafelStatus" void en btn# click
        private List<Button> buttons; // voor de "TafelPNL" en "TafelStatus" voids
        private List<Bestelling_MenuItem> Bestelling_MenuItems;
        private List<ChapooModel.MenuItem> menuItems; //ChappoModel omdat ik anders de model laag niet in kan.
        public Bestelling Bestelling;

        private ListViewItem listviewItem;

        private int rekeningID; //voor de rekening om het ID mee te geven tussen methoden
        private int rekeningtafelID;
        private double Prijsinclbtw;
        private string betaalwijze;
        private int teller;

        public Rekening Rekening { get; set; }

        private Bestelling_MenuItemService bestelling_MenuItemService = new Bestelling_MenuItemService();

        private void OverzichtReturnBTN_Click(object sender, EventArgs e)
        {
            BestellingPNL.Show();
            OverzichtPNL.Hide();
            WijzigenPNL.Visible = false;
        }
        private void BTNBestellingVerwijderen_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Weet je zeker dat je de bestelling wilt verwijderen?\n" +
                            "Bestelling en inhoud worden niet opgeslagen", "Bestelling verwijderen ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = BestellingService.GetNieuuwsteID();
                bestelling_MenuItemService.DeleteBestellingItem(id);
                bestelling_MenuItemService.DeleteBestelling(id);
                OverzichtPNL.Hide();
                TafelNummerPNL.Show();
            }
        }
        private void TafelEXITPNL_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Weet je zeker dat je wilt afmelden?", "Afmelden", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            Date_Time(TafelNummerDateLBL, TafelNummerTimeLBL);
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

        private void BTNVoegToeL_Click(object sender, EventArgs e)
        {
            if(listviewItem != null)
            {
                BestellingToevoegen1(listviewItem, ALBLLunch);
            }
        }
        private void BTNVoegToeDrank_Click(object sender, EventArgs e)
        {
            if(listviewItem != null)
            {
                BestellingToevoegen1(listviewItem, ALBLDrank);
            }
        }
        private void BTNVoegToeDiner_Click(object sender, EventArgs e)
        {
            if(listviewItem != null)
            {
                BestellingToevoegen1(listviewItem, ALBLDiner);
            }
        }
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

        private void GoToOverzicht()
        {
            ListViewOverzicht.Items.Clear();
            int ID = BestellingService.GetNieuuwsteID();

            Bestelling_MenuItems = bestelling_MenuItemService.GetBestelling(ID);
            foreach (Bestelling_MenuItem BM in Bestelling_MenuItems)
            {
                ListViewItem list = new ListViewItem(BM.MenuItemID.ToString());
                list.Tag = BM;
                list.SubItems.Add(BM.Omschrijving);
                list.SubItems.Add(BM.Aantal.ToString());

                ListViewOverzicht.Items.Add(list);
            }
        }

        //Om de status van een tafel te krijgen
        private void TafelStatus() //om de tafel status terug te krijgen
        {
            tafels = TafelService.TafelBeschikbaar();

            for (int i = 0; i < tafels.Count; i++)
            {
                if (tafels[i].TafelStatus > 0)
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
            LunchPNL.Refresh();
            DrankPNL.Refresh();
            DinerPNL.Refresh();
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
        private void BTNBestellingPlaatsen_Click(object sender, EventArgs e)
        {

            int ID = BestellingService.GetNieuuwsteID();

           // bestelling_MenuItemService.UpdateVoorraad();

            if (ListViewOverzicht.Items.Count > 0)
            {
                if (MessageBox.Show("Terug naar tafels", "Bestelling geplaatst", MessageBoxButtons.OK, MessageBoxIcon.None) == DialogResult.OK)
                {
                    string test = TXTOverzicht.Text;
                    bestelling_MenuItemService.UpdateOpmerking(ID, test);
                    OverzichtPNL.Hide();
                    TafelPNL.Visible = true;
                    WijzigenPNL.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Bestellinlijst is leeg", "Kan geen bestelling plaatsen !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BestellingToevoegen1(ListViewItem item, Label label1)
        {

            //ListViewItem item1 = listView6_SelectedIndexChanged[0];
            ChapooModel.MenuItem menuItem = (ChapooModel.MenuItem)item.Tag;

            Bestelling_MenuItem Item = new Bestelling_MenuItem()
            {
                MenuItem = menuItem,
                BestellingID = BestellingService.GetNieuuwsteID(),
                Aantal = int.Parse(label1.Text),
            };

            menuItems = MenuItemService.GetMenuItems();

            if (int.Parse(label1.Text) > 0)//hoger dan 1
            {
                if (menuItem.aantalInVoorraad >= Item.Aantal)
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
            GoToOverzicht();
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
            VulListView("Lunch", "Voorgerecht", ListViewLunchV);
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
        private void BTNPlusWijzigen_Click_1(object sender, EventArgs e)
        {
            PlusClick(ALBLWijzigen);
        }

        private void BTNMinWijzigen_Click_1(object sender, EventArgs e)
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
            Date_Time(OverzichtDateLBL, OverzichtTimeLBL);
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



        private void BTNItemAanpassen_Click(object sender, EventArgs e)
        {
            WijzigenPNL.Visible = true;
            OverzichtPNL.SendToBack();
        }

        private void panel11_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void TijdLBL_Click(object sender, EventArgs e)
        {

        }

        private void BTNBestellingLunch_Click(object sender, EventArgs e)
        {
            LunchPNL.Hide();
            OverzichtPNL.Show();
            GoToOverzicht();
        }

        private void OverzichtPNL_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BTNBestellingDiner_Click(object sender, EventArgs e)
        {
            OverzichtPNL.Show();
            DinerPNL.Hide();
            GoToOverzicht();
        }

        private void BTNBestellingDrank_Click(object sender, EventArgs e)
        {
            DrankPNL.Hide();
            OverzichtPNL.Show();
            GoToOverzicht();

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
            if (MessageBox.Show("Weet je zeker dat je de terug wilt gaan?\n" +
                            "Bestelling en inhoud worden niet opgeslagen", "Terug naar tafels", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = BestellingService.GetNieuuwsteID();
                bestelling_MenuItemService.DeleteBestellingItem(id);
                bestelling_MenuItemService.DeleteBestelling(id);
                BestellingPNL.Hide();
                TafelNummerPNL.Show();
                WijzigenPNL.Visible = false;
            }
            Date_Time(OverzichtDateLBL, OverzichtTimeLBL);
        }

        private void TafelNummerTerugBT_Click(object sender, EventArgs e)
        {
            TafelNummerPNL.Hide();
            TafelPNL.Show();
            TafelStatus(); 
        }


        ////////
        ////////
        ///////
        /////// Graag hieronder werken
        ///////
        //////
        //////
        ///

        private void BTNRekening_Click(object sender, EventArgs e)
        {
            Date_Time(RekeningDateLBL, RekeningTimeLBL);

            int TafelID;
            string tafel = LBLTafelNummer.Text.ToString();
            char last = tafel.Last();
            int num = int.Parse(last.ToString());

            // opvragen van een tafelID, op een 0 is het 10 = laatste 2.
            if (last.Equals(0))
            {
                string result = tafel.Substring(tafel.Length - Math.Min(2, tafel.Length));
                TafelID = int.Parse(result);
            }
            else
            {
                TafelID = num;
            }

            List<Rekening> rekeningenlist= RekeningService.GetRekeningen(TafelID);

            int onbetaald = 0;

            //service laag/dao check op bestaande rekening // (query: check if status = truefalse met rekeningID)

            foreach(Rekening r in rekeningenlist)
            {
              onbetaald++;
            }

            if(onbetaald > 0)
            {
                rekeningtafelID = rekeningenlist[0].tafelID;
            }
            else
            {
                rekeningtafelID = TafelID;
            }

            int RekeningID;

            string opmerking;

            if(rekeningtafelID == TafelID && onbetaald > 0)
            {
               //maak geen nieuwe rekening aan
               RekeningID = rekeningenlist[0].ID;
                opmerking = rekeningenlist[0].opmerking;
            }
            else
            {
                //maak nieuwe rekening aan (variabelen hiervoor)

                RekeningService.AddNewRekening(0, "", TafelID, false, "");
                Rekening r = RekeningService.GetLopendeRekening(TafelID);
                RekeningID = r.ID;
                opmerking = r.opmerking;
            }
            //hier is sowieso (in elk geval) een RekeningID aangemaakt no matter what (RekeningID kan nooit 0 zijn na dit punt)

            rekeningID = RekeningID;

            //methode invullen listview op RekeningPNL voor het rekening overzicht
            List<Rekening> rekeninglist = RekeningService.GetMenuitems(TafelID);

            teller = 0;

            foreach (Rekening r in rekeninglist)
            {
                teller++;
            }

            ListviewFiller(rekeninglist);

            //query voor het invullen van de totaalprijs label
            double totaalprijs = RekeningService.GetTotaalBedrag(RekeningID);

            lbl_Exclbtwoutput.Text = totaalprijs.ToString();

            //query voor het invullen van de btw label

            int btw = RekeningService.GetUpperBTW(rekeningID);
            double btw2 = (btw + 100.00);
            double btw3 = (btw2 / 100.00);

            double prijsinclbtw = (totaalprijs * btw3);

            lbl_Inclbtwoutput.Text = $"€ {prijsinclbtw.ToString()}";

            Prijsinclbtw = prijsinclbtw;

            txt_Opmerkingen.Text = opmerking.ToString();

            RekeningPNL.Show();
            TafelNummerPNL.Hide();
        }

        private void ListviewFiller(List<Rekening> rekeninglist)
        {

            ListViewRekening.Items.Clear();

            foreach (Rekening r in rekeninglist)
            {
                ListViewItem lv = new ListViewItem(r.omschrijving);
                lv.SubItems.Add(r.aantal.ToString());
                lv.SubItems.Add(r.prijs.ToString());
                double btwprijs = ((r.prijs) * ((100.00 + r.btw) / 100));
                lv.SubItems.Add(btwprijs.ToString());
                ListViewRekening.Items.Add(lv);

            }

        }

        private void txt_Fooi_TextChanged(object sender, EventArgs e)
        {
            double fooi = 0;
            if(txt_Fooi.Text != "")
            {
                fooi = double.Parse(txt_Fooi.Text);
            }
            double totaal = Prijsinclbtw + fooi;
            lbl_Totaaloutput.Text = totaal.ToString();
        }


        private void BTNWijzigingOplsaan_Click(object sender, EventArgs e)
        {
            int aantal = int.Parse(ALBLWijzigen.Text);
            ListViewItem item1 = ListViewOverzicht.SelectedItems[0];
            int bestellingID = BestellingService.GetNieuuwsteID();
            bestelling_MenuItemService.SetNewAantal(aantal, bestellingID, int.Parse(item1.Text));
            ListViewOverzicht.Items.Clear();
            GoToOverzicht();
            ALBLWijzigen.Text = 0.ToString();
        }

        private void BTNItemVerwijderen_Click(object sender, EventArgs e)
        {
            ListViewItem item1 = ListViewOverzicht.SelectedItems[0];
            int bestellingID = BestellingService.GetNieuuwsteID();
            bestelling_MenuItemService.DeleteMenuItem(bestellingID, int.Parse(item1.Text));
            ListViewOverzicht.Items.Clear();
            GoToOverzicht();
        }


        //afreken scherm (1)
        private void ListViewRekening_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_Betalen_Click(object sender, EventArgs e)
        {
            if (teller == 0)
            {
                btn_Betalen.Enabled = false;
            }

            float fooi = 0.00f;
            if(txt_Fooi.Text != "")
            {
                fooi = float.Parse(txt_Fooi.Text.ToString());
            }
            string opmerking = txt_Opmerkingen.Text.ToString();

            //update query voor de rekening om de fooi en opmerking er in te zetten, nog geen betaalwijze of betaalstatus
            RekeningService.EditRekening(rekeningID, fooi, "", rekeningtafelID, false, opmerking);

            RekeningPNL.Hide();
            AfrekenenPNL.Show();
        }


        private void RekeningTerugBTN_Click(object sender, EventArgs e)
        {
            lbl_Exclbtwoutput.Text = "";
            lbl_Inclbtwoutput.Text = "";
            lbl_Totaaloutput.Text = "";
            txt_Fooi.Text = "";
            txt_Opmerkingen.Text = "";
            RekeningPNL.Hide();
            TafelNummerPNL.Show();

        }

        //afrekeningscherm (2)
        private void AfrekenenTerugBTN_Click(object sender, EventArgs e)
        {
            AfrekenenPNL.Hide();
            RekeningPNL.Show();
        }

        //knop voor contante betaling
        private void btn_Contant_Click(object sender, EventArgs e)
        {
            betaalwijze = "Contant";
            btn_Contant.BackColor = SystemColors.Highlight;

            btn_Pinnen.BackColor = SystemColors.Control;
            btn_CC.BackColor = SystemColors.Control;
        }

        //knop voor pin betaling
        private void btn_Pinnen_Click(object sender, EventArgs e)
        {
            betaalwijze = "Pinnen";
            btn_Pinnen.BackColor = SystemColors.Highlight;

            btn_Contant.BackColor = SystemColors.Control;
            btn_CC.BackColor = SystemColors.Control;
        }

        //knop voor credit card betaling
        private void btn_CC_Click(object sender, EventArgs e)
        {
            betaalwijze = "Credit Card";
            btn_CC.BackColor = SystemColors.Highlight;

            btn_Contant.BackColor = SystemColors.Control;
            btn_Pinnen.BackColor = SystemColors.Control;
        }


        private void btn_Afronden_Click(object sender, EventArgs e)
        {
            //query voor verwijderen bestaande afgeronden bestellingen van een rekening...
            RekeningService.DeleteBestellingen(rekeningtafelID);

            float fooi = 0.00f;
            if (txt_Fooi.Text != "")
            {
                fooi = float.Parse(txt_Fooi.Text.ToString());
            }

            string opmerking = txt_Opmerkingen.Text.ToString();

            //query/methode voor afronden rekening (betaalstatus = true)
            RekeningService.EditRekening(rekeningID, fooi, betaalwijze, rekeningtafelID, true, opmerking);

            AfrekenenPNL.Hide();
            AfgerondPNL.Show();

            lbl_Tafel.Text += rekeningtafelID;
            // 

        }

        //afrekenscherm afgerond (3)

        private void AfgerondTerugBTN_Click(object sender, EventArgs e)
        {
            RekeningReset();
        }

        private void btn_TerugTafels_Click(object sender, EventArgs e)
        {

            RekeningReset();
        }

        private void RekeningReset()
        {
            lbl_Exclbtwoutput.Text = "";
            lbl_Inclbtwoutput.Text = "";
            lbl_Totaaloutput.Text = "";
            txt_Fooi.Text = "";
            txt_Opmerkingen.Text = "";
            lbl_Tafel.Text = "Tafel #";

            btn_Contant.BackColor = SystemColors.Control;
            btn_Pinnen.BackColor = SystemColors.Control;
            btn_CC.BackColor = SystemColors.Control;

            TafelStatus();
            AfgerondPNL.Hide();
            TafelPNL.Show();
        }

        private void BTNaangepast_Click(object sender, EventArgs e)
        {
            WijzigenPNL.Visible = false;
        }
        // Methodes om te selecteren op basis van listviews
        private void ListViewDrankThee_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = ListViewDrankThee.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = ListViewDrankThee.SelectedIndices[0];
                listviewItem = ListViewDrankThee.Items[index];
            }
        }

        private void ListViewDrankWijn_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = ListViewDrankWijn.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = ListViewDrankWijn.SelectedIndices[0];
                listviewItem = ListViewDrankWijn.Items[index];
            }
        }

        private void ListViewDrankGed_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = ListViewDrankGed.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = ListViewDrankGed.SelectedIndices[0];
                listviewItem = ListViewDrankGed.Items[index];
            }
        }

        private void ListViewDrankTap_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = ListViewDrankTap.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = ListViewDrankTap.SelectedIndices[0];
                listviewItem = ListViewDrankTap.Items[index];
            }
        }

        private void ListViewDrankFris_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = ListViewDrankFris.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = ListViewDrankFris.SelectedIndices[0];
                listviewItem = ListViewDrankFris.Items[index];
            }
        }

        private void ListViewDinerN_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = ListViewDinerN.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = ListViewDinerN.SelectedIndices[0];
                listviewItem = ListViewDinerN.Items[index];
            }
        }

        private void ListViewDinerH_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = ListViewDinerH.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = ListViewDinerH.SelectedIndices[0];
                listviewItem = ListViewDinerH.Items[index];
            }
        }

        private void ListViewDinerT_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = ListViewDinerT.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = ListViewDinerT.SelectedIndices[0];
                listviewItem = ListViewDinerT.Items[index];
            }
        }

        private void ListViewDinerV_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = ListViewDinerV.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = ListViewDinerV.SelectedIndices[0];
                listviewItem = ListViewDinerV.Items[index];
            }
        }

        private void ListViewLunchN_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = ListViewLunchN.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = ListViewLunchN.SelectedIndices[0];
                listviewItem = ListViewLunchN.Items[index];
            }
        }

        private void ListViewLunchH_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = ListViewLunchH.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = ListViewLunchH.SelectedIndices[0];
                listviewItem = ListViewLunchH.Items[index];
            }
        }
        private void ListViewLunchV_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = ListViewLunchV.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = ListViewLunchV.SelectedIndices[0];
                listviewItem = ListViewLunchV.Items[index];
            }
        }
    }
}
