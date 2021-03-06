﻿using System;
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
        public Laser(Vector2 position, Enemy target, float damage, Tower tower) : base (texturen[0], position, target, damage, tower)
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

            hitTarget(damage);
            ParticleManager.Instance.GenerateExplosion(target.Center, laserColor, 4);
            WeaponManager.deleteWeapon(this);
        }

        protected virtual void hitTarget(float dmg)
        {
            if (target.hit(damage)) // Wenn true, dann war der schuss tötlich
                if (tower != null)  // Check ob der Tower noch existiert
                    tower.killDone();   // erhöhe killcounter um 1
        }

        /*
         * Kugelposition neu Zeichnen
         */
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!target.IsDead)
            {
                spriteBatch.DrawLine(texturen[0], position, target.Center, laserColor, 2f);
            }

        }
    }
}
