using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class WeaponManager
    {
        public static List<Weapon> waffen = new List<Weapon>();

        public static void addWeapon(Weapon w)
        {
            waffen.Add(w);
        }

        public static void deleteWeapon(Weapon w)
        {
            waffen.Remove(w);
            Console.WriteLine("Waffe entfernt");
        }

        public static void UpdateAll(GameTime gameTime)
        {
            for (int i = waffen.Count - 1; i >= 0; i--)
            {
                if (waffen[i] != null)
                    waffen[i].Update(gameTime);
            }
        }

        public static void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (Weapon w in waffen)
                w.Draw(spriteBatch);
        }
    }
}
