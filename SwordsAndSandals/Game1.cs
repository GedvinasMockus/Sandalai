
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SwordsAndSandals.JsonTemplates;
using SwordsAndSandals.Objects;
using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Utils;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SwordsAndSandals
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int _screenHeight = 1080;
        private const int _screenWidth = 1920;
        private const int _groundLevel = 11 * _screenHeight / 17;

        private Player player;
        private Background background;
        private Dictionary<string, Texture2D> icons;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            icons = new Dictionary<string, Texture2D>
            {
                {"Jump", Content.Load<Texture2D>("Icons/Icon_02")},
                {"Sleep", Content.Load<Texture2D>("Icons/Icon_05")},
                {"Heal", Content.Load<Texture2D>("Icons/Icon_11")},
                {"Melee_attack", Content.Load<Texture2D>("Icons/Icon_15")},
                {"Shield", Content.Load<Texture2D>("Icons/Icon_18")},
                {"Run", Content.Load<Texture2D>("Icons/Icon_29")},
                {"Shoot", Content.Load<Texture2D>("Icons/Icon_34")},

            };
            player = new Player(Content.Load<Texture2D>("Character/Ninja/Kunoichi/idle"), new Vector2(_screenWidth / 6, _groundLevel), 3.0f, 128, 128);
            player.AddAbility("Run_right", new Run(300f, 100f), icons["Run"], 2.0f, SpriteEffects.None);
            player.AddAbility("Run_left", new Run(300f, -100f), icons["Run"], 2.0f, SpriteEffects.FlipHorizontally);
            player.AddAbility("Melee_attack_right", new Ability(), icons["Melee_attack"], 2.0f, SpriteEffects.None);
            player.AddAbility("Melee_attack_left", new Ability(), icons["Melee_attack"], 2.0f, SpriteEffects.FlipHorizontally);
            player.AddAbility("Jump", new Ability(), icons["Jump"], 2.0f, SpriteEffects.None);
            player.AddAbility("Sleep", new Ability(), icons["Sleep"], 2.0f, SpriteEffects.None);
            player.AddAbility("Heal", new Ability(), icons["Heal"], 2.0f, SpriteEffects.None);
            player.AddAbility("Shield", new Ability(), icons["Shield"], 2.0f, SpriteEffects.None);
            background = new Background(Content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"), new Vector2(0, 0));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            background.Draw(_spriteBatch);
            player.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}