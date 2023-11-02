
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Music;

namespace SwordsAndSandals
{
    public abstract class State
    {
        protected GraphicsDeviceManager graphicsDevice;
        protected internal IMusic music;

        public State(GraphicsDeviceManager graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
    }
}
