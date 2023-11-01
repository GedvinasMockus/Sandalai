using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Stats;

namespace SwordsAndSandals.Classes
{
    public class SamuraiFactory : PlayerFactory
    {
        public override Player CreatePlayerWithoutButtons(ContentManager content, Vector2 position, SpriteEffects flip, Attributes attributes, string name)
        {
            PlayerBuilder builder = new SamuraiBuilder(content)
                .SetPosition(position)
                .SetName(name)
                .SetAttributes(attributes)
                .SetDefaultAbility(flip)
                .SetAbilities(flip);
            return builder.GetPlayer();
        }

        public override Player CreatePlayerWithButtons(ContentManager content, Vector2 position, SpriteEffects flip, Attributes attributes, string name)
        {
            PlayerBuilder builder = new SamuraiBuilder(content)
                .SetPosition(position)
                .SetName(name)
                .SetAttributes(attributes)
                .SetDefaultAbility(flip)
                .SetAbilities(flip)
                .SetCorrection(32)
                .SetButtons();
            return builder.GetPlayer();
        }
    }
}
