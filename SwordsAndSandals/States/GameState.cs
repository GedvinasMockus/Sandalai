using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects;
using SwordsAndSandals.Objects.Abilities;

using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class GameState : State
    {
        private int _groundLevel;
        private Player player;
        private Player playerOpponent;
        private Background background;
        private Dictionary<string, Texture2D> icons;
        private List<Component> _components;
        private IHubProxy hub;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager, int screenWidth, int screenHeight, Vector2 pos1, Vector2 pos2, IHubProxy hub, int flip1, int flip2) : base(game, graphicsDevice, contentManager, screenWidth, screenHeight)
        {
            this.hub = hub;
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
            player = new Player(_content.Load<Texture2D>("Character/Ninja/Kunoichi/idle"), pos1, 3.0f, 95, (SpriteEffects)flip1);
            player.AddAbility("Jump", new Ability(), icons["Jump"], 2.0f, SpriteEffects.None);
            player.AddAbility("Melee_attack_left", new Ability(), icons["Melee_attack"], 2.0f, SpriteEffects.FlipHorizontally);
            player.AddAbility("Run_left", new Run(300f, -100f), icons["Run"], 2.0f, SpriteEffects.FlipHorizontally);
            player.AddAbility("Shield", new Ability(), icons["Shield"], 2.0f, SpriteEffects.None);
            player.AddAbility("Sleep", new Ability(), icons["Sleep"], 2.0f, SpriteEffects.None);
            player.AddAbility("Heal", new Ability(), icons["Heal"], 2.0f, SpriteEffects.None);
            player.AddAbility("Run_right", new Run(300f, 100f), icons["Run"], 2.0f, SpriteEffects.None);
            player.AddAbility("Melee_attack_right", new Ability(), icons["Melee_attack"], 2.0f, SpriteEffects.None);

            playerOpponent = new Player(_content.Load<Texture2D>("Character/Ninja/Kunoichi/idle"), pos2, 3.0f, 95, (SpriteEffects)flip2);
            var positionX = screenWidth / 8;
            var positionY = screenHeight / 12;
            var buttonTexture = _content.Load<Texture2D>("Views/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/vinque");
            var logoutButton = new Button(buttonTexture, buttonFont, "Logout", 2f, SpriteEffects.None)
            {
                Position = new Vector2(positionX, positionY),
            };
            logoutButton.Click += LogoutButton_Click;
            _components = new List<Component>() {
                logoutButton
            };

        }

        private void LogoutButton_Click(object sender, System.EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content, _screenWidth, _screenHeight, hub));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            player.Draw(gameTime, spriteBatch);
            playerOpponent.Draw(gameTime, spriteBatch);
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
            player.Update(gameTime);
            playerOpponent.Update(gameTime);
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }
    }
}
