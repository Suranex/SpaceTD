using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using GameStateManagementSample.Logic;

namespace GameStateManagementSample.Logic
{

    class GameMenuRight
    {
        #region Fields
        ScreenManager screenManager;
        GraphicsDevice graphicsDevice;
        Texture2D txPixel;
        Rectangle rec;

        WaveManager waveManager;

        TowerButton btnTowerGreenOne;
        TowerButton btnTowerRedOne;
        TowerButton btnTowerBlueOne;
        TowerButton btnTowerPurpleOne;

        Button btnWave;
        #endregion

        public GameMenuRight(int x, int y, int width, int height, WaveManager wm)
        {
            waveManager = wm;
            rec = new Rectangle(x, y, width, height);

            // Send Wave Button
            btnWave = new Button(new Vector2(x + (width / 2), y + (height / 10 * 7)),
                "Send wave!", Color.White, Color.Red,Color.Gray,Color.Green);
            btnWave.Click += new EventHandler(btnWave_Click);
            btnWave.DrawExtra += new Button.DrawExtraHandler(btnWave_DrawExtra);

            btnTowerGreenOne = new TowerButton(new Vector2(x + (width /100 *20 ), y + (height / 100 * 20)));
            btnTowerGreenOne.Click += new EventHandler(btnTowerGreenOne_Click);

            btnTowerRedOne = new TowerButton(new Vector2(x + (width / 100 * 40), y + (height / 100 * 20)));
            btnTowerRedOne.Click += new EventHandler(btnTowerRedOne_Click);

            btnTowerBlueOne = new TowerButton(new Vector2(x + (width / 100 * 60), y + (height / 100 * 20)));
            btnTowerBlueOne.Click += new EventHandler(btnTowerBlueOne_Click);

            btnTowerPurpleOne = new TowerButton(new Vector2(x + (width / 100 * 80), y + (height / 100 * 20)));
            btnTowerPurpleOne.Click += new EventHandler(btnTowerPurpleOne_Click);
        }

        #region btnTowerGreenOne Handlers
        void btnTowerGreenOne_Click(object sender, EventArgs e)
        {
            GameplayScreen.selectetTowerType = 1;
           // Console.Write("green selected \n");
        }
        #endregion

        #region btnTowerRedOne Handlers
        void btnTowerRedOne_Click(object sender, EventArgs e)
        {
            GameplayScreen.selectetTowerType = 0;
        }
        #endregion

        #region btnTowerBlueOne Handlers
        void btnTowerBlueOne_Click(object sender, EventArgs e)
        {
            GameplayScreen.selectetTowerType = 3;
        }
        #endregion

        #region btnTowerPurpleOne Handlers
        void btnTowerPurpleOne_Click(object sender, EventArgs e)
        {
            GameplayScreen.selectetTowerType = 4;
        }
        #endregion

        #region btnWave Handlers
        void btnWave_Click(object sender, EventArgs e)
        {
            waveManager.StartNextWave();
        }

        void btnWave_DrawExtra(SpriteBatch spriteBatch)
        {
            // Hier kann man extra Sachen beim Button zeichnen, z.B. nen Tower Sprite
        }
        #endregion

        public void Initialize(GameStateManagement.ScreenManager sm, ContentManager content)
        {
            this.screenManager = sm;
            graphicsDevice = sm.GraphicsDevice;
            txPixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            txPixel.SetData<Color>(new Color[] { Color.White });

            btnWave.LoadContent(content, "ButtonBG");
            btnTowerGreenOne.LoadContent(content, Tower.texturen[1]);
            btnTowerRedOne.LoadContent(content, Tower.texturen[0]);
            btnTowerBlueOne.LoadContent(content, Tower.texturen[2]);
            btnTowerPurpleOne.LoadContent(content, Tower.texturen[3]);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(txPixel, rec, Color.Black); // Schwarzes Feld rechts
            btnWave.Draw(spriteBatch); // Send Wave button
            btnTowerGreenOne.Draw(spriteBatch);
            btnTowerRedOne.Draw(spriteBatch);
            btnTowerBlueOne.Draw(spriteBatch);
            btnTowerPurpleOne.Draw(spriteBatch);
        }

        public void Update()
        {
            btnWave.Update();
            btnTowerGreenOne.Update();
            btnTowerRedOne.Update();
            btnTowerBlueOne.Update();
            btnTowerPurpleOne.Update();            
        }
    }
}