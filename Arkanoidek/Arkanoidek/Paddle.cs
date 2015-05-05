using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoidek
{
    public class Paddle : Objekt
    {
        public Paddle(ContentManager contentManager, string spriteName, int x, int y, Vector2 velocity,
            SoundEffect shootSound) : base(contentManager, spriteName, x, y, velocity, shootSound)
            {

            }

        /// <summary>
        /// Updates the paddle location based on mouse. 
        /// </summary>
        /// <param name="gameTime">game time</param>
        /// <param name="mouse">the current state of the mouse</param>
        /// <param name="keyboard">the current state of the keyboard</param>
        public void Update(GameTime gameTime, MouseState mouse, KeyboardState keyboard)
        {
            // paddle should only respond to input if it still has health
            if (active == true)
            {
                // move using mouse
                //drawRectangle.X = mouse.X - drawRectangle.Width / 2;
                //drawRectangle.Y = mouse.Y - drawRectangle.Height / 2;

                // move using keyoard
                if (keyboard.IsKeyDown(Keys.Left))
                {
                    velocity.X = - GameConstants.PADDLE_MOVEMENT_AMOUNT;
                }
                else if (keyboard.IsKeyDown(Keys.Right))
                {
                    velocity.X = GameConstants.PADDLE_MOVEMENT_AMOUNT;
                }
                else
                    velocity.X = 0;

                drawRectangle.X += (int)velocity.X;
                drawRectangle.Y += (int)velocity.Y;
               
                //Clamp in Window;
                ClampInWindow();


            }
        }

    }
}
