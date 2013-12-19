using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Logic
{
    public class ParticleManager
    {
        private static ParticleManager instance;
        private Random random;
        private List<Particle> particles;
        private Texture2D texture;

        public static ParticleManager Instance { get { return instance; } }

        public ParticleManager() {
            particles = new List<Particle>();
            random = new Random();
            instance = this;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("particle");
        }

        private Particle GenerateNewParticle(Vector2 EmitterPosition, Color color)
        {
            Vector2 position = EmitterPosition;
            Vector2 velocity = new Vector2(
                1f + (float)(random.NextDouble() * 2 - 1),
                1f + (float)(random.NextDouble() * 2 - 1));

            // Nur interessant wenn man keine runden Partikel hat. Liegt aber soweit erstmal nicht vor...
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);

            /*Vector3 colorVariance = new Vector3(
                (float)random.NextDouble() * 0.2f,
                (float)random.NextDouble() * 0.2f,
                (float)random.NextDouble() * 0.2f);

            color = new Color(color.ToVector3() + colorVariance);*/
            float size = (float)random.NextDouble() * 1.4f;
            int ttl = 5 + random.Next(10);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void GenerateExplosion(Vector2 EmitterPosition, Color color, int numParticles)
        {
            for (int i = 0; i < numParticles; i++)
            {
                particles.Add(GenerateNewParticle(EmitterPosition, color));
            }
        }

        public void Update()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update();
                if (particles[i].TTL <= 0)
                {
                    particles.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle particle in particles)
                particle.Draw(spriteBatch);
        }
    }
}
