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
        public SlowTower(Vector2 position)
            : base(texturen[2], position)
        {
            type = 2;
        }
    }
}
