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
            drawCollision = false;                                  //set whether to draw collision box

            collisionInsetMax = new Vector2(0.25f, 0.3f);           //Correction for collision box
            collisionInsetMax = new Vector2(0.25f, 0f);             //^

            frameTime = 0.3f;                                       //How long each frame of animation takes

            anims = new List<List<Rectangle>>();                    //Define 2D list for all animations

            //Idle Anim
            anims.Add(new List<Rectangle>());                       //Add empty animation
            anims[0].Add(new Rectangle(0, 0, 48, 48));              //Add first frame
            anims[0].Add(new Rectangle(48, 0, 48, 48));             //Add second frame

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

            hasCollided = false;                                    //Player hasn't collided with a surface
            jumping = false;                                        //Player isn't jumping
            walking = false;                                        //Player isn't walking
            falling = true;                                         //Player isn't falling
            jumpPressed = false;                                    //Jump key hasn't been pressed yet
        }

        public void Update(GameTime gameTime, List<PlatformSprite> platforms)
        {
            if((falling || jumping) && spriteVel.Y < 500)
                spriteVel.Y += 10f * (float)gameTime.ElapsedGameTime.TotalSeconds;      //If player is falling or jumping, increase player's velocity downwards until max is reached

            spritePos += spriteVel;                                         //Update player's position based on velocity

            foreach(PlatformSprite platform in platforms)                   //Check for collision on all sides of the player
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

                if (walking) setAnim(1);                //If walking is true, set player's anim to walking anim
                else if (falling) setAnim(3);           //Same for falling
                else if (jumping) setAnim(2);           //Same for jumping
                else setAnim(0);                        //If none of these, default to idle animation
            }
        }
        public void resetPlayer(Vector2 newPos)             //Function to reset player on death (or at any other point)
        {
            spritePos = newPos;                         //Set sprite position
            spriteVel = new Vector2();                  //Set player's velocity to nothing
            jumping = false;                            //Set not jumping
            walking = false;                            //Set not walking
            falling = true;                             //Set not falling
            jumpPressed = false;                        //Set jump button to not pressed
        }
    }
}
