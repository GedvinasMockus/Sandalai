using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects;

using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class GameState : State
    {
        private int _groundLevel;
        private Player player1;
        private Background background;
        private Dictionary<string, Texture2D> icons;
        private List<Component> _components;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager, int screenWidth, int screenHeight) : base(game, graphicsDevice, contentManager, screenWidth, screenHeight)
        {
            _groundLevel = 11 * _screenHeight / 17;
            background = new Background(_content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"), new Vector2(0, 0));
            icons = new Dictionary<string, Texture2D>
            {
                {"Jump", _content.Load<Texture2D>("Icons/Icon_02")},
                {"Sleep", _content.Load<Texture2D>("Icons/Icon_05")},
                {"Heal", _content.Load<Texture2D>("Icons/Icon_11")},
                {"Melee_attack", _content.Load<Texture2D>("Icons/Icon_15")},
                {"Shield", _content.Load<Texture2D>("Icons/Icon_18")},
                {"Run", _content.Load<Texture2D>("Icons/Icon_29")},
                {"Shoot", _content.Load<Texture2D>("Icons/Icon_34")},
            };
            Dictionary<string, Ability> availableAbilities1 = new Dictionary<string, Ability>()
            {
                { "Jump", new Ability(icons["Jump"], 1.5f) },
                { "Melee_attack_left", new Ability(icons["Melee_attack"], 1.5f, SpriteEffects.FlipHorizontally) },
                { "Run_left", new Ability(icons["Run"], 1.5f ,SpriteEffects.FlipHorizontally) },
                { "Shield", new Ability(icons["Shield"], 1.5f) },
                { "Sleep", new Ability(icons["Sleep"], 1.5f) },
                { "Heal", new Ability(icons["Heal"], 1.5f) },
                { "Run_right", new Ability(icons["Run"], 1.5f) },
                { "Melee_attack_right", new Ability(icons["Melee_attack"], 1.5f) },
            };
            player1 = new Player(_content.Load<Texture2D>("Character/Ninja/Kunoichi/idle"), new Vector2(_screenWidth / 6, _groundLevel), 3.0f, availableAbilities1, 95);
            availableAbilities1 = null;
            var positionX = screenWidth / 8;
            var positionY = screenHeight / 12;
            var buttonTexture = _content.Load<Texture2D>("Views/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/vinque");
            var logoutButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(positionX, positionY),
                Text = "Logout",
            };
            logoutButton.Click += LogoutButton_Click;
            _components = new List<Component>() {
                logoutButton
            };
        }

        private void LogoutButton_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content, _screenWidth, _screenHeight));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            player1.Draw(spriteBatch);
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
            player1.Update(gameTime);
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }
    }
}
