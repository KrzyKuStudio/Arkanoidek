using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Arkanoidek
{
    public static class GameConstants
    {
        // resolution
        public const int WINDOW_WIDTH = 800;
        public const int WINDOW_HEIGHT = 600;

        public const int PADDLE_MOVEMENT_AMOUNT = 10;

        // display support
        const int DISPLAY_OFFSET = 15;
        public const string SCORE_PREFIX = "Score: ";
        public static readonly Vector2 SCORE_LOCATION =
            new Vector2(DISPLAY_OFFSET, DISPLAY_OFFSET);
        public const string HEALTH_PREFIX = "Lives: ";
        public static readonly Vector2 HEALTH_LOCATION =
            new Vector2(DISPLAY_OFFSET, 2 * DISPLAY_OFFSET);

        public const int PADDLE_INITIAL_HEALTH = 3;

        public const float CLOUD_CORNER = 0.11F;
        public const int BRICKS = 24;
        public const int ROWS = 3;

        public const float MUSIC_VOL = 0.3F;
        public const float SFX_VOL = 0.4F;
    }
}
