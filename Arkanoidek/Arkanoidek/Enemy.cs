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
    public class Enemy : Objekt
    {
        public Enemy(ContentManager contentManager, string spriteName, int x, int y, Vector2 velocity,
            SoundEffect shootSound) : base(contentManager, spriteName, x, y, velocity,shootSound)
            {

            }

        /// <summary>
        /// Bounces the off the top and bottom window borders if necessary
        /// </summary>
        private void BounceTopBottom()
        {
            if (drawRectangle.Y < 0)
            {
                // bounce off top
                drawRectangle.Y = 0;
                velocity.Y *= -1;

            }
            else if ((drawRectangle.Y + drawRectangle.Height) > GameConstants.WINDOW_HEIGHT)
            {
                // bounce off bottom
                drawRectangle.Y = GameConstants.WINDOW_HEIGHT - drawRectangle.Height;
                velocity.Y *= -1;

            }
        }
        /// <summary>
        /// Bounces  off the left and right window borders if necessary
        /// </summary>
        private void BounceLeftRight()
        {
            if (drawRectangle.X < 0)
            {
                // bounc off left
                drawRectangle.X = 0;
                velocity.X *= -1;
                drawRectangle.Y += 60;

            }
            else if ((drawRectangle.X + drawRectangle.Width) > GameConstants.WINDOW_WIDTH)
            {
                // bounce off right
                drawRectangle.X = GameConstants.WINDOW_WIDTH - drawRectangle.Width;
                velocity.X *= -1;
                drawRectangle.Y += 60;

            }
        }

        public void Update(GameTime gameTime)
        {

            if (Active == true)
            {
                drawRectangle.X += (int)velocity.X;
                drawRectangle.Y += (int)velocity.Y;



            }
            BounceLeftRight();
            //BounceTopBottom();
        }
        
        
    }
}
