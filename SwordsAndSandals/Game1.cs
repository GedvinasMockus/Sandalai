using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SwordsAndSandals.JsonTemplates;
using SwordsAndSandals.Objects;
using SwordsAndSandals.Utils;

namespace SwordsAndSandals
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int _screenHeight = 1080;
        private const int _screenWidth = 1920;
        private const int _groundLevel = 11 * _screenHeight / 17;

        //private Player player1;
        private Player player2;
        private Background background;
        private Dictionary<string, Texture2D> icons;


        Test _playerMoveTest;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _playerMoveTest = new Test();
            _playerMoveTest.Connect();
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            List<IconTemplate> templates = JsonUtils.ReadJson<List<IconTemplate>>("../../../Content/Icons/Icons.json");
            icons = new Dictionary<string, Texture2D>();
            foreach (var i in templates)
            {
                icons.Add(i.Name, Content.Load<Texture2D>(i.Path));
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
            //player1 = new Player(Content.Load<Texture2D>("Character/Ninja/Kunoichi/idle"), new Vector2(_screenWidth / 6, _groundLevel), 3.0f, 128, 128, availableAbilities1);
            _playerMoveTest.LoadContent(new Player(Content.Load<Texture2D>("Character/Ninja/Kunoichi/idle"), new Vector2(_screenWidth / 6, _groundLevel), 3.0f, 128, 128, availableAbilities1, 95));
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
            player2 = new Player(Content.Load<Texture2D>("Character/Ninja/Ninja_Monk/Idle"), new Vector2(5 * _screenWidth / 6, _groundLevel), 3.0f, 96, 96, availableAbilities2, 58);
            availableAbilities2 = null;
            background = new Background(Content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"), new Vector2(0, 0));
            //Debug.WriteLine("Height: " + player.texture.Height);
            //Debug.WriteLine("Width: " + player.texture.Width);
            //Debug.WriteLine("Position: " + player.position.Y);
            //Debug.WriteLine("Position: " + player.position.X);
            //Debug.WriteLine("Height2: " + player2.texture.Height);
            //Debug.WriteLine("Width2: " + player2.texture.Width);
            //Debug.WriteLine("Position2: " + player2.position.Y);
            //Debug.WriteLine("Position2: " + player2.position.X);



        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //player1.Update(gameTime);
            player2.Update(gameTime);
            // TODO: Add your update logic here
            _playerMoveTest.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            background.Draw(_spriteBatch);
            //player1.Draw(_spriteBatch);
            player2.Draw(_spriteBatch);
            _playerMoveTest.Draw(_spriteBatch);




            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}