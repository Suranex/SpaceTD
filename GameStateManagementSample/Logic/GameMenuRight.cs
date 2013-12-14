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

        int x;
        int y;
        int width;
        int height;

        //TODO irgendwo zentraler verwalten...
        bool buildmode = true; // wechsel zwischen update und bauen
        //neubau
        String name="Kanonenturm";
        double price=42;
        double maxRange=100;
        double cooldown=0.75;         
        double damage=15;
        String description1 = "Ein Single-target-Turm";
        String description2 = "welcher grossen";
        String description3 = "Schaden verursacht.";
        String description4 = "";
        String description5 = "";
        //Slow tower attribute:
        double slowTime = 0;
        float factor = 0;
        //upgrade
        Tower tower;
        double level=0;
        double sellReward=0;


        WaveManager waveManager;

        PictureButton btnTowerGreenOne;
        PictureButton btnTowerRedOne;
        PictureButton btnTowerBlueOne;
        PictureButton btnTowerPurpleOne;

        PictureButton btnUpgrade;
        PictureButton btnSell;

        Button btnWave;
        #endregion

        public GameMenuRight(int x, int y, int width, int height, WaveManager wm)
        {
            waveManager = wm;
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
            rec = new Rectangle(x, y, width, height);

            // Send Wave Button
            btnWave = new Button(new Vector2(x + (width / 2), y + (height / 10 * 7)),
                "Send wave!", Color.White, Color.Red,Color.Gray,Color.Green);
            btnWave.Click += new EventHandler(btnWave_Click);
            btnWave.DrawExtra += new Button.DrawExtraHandler(btnWave_DrawExtra);

            btnTowerGreenOne = new PictureButton(new Vector2(x + (width /100 *20 ), y + (height / 100 * 20)));
            btnTowerGreenOne.Click += new EventHandler(btnTowerGreenOne_Click);

            btnTowerRedOne = new PictureButton(new Vector2(x + (width / 100 * 40), y + (height / 100 * 20)));
            btnTowerRedOne.Click += new EventHandler(btnTowerRedOne_Click);

            btnTowerBlueOne = new PictureButton(new Vector2(x + (width / 100 * 60), y + (height / 100 * 20)));
            btnTowerBlueOne.Click += new EventHandler(btnTowerBlueOne_Click);

            btnTowerPurpleOne = new PictureButton(new Vector2(x + (width / 100 * 80), y + (height / 100 * 20)));
            btnTowerPurpleOne.Click += new EventHandler(btnTowerPurpleOne_Click);

            btnUpgrade = new PictureButton(new Vector2(x + (width / 100 * 30), y + (height / 100 * 62)));
            btnUpgrade.Click += new EventHandler(btnUpgrade_Click);

            btnSell = new PictureButton(new Vector2(x + (width / 100 * 70), y + (height / 100 * 62)));
            btnSell.Click += new EventHandler(btnSell_Click);
        }

        #region btnUpgrade Handlers
        void btnUpgrade_Click(object sender, EventArgs e)
        {
            tower.Upgrade();
            TowerSelected(tower);
        }
        #endregion

        #region btnSell Handlers
        void btnSell_Click(object sender, EventArgs e)
        {
            //TODO
        }
        #endregion

        #region btnTowerGreenOne Handlers
        void btnTowerGreenOne_Click(object sender, EventArgs e)
        {
            GameplayScreen.selectetTowerType = 1;
            buildmode = true;
            name = "Laserturm";
            damage = 4;
            price = 42;
            cooldown = 0.25;
            maxRange = 200;
            description1 = "Der Klassiker, ein Turm";
            description2 = "welcher ein Ziel";
            description3 = "angreift.";
            description4 = "Pew Pew Pew";
            description5 = "";
        }
        #endregion

        #region btnTowerRedOne Handlers
        void btnTowerRedOne_Click(object sender, EventArgs e)
        {
            GameplayScreen.selectetTowerType = 0;
            buildmode = true;
            name = "Kanonenturm";
            damage = 15;
            price = 42;
            cooldown = 0.75;
            maxRange = 100;
            description1 = "Ein Single-target-Turm";
            description2 = "welcher grossen";
            description3 = "Schaden verursacht.";
            description4 = "";
            description5 = "";
        }
        #endregion

        #region btnTowerBlueOne Handlers
        void btnTowerBlueOne_Click(object sender, EventArgs e)
        {
            GameplayScreen.selectetTowerType = 2;
            buildmode = true;
            name = "Verlangsamungsturm";
            damage = 3;
            price = 42;
            cooldown = 2;
            maxRange = 75;
            description1 = "Dieser Turm verlangsamt";
            description2 = "Gegner eine gewisse Zeit";
            description3 = "um einen gewissen";
            description4 = "Prozentsatz.";
            description5 = "";
            slowTime = 4;
            factor = 0.5f;
        }
        #endregion

        #region btnTowerPurpleOne Handlers
        void btnTowerPurpleOne_Click(object sender, EventArgs e)
        {
            GameplayScreen.selectetTowerType = 3;
            buildmode = true;
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

            btnUpgrade.LoadContent(content, content.Load<Texture2D>("UIshapes/shape 442"));
            btnSell.LoadContent(content, content.Load<Texture2D>("UIshapes/shape 442"));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(txPixel, rec, Color.Black); // Schwarzes Feld rechts
            btnWave.Draw(spriteBatch); // Send Wave button
            btnTowerGreenOne.Draw(spriteBatch);
            btnTowerRedOne.Draw(spriteBatch);
            btnTowerBlueOne.Draw(spriteBatch);
            btnTowerPurpleOne.Draw(spriteBatch);
            if (!name.Equals(""))
            {
                if (buildmode)
                {
                    spriteBatch.DrawString(GameplayScreen.gameFont, name, new Vector2(x + (width / 100 * 5), y + (height / 100 * 32)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Preis: " + price, new Vector2(x + (width / 100 * 5), y + (height / 100 * 34)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Schaden: " + damage, new Vector2(x + (width / 100 * 5), y + (height / 100 * 36)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Reichweite: " + maxRange, new Vector2(x + (width / 100 * 5), y + (height / 100 * 38)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Abklingzeit: " + cooldown, new Vector2(x + (width / 100 * 5), y + (height / 100 * 40)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, description1, new Vector2(x + (width / 100 * 5), y + (height / 100 * 42)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, description2, new Vector2(x + (width / 100 * 5), y + (height / 100 * 44)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, description3, new Vector2(x + (width / 100 * 5), y + (height / 100 * 46)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, description4, new Vector2(x + (width / 100 * 5), y + (height / 100 * 48)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, description5, new Vector2(x + (width / 100 * 5), y + (height / 100 * 50)), Color.White);
                    if (GameplayScreen.selectetTowerType == 2)
                    {
                        spriteBatch.DrawString(GameplayScreen.gameFont, "Extra:", new Vector2(x + (width / 100 * 5), y + (height / 100 * 52)), Color.White);
                        spriteBatch.DrawString(GameplayScreen.gameFont, "Verlangsamungsdauer: " + slowTime, new Vector2(x + (width / 100 * 5), y + (height / 100 * 54)), Color.White);
                        spriteBatch.DrawString(GameplayScreen.gameFont, "Verlangsamung: " + factor * 100 + "%", new Vector2(x + (width / 100 * 5), y + (height / 100 * 56)), Color.White);
                    }
                }
                else
                {
                    spriteBatch.DrawString(GameplayScreen.gameFont, name, new Vector2(x + (width / 100 * 5), y + (height / 100 * 32)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Schaden: " + damage, new Vector2(x + (width / 100 * 5), y + (height / 100 * 34)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Reichweite: " + maxRange, new Vector2(x + (width / 100 * 5), y + (height / 100 * 36)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Abklingzeit: " + cooldown, new Vector2(x + (width / 100 * 5), y + (height / 100 * 38)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Upgradekosten: " + price, new Vector2(x + (width / 100 * 5), y + (height / 100 * 40)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Verkaufserloes: " + sellReward, new Vector2(x + (width / 100 * 5), y + (height / 100 * 42)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Turmlevel: : " + level, new Vector2(x + (width / 100 * 5), y + (height / 100 * 44)), Color.White);

                    if (tower.type == 2)
                    {
                        spriteBatch.DrawString(GameplayScreen.gameFont, "Extra:", new Vector2(x + (width / 100 * 5), y + (height / 100 * 46)), Color.White);
                        spriteBatch.DrawString(GameplayScreen.gameFont, "Verlangsamungsdauer: " + slowTime, new Vector2(x + (width / 100 * 5), y + (height / 100 * 48)), Color.White);
                        spriteBatch.DrawString(GameplayScreen.gameFont, "Verlangsamung: " + factor * 100 + "%", new Vector2(x + (width / 100 * 5), y + (height / 100 * 50)), Color.White);
                    }
                    btnUpgrade.Draw(spriteBatch);
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Upgrade!", new Vector2(x + (width / 100 * 15), y + (height / 100 * 61)), Color.White);
                    btnSell.Draw(spriteBatch);
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Sell!", new Vector2(x + (width / 100 * 65), y + (height / 100 * 61)), Color.White);
                }

            }

        }

        public void Update()
        {
            btnWave.Update();
            btnTowerGreenOne.Update();
            btnTowerRedOne.Update();
            btnTowerBlueOne.Update();
            btnTowerPurpleOne.Update();
            btnUpgrade.Update();
            btnSell.Update();
        }

        public void TowerSelected(Tower t)
        {
            buildmode = false;
            this.tower = t;
            name = t.name;
            damage = t.damage;
            price = 42;
            cooldown = t.cooldown;
            maxRange = t.maxRange;
            level = t.towerlevel;
            sellReward = 21;
        }
    }
}