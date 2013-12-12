using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    class Weapon : Sprite
    {
        public Weapon(Texture2D texture, Vector2 position) : base(texture, position)
        {
        }

        public virtual void Update(GameTime gameTime) { base.Update(gameTime); }
        public virtual void Draw(SpriteBatch spriteBatch) { base.Draw(spriteBatch); }
    }
}
