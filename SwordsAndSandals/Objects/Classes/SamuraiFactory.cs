using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Classes
{
    public class SamuraiFactory : PlayerFactory
    {
        public override Player CreatePlayer(ContentManager content, Vector2 position, SpriteEffects flip, bool addButtons)
        {
            Animation anim = new SamuraiIdleAnimation(content, 0.1f, flip);
            Player p = new Samurai(anim, position);
            p.LoadStartInfo(content, flip);
            if (addButtons) p.LoadButtons(content);
            return p;
        }
    }
}
