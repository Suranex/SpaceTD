using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using GameStateManagementSample.Logic;
using GameStateManagementSample.Utility;
using Microsoft.Xna.Framework.Media;

namespace GameStateManagementSample.Logic
{

    class GameMenuRight
    {
        #region Fields

        public static bool sound = true;

        ScreenManager screenManager;
        GraphicsDevice graphicsDevice;
        Texture2D txPixel;

        Rectangle backgroundrec;
        Texture2D background;

        Texture2D soundOn;
        Texture2D soundOff;

        Texture2D lifebarOn;
        Texture2D lifebarOff;

        int x;
        int y;
        int width;
        int height;

        //TODO irgendwo zentraler verwalten...
        bool buildmode = false; // wechsel zwischen update und bauen
        //neubau - Am anfang leer
        String name="";
        double price;
        double maxRange;
        double cooldown;         
        double damage;
        String description1;
        String description2;
        String description3;
        String description4;
        String description5;
        //Slow tower attribute:
        double slowTime;
        float factor;
        //upgrade
        Tower tower;
        double level=0;
        double sellReward=0;


        WaveManager waveManager;

        PictureButton btnToggleSound;
      //  PictureButton btnToggleSoundOff;

        PictureButton btnToggleLifebar;

        PictureButton btnTowerGreenOne;
        PictureButton btnTowerRedOne;
        PictureButton btnTowerBlueOne;
        PictureButton btnTowerPurpleOne;

        PictureButton btnUpgrade;
        PictureButton btnSell;

        GameplayScreen gameplayscreen;

        Button btnWave;
        #endregion

        public bool Buildmode
        {
            get { return buildmode; }
            set { buildmode = value; }
        }

        public bool DisplayTowerInfo
        {
            get { if (buildmode == true || gameplayscreen.selectedTower != null) return true; else return false; }
        }

        public double MaxRange
        {
            get { return maxRange; }
        }

        public GameMenuRight(int x, int y, int width, int height, WaveManager wm,GameplayScreen gameplayscreen)
        {
            this.gameplayscreen = gameplayscreen;
            waveManager = wm;
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;
            
            btnToggleSound=new PictureButton(new Vector2(x + (width / 100 * 80), y + (height / 100 * 5)),Color.Gray);
            btnToggleSound.Click += new EventHandler(btnToggleSound_Click);

            btnToggleLifebar = new PictureButton(new Vector2(x + (width / 100 * 80), y + (height / 100 * 11)), Color.Gray);
            btnToggleLifebar.Click += new EventHandler(btnLifebar_Click);

            // Send Wave Button
            btnWave = new Button(new Vector2(x + (width / 2), y + (height / 10 * 7)),
                "Send wave!", Color.White, Color.Red,Color.Gray,Color.Green);
            btnWave.Click += new EventHandler(btnWave_Click);
            btnWave.DrawExtra += new Button.DrawExtraHandler(btnWave_DrawExtra);

            btnTowerGreenOne = new PictureButton(new Vector2(x + (width / 100 * 20), y + (height / 100 * 20)),Color.Gray);
            btnTowerGreenOne.Click += new EventHandler(btnTowerGreenOne_Click);

            btnTowerRedOne = new PictureButton(new Vector2(x + (width / 100 * 40), y + (height / 100 * 20)),Color.Gray);
            btnTowerRedOne.Click += new EventHandler(btnTowerRedOne_Click);

            btnTowerBlueOne = new PictureButton(new Vector2(x + (width / 100 * 60), y + (height / 100 * 20)),Color.Gray);
            btnTowerBlueOne.Click += new EventHandler(btnTowerBlueOne_Click);

            btnTowerPurpleOne = new PictureButton(new Vector2(x + (width / 100 * 80), y + (height / 100 * 20)), Color.Gray);
            btnTowerPurpleOne.Click += new EventHandler(btnTowerPurpleOne_Click);

            btnUpgrade = new PictureButton(new Vector2(x + (width / 100 * 30), y + (height / 100 * 62)), Color.Gray);
            btnUpgrade.Click += new EventHandler(btnUpgrade_Click);

            btnSell = new PictureButton(new Vector2(x + (width / 100 * 70), y + (height / 100 * 62)), Color.Gray);
            btnSell.Click += new EventHandler(btnSell_Click);
        }

        #region btnUpgrade Handlers
        void btnUpgrade_Click(object sender, EventArgs e)
        {
            // damage um ein zentel steigern
            // Kosten immer um ein fünftel steigern
            // reichweite immer um 2 steigern
            tower.Upgrade();
            TowerSelected(tower);
        }
        #endregion
        

        #region btnSound Handlers
        void btnToggleSound_Click(object sender, EventArgs e)
        {
            if (GameMenuRight.sound == true)
            {
                GameMenuRight.sound = false;
                MediaPlayer.Pause();
                btnToggleSound.ChangeTexture(soundOff);
            }
            else
            {
                GameMenuRight.sound = true;
                MediaPlayer.Resume();
                btnToggleSound.ChangeTexture(soundOn);
            }
        }
        #endregion

