using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.Logic
{


    class GameLevelTile
    {
        private bool buildfield;
        private int x;
        private int y;
        private Tower tower = null;

        public GameLevelTile(bool buildfield,int x,int y)
        {
            this.buildfield = buildfield;
            this.x = x;
            this.y = y;
        }

        public bool build(Tower tower)
        {
            if (!buildfield || this.tower != null)
                return false;

            this.tower = tower;
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
