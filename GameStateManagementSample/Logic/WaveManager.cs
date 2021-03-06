﻿using System;
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

        private int numOfWaves; // Wieviele Wellen werden insgesamt erzeugt
        private Queue<Wave> waves; // Warteschlange mit den Wellen
        //private Texture2D enemyTexture; // Textur für die Gegner
                                        // TODO: Unterschiedliche Texturen
        private List<Texture2D> textures;
        private bool waveFinished = true; // Welle geschafft?
        private Level level; // Referenz zum Level
        #endregion

        #region Properties
        internal static WaveManager Instance
        {
            get { return WaveManager.instance; }
        }

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
            get { return CurrentWave.RoundNumber + 1; }
        }

        public bool AllWavesFinished
        {
            get { return waves.Count <= 1 && CurrentWave.IsRoundOver; }
        }
        #endregion

        #region Content loading
        public void LoadContent(ContentManager content)
        {
            textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("Enemies/pinkpinwheel"));  // standard, spin
            textures.Add(content.Load<Texture2D>("Enemies/orangetriangle")); // schnell, nonspin
            textures.Add(content.Load<Texture2D>("Enemies/purplesquare2")); // stark, nonspin
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
            waves = new Queue<Wave>();
            for (int i = 0; i < numOfWaves; i++)
            {
                // Hier kann man die Parameter der einzelnen Wellen verändern..
                // z.B. könnte man auch alle 5 Wellen eine "Schnelle" Welle haben, oder nen Boss
                // TODO: Auslagerung in XML/ini/whatever anstatt hardcoded, vlt. dann auch einfach für jede Welle einzeln den Parametersatz angeben (Anstatt dieses rumgefrickel mit multiplikatoren)
                int numOfEnemies = 20 * ((i / 3) + 1);
                int health = (int)(450 * ((i / 5f) + 1));
                int bounty = (int)(5 * ((i / 5f) + 1));
                float speed = 2.0f;
                int respawnTime = 450;
                bool spins = true;
                Texture2D texture = textures[0];

                // Schnelle Welle alle 3 Wellen, dafür weniger HP
                if ((i + 1) % 3 == 0) {
                    speed = 4.0f;
                    health = (int) (health / 1.4);
                    texture = textures[1];
                    spins = false;
                } 
                // Stärkere Gegner alle 5 Wellen, dafür nur halb so viele
                else if((i + 1) % 5 == 0)
                {
                    numOfEnemies /= 2;
                    health *= 2;
                    bounty = (int)(bounty * 2.3);
                    texture = textures[2];
                    spins = false;
                }


                Wave wave = new Wave(i, numOfEnemies, level, texture, health, speed, bounty, respawnTime, spins);
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
                if(!AllWavesFinished) // Letzte Welle sollte hinterher nicht entfernt werden, sonst gibt's Fehler an einigen Stellen.
                    waves.Dequeue();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentWave.Draw(spriteBatch);
        }
    }
}
