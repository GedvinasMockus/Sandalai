
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Mediator;

namespace SwordsAndSandals.UI
{
    public abstract class Component
    {
        protected IMediator mediator;

        public Component(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
