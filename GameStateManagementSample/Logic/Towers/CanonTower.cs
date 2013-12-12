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

        public new void shoot(Enemy e)
        {
            Console.WriteLine("Canon Shoot");
        }
    }
}
