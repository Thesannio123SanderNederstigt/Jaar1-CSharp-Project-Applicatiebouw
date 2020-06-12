using ChapooLogic;
using ChapooModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class Keuken : Form
    {
        //string Type = Login.MedewerkerType;
        User user = Login.user;
        string Overzicht = Kassa.Bestellingoverzicht;

        bool inkomend;
        bool current;

        public void HidePanels()
        {
            //pnl_KeukenBarStart().Hide();
            pnl_BinnenkomendeBestellingen.Hide();
            pnl_TafelBinnenkomendeBestelling.Hide();
            pnl_AfgerondeBestellingen.Hide();
            pnl_TafelAfgerondeBestelling.Hide();
        }

        public Keuken()
        {
            InitializeComponent();

            HidePanels();

            if (Overzicht == "kok" || Overzicht == "bar")
            {
                pnl_KeukenBarStart.Hide();
                pnl_BinnenkomendeBestellingen.Show();
                inkomend = true;
                SelectOrders();
            }
        }


        private void SelectOrders()
        {
            List<Bestelling> bestellinglist = new List<Bestelling>();

            BestellingService bestelservice = new BestellingService();

            if (user == User.ChefKok || Overzicht == "kok")
            {
                if (inkomend == true)
                {
                    bestellinglist = bestelservice.GetOrders("DESC");
                }
                else
                {
                    bestellinglist = bestelservice.GetOrders("ASC");
                }
            }
            else if (user == User.Barpersoneel || Overzicht == "bar")
            {
                if (inkomend == true)
                {
                    bestellinglist = bestelservice.GetDrinkOrders("DESC");
                }
                else
                {
                    bestellinglist = bestelservice.GetDrinkOrders("ASC");
                }
            }

            List<int> bestellingnumberlist = new List<int>();
            foreach (Bestelling b in bestellinglist)
            {
                if (inkomend == true)
                {
                    if (b.status == false) //om alleen huidige bestellingen te krijgen
                    {
                        int tafelID = b.tafelID;
                        int bestellingID = b.ID;
                        bestellingnumberlist.Add(bestellingID);
                    }
                }
                if (inkomend == false)
                {
                    if (b.status == true) //om alleen afgeronde bestellingen te krijgen
                    {
                        int tafelID = b.tafelID;
                        int bestellingID = b.ID;
                        bestellingnumberlist.Add(bestellingID);
                    }
                }
            }

            string listviewname = "lv_Tafel";

            int tafelID2 = 0;
            int bestellingID2 = 0;
            DateTime besteltijd;

            if (inkomend == true)
            {
                current = true;
            }
            else
            {
                current = false;
            }


            for (int i = 0; i < bestellingnumberlist.Count; i++) //van 0 t/m 7 maximaal
            {
                bestellingID2 = bestellingnumberlist[i];

                Bestelling bestelling = bestelservice.GetSingleBestelling(bestellingID2);
                tafelID2 = bestelling.tafelID;
                besteltijd = bestelling.besteltijd;

                ListView lv = new System.Windows.Forms.ListView();
                CreateListView(lv, i, tafelID2, bestellingID2, besteltijd, current);

                lv.Name = listviewname + i;

                List<Bestelling> Bestellinglist = bestelservice.GetBestellingListView(bestellingID2);
                CreateOptionalButtons(Bestellinglist, i, current);

                lv.Items.Clear();

                if (user == User.ChefKok || Overzicht == "kok")
                {
                    foreach (Bestelling b in Bestellinglist)
                    {
                        if (inkomend == true)
                        {
                            if (b.status == false && b.kaartsoort != "Dranken")
                            {
                                if (b.aantal >= 1)
                                {
                                    ListViewItem li = new ListViewItem(b.omschrijving.ToString());
                                    li.SubItems.Add(b.aantal.ToString());

                                    lv.Items.Add(li);
                                }
                            }
                        }
                        else
                        {
                            if (b.kaartsoort != "Dranken")
                            {
                                if (b.aantal >= 1)
                                {
                                    ListViewItem li = new ListViewItem(b.omschrijving.ToString());
                                    li.SubItems.Add(b.aantal.ToString());

                                    lv.Items.Add(li);
                                }
                            }
                        }
                    }
                }
                else if (user == User.Barpersoneel || Overzicht == "bar")
                {
                    foreach (Bestelling b in Bestellinglist)
                    {
                        if (inkomend == true)
                        {
                            if (b.status == false && b.kaartsoort == "Dranken")
                            {
                                if (b.aantal >= 1)
                                {
                                    ListViewItem li = new ListViewItem(b.omschrijving.ToString());
                                    li.SubItems.Add(b.aantal.ToString());

                                    lv.Items.Add(li);
                                }
                            }
                        }
                        else
                        {
                            if (b.kaartsoort == "Dranken")
                            {
                                if (b.aantal >= 1)
                                {
                                    ListViewItem li = new ListViewItem(b.omschrijving.ToString());
                                    li.SubItems.Add(b.aantal.ToString());

                                    lv.Items.Add(li);
                                }
                            }
                        }
                    }
                }
                if (bestellingnumberlist.Count >= 7)
                {
                    return;
                }
            }
        }

        private void CreateListView(ListView lv, int i, int tafelID2, int bestellingID, DateTime besteltijd, bool current)
        {
            lv.Bounds = new Rectangle(new Point(0, 40), new Size(307, 244));
            ColumnHeader col_MenuItem = new ColumnHeader();
            col_MenuItem.Text = "Menu Item";
            col_MenuItem.Width = 243;
            col_MenuItem.TextAlign = HorizontalAlignment.Left;
            ColumnHeader col_Aantal = new ColumnHeader();
            col_Aantal.Text = "Aantal";
            col_Aantal.Width = 60;
            col_Aantal.TextAlign = HorizontalAlignment.Left;
            lv.Columns.Add(col_MenuItem);
            lv.Columns.Add(col_Aantal);
            lv.HideSelection = false;
            lv.TabIndex = 42;
            lv.UseCompatibleStateImageBehavior = false;
            lv.View = System.Windows.Forms.View.Details;

            if (current == true)
            {
                switch (i)
                {
                    case 0:
                        {
                            gBox_Tafel1.Controls.Add(lv);
                            lblTijd1.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_Bestelling1.Text = "Tafel " + tafelID2;
                            lbl_Bestelling1ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel1_SelectedIndexChanged);
                        }
                        break;
                    case 1:
                        {
                            gBox_Tafel2.Controls.Add(lv);
                            lblTijd2.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_Bestelling2.Text = "Tafel " + tafelID2;
                            lbl_Bestelling2ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel2_SelectedIndexChanged);
                        }
                        break;
                    case 2:
                        {
                            gBox_Tafel3.Controls.Add(lv);
                            lblTijd3.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_Bestelling3.Text = "Tafel " + tafelID2;
                            lbl_Bestelling3ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel3_SelectedIndexChanged);
                        }
                        break;
                    case 3:
                        {
                            gBox_Tafel4.Controls.Add(lv);
                            lblTijd4.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_Bestelling4.Text = "Tafel " + tafelID2;
                            lbl_Bestelling4ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel4_SelectedIndexChanged);
                        }
                        break;
                    case 4:
                        {
                            gBox_Tafel5.Controls.Add(lv);
                            lblTijd5.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_Bestelling5.Text = "Tafel " + tafelID2;
                            lbl_Bestelling5ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel5_SelectedIndexChanged);
                        }
                        break;
                    case 5:
                        {
                            gBox_Tafel6.Controls.Add(lv);
                            lblTijd6.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_Bestelling6.Text = "Tafel " + tafelID2;
                            lbl_Bestelling6ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel6_SelectedIndexChanged);
                        }
                        break;
                    case 6:
                        {
                            gBox_Tafel7.Controls.Add(lv);
                            lblTijd7.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_Bestelling7.Text = "Tafel " + tafelID2;
                            lbl_Bestelling7ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel7_SelectedIndexChanged);
                        }
                        break;
                    case 7:
                        {
                            gBox_Tafel8.Controls.Add(lv);
                            lblTijd8.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_Bestelling8.Text = "Tafel " + tafelID2;
                            lbl_Bestelling8ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel8_SelectedIndexChanged);
                        }
                        break;
                }

            }
            else
            {
                switch (i)
                {
                    case 0:
                        {
                            gBox_AFTafel1.Controls.Add(lv);
                            lblAFTijd1.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_AFBestelling1.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF1ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel1_SelectedIndexChanged);
                        }
                        break;
                    case 1:
                        {
                            gBox_AFTafel2.Controls.Add(lv);
                            lblAFTijd2.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_AFBestelling2.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF2ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel2_SelectedIndexChanged);
                        }
                        break;
                    case 2:
                        {
                            gBox_AFTafel3.Controls.Add(lv);
                            lblAFTijd3.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_AFBestelling3.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF3ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel3_SelectedIndexChanged);
                        }
                        break;
                    case 3:
                        {
                            gBox_AFTafel4.Controls.Add(lv);
                            lblAFTijd4.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_AFBestelling4.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF4ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel4_SelectedIndexChanged);
                        }
                        break;
                    case 4:
                        {
                            gBox_AFTafel5.Controls.Add(lv);
                            lblAFTijd5.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_AFBestelling5.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF5ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel5_SelectedIndexChanged);
                        }
                        break;
                    case 5:
                        {
                            gBox_AFTafel6.Controls.Add(lv);
                            lblAFTijd6.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_AFBestelling6.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF6ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel6_SelectedIndexChanged);
                        }
                        break;
                    case 6:
                        {
                            gBox_AFTafel7.Controls.Add(lv);
                            lblAFTijd7.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_AFBestelling7.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF7ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel7_SelectedIndexChanged);
                        }
                        break;
                    case 7:
                        {
                            gBox_AFTafel8.Controls.Add(lv);
                            lblAFTijd8.Text = besteltijd.ToString("dd/MM/yy HH:mm");
                            lbl_AFBestelling8.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF8ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel8_SelectedIndexChanged);
                        }
                        break;
                }
            }
        }

        private void CreateOptionalButtons(List<Bestelling> Bestellinglist, int i, bool current)
        {
            int aantal = Bestellinglist.Count();

            if (aantal < 10)
            {
                return;
            }
            else
            {
                Button button = new System.Windows.Forms.Button();
                button.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                button.Location = new System.Drawing.Point(117, 286);
                button.Size = new System.Drawing.Size(67, 28);
                button.TabIndex = 41;
                button.Text = "˙˙˙";
                button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                button.UseVisualStyleBackColor = true;

                if (current == true)
                {
                    switch (i)
                    {
                        case 0:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 1 aanmaken
                                button.Name = "btn_Tafel1More";
                                gBox_Tafel1.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_Tafel1More_Click);
                            }
                            break;
                        case 1:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 2
                                button.Name = "btn_Tafel2More";
                                gBox_Tafel2.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_Tafel2More_Click);
                            }
                            break;
                        case 2:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 3
                                button.Name = "btn_Tafel3More";
                                gBox_Tafel3.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_Tafel3More_Click);
                            }
                            break;
                        case 3:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 4
                                button.Name = "btn_Tafel4More";
                                gBox_Tafel4.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_Tafel4More_Click);
                            }
                            break;
                        case 4:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 5
                                button.Name = "btn_Tafel5More";
                                gBox_Tafel5.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_Tafel5More_Click);
                            }
                            break;
                        case 5:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 6
                                button.Name = "btn_Tafel2More";
                                gBox_Tafel6.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_Tafel6More_Click);
                            }
                            break;
                        case 6:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 7
                                button.Name = "btn_Tafel7More";
                                gBox_Tafel7.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_Tafel7More_Click);
                            }
                            break;
                        case 7:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 8
                                button.Name = "btn_Tafel8More";
                                gBox_Tafel8.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_Tafel8More_Click);
                            }
                            break;

                    }
                }
                else
                {
                    switch (i)
                    {
                        case 0:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 1 aanmaken
                                button.Name = "btn_AFTafel1More";
                                gBox_AFTafel1.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_AFTafel1More_Click);
                            }
                            break;
                        case 1:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 2
                                button.Name = "btn_AFTafel2More";
                                gBox_AFTafel2.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_AFTafel2More_Click);
                            }
                            break;
                        case 2:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 3
                                button.Name = "btn_AFTafel3More";
                                gBox_AFTafel3.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_AFTafel3More_Click);
                            }
                            break;
                        case 3:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 4
                                button.Name = "btn_AFTafel4More";
                                gBox_AFTafel4.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_AFTafel4More_Click);
                            }
                            break;
                        case 4:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 5
                                button.Name = "btn_AFTafel5More";
                                gBox_AFTafel5.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_AFTafel5More_Click);
                            }
                            break;
                        case 5:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 6
                                button.Name = "btn_AFTafel2More";
                                gBox_AFTafel6.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_AFTafel6More_Click);
                            }
                            break;
                        case 6:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 7
                                button.Name = "btn_AFTafel7More";
                                gBox_AFTafel7.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_AFTafel7More_Click);
                            }
                            break;
                        case 7:
                            {
                                //specifieke eigenschappen voor de knop in groupbox 8
                                button.Name = "btn_AFTafel8More";
                                gBox_AFTafel8.Controls.Add(button);
                                button.Click += new System.EventHandler(btn_AFTafel8More_Click);
                            }
                            break;

                    }
                }

            }

        }

        // Methode om de listviews te verwijderen
        private void DeleteListViews()
        {
            foreach (Panel panel in this.Controls)
            {
                foreach (Control outerbox in panel.Controls)
                {
                    if (outerbox.GetType() == typeof(GroupBox))
                    {
                        foreach (Control innerbox in outerbox.Controls)
                        {
                            if (innerbox.GetType() == typeof(GroupBox))
                            {
                                foreach (Control listview in innerbox.Controls)
                                {
                                    if (listview.GetType() == typeof(ListView))
                                    {
                                        innerbox.Controls.Remove(listview);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Methode om de labels binnen de groepboxen te resetten / legen
        private void TextLabelReset()
        {
            foreach (Panel panel in this.Controls)
            {
                foreach (Control outerbox in panel.Controls)
                {
                    if (outerbox.GetType() == typeof(GroupBox))
                    {
                        foreach (Control innerbox in outerbox.Controls)
                        {

                            if (innerbox.GetType() == typeof(Label))
                            {
                                innerbox.Text = "";
                            }

                            foreach (Control label in innerbox.Controls)
                            {
                                if (label.GetType() == typeof(Label))
                                {
                                    if (label.Name.Contains("lbl_Bestelling"))
                                    {
                                        label.Text = "";
                                    }
                                    if (label.Name.Contains("Tijd"))
                                    {
                                        label.Text = "";
                                    }
                                    if (label.Name.Contains("lbl_Bestelling") & !label.Name.Contains("ID"))
                                    {
                                        label.Text = "Tafel ";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Methode om de kleuren van de labels weer terug te zetten
        private void TextLabelColorReset()
        {
            foreach (Panel panel in this.Controls)
            {
                foreach (Control outerbox in panel.Controls)
                {
                    if (outerbox.GetType() == typeof(GroupBox))
                    {
                        foreach (Control innerbox in outerbox.Controls)
                        {
                            if (innerbox.GetType() == typeof(GroupBox))
                            {
                                foreach (Control label in innerbox.Controls)
                                {
                                    if (label.GetType() == typeof(Label))
                                    {
                                        label.ForeColor = SystemColors.GrayText;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //eventhandlers voor op het startscherm panel (pnl_KeukenBarStart)
        private void pictureBx_Uitloggen_Keuken_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            Login login = new Login();
            login.Show();
        }

        private void btn_StartInkomende_Click(object sender, EventArgs e)
        {

            pnl_KeukenBarStart.Hide();
            LoadBinnenkomendeBestellingen();
            ResetGroupBoxSystemColors();

        }

        private void btn_StartAfgeronde_Click(object sender, EventArgs e)
        {
            pnl_KeukenBarStart.Hide();
            LoadAfgerondeBestellingen();
        }

        private void btn_StartVoorraad_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Kassa kassa = new Kassa();
            kassa.Show();
        }


        //eventhandlers/methoden binnen het pnl_BinnenkomendeBestellingen
        private void pictureBx_KeukenBarStartscherm_Keuken_Click(object sender, EventArgs e)
        {
            if (user == User.Eigenaar)
            {
                this.Visible = false;
                Kassa kassa = new Kassa();
                kassa.Show();
            }
            pnl_BinnenkomendeBestellingen.Hide();
            DeleteListViews();
            TextLabelReset();
            ResetGroupBoxSystemColors();
            pnl_KeukenBarStart.Show();

        }



        // Listviews binnenkomende bestellingen
        private void lv_Tafel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling1.ForeColor = Color.Orange;
            FillTafelPanel1();

        }


        private void lv_Tafel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling2.ForeColor = Color.Orange;
            FillTafelPanel2();

        }

        private void lv_Tafel3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling3.ForeColor = Color.Orange;
            FillTafelPanel3();

        }

        private void lv_Tafel4_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling4.ForeColor = Color.Orange;
            FillTafelPanel4();
        }

        private void lv_Tafel5_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling5.ForeColor = Color.Orange;
            FillTafelPanel5();
        }

        private void lv_Tafel6_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling6.ForeColor = Color.Orange;
            FillTafelPanel6();
        }

        private void lv_Tafel7_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling7.ForeColor = Color.Orange;
            FillTafelPanel7();
        }

        private void lv_Tafel8_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling8.ForeColor = Color.Orange;
            FillTafelPanel8();
        }

        private void btn_Tafel1More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling1.ForeColor = Color.Orange;
            FillTafelPanel1();
        }

        private void btn_Tafel2More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling2.ForeColor = Color.Orange;
            FillTafelPanel2();
        }

        private void btn_Tafel3More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling3.ForeColor = Color.Orange;
            FillTafelPanel3();
        }

        private void btn_Tafel4More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling4.ForeColor = Color.Orange;
            FillTafelPanel4();
        }

        private void btn_Tafel5More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling5.ForeColor = Color.Orange;
            FillTafelPanel5();
        }

        private void btn_Tafel6More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling6.ForeColor = Color.Orange;
            FillTafelPanel6();
        }

        private void btn_Tafel7More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling7.ForeColor = Color.Orange;
            FillTafelPanel7();
        }

        private void btn_Tafel8More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            lbl_Bestelling8.ForeColor = Color.Orange;
            FillTafelPanel8();
        }

        private void ResetGroupBoxSystemColors()
        {
            foreach (Panel panel in this.Controls)
            {
                foreach (Control outerbox in panel.Controls)
                {
                    if (outerbox.GetType() == typeof(GroupBox))
                    {
                        foreach (Control innerbox in outerbox.Controls)
                        {
                            if (innerbox.GetType() == typeof(GroupBox))
                            {
                                innerbox.BackColor = SystemColors.Control;
                            }
                        }
                    }
                }
            }
        }

        // Groupboxes voor binnengekomen bestelingen
        private void gBox_Tafel1_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingID.Text = lbl_Bestelling1ID.Text;
            gBox_Tafel1.BackColor = Color.LightBlue;
        }

        private void gBox_Tafel2_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingID.Text = lbl_Bestelling2ID.Text;
            gBox_Tafel2.BackColor = Color.LightBlue;
        }

        private void gBox_Tafel3_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingID.Text = lbl_Bestelling3ID.Text;
            gBox_Tafel3.BackColor = Color.LightBlue;
        }

        private void gBox_Tafel4_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingID.Text = lbl_Bestelling4ID.Text;
            gBox_Tafel4.BackColor = Color.LightBlue;
        }

        private void gBox_Tafel5_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingID.Text = lbl_Bestelling5ID.Text;
            gBox_Tafel5.BackColor = Color.LightBlue;
        }

        private void gBox_Tafel6_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingID.Text = lbl_Bestelling6ID.Text;
            gBox_Tafel6.BackColor = Color.LightBlue;
        }

        private void gBox_Tafel7_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingID.Text = lbl_Bestelling7ID.Text;
            gBox_Tafel7.BackColor = Color.LightBlue;
        }

        private void gBox_Tafel8_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingID.Text = lbl_Bestelling8ID.Text;
            gBox_Tafel8.BackColor = Color.LightBlue;
        }


        private void btn_GereedBestelling_Click(object sender, EventArgs e)
        {
            //verwerken van gereed melden geselecteerde bestelling (welk label is geselecteerd)
            if (lbl_BestellingID.Text != "")
            {
                int bestellingID = int.Parse(lbl_BestellingID.Text);
                BestellingService bestelservice = new BestellingService();
                bestelservice.UpdateBestelling(bestellingID);
                bestelservice.UpdateBestellingMenuItems(bestellingID);
                TextLabelColorReset();
                LoadBinnenkomendeBestellingen();
            }

            lbl_BestellingID.Text = "";

        }

        private void btn_AfgerondeBestelling_Click(object sender, EventArgs e)
        {
            LoadAfgerondeBestellingen();
        }

        private void LoadAfgerondeBestellingen()
        {
            pnl_BinnenkomendeBestellingen.Hide();
            DeleteListViews();
            TextLabelReset();
            ResetGroupBoxSystemColors();

            pnl_AfgerondeBestellingen.Show();
            inkomend = false;
            SelectOrders();
        }

        private void btn_VerwijderBestelling_Click(object sender, EventArgs e)
        {
            //verwijderen van geselecteerde bestelling (welk label is geselecteerd)
            if (lbl_BestellingID.Text != "")
            {
                int bestellingID = int.Parse(lbl_BestellingID.Text);
                BestellingService bestelservice = new BestellingService();
                bestelservice.DeleteBestelling(bestellingID);
                TextLabelColorReset();
                LoadBinnenkomendeBestellingen();
            }

            lbl_BestellingID.Text = "";
        }


        //eventhandlers/methoden voor het tafel panel van de inkomende bestellingen (pnl_TafelBinnenkomendeBestelling)
        private void pictureBx_TerugInkomendeBestellingen_Click(object sender, EventArgs e)
        {
            lbl_Tafel.Text = "Tafel ";
            txtMenuItem.Text = "";
            txtAantal.Text = "";
            txt_Opmerkingen.Text = "";
            lbl_Bestelling.Text = "Bestelling ";
            lbl_DatumTijd.Text = "";

            DeleteListViews();
            TextLabelReset();
            ResetGroupBoxSystemColors();
            inkomend = true;
            SelectOrders();

            pnl_TafelBinnenkomendeBestelling.Hide();
        }

        private void listView_BestelItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = listView_BestelItems.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = listView_BestelItems.SelectedIndices[0];
                txtMenuItem.Text = listView_BestelItems.Items[index].SubItems[0].Text;
                txtAantal.Text = listView_BestelItems.Items[index].SubItems[1].Text;
                txt_Opmerkingen.Text = listView_BestelItems.Items[index].SubItems[2].Text;
                //lbl_Bestelling.Text = "Bestelling " + listView_BestelItems.Items[index].SubItems[3].Text;
                lbl_Bestelling.Text = "Bestelling " + lbl_TafelBestellingID.Text;

            }

        }

        private void btnGereedMenuItem_Click_1(object sender, EventArgs e)
        {
            //bestelling items gereed melden

            ListView.SelectedListViewItemCollection listviewitems = listView_BestelItems.SelectedItems;
            int index = listView_BestelItems.SelectedIndices[0];
            int BestellingMenuItemID = int.Parse(listView_BestelItems.Items[index].SubItems[3].Text);
            string opmerking = listView_BestelItems.Items[index].SubItems[2].Text;
            //opmerking = txt_Opmerkingen.Text;

            BestellingService bestellingservice = new BestellingService();
            bestellingservice.UpdateBestellingMenuItem(BestellingMenuItemID);
            EntireOrderCheck();
            DeleteListViews();
            LoadBinnenkomendeBestellingen();
            pnl_TafelBinnenkomendeBestelling.Hide();
        }

        // Methode om de volledige order te krijgen binnen de andere listview
        private void EntireOrderCheck()
        {

            int bestellingID = int.Parse(lbl_TafelBestellingID.Text);


            BestellingService bestelservice = new BestellingService();
            List<Bestelling> Bestellinglistview = bestelservice.GetBestellingListView(bestellingID);

            listView_BestelItems.Items.Clear();

            foreach (Bestelling b in Bestellinglistview)
            {
                if (b.status == false)
                {
                    ListViewItem li = new ListViewItem(b.omschrijving.ToString());
                    li.SubItems.Add(b.aantal.ToString());
                    li.SubItems.Add(b.ID.ToString());
                    listView_BestelItems.Items.Add(li);
                }
            }


            int aantal = listView_BestelItems.Items.Count;

            if (aantal == 0 && (aantal > 0) == false) //eigenlijk dus als dit nul (0) is en alle menu items in de bestelling al gereed zijn gemeldt
            {
                //zet hier dan de gehele bestelling op gereed (dezelfde functionaliteit als de 'gereed voor serveren' knop
                Bestelling b = Bestellinglistview[0];
                int BestellingID = b.ID; //hopen dat deze niet NULL is gemaakt (dus niet is meegegeven) in dit listview object
                bestelservice.UpdateBestelling(BestellingID);
                TextLabelColorReset();
            }
        }


        private void FillTafelPanel1()
        {
            string label = lbl_Bestelling1.Text;
            char last = lbl_Bestelling1.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd1.Text;
            int bestellingID = int.Parse(lbl_Bestelling1ID.Text);

            FillTafelPanel(label, last, num, bestellingID);
        }

        private void FillTafelPanel2()
        {
            string label = lbl_Bestelling2.Text;
            char last = lbl_Bestelling2.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd2.Text;
            int bestellingID = int.Parse(lbl_Bestelling2ID.Text);

            FillTafelPanel(label, last, num, bestellingID);
        }

        private void FillTafelPanel3()
        {
            string label = lbl_Bestelling3.Text;
            char last = lbl_Bestelling3.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd3.Text;
            int bestellingID = int.Parse(lbl_Bestelling3ID.Text);

            FillTafelPanel(label, last, num, bestellingID);
        }

        private void FillTafelPanel4()
        {
            string label = lbl_Bestelling4.Text;
            char last = lbl_Bestelling4.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd4.Text;
            int bestellingID = int.Parse(lbl_Bestelling4ID.Text);

            FillTafelPanel(label, last, num, bestellingID);
        }

        private void FillTafelPanel5()
        {
            string label = lbl_Bestelling5.Text;
            char last = lbl_Bestelling5.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd5.Text;
            int bestellingID = int.Parse(lbl_Bestelling5ID.Text);

            FillTafelPanel(label, last, num, bestellingID);
        }

        private void FillTafelPanel6()
        {
            string label = lbl_Bestelling6.Text;
            char last = lbl_Bestelling6.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd6.Text;
            int bestellingID = int.Parse(lbl_Bestelling6ID.Text);

            FillTafelPanel(label, last, num, bestellingID);
        }

        private void FillTafelPanel7()
        {
            string label = lbl_Bestelling7.Text;
            char last = lbl_Bestelling7.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd7.Text;
            int bestellingID = int.Parse(lbl_Bestelling7ID.Text);

            FillTafelPanel(label, last, num, bestellingID);
        }

        private void FillTafelPanel8()
        {
            string label = lbl_Bestelling8.Text;
            char last = lbl_Bestelling8.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd8.Text;

            int bestellingID = int.Parse(lbl_Bestelling8ID.Text);

            FillTafelPanel(label, last, num, bestellingID);
        }


        // Methode om de tafel panels te vullen met de juiste gegevens
        private void FillTafelPanel(string label, char last, int num, int bestellingID)
        {
            int tafelID;

            if (num.Equals(0))
            {
                string result = label.Substring(label.Length - Math.Min(2, label.Length));
                tafelID = int.Parse(result);
            }
            else
            {
                tafelID = num;
            }
            lbl_Tafel.Text = "Tafel ";
            lbl_Tafel.Text += tafelID;
            lbl_TafelBestellingID.Text = bestellingID.ToString();

            BestellingService bestelservice = new BestellingService();
            List<Bestelling> Bestellinglistview = bestelservice.GetBestellingOpmerking(bestellingID);

            if (user == User.ChefKok || Overzicht == "kok")
            {
                foreach (Bestelling b in Bestellinglistview)
                {
                    if (b.status == false && b.kaartsoort != "Dranken")
                    {
                        addStuff(bestellingID);
                    }
                }

            }
            else if (user == User.Barpersoneel || Overzicht == "bar")
            {
                foreach (Bestelling b in Bestellinglistview)
                {
                    if (b.status == false && b.kaartsoort == "Dranken")
                    {
                        addStuff(bestellingID);
                    }
                }
            }

        }
        // Deze wordt gebruikt om alle bestellingen te vullen in de listviews
        private List<Bestelling> addStuff(int ID)
        {
            listView_BestelItems.Items.Clear();
            listView_AFBestelItems.Items.Clear();
            BestellingService bestelservice = new BestellingService();
            List<Bestelling> Bestellinglistview = bestelservice.GetBestellingOpmerking(ID);
            {
                foreach (Bestelling b in Bestellinglistview)
                {
                    ListViewItem li = new ListViewItem(b.omschrijving.ToString());
                    li.SubItems.Add(b.aantal.ToString());
                    li.SubItems.Add(b.opmerking.ToString());
                    li.SubItems.Add(b.bestellingmenuitemID.ToString());
                    if (b.aantal >= 1)
                    {
                        if (current == true)
                        {
                            listView_BestelItems.Items.Add(li);
                        }
                        else
                        {
                            listView_AFBestelItems.Items.Add(li);
                        }
                    }
                }
            }
            return Bestellinglistview;
        }



        //eventhandlers/methoden die op het Afgerondenbestellingen panel staan
        private void pictureBxAF_KeukenBarStartscherm_Keuken_Click(object sender, EventArgs e)
        {
            if (user == User.Eigenaar)
            {
                this.Visible = false;
                Kassa kassa = new Kassa();
                kassa.Show();
            }
            pnl_AfgerondeBestellingen.Hide();
            DeleteListViews();
            TextLabelReset();
            ResetGroupBoxSystemColors();
            pnl_KeukenBarStart.Show();
        }

        // Listviews Afgeronde bestellingen
        private void lv_AFTafel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel1();
        }

        private void lv_AFTafel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel2();

        }

        private void lv_AFTafel3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel3();
        }

        private void lv_AFTafel4_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel4();
        }

        private void lv_AFTafel5_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel5();
        }

        private void lv_AFTafel6_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel6();
        }

        private void lv_AFTafel7_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel7();
        }

        private void lv_AFTafel8_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel8();
        }

        private void btn_AFTafel1More_Click(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel1();
        }

        private void btn_AFTafel2More_Click(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel2();
        }

        private void btn_AFTafel3More_Click(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel3();
        }

        private void btn_AFTafel4More_Click(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel4();
        }

        private void btn_AFTafel5More_Click(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel5();
        }

        private void btn_AFTafel6More_Click(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel6();
        }

        private void btn_AFTafel7More_Click(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel7();
        }

        private void btn_AFTafel8More_Click(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            FillAFTafelPanel8();
        }

        private void gBox_AFTafel1_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF1ID.Text;
            gBox_AFTafel1.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel2_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF2ID.Text;
            gBox_AFTafel2.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel3_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF3ID.Text;
            gBox_AFTafel3.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel4_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF4ID.Text;
            gBox_AFTafel4.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel5_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF5ID.Text;
            gBox_AFTafel5.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel6_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF6ID.Text;
            gBox_AFTafel6.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel7_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF7ID.Text;
            gBox_AFTafel7.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel8_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF8ID.Text;
            gBox_AFTafel8.BackColor = Color.LightBlue;
        }

        private void FillAFTafelPanel1()
        {

            string label = lbl_AFBestelling1.Text;
            char last = lbl_AFBestelling1.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd1.Text;

            int bestellingID = int.Parse(lbl_BestellingAF1ID.Text);

            FillAFTafelPanel(label, last, num, bestellingID);
        }

        private void FillAFTafelPanel2()
        {

            string label = lbl_AFBestelling2.Text;
            char last = lbl_AFBestelling2.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd2.Text;

            int bestellingID = int.Parse(lbl_BestellingAF2ID.Text);

            FillAFTafelPanel(label, last, num, bestellingID);
        }

        private void FillAFTafelPanel3()
        {

            string label = lbl_AFBestelling3.Text;
            char last = lbl_AFBestelling3.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd3.Text;

            int bestellingID = int.Parse(lbl_BestellingAF3ID.Text);

            FillAFTafelPanel(label, last, num, bestellingID);
        }

        private void FillAFTafelPanel4()
        {
            string label = lbl_AFBestelling4.Text;
            char last = lbl_AFBestelling4.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd4.Text;
            int bestellingID = int.Parse(lbl_BestellingAF4ID.Text);

            FillAFTafelPanel(label, last, num, bestellingID);
        }

        private void FillAFTafelPanel5()
        {
            string label = lbl_AFBestelling5.Text;
            char last = lbl_AFBestelling5.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd5.Text;
            int bestellingID = int.Parse(lbl_BestellingAF5ID.Text);

            FillAFTafelPanel(label, last, num, bestellingID);
        }

        private void FillAFTafelPanel6()
        {
            string label = lbl_AFBestelling6.Text;
            char last = lbl_AFBestelling6.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd6.Text;
            int bestellingID = int.Parse(lbl_BestellingAF6ID.Text);

            FillAFTafelPanel(label, last, num, bestellingID);
        }

        private void FillAFTafelPanel7()
        {
            string label = lbl_AFBestelling7.Text;
            char last = lbl_AFBestelling7.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd7.Text;
            int bestellingID = int.Parse(lbl_BestellingAF7ID.Text);

            FillAFTafelPanel(label, last, num, bestellingID);
        }

        private void FillAFTafelPanel8()
        {
            string label = lbl_AFBestelling8.Text;
            char last = lbl_AFBestelling8.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd8.Text;
            int bestellingID = int.Parse(lbl_BestellingAF8ID.Text);

            FillAFTafelPanel(label, last, num, bestellingID);
        }

        private void FillAFTafelPanel(string label, char last, int num, int bestellingID)
        {
            int tafelID;
            if (num.Equals(0))
            {
                string result = label.Substring(label.Length - Math.Min(2, label.Length));
                tafelID = int.Parse(result);
            }
            else
            {
                tafelID = num;
            }

            lbl_AFTafel.Text = "Tafel ";
            lbl_AFTafel.Text += tafelID;
            lbl_BestellingAFID.Text = bestellingID.ToString();

            BestellingService bestelservice = new BestellingService();
            List<Bestelling> Bestellinglistview = bestelservice.GetBestellingOpmerking(bestellingID);
            listView_AFBestelItems.Items.Clear();

            if (user == User.ChefKok || Overzicht == "kok")
            {
                foreach (Bestelling b in Bestellinglistview)
                {
                    if (b.kaartsoort != "Dranken")
                    {
                        addStuff(bestellingID);
                    }
                }
            }
            else if (user == User.Barpersoneel || Overzicht == "bar")
            {
                foreach (Bestelling b in Bestellinglistview)
                {
                    if (b.kaartsoort == "Dranken")
                    {
                        addStuff(bestellingID);
                    }
                }
            }
        }

        private void listView_AFBestelItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection listviewitems = listView_AFBestelItems.SelectedItems;

            if (listviewitems.Count > 0)
            {
                int index = listView_AFBestelItems.SelectedIndices[0];
                txt_AFOpmerkingen.Text = listView_AFBestelItems.Items[index].SubItems[2].Text;
            }
        }

        private void btn_Binnenkomendestelling_Click(object sender, EventArgs e)
        {
            ResetGroupBoxSystemColors();
            LoadBinnenkomendeBestellingen();
        }

        private void LoadBinnenkomendeBestellingen()
        {
            pnl_AfgerondeBestellingen.Hide();
            DeleteListViews();
            TextLabelReset();
            ResetGroupBoxSystemColors();

            pnl_BinnenkomendeBestellingen.Show();
            inkomend = true;
            SelectOrders();
        }

        private void btn_VerwijderAfgerondeBestelling_Click(object sender, EventArgs e)
        {
            //verwijderen van geselecteerde bestelling (welk label is geselecteerd)
            if (lbl_BestellingAFID.Text != "")
            {
                int bestellingID = int.Parse(lbl_BestellingAFID.Text);
                BestellingService bestelservice = new BestellingService();
                bestelservice.DeleteBestelling(bestellingID);
                LoadAfgerondeBestellingen();
            }
            lbl_BestellingAFID.Text = "";
        }


        //eventhandler/methode die op het afgeronde tafel bestelling panel (pnl_TafelAfgerondeBestelling) staat
        private void pictureBx_TerugAfgerondeBestellingen_Click(object sender, EventArgs e)
        {
            lbl_AFTafel.Text = "Tafel ";
            txt_AFOpmerkingen.Text = "";
            lbl_AFBestelling.Text = "Bestelling details";
            lbl_AFDatumTijd.Text = "";

            DeleteListViews();
            TextLabelReset();
            ResetGroupBoxSystemColors();
            inkomend = false;
            SelectOrders();


            pnl_TafelAfgerondeBestelling.Hide();
        }

        private void Keuken_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Environment.Exit(0);
            }
        }


    }
}
