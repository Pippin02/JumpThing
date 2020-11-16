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
        Point screenSize = new Point(800, 450);

        Texture2D backTex, sheet1, sheet2, whiteBox;

        Vector2 startPos = new Vector2(100, 200);
        PlayerSprite player;
        List<List<PlatformSprite>> levels = new List<List<PlatformSprite>>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = screenSize.X;
            _graphics.PreferredBackBufferHeight = screenSize.Y;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            backTex = Content.Load<Texture2D>("background");
            sheet1 = Content.Load<Texture2D>("Sheet1");
            sheet2 = Content.Load<Texture2D>("Sheet2");

            whiteBox = new Texture2D(GraphicsDevice, 1, 1);
            whiteBox.SetData(new[] { Color.White });

            player = new PlayerSprite(sheet1, whiteBox, startPos);

            BuildLevels();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime, levels[0]);

            if (player.spritePos.Y - 150 > screenSize.Y) player.resetPlayer(startPos);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(
                backTex,
                new Rectangle(0, 0, screenSize.X, screenSize.Y),
                Color.White
                );

            foreach(PlatformSprite platform in levels[0])
                platform.Draw(_spriteBatch, gameTime);

            player.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        void BuildLevels()
        {
            levels.Add(new List<PlatformSprite>());
            levels[0].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(100, 300)));
            levels[0].Add(new PlatformSprite(sheet2, whiteBox, new Vector2(250, 300)));
        }
    }
}
