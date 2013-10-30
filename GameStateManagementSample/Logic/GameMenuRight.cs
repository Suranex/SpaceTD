using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagementSample.Utility;

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
            spriteBatch.Draw(txPixel,new Rectangle(x+(width/100*10),(y+(height/100*10)),width/100*40,height/100*7),Color.Red);
            drawTowerPicture(spriteBatch);
            spriteBatch.End();
        }

        private void drawTowerPicture(SpriteBatch spriteBatch)
        {
            //TODO Tower VorschauBilder rendern
        }
    }
}