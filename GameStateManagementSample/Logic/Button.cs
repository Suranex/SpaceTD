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
        private ButtonMouseState bmstate;
        private Color hoverColor;
        private Color pressedColor;
        private Color buttonColor;
        private enum ButtonMouseState{normal,hover,pressed};

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

        /// <summary>
        /// Button ohne hover und pressed Reaktion
        /// </summary>
        public Button(Vector2 position, String text, Color textColor,Color buttonColor)
        {
            this.position = position;
            this.enabled = true;
            this.text = text;
            this.textColor = textColor;
            this.buttonColor = buttonColor;
            this.hoverColor = buttonColor;
            this.pressedColor = buttonColor;
        }

        /// <summary>
        /// Button mit hover und pressed Reaktion
        /// </summary>
        public Button(Vector2 position, String text, Color textColor, Color buttonColor, Color hoverColor, Color pressedColor)
        {
            this.position = position;
            this.enabled = true;
            this.text = text;
            this.textColor = textColor;
            this.buttonColor = buttonColor;
            this.hoverColor = hoverColor;
            this.pressedColor = pressedColor;
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
                    if (currentState.LeftButton == ButtonState.Released &&
                        lastState.LeftButton == ButtonState.Pressed)
                    {   // Maustaste wird losgelassen -> state = normal
                        bmstate = ButtonMouseState.normal;
                    }
                    else if (currentState.LeftButton == ButtonState.Pressed &&
                        lastState.LeftButton == ButtonState.Released &&
                        bounds.Contains(new Point(currentState.X, currentState.Y)))
                    {   // Maustaste wird gedrückt, Mauszeiger auf Button -> state = pressed
                        bmstate = ButtonMouseState.pressed;
                        this.OnClick(EventArgs.Empty);
                    }
                    else if (bounds.Contains(new Point(currentState.X, currentState.Y)) &&
                        currentState.LeftButton == ButtonState.Released &&
                        lastState.LeftButton == ButtonState.Released)
                    {   // Maustaste war und ist released, Mauszeiger auf Button -> state = hover
                        bmstate = ButtonMouseState.hover;
                    }
                    else if(bmstate == ButtonMouseState.hover &&
                        !bounds.Contains(new Point(currentState.X, currentState.Y)))
                    {   // State == hover und Mauszeiger ist nicht mehr auf Button -> state = normal
                        bmstate = ButtonMouseState.normal;
                    }
                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (enabled)
            {
                Vector2 stringDimensions = GameplayScreen.gameFont.MeasureString(text);
                

                // TODO State Unterscheidung vervollständigen
                switch (bmstate)
                {
                    case ButtonMouseState.normal:
                        spriteBatch.Draw(texture, position, null, buttonColor, 0, Origin, 1, SpriteEffects.None, 0);
                        break;
                    case ButtonMouseState.hover:
                        spriteBatch.Draw(texture, position, null, hoverColor, 0, Origin, 1, SpriteEffects.None, 0);
                        break;
                    case ButtonMouseState.pressed:
                        spriteBatch.Draw(texture, position, null, pressedColor, 0, Origin, 1, SpriteEffects.None, 0);
                        break;
                }
                spriteBatch.DrawString(GameplayScreen.gameFont, text, position - stringDimensions / 2, textColor);
            }

            if(DrawExtra != null)
                DrawExtra(spriteBatch);
        }
    }
}
