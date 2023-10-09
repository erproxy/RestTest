using UnityEngine;

namespace Tools.Extensions
{
    public static class SpriteExt
    {
        public static Sprite CreateSprite(this Texture2D texture2D) 
        {
            Rect spriteRect = new Rect(0, 0, texture2D.width, texture2D.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            return Sprite.Create(texture2D, spriteRect, pivot);
        }
    }
}