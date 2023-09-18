
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwordsAndSandals
{
    public abstract class State
    {
        protected ContentManager _content;
        protected GraphicsDevice _graphicsDevice;
        protected Game1 _game;
        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = contentManager;
        }
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void PostUpdate(GameTime gameTime);
        public abstract void Update(GameTime gameTime);
    }
}
