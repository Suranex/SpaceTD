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
        /*int[,] initMap = new int[,] {
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
        };*/

    int[,] initMap = new int[,] {
    {0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0},
    {0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,0,0},
    {0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0},
    {0,0,1,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0},
    {0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,1,1,1,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0},
    {0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0},
    {0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0},
    {0,1,1,1,1,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0},
    {0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0},
    {0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,1,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0}
    };
        GameLevelTile[,] gridMap;

        internal GameLevelTile[,] GridMap
        {
            get { return gridMap; }
            set { gridMap = value; }
        }

        public Queue<Vector2> waypoints = new Queue<Vector2>();

        private Color normalLineColor = new Color(24, 66, 107);
        private Color pathLineColor = new Color(121, 208, 231);
        private float buildFieldAlpha = 0.2f; // Schwarz * Alpha
        private float pathFieldAlpha = 0.4f;

        GameStateManagement.ScreenManager screenManager;
        GraphicsDevice graphicsDevice;
        RenderTarget2D renderTarget;
        Texture2D txPixel;

        private static int tileWidth = 24;
        private static int tileHeight = 24;

        private Vector2 startOffset = new Vector2(10, 10);
        private Vector2 towerPosOffset = new Vector2(-2, -2);

        public Level()
        {
            // Erstmal hardcoden, kann man hinterher aus einer XML auslesen
            // Außerdem wird angenommen das tileWidth == tileHeight... ist eigentlich eh überflüssig dafür zwei Attribute
            // zu haben, wenn die doch eh immer den gleichen Wert haben.
            /*waypoints.Enqueue(new Vector2(2, 0) * tileWidth + Offset);
            waypoints.Enqueue(new Vector2(2, 1) * tileWidth + Offset);
            waypoints.Enqueue(new Vector2(5, 1) * tileWidth + Offset);
            waypoints.Enqueue(new Vector2(5, 2) * tileWidth + Offset);
            waypoints.Enqueue(new Vector2(8, 2) * tileWidth + Offset);
            waypoints.Enqueue(new Vector2(8, 4) * tileWidth + Offset);
            waypoints.Enqueue(new Vector2(3, 4) * tileWidth + Offset);
            waypoints.Enqueue(new Vector2(3, 3) * tileWidth + Offset);
            waypoints.Enqueue(new Vector2(1, 3) * tileWidth + Offset);
            waypoints.Enqueue(new Vector2(1, 6) * tileWidth + Offset);
            waypoints.Enqueue(new Vector2(4, 6) * tileWidth + Offset);
            waypoints.Enqueue(new Vector2(4, 9) * tileWidth + Offset);
             */
            waypoints.Enqueue(new Vector2(8, 0) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(8, 2) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(13, 2) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(13, 5) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(17, 5) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(17, 8) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(8, 8) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(8, 6) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(2, 6) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(2, 12) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(4, 12) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(4, 15) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(1, 15) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(1, 17) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(6, 17) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(6, 12) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(16, 12) * tileWidth + startOffset);
            waypoints.Enqueue(new Vector2(16, 19) * tileWidth + startOffset);



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

        public static int TileWidth
        {
            get { return tileWidth; }
        }

        public static int TileHeight
        {
            get { return tileHeight; }
        }


        public int Width
        {
            get { return gridMap.GetLength(1); }
        }

        public int Height
        {
            get { return gridMap.GetLength(0); }
        }

        public Vector2 Offset
        {
            get { return startOffset; }
        }

        public void DrawRenderTarget()
        {
            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(ClearOptions.Target, Color.Transparent, 0, 0);

            SpriteBatch spriteBatch = new SpriteBatch(graphicsDevice);
            Matrix Transform = Matrix.CreateTranslation(2, 2, 0);

            spriteBatch.Begin(
                SpriteSortMode.BackToFront, // 0f = Vorne, 1f = Hinten
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
                    // -------------------------- Felderbackground selbst
                    // Noch nen Check damit es kein ArrayOutOfBounds gibt. Bei Linien muss es <= Width/Height sein, bei Feldern < Width/Height
                    if (y != Height && x != Width)
                    {
                        if (gridMap[y, x].Buildfield)
                            spriteBatch.Draw(txPixel, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), null, Color.Black * buildFieldAlpha, 0f, Vector2.Zero, SpriteEffects.None, 1f);
                        else
                            spriteBatch.Draw(txPixel, new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), null, Color.Black * pathFieldAlpha, 0f, Vector2.Zero, SpriteEffects.None, 1f);
                    }



                    // -------------------------- Linien zwischen Feldern
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
                            spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2((x + 1) * tileWidth, y * tileWidth), normalLineColor, 1f, 0.5f);
                        }
                        else if (y == Height)
                        {
                            spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2((x + 1) * tileWidth, y * tileWidth), normalLineColor, 1f, 0.5f);
                        }
                        else
                        {
                            // Wand/Wand
                            if (gridMap[y, x].Buildfield && gridMap[y - 1, x].Buildfield)
                            {
                                spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2((x + 1) * tileWidth, y * tileWidth), normalLineColor, 1f, 0.5f);
                            }
                            // Wand/Pfad
                            else if (gridMap[y, x].Buildfield != gridMap[y - 1, x].Buildfield)
                            {
                                spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2((x + 1) * tileWidth, y * tileWidth), pathLineColor, 1f, 0f);
                            }
                            // Pfad/Pfad
                            //else if (gridMap[y, x].Buildfield && gridMap[y - 1, x].Buildfield)
                            //{
                                // Keine Linie!
                            //}
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
                            spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2(x * tileWidth, (y + 1) * tileWidth), normalLineColor, 1f, 0.5f);
                        }
                        else if (x == Width)
                        {
                            spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2(x * tileWidth, (y + 1) * tileWidth), normalLineColor, 1f, 0.5f);
                        }
                        else
                        {
                            // Wand/Wand
                            if (gridMap[y, x].Buildfield && gridMap[y, x - 1].Buildfield)
                            {
                                spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2(x * tileWidth, (y + 1) * tileWidth), normalLineColor, 1f, 0.5f);
                            }
                            // Wand/Pfad
                            else if (gridMap[y, x].Buildfield != gridMap[y, x - 1].Buildfield)
                            {
                                spriteBatch.DrawLine(txPixel, new Vector2(x * tileWidth, y * tileWidth), new Vector2(x * tileWidth, (y + 1) * tileWidth), pathLineColor, 1f, 0f);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            // renderTarget zeichnen
            spriteBatch.Draw((Texture2D)renderTarget,
                startOffset,
                new Rectangle(0, 0, 3+Width*tileWidth, 3+Height*tileHeight),
                Color.White
            );
        }

        /// <summary>
        /// Get the Coodinates of the clicked position. Returns null if out of field.
        /// </summary>
        public int[] GetFieldCoordinates(int x, int y)
        {
            x -= (int)startOffset.X;
            y -= (int)startOffset.Y;
            if (x < 0 || y < 0) // Check if click is out of level field (left or top)
                return null;
            int[] feld = new int[2];
            feld[0] = (int) x / tileWidth;
            feld[1] = (int) y / tileHeight;
            if (feld[0] >= Width || feld[1] >= Height) // Check if cklick is out of level field (right or bottom)
                return null;
            return feld;
        }

        /// <summary>
        /// Erwartet array Koordinaten, also Ausgabe von GetFieldCoordinates
        /// </summary>
        public bool buildable(int x, int y)
        {
            return gridMap[y, x].Buildfield && gridMap[y, x].Tower == null;
        }

        public bool placeTower(int x, int y, int type)
        {
            return gridMap[y, x].build(GetTowerPos(x, y), type);
        }

        public Tower getTowerAtPosition(int x, int y)
        {
            return gridMap[y, x].Tower;
        }
        
        public Vector2 GetTowerPos(int x, int y)
        {
            Vector2 v;
            v.X = x * tileWidth + startOffset.X + towerPosOffset.X;
            v.Y = y * tileHeight + startOffset.Y + towerPosOffset.Y;
            return v;
        }
    }
}
