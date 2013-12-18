using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Utility
{
    public static class RendererHelper
    {
        private static Texture2D dummyTexture;
        public static void DrawLine(this SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 end, Color color, float thickness, float layer)
        {
            // Distanz
            float distance = Vector2.Distance(start, end);

            // Winkel
            float angle = (float)Math.Atan2((double)(end.Y - start.Y), (double)(end.X - start.X));

            spriteBatch.Draw(texture,
                start + 0.5f * (end - start),
                null,
                color,
                angle,
                new Vector2(0.5f, 0.5f),
                new Vector2(distance, thickness),
                SpriteEffects.None,
                layer
            );
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 end, Color color, float thickness)
        {
            DrawLine(spriteBatch, texture, start, end, color, thickness, 0f);
        }

        public static void DrawCircle(this SpriteBatch spriteBatch, Vector2 position, int radius, Color color)
        {
            /*
             * Textur erzeugen,
             * Credits: Stackoverflow, http://stackoverflow.com/questions/2983809/how-to-draw-circle-with-specific-color-in-xna/2984527#2984527
             */
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(GameplayScreen.gd, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            texture.SetData(data);

            spriteBatch.Draw(texture, position - new Vector2(texture.Width/2, texture.Height/2), color);
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color)
        {
            if(dummyTexture == null) {
                dummyTexture = new Texture2D(GameplayScreen.gd, 1, 1);
                Color[] data = new Color[1];
                data[0] = Color.White;
                dummyTexture.SetData(data);
            }

            spriteBatch.Draw(dummyTexture, destinationRectangle, color);
        }
    }
}
