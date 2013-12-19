using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class SlowLaser : Laser
    {
        private double seconds;
        private float factor;

        public SlowLaser(Vector2 position, Enemy target, float damage, double seconds, float factor, Tower tower) : base (position, target, damage, tower)
        {
            this.seconds = seconds;
            this.factor = factor;
            this.LaserColor = Color.Aquamarine;
        }

        protected override void hitTarget(float damage)
        {
            Console.WriteLine("slow!");
            base.hitTarget(damage);
            target.setSlow(seconds, factor);
        }
    }
}
