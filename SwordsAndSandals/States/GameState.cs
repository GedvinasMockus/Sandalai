using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.Objects;
using SwordsAndSandals.Objects.Classes;
using SwordsAndSandals.Objects.Items.Weapons;

using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class GameState : State
    {
        //TODO refactor and optimize collisions and sprite code !!!!!!!!!!!!!!!!!!!!
        private BattleInfo battleInfo;
        public Player player { get; private set; }
        private List<Sprite> p1sprites;

        public Player opponent { get; private set; }
        private List<Sprite> p2sprites;

        private Background background;
        private List<Button> buttons;

        private int screenWidth;
        private int screenHeight;
        public GameState(GraphicsDeviceManager graphicsDevice, BattleInfo bInfo) : base(graphicsDevice)
        {
            p1sprites = new List<Sprite>();
            p2sprites = new List<Sprite>();
            battleInfo = bInfo;
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
        }

        public PlayerFactory GetPlayerFactory(string className)
        {
            switch (className)
            {
                case "Kunoichi":
                    return new KunoichiFactory();
                case "Samurai":
                    return new SamuraiFactory();
                default:
                    return new SkeletonFactory();
            }
        }

        public WeaponFactory GetPlayerWeaponFactory(string className)
        {
            switch (className)
            {
                case "Kunoichi":
                    return new KunoichiWeaponFactory();
                case "Samurai":
                    return new SamuraiWeaponFactory();
                default:
                    return new SkeletonWeaponFactory();
            }
        }

        private void LogoutButton_Click(object sender, System.EventArgs e)
        {
            ConnectionManager.Instance.Invoke("LeaveBattle");
            StateManager.Instance.ChangeState(new MenuState(graphicsDevice));
        }

        private void OnAbilityUsed(object sender, AbilityUsedEventArgs e)
        {
            ConnectionManager.Instance.Invoke("AbilityUsed", e.Name);
        }

        public override void LoadContent(ContentManager content)
        {
            PlayerFactory p1Factory = GetPlayerFactory(battleInfo.Player1.ClassName);
            PlayerFactory p2Factory = GetPlayerFactory(battleInfo.Player2.ClassName);
            WeaponFactory weaponFactory = GetPlayerWeaponFactory(battleInfo.Player1.ClassName);
            Vector2 p1Pos = new Vector2(battleInfo.Player1.Position.X * screenWidth, battleInfo.Player1.Position.Y * screenHeight);
            Vector2 p2Pos = new Vector2(battleInfo.Player2.Position.X * screenWidth, battleInfo.Player2.Position.Y * screenHeight);

            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));
            player = p1Factory.CreatePlayer(content, p1Pos, (SpriteEffects)battleInfo.Player1.Flip, true);
            //player.AddWeapons(weaponFactory, content);
            player.AbilityUsed += OnAbilityUsed;
            opponent = p2Factory.CreatePlayer(content, p2Pos, (SpriteEffects)battleInfo.Player2.Flip, false);

            Button logoutButton = new Button(content.Load<Texture2D>("Views/Button"), content.Load<SpriteFont>("Fonts/vinque"), "Leave battle", 2f, SpriteEffects.None)
            {
                Position = new Vector2(screenWidth / 8, screenHeight / 12)
            };
            logoutButton.Click += LogoutButton_Click;
            buttons = new List<Button>()
            {
                logoutButton
            };
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            opponent.Draw(spriteBatch);
            player.Draw(spriteBatch);
            foreach (var b in buttons)
            {
                b.Draw(spriteBatch);
            }
            foreach (var s1 in p1sprites)
            {
                s1.Draw(spriteBatch);
            }
            foreach (var s2 in p2sprites)
            {
                s2.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime, p1sprites);
            opponent.Update(gameTime, p2sprites);
            foreach (var b in buttons)
            {
                b.Update(gameTime);
            }
            foreach (var s1 in p1sprites)
            {
                s1.Update(gameTime);
            }
            p1sprites.RemoveAll(s => Intersects(s.Rectangle, opponent.Rectangle));
            foreach (var s2 in p2sprites)
            {
                s2.Update(gameTime);
            }
            p2sprites.RemoveAll(s => Intersects(s.Rectangle, player.Rectangle));
        }

        public override void UnloadContent()
        {

        }

        private bool Intersects(Rectangle rect1, Rectangle rect2)
        {
            return rect1.Intersects(rect2);
        }
    }
}
