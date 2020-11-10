using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace JumpThing
{
    class PlayerSprite : Sprite
    {
        public PlayerSprite(Texture2D newSpriteSheet, Texture2D newColTex, Vector2 newPos) : base(newSpriteSheet, newColTex, newPos)
        {
            frameTime = 0.1f;

            anims = new List<List<Rectangle>>();

            //Idle Anim
            anims.Add(new List<Rectangle>());
            anims[0].Add(new Rectangle(0, 0, 48, 48));
            anims[0].Add(new Rectangle(48, 0, 48, 48));

            //Walk Anim
            anims.Add(new List<Rectangle>());
            anims[1].Add(new Rectangle(48, 0, 48, 48));
            anims[1].Add(new Rectangle(96, 0, 48, 48));
            anims[1].Add(new Rectangle(48, 0, 48, 48));
            anims[1].Add(new Rectangle(144, 0, 48, 48));

            //Different Anim
            anims.Add(new List<Rectangle>());
            anims[2].Add(new Rectangle(98, 0, 48, 48));

            //Different Anim
            anims.Add(new List<Rectangle>());
            anims[3].Add(new Rectangle(0, 48, 48, 48));
        }
    }
}
