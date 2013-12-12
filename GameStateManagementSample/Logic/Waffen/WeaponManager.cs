using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic.Waffen
{
    class WeaponManager
    {
        private static List<Weapon> waffen = new List<Weapon>();

        public static void addWeapon(Weapon w)
        {
            waffen.Add(w);
        }

        public static void deleteWeapon(Weapon w)
        {
            waffen.Remove(w);
        }

        public void Update(GameTime gametime)
        {
            foreach (Weapon w in waffen)
                w.Update(gametime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Weapon w in waffen)
                w.Draw(spriteBatch);
        }
    }
}
