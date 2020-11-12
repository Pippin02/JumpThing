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
        bool jumping, walking, falling, jumpPressed;
        const float jumpSpeed = 150f, walkSpeed = 100f;

        public PlayerSprite(Texture2D newSpriteSheet, Texture2D newColTex, Vector2 newPos) : base(newSpriteSheet, newColTex, newPos)
        {
            spriteOrigin = new Vector2(0.5f, 1f);
            isColliding = true;
            drawCollision = false;

            collisionInsetMax = new Vector2(0.25f, 0.3f);
            collisionInsetMax = new Vector2(0.25f, 0f);

            frameTime = 0.3f;

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

            jumping = false;
            walking = false;
            falling = true;
            jumpPressed = false;
        }

        public override void Update(GameTime gameTime)
        {
            if((falling || jumping) && spriteVel.Y < 500)
                spriteVel.Y += 10f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            spritePos += spriteVel;
        }
        public void resetPlayer(Vector2 newPos)
        {
            spritePos = newPos;
            spriteVel = new Vector2();
            jumping = false;
            walking = false;
            falling = true;
            jumpPressed = false;
        }
    }
}
