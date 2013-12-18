using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagementSample.Utility;

namespace GameStateManagementSample.Logic
{
    class Enemy : Sprite
    {
        #region Fields
        protected float startHealth;
        protected float currentHealth;

        protected bool alive = true;

        protected float speed = 0.5f;
        protected float currentspeed;

        private double slowTime;

        protected int bountyGiven;

        private Queue<Vector2> waypoints = new Queue<Vector2>();
        private static Texture2D dummyTexture;
        #endregion

        #region Properties
        public float CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public bool IsDead
        {
            get { return !alive; }
        }

        public int BountyGiven
        {
            get { return bountyGiven; }
        }

        public float DistanceToDestination
        {
            get { return Vector2.Distance(Position, waypoints.Peek()); }
        }
        #endregion

        public Enemy(Texture2D texture, Vector2 position, float health, int bountyGiven, float speed)
            : base(texture, position)
        {
            this.startHealth = health;
            this.currentHealth = startHealth;
            this.bountyGiven = bountyGiven;
            this.speed = speed;
            this.currentspeed = speed;
        }

        public void hit(float damage)
        {
            currentHealth -= damage;
        }

        public void SetWaypoints(Queue<Vector2> waypoints)
        {
            foreach (Vector2 waypoint in waypoints)
                this.waypoints.Enqueue(waypoint);

            this.Position = this.waypoints.Dequeue();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (currentHealth <= 0)
            {
                alive = false;
                Player.getInstance().rewardMoney(bountyGiven);
            }

            if (slowTime > 0) // Wenn slowTime > 0 ziehe vergangende zeit ab. 
                slowTime -= gameTime.ElapsedGameTime.TotalSeconds;

            if (slowTime <= 0) // Wenn slowTime <= 0, dann ist slow vorbei. Setze Speed auf normal
            {    
                currentspeed = speed;
                slowTime = 0;
            }

            if (waypoints.Count > 0)
            {
                if (DistanceToDestination < currentspeed)
                {
                    Position = waypoints.Peek();
                    waypoints.Dequeue();
                }
                else
                {
                    Vector2 direction = waypoints.Peek() - Position;
                    direction.Normalize();

                    velocity = Vector2.Multiply(direction, currentspeed);

                    Position += velocity;

                    rotation = (float)(Math.Atan2(direction.X, -direction.Y));
                }
            }
            else // Ende erreicht! Darf nun auch nicht mehr leben.
                alive = false;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                float healthPercentage = currentHealth / startHealth;
                Color color = new Color(new Vector3(1, healthPercentage, healthPercentage));
                base.Draw(spriteBatch, color);

                if (OptionsMenuScreen.showHealthbars)
                {
                    Color barColor = new Color(new Vector3(1 - healthPercentage, healthPercentage, 0));
                    spriteBatch.DrawRectangle(new Rectangle((int)Position.X, (int)Position.Y+3, (int)(texture.Width * healthPercentage * scale), 3), barColor);
                }
            }
        }

        internal void setSlow(double seconds, float factor)
        {
            slowTime = seconds;
            currentspeed *= factor;
        }
    }
}
