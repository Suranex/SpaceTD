﻿using System;
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
        public Kugel(Vector2 position, Enemy target, int damage, float speed) : base (texturen[1], position, target, damage)
        {
            this.speed = speed;
            WeaponManager.addWeapon(this); // füge dich selbst in die Liste ein
        }

        public Kugel(Texture2D tex, Vector2 position, Enemy target, int damage, float speed)
            : base(tex, position, target, damage)
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

            // position stimmt nie genau überein, daher auf ungefähren pixelabstand (max 5 pixel)
            if (Math.Abs(Position.X - target.Position.X) + Math.Abs(Position.Y - target.Position.Y) < 10)
            {
                dealDamage(damage);
                WeaponManager.deleteWeapon(this);
            }

        }

        protected virtual void dealDamage(int damage)
        {
            target.hit(damage);
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
