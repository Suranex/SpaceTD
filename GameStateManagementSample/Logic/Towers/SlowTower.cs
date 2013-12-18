using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class SlowTower : Tower
    {
        private double slowTime;
        private float factor;

        public SlowTower(Vector2 position, GameLevelTile gameLevelTile)
            : base(texturen[2], position, gameLevelTile)
        {
            name = "Slow Tower";
            type = 2;
            slowTime = 4;
            factor = 0.5f;
            damage = 3;
            maxRange = 75;
            cooldown = 2;
            Cost = 40;
        }

        public override void Upgrade()
        {
            slowTime *= 1.1;
            factor += 0.05f;
            if (factor > 0.9f)
                factor = 0.9f;
            base.Upgrade();
        }

        protected override void shoot(Enemy e)
        {
            new SlowKugel(Center, e, damage, 10.0f, slowTime, factor);
        }
    }
}
