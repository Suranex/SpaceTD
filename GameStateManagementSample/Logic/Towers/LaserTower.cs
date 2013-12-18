using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class LaserTower : Tower
    {
        public static int startcost = 100;
        public static String towerName = "Laserturm";
        public static double startCooldown = 0.15;
        public static int startDamage = 32;
        public static int startMaxRange = 150;

        public LaserTower(Vector2 position,GameLevelTile gameLevelTile)
            : base(texturen[0], position,gameLevelTile)
        {
            name = towerName;
            cooldown = startCooldown;
            type = 0;
            damage = startDamage;
            maxRange = startMaxRange;
            Cost = startcost;
        }

        protected override void shoot(Enemy e)
        {
            new Laser(Center, e, damage);
        }
    }
}
