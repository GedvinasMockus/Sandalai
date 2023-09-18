using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects;
using SwordsAndSandals.States;

namespace SwordsAndSandals
{
    public class MenuState : State
    {
        private List<Component> _components;
        private int _screenWidth, _screenHeight;
        private Background background;
        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager, int screenWidth, int screenHeight) : base(game, graphicsDevice, contentManager)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            var buttonTexture = _content.Load<Texture2D>("Views/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(_content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"), new Vector2(0, 0));
            var positionX = screenWidth / 2;
            var positionY = screenHeight / 2;
            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(positionX, positionY + 100),
                Text = "New Game",
            };
            newGameButton.Click += NewGameButton_Click;

            var settingsButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(positionX, positionY + 200),
                Text = "Settings",
            };
            settingsButton.Click += SettingButton_Click;

            var exitButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(positionX, positionY + 300),
                Text = "Exit",
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
            _game.ChangeState(new SettingsState(_game, _graphicsDevice, _content, _screenWidth, _screenHeight));
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content, _screenWidth, _screenHeight));
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
