using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace JumpThing
{
    class CoinSprite : Sprite
    {
        public CoinSprite(Texture2D newSpriteSheet, Texture2D newCollisionTex, Vector2 newPos)
            : base(newSpriteSheet, newCollisionTex, newPos)
        {
            spriteOrigin = new Vector2(0.5f, 0.5f);         //Set coin's origin to middle center
            isColliding = true;                             //Colliding is always true for coins
            frameTime = 0.3f;                               //Set animation speed

            collisionInsetMin = new Vector2(0.2f, 0.2f);    //Correction for collision box
            collisionInsetMax = new Vector2(0.2f, 0.2f);    //^

            anims = new List<List<Rectangle>>();            //Initialize 2D list for sprite animations
            anims.Add(new List<Rectangle>());               //Add empty animation
            anims[0].Add(new Rectangle(48, 48, 48, 48));    //Add first image
            anims[0].Add(new Rectangle(96, 48, 48, 48));    //Add second image
            anims[0].Add(new Rectangle(144, 48, 48, 48));   //Add third image
            anims[0].Add(new Rectangle(96, 48, 48, 48));    //Add fourth image
        }
    }
}