using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
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
        int currentLevel = 0;                                                           //Integer for the current level number
        int lives = 3;                                                                  //Number of player lives
        int coinCount = 8;                                                              //Number of coins collected
        bool gameOver = false;                                                          //Set to true if player is out of lives

        Texture2D backTex, sheet1, sheet2, whiteBox;                                    //Define all Texture2Ds in the game
        SpriteFont font, bigFont;                                                       //Define fonts for the UI
        public SoundEffect jumpSound, deathSound, coinSound;                                   //Define sound effects

        Vector2 startPos = new Vector2(100, 300);                                       //Define starting position for the player
        PlayerSprite player;                                                            //Define player as a PlayerSprite
        CoinSprite coin;                                                                //Define a single coin per level
        List<Vector2> coinPos = new List<Vector2>();
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
            font = Content.Load<SpriteFont>("Font1");                                   //Load font file for UI
            bigFont = Content.Load<SpriteFont>("BigFont");                              //Load big font file for game over
            jumpSound = Content.Load<SoundEffect>("JumpSound");                         //Load jump sound
            deathSound = Content.Load<SoundEffect>("DeathSound");                       //Load death sound
            coinSound = Content.Load<SoundEffect>("CoinSound");                         //Load coin collction sound

            whiteBox = new Texture2D(GraphicsDevice, 1, 1);                             //Create empty sprite for drawing collision (1 by 1 pixel only)
            whiteBox.SetData(new[] { Color.White });                                    //Fill collision sprite with a white pixel

            BuildLevels();                                                              //Run BuildLevels (places all platforms for current level)

            player = new PlayerSprite(sheet1, whiteBox, startPos);                      //Create player
            coin = new CoinSprite(sheet1, whiteBox, coinPos[currentLevel]);             //Create coin sprite
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!gameOver)
            {
                player.Update(gameTime, levels[currentLevel], jumpSound);                              //Update player

                if (player.spritePos.Y - 150 > screenSize.Y)
                {
                    if (lives > 0)
                    {
                        player.resetPlayer(startPos);  //Reset player to the starting position if they fall below the screen
                        lives--;
                        deathSound.Play();
                    }
                    if (lives == 0)
                    {
                        gameOver = true;
                    }
                }

                if (player.checkCollision(coin))
                {
                    currentLevel++;
                    if (currentLevel >= levels.Count)
                        currentLevel = 0;
                    coin.spritePos = coinPos[currentLevel];
                    player.resetPlayer(startPos);
                    coinSound.Play();
                    if (coinCount == 9)
                    {
                        coinCount = 0;
                        lives++;
                    }
                    else
                        coinCount++;
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    currentLevel = 0;
                    player.resetPlayer(startPos);
                    coinCount = 0;
                    lives = 3;
                    gameOver = false;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            string livesString = "";

            _spriteBatch.Begin();

            _spriteBatch.Draw(                                                          //Draw background
                   backTex,
                   new Rectangle(0, 0, screenSize.X, screenSize.Y),
                   Color.White
                   );

            for (int i = 0; i < lives; i++)
                livesString = livesString + "I";
            _spriteBatch.DrawString(font, "Lives: " + livesString, new Vector2(8, -10), Color.Gray);
            _spriteBatch.DrawString(font, "Lives: " + livesString, new Vector2(5, -13), Color.White);

            Vector2 coinTextSize = font.MeasureString("Coins Collected: " + coinCount.ToString());
            _spriteBatch.DrawString(font, "Coins Collected: " + coinCount.ToString(), new Vector2(screenSize.X - coinTextSize.X - 8, -10), Color.Gray);
            _spriteBatch.DrawString(font, "Coins Collected: " + coinCount.ToString(), new Vector2(screenSize.X - coinTextSize.X - 5, -10), Color.White);

            if (!gameOver)
            {
                Vector2 textSize = font.MeasureString("collect 10 coins for another life!");
                _spriteBatch.DrawString(font, "collect 10 coins for another life!", new Vector2(screenSize.X / 2 - (textSize.X / 2) - 5, screenSize.Y - 55), Color.Gray);
                _spriteBatch.DrawString(font, "collect 10 coins for another life!", new Vector2(screenSize.X / 2 - (textSize.X / 2), screenSize.Y - 52), Color.White);

                player.Draw(_spriteBatch, gameTime);                                        //Draw player

                foreach (PlatformSprite platform in levels[currentLevel])                   //Draw all platforms
                    platform.Draw(_spriteBatch, gameTime);

                coin.Draw(_spriteBatch, gameTime);
            }
            else
            {
                Vector2 textSize = bigFont.MeasureString("GAME OVER");
                _spriteBatch.DrawString(bigFont, "GAME OVER", new Vector2(screenSize.X / 2 - (textSize.X / 2) - 5, screenSize.Y / 2 - (textSize.Y / 2) - 5), Color.White);
                _spriteBatch.DrawString(bigFont, "GAME OVER", new Vector2(screenSize.X / 2 - (textSize.X / 2), screenSize.Y / 2 - (textSize.Y / 2)), Color.DarkRed);
                textSize = font.MeasureString("Press Enter to restart.");
                _spriteBatch.DrawString(font, "Press Enter to restart.", new Vector2(screenSize.X / 2 - (textSize.X / 2) - 5, screenSize.Y / 2 - (textSize.Y / 2) + 45), Color.Gray);
                _spriteBatch.DrawString(font, "Press Enter to restart.", new Vector2(screenSize.X / 2 - (textSize.X / 2), screenSize.Y / 2 - (textSize.Y / 2) + 48), Color.White);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        void BuildLevels()
        {                                                                               //Place platforms for specific level
            levels.Add(new List<PlatformSprite>());                                     //Add first level
            levels[0].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(100, 300))); //Add first platform
            levels[0].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(250, 300))); //Add second platform
            levels[0].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(550, 325)));
            levels[0].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(700, 275)));

            levels.Add(new List<PlatformSprite>());                                     //Add first level
            levels[1].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(100, 300))); //Add first platform
            levels[1].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(250, 250))); //Add second platform
            levels[1].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(700, 275)));
            levels[1].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(400, 350)));

            levels.Add(new List<PlatformSprite>());
            levels[2].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(100, 300)));
            levels[2].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(290, 275)));
            levels[2].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(400, 200)));
            levels[2].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(550, 325)));

            levels.Add(new List<PlatformSprite>());
            levels[3].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(100, 400)));
            levels[3].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(190, 275)));
            levels[3].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(250, 375)));
            levels[3].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(300, 250)));
            levels[3].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(400, 350)));
            levels[3].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(550, 325)));
            levels[3].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(700, 275)));
            levels[3].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(550, 225)));
            levels[3].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(400, 200)));
            levels[3].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(250, 150)));
            levels[3].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(100, 100)));

            coinPos.Add(new Vector2(250, 280));                                         //Add first level's coin
            coinPos.Add(new Vector2(250, 230));                                         //Add second level's coin
            coinPos.Add(new Vector2(325, 200));
            coinPos.Add(new Vector2(100, 50));
        }
    }
}
