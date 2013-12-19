﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class SlowTower : Tower
    {
        public static int startcost = 120;
        public static String towerName = "Verlangsamungs Turm";
        public static double startCooldown = 1.5;
        public static int startDamage = 10;
        public static int startMaxRange = 75;
        public static double startSlowTime = 2;
        public static float startSlowFactor = 0.5f;

        private double slowTime;
        private float factor;
        private int numSlowEnemies = 3;

        public SlowTower(Vector2 position, GameLevelTile gameLevelTile)
            : base(texturen[2], position, gameLevelTile)
        {
            name = towerName;
            type = 2;
            slowTime = startSlowTime;
            factor = startSlowFactor;
            damage = startDamage;
            maxRange = startMaxRange;
            cooldown = startCooldown;
            Cost = startcost;
        }

        public override void Upgrade()
        {
            slowTime *= 1.1;
            factor += 0.05f;
            if (factor > 0.9f)
                factor = 0.9f;
            base.Upgrade();
        }

        protected override void shoot(Enemy e)
        {
            List<KeyValuePair<Enemy, Double>> enemyDistances = new List<KeyValuePair<Enemy, Double>>();
            double tempDistance;

            // shoot schießt normalerweise nur auf einen Tower. Um keine größeren Änderungen zu machen, werd ich hier nochmal alle Gegner in Reichweite suchen.
            // Alle Gegner mit Distanz in eine Liste speichern
            foreach (Enemy enemy in WaveManager.Instance.Enemies)
            {
                tempDistance = Vector2.Distance(Center, enemy.Center);
                if (tempDistance < maxRange)
                    enemyDistances.Add(new KeyValuePair<Enemy, Double>(enemy, tempDistance));
            }

            // Liste sortieren nach Distanz
            enemyDistances.Sort((x,y)=>x.Value.CompareTo(y.Value));

            // numSlowEnemies Gegner in der Liste slowen.
            for (int i = 0; i < numSlowEnemies && i < enemyDistances.Count; i++)
            {
                new SlowLaser(Center, enemyDistances[i].Key, damage, slowTime, factor);
            }
        }
    }
}
