using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class WaveManager
    {
        #region Fields
        private static WaveManager instance;

        internal static WaveManager Instance
        {
            get { return WaveManager.instance; }
        }

        private int numOfWaves; // Wieviele Wellen werden insgesamt erzeugt
        private Queue<Wave> waves = new Queue<Wave>(); // Warteschlange mit den Wellen
        private Texture2D enemyTexture; // Textur für die Gegner
                                        // TODO: Unterschiedliche Texturen
        private bool waveFinished = true; // Welle geschafft?
        private Level level; // Referenz zum Level
        #endregion

        #region Properties
        public Wave CurrentWave
        {
            get { return waves.Peek(); }
        }

        public List<Enemy> Enemies
        {
            get { return CurrentWave.Enemies; }
        }

        public int Round
        {
            get { return CurrentWave.RoundNumber; }
        }
        #endregion

        #region Content loading
        public void LoadContent(ContentManager content)
        {
            enemyTexture = content.Load<Texture2D>("enemy");
        }
        #endregion

        public WaveManager(Level level, int numOfWaves)
        {
            this.level = level;
            this.numOfWaves = numOfWaves;
            instance = this;
        }

        public void InitWaves()
        {
            for (int i = 0; i < numOfWaves; i++)
            {
                // Hier kann man die Parameter der einzelnen Wellen verändern..
                // z.B. könnte man auch alle 5 Wellen eine "Schnelle" Welle haben, oder nen Boss
                // TODO: Auslagerung in XML/ini/whatever anstatt hardcoded, vlt. dann auch einfach für jede Welle einzeln den Parametersatz angeben (Anstatt dieses rumgefrickel mit multiplikatoren)
                int numOfEnemies = 5 * ((i / 3) + 1);
                int health = (int)(50 * ((i / 5f) + 1));
                int bounty = (int)(20 * ((i / 5f) + 1));
                float speed = 5.0f;
                int respawnTime = 500;

                /* Beispiel für ne "Schnelle" Welle alle 5 Wellen:
                if(i+1 % 5 == 0) { speed = 5.0f; }
                */

                Wave wave = new Wave(i, numOfEnemies, level, enemyTexture, health, speed, bounty, respawnTime);
                waves.Enqueue(wave);
            }
        }

        public void StartNextWave()
        {
            if (waves.Count > 0 && waveFinished)
            {
                waves.Peek().Start();
                waveFinished = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            CurrentWave.Update(gameTime);

            if (CurrentWave.IsRoundOver)
            {
                waveFinished = true;
                waves.Dequeue();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentWave.Draw(spriteBatch);
        }
    }
}
