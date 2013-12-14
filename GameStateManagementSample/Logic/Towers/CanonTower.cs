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
        public CanonTower(Vector2 position,GameLevelTile gameLevelTile)
            : base(texturen[1], position,gameLevelTile)
        {
            name = "Canon Tower";
            type = 1;
            cooldown = 0.75;
            damage = 15;
            maxRange = 100;
        }

        protected override void shoot(Enemy e)
        {
            new Kugel(Center, e, damage, 10.0f);
        }
    }
}
