﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Animations
{
    public class SamuraiJumpAnimation : Animation
    {
        public SamuraiJumpAnimation(ContentManager content, float duration, SpriteEffects flip, bool flipChangeable) : base(duration, flip, flipChangeable)
        {
            Scale = 3.0f;
            texture = content.Load<Texture2D>("Character/Samurai/Samurai_Commander/Jump");
            totalFrames = 7;
            frameWidth = texture.Width / totalFrames;
            frameHeight = texture.Height;
        }
    }
}
