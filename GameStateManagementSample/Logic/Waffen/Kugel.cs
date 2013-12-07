using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic.Waffen
{
    class Kugel : Weapon
    {
        Vector2 position;
        Enemy target;
        double damage;
        double speed;

        /* 
         * Jeder Schuss wird eigenständig als Objekt behandelt. Diese werden in der Waffen.cs verwaltet
         */
        public Kugel(Vector2 position, Enemy target, double damage, double speed)
        {
            this.position = position;
            this.target = target;
            this.damage = damage;
            this.speed = speed;
            WeaponManager.addWeapon(this); // füge dich selbst in die Liste ein
        }

        /** 
         * Kugelposition bei jeden Update neu berechnen 
         */
        public override void Update(GameTime gametime)
        {
            // TODO weg berechnung
            // Dazu eigene aktuelle position neu berechnen im bezug auf Gegener position. 
        }

        /*
         * Kugelposition neu Zeichnen
         */
        public override void Draw()
        {

        }



    }
}
