using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagementSample.Utility;

namespace GameStateManagementSample.Logic
{
    class Laser : Weapon
    {
        private Color laserColor;

        public Color LaserColor
        {
            get { return laserColor; }
            set { laserColor = value; }
        }

        /* 
         * Jeder Schuss wird eigenständig als Objekt behandelt. Diese werden in der Waffen.cs verwaltet
         */
        public Laser(Vector2 position, Enemy target, float damage) : base (texturen[0], position, target, damage)
        {
            WeaponManager.addWeapon(this); // füge dich selbst in die Liste ein
            laserColor = Color.Green;
        }

        /** 
         * Kugelposition bei jeden Update neu berechnen 
         */
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Wenn kein Gegner mehr da, lösche Schuss
            if (target.IsDead)
            {
                WeaponManager.deleteWeapon(this);
                return;
            }

            Console.WriteLine("hit!!!");
            hitTarget(damage);
            WeaponManager.deleteWeapon(this);
        }

        protected virtual void hitTarget(int dmg)
        {
            target.hit(damage);
        }

        /*
         * Kugelposition neu Zeichnen
         */
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!target.IsDead)
            {
                spriteBatch.DrawLine(texturen[0], position, target.Center, laserColor, 1f);
            }

        }
    }
}
