
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SwordsAndSandals.Objects;

namespace SwordsAndSandals
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int _screenHeight = 1080;
        private const int _screenWidth = 1920;
        private const int _groundLevel = 9 * _screenHeight / 17;

        private Player player;
        private Player player2;
        private Background background;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(Content.Load<Texture2D>("Character/Ninja/Kunoichi/idle"), new Vector2(_screenWidth / 6, _groundLevel), 3.0f, 128, 128);
            player2 = new Player(Content.Load<Texture2D>("Character/Ninja/Ninja_Monk/Idle"), new Vector2(5 * _screenWidth / 6, _groundLevel), 3.0f, 96, 96);
            background = new Background(Content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"), new Vector2(0, 0));
            //Debug.WriteLine("Height: " + player.texture.Height);
            //Debug.WriteLine("Width: " + player.texture.Width);
            //Debug.WriteLine("Position: " + player.position.Y);
            //Debug.WriteLine("Position: " + player.position.X);
            //Debug.WriteLine("Height2: " + player2.texture.Height);
            //Debug.WriteLine("Width2: " + player2.texture.Width);
            //Debug.WriteLine("Position2: " + player2.position.Y);
            //Debug.WriteLine("Position2: " + player2.position.X);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            background.Draw(_spriteBatch);
            player.Draw(_spriteBatch);
            player2.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}