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
    }
}
