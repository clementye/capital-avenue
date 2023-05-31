﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capital_Avenue.Views.Board
{
    public partial class Board : UserControl
    {
        public Board()
        {
            InitializeComponent();
            AddBoard();
        }

        public void AddBoard()
        {
            // Configurer les propriétés de dimensionnement du UCBoard
            // AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            // Dock = DockStyle.Fill;

            SquareCase StartCase = new SquareCase(); // Crée une instance de la case de départ
            StartCase.Location = new Point(782, 776); // Définit la position de la case de départ
            StartCase.BackgroundImage = Properties.Resources.Casedepart;
            Controls.Add(StartCase);

            // Crée et configure la case de prison
            SquareCase PrisonCase = new SquareCase();
            PrisonCase.Location = new Point(0, 776); // Définit la position de la case de prison
            PrisonCase.BackgroundImage = Properties.Resources.case10;
            Controls.Add(PrisonCase);

            SquareCase FreeParkingCase = new SquareCase(); // Crée une instance de la case de parking gratuit
            FreeParkingCase.Location = new Point(782, 0); // Définit la position de la case de parking gratuit
            FreeParkingCase.BackgroundImage = Properties.Resources.case30;
            Controls.Add(FreeParkingCase);

            SquareCase GotojailCase = new SquareCase(); // Crée une instance de la case "Allez en prison"
            GotojailCase.Location = new Point(0, 0); // Définit la position de la case "Allez en prison" 
            GotojailCase.BackgroundImage = Properties.Resources.case20;
            Controls.Add(GotojailCase);

            // LeftRowCase
            VerticalCase VerticalLeftCase1 = new VerticalCase();
            VerticalLeftCase1.Location = new Point(0, 124);
            VerticalLeftCase1.BackgroundImage = Properties.Resources.case19;
            Controls.Add(VerticalLeftCase1);

            VerticalCase VerticalLeftCase2 = new VerticalCase();
            VerticalLeftCase2.Location = new Point(0, 197);
            VerticalLeftCase2.BackgroundImage = Properties.Resources.case18;
            Controls.Add(VerticalLeftCase2);

            VerticalCase VerticalLeftCase3 = new VerticalCase();
            VerticalLeftCase3.Location = new Point(0, 270);
            VerticalLeftCase3.BackgroundImage = Properties.Resources.case17;
            Controls.Add(VerticalLeftCase3);

            VerticalCase VerticalLeftCase4 = new VerticalCase();
            VerticalLeftCase4.Location = new Point(0, 343);
            VerticalLeftCase4.BackgroundImage = Properties.Resources.case16;
            Controls.Add(VerticalLeftCase4);

            VerticalCase VerticalLeftCase5 = new VerticalCase();
            VerticalLeftCase5.Location = new Point(0, 416);
            VerticalLeftCase5.BackgroundImage = Properties.Resources.case15;
            Controls.Add(VerticalLeftCase5);

            VerticalCase VerticalLeftCase6 = new VerticalCase();
            VerticalLeftCase6.Location = new Point(0, 489);
            VerticalLeftCase6.BackgroundImage = Properties.Resources.case14;
            Controls.Add(VerticalLeftCase6);

            VerticalCase VerticalLeftCase7 = new VerticalCase();
            VerticalLeftCase7.Location = new Point(0, 562);
            VerticalLeftCase7.BackgroundImage = Properties.Resources.case13;
            Controls.Add(VerticalLeftCase7);

            VerticalCase VerticalLeftCase8 = new VerticalCase();
            VerticalLeftCase8.Location = new Point(0, 635);
            VerticalLeftCase8.BackgroundImage = Properties.Resources.case12;s
            Controls.Add(VerticalLeftCase8);

            VerticalCase VerticalLeftCase9 = new VerticalCase();
            VerticalLeftCase9.Location = new Point(0, 708);
            VerticalLeftCase9.BackgroundImage = Properties.Resources.case11;
            Controls.Add(VerticalLeftCase9);

            // UpCase
            HorizontalCase HorizontalupCase1 = new HorizontalCase();
            HorizontalupCase1.Location = new Point(124, 0);
            HorizontalupCase1.BackgroundImage = Properties.Resources.case21;
            Controls.Add(HorizontalupCase1);

            HorizontalCase HorizontalupCase2 = new HorizontalCase();
            HorizontalupCase2.Location = new Point(197, 0);
            HorizontalupCase2.BackgroundImage = Properties.Resources.Case22;
            Controls.Add(HorizontalupCase2);

            HorizontalCase HorizontalupCase3 = new HorizontalCase();
            HorizontalupCase3.Location = new Point(270, 0);
            HorizontalupCase3.BackgroundImage = Properties.Resources.case23;
            Controls.Add(HorizontalupCase3);

            HorizontalCase HorizontalupCase4 = new HorizontalCase();
            HorizontalupCase4.Location = new Point(343, 0);
            HorizontalupCase4.BackgroundImage = Properties.Resources.case24;
            Controls.Add(HorizontalupCase4);

            HorizontalCase HorizontalupCase5 = new HorizontalCase();
            HorizontalupCase5.Location = new Point(416, 0);
            HorizontalupCase5.BackgroundImage = Properties.Resources.case25;
            Controls.Add(HorizontalupCase5);

            HorizontalCase HorizontalupCase6 = new HorizontalCase();
            HorizontalupCase6.Location = new Point(489, 0);
            HorizontalupCase6.BackgroundImage = Properties.Resources.case26;
            Controls.Add(HorizontalupCase6);

            HorizontalCase HorizontalupCase7 = new HorizontalCase();
            HorizontalupCase7.Location = new Point(562, 0);
            HorizontalupCase7.BackgroundImage = Properties.Resources.case27;
            Controls.Add(HorizontalupCase7);

            HorizontalCase HorizontalupCase8 = new HorizontalCase();
            HorizontalupCase8.Location = new Point(635, 0);
            HorizontalupCase8.BackgroundImage = Properties.Resources.case28;
            Controls.Add(HorizontalupCase8);

            HorizontalCase HorizontalupCase9 = new HorizontalCase();
            HorizontalupCase9.Location = new Point(708, 0);
            HorizontalupCase9.BackgroundImage = Properties.Resources.case29;
            Controls.Add(HorizontalupCase9);

            // RightRowCase
            VerticalCase VerticalRightCase1 = new VerticalCase();
            VerticalRightCase1.Location = new Point(782, 124);
            VerticalRightCase1.BackgroundImage = Properties.Resources.case31;
            Controls.Add(VerticalRightCase1);

            VerticalCase VerticalRightCase2 = new VerticalCase();
            VerticalRightCase2.Location = new Point(782, 197);
            VerticalRightCase2.BackgroundImage = Properties.Resources.case32;
            Controls.Add(VerticalRightCase2);
                
            VerticalCase VerticalRightCase3 = new VerticalCase();
            VerticalRightCase3.Location = new Point(782, 270);
            VerticalRightCase3.BackgroundImage = Properties.Resources.case33;
            Controls.Add(VerticalRightCase3);

            VerticalCase VerticalRightCase4 = new VerticalCase();
            VerticalRightCase4.Location = new Point(782, 343);
            VerticalRightCase4.BackgroundImage = Properties.Resources.case34;
            Controls.Add(VerticalRightCase4);

            VerticalCase VerticalRightCase5 = new VerticalCase();
            VerticalRightCase5.Location = new Point(782, 416);
            VerticalRightCase5.BackgroundImage = Properties.Resources.case35;
            Controls.Add(VerticalRightCase5);

            VerticalCase VerticalRightCase6 = new VerticalCase();
            VerticalRightCase6.Location = new Point(782, 489);
            VerticalRightCase6.BackgroundImage = Properties.Resources.case36;
            Controls.Add(VerticalRightCase6);

            VerticalCase VerticalRightCase7 = new VerticalCase();
            VerticalRightCase7.Location = new Point(782, 562);
            VerticalRightCase7.BackgroundImage = Properties.Resources.case37;
            Controls.Add(VerticalRightCase7);

            VerticalCase VerticalRightCase8 = new VerticalCase();
            VerticalRightCase8.Location = new Point(782, 635);
            VerticalRightCase8.BackgroundImage = Properties.Resources.case38;
            Controls.Add(VerticalRightCase8);

            VerticalCase VerticalRightCase9 = new VerticalCase();
            VerticalRightCase9.Location = new Point(782, 708);
            VerticalRightCase9.BackgroundImage = Properties.Resources.case39;
            Controls.Add(VerticalRightCase9);

            // DownCase
            HorizontalCase HorizontaldownCase1 = new HorizontalCase();
            HorizontaldownCase1.Location = new Point(124, 776);
            HorizontaldownCase1.BackgroundImage = Properties.Resources.case9;
            Controls.Add(HorizontaldownCase1);

            HorizontalCase HorizontaldownCase2 = new HorizontalCase();
            HorizontaldownCase2.Location = new Point(197, 776);
            HorizontaldownCase2.BackgroundImage = Properties.Resources.case8;
            Controls.Add(HorizontaldownCase2);

            HorizontalCase HorizontaldownCase3 = new HorizontalCase();
            HorizontaldownCase3.Location = new Point(270, 776);
            HorizontaldownCase3.BackgroundImage = Properties.Resources.case7;
            Controls.Add(HorizontaldownCase3);

            HorizontalCase HorizontaldownCase4 = new HorizontalCase();
            HorizontaldownCase4.Location = new Point(343, 776);
            HorizontaldownCase4.BackgroundImage = Properties.Resources.case6;
            Controls.Add(HorizontaldownCase4);

            HorizontalCase HorizontaldownCase5 = new HorizontalCase();
            HorizontaldownCase5.Location = new Point(416, 776);
            HorizontaldownCase5.BackgroundImage = Properties.Resources.case5;
            Controls.Add(HorizontaldownCase5);

            HorizontalCase HorizontaldownCase6 = new HorizontalCase();
            HorizontaldownCase6.Location = new Point(489, 776);
            HorizontaldownCase6.BackgroundImage = Properties.Resources.case4;
            Controls.Add(HorizontaldownCase6);

            HorizontalCase HorizontaldownCase7 = new HorizontalCase();
            HorizontaldownCase7.Location = new Point(562, 776);
            HorizontaldownCase7.BackgroundImage = Properties.Resources.case3;
            Controls.Add(HorizontaldownCase7);

            HorizontalCase HorizontaldownCase8 = new HorizontalCase();
            HorizontaldownCase8.Location = new Point(635, 776);
            HorizontaldownCase8.BackgroundImage = Properties.Resources.case2;
            Controls.Add(HorizontaldownCase8);

            HorizontalCase HorizontaldownCase9 = new HorizontalCase();
            HorizontaldownCase9.Location = new Point(708, 776);
            HorizontaldownCase9.BackgroundImage = Properties.Resources.case1;
            Controls.Add(HorizontaldownCase9);
        }
    }
}