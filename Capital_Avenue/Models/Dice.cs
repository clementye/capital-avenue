﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capital_Avenue.Models
{
    public class Dice
    {
        Random rnd = new Random();

        public List<int> DiceList;
        public bool isDouble { get; set; }
        public int ResultDice { get; set; }
        public int DoubleDice { get; set; }
        
        public Dice()
        {
            this.DiceList = new List<int>();
            this.DoubleDice = 0; // Change for actual rule of Double later
            this.ResultDice = 0;
            this.isDouble = false;
        }

        public void addDice(int dice, int nbDice)
        {
            for (int i = 0; i < nbDice; i++)
            {
                DiceList.Add(dice);
            }
          
        }

        public void DiceThrower()
        {
            isDouble = false;
            ResultDice = 0;
            for (int i = 0;i < DiceList.Count; i++)
            {
                DiceList[i] = rnd.Next(1, 7);
                ResultDice += DiceList[i];
            }
            if (DiceList.Distinct().Count() == 1) 
            {
                isDouble = true;
            }
        }

        
    }
}
