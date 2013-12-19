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
        int splashRange = 35;

        /* 
         * Jeder Schuss wird eigenständig als Objekt behandelt. Diese werden in der Waffen.cs verwaltet
         */
        public Kugel(Vector2 position, Enemy target, float damage, float speed, Tower tower) : base (texturen[1], position, target, damage, tower)
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

            // position stimmt nie genau überein, daher auf ungefähren pixelabstand
            if (Vector2.Distance(Center, target.Center) <= speed)
            {
                foreach (Enemy e in WaveManager.Instance.CurrentWave.Enemies)
                {
                    float range = Vector2.Distance(Center, e.Center);

                    if (range < splashRange)
                    {
                        new Laser(Center, e, damage, tower).LaserColor = Color.Red;
                        //e.hit(damage);
                    }
                }
                ParticleManager.Instance.GenerateExplosion(Center, Color.Red, 7);
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
