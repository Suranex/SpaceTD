using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.Screens
{


    class GameLevelTile
    {
        private bool buildfield;
        private int x;
        private int y;
        private Logic.Tower tower = null;

        public GameLevelTile(bool buildfield,int x,int y)
        {
            this.buildfield = buildfield;
            this.x = x;
            this.y = y;
        }

        public void build(Logic.Tower tower)
        {
            if (buildfield == true)
            {
                //TODO tower unterscheidung der Towertypen und anlegung des Towerobjektes
            }

        }

        public bool Buildfield
        {
            get { return buildfield; }
            set { buildfield = value; }
        }

    }
}
