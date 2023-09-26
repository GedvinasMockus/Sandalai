
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SwordsAndSandals
{
    public abstract class State
    {
        protected GraphicsDeviceManager _graphicsDevice;

        public State(GraphicsDeviceManager graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
