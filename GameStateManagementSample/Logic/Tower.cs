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
        protected Enemy selectedEnemy;
        protected double maxRange = 100; // testweise 100 reichweite
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

            if (!IsEnemyInRange()) // Wenn der Gegner außer reichweite ist, habe ihn nicht mehr selektiert
            {
                selectedEnemy = null;
            }

            // Wenn kein Feind da, tue nichts
            if (selectedEnemy == null)
            {
                Enemy e = GetEnemyInRange();
                if (e == null)
                    return;
            }
            Console.WriteLine("Gegner gefunden");
        }

        protected virtual void shoot(Enemy e) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color color = new Color(255, 0, 255);
            base.Draw(spriteBatch, color);
        }

        protected Boolean IsEnemyInRange()
        {
            // Wenn kein Gegner da, return
            if (selectedEnemy == null)
                return false;
            double tempRange = Math.Sqrt(Math.Pow(Math.Abs(selectedEnemy.Position.X - this.Position.X), 2) +
                Math.Pow(Math.Abs(selectedEnemy.Position.Y - this.Position.Y), 2));
            if (tempRange <= maxRange) // Gegner ist innerhalb Max. Reichweite
            {
                return true;
            }
            return false;
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
        // Canon id 3
        // splash id 4
        //TODO alles bisher nur Platzhalter
    }
}
