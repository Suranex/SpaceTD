using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using GameStateManagementSample.Logic;
using GameStateManagementSample.Logic.Towers;

namespace GameStateManagementSample.Logic
{

    //TODO noch ein dickes TODO die einzelnen Menüelemente müssen ausgelagert werden, ich derern zeichnung soll nicht hier stattfinden die beiden kästen die ihr seht sind nur zum testen
    //die werden nacher eigen...

    class GameMenuRight : Clickable
    {
        GameStateManagement.ScreenManager screenManager;
        GraphicsDevice graphicsDevice;

        // TODO: Nur zum testen hier... denk ich :D
        WaveManager waveManager;

        public GameMenuRight(int x, int y, int width, int height, WaveManager wm) : base (x,y,width,height)
        {
            waveManager = wm;
        }

        Texture2D txPixel;
        Rectangle rec;

        public void Initialize(GameStateManagement.ScreenManager sm)
        {
            this.screenManager = sm;
            graphicsDevice = sm.GraphicsDevice;
            txPixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            txPixel.SetData<Color>(new Color[] { Color.White });
            rec = new Rectangle(x, y, width, height);
            

        }

        public void draw()
        {
            SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);            
            spriteBatch.Begin();
          //  LineRenderer.DrawLine(spriteBatch, txPixel, new Vector2(x, y), new Vector2(600, 600), Color.Black, 1f, 1f);
            spriteBatch.Draw(txPixel,rec,Color.Black);
            spriteBatch.Draw(txPixel,new Rectangle(x+(width/100*5),(y+(height/100*70)),width/100*40,height/100*7),Color.Red);
            spriteBatch.Draw(txPixel, new Rectangle(x + (width / 100 * 55), (y + (height / 100 * 70)), width / 100 * 40, height / 100 * 7), Color.Red);
            spriteBatch.DrawString(GameplayScreen.gameFont, "Send new", new Vector2(x + (width / 100 * 8), (y + (height / 100 * 70))), Color.White);
            spriteBatch.DrawString(GameplayScreen.gameFont, "Wave!", new Vector2(x + (width / 100 * 10), (y + (height / 100 * 73))), Color.White);
            spriteBatch.DrawString(GameplayScreen.gameFont, "Sell", new Vector2(x + (width / 100 * 60), (y + (height / 100 * 70))), Color.White);
            spriteBatch.DrawString(GameplayScreen.gameFont, "tower", new Vector2(x + (width / 100 * 60), (y + (height / 100 * 73))), Color.White);
            drawTowerPicture(spriteBatch);
            spriteBatch.End();
        }

        private void drawTowerPicture(SpriteBatch spriteBatch)
        {
            //TODO Tower VorschauBilder rendern
        }

        public override void clickaction()
        {
            Console.WriteLine("Wuhu das rechte Menu wurde angeklickt :)");
            Console.WriteLine("Zum testen pack ich das \"Wellen Senden\" erstmal hier rein...");
            waveManager.StartNextWave();
        }
    }
}