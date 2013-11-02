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
    class GameMenuRight
    {
        private int x;
        private int y;
        private int width;
        private int height;

        GameStateManagement.ScreenManager screenManager;
        GraphicsDevice graphicsDevice;


        public GameMenuRight(int x,int y,int width,int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public GameMenuRight()
        {
            this.x = 600;
            this.y = 0;
            this.width = 200;
            this.height = 600;


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
            GameplayScreen.gameFont.MeasureString("5");
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
    }
}