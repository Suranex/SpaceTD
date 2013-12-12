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
        public CanonTower(Texture2D tex, Vector2 position)
            : base(tex, position)
        {
            type = 3;
        }

        protected override void shoot(Enemy e)
        {
            if (WeaponManager.waffen.Count==0) // testweise nur 1 schuss auf dem Feld!
                new Kugel(GameplayScreen.testtex2, this.position, e, 50, 5.0f);
            // Console.WriteLine("Canon Shoot");
        }
    }
}
