using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects;

namespace SwordsAndSandals
{
    public class SettingsState : State
    {
        private List<Component> _components;
        private int _screenWidth, _screenHeight;
        private Background background;
        private TextBox textBox;
        public SettingsState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager, int screenWidth, int screenHeight) : base(game, graphicsDevice, contentManager)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            var buttonTexture = _content.Load<Texture2D>("Views/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/vinque");
            background = new Background(_content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"), new Vector2(0, 0));
            textBox = new TextBox(buttonFont)
            {
                Position = new Vector2(screenWidth / 2, screenHeight / 8),
                Text = "Edit settings",
                TextSize = 2f,
                PenColour = Color.Orange,
                OutlineColor = Color.Black
            };
            var backButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(screenWidth / 6, 7 * screenHeight / 8),
                Text = "Back",
            };
            backButton.Click += BackButton_Click;


            _components = new List<Component>() {
                backButton
            };
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content, _screenWidth, _screenHeight));
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
