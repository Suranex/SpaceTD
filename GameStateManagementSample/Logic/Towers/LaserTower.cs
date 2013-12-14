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
        public LaserTower(Vector2 position,GameLevelTile gameLevelTile)
            : base(texturen[0], position,gameLevelTile)
        {
            name = "Laser Tower";
            cooldown = 0.25;
            type = 0;
            damage = 4;
            maxRange = 200;
        }

        protected override void shoot(Enemy e)
        {
            new Laser(Center, e, damage);
        }
    }
}
