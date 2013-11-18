using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace GameStateManagementSample.Logic
{
    class Button
    {
        #region Fields
        public event EventHandler Click;

        private MouseState lastState, currentState;
        private Rectangle bounds;
        private Texture2D texture;
        private Vector2 position;
        private Boolean enabled;
        private String text;
        private Color textColor;

        public delegate void DrawExtraHandler(SpriteBatch spriteBatch);
        public event DrawExtraHandler DrawExtra;
        #endregion

        #region Properties
        public Texture2D Texture
        {
            get { return texture; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public Boolean Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public Vector2 Origin
        {
            get { return new Vector2(texture.Width / 2, texture.Height / 2); }
        }
        #endregion

        public Button(Vector2 position, String text, Color textColor)
        {
            this.position = position;
            this.enabled = true;
            this.text = text;
            this.textColor = textColor;
        }

        public void LoadContent(ContentManager content, String texture)
        {
            this.texture = content.Load<Texture2D>(texture);
            this.bounds = new Rectangle((int)(position.X - Origin.X),
                                        (int)(position.Y - Origin.Y),
                                        this.texture.Width, this.texture.Height);
        }

        protected void OnClick(EventArgs args)
        {
            if (Click != null)
                this.Click(this, args);
        }

        public void Update()
        {
            if (enabled == true)
            {
                this.lastState = currentState;
                this.currentState = Mouse.GetState();
                if (bounds.Contains(new Point(currentState.X, currentState.Y)) &&
                    currentState.LeftButton == ButtonState.Pressed &&
                    lastState.LeftButton == ButtonState.Released)
                {
                    this.OnClick(EventArgs.Empty);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (enabled)
            {
                Vector2 stringDimensions = GameplayScreen.gameFont.MeasureString(text);
                spriteBatch.Draw(texture, position, null, Color.White, 0, Origin, 1, SpriteEffects.None, 0);
                spriteBatch.DrawString(GameplayScreen.gameFont, text, position - stringDimensions / 2, textColor);
            }

            if(DrawExtra != null)
                DrawExtra(spriteBatch);
        }
    }
}
