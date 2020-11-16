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
        bool jumping, walking, falling, jumpPressed, hasCollided;   //is player jumping, walking, falling, has jum,p been pressed, has player sprite collided
        const float jumpSpeed = 150f, walkSpeed = 100f;             //Constant variables for the player's jump and walk speed

        public PlayerSprite(Texture2D newSpriteSheet, Texture2D newColTex, Vector2 newPos) : base(newSpriteSheet, newColTex, newPos)
        {
            spriteOrigin = new Vector2(0.5f, 1f);                   //set origin of player sprite
            isColliding = true;                                     //Player can collide
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

            hasCollided = false;
            jumping = false;
            walking = false;
            falling = true;
            jumpPressed = false;
        }

        public void Update(GameTime gameTime, List<PlatformSprite> platforms)
        {
            if((falling || jumping) && spriteVel.Y < 500)
                spriteVel.Y += 10f * (float)gameTime.ElapsedGameTime.TotalSeconds;

            spritePos += spriteVel;

            foreach(PlatformSprite platform in platforms)
            {
                if(checkCollisionBelow(platform))
                {
                    hasCollided = true;

                    while(checkCollision(platform))
                        spritePos.Y--;

                    spriteVel.Y = 0;
                    jumping = false;
                    falling = false;
                }
                else if(checkCollisionAbove(platform))
                {
                    hasCollided = true;
                    while (checkCollision(platform)) spritePos.Y++;
                    spriteVel.Y = 0;
                    jumping = false;
                    falling = true;
                }

                if (checkCollisionLeft(platform))
                {
                    hasCollided = true;
                    while (checkCollision(platform))
                        spritePos.X++;
                    spriteVel.X = 0;
                }
                else if (checkCollisionRight(platform))
                {
                    hasCollided = true;
                    while (checkCollision(platform))
                        spritePos.X--;
                    spriteVel.X = 0;
                }

                if(!hasCollided && walking)
                    falling = true;

                if (jumping && spriteVel.Y > 0)
                {
                    jumping = false;
                    falling = true;
                }

                if (walking) setAnim(1);
                else if (falling) setAnim(3);
                else if (jumping) setAnim(2);
                else setAnim(0);
            }
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
