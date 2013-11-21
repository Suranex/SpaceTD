using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.Logic
{
    class Player
    {
        private double points;
        private long kills;
        private double money = 200;
        private int live = 40;
        private String name;

        public Player()
        {
            //TODO Werte aus optionen ziehen und startwerte anpassen
            name = OptionsMenuScreen.name;
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

        public double Money
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

    }
}
