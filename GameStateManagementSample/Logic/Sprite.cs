using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class Sprite
    {
        #region Fields
        protected Texture2D texture;

        protected Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        protected Vector2 velocity;

        protected Vector2 center;
        protected Vector2 origin;

        protected float skale;

        protected float rotation;
        #endregion

        #region Properties
        public Vector2 Center
        {
            get { return center; }
        }
        #endregion

        public Sprite(Texture2D tex, Vector2 pos)
        {
            texture = tex;

            position = pos;
            velocity = Vector2.Zero;

            skale = (float)Level.TileWidth / texture.Width;

            center = new Vector2(position.X + texture.Width * skale/ 2 - 2,
                                position.Y + texture.Height * skale / 2 - 2);
            origin = new Vector2(texture.Width * skale / 2, texture.Height * skale / 2);
        }

        public virtual void Update(GameTime gameTime)
        {
            this.center = new Vector2(position.X + texture.Width  * skale / 2-2,
                                      position.Y + texture.Height * skale / 2-2);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, center, null, Color.White,
                rotation, origin, skale, SpriteEffects.None, 0);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, center, null, color, rotation, origin, skale, SpriteEffects.None, 0);
        }
    }
}
