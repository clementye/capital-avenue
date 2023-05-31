﻿using Capital_Avenue.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Capital_Avenue.Views
{
    public class PlayerTextbox:UserControl
    {

        //public Panel p1;
        public TextBox Textbox;
        public ComboBox comboBox { get; set; }
        public PlayerTextbox(int position)
        {
            List<string> colors = new List<string> {
                "#6AFF2D",
                "#401ad6",
                "#6AFF2D",
                "#6AFF2D",
                "#6AFF2D",
            };
            int basePosition = 0;
            int space = 10;
            int pos = (basePosition + 250 * (position-1) ) + space * position;
            this.AddTexbox(position, colors[position-1], pos);
        }

        public void AddTexbox(int numberChoice,string color, int position)
        {
            Panel p1 = new Panel();
            Label l1 = new Label();
            Textbox = new TextBox();
            comboBox = new ComboBox();
            comboBox.Size = new Size(150, 100);
            Textbox.Size = new Size(150, 100);
            p1.Size = new Size(200, 150);
            p1.BackColor = ColorTranslator.FromHtml(color);
            this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox.DrawMode = DrawMode.OwnerDrawVariable;
            comboBox.Items.Clear();
            this.Controls.Add(p1);
            this.ImageShow();
            l1.Text = "joueur " + numberChoice;
            p1.Controls.Add(l1);
            p1.Controls.Add(comboBox);
            p1.Controls.Add(Textbox);
            comboBox.Left = (this.ClientSize.Width - comboBox.Width) / 2;
            comboBox.Top = (this.ClientSize.Height - comboBox.Height) / 2;
            Textbox.Left = Width / 2 - Textbox.Width / 2;
            Textbox.Top = Height / 2 - Textbox.Height / 2 - comboBox.Height - 2;
            comboBox.Top = Textbox.Bottom +1;
            this.Location = new Point(0, position);
            this.Size = new Size(250,250);
        }
      public void ImageShow()
        {
            Image[] images =
            {
                Properties.Resources.face1,
                Properties.Resources.face2,
                Properties.Resources.Chapeau,
                Properties.Resources.Cloche,
            };
            comboBox.DisplayImages(images);
            comboBox.SelectedIndex = 0;
            comboBox.DropDownHeight = 200;
        }
       

    }
}