﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Animations
{
    public class KunoichiIdleAnimation : Animation
    {
        public KunoichiIdleAnimation(ContentManager content, float duration, SpriteEffects flip, bool flipChangeable) : base(duration, flip, flipChangeable)
        {
            Scale = 3.0f;
            texture = content.Load<Texture2D>("Character/Ninja/Kunoichi/Idle");
            totalFrames = 9;
            frameWidth = texture.Width / totalFrames;
            frameHeight = texture.Height;
        }
    }
}