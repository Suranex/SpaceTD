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
        Enemy target;
        double damage;
        float speed = 1.0f;

        /* 
         * Jeder Schuss wird eigenständig als Objekt behandelt. Diese werden in der Waffen.cs verwaltet
         */
        public Kugel(Texture2D texture, Vector2 position, Enemy target, double damage, float speed) : base (texture, position)
        {
            this.target = target;
            this.damage = damage;
            this.speed = speed;
            WeaponManager.addWeapon(this); // füge dich selbst in die Liste ein
        }

        /** 
         * Kugelposition bei jeden Update neu berechnen 
         */
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // TODO weg berechnung
            // Dazu eigene aktuelle position neu berechnen im bezug auf Gegener position. 
            if (target == null)
            {
                Console.WriteLine("kein Gegner!");
                return;
            }
            Vector2 direction = target.Position - position;
            direction.Normalize();

            position += Vector2.Multiply(direction, speed);
            //position.X += speed;
            Console.WriteLine(position.X);
        }

        /*
         * Kugelposition neu Zeichnen
         */
        public override void Draw(SpriteBatch spriteBatch)
        {

                base.Draw(spriteBatch);

        }
    }
}
