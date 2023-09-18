using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.JsonTemplates;
using SwordsAndSandals.Objects;
using SwordsAndSandals.Utils;

namespace SwordsAndSandals.States
{
    public class GameState : State
    {
        private int _groundLevel;
        private int _screenWidth, _screenHeight;
        private Player player1;
        private Player player2;
        private Background background;
        private Dictionary<string, Texture2D> icons;
        private List<Component> _components;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager contentManager, int screenWidth, int screenHeight) : base(game, graphicsDevice, contentManager)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _groundLevel = 11 * _screenHeight / 17;
            List<IconTemplate> templates = JsonUtils.ReadJson<List<IconTemplate>>("../../../Content/Icons/Icons.json");
            background = new Background(_content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"), new Vector2(0, 0));
            icons = new Dictionary<string, Texture2D>();
            foreach (var i in templates)
            {
                icons.Add(i.Name, _content.Load<Texture2D>(i.Path));
            }
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
            //_playerMoveTest.LoadContent(new Player(Content.Load<Texture2D>("Character/Ninja/Kunoichi/idle"), new Vector2(_screenWidth / 6, _groundLevel), 3.0f, availableAbilities1, 95));
            availableAbilities1 = null;
            Dictionary<string, Ability> availableAbilities2 = new Dictionary<string, Ability>()
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
            player2 = new Player(_content.Load<Texture2D>("Character/Ninja/Ninja_Monk/Idle"), new Vector2(5 * _screenWidth / 6, _groundLevel), 3.0f, availableAbilities2, 58);
            availableAbilities2 = null;
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
            player2.Draw(spriteBatch);
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
            player2.Update(gameTime);
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }
    }
}
