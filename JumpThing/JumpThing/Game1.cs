using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace JumpThing
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Point screenSize = new Point(800, 450);                                         //Two integers that define the size of the window

        Texture2D backTex, sheet1, sheet2, whiteBox;                                    //Define all Texture2Ds in the game

        Vector2 startPos = new Vector2(100, 200);                                       //Define starting position for the player
        PlayerSprite player;                                                            //Define player as a PlayerSprite
        List<List<PlatformSprite>> levels = new List<List<PlatformSprite>>();           //Define 2D list for all platforms in all levels

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = screenSize.X;                          //Set width of the screen
            _graphics.PreferredBackBufferHeight = screenSize.Y;                         //Set height of the screen
            _graphics.ApplyChanges();                                                   //Apply graphics changes

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            backTex = Content.Load<Texture2D>("background");                            //Load background texture
            sheet1 = Content.Load<Texture2D>("Sheet1");                                 //Load sprite sheet 1 (Player and coins)
            sheet2 = Content.Load<Texture2D>("Sheet2");                                 //Load sprite sheet 2 (platforms)

            whiteBox = new Texture2D(GraphicsDevice, 1, 1);                             //Create empty sprite for drawing collision (1 by 1 pixel only)
            whiteBox.SetData(new[] { Color.White });                                    //Fill collision sprite with a white pixel

            player = new PlayerSprite(sheet1, whiteBox, startPos);                      //Create player

            BuildLevels();                                                              //Run BuildLevels (places all platforms for current level)
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime, levels[0]);                                         //Update player

            if (player.spritePos.Y - 150 > screenSize.Y) player.resetPlayer(startPos);  //Reset player to the starting position if they fall below the screen

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(                                                          //Draw background
                backTex,
                new Rectangle(0, 0, screenSize.X, screenSize.Y),
                Color.White
                );

            foreach(PlatformSprite platform in levels[0])                               //Draw all platforms
                platform.Draw(_spriteBatch, gameTime);

            player.Draw(_spriteBatch, gameTime);                                        //Draw player

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        void BuildLevels()                                                              //Place platforms for specific level
        {
            levels.Add(new List<PlatformSprite>());                                     //Add first level
            levels[0].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(100, 300))); //Add first platform
            levels[0].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(250, 300))); //Add second platform
        }
    }
}
