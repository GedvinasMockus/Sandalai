using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Animations;
using SwordsAndSandals.Sprites;
using SwordsAndSandals.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Classes
{
    public class PlayerFactory
    {
        private AnimationFactory factory;

        public bool UseAnimationCache
        {
            get
            {
                return factory.UseCache;
            }
            set
            {
                factory.UseCache = value;
            }
        }

        private KunoichiBuilder builder1;
        private SamuraiBuilder builder2;
        private SkeletonBuilder builder3;

        public PlayerFactory(ContentManager content)
        {
            factory = new AnimationFactory();
            builder1 = new KunoichiBuilder(content, factory);
            builder2 = new SamuraiBuilder(content, factory);
            builder3 = new SkeletonBuilder(content, factory);
        }
        public Player CreatePlayerWithButtons(string className, Vector2 position, SpriteEffects flip, List<Sprite> ctx, Attributes attributes, string name)
        {
            PlayerBuilder builder = GetCorrectBuilder(className);
            return builder.SetPosition(position)
                .SetName(name)
                .SetAttributes(attributes)
                .SetDefaultAbility(flip)
                .SetAbilities(flip, ctx)
                .SetButtons()
                .GetPlayer();

        }
        public Player CreatePlayerWithoutButtons(string className, Vector2 position, SpriteEffects flip, List<Sprite> ctx, Attributes attributes, string name)
        {
            PlayerBuilder builder = GetCorrectBuilder(className);
            return builder
                .SetPosition(position)
                .SetName(name)
                .SetAttributes(attributes)
                .SetDefaultAbility(flip)
                .SetAbilities(flip, ctx)
                .GetPlayer();
        }

        private PlayerBuilder GetCorrectBuilder(string className)
        {
            switch(className)
            {
                case "Kunoichi":
                    return builder1;
                case "Samurai":
                    return builder2;
                default:
                    return builder3;
            }
        }
    }
}
