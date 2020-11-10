using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace JumpThing
{
    class Sprite
    {
        public Texture2D spriteSheetTex, collisionTex;
        public Vector2 spritePos, spriteVel, spriteOffset, colScale, colOffset;
        public bool isDead, isColliding, drawCol, isFlipped;

        public int currentAnim, currentFrame;
        public float frameTime, frameCount;
        public List<List<Rectangle>> anims;

        public Sprite(Texture2D newSpriteSheet, Texture2D newCollisionTex, Vector2 newPos)
        {
            spriteSheetTex = newSpriteSheet;
            collisionTex = newCollisionTex;
            spritePos = newPos;

            spriteOffset = new Vector2(0.5f, 0.5f);
            spriteVel = new Vector2(0f, 0f);
            colScale = new Vector2(1f, 1f);
            colOffset = new Vector2(0f, 0f);
            isColliding = false;
            drawCol = false;
            isDead = false;
            currentAnim = 0;
            currentFrame = 0;frameTime = 0.5f;
            frameCount = frameTime;

            anims = new List<List<Rectangle>>();
            
            anims.Add(new List<Rectangle>());
            anims[0].Add(new Rectangle(0, 0, 48, 48));
        }

        public virtual void Update(GameTime gameTime, Point screenSize) { }
    }
}
