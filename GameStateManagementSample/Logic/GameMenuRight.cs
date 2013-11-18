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

    //TODO noch ein dickes TODO die einzelnen Menüelemente müssen ausgelagert werden, ich derern zeichnung soll nicht hier stattfinden die beiden kästen die ihr seht sind nur zum testen
    //die werden nacher eigen...

    class GameMenuRight
    {
        #region Fields
        ScreenManager screenManager;
        GraphicsDevice graphicsDevice;
        Texture2D txPixel;
        Rectangle rec;

        WaveManager waveManager;

        Button btnWave;
        #endregion

        public GameMenuRight(int x, int y, int width, int height, WaveManager wm)
        {
            waveManager = wm;
            rec = new Rectangle(x, y, width, height);

            // Send Wave Button
            btnWave = new Button(new Vector2(x + (width / 100 * 50), y + (height / 100 * 70)));
            btnWave.Click += new EventHandler(btnWave_Click);
            btnWave.DrawExtra += new Button.DrawExtraHandler(btnWave_DrawExtra);
        }

        #region WaveButton Zeugs
        void btnWave_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Clicked!");
            waveManager.StartNextWave();
        }

        void btnWave_DrawExtra(SpriteBatch spriteBatch)
        {
            String str = "Send Wave";
            Vector2 stringDimensions = GameplayScreen.gameFont.MeasureString(str);
            spriteBatch.DrawString(GameplayScreen.gameFont, str, btnWave.Position - stringDimensions/2, Color.White);
        }
        #endregion

        public void Initialize(GameStateManagement.ScreenManager sm, ContentManager content)
        {
            this.screenManager = sm;
            graphicsDevice = sm.GraphicsDevice;
            txPixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            txPixel.SetData<Color>(new Color[] { Color.White });

            btnWave.LoadContent(content, "ButtonBG");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(txPixel, rec, Color.Black); // Schwarzes Feld rechts
            btnWave.Draw(spriteBatch); // Send Wave button
        }

        public void Update()
        {
            btnWave.Update();
        }
    }
}