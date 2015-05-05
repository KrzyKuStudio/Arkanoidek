using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Arkanoidek
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch backgroundBatch;

        //Game objectr
        Paddle paddle;
        List<Ball> balls = new List<Ball>();
        List<Enemy> enemys = new List<Enemy>();

        // textures
        //static Texture2D paddleSprite;
        //static Texture2D ballSprite;
        //static Texture2D enemySprite;

        //background
        Texture2D background;
        Rectangle mainFrame;

        // scoring support
        int score = 0;
        string scoreString = GameConstants.SCORE_PREFIX + 0;

        // health support
        string healthString = GameConstants.HEALTH_PREFIX +
            GameConstants.PADDLE_INITIAL_HEALTH;

        // text display support
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // set resolution
            graphics.PreferredBackBufferWidth = GameConstants.WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = GameConstants.WINDOW_HEIGHT;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            RandomNumberGenerator.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load sprite font
            //font = Content.Load<SpriteFont>("Arial20");

            background = Content.Load<Texture2D>("bground");
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            //spawn objekts
            paddle = new Paddle(Content, "paddle", graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight - graphics.PreferredBackBufferHeight / 19, new Vector2(0, 0),
                null);
            
            
            Ball ball = new Ball(Content, "ball", graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight, new Vector2(-1, -1),
                null);

            // set initial health and score strings
            //healthString = GameConstants.HEALTH_PREFIX + paddle.Health.ToString();
            // scoreString = GameConstants.SCORE_PREFIX + score.ToString();


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            MouseState mouse = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();

            paddle.Update(gameTime, mouse, keyboard);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(background, mainFrame, Color.White);


            foreach (Enemy enemy in enemys)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (Ball ball in balls)
            {
                ball.Draw(spriteBatch);
            }
            paddle.Draw(spriteBatch);

            // draw score and health
            //spriteBatch.DrawString(font, healthString, GameConstants.HEALTH_LOCATION, Color.White);
            //spriteBatch.DrawString(font, scoreString, GameConstants.SCORE_LOCATION, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
