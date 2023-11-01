using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Stats;

namespace SwordsAndSandals.Classes
{
    public class KunoichiFactory : PlayerFactory
    {
        public override Player CreatePlayer(ContentManager content, Vector2 position, SpriteEffects flip, Attributes attributes, bool setButtons)
        {
            PlayerBuilder builder = new KunoichiBuilder(content).SetPosition(position).SetAttributes(attributes).SetDefaultAbility(flip).SetAbilities(flip);
            if (setButtons) builder.SetCorrection(32).SetButtons();
            return builder.GetPlayer();
        }
    }
}
