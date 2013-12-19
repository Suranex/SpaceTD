using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace GameStateManagementSample.Logic
{
    class CanonTower : Tower
    {
        public static int startcost=140;
        public static String towerName = "Kanonenturm";
        public static double startCooldown = 0.75;
        public static int startDamage = 160;
        public static int startMaxRange = 100;

        public CanonTower(Vector2 position,GameLevelTile gameLevelTile)
            : base(texturen[1], position,gameLevelTile)
        {
            name = towerName;
            type = 1;
            cooldown = startCooldown;
            damage = startDamage;
            maxRange = startMaxRange;
            Cost = startcost;
        }

        protected override void shoot(Enemy e)
        {
            new Kugel(Center, e, damage, 10.0f);
        }
    }
}
