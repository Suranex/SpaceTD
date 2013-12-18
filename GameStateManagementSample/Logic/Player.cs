﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.Logic
{
    class Player
    {
        private double points;
        private long kills;
        private float money = 2000;
        private int live = 1;
        private String name;
        private static Player player; // instance of player

        private Player()
        {
            //TODO Werte aus optionen ziehen und startwerte anpassen
            name = OptionsMenuScreen.name;
        }

        public static Player getInstance(){
            if(player == null)
                player = new Player();
            return player;
        }

        public void addPoints(float points)
        {
            this.points = this.points + points;
        }

        public void reduceLive(int count)
        {
            this.live = this.live - count;
            if (live <= 0)
            {
                GameplayScreen.getInstance().GameOver();
            }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Live
        {
            get { return live; }
            set { live = value; }
        }

        public float Money
        {
            get { return money; }
            set { money = value; }
        }

        public long Kills
        {
            get { return kills; }
            set { kills = value; }
        }

        public double Points
        {
            get { return points; }
            set { points = value; }
        }

        /*
         * Player bekommt Geld 
         */
        public void rewardMoney(float rewarded)
        {
            money += rewarded;
        }

        /*
         * Player gibt Geld aus, sofern genug vorhanden
         */
        public bool costMoney(float cost)
        {
            if ((money - cost) > 0)
            {
                money -= cost;
                return true;
            }
            return false;
        }

    }
}
