using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{


    class GameLevelTile
    {
        protected bool buildfield;
        protected int x;
        protected int y;
        protected Tower tower = null;

        public GameLevelTile(bool buildfield,int x,int y)
        {
            this.buildfield = buildfield;
            this.x = x;
            this.y = y;
        }

        public bool build(Texture2D tex, Vector2 pos)
        {
            if (!buildfield || this.tower != null)
                return false;

            this.tower = new CanonTower(tex, pos);
            return true;
        }

        public void destroy()
        {
            tower = null;
        }

        public bool Buildfield
        {
            get { return buildfield; }
            set { buildfield = value; }
        }

        public Tower Tower
        {
            get { return tower; }
        }

    }
}
