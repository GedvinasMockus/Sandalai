using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Objects;
using SwordsAndSandals.Objects.Abilities;
using SwordsAndSandals.Objects.Classes;
using SwordsAndSandals.Objects.Items.Weapons;
using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class GameState : State
    {
        public Player player { get; private set; }
        private PlayerFactory p1Factory;
        private Vector2 playerSpawnPos;
        private int playerFlip;

        private WeaponFactory weaponFactory;

        public Player opponent { get; private set; }
        private PlayerFactory p2Factory;
        private Vector2 opponentSpawnPos;
        private int opponentFlip;

        private Background background;
        private List<Button> buttons;
        private IHubProxy hub;

        private int screenWidth;
        private int screenHeight;
        public GameState(GraphicsDeviceManager graphicsDevice, IHubProxy hub, Vector2 pos1, Vector2 pos2, int flip1, int flip2, string className1, string className2) : base(graphicsDevice)
        {
            this.hub = hub;
            playerSpawnPos = pos1;
            playerFlip = flip1;
            opponentSpawnPos = pos2;
            opponentFlip = flip2;
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
            p1Factory = GetPlayerFactory(className1);
            p2Factory = GetPlayerFactory(className2);
            weaponFactory = GetPlayerWeaponFactory(className1);
        }

        public PlayerFactory GetPlayerFactory(string className)
        {
            switch(className)
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
            hub.Invoke("LeaveBattle");
            StateManager.Instance.ChangeState(new MenuState(_graphicsDevice, hub));
        }

        private void OnAbilityUsed(object sender, AbilityUsedEventArgs e)
        {
            hub.Invoke("AbilityUsed", e.Name);
        }

        public override void LoadContent(ContentManager content)
        {
            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));
            player = p1Factory.CreatePlayer(content, playerSpawnPos, (SpriteEffects)playerFlip, true);
            player.AddWeapons(weaponFactory, content);
            player.AbilityUsed += OnAbilityUsed;
            opponent = p2Factory.CreatePlayer(content, opponentSpawnPos, (SpriteEffects)opponentFlip, false);

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
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            opponent.Update(gameTime);
            foreach (var b in buttons)
            {
                b.Update(gameTime);
            }
        }

        public override void UnloadContent()
        {
            
        }
    }
}
