using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.States;

using System;

namespace SwordsAndSandals
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int _screenHeight = 1080;
        private const int _screenWidth = 1920;
        private HubConnection _connection;
        private IHubProxy lobbyHubProxy;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _connection = new HubConnection("http://localhost:8081");
            lobbyHubProxy = _connection.CreateHubProxy("MainHub");
            lobbyHubProxy.On<System.Numerics.Vector2, System.Numerics.Vector2, int, int>("FoundOpponent", (pos1, pos2, flip1, flip2) =>
            {
                StateManager.Instance.ChangeState(new GameState(_graphics, lobbyHubProxy, new Vector2(pos1.X * _screenWidth, pos1.Y * _screenHeight), new Vector2(pos2.X * _screenWidth, pos2.Y * _screenHeight), flip1, flip2));
            });
            _connection.Start().Wait();
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            StateManager.Instance.SetContentManager(Content);
            StateManager.Instance.ChangeState(new MenuState(_graphics, lobbyHubProxy));
        }

        protected override void Update(GameTime gameTime)
        {
            if (StateManager.Instance.NotInAState()) this.Exit();
            StateManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);
            StateManager.Instance.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            _connection.Stop();
            base.OnExiting(sender, args);
        }
    }
}