        #region btnLifebar Handlers
        void btnLifebar_Click(object sender, EventArgs e)
        {
            if (OptionsMenuScreen.showHealthbars == true)
            {
                OptionsMenuScreen.showHealthbars = false;
                btnToggleLifebar.ChangeTexture(lifebarOff);
            }
            else
            {
                OptionsMenuScreen.showHealthbars = true;
                btnToggleLifebar.ChangeTexture(lifebarOn);
            }
            
        }
        #endregion

        #region btnSell Handlers
        void btnSell_Click(object sender, EventArgs e)
        {            
            if (tower.gameLevelTile.destroy() == true)
            {
                Tower.Towers.Remove(tower);
                Player.getInstance().rewardMoney((int)sellReward);
                gameplayscreen.selectedTower = null;
            }
        }
        #endregion

        #region btnTowerGreenOne Handlers
        void btnTowerGreenOne_Click(object sender, EventArgs e)
        {
            GameplayScreen.selectedTowerType = 0;
            buildmode = true;
            name = LaserTower.towerName;
            damage = LaserTower.startDamage;
            price = LaserTower.startcost;
            cooldown = LaserTower.startCooldown;
            maxRange = LaserTower.startMaxRange;
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
            GameplayScreen.selectedTowerType = 1;
            buildmode = true;
            name = CanonTower.towerName;
            damage = CanonTower.startDamage;
            price = CanonTower.startcost;
            cooldown = CanonTower.startCooldown;
            maxRange = CanonTower.startMaxRange;
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
            GameplayScreen.selectedTowerType = 2;
            buildmode = true;
            name = SlowTower.towerName;
            damage = SlowTower.startDamage;
            price = SlowTower.startcost;
            cooldown = SlowTower.startCooldown;
            maxRange = SlowTower.startMaxRange;
            description1 = "Dieser Turm verlangsamt";
            description2 = "Gegner eine gewisse Zeit";
            description3 = "um einen gewissen";
            description4 = "Prozentsatz.";
            description5 = "";
            slowTime = SlowTower.startSlowTime;
            factor = SlowTower.startSlowFactor;
        }
        #endregion

