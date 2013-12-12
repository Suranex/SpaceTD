﻿using System;
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
        protected int cost;                 // wie Teuer der Tower ist
        protected string name;
        protected int towerlevel;
        protected Enemy selectedEnemy;
        protected double maxRange;
        protected double currentCooldown;   // wie lang der Tower noch cooldown hat
        protected double cooldown;          // standard cooldown
        protected int damage;               // Schadenswerte eines Turmes
        private static List<Tower> tower = new List<Tower>();
        protected static List<Texture2D> texturen = new List<Texture2D>();

        #region Content loading
        public static void LoadContent(ContentManager content)
        {
            texturen.Add(content.Load<Texture2D>("greentower1")); // 0 Laser
            texturen.Add(content.Load<Texture2D>("redtower3")); // 1 Canon
            texturen.Add(content.Load<Texture2D>("bluetower1")); // 2 Slow
        }
        #endregion


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
            // schaue, ob der Tower wieder bereit ist
            if (currentCooldown > 0)
            {
                currentCooldown -= (double)gameTime.ElapsedGameTime.TotalSeconds;
                return;
            }

            if (!IsEnemyInRange()) // Wenn der Gegner außer reichweite ist, habe ihn nicht mehr selektiert
            {
                selectedEnemy = null;
            }

            // Wenn kein Feind da, tue nichts
            if (selectedEnemy == null)
            {
                selectedEnemy = GetEnemyInRange();
                if (selectedEnemy == null)
                    return;
            }
            if (!selectedEnemy.IsDead)
            {
                currentCooldown = cooldown; // Tower schießt, hat demnach cooldown
                shoot(selectedEnemy); // Feind bekannt, schieße!
            }
            else
                selectedEnemy = null;
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

    }
}
