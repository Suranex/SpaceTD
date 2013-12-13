using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class SlowKugel : Kugel
    {
        private double seconds;
        private float factor;

        public SlowKugel(Vector2 position, Enemy target, int damage, float speed, double seconds, float factor) : base (texturen[2], position, target, damage, speed)
        {
            this.seconds = seconds;
            this.factor = factor;
        }

        protected override void dealDamage(int damage)
        {
            Console.WriteLine("slow!");
            base.dealDamage(damage);
            target.setSlow(seconds, factor);
        }
    }
}
