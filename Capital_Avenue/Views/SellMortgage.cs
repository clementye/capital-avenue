﻿using Capital_Avenue.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capital_Avenue.Views
{
    
    public partial class SellMortgage : Form
    {
        private Player currentPlayer;

        /*ImageList ImgList;
        ListView ListView;*/
        CheckedListBox PropertiesBox;
        public void SMABox(Player player, string action) // string action is for what is the player doing
        {
            InitializeComponent();
            this.currentPlayer = player;
            this.Size = new Size(300, 500);
            this.Location = new Point(200, 100);
            switch (action)
            {
                case "Sell":
                    this.BaseHeader(player);
                    this.CheckBoxPOption(player);
                    this.SMButtonAdder();
                    break;
                case "Mortgage":
                    this.BaseHeader(player);
                    this.CheckBoxPOption(player);
                    this.MButtonAdder();
                    break;
            }
            
        }

        public void BaseHeader(Player player) // For SELL/MORTGAGE
        {
            // Will be used to show what is the player doing (sell or auction), and also change player in case of auction
            Panel BankPanel = new Panel();
            Label BankLabelSell = new Label();
            Label PlayerName = new Label();
            FlowLayoutPanel flow = new FlowLayoutPanel();
            flow.FlowDirection = FlowDirection.TopDown;
            flow.AutoSize = true;
            PlayerName.Text = player.Name +", vos propriétés :";
            PlayerName.Location = new Point(0, 80);
            BankLabelSell.Text = "Vendre à la Banque";
            BankLabelSell.Font = new Font("Arial", 12, FontStyle.Bold);
            BankLabelSell.Location = new Point(0, 30);
            BankLabelSell.Size = new Size(150, 50);
            BankPanel.Controls.Add(BankLabelSell);
            BankPanel.Controls.Add(PlayerName);
            BankPanel.Size = new Size(200, 100);
            this.Controls.Add(BankPanel);
        }

        public void CheckBoxPOption(Player player)
        {
            PropertiesBox = new CheckedListBox();
            foreach (Property p in player.OwnedProperties)
            {
                PropertiesBox.Items.Add(p.Name, false);
            }
            PropertiesBox.Location = new Point(0, 110);
            // Later, add line to change size, with width at precise value and length depending of owned properties.
            this.Controls.Add(PropertiesBox);

            // Later, change the checkbox design to something more pleasing. Will be using this under.
            /*ListView = new ListView();
            ImgList = new ImageList();
            //ImgList.Images.Add(ImgList);
            ListView.StateImageList = ImgList;
            this.Controls.Add(ListView);*/
        }

        public void SMButtonAdder()
        {
            Button SellButton = new Button(); // Will become it's own function
            Button MortgageButton = new Button(); // Same here
            Panel SMPanel = new Panel();
            SellButton.Text = "Vendre";
            SellButton.Location = new Point(0, 0);
            SellButton.Size = new Size(70, 50);
            SellButton.Click += SellButton_Click;
            MortgageButton.Text = "Hypothéquer";
            MortgageButton.Location = new Point(100, 0);
            MortgageButton.Size = new Size(70, 50);
            MortgageButton.Click += MortgageButton_Click;
            SMPanel.Controls.Add(SellButton);
            SMPanel.Controls.Add(MortgageButton);
            SMPanel.Size = new Size(200, 100);
            SMPanel.Location = new Point(0, 300);
            this.Controls.Add(SMPanel);
        }

        public void MButtonAdder()
        {
            Button MortgageButton = new Button();
            Panel MPanel = new Panel();
            MortgageButton.Text = "Hypothéquer";
            MortgageButton.Location = new Point(0, 0);
            MortgageButton.Size = new Size(70, 50);
            MortgageButton.Click += MortgageButton_Click;
            MPanel.Controls.Add(MortgageButton);
            MPanel.Size = new Size(200, 100);
            MPanel.Location = new Point(0, 300);
            this.Controls.Add(MPanel);
        }
        public void SellButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < PropertiesBox.CheckedItems.Count; i++)
            {
                var FoundProperty = currentPlayer.OwnedProperties.Find(p2 => p2.Name == PropertiesBox.CheckedItems[i]);
                FoundProperty.Owner = null;
                currentPlayer.Capital += FoundProperty.Price / 2;
            }
            currentPlayer.OwnedProperties.RemoveAll(p3 => p3.Owner == null);
            this.Close();
        }

        public void MortgageButton_Click(Object sender, EventArgs e)
        {
            for (int i = 0; i < PropertiesBox.CheckedItems.Count; i++)
            {
                var FoundProperty = currentPlayer.OwnedProperties.Find(p2 => p2.Name == PropertiesBox.CheckedItems[i]);
                FoundProperty.IsInBank = true;
                currentPlayer.Capital += FoundProperty.Price / 2;
            }
            this.Close();
        }
    }
}