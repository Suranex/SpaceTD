using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class Tower : Sprite
    {
        protected int type; // e.g. as reference to the power picture
        protected string name;
        protected int towerlevel;
        protected double cooldown;
        protected double maxRange;
        protected Texture2D texture;
        protected Vector2 Position; // in Pixel
        private static List<Tower> tower = new List<Tower>();

        public Vector2 OriginPosition
        {
            get { return new Vector2(texture.Width / 2 + Position.X, texture.Height / 2 + Position.Y); }
        }

        internal static List<Tower> Towers
        {
            get { return Tower.tower; }
        }

        protected Tower(Texture2D tex, Vector2 Position) : base (tex, Position)
        {
            this.Position = Position;
            tower.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Wenn kein Feind da, tue nichts
            Enemy e = GetEnemyInRange();
            if (e == null)
                return;
            Console.WriteLine("Gegner gefunden");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color color = new Color(255, 0, 255);
            base.Draw(spriteBatch, color);
        }


        protected Enemy GetEnemyInRange()
        {
            // TODO untested - function check
            double range = 100;
            double tempRange;
            Enemy foundEnemy = null;
            foreach (Enemy e in WaveManager.Instance.CurrentWave.Enemies)
            {
                tempRange = Math.Sqrt(Math.Pow(Math.Abs(e.Position.X - this.Position.X), 2) + 
                    Math.Pow(Math.Abs(e.Position.Y - this.Position.Y), 2));
                if (tempRange < range && tempRange <= maxRange)
                {
                    range = tempRange;
                    foundEnemy = e;
                }
            }
            return foundEnemy;
        }

        // Laser id 1
        // Slow id 2
        // Multi target id 3
        // splash id 4
        //TODO alles bisher nur Platzhalter
    }
}
