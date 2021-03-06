﻿using System;
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
    class PictureButton
    {
        #region Fields
        public event EventHandler Click;

        private MouseState lastState, currentState;
        private Rectangle bounds;
        private Texture2D texture;
        private Vector2 position;
        private Boolean enabled;
        private ButtonMouseState bmstate;
        private enum ButtonMouseState{normal,hover,pressed};
        private Color hoverColor;        

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

        public PictureButton(Vector2 position)
        {
            this.position = position;
            this.enabled = true;
            hoverColor = Color.White;
        }

        public PictureButton(Vector2 position,Color hover)
        {
            this.hoverColor = hover;
            this.position = position;
            this.enabled = true;
        }

        public void LoadContent(ContentManager content, Texture2D texture)
        {
            this.texture = texture;
            this.bounds = new Rectangle((int)(position.X - Origin.X),
                                        (int)(position.Y - Origin.Y),
                                        this.texture.Width, this.texture.Height);
        }

        protected void OnClick(EventArgs args)
        {
            if (Click != null)
                this.Click(this, args);
        }

        public bool isHoverOrPressed()
        {
            return bmstate == ButtonMouseState.hover || bmstate == ButtonMouseState.pressed;
        }


        public void Update()
        {   

            //if (enabled == true)
            //{
                this.lastState = currentState;
                this.currentState = Mouse.GetState();
                if (currentState.LeftButton == ButtonState.Released &&
                    lastState.LeftButton == ButtonState.Pressed)
                {   // Maustaste wird losgelassen -> state = normal
                    bmstate = ButtonMouseState.normal;
                }
                else if (currentState.LeftButton == ButtonState.Pressed &&
                    lastState.LeftButton == ButtonState.Released &&
                    bounds.Contains(new Point(currentState.X, currentState.Y))
                    && enabled)
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
            //}
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (enabled)
            {
                if (bmstate == ButtonMouseState.hover)
                    spriteBatch.Draw(texture, bounds, hoverColor);
                else
                {
                    spriteBatch.Draw(texture, bounds, Color.White);
                }
            } else {
                spriteBatch.Draw(texture, bounds, Color.White * 0.5f);
            }

            if(DrawExtra != null)
                DrawExtra(spriteBatch);
        }

        public void ChangeTexture(Texture2D tex)
        {
            texture = tex;
        }
    }
}
