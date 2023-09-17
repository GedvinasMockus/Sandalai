
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SwordsAndSandals.Objects;

namespace SwordsAndSandals
{
    public class Test
    {
        private readonly HubConnection _connection;
        private IHubProxy _playerMoveHub;
        private Player _texture;
        public Test()
        {
            _connection = new HubConnection("http://192.168.1.182:8081");
        }

        public void Connect()
        {
            _playerMoveHub = _connection.CreateHubProxy("playerMoveHub");
            _playerMoveHub.On<int, int>("updateState", (x, y) =>
            {
                _texture.position = new Vector2(x, y);
            });
            _connection.Start();
        }

        public void LoadContent(Player player)
        {
            _texture = player;
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                _playerMoveHub.Invoke("Move", 0, 1);
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _playerMoveHub.Invoke("Move", 0, -1);
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                _playerMoveHub.Invoke("Move", 1, 0);
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _playerMoveHub.Invoke("Move", -1, 0);
            }
            _texture.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            _texture.Draw(spriteBatch);
        }

    }
}
