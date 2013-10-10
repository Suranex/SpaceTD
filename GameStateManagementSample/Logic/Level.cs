using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class Level
    {
        //private GameLevelTile[,] gridMap;
        int[,] gridMap = new int[,] {
            {0,0,1,0,0,0,0,0,},
            {0,0,1,1,1,1,0,0,},
            {0,0,0,0,0,1,0,0,},
            {0,0,0,1,1,1,0,0,},
            {0,0,0,1,0,0,0,0,},
            {0,0,1,1,0,0,0,0,},
            {0,0,1,0,0,0,0,0,},
            {0,0,1,0,0,0,0,0,},
        };

        private Queue<Vector2> waypoints = new Queue<Vector2>();

        private int tileWidth = 32;
        private int tileHeight = 32;

        public Level()
        {

        }

        public int Width
        {
            get { return gridMap.GetLength(1); }
        }

        public int Height
        {
            get { return gridMap.GetLength(0); }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            // TODO: Das alles hier Fehlerfrei hinkriegen. Und am besten mit Vertices realisieren, damit auch diagonale Linien möglich sind.
            Texture2D txPixel = new Texture2D(graphicsDevice, 1, 1);
            txPixel.SetData<Color>(new []{ Color.White });

            
            // Rand zeichnen
            // Top
            spriteBatch.Draw(txPixel, new Rectangle(0, 0, Width*tileWidth, 1), Color.White);
            // Left
            spriteBatch.Draw(txPixel, new Rectangle(0, 0, 1, Height*tileHeight), Color.White);
            // Right
            spriteBatch.Draw(txPixel, new Rectangle(Width*tileWidth, 0, 1, Height*tileHeight), Color.White);
            // Bottom
            spriteBatch.Draw(txPixel, new Rectangle(0, Height*tileHeight, Width*tileWidth, 1), Color.White);

            // Zwischenlinien zeichnen
            for (int x = 0; x < Width-1; x++)
            {
                for (int y = 0; y < Height-1; y++)
                {
                    // Momentanes Feld mit Feld rechts vergleichen
                    if (gridMap[y, x] != gridMap[y, x + 1])
                        spriteBatch.Draw(txPixel, new Rectangle((x+1) * tileWidth, y * tileWidth, 2, tileHeight), Color.Red);
                    else
                        spriteBatch.Draw(txPixel, new Rectangle((x+1) * tileWidth, y * tileWidth, 1, tileHeight), Color.LightCyan);

                    // Momentanes Feld mit Feld darunter vergleichen
                    if(gridMap[y,x] != gridMap[y+1,x])
                        spriteBatch.Draw(txPixel, new Rectangle(x * tileWidth, (y+1) * tileWidth, tileWidth, 2), Color.Red);
                    else
                        spriteBatch.Draw(txPixel, new Rectangle(x * tileWidth, (y+1) * tileWidth, tileWidth, 1), Color.LightCyan);
                }
            }
        }
    }
}
