/*
 * Script Author: Charles d'Ansembourg
 * Creation Date: 01/04/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace Utils
{
    public static class UtilityFunctions
    {
        public static (Vector2, Vector2) GetBoxCollSizeFromSprite(SpriteRenderer spriteRenderer)
        {
            Sprite sprite = spriteRenderer.sprite;
            Texture2D texture = sprite.texture;
            Rect rect = sprite.textureRect;
            float pixelsPerUnit = sprite.pixelsPerUnit;
            int xMin = Mathf.FloorToInt(rect.width), xMax = 0, yMin = Mathf.FloorToInt(rect.height), yMax = 0;

            for (int i = 0; i < rect.width; i++)
            {
                for (int j = 0; j < rect.height; j++)
                {
                    Color pixel = texture.GetPixel(Mathf.FloorToInt(rect.x) + i, Mathf.FloorToInt(rect.y) + j);
                    if (pixel.a > 0)
                    {
                        xMin = Mathf.Min(xMin, i);
                        xMax = Mathf.Max(xMax, i);
                        yMin = Mathf.Min(yMin, j);
                        yMax = Mathf.Max(yMax, j);
                    }
                }
            }

            Vector2 size = new Vector2((xMax - xMin + 1) / pixelsPerUnit, (yMax - yMin + 1) / pixelsPerUnit);
            Vector2 offset = new Vector2(((xMin + xMax) / 2f - rect.width / 2f) / pixelsPerUnit, ((yMin + yMax) / 2f - rect.height / 2f) / pixelsPerUnit);
            Vector2 offsetFinal = offset + (Vector2)sprite.pivot / pixelsPerUnit - new Vector2(sprite.bounds.size.x / 2, sprite.bounds.size.y / 2);

            return (size, offsetFinal);
        }
    }
}