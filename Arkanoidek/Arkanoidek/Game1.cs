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
        
        //Gamestate
        public enum GameState
        {
            NewGame,
            Playing,
            GameOver,
            StartGame
        }

        GameState gameState = GameState.StartGame;
        //Game objectr
        Paddle paddle;
        List<Ball> balls = new List<Ball>();
        List<Enemy> enemys = new List<Enemy>();

        static Vector2 enemysvelocity = new Vector2(1,0);

        SoundEffect ballBounceSound;
        SoundEffect themeSound;
        SoundEffect crowdSound;
        SoundEffectInstance soundThemeInstance;

        //background texture
        Texture2D background;
        Texture2D backgroundborder;
        Rectangle mainFrame;

        // scoring support
        int score = 0;
        string scoreString = GameConstants.SCORE_PREFIX + 0;

        // text display support
        SpriteFont font1;
        SpriteFont gameoverfont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            // load sprite font
            font1 = Content.Load<SpriteFont>("fonts\\Arial20");
            gameoverfont = Content.Load<SpriteFont>("fonts\\Arial100");

            ballBounceSound = Content.Load<SoundEffect>("sounds\\ballbounce");
            themeSound = Content.Load<SoundEffect>("sounds\\soundtheme");
            crowdSound = Content.Load<SoundEffect>("sounds\\crowd");
            soundThemeInstance = themeSound.CreateInstance();
            soundThemeInstance.IsLooped = true;
            soundThemeInstance.Volume = GameConstants.MUSIC_VOL;

            background = Content.Load<Texture2D>("image\\bground");
            backgroundborder = Content.Load<Texture2D>("image\\bgborder");
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            

         }
    

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            balls.Clear();
            enemys.Clear();
            
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
            if(gameState == GameState.GameOver)
            {
                soundThemeInstance.Stop();
                if (keyboard.IsKeyDown(Keys.Space))
                {
                    gameState = GameState.NewGame;

                }
            }
            if (gameState == GameState.StartGame)
            {
                
                if (keyboard.IsKeyDown(Keys.Space))
                {
                    gameState = GameState.NewGame;

                }
            }

            // new game 
            #region
            if (gameState==GameState.NewGame)
         {
                //clear objects
             balls.Clear();
             enemys.Clear();
                
                //music theme
             soundThemeInstance.Play();
           
                //spawn objekts

            paddle = new Paddle(Content, "image\\paddle", graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight - graphics.PreferredBackBufferHeight / 25, new Vector2(0, 0),
                null);
            int ball1velx,ball1vely,ball2velx,ball2vely;

            //randomize balls velocitys
            #region

            if (RandomNumberGenerator.Next(100)<50)
            {
                ball1velx = -2;
            }
            else
            {
                ball1velx = 2;
            }
            if (RandomNumberGenerator.Next(100) < 50)
            {
                ball1vely = -2;
            }
            else
            {
                ball1vely = 2;
            }
            if (RandomNumberGenerator.Next(100) < 50)
            {
                ball2velx = -1;
            }
            else
            {
                ball2velx = 1;
            }
            if (RandomNumberGenerator.Next(100) < 50)
            {
                ball2vely = -1;
            }
            else
            {
                ball2vely = 1;
            }
            #endregion

            balls.Add(new Ball(Content, "image\\ball", graphics.PreferredBackBufferWidth / 3 + RandomNumberGenerator.Next(graphics.PreferredBackBufferHeight / 3),
                graphics.PreferredBackBufferHeight / 2, new Vector2(ball1velx, ball1vely),
                null));
            balls.Add(new Ball(Content, "image\\ball", graphics.PreferredBackBufferWidth / 2,
                graphics.PreferredBackBufferHeight /2, new Vector2(ball2velx, ball2vely),
                null));

            //add start bricks          
            int row = 0;
            for (int idx = 0; idx < (GameConstants.ROWS); ++idx)
            {

                for (int it = 0; it < (GameConstants.BRICKS / GameConstants.ROWS); ++it)
                {
                    enemys.Add(new Enemy(Content, "image\\brick", (int)(GameConstants.CLOUD_CORNER * graphics.PreferredBackBufferWidth) + it * (int)(GameConstants.CLOUD_CORNER * graphics.PreferredBackBufferWidth),
                    25 + row * 60, enemysvelocity, null));
                }
                row++;

            }
            score = 0;
            gameState = GameState.Playing;
         }
            #endregion
            // set score strings
            scoreString = GameConstants.SCORE_PREFIX + score.ToString();

            if (gameState == GameState.Playing)
            {
                paddle.Update(gameTime, mouse, keyboard);
                foreach (Ball ball in balls)
                {
                    ball.Update(gameTime);
                }
                foreach (Enemy enemy in enemys)
                {
                    enemy.Update(gameTime);
                }



                //Add new blok of bricks
                if (enemys.Count > 0)
                {
                    if (enemys[(enemys.Count - 1)].DrawRectangle.Y > 80)
                    {

                        for (int it = 0; it < (GameConstants.BRICKS / GameConstants.ROWS); ++it)
                        {
                            enemys.Add(new Enemy(Content, "image\\brick", (int)(GameConstants.CLOUD_CORNER * graphics.PreferredBackBufferWidth) + it * (int)(GameConstants.CLOUD_CORNER * graphics.PreferredBackBufferWidth),
                            25, enemys[(enemys.Count - 1)].Velocity, null));
                        }

                    }
                }


                // check bounce between balls
                #region
                {
                    CollisionResolutionInfo var;
                    int elapsedSpawnDelayMilliseconds = 0;
                    elapsedSpawnDelayMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
                    for (int i = 0; i < balls.Count; i++)
                    {
                        for (int j = i + 1; j < balls.Count; j++)
                        {
                            if (balls[i].Active &&
                                balls[j].Active)
                            {

                                var = CollisionUtils.CheckCollision(elapsedSpawnDelayMilliseconds,
                                   GameConstants.WINDOW_WIDTH, GameConstants.WINDOW_HEIGHT, balls[i].Velocity,
                                   balls[i].DrawRectangle, balls[j].Velocity, balls[j].DrawRectangle);
                                if (var != null)
                                {
                                    if (var.FirstOutOfBounds == true)
                                    {
                                        balls[i].Active = false;
                                    }
                                    else
                                    {
                                        balls[i].Velocity = var.FirstVelocity;
                                        balls[i].DrawRectangle = var.FirstDrawRectangle;
                                        ballBounceSound.Play(GameConstants.SFX_VOL - 0.1F, 1, 0);


                                    }

                                    if (var.SecondOutOfBounds == true)
                                    {
                                        balls[j].Active = false;
                                    }
                                    else
                                    {
                                        balls[j].Velocity = var.SecondVelocity;
                                        balls[j].DrawRectangle = var.SecondDrawRectangle;
                                    }

                                }


                            }
                        }
                    }

                }
                #endregion

                //Bounce ball vs paddle
                #region
                //{
                //    CollisionResolutionInfo var;
                //    int elapsedSpawnDelayMilliseconds = 0;
                //    elapsedSpawnDelayMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
                //    for (int i = 0; i < balls.Count; i++)
                //    {
                //        if (balls[i].Active &&
                //                paddle.Active)
                //        {

                //            var = CollisionUtils.CheckCollision(elapsedSpawnDelayMilliseconds,
                //               GameConstants.WINDOW_WIDTH, GameConstants.WINDOW_HEIGHT, balls[i].Velocity,
                //               balls[i].DrawRectangle, paddle.Velocity, paddle.DrawRectangle);
                //            if (var != null)
                //            {
                //                if (var.FirstOutOfBounds == true)
                //                {
                //                    balls[i].Active = false;
                //                }
                //                else
                //                {
                //                    balls[i].Velocity = var.FirstVelocity;
                //                    balls[i].DrawRectangle = var.FirstDrawRectangle;
                //                }

                //                if (var.SecondOutOfBounds == true)
                //                {
                //                    paddle.Active = false;
                //                }
                //                else
                //                {
                //                    paddle.Velocity = var.SecondVelocity;
                //                    paddle.DrawRectangle = var.SecondDrawRectangle;
                //                }

                //            }

                //        }
                //    }
                //}
                for (int i = 0; i < balls.Count; i++)
                {
                    if (balls[i].CollisionRectangle.Intersects(paddle.CollisionRectangle) &&
                        balls[i].DrawRectangle.Bottom <= paddle.DrawRectangle.Top + 2)
                    {
                        balls[i].Velocity *= new Vector2(1, -1);
                        ballBounceSound.Play(GameConstants.SFX_VOL, 0, 0);
                    }
                }

                #endregion

                //Bounce balls with enemy
                #region
                {
                    CollisionResolutionInfo var;
                    int elapsedSpawnDelayMilliseconds = 0;
                    elapsedSpawnDelayMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
                    for (int i = 0; i < balls.Count; i++)
                    {
                        for (int j = 0; j < enemys.Count; j++)
                        {
                            if (balls[i].Active &&
                                enemys[j].Active)
                            {

                                var = CollisionUtils.CheckCollision(elapsedSpawnDelayMilliseconds,
                                   GameConstants.WINDOW_WIDTH, GameConstants.WINDOW_HEIGHT, balls[i].Velocity,
                                   balls[i].DrawRectangle, enemys[j].Velocity, enemys[j].DrawRectangle);
                                if (var != null)
                                {
                                    if (var.FirstOutOfBounds == true)
                                    {
                                        balls[i].Active = false;
                                    }
                                    else
                                    {
                                        balls[i].Velocity = var.FirstVelocity;
                                        balls[i].DrawRectangle = var.FirstDrawRectangle;
                                    }

                                    if (var.SecondOutOfBounds == true)
                                    {
                                        enemys[j].Active = false;
                                    }
                                    else
                                    {
                                        enemys[j].Velocity = var.SecondVelocity;
                                        enemys[j].DrawRectangle = var.SecondDrawRectangle;
                                        enemys[j].Active = false;
                                        ballBounceSound.Play(GameConstants.SFX_VOL - 0.1F, -1, 0);
                                        score += 1;
                                    }

                                }


                            }
                        }
                    }

                }
                #endregion

                //Colision enemy and paddle
                #region
                foreach (Enemy enemy in enemys)
                {
                    if (paddle.CollisionRectangle.Intersects(enemy.CollisionRectangle))
                    {
                        gameState = GameState.GameOver;
                    }

                }
                #endregion

                //clen up balls and enemys
                #region
                foreach (Ball ball in balls)
                {
                    if (ball.DrawRectangle.Y > GameConstants.WINDOW_HEIGHT)
                    {
                        ball.Active = false;
                    }
                }
                for (int i = balls.Count - 1; i >= 0; i--)
                {
                    if (!balls[i].Active)
                    {
                        balls.RemoveAt(i);
                    }

                }
                for (int i = enemys.Count - 1; i >= 0; i--)
                {
                    if (!enemys[i].Active)
                    {
                        enemys.RemoveAt(i);
                    }
                }
                foreach (Enemy enemy in enemys)
                {
                    if (enemy.DrawRectangle.Y > GameConstants.WINDOW_HEIGHT)
                    {
                        enemy.Active = false;
                    }
                }
                if (gameState == GameState.Playing && balls.Count <= 0)
                {
                    gameState = GameState.GameOver;
                    crowdSound.Play(GameConstants.SFX_VOL, 0, 0);
                }
                #endregion
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.Draw(background, mainFrame, Color.White);
            spriteBatch.Draw(backgroundborder, mainFrame, Color.White);

            
            foreach (Enemy enemy in enemys)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (Ball ball in balls)
            {
                ball.Draw(spriteBatch);
            }
            if (gameState != GameState.StartGame)
            {
                paddle.Draw(spriteBatch);
                // draw score
                spriteBatch.DrawString(font1, scoreString, GameConstants.SCORE_LOCATION, Color.White);
            }
 
            
            //gameover
            if (gameState == GameState.GameOver)
            {
                spriteBatch.DrawString(gameoverfont, "GAME OVER", new Vector2(5,graphics.GraphicsDevice.Viewport.Height/3) , Color.White);
                spriteBatch.DrawString(font1, "Press space for New Game", new Vector2(graphics.GraphicsDevice.Viewport.Width / 3.6F, graphics.GraphicsDevice.Viewport.Height / 1.7F), Color.White);
            
            }
            //startgame
            if (gameState == GameState.StartGame)
            {
                spriteBatch.DrawString(gameoverfont, "ANDROIDEK", new Vector2(5, graphics.GraphicsDevice.Viewport.Height / 3), Color.White);
                spriteBatch.DrawString(font1, "Press space for New Game", new Vector2(graphics.GraphicsDevice.Viewport.Width / 3.6F, graphics.GraphicsDevice.Viewport.Height / 1.7F), Color.White);

            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
