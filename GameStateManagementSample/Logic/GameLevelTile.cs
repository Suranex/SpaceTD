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

        public bool build(Vector2 pos, int type)
        {
            if (!buildfield || this.tower != null)
                return false;
            switch (type)
            {
                case 0: // Laser
                    if (Player.getInstance().costMoney(20)) // testweise, TODO Balance
                    {
                        this.tower = new LaserTower(pos);
                        return true;
                    }
                    break;
                case 1: // Canon
                    if (Player.getInstance().costMoney(50)) // testweise kosten von 50. Muss noch abh. von Gui selected Tower werden
                    {
                        this.tower = new CanonTower(pos);
                        return true;
                    }
                    break;
                case 2: // Slow
                    if (Player.getInstance().costMoney(100)) // testweise kosten von 50. Muss noch abh. von Gui selected Tower werden
                    {
                        this.tower = new SlowTower(pos);
                        return true;
                    }
                    break;
                default:
                    break;
            }

            return false;
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
