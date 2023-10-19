using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Animations;
using SwordsAndSandals.Objects.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Objects.Classes
{
    public class KunoichiFactory : PlayerFactory
    {
        public override Player CreatePlayer(ContentManager content, Vector2 position, SpriteEffects flip, bool setButtons)
        {
            PlayerBuilder builder = new KunoichiBuilder(content).SetPosition(position).SetAttributes().SetDefaultAbility(flip).SetAbilities(flip);
            if (setButtons) builder.SetCorrection(32).SetButtons();
            return builder.GetPlayer();
        }
    }
}
