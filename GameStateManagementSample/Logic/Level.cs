using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagementSample.Utility;


namespace GameStateManagementSample.Logic
{
    class Level
    {
        //private GameLevelTile[,] gridMap;
        int[,] initMap = new int[,] {
            {0,0,1,0,0,0,0,0,0,0,},
            {0,0,1,1,1,1,0,0,0,0,},
            {0,0,0,0,0,1,1,1,1,0,},
            {0,1,1,1,0,0,0,0,1,0,},
            {0,1,0,1,1,1,1,1,1,0,},
            {0,1,0,0,0,0,0,0,0,0,},
            {0,1,1,1,1,0,0,0,0,0,},
            {0,0,0,0,1,0,0,0,0,0,},
            {0,0,0,0,1,0,0,0,0,0,},
            {0,0,0,0,1,0,0,0,0,0,},
        };

        GameLevelTile[,] gridMap;

        private Queue<Vector2> waypoints = new Queue<Vector2>();

        private int tileWidth = 32;
        private int tileHeight = 32;

        private int startx = 50;
        private int starty = 50;

        GameStateManagement.ScreenManager screenManager;
        GraphicsDevice graphicsDevice;
        RenderTarget2D renderTarget;
        Texture2D txPixel;

        public Level()
        {
            gridMap = new GameLevelTile[initMap.GetLength(1), initMap.GetLength(0)];
            for (int i = 0; i<Width; i++)
                for (int j = 0; j < Height; j++)
                    gridMap[j,i] = new GameLevelTile(initMap[j,i] == 0, i, j);
        }

        public void Initialize(GameStateManagement.ScreenManager sm)
        {
            screenManager = sm;
            graphicsDevice = screenManager.GraphicsDevice;
            renderTarget = new RenderTarget2D(
                graphicsDevice,
                graphicsDevice.PresentationParameters.BackBufferWidth,
                graphicsDevice.PresentationParameters.BackBufferHeight
            );

            txPixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            txPixel.SetData<Color>(new Color[] { Color.White });
        }

        public int Width
        {
            get { return gridMap.GetLength(1); }
        }

        public int Height
        {
            get { return gridMap.GetLength(0); }
        }

        public void DrawRenderTarget()
        {
            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(ClearOptions.Target, new Color(0, 82, 82), 0, 0);

            SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
            Matrix Transform = Matrix.CreateTranslation(2, 2, 0);

            spriteBatch.Begin(
                SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend,
                SamplerState.LinearClamp,
                DepthStencilState.Default,
                null,
                null,
                Transform
            );

            for (int y = 0; y <= Height; y++)
            {
                for (int x = 0; x <= Width; x++)
                {
                    // Nach rechts
                    if (x < Width) // Nur wenn wir noch nicht am Rand sind, ergo ein x+1 auch existiert
                    {
                        // Bei einer Linie nach rechts, muss das momentane Feld mit dem Feld dadrüber verglichen werden. Problematisch in zwei Fällen:
                        // oberes Ende und unteres Ende.

                        // Sind wir am oberen Ende, also y == 0, so brauch man kein Vergleich; Es ist eine Außenwand.
                        // Sind wir am unteren Ende, also y == Height, so brauch man kein Vergleich; Es ist eine Außenwand.
                        // Falls keins der beiden Fälle zutrifft: [y,x] mit [y-1,x] vergleichen
                        if (y == 0)
                        {
                            spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2((x + 1) * tileWidth, y * tileWidth), Color.White, 3f, 1f);
                        }
                        else if (y == Height)
                        {
                            spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2((x + 1) * tileWidth, y * tileWidth), Color.White, 3f, 1f);
                        }
                        else
                        {
                            // Wand/Wand
                            if (gridMap[y, x].Buildfield && gridMap[y - 1, x].Buildfield)
                            {
                                spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2((x + 1) * tileWidth, y * tileWidth), Color.White, 1f);
                            }
                            // Wand/Pfad
                            else if (gridMap[y, x].Buildfield != gridMap[y - 1, x].Buildfield)
                            {
                                spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2((x + 1) * tileWidth, y * tileWidth), Color.Red, 3f, 0.5f);
                            }
                            // Pfad/Pfad
                            else if (gridMap[y, x].Buildfield && gridMap[y - 1, x].Buildfield)
                            {
                                // Keine Linie!
                            }
                        }
                    }


                    // Nach unten
                    if (y < Height) // Nur wenn wir noch nicht am Rand sind, ergo ein y+1 auch existiert
                    {
                        // Bei einer Linie nach unten, muss das momentane Feld mit dem Feld links daneben verglichen werden. Problematisch in zwei Fällen:
                        // linkes Ende und rechtes Ende.

                        // Sind wir am linken Ende, also x == 0, so brauch man kein Vergleich; Es ist eine Außenwand.
                        // Sind wir am rechten Ende, also x == Width, so brauch man kein Vergleich; Es ist eine Außenwand.
                        // Falls keins der beiden Fälle zutrifft: [y,x] mit [y,x-1] vergleichen
                        if (x == 0)
                        {
                            spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2(x * tileWidth, (y+1) * tileWidth), Color.White, 3f, 1f);
                        }
                        else if (x == Width)
                        {
                            spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2(x * tileWidth, (y+1) * tileWidth), Color.White, 3f, 1f);
                        }
                        else
                        {
                            // Wand/Wand
                            if (gridMap[y, x].Buildfield && gridMap[y, x - 1].Buildfield)
                            {
                                spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2(x * tileWidth, (y+1) * tileWidth), Color.White, 1f);
                            }
                            // Wand/Pfad
                            else if (gridMap[y, x].Buildfield != gridMap[y, x - 1].Buildfield)
                            {
                                spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2(x * tileWidth, (y+1) * tileWidth), Color.Red, 3f, 0.5f);
                            }
                            // Pfad/Pfad
                            //else if (!gridMap[y, x].Buildfield && !gridMap[y, x - 1].Buildfield)
                            //{
                            //    // Keine Linie!
                            //}
                        }
                    }
                }
            }

            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
        }

        public void Draw()
        {
            // renderTarget zeichnen
            SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
            spriteBatch.Begin();
            spriteBatch.Draw((Texture2D)renderTarget,
                new Vector2(startx, starty),
                new Rectangle(0, 0, 3+Width*tileWidth, 3+Height*tileHeight),
                Color.White
            );
            spriteBatch.End();
        }

        /// <summary>
        /// Get the Coodinates of the clicked position. Returns null if out of field.
        /// </summary>
        public int[] GetFieldCoordinates(int x, int y)
        {
            x = x - startx;
            y = y - starty;
            if (x < 0 || y < 0) // Check if click is out of level field (left or top)
                return null;
            int[] feld = new int[2];
            feld[0] = (int) x / tileWidth;
            feld[1] = (int) y / tileHeight;
            if (feld[0] >= 10 || feld[1] >= 10) // Check if cklick is out of level field (right or bottom)
                return null;
            return feld;
        }

        public void placerTower(int x, int y, Tower tower)
        {
            if (gridMap[y, x].build(tower))
                Console.Out.WriteLine("Gebaut");
            else
                Console.Out.WriteLine("Besetzt");
        }
    }
}
