#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using GameStateManagementSample.Logic;
using GameStateManagementSample.Utility;
#endregion

namespace GameStateManagementSample
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields
        Level level;

        private static GameplayScreen gameplayscreen;
        GameMenuRight gmr;
        //Enemy enemy1;
        //Wave wave;
        WaveManager waveManager;
        bool gameOver = false;

        public static ContentManager content;
        public static SpriteFont gameFont;

        Vector2 playerPosition = new Vector2(100, 100);
        Vector2 enemyPosition = new Vector2(100, 100);

        public static Texture2D testtex;  // gegener bild
        public static Texture2D testtex2; // roter kasten

        Random random = new Random();

        float pauseAlpha;
        //float clickacceptpausetime;
        MouseState lastMouseState;
        MouseState currentMouseState;

        public Tower selectedTower = null;
        public static int selectedTowerType = 0;
        Sprite towerPreviewSprite;

        InputAction pauseAction;

        Texture2D background;
        Rectangle backgroundrec;

        public static GraphicsDevice gd;

        public Level Level
        {
            get { return level; }
            set { level = value; }
        }

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            gameplayscreen = this;
            level = new Logic.Level();
            waveManager = new WaveManager(level, 20);
            gmr = new Logic.GameMenuRight(600,0,200,600, waveManager,this);
            backgroundrec = new Rectangle(0, 0, 600, 600);

            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                true);

            
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");
                testtex = content.Load<Texture2D>("enemy");
                testtex2 = content.Load<Texture2D>("rot");
                gameFont = content.Load<SpriteFont>("gamefont");
                background=content.Load<Texture2D>("background");

                Tower.LoadContent(content);
                Weapon.LoadContent(content);

                waveManager.LoadContent(content);
                waveManager.InitWaves();

                //clickacceptpausetime = 1;
                level.Initialize(ScreenManager);
                gmr.Initialize(ScreenManager, content);
                //gmr.Activelayer = true;

                gd = ScreenManager.GraphicsDevice;

                // A real game would probably have more content than this sample, so
                // it would take longer to load. We simulate that by delaying for a
                // while, giving you a chance to admire the beautiful loading screen.
                //Thread.Sleep(1000);

                // once the load has finished, we use ResetElapsedTime to tell the game's
                // timing mechanism that we have just finished a very long frame, and that
                // it should not try to catch up.
                ScreenManager.Game.ResetElapsedTime();
            }
        }


        public override void Deactivate()
        {
            base.Deactivate();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void Unload()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);


            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            // Tower auswählen/bauen
            currentMouseState = Mouse.GetState();
            if (currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton != ButtonState.Pressed)
            {
                bool platziert = false;
                int[] pos = level.GetFieldCoordinates(currentMouseState.X, currentMouseState.Y);
                if (pos != null)
                {
                    if(gmr.Buildmode == true)
                        platziert = level.placeTower(pos[0], pos[1], selectedTowerType);
                    selectedTower = level.getTowerAtPosition(pos[0], pos[1]);

                    if (selectedTower == null)
                    {
                        Console.WriteLine("Kein Tower selectet");
                    }
                    else
                    {
                        Console.WriteLine("Tower selectiert");
                        gmr.TowerSelected(selectedTower);
                    }
                }
            }

            // Tower deselect
            if (currentMouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton != ButtonState.Pressed)
            {
                selectedTower = null;
            }
            lastMouseState = currentMouseState;

            // Vorschau für neu zu bauende Tower, Erstellung des Sprites
            if (gmr.Buildmode == true)
            {
                int[] pos = level.GetFieldCoordinates(currentMouseState.X, currentMouseState.Y);
                if (pos != null)
                {
                    towerPreviewSprite = new Sprite(Tower.texturen[selectedTowerType], level.GetTowerPos(pos[0], pos[1]));
                }
            }

            if (IsActive)
            {
                // TODO: this game isn't very fun! You could probably improve
                // it by inserting something more interesting in this space :-)

                waveManager.Update(gameTime);
                gmr.Update();
                WeaponManager.UpdateAll(gameTime);
                foreach(Tower t in Tower.Towers) // tower update; Verwaltungsklasse fehlt!
                    t.Update(gameTime);
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (pauseAction.Evaluate(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }else if(gameOver==true)
            {                
                ScreenManager.AddScreen(new GameOverScreen(), ControllingPlayer);

            }
            else
            {
                // Otherwise move the player position.
                Vector2 movement = Vector2.Zero;
                if (keyboardState.IsKeyDown(Keys.D1))
                    selectedTowerType = 0;
                if (keyboardState.IsKeyDown(Keys.D2))
                    selectedTowerType = 1;
                if (keyboardState.IsKeyDown(Keys.K))
                    if (selectedTower != null)
                        selectedTower.Upgrade();



                if (keyboardState.IsKeyDown(Keys.Left))
                    movement.X--;

                if (keyboardState.IsKeyDown(Keys.Right))
                    movement.X++;

                if (keyboardState.IsKeyDown(Keys.Up))
                    movement.Y--;

                if (keyboardState.IsKeyDown(Keys.Down))
                    movement.Y++;

                Vector2 thumbstick = gamePadState.ThumbSticks.Left;

                movement.X += thumbstick.X;
                movement.Y -= thumbstick.Y;

                if (input.TouchState.Count > 0)
                {
                    Vector2 touchPosition = input.TouchState[0].Position;
                    Vector2 direction = touchPosition - playerPosition;
                    direction.Normalize();
                    movement += direction;
                }

                if (movement.Length() > 1)
                    movement.Normalize();

                playerPosition += movement * 8f;
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // -----------------------
            // http://stackoverflow.com/questions/3270507/changing-rendertarget-results-in-purple-screen
            // Laut einer Antwort, muss man aufs RenderTarget schreiben, bevor man clear benutzt.
            level.DrawRenderTarget();
            // -----------------------

            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Blue, 0, 0);
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;


            spriteBatch.Begin();
            spriteBatch.Draw(background, backgroundrec, Color.White);
            level.Draw(spriteBatch);
            gmr.Draw(spriteBatch); // Menü Rechts
            foreach (Tower t in Tower.Towers) // Tower, sollte vll noch in eine Verwaltungsklasse
                t.Draw(spriteBatch);
            waveManager.Draw(spriteBatch); // Gegner
            WeaponManager.DrawAll(spriteBatch);

            // Rangeindikator für selectedTower anzeigen
            if (selectedTower != null)
                spriteBatch.DrawCircle(selectedTower.OriginPosition, selectedTower.maxRange, Color.White);

            // Vorschau des neu zu erstellenden Towers - Rendering des preview Sprites
            if (gmr.Buildmode == true)
            {
                int[] pos = level.GetFieldCoordinates(currentMouseState.X, currentMouseState.Y);
                if (pos != null && level.buildable(pos[0], pos[1]))
                {
                    towerPreviewSprite.Draw(spriteBatch, Color.White * 0.5f);
                    spriteBatch.DrawCircle(towerPreviewSprite.Center, (int)gmr.MaxRange, Color.White * 0.5f);
                }
            }
            
            
            spriteBatch.End();

            

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }


        #endregion

        public static GameplayScreen getInstance()
        {
            if (gameplayscreen == null)
                gameplayscreen = new GameplayScreen();
            return gameplayscreen;
        }

        public void GameOver()
        {
            this.gameOver=true;
        }

    }
}
