using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Diagnostics;

namespace SwordsAndSandals
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int _screenHeight = 1080;
        private const int _screenWidth = 1920;
        private State _currentState;
        private State _nextState;
        private HubConnection _connection;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _connection = new HubConnection("http://192.168.1.182:8081");
            IHubProxy lobbyHubProxy = _connection.CreateHubProxy("MainHub");
            lobbyHubProxy.On<string>("Send", (message) =>
            {
                Debug.WriteLine(message);
            });
            _connection.Start().Wait();
            lobbyHubProxy.Invoke("Send", "Bananas");
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content, _screenWidth, _screenHeight);
        }

        protected override void Update(GameTime gameTime)
        {

            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }
            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);
            _currentState.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }
        public void ChangeState(State state)
        {
            _nextState = state;
        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            _connection.Stop();
            base.OnExiting(sender, args);
        }
    }
}