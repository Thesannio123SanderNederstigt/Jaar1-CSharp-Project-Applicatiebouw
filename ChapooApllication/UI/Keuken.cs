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
        string Type = Login.MedewerkerType;

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

        }

        private void SelectCurrentOrders()
        {
            if(Type == "chef-kok")
            {

                BestellingService bestelservice = new BestellingService();
                List<Bestelling> bestellinglist = bestelservice.GetOrders("MIN");
                List<int> numberlist = new List<int>();

                foreach (Bestelling b in bestellinglist)
                {
                    if (b.status == false && b.kaartsoort != "Dranken") //om alleen huidige bestellingen te krijgen
                    {
                        int tafelID = b.tafelID;
                        numberlist.Add(tafelID);
                    }

                }

                string listviewname = "lv_Tafel";

                int tafelID2 = 0;
                int bestellingID = 0;
                DateTime besteltijd;
                current = true;

                for (int i = 0; i < numberlist.Count; i++) //van 0 t/m 7 maximaal
                {
                    tafelID2 = numberlist[i];

                    Bestelling bestelling = bestellinglist[i];
                    bestellingID = bestelling.ID;
                    besteltijd = bestelling.besteltijd;


                    ListView lv = new System.Windows.Forms.ListView();
                    CreateListView(lv, i, tafelID2, bestellingID, besteltijd, current);

                    lv.Name = listviewname + i;



                    List<Bestelling> Bestellinglistview = bestelservice.GetBestellingListView(tafelID2);


                    CreateOptionalButtons(Bestellinglistview, i, current);

                    lv.Items.Clear();

                    foreach (Bestelling b in Bestellinglistview)
                    {
                        if (b.status == false && b.kaartsoort != "Dranken")
                        {
                            ListViewItem li = new ListViewItem(b.omschrijving.ToString());
                            li.SubItems.Add(b.aantal.ToString());

                            lv.Items.Add(li);
                        }

                    }

                    if(numberlist.Count >= 7)
                    {
                        return;
                    }
                }
            }
            else if (Type == "barmedewerker")
            {

            }

        }

        private void SelectClearedOrders()
        {
            if (Type == "chef-kok")
            {

                BestellingService bestelservice = new BestellingService();
                List<Bestelling> bestellinglist = bestelservice.GetOrders("MAX");
                List<int> numberlist = new List<int>();

                foreach (Bestelling b in bestellinglist)
                {
                    if (b.status == true && b.kaartsoort != "Dranken") //om alleen huidige bestellingen te krijgen
                    {
                        int tafelID = b.tafelID;
                        numberlist.Add(tafelID);
                    }

                }

                string listviewname = "lv_Tafel";

                int tafelID2 = 0;
                int bestellingID = 0;
                DateTime besteltijd;
                current = false;

                for (int i = 0; i < numberlist.Count; i++) //van 0 t/m 7 maximaal
                {
                    tafelID2 = numberlist[i];

                    Bestelling bestelling = bestellinglist[i];
                    bestellingID = bestelling.ID;
                    besteltijd = bestelling.besteltijd;

                    ListView lv = new System.Windows.Forms.ListView();
                    CreateListView(lv, i, tafelID2, bestellingID, besteltijd, current);

                    lv.Name = listviewname + i;



                    List<Bestelling> Bestellinglistview = bestelservice.GetBestellingListView(tafelID2);


                    CreateOptionalButtons(Bestellinglistview, i, current);

                    lv.Items.Clear();

                    foreach (Bestelling b in Bestellinglistview)
                    {
                        if (b.status == true && b.kaartsoort != "Dranken")
                        {
                            ListViewItem li = new ListViewItem(b.omschrijving.ToString());
                            li.SubItems.Add(b.aantal.ToString());

                            lv.Items.Add(li);
                        }

                    }

                    if (numberlist.Count >= 7)
                    {
                        return;
                    }

                }
            }
            else if (Type == "barmedewerker")
            {

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
                            lblTijd1.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_Bestelling1.Text = "Tafel " + tafelID2;
                            lbl_Bestelling1ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel1_SelectedIndexChanged);
                        }
                        break;
                    case 1:
                        {
                            gBox_Tafel2.Controls.Add(lv);
                            lblTijd2.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_Bestelling2.Text = "Tafel " + tafelID2;
                            lbl_Bestelling2ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel2_SelectedIndexChanged);
                        }
                        break;
                    case 2:
                        {
                            gBox_Tafel3.Controls.Add(lv);
                            lblTijd3.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_Bestelling3.Text = "Tafel " + tafelID2;
                            lbl_Bestelling3ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel3_SelectedIndexChanged);
                        }
                        break;
                    case 3:
                        {
                            gBox_Tafel4.Controls.Add(lv);
                            lblTijd4.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_Bestelling4.Text = "Tafel " + tafelID2;
                            lbl_Bestelling4ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel4_SelectedIndexChanged);
                        }
                        break;
                    case 4:
                        {
                            gBox_Tafel5.Controls.Add(lv);
                            lblTijd5.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_Bestelling5.Text = "Tafel " + tafelID2;
                            lbl_Bestelling5ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel5_SelectedIndexChanged);
                        }
                        break;
                    case 5:
                        {
                            gBox_Tafel6.Controls.Add(lv);
                            lblTijd6.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_Bestelling6.Text = "Tafel " + tafelID2;
                            lbl_Bestelling6ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel6_SelectedIndexChanged);
                        }
                        break;
                    case 6:
                        {
                            gBox_Tafel7.Controls.Add(lv);
                            lblTijd7.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_Bestelling7.Text = "Tafel " + tafelID2;
                            lbl_Bestelling7ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_Tafel7_SelectedIndexChanged);
                        }
                        break;
                    case 7:
                        {
                            gBox_Tafel8.Controls.Add(lv);
                            lblTijd8.Text = besteltijd.ToString("dd/MM/yy hh:mm");
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
                            lblAFTijd1.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_AFBestelling1.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF1ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel1_SelectedIndexChanged);
                        }
                        break;
                    case 1:
                        {
                            gBox_AFTafel2.Controls.Add(lv);
                            lblAFTijd2.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_AFBestelling2.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF2ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel2_SelectedIndexChanged);
                        }
                        break;
                    case 2:
                        {
                            gBox_AFTafel3.Controls.Add(lv);
                            lblAFTijd3.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_AFBestelling3.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF3ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel3_SelectedIndexChanged);
                        }
                        break;
                    case 3:
                        {
                            gBox_AFTafel4.Controls.Add(lv);
                            lblAFTijd4.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_AFBestelling4.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF4ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel4_SelectedIndexChanged);
                        }
                        break;
                    case 4:
                        {
                            gBox_AFTafel5.Controls.Add(lv);
                            lblAFTijd5.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_AFBestelling5.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF5ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel5_SelectedIndexChanged);
                        }
                        break;
                    case 5:
                        {
                            gBox_AFTafel6.Controls.Add(lv);
                            lblAFTijd6.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_AFBestelling6.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF6ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel6_SelectedIndexChanged);
                        }
                        break;
                    case 6:
                        {
                            gBox_AFTafel7.Controls.Add(lv);
                            lblAFTijd7.Text = besteltijd.ToString("dd/MM/yy hh:mm");
                            lbl_AFBestelling7.Text = "Tafel " + tafelID2;
                            lbl_BestellingAF7ID.Text = bestellingID.ToString();
                            lv.SelectedIndexChanged += new System.EventHandler(lv_AFTafel7_SelectedIndexChanged);
                        }
                        break;
                    case 7:
                        {
                            gBox_AFTafel8.Controls.Add(lv);
                            lblAFTijd8.Text = besteltijd.ToString("dd/MM/yy hh:mm");
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

                if(current == true)
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

        private void DeleteListViews()
        {
            gBox_Tafel1.Controls.RemoveByKey("lv_Tafel0");
            gBox_Tafel2.Controls.RemoveByKey("lv_Tafel1");
            gBox_Tafel3.Controls.RemoveByKey("lv_Tafel2");
            gBox_Tafel4.Controls.RemoveByKey("lv_Tafel3");
            gBox_Tafel5.Controls.RemoveByKey("lv_Tafel4");
            gBox_Tafel6.Controls.RemoveByKey("lv_Tafel5");
            gBox_Tafel7.Controls.RemoveByKey("lv_Tafel6");
            gBox_Tafel8.Controls.RemoveByKey("lv_Tafel7");
        }
        

        private void TextLabelReset()
        {
            //resetten labels binnenkomende bestellingen scherm
            lbl_Bestelling1.Text = "Tafel ";
            lbl_Bestelling2.Text = "Tafel ";
            lbl_Bestelling3.Text = "Tafel ";
            lbl_Bestelling4.Text = "Tafel ";
            lbl_Bestelling5.Text = "Tafel ";
            lbl_Bestelling6.Text = "Tafel ";
            lbl_Bestelling7.Text = "Tafel ";
            lbl_Bestelling8.Text = "Tafel ";

            lblTijd1.Text = "";
            lblTijd2.Text = "";
            lblTijd3.Text = "";
            lblTijd4.Text = "";
            lblTijd5.Text = "";
            lblTijd6.Text = "";
            lblTijd7.Text = "";
            lblTijd8.Text = "";

            lbl_BestellingID.Text = "";

            lbl_Bestelling1ID.Text = "";
            lbl_Bestelling2ID.Text = "";
            lbl_Bestelling3ID.Text = "";
            lbl_Bestelling4ID.Text = "";
            lbl_Bestelling5ID.Text = "";
            lbl_Bestelling6ID.Text = "";
            lbl_Bestelling7ID.Text = "";
            lbl_Bestelling8ID.Text = "";


            //resetten labels afgerond bestellingscherm
            lbl_AFBestelling1.Text = "Tafel ";
            lbl_AFBestelling2.Text = "Tafel ";
            lbl_AFBestelling3.Text = "Tafel ";
            lbl_AFBestelling4.Text = "Tafel ";
            lbl_AFBestelling5.Text = "Tafel ";
            lbl_AFBestelling6.Text = "Tafel ";
            lbl_AFBestelling7.Text = "Tafel ";
            lbl_AFBestelling8.Text = "Tafel ";

            lblAFTijd1.Text = "";
            lblAFTijd2.Text = "";
            lblAFTijd3.Text = "";
            lblAFTijd4.Text = "";
            lblAFTijd5.Text = "";
            lblAFTijd6.Text = "";
            lblAFTijd7.Text = "";
            lblAFTijd8.Text = "";

            lbl_BestellingAFID.Text = "";

            lbl_BestellingAF1ID.Text = "";
            lbl_BestellingAF2ID.Text = "";
            lbl_BestellingAF3ID.Text = "";
            lbl_BestellingAF4ID.Text = "";
            lbl_BestellingAF5ID.Text = "";
            lbl_BestellingAF6ID.Text = "";
            lbl_BestellingAF7ID.Text = "";
            lbl_BestellingAF8ID.Text = "";

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
            pnl_BinnenkomendeBestellingen.Show();
            SelectCurrentOrders();
        }

        private void btn_StartAfgeronde_Click(object sender, EventArgs e)
        {
            pnl_KeukenBarStart.Hide();
            pnl_AfgerondeBestellingen.Show();
            SelectClearedOrders();
        }

        //eventhandlers/methoden op/binnen het pnl_BinnenkomendeBestellingen
        private void pictureBx_KeukenBarStartscherm_Keuken_Click(object sender, EventArgs e)
        {
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
            FillTafelPanel1();
        }

        private void btn_Tafel2More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            FillTafelPanel2();
        }

        private void btn_Tafel3More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            FillTafelPanel3();
        }

        private void btn_Tafel4More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            FillTafelPanel4();
        }

        private void btn_Tafel5More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            FillTafelPanel5();
        }

        private void btn_Tafel6More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            FillTafelPanel6();
        }

        private void btn_Tafel7More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            FillTafelPanel7();
        }

        private void btn_Tafel8More_Click(object sender, EventArgs e)
        {
            pnl_TafelBinnenkomendeBestelling.Show();
            FillTafelPanel8();
        }

        private void ResetGroupBoxSystemColors()
        {
            gBox_Tafel1.BackColor = SystemColors.Control;
            gBox_Tafel2.BackColor = SystemColors.Control;
            gBox_Tafel3.BackColor = SystemColors.Control;
            gBox_Tafel4.BackColor = SystemColors.Control;
            gBox_Tafel5.BackColor = SystemColors.Control;
            gBox_Tafel6.BackColor = SystemColors.Control;
            gBox_Tafel7.BackColor = SystemColors.Control;
            gBox_Tafel8.BackColor = SystemColors.Control;
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
            }

            lbl_BestellingID.Text = "";

        }

        private void btn_AfgerondeBestelling_Click(object sender, EventArgs e)
        {
            pnl_BinnenkomendeBestellingen.Hide();
            pnl_AfgerondeBestellingen.Show();
            SelectClearedOrders();
        }

        private void btn_VerwijderBestelling_Click(object sender, EventArgs e)
        {
            //verwijderen van geselecteerde bestelling (welk label is geselecteerd)
            if(lbl_BestellingID.Text != "")
            {
                int bestellingID = int.Parse(lbl_BestellingID.Text);
                BestellingService bestelservice = new BestellingService();
                bestelservice.DeleteBestelling(bestellingID);
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
            SelectCurrentOrders();

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
                lbl_Bestelling.Text = "Bestelling " + listView_BestelItems.Items[index].SubItems[3].Text;

            }

        }

        private void btnGereedMenuItem_Click_1(object sender, EventArgs e)
        {
            //bestelling items gereed melden

            ListView.SelectedListViewItemCollection listviewitems = listView_BestelItems.SelectedItems;
            int index = listView_BestelItems.SelectedIndices[0];
            int BestellingMenuItemID = int.Parse(listView_BestelItems.Items[index].SubItems[3].Text);
            string opmerking = listView_BestelItems.Items[index].SubItems[2].Text;
            opmerking = txt_Opmerkingen.Text;

            BestellingService bestellingservice = new BestellingService();
            bestellingservice.UpdateBestellingMenuItem(BestellingMenuItemID, opmerking);
            EntireOrderCheck();
            DeleteListViews();
            SelectCurrentOrders();
            pnl_TafelBinnenkomendeBestelling.Hide();
        }

        private void EntireOrderCheck()
        {
            string label = lbl_Tafel.Text;
            char last = lbl_Tafel.Text.Last();
            int tafelID;
            int num = int.Parse(last.ToString());

            if (last.Equals(0))
            {
                string result = label.Substring(label.Length - Math.Min(2, label.Length));
                tafelID = int.Parse(result);
            }
            else
            {
                tafelID = num;
            }

            BestellingService bestelservice = new BestellingService();
            List<Bestelling> Bestellinglistview = bestelservice.GetBestellingOpmerking(tafelID);

            listView_BestelItems.Items.Clear();

            foreach (Bestelling b in Bestellinglistview)
            {
                if (b.status == false && b.kaartsoort != "Dranken")
                {
                    ListViewItem li = new ListViewItem(b.omschrijving.ToString());
                    li.SubItems.Add(b.aantal.ToString());
                    li.SubItems.Add(b.opmerking.ToString());
                    li.SubItems.Add(b.rekeningID.ToString());
                    listView_BestelItems.Items.Add(li);
                }

            }

            int aantal = listView_BestelItems.Items.Count;

            if (aantal < 1 || aantal == 0 && (aantal < 0) == false) //eigenlijk dus als dit nul (0) is en alle menu items in de bestelling al gereed zijn gemeldt
            {
                //zet hier dan de gehele bestelling op gereed (dezelfde functionaliteit als de 'gereed voor serveren' knop
                Bestelling b = Bestellinglistview[0];
                int BestellingID = b.ID; //hopen dat deze niet NULL is gemaakt (dus niet is meegegeven) in dit listview object
                bestelservice.UpdateBestelling(BestellingID);

            }
        }

        private void FillTafelPanel1()
        {

            string label = lbl_Bestelling1.Text;
            char last = lbl_Bestelling1.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd1.Text;

            FillTafelPanel(label, last, num);
        }

        private void FillTafelPanel2()
        {

            string label = lbl_Bestelling2.Text;
            char last = lbl_Bestelling2.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd2.Text;

            FillTafelPanel(label, last, num);
        }

        private void FillTafelPanel3()
        {

            string label = lbl_Bestelling3.Text;
            char last = lbl_Bestelling3.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd3.Text;

            FillTafelPanel(label, last, num);
        }

        private void FillTafelPanel4()
        {

            string label = lbl_Bestelling4.Text;
            char last = lbl_Bestelling4.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd4.Text;

            FillTafelPanel(label, last, num);
        }

        private void FillTafelPanel5()
        {

            string label = lbl_Bestelling5.Text;
            char last = lbl_Bestelling5.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd5.Text;

            FillTafelPanel(label, last, num);
        }

        private void FillTafelPanel6()
        {

            string label = lbl_Bestelling6.Text;
            char last = lbl_Bestelling6.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd6.Text;

            FillTafelPanel(label, last, num);
        }

        private void FillTafelPanel7()
        {

            string label = lbl_Bestelling7.Text;
            char last = lbl_Bestelling7.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd7.Text;

            FillTafelPanel(label, last, num);
        }

        private void FillTafelPanel8()
        {

            string label = lbl_Bestelling8.Text;
            char last = lbl_Bestelling8.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_DatumTijd.Text = lblTijd8.Text;

            FillTafelPanel(label, last, num);
        }

        private void FillTafelPanel(string label, char last, int num)
        {
            int tafelID;

            if (last.Equals(0))
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

            BestellingService bestelservice = new BestellingService();
            List<Bestelling> Bestellinglistview = bestelservice.GetBestellingOpmerking(tafelID);

            listView_BestelItems.Items.Clear();

            foreach (Bestelling b in Bestellinglistview)
            {
                if(b.status == false)
                {
                    ListViewItem li = new ListViewItem(b.omschrijving.ToString());
                    li.SubItems.Add(b.aantal.ToString());
                    li.SubItems.Add(b.opmerking.ToString());
                    li.SubItems.Add(b.rekeningID.ToString());
                    listView_BestelItems.Items.Add(li);
                }

            }
        }

        //eventhandlers/methoden die op het Afgerondenbestellingen panel staan
        private void pictureBxAF_KeukenBarStartscherm_Keuken_Click(object sender, EventArgs e)
        {
            pnl_AfgerondeBestellingen.Hide();
            pnl_KeukenBarStart.Show();
        }

        // Listviews Afgeronde bestellingen
        private void lv_AFTafel1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling1.ForeColor = Color.Orange;
            FillAFTafelPanel1();
        }

        private void lv_AFTafel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling2.ForeColor = Color.Orange;
            FillAFTafelPanel2();

        }

        private void lv_AFTafel3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling3.ForeColor = Color.Orange;
            FillAFTafelPanel3();
        }

        private void lv_AFTafel4_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling4.ForeColor = Color.Orange;
            FillAFTafelPanel4();
        }

        private void lv_AFTafel5_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling5.ForeColor = Color.Orange;
            FillAFTafelPanel5();
        }

        private void lv_AFTafel6_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling6.ForeColor = Color.Orange;
            FillAFTafelPanel6();
        }

        private void lv_AFTafel7_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling7.ForeColor = Color.Orange;
            FillAFTafelPanel7();
        }

        private void lv_AFTafel8_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnl_TafelAfgerondeBestelling.Show();
            lbl_AFBestelling8.ForeColor = Color.Orange;
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

        private void ResetGroupBoxAFSystemColors()
        {
            gBox_AFTafel1.BackColor = SystemColors.Control;
            gBox_AFTafel2.BackColor = SystemColors.Control;
            gBox_AFTafel3.BackColor = SystemColors.Control;
            gBox_AFTafel4.BackColor = SystemColors.Control;
            gBox_AFTafel5.BackColor = SystemColors.Control;
            gBox_AFTafel6.BackColor = SystemColors.Control;
            gBox_AFTafel7.BackColor = SystemColors.Control;
            gBox_AFTafel8.BackColor = SystemColors.Control;
        }

        // Groupboxes voor afgeronde bestellingen
        private void gBox_AFTafel1_Click(object sender, EventArgs e)
        {
            ResetGroupBoxAFSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF1ID.Text;
            gBox_AFTafel1.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel2_Click(object sender, EventArgs e)
        {
            ResetGroupBoxAFSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF2ID.Text;
            gBox_AFTafel2.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel3_Click(object sender, EventArgs e)
        {
            ResetGroupBoxAFSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF3ID.Text;
            gBox_AFTafel3.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel4_Click(object sender, EventArgs e)
        {
            ResetGroupBoxAFSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF4ID.Text;
            gBox_AFTafel4.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel5_Click(object sender, EventArgs e)
        {
            ResetGroupBoxAFSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF5ID.Text;
            gBox_AFTafel5.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel6_Click(object sender, EventArgs e)
        {
            ResetGroupBoxAFSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF6ID.Text;
            gBox_AFTafel6.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel7_Click(object sender, EventArgs e)
        {
            ResetGroupBoxAFSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF7ID.Text;
            gBox_AFTafel7.BackColor = Color.LightBlue;
        }

        private void gBox_AFTafel8_Click(object sender, EventArgs e)
        {
            ResetGroupBoxAFSystemColors();
            lbl_BestellingAFID.Text = lbl_BestellingAF8ID.Text;
            gBox_AFTafel8.BackColor = Color.LightBlue;
        }

        private void FillAFTafelPanel1()
        {

            string label = lbl_AFBestelling1.Text;
            char last = lbl_AFBestelling1.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd1.Text;

            FillAFTafelPanel(label, last, num);
        }

        private void FillAFTafelPanel2()
        {

            string label = lbl_AFBestelling2.Text;
            char last = lbl_AFBestelling2.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd2.Text;

            FillAFTafelPanel(label, last, num);
        }

        private void FillAFTafelPanel3()
        {

            string label = lbl_AFBestelling3.Text;
            char last = lbl_AFBestelling3.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd3.Text;

            FillAFTafelPanel(label, last, num);
        }

        private void FillAFTafelPanel4()
        {

            string label = lbl_AFBestelling4.Text;
            char last = lbl_AFBestelling4.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd4.Text;

            FillAFTafelPanel(label, last, num);
        }

        private void FillAFTafelPanel5()
        {

            string label = lbl_AFBestelling5.Text;
            char last = lbl_AFBestelling5.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd5.Text;

            FillAFTafelPanel(label, last, num);
        }

        private void FillAFTafelPanel6()
        {

            string label = lbl_AFBestelling6.Text;
            char last = lbl_AFBestelling6.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd6.Text;

            FillAFTafelPanel(label, last, num);
        }

        private void FillAFTafelPanel7()
        {

            string label = lbl_AFBestelling7.Text;
            char last = lbl_AFBestelling7.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd7.Text;

            FillAFTafelPanel(label, last, num);
        }

        private void FillAFTafelPanel8()
        {

            string label = lbl_AFBestelling8.Text;
            char last = lbl_AFBestelling8.Text.Last();
            int num = int.Parse(last.ToString());
            lbl_AFDatumTijd.Text = lblAFTijd8.Text;

            FillAFTafelPanel(label, last, num);
        }

        private void FillAFTafelPanel(string label, char last, int num)
        {
            int tafelID;

            if (last.Equals(0))
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

            BestellingService bestelservice = new BestellingService();
            List<Bestelling> Bestellinglistview = bestelservice.GetBestellingOpmerking(tafelID);

            listView_BestelItems.Items.Clear();

            foreach (Bestelling b in Bestellinglistview)
            {
                if (b.status == true)
                {
                    ListViewItem li = new ListViewItem(b.omschrijving.ToString());
                    li.SubItems.Add(b.aantal.ToString());
                    li.SubItems.Add(b.opmerking.ToString());
                    li.SubItems.Add(b.rekeningID.ToString());
                    listView_BestelItems.Items.Add(li);
                }

            }
        }


        private void btn_Binnenkomendestelling_Click(object sender, EventArgs e)
        {
            pnl_AfgerondeBestellingen.Hide();
            pnl_BinnenkomendeBestellingen.Show();
            SelectCurrentOrders();
        }

        private void btn_VerwijderAfgerondeBestelling_Click(object sender, EventArgs e)
        {
            //verwijderen van geselecteerde bestelling (welk label is geselecteerd)
            if (lbl_BestellingAFID.Text != "")
            {
                int bestellingID = int.Parse(lbl_BestellingAFID.Text);
                BestellingService bestelservice = new BestellingService();
                bestelservice.DeleteBestelling(bestellingID);
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
            SelectClearedOrders();


            pnl_TafelAfgerondeBestelling.Hide();
        }

        private void Keuken_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Environment.Exit(0);
            }
        }

        // TODO: Optioneel nog een panel toevoegen
        private void btn_StartVoorraad_Click(object sender, EventArgs e)
        {
            // Hide en Show de goede panels
            HidePanels();

        }



    }
}
