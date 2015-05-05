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
    public class Ball : Objekt
    {
        public Ball(ContentManager contentManager, string spriteName, int x, int y, Vector2 velocity,
            SoundEffect shootSound) : base(contentManager, spriteName, x, y, velocity,shootSound)
            {

            }

         /// <summary>
         /// Updates the ball's location based 
         /// </summary>
         /// <param name="gameTime">game time</param>
         public void Update(GameTime gameTime)
         {
             // should only respond to input if it still has health
             if (active == true)
             {
                 // move burger using keyoard
               
                 drawRectangle.X += (int)velocity.X;
                 drawRectangle.Y += (int)velocity.Y;


                 // clamp objekt in window
                 ClampInWindow();



             }
         }

    }
}
