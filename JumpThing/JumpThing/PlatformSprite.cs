using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace JumpThing
{
    class PlatformSprite : Sprite
    {
        public PlatformSprite(Texture2D newSpriteSheet, Texture2D newCollisionTex, Vector2 newPos)
            : base(newSpriteSheet, newCollisionTex, newPos)
        {
            spriteOrigin = new Vector2(0.5f, 0f);
            isColliding = true;

            anims = new List<List<Rectangle>>();
            anims.Add(new List<Rectangle>());
            anims[0].Add(new Rectangle(0, 0, 96, 32));
        }
    }
}
