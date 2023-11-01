using Microsoft.Xna.Framework;
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
        public abstract Player CreatePlayerWithButtons(ContentManager content, Vector2 position, SpriteEffects flip, Attributes attributes, string name);
        public abstract Player CreatePlayerWithoutButtons(ContentManager content, Vector2 position, SpriteEffects flip, Attributes attributes, string name);
    }
}
