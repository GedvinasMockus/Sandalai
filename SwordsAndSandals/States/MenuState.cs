using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects;
using SwordsAndSandals.States;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals
{
    public class MenuState : State
    {
        private List<Component> _components;
        private Background background;
        private IHubProxy hub;
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager, int screenWidth, int screenHeight, IHubProxy hub) : base(game, graphicsDevice, contentManager, screenWidth, screenHeight)
        {
            var buttonTexture = _content.Load<Texture2D>("Views/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(_content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"), new Vector2(0, 0));
            this.hub = hub;
            var positionX = screenWidth / 2;
            var positionY = screenHeight / 2;
            var newGameButton = new Button(buttonTexture, buttonFont, "New Game", 2f, SpriteEffects.None)
            {
                Position = new Vector2(positionX, positionY + 100),
            };
            newGameButton.Click += NewGameButton_Click;

            var settingsButton = new Button(buttonTexture, buttonFont, "Settings", 2f, SpriteEffects.None)
            {
                Position = new Vector2(positionX, positionY + 200),
            };
            settingsButton.Click += SettingButton_Click;

            var exitButton = new Button(buttonTexture, buttonFont, "Exit", 2f, SpriteEffects.None)
            {
                Position = new Vector2(positionX, positionY + 300),
            };
            exitButton.Click += ExitButton_Click;
            _components = new List<Component>() {
                newGameButton,
                settingsButton,
                exitButton
            };

        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new SettingsState(_game, _graphicsDevice, _content, _screenWidth, _screenHeight, hub));
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            //_game.ChangeState(new GameState(_game, _graphicsDevice, _content, _screenWidth, _screenHeight));

            _game.ChangeState(new LoadingScreenState(_game, _graphicsDevice, _content, _screenWidth, _screenHeight, hub));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }

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
