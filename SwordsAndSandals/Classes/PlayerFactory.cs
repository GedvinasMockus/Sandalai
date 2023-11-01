﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Classes
{
    public abstract class PlayerFactory
    {
        public abstract Player CreatePlayer(ContentManager content, Vector2 position, SpriteEffects flip, Attributes attributes, bool setButtons);
    }
}