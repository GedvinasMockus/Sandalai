using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Classes
{
    public abstract class PlayerFactory
    {
        public abstract Player CreatePlayer(ContentManager content, Vector2 position, SpriteEffects flip, bool addButtons);
    }
}
