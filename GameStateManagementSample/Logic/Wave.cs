using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class Wave
    {
        #region Fields
        private int numOfEnemies; // Anzahl der Gegner die gespawnt werden sollen
        private int waveNumber; // Bei welcher Welle sind wir?
        private float spawnTimer = 0; // Vergangene Zeit wird hier festgehalten
        private int enemiesSpawned = 0; // Wieviele Gegner wurden schon gespawnt?

        private bool enemyAtEnd; // Hat ein Gegner das Ende erreicht?
        private bool spawningEnemies; // Spawnen wir Gegner?
        private Level level; // Referenz zum Level

        private Texture2D enemyTexture; // Textur die von den Gegnern benutzt werden soll
        private float enemyHealth; // Wieviel HP sollen die Gegner haben?
        private float enemySpeed; // Wie schnell sollen die Gegner sein?
        private int enemyBounty; // Wieviel Geld gibt es wenn man einen Gegner killt?
        private int respawnTime; // Wieviel Zeit vergeht zwischen den spawns? WICHTIG: Angabe in ms!
        private bool hasSpinningEnemies; // Drehen sich die Gegner ständig? Oder zeigen sie in Richtung des nächsten Waypoints?
        public List<Enemy> enemies = new List<Enemy>(); // Liste mit allen Gegnern
        #endregion

        #region Properties
        public bool IsRoundOver {
            get { return enemies.Count == 0 && enemiesSpawned == numOfEnemies; }
        }

        public int RoundNumber
        {
            get { return waveNumber; }
        }

        public bool EnemyAtEnd
        {
            get { return enemyAtEnd; }
            set { enemyAtEnd = value; }
        }

        public List<Enemy> Enemies
        {
            get { return enemies; }
        }
        #endregion

        public Wave(int waveNumber, int numOfEnemies, Level level, Texture2D enemyTexture, float health, float speed, int bounty, int respawnTime, bool hasSpinningEnemies)
        {
            this.waveNumber = waveNumber;
            this.numOfEnemies = numOfEnemies;
            this.level = level;
            this.enemyTexture = enemyTexture;
            this.enemyHealth = health;
            this.enemySpeed = speed;
            this.enemyBounty = bounty;
            this.respawnTime = respawnTime;
            this.hasSpinningEnemies = hasSpinningEnemies;
        }

        private void AddEnemy()
        {
            Enemy enemy = new Enemy(enemyTexture, level.waypoints.Peek(),
                enemyHealth, enemyBounty, enemySpeed, hasSpinningEnemies);
            enemy.SetWaypoints(level.waypoints);
            enemies.Add(enemy);
            spawnTimer = 0;
            enemiesSpawned++;
        }

        public void Start()
        {
            spawningEnemies = true;
        }

        public void Update(GameTime gameTime)
        {
            if (enemiesSpawned == numOfEnemies)
                spawningEnemies = false;

            if (spawningEnemies)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (spawnTimer > respawnTime)
                    AddEnemy();
            }

            for (int i = 0; i < enemies.Count; i++ )
            {
                Enemy enemy = enemies[i];
                enemy.Update(gameTime);
                if (enemy.IsDead)
                {
                    if (enemy.CurrentHealth > 0)
                    {
                        enemyAtEnd = true;
                        Player.getInstance().reduceLive(1);

                    }
                    else
                    {
                        Player.getInstance().Kills++;
                        Player.getInstance().addPoints(enemy.BountyGiven);
                    }
                    enemies.Remove(enemy);
                    i--;

                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
                enemy.Draw(spriteBatch);
        }
    }
}
