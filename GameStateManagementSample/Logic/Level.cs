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
        int[,] gridMap = new int[,] {
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

        private Queue<Vector2> waypoints = new Queue<Vector2>();

        private int tileWidth = 32;
        private int tileHeight = 32;

        GameStateManagement.ScreenManager screenManager;
        GraphicsDevice graphicsDevice;
        RenderTarget2D renderTarget;
        Texture2D txPixel;

        public Level()
        {

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
                            if (gridMap[y, x] == 0 && gridMap[y - 1, x] == 0)
                            {
                                spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2((x + 1) * tileWidth, y * tileWidth), Color.White, 1f);
                            }
                            // Wand/Pfad
                            else if (gridMap[y, x] != gridMap[y - 1, x])
                            {
                                spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2((x + 1) * tileWidth, y * tileWidth), Color.Red, 3f, 0.5f);
                            }
                            // Pfad/Pfad
                            else if (gridMap[y, x] == 1 && gridMap[y - 1, x] == 1)
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
                            if (gridMap[y, x] == 0 && gridMap[y, x - 1] == 0)
                            {
                                spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2(x * tileWidth, (y+1) * tileWidth), Color.White, 1f);
                            }
                            // Wand/Pfad
                            else if (gridMap[y, x] != gridMap[y, x - 1])
                            {
                                spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2(x * tileWidth, (y+1) * tileWidth), Color.Red, 3f, 0.5f);
                            }
                            // Pfad/Pfad
                            else if (gridMap[y, x] == 1 && gridMap[y, x - 1] == 1)
                            {
                                // Keine Linie!
                            }
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
                new Vector2(10, 10),
                new Rectangle(0, 0, 3+Width*tileWidth, 3+Height*tileHeight),
                Color.White
            );
            spriteBatch.End();
        }
    }
}
