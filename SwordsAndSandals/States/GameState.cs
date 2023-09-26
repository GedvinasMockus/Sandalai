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
        public Player player { get; private set; }
        private Vector2 playerSpawnPos;
        private int playerFlip;

        public Player opponent { get; private set; }
        private Vector2 opponentSpawnPos;
        private int opponentFlip;

        private Background background;
        private List<Button> buttons;
        private IHubProxy hub;

        private int screenWidth;
        private int screenHeight;
        public GameState(GraphicsDeviceManager graphicsDevice, IHubProxy hub, Vector2 pos1, Vector2 pos2, int flip1, int flip2) : base(graphicsDevice)
        {
            this.hub = hub;
            playerSpawnPos = pos1;
            playerFlip = flip1;
            opponentSpawnPos = pos2;
            opponentFlip = flip2;
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
        }

        private void LogoutButton_Click(object sender, System.EventArgs e)
        {
            StateManager.Instance.ChangeState(new MenuState(_graphicsDevice, hub));
        }

        private void OnAbilityUsed(object sender, AbilityUsedEventArgs e)
        {
            hub.Invoke("AbilityUsed", e.Name);
        }

        public override void LoadContent(ContentManager content)
        {
            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));
            player = new Player(content.Load<Texture2D>("Character/Ninja/Kunoichi/idle"), playerSpawnPos, 3.0f, 95, (SpriteEffects)playerFlip);
            player.AddAbility("Jump", new Ability());
            player.AddAbilityButton("Jump", content.Load<Texture2D>("Icons/Icon_02"), 2.0f, SpriteEffects.None);
            player.AddAbility("Melee_attack_left", new Ability());
            player.AddAbilityButton("Melee_attack_left", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.FlipHorizontally);
            player.AddAbility("Run_left", new Run(300f, -100f));
            player.AddAbilityButton("Run_left", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.FlipHorizontally);
            player.AddAbility("Shield", new Ability());
            player.AddAbilityButton("Shield", content.Load<Texture2D>("Icons/Icon_18"), 2.0f, SpriteEffects.None);
            player.AddAbility("Sleep", new Ability());
            player.AddAbilityButton("Sleep", content.Load<Texture2D>("Icons/Icon_05"), 2.0f, SpriteEffects.None);
            player.AddAbility("Heal", new Ability());
            player.AddAbilityButton("Heal", content.Load<Texture2D>("Icons/Icon_11"), 2.0f, SpriteEffects.None);
            player.AddAbility("Run_right", new Run(300f, 100f));
            player.AddAbilityButton("Run_right", content.Load<Texture2D>("Icons/Icon_29"), 2.0f, SpriteEffects.None);
            player.AddAbility("Melee_attack_right", new Ability());
            player.AddAbilityButton("Melee_attack_right", content.Load<Texture2D>("Icons/Icon_15"), 2.0f, SpriteEffects.None);
            player.AbilityUsed += OnAbilityUsed;
            opponent = new Player(content.Load<Texture2D>("Character/Ninja/Kunoichi/idle"), opponentSpawnPos, 3.0f, 95, (SpriteEffects)opponentFlip);
            opponent.AddAbility("jump", new Ability());
            opponent.AddAbility("Melee_attack_left", new Ability());
            opponent.AddAbility("Run_left", new Run(300f, -100f));
            opponent.AddAbility("Shield", new Ability());
            opponent.AddAbility("Sleep", new Ability());
            opponent.AddAbility("Heal", new Ability());
            opponent.AddAbility("Run_right", new Run(300f, 100f));
            opponent.AddAbility("Melee_attack_right", new Ability());

            Button logoutButton = new Button(content.Load<Texture2D>("Views/Button"), content.Load<SpriteFont>("Fonts/vinque"), "Logout", 2f, SpriteEffects.None)
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
            player.Draw(spriteBatch);
            opponent.Draw(spriteBatch);
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
