using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class LoadingScreenState : State
    {
        private List<Component> _components;
        private Background background;
        private TextBox textBox;
        private Spinner spinner;
        private IHubProxy hub;
        public LoadingScreenState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager, int screenWidth, int screenHeight, IHubProxy hub) : base(game, graphicsDevice, contentManager, screenWidth, screenHeight)
        {
            this.hub = hub;
            hub.Invoke("AddToLobby");
            hub.Invoke("FindOpponent");
            var buttonTexture = _content.Load<Texture2D>("Views/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/vinque");

            background = new Background(_content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"), new Vector2(0, 0));
            var positionX = screenWidth / 2;
            var positionY = screenHeight / 2;
            spinner = new Spinner(_content.Load<Texture2D>("Objects/Gear"), Color.DarkOrange, new Vector2(positionX, screenHeight / 3), 1f, 1f);
            var leaveLobby = new Button(buttonTexture, buttonFont, "Leave lobby", 2f, SpriteEffects.None)
            {
                Position = new Vector2(positionX, positionY + 100),
            };
            leaveLobby.Click += LeaveLobby_Click;
            _components = new List<Component>() {
                leaveLobby,
                spinner
            };
            textBox = new TextBox(buttonFont)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 8),
                Text = "Waiting for opponent",
                TextSize = 2f,
                PenColour = Color.Orange,
                OutlineColor = Color.Black
            };
        }

        private void LeaveLobby_Click(object sender, EventArgs e)
        {
            hub.Invoke("RemoveFromLobby");
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content, _screenWidth, _screenHeight, hub));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            textBox.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }
    }
}
