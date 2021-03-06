﻿using System;
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
        protected Enemy target;
        protected float damage;
        protected Tower tower;      // welcher Tower diese Waffe gefeuert hat
        protected static List<Texture2D> texturen;

        #region Content loading
        public static void LoadContent(ContentManager content)
        {
            texturen = new List<Texture2D>();
            texturen.Add(content.Load<Texture2D>("weißPixel")); // 0 Laser
            texturen.Add(content.Load<Texture2D>("redProjectile")); // 1 Canon
            texturen.Add(content.Load<Texture2D>("blau")); // 2 slow
        }
        #endregion


        public Weapon(Texture2D texture, Vector2 position, Enemy target, float damage, Tower tower) : base(texture, position)
        {
            this.target = target;
            this.damage = damage;
            this.scale = 1f;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.tower = tower;
        }

        //public virtual void Update(GameTime gameTime) { base.Update(gameTime); }
        //public virtual void Draw(SpriteBatch spriteBatch) { base.Draw(spriteBatch); }
    }
}
