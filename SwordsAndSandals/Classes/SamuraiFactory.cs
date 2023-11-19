using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Sprites;
using SwordsAndSandals.Stats;
using System.Collections.Generic;

namespace SwordsAndSandals.Classes
{
    public class SamuraiFactory : PlayerFactory
    {
        public override Player CreatePlayerWithoutButtons(ContentManager content, Vector2 position, SpriteEffects flip, List<Sprite> ctx, Attributes attributes, string name)
        {
            PlayerBuilder builder = new SamuraiBuilder(content)
                .SetPosition(position)
                .SetName(name)
                .SetAttributes(attributes)
                .SetDefaultAbility(flip)
                .SetAbilities(flip, ctx);
            return builder.GetPlayer();
        }

        public override Player CreatePlayerWithButtons(ContentManager content, Vector2 position, SpriteEffects flip, List<Sprite> ctx, Attributes attributes, string name)
        {
            PlayerBuilder builder = new SamuraiBuilder(content)
                .SetPosition(position)
                .SetName(name)
                .SetAttributes(attributes)
                .SetDefaultAbility(flip)
                .SetAbilities(flip, ctx)
                .SetCorrection(32)
                .SetButtons();
            return builder.GetPlayer();
        }
    }
}
