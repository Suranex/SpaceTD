using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class Kugel : Weapon
    {
        float speed = 1.0f;

        /* 
         * Jeder Schuss wird eigenständig als Objekt behandelt. Diese werden in der Waffen.cs verwaltet
         */
        public Kugel(Texture2D texture, Vector2 position, Enemy target, int damage, float speed) : base (texture, position, target, damage)
        {
            this.speed = speed;
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

            // Positionsbestimmung
            Vector2 direction = target.Position - position;
            direction.Normalize();
            position += Vector2.Multiply(direction, speed);

            // position stimmt nie genau überein, daher auf ungefähren pixelabstand (max 10 pixel)
            if (Math.Abs(Position.X - target.Position.X) + Math.Abs(Position.Y - target.Position.Y) < 10)
            {
                Console.WriteLine("hit!!!");
                target.hit(damage);
                WeaponManager.deleteWeapon(this);
            }

        }

        /*
         * Kugelposition neu Zeichnen
         */
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!target.IsDead)
                base.Draw(spriteBatch);

        }
    }
}
