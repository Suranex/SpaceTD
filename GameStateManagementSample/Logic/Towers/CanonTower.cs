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
        public CanonTower(Vector2 position)
            : base(texturen[2], position)
        {
            type = 3;
            cooldown = 0.75;
        }

        protected override void shoot(Enemy e)
        {
            new Kugel(GameplayScreen.testtex2, this.position, e, 15, 10.0f);
        }
    }
}
