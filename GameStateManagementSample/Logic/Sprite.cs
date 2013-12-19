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

        protected Vector2 velocity;

        protected Vector2 center;
        protected Vector2 origin;

        protected float scale;

        protected float rotation;
        #endregion

        #region Properties
        public Vector2 Center
        {
            get { return center; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 OriginPosition
        {
            get { return Vector2.Add(Position, origin); }
        }
        #endregion

        public Sprite(Texture2D tex, Vector2 pos)
        {
            texture = tex;

            position = pos;
            velocity = Vector2.Zero;

            scale = (float)Level.TileWidth / texture.Width;
            origin = new Vector2(texture.Width * scale / 2, texture.Height * scale / 2);
            center = position + origin;
        }

        public virtual void Update(GameTime gameTime)
        {
            this.center = position + origin;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, center, null, Color.White,
                rotation, origin, scale, SpriteEffects.None, 0);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, center, null, color, rotation, origin, scale, SpriteEffects.None, 0);
        }
    }
}
