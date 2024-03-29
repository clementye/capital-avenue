﻿using Capital_Avenue.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Capital_Avenue.Views.Board
{
    public partial class Board : UserControl
    {
        private List<Case> Cases;
        private CardChance CardChance;
        private CardCommunity CardCommunity;
        Dictionary<Player, int> PlayerPositions;
        private Dictionary<int, Property> Property = new Dictionary<int, Property>();
        public List<ProOwned> CaseOwner;

        public Board()
        {
            InitializeComponent();
            Cases = new List<Case>();
            CaseOwner = new List<ProOwned>();
            CreateBoard();
            CardChance = new CardChance(this);
            CardCommunity = new CardCommunity(this);
        }

        public void Init(Game game)
        {
            game.GameBoard = this;
            foreach (Player player in game.PlayerList)
            {
                Cases[0].AddPawn(player);
            }
        }



        public void MovePawn(Player p, int diceValue, bool isGoingToJail = false)
        {
            int NewPosition = p.Position + diceValue;
            if (NewPosition < 0)
            {
                NewPosition += 40;
            }
            if (NewPosition > 39)
            {
                NewPosition -= 40;
                if (!isGoingToJail)
                {
                    string Message = $"Vous avez reçu \n 200 euros,\n {p.Name}!";
                    Card c = new Card(Properties.Resources.Emojy_Depart, Message, "Bonus");
                    c.ShowDialog();
                    p.Capital += 200;

                }

            }

            Cases[p.Position].RemovePawn(p);
            p.Position = NewPosition;
            Cases[p.Position].AddPawn(p);
            CheckStatusProperty(p, NewPosition);

        }


        public void CheckStatusProperty(Player player, int indexCase)
        {
            if (Property.ContainsKey(indexCase))
            {
                Property pro = Property[indexCase];
                if (pro.CheckPropietorship(Property[indexCase]) == true)
                {
                    pro.TaxProperty(player, pro);
                }
                else
                {
                    PropertyCard c = new PropertyCard(pro, player);
                    c.ShowDialog();
                }
            }
            else if (indexCase == 7 || indexCase == 22 || indexCase == 36)
            {
                PlayCardSoundEffect();
                CardChance.ExecuteChanceCardAction(player);
            }
            else if (indexCase == 2 || indexCase == 17 || indexCase == 33)
            {
                PlayCardSoundEffect();
                CardCommunity.ExecuteCommunityCardAction(player);
            }
            else if (indexCase == 30)
            {
                MovePlayerToJail(player);
            }
            else if (indexCase == 4)
            {
                PlayTaxSoundEffect();
                DialogResult result = MessageBox.Show("You landed on a tax space. Choose how to pay the tax:\n\n" +
                    "1. Pay a fixed amount of 200 (Yes)\n" +
                    "2. Pay 10% of your capital (No)", "Tax Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes) // Pay fixed amount
                {
                    int taxAmount = 200;
                    player.Capital -= taxAmount;
                    MessageBox.Show($"You paid {taxAmount} in taxes.", "Tax Payment");
                }
                else // Pay 10% of capital
                {
                    int taxAmount = (int)(player.Capital * 0.1);
                    player.Capital -= taxAmount;
                    MessageBox.Show($"You paid {taxAmount} ({(int)(0.1 * 100)}% of your capital) in taxes.", "Tax Payment");
                }
            }
            else if (indexCase == 38)
            {
                int taxAmount = 100;
                player.Capital -= taxAmount;
                PlayTaxSoundEffect();
                MessageBox.Show($"You landed on a luxury tax space. You paid {taxAmount} in taxes.", "Tax Payment");
            }
        }

        private void PlayCardSoundEffect()
        {
            try
            {
                SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.card);
                soundPlayer.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing Card sound effect: " + ex.Message);
            }
        }

        private void PlayTaxSoundEffect()
        {
            try
            {
                SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.tax);
                soundPlayer.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing Card sound effect: " + ex.Message);
            }
        }

        public void MovePlayerToJail(Player player)
        {
            int move = player.Position > 10 ? 50 - player.Position : 10 - player.Position;
            PlayPoliceSoundEffect();
            MovePawn(player, move, true);
            player.isInJail = true;
            MessageBox.Show($"Vous allez en prison, {player.Name} !");
        }

        private void PlayPoliceSoundEffect()
        {
            try
            {
                SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.police);
                soundPlayer.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing winner sound effect: " + ex.Message);
            }
        }

        public void MovePawnToPosition(Player player, int newPosition)
        {
            int move = newPosition > player.Position ? newPosition - player.Position : 40 - newPosition - player.Position;
            MovePawn(player, move);
        }

        public void CreateBoard()
        {
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Case StartCase = new SquareCase(); // Crée une instance de la case de départ
            StartCase.Location = new Point(782, 776); // Définit la position de la case de départ
            StartCase.BackgroundImage = Properties.Resources.Casedepart;
            Controls.Add(StartCase);


            Case PrisonCase = new SquareCase();
            PrisonCase.Location = new Point(0, 776); // Définit la position de la case de prison
            PrisonCase.BackgroundImage = Properties.Resources.case10;
            Controls.Add(PrisonCase);


            Case FreeParkingCase = new SquareCase(); // Crée une instance de la case de parking gratuit
            FreeParkingCase.Location = new Point(782, 0); // Définit la position de la case de parking gratuit
            FreeParkingCase.BackgroundImage = Properties.Resources.case30;
            Controls.Add(FreeParkingCase);

            Case GotojailCase = new SquareCase(); // Crée une instance de la case "Allez en prison"
            GotojailCase.Location = new Point(0, 0); // Définit la position de la case "Allez en prison" 
            GotojailCase.BackgroundImage = Properties.Resources.case20;
            Controls.Add(GotojailCase);

            // LeftRowCase
            Case VerticalLeftCase1 = new VerticalCase();
            
            VerticalLeftCase1.Location = new Point(0, 124);
            VerticalLeftCase1.BackgroundImage = Properties.Resources.case19;
            ProOwned Case19 = new ProOwned(19);
            Case19.Location = new Point(120, 150);
            Controls.Add(VerticalLeftCase1);
            Controls.Add(Case19);

            Case VerticalLeftCase2 = new VerticalCase();
            VerticalLeftCase2.Location = new Point(0, 197);
            VerticalLeftCase2.BackgroundImage = Properties.Resources.case18;
            ProOwned Case18 = new ProOwned(18);
            Case18.Location = new Point(120, 223);
            Controls.Add(VerticalLeftCase2);
            Controls.Add(Case18);

            Case VerticalLeftCase3 = new VerticalCase();
            VerticalLeftCase3.Location = new Point(0, 270);
            VerticalLeftCase3.BackgroundImage = Properties.Resources.case17;
            Controls.Add(VerticalLeftCase3);

            Case VerticalLeftCase4 = new VerticalCase();
            VerticalLeftCase4.Location = new Point(0, 343);
            VerticalLeftCase4.BackgroundImage = Properties.Resources.case16;
            ProOwned Case16 = new ProOwned(16);
            Case16.Location = new Point(120, 369);
            Controls.Add(VerticalLeftCase4);
            Controls.Add(Case16);

            Case VerticalLeftCase5 = new VerticalCase();
            VerticalLeftCase5.Location = new Point(0, 416);
            VerticalLeftCase5.BackgroundImage = Properties.Resources.case15;
            ProOwned Case15 = new ProOwned(15);
            Case15.Location = new Point(120, 442);
            Controls.Add(VerticalLeftCase5);
            Controls.Add(Case15);

            Case VerticalLeftCase6 = new VerticalCase();
            VerticalLeftCase6.Location = new Point(0, 489);
            VerticalLeftCase6.BackgroundImage = Properties.Resources.case14;
            ProOwned Case14 = new ProOwned(14);
            Case14.Location = new Point(120, 513);
            Controls.Add(VerticalLeftCase6);
            Controls.Add(Case14);

            Case VerticalLeftCase7 = new VerticalCase();
            VerticalLeftCase7.Location = new Point(0, 562);
            VerticalLeftCase7.BackgroundImage = Properties.Resources.case13;
            ProOwned Case13 = new ProOwned(13);
            Case13.Location = new Point(120, 586);
            Controls.Add(VerticalLeftCase7);
            Controls.Add(Case13);

            Case VerticalLeftCase8 = new VerticalCase();
            VerticalLeftCase8.Location = new Point(0, 635);
            VerticalLeftCase8.BackgroundImage = Properties.Resources.case12;
            Controls.Add(VerticalLeftCase8);

            Case VerticalLeftCase9 = new VerticalCase();
            VerticalLeftCase9.Location = new Point(0, 708);
            VerticalLeftCase9.BackgroundImage = Properties.Resources.case11;
            ProOwned Case11 = new ProOwned(11);
            Case11.Location = new Point(120, 732);
            Controls.Add(VerticalLeftCase9);
            Controls.Add(Case11);

            // UpCase
            Case HorizontalupCase1 = new HorizontalCase();
            HorizontalupCase1.Location = new Point(124, 0);
            HorizontalupCase1.BackgroundImage = Properties.Resources.case21;
            ProOwned Case21 = new ProOwned(21);
            Case21.Location = new Point(150, 120);
            Controls.Add(HorizontalupCase1);
            Controls.Add(Case21);

            Case HorizontalupCase2 = new HorizontalCase();
            HorizontalupCase2.Location = new Point(197, 0);
            HorizontalupCase2.BackgroundImage = Properties.Resources.Case22;
            Controls.Add(HorizontalupCase2);

            Case HorizontalupCase3 = new HorizontalCase();
            HorizontalupCase3.Location = new Point(270, 0);
            HorizontalupCase3.BackgroundImage = Properties.Resources.case23;
            ProOwned Case23 = new ProOwned(23);
            Case23.Location = new Point(296, 120);
            Controls.Add(HorizontalupCase3);
            Controls.Add(Case23);

            Case HorizontalupCase4 = new HorizontalCase();
            HorizontalupCase4.Location = new Point(343, 0);
            HorizontalupCase4.BackgroundImage = Properties.Resources.case24;
            ProOwned Case24 = new ProOwned(24);
            Case24.Location = new Point(369, 120);
            Controls.Add(HorizontalupCase4);
            Controls.Add(Case24);

            Case HorizontalupCase5 = new HorizontalCase();
            HorizontalupCase5.Location = new Point(416, 0);
            HorizontalupCase5.BackgroundImage = Properties.Resources.case25;
            ProOwned Case25 = new ProOwned(25);
            Case25.Location = new Point(442, 120);
            Controls.Add(HorizontalupCase5);
            Controls.Add(Case25);

            Case HorizontalupCase6 = new HorizontalCase();
            HorizontalupCase6.Location = new Point(489, 0);
            HorizontalupCase6.BackgroundImage = Properties.Resources.case26;
            ProOwned Case26 = new ProOwned(26);
            Case26.Location = new Point(513, 120);
            Controls.Add(HorizontalupCase6);
            Controls.Add(Case26);

            Case HorizontalupCase7 = new HorizontalCase();
            HorizontalupCase7.Location = new Point(562, 0);
            HorizontalupCase7.BackgroundImage = Properties.Resources.case27;
            ProOwned Case27 = new ProOwned(27);
            Case27.Location = new Point(586, 120);
            Controls.Add(HorizontalupCase7);
            Controls.Add(Case27);

            Case HorizontalupCase8 = new HorizontalCase();
            HorizontalupCase8.Location = new Point(635, 0);
            HorizontalupCase8.BackgroundImage = Properties.Resources.case28;
            ProOwned Case28 = new ProOwned(28);
            Case28.Location = new Point(659, 120);
            Controls.Add(HorizontalupCase8);
            Controls.Add(Case28);

            Case HorizontalupCase9 = new HorizontalCase();
            HorizontalupCase9.Location = new Point(708, 0);
            HorizontalupCase9.BackgroundImage = Properties.Resources.case29;
            ProOwned Case29 = new ProOwned(29);
            Case29.Location = new Point(732, 120);
            Controls.Add(HorizontalupCase9);
            Controls.Add(Case29);

            // RightRowCase
            Case VerticalRightCase1 = new VerticalCase();
            VerticalRightCase1.Location = new Point(782, 124);
            VerticalRightCase1.BackgroundImage = Properties.Resources.case31;
            ProOwned Case31 = new ProOwned(31);
            Case31.Location = new Point(752, 150);
            Controls.Add(VerticalRightCase1);
            Controls.Add(Case31);

            Case VerticalRightCase2 = new VerticalCase();
            VerticalRightCase2.Location = new Point(782, 197);
            VerticalRightCase2.BackgroundImage = Properties.Resources.case32;
            ProOwned Case32 = new ProOwned(32);
            Case32.Location = new Point(752, 223);
            Controls.Add(VerticalRightCase2);
            Controls.Add(Case32);

            Case VerticalRightCase3 = new VerticalCase();
            VerticalRightCase3.Location = new Point(782, 270);
            VerticalRightCase3.BackgroundImage = Properties.Resources.case33;
            Controls.Add(VerticalRightCase3);

            Case VerticalRightCase4 = new VerticalCase();
            VerticalRightCase4.Location = new Point(782, 343);
            VerticalRightCase4.BackgroundImage = Properties.Resources.case34;
            ProOwned Case34 = new ProOwned(34);
            Case34.Location = new Point(752, 369);
            Controls.Add(VerticalRightCase4);
            Controls.Add(Case34);

            Case VerticalRightCase5 = new VerticalCase();
            VerticalRightCase5.Location = new Point(782, 416);
            VerticalRightCase5.BackgroundImage = Properties.Resources.case35;
            ProOwned Case35 = new ProOwned(35);
            Case35.Location = new Point(752, 442);
            Controls.Add(VerticalRightCase5);
            Controls.Add(Case35);

            Case VerticalRightCase6 = new VerticalCase();
            VerticalRightCase6.Location = new Point(782, 489);
            VerticalRightCase6.BackgroundImage = Properties.Resources.case36;
            Controls.Add(VerticalRightCase6);

            Case VerticalRightCase7 = new VerticalCase();
            VerticalRightCase7.Location = new Point(782, 562);
            VerticalRightCase7.BackgroundImage = Properties.Resources.case37;
            ProOwned Case37 = new ProOwned(37);
            Case37.Location = new Point(752, 586);
            Controls.Add(VerticalRightCase7);
            Controls.Add(Case37);

            Case VerticalRightCase8 = new VerticalCase();
            VerticalRightCase8.Location = new Point(782, 635);
            VerticalRightCase8.BackgroundImage = Properties.Resources.case38;
            Controls.Add(VerticalRightCase8);

            Case VerticalRightCase9 = new VerticalCase();
            VerticalRightCase9.Location = new Point(782, 708);
            VerticalRightCase9.BackgroundImage = Properties.Resources.case39;
            ProOwned Case39 = new ProOwned(39);
            Case39.Location = new Point(752, 732);
            Controls.Add(VerticalRightCase9);
            Controls.Add(Case39);

            // DownCase
            Case HorizontaldownCase1 = new HorizontalCase();
            HorizontaldownCase1.Location = new Point(124, 776);
            HorizontaldownCase1.BackgroundImage = Properties.Resources.case9;
            ProOwned Case9 = new ProOwned(9);
            Case9.Location = new Point(150, 746);
            Controls.Add(HorizontaldownCase1);
            Controls.Add(Case9);

            Case HorizontaldownCase2 = new HorizontalCase();
            HorizontaldownCase2.Location = new Point(197, 776);
            HorizontaldownCase2.BackgroundImage = Properties.Resources.case8;
            ProOwned Case8 = new ProOwned(8);
            Case8.Location = new Point(223, 746);
            Controls.Add(HorizontaldownCase2);
            Controls.Add(Case8);

            Case HorizontaldownCase3 = new HorizontalCase();
            HorizontaldownCase3.Location = new Point(270, 776);
            HorizontaldownCase3.BackgroundImage = Properties.Resources.case7;
            Controls.Add(HorizontaldownCase3);

            Case HorizontaldownCase4 = new HorizontalCase();
            HorizontaldownCase4.Location = new Point(343, 776);
            HorizontaldownCase4.BackgroundImage = Properties.Resources.case6;
            ProOwned Case6 = new ProOwned(6);
            Case6.Location = new Point(369, 746);
            Controls.Add(HorizontaldownCase4);
            Controls.Add(Case6);

            Case HorizontaldownCase5 = new HorizontalCase();
            HorizontaldownCase5.Location = new Point(416, 776);
            HorizontaldownCase5.BackgroundImage = Properties.Resources.case5;
            ProOwned Case5 = new ProOwned(5);
            Case5.Location = new Point(442, 746);
            Controls.Add(HorizontaldownCase5);
            Controls.Add(Case5);

            Case HorizontaldownCase6 = new HorizontalCase();
            HorizontaldownCase6.Location = new Point(489, 776);
            HorizontaldownCase6.BackgroundImage = Properties.Resources.case4;
            Controls.Add(HorizontaldownCase6);

            Case HorizontaldownCase7 = new HorizontalCase();
            HorizontaldownCase7.Location = new Point(562, 776);
            HorizontaldownCase7.BackgroundImage = Properties.Resources.case3;
            ProOwned Case3 = new ProOwned(3);
            Case3.Location = new Point(586, 746);
            Controls.Add(HorizontaldownCase7);
            Controls.Add(Case3);

            Case HorizontaldownCase8 = new HorizontalCase();
            HorizontaldownCase8.Location = new Point(635, 776);
            HorizontaldownCase8.BackgroundImage = Properties.Resources.case2;
            Controls.Add(HorizontaldownCase8);

            Case HorizontaldownCase9 = new HorizontalCase();
            HorizontaldownCase9.Location = new Point(708, 776);
            HorizontaldownCase9.BackgroundImage = Properties.Resources.case1;
            ProOwned Case1 = new ProOwned(1);
            Case1.Location = new Point(732, 746);
            Controls.Add(HorizontaldownCase9);
            Controls.Add(Case1);


            //Ajouter les case dans la liste des cases
            Cases.Add(StartCase);
            Cases.Add(HorizontaldownCase9);
            Property[1] = new Property(1, "GHANA", ColorProperty.Marron, Color.Peru, 60, 2, 10, 30, 90, 160, 250);
            Cases.Add(HorizontaldownCase8);
            Cases.Add(HorizontaldownCase7);
            Property[3] = new Property(3, "UGANDA", ColorProperty.Marron, Color.Peru, 60, 4, 20, 60, 180, 320, 450);
            Cases.Add(HorizontaldownCase6);
            Cases.Add(HorizontaldownCase5);
            Property[5] = new Property(5, "READING RAILROAD", ColorProperty.Station, Color.White, 200, 25, 50, 100, 150, 200, 400);
            Cases.Add(HorizontaldownCase4);
            Property[6] = new Property(6, "KENYA", ColorProperty.BleauC, Color.LightCyan, 100, 6, 30, 90, 270, 400, 550);
            Cases.Add(HorizontaldownCase3);
            Cases.Add(HorizontaldownCase2);
            Property[8] = new Property(8, "MADAGASCAR", ColorProperty.BleauC, Color.LightCyan, 100, 6, 30, 90, 270, 400, 550);
            Cases.Add(HorizontaldownCase1);
            Property[9] = new Property(9, "MYANNAR", ColorProperty.BleauC, Color.LightCyan, 120, 8, 40, 100, 300, 450, 600);
            Cases.Add(PrisonCase); //10
            Cases.Add(VerticalLeftCase9);//11
            Property[11] = new Property(11, "NIGERIA", ColorProperty.Rose, Color.DeepPink, 140, 10, 50, 150, 450, 625, 750);
            Cases.Add(VerticalLeftCase8);//12
            Property[12] = new Property(12, "DISTRIBUTION COMPANY", ColorProperty.Aucun, Color.White, 150, 75, 20, 40, 60, 80, 110);
            Cases.Add(VerticalLeftCase7);//13
            Property[13] = new Property(13, "BANGLADESH", ColorProperty.Rose, Color.DeepPink, 140, 10, 50, 150, 450, 625, 750);
            Cases.Add(VerticalLeftCase6);
            Property[14] = new Property(14, "PHILIPPINES", ColorProperty.Rose, Color.DeepPink, 160, 12, 60, 180, 500, 700, 900);
            Cases.Add(VerticalLeftCase5);
            Property[15] = new Property(15, "PENNSYVALIA ARWAYS", ColorProperty.Station, Color.White, 200, 25, 50, 100, 150, 200, 400);
            Cases.Add(VerticalLeftCase4);//16
            Property[16] = new Property(16, "VIETNAM", ColorProperty.Orange, Color.DarkOrange, 180, 14, 70, 200, 550, 700, 900);
            Cases.Add(VerticalLeftCase3);
            Cases.Add(VerticalLeftCase2);//18
            Property[18] = new Property(18, "RUSSIA", ColorProperty.Orange, Color.DarkOrange, 180, 14, 70, 200, 550, 700, 900);
            Cases.Add(VerticalLeftCase1);//19
            Property[19] = new Property(19, "MALAYSIA", ColorProperty.Orange, Color.DarkOrange, 200, 16, 80, 220, 600, 800, 1000);
            Cases.Add(GotojailCase);//20
            Cases.Add(HorizontalupCase1);
            Property[21] = new Property(21, "INDONESIA", ColorProperty.RougeOrange, Color.OrangeRed, 220, 18, 90, 250, 700, 875, 1050);
            Cases.Add(HorizontalupCase2);//22
            Cases.Add(HorizontalupCase3);
            Property[23] = new Property(23, "FRANCE", ColorProperty.RougeOrange, Color.OrangeRed, 220, 18, 90, 250, 700, 875, 1050);
            Cases.Add(HorizontalupCase4);//24
            Property[24] = new Property(24, "COLOMBIA", ColorProperty.RougeOrange, Color.OrangeRed, 240, 20, 100, 300, 750, 925, 1100);
            Cases.Add(HorizontalupCase5);
            Property[25] = new Property(25, "B & O CARGO", ColorProperty.Station, Color.White, 200, 25, 50, 100, 150, 200, 400);
            Cases.Add(HorizontalupCase6);//26
            Property[26] = new Property(26, "GERMANY", ColorProperty.Jaune, Color.Yellow, 260, 22, 110, 330, 800, 975, 1150);
            Cases.Add(HorizontalupCase7);
            Property[27] = new Property(27, "THAILAND", ColorProperty.Jaune, Color.Yellow, 260, 22, 110, 330, 800, 975, 1150);
            Cases.Add(HorizontalupCase8);//28
            Property[28] = new Property(28, "STORAGE WORKS", ColorProperty.Aucun, Color.White, 150, 75, 20, 40, 60, 80, 160);
            Cases.Add(HorizontalupCase9);
            Property[29] = new Property(29, "MEXICO", ColorProperty.Aucun, Color.Yellow, 280, 24, 120, 360, 850, 1025, 1200);
            Cases.Add(FreeParkingCase);//30
            Cases.Add(VerticalRightCase1);
            Property[31] = new Property(31, "ARGENTINA", ColorProperty.Vert, Color.Green, 300, 26, 130, 390, 900, 1100, 1275);
            Cases.Add(VerticalRightCase2);//32
            Property[32] = new Property(32, "INDIA", ColorProperty.Vert, Color.Green, 300, 26, 130, 390, 900, 1100, 1275);
            Cases.Add(VerticalRightCase3);
            Cases.Add(VerticalRightCase4);
            Property[34] = new Property(34, "BRAZIL", ColorProperty.Vert, Color.Green, 320, 28, 150, 450, 1000, 1200, 1400);
            Cases.Add(VerticalRightCase5);//35
            Property[35] = new Property(35, "SHORT TRUCIS", ColorProperty.Station, Color.White, 200, 25, 50, 100, 150, 200, 400);
            Cases.Add(VerticalRightCase6);
            Cases.Add(VerticalRightCase7);//37
            Property[37] = new Property(37, "USA", ColorProperty.Bleu, Color.DeepSkyBlue, 350, 35, 175, 500, 1100, 1300, 1500);
            Cases.Add(VerticalRightCase8);
            Cases.Add(VerticalRightCase9);
            Property[39] = new Property(39, "CHINA", ColorProperty.Bleu, Color.DeepSkyBlue, 400, 50, 200, 600, 1400, 1700, 2000);

            // List of Ownership
            this.CaseOwner.Add(Case1);
            this.CaseOwner.Add(Case3);
            this.CaseOwner.Add(Case5);
            this.CaseOwner.Add(Case6);
            this.CaseOwner.Add(Case8);
            this.CaseOwner.Add(Case9);
            this.CaseOwner.Add(Case11);
            this.CaseOwner.Add(Case13);
            this.CaseOwner.Add(Case14);
            this.CaseOwner.Add(Case15);
            this.CaseOwner.Add(Case16);
            this.CaseOwner.Add(Case18);
            this.CaseOwner.Add(Case19);
            this.CaseOwner.Add(Case21);
            this.CaseOwner.Add(Case23);
            this.CaseOwner.Add(Case24);
            this.CaseOwner.Add(Case25);
            this.CaseOwner.Add(Case27);
            this.CaseOwner.Add(Case28);
            this.CaseOwner.Add(Case29);
            this.CaseOwner.Add(Case31);
            this.CaseOwner.Add(Case32);
            this.CaseOwner.Add(Case34);
            this.CaseOwner.Add(Case35);
            this.CaseOwner.Add(Case37);
            this.CaseOwner.Add(Case39);
        }

    }
}