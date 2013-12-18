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

        public SlowKugel(Vector2 position, Enemy target, float damage, float speed, double seconds, float factor) : base (texturen[2], position, target, damage, speed)
        {
            this.seconds = seconds;
            this.factor = factor;
            this.LaserColor = Color.Aquamarine;
        }

        protected override void dealDamage(float damage)
        {
            Console.WriteLine("slow!");
            base.hitTarget(dmg);
            target.setSlow(seconds, factor);
        }
    }
}
