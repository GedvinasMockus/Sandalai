
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
        protected int _screenWidth, _screenHeight;
        private Game1 game;
        private GraphicsDevice graphicsDevice;
        private ContentManager contentManager;

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager, int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = contentManager;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void PostUpdate(GameTime gameTime);
        public abstract void Update(GameTime gameTime);
    }
}
