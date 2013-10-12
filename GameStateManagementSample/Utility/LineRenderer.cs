using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Utility
{
    public static class LineRenderer
    {
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
    }
}
