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
        public int type; // e.g. as reference to the power picture
        private int cost;                 // wie Teuer der Tower ist
        public string name;
        public int towerlevel = 1;
        protected Enemy selectedEnemy;
        public int maxRange;
        protected double currentCooldown;   // wie lang der Tower noch cooldown hat
        public double cooldown;          // standard cooldown
        public float damage;               // Schadenswerte eines Turmes
        private int killcounter = 0;
        public GameLevelTile gameLevelTile;
        private static List<Tower> tower;
        public static List<Texture2D> texturen;


        public static float upgradeDamageFactor = 1.1f;
        public static float upgradeCostFactor = 1.3f; 
        public static float upgradeRangeFactor = 1.02f;

        public int Killcounter
        {
            get { return killcounter; }
        }

        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        #region Content loading
        public static void LoadContent(ContentManager content)
        {
            texturen.Add(content.Load<Texture2D>("Tower/greentower1")); // 0 Laser
            texturen.Add(content.Load<Texture2D>("Tower/redtower3")); // 1 Canon
            texturen.Add(content.Load<Texture2D>("Tower/bluetower1")); // 2 Slow
            texturen.Add(content.Load<Texture2D>("Tower/purpletower")); // 3 single target BEAM :D - unimplemented?
        }

        public static void Initialize()
        {
            tower = new List<Tower>();
            texturen = new List<Texture2D>();
        }
        #endregion

        internal static List<Tower> Towers
        {
            get { return Tower.tower; }
        }

        protected Tower(Texture2D tex, Vector2 Position,GameLevelTile gameLevelTile) : base (tex, Position)
        {
            this.gameLevelTile = gameLevelTile;
            this.Position = Position;
            tower.Add(this);
        }

        public void killDone()
        {
            killcounter++;
        }

        public virtual void Upgrade()
        {
            if (Player.getInstance().costMoney((int)(cost * (upgradeCostFactor - 1f))))
            {
                towerlevel++;
                damage *= upgradeDamageFactor;
                cost = (int)(cost * upgradeCostFactor);
                maxRange = (int)(maxRange * upgradeRangeFactor);
            }
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
            base.Draw(spriteBatch);
            spriteBatch.DrawString(GameplayScreen.gameFont, towerlevel.ToString(), Position + new Vector2(5, 1), Color.White);
        }

        protected Boolean IsEnemyInRange()
        {
            // Wenn kein Gegner da, return
            if (selectedEnemy == null)
                return false;
            // Vector2.Distance...
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
            double range = Double.MaxValue;
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