        #region btnTowerPurpleOne Handlers
        void btnTowerPurpleOne_Click(object sender, EventArgs e)
        {
            GameplayScreen.selectedTowerType = 3;
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
            btnTowerGreenOne.LoadContent(content, Tower.texturen[0]);
            btnTowerRedOne.LoadContent(content, Tower.texturen[1]);
            btnTowerBlueOne.LoadContent(content, Tower.texturen[2]);
            btnTowerPurpleOne.LoadContent(content, Tower.texturen[3]);

            btnUpgrade.LoadContent(content, content.Load<Texture2D>("UIshapes/blueButtonBG"));
            btnSell.LoadContent(content, content.Load<Texture2D>("UIshapes/blueButtonBG"));
            background = content.Load<Texture2D>("UIshapes/rightbackground");
            backgroundrec = new Rectangle(x, y, background.Width, background.Height);

            soundOn=content.Load<Texture2D>("audio-volume-high-2");
            soundOff = content.Load<Texture2D>("audio-volume-muted-2");

            lifebarOn = content.Load<Texture2D>("heart");
            lifebarOff = content.Load<Texture2D>("blood");
            if (GameMenuRight.sound == true)
            {
                btnToggleSound.LoadContent(content, soundOn);
            }
            else
            {
                btnToggleSound.LoadContent(content, soundOff);
            }

            if (OptionsMenuScreen.showHealthbars == true)
            {
                btnToggleLifebar.LoadContent(content, lifebarOn);
            }
            else
            {
                btnToggleLifebar.LoadContent(content, lifebarOff);
            }
            
            
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(txPixel, backgroundrec, Color.Black); // Schwarzes Feld rechts
            spriteBatch.Draw(background, backgroundrec, Color.White);
            //spriteBatch.DrawLine(txPixel, new Vector2(600, 0), new Vector2(600, 600), Color.Black,5.0f);

            btnToggleSound.Draw(spriteBatch);
            btnToggleLifebar.Draw(spriteBatch);

            spriteBatch.DrawString(GameplayScreen.gameFont, "Welle: "+waveManager.CurrentWave.RoundNumber, new Vector2(x + (width / 100 * 5), y + (height / 100 * 2)), Color.Silver);
            spriteBatch.DrawString(GameplayScreen.gameFont, "Geld: " + Player.getInstance().Money, new Vector2(x + (width / 100 * 5), y + (height / 100 * 4)), Color.Gold);
            spriteBatch.DrawString(GameplayScreen.gameFont, "Leben: "+Player.getInstance().Live, new Vector2(x + (width / 100 * 5), y + (height / 100 * 6)), Color.Red);
            spriteBatch.DrawString(GameplayScreen.gameFont, "Punkte: " +Player.getInstance().Points, new Vector2(x + (width / 100 * 5), y + (height / 100 * 8)), Color.Green);
            spriteBatch.DrawString(GameplayScreen.gameFont, "Kills: "+Player.getInstance().Kills, new Vector2(x + (width / 100 * 5), y + (height / 100 * 10)), Color.Blue);

            
            btnWave.Draw(spriteBatch); // Send Wave button
            btnTowerGreenOne.Draw(spriteBatch);
            btnTowerRedOne.Draw(spriteBatch);
            btnTowerBlueOne.Draw(spriteBatch);
         //   btnTowerPurpleOne.Draw(spriteBatch);
            if (DisplayTowerInfo)
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
                    if (GameplayScreen.selectedTowerType == 2)
                    {
                        spriteBatch.DrawString(GameplayScreen.gameFont, "Extra:", new Vector2(x + (width / 100 * 5), y + (height / 100 * 52)), Color.White);
                        spriteBatch.DrawString(GameplayScreen.gameFont, "Verlangsamungsdauer: " + slowTime, new Vector2(x + (width / 100 * 5), y + (height / 100 * 54)), Color.White);
                        spriteBatch.DrawString(GameplayScreen.gameFont, "Verlangsamung: " + factor * 100 + "%", new Vector2(x + (width / 100 * 5), y + (height / 100 * 56)), Color.White);
                    }
                }
                else
                {
                    spriteBatch.DrawString(GameplayScreen.gameFont, name, new Vector2(x + (width / 100 * 5), y + (height / 100 * 32)), Color.White);
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Schaden: " + Math.Round(damage, 2), new Vector2(x + (width / 100 * 5), y + (height / 100 * 34)), Color.White);
                    if (btnUpgrade.isHoverOrPressed())
                    {
                        spriteBatch.DrawString(GameplayScreen.gameFont,"+"+Math.Round((damage*Tower.upgradeDamageFactor)-damage,2), new Vector2(x + (width / 100 * 75), y + (height / 100 * 34)), Color.Green);
                    }
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Reichweite: " + maxRange, new Vector2(x + (width / 100 * 5), y + (height / 100 * 36)), Color.White);
                    if (btnUpgrade.isHoverOrPressed())
                    {
                        spriteBatch.DrawString(GameplayScreen.gameFont, "+" + ((int)(maxRange*Tower.upgradeRangeFactor)-maxRange), new Vector2(x + (width / 100 * 75), y + (height / 100 * 36)), Color.Green);
                    }
                    spriteBatch.DrawString(GameplayScreen.gameFont, "Abklingzeit: " + cooldown, new Vector2(x + (width / 100 * 5), y + (height / 100 * 38)), Color.White);

                    if (btnUpgrade.isHoverOrPressed()==true)
                    {
                        spriteBatch.DrawString(GameplayScreen.gameFont, "Upgradekosten: " + Math.Round((price*Tower.upgradeCostFactor)-price,2), new Vector2(x + (width / 100 * 5), y + (height / 100 * 40)), Color.Red);
                    }
                    else
                    {
                        spriteBatch.DrawString(GameplayScreen.gameFont, "Upgradekosten: " + Math.Round((price * Tower.upgradeCostFactor) - price, 2), new Vector2(x + (width / 100 * 5), y + (height / 100 * 40)), Color.White);
                    }
                    if (btnSell.isHoverOrPressed()==true)
                    {
                        spriteBatch.DrawString(GameplayScreen.gameFont, "Verkaufserloes: " + sellReward, new Vector2(x + (width / 100 * 5), y + (height / 100 * 42)), Color.Green);
                    }
                    else
                    {
                        spriteBatch.DrawString(GameplayScreen.gameFont, "Verkaufserloes: " + sellReward, new Vector2(x + (width / 100 * 5), y + (height / 100 * 42)), Color.White);
                    }
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
           // btnTowerPurpleOne.Update();
            btnUpgrade.Update();
            btnSell.Update();
            btnToggleSound.Update();
            btnToggleLifebar.Update();

            // Enable/Disable von Towerbuttons je nach Geldstand
            btnTowerGreenOne.Enabled = Player.getInstance().Money >= LaserTower.startcost;
            btnTowerRedOne.Enabled = Player.getInstance().Money >= CanonTower.startcost;
            btnTowerBlueOne.Enabled = Player.getInstance().Money >= SlowTower.startcost;
        }

        public void TowerSelected(Tower t)
        {
            buildmode = false;
            this.tower = t;
            name = t.name;
            damage = t.damage;
            price = t.Cost;
            cooldown = t.cooldown;
            maxRange = t.maxRange;
            level = t.towerlevel;
            sellReward = t.Cost/2;
        }
    }
}