using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Animations
{
    public class AnimationShared
    {
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        public int CollisionWidth { get; private set; }
        public int CollisionHeight { get; private set; }
        public Vector2 CollisionRectPoint { get; private set; }
        public bool FlipChangeable { get; private set; }
        public int TotalFrames { get; private set; }
        public Texture2D Texture { get; private set; }

        public AnimationShared(Texture2D texture, int totalFrames, bool flipChangeable, int collisionWidth, int collisionHeight, Vector2 collisionRectPoint)
        {
            Texture = texture;
            TotalFrames = totalFrames;
            FrameWidth = Texture.Width / TotalFrames;
            FrameHeight = Texture.Height;
            FlipChangeable = flipChangeable;
            CollisionWidth = collisionWidth;
            CollisionHeight = collisionHeight;
            CollisionRectPoint = collisionRectPoint;
        }
    }
}
