using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class Laser : Weapon
    {
        /* 
         * Jeder Schuss wird eigenständig als Objekt behandelt. Diese werden in der Waffen.cs verwaltet
         */
        public Laser(Vector2 position, Enemy target, float damage) : base (texturen[0], position, target, damage)
        {
            WeaponManager.addWeapon(this); // füge dich selbst in die Liste ein
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
                target.hit(damage);
                WeaponManager.deleteWeapon(this);


        }

        /*
         * Kugelposition neu Zeichnen
         */
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!target.IsDead)
            {
                float distance = Vector2.Distance(Position, target.Center);

                float angle = (float)Math.Atan2((double)(target.Center.Y - Position.Y), (double)(target.Center.X - Position.X));
                // TODO testtex2 austauschen
                spriteBatch.Draw(texturen[0], Position, null, Color.Red, angle, Vector2.Zero, new Vector2(distance, 1.2f), SpriteEffects.None, 1);
            }

        }
    }
}
