using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SwordsAndSandals.Classes;
using SwordsAndSandals.Command;
using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.Items;
using SwordsAndSandals.Sprites;
using SwordsAndSandals.Stats;
using SwordsAndSandals.UI;
using System;
using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class SpectateState : State
    {
        private int screenWidth;
        private int screenHeight;

        private BattleInfo battleInfo;
        private bool turnDone;
        public event EventHandler BattleUpdateNeeded;
        public bool BattleInfoAvailable { get; set; }

        private Background background;
        private List<Button> buttons;

        private Player player;
        private List<Sprite> p1sprites;

        private Player opponent;
        private List<Sprite> p2sprites;
        public SpectateState(GraphicsDeviceManager graphicsDevice, BattleInfo bInfo) : base(graphicsDevice)
        {
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
            battleInfo = bInfo;
            turnDone = false;
            BattleInfoAvailable = false;
            p1sprites = new List<Sprite>();
            p2sprites = new List<Sprite>();
        }
        public override void LoadContent(ContentManager content)
        {
            WeaponFactory weaponFactory = GetPlayerWeaponFactory(battleInfo.Player1.ClassName);
            Vector2 p1Pos = new Vector2(battleInfo.Player1.Position.X * screenWidth, battleInfo.Player1.Position.Y * screenHeight);
            Vector2 p2Pos = new Vector2(battleInfo.Player2.Position.X * screenWidth, battleInfo.Player2.Position.Y * screenHeight);
            ITarget target = new PlayerAdapter();

            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));
            //p1Weapons = new List<Weapon>()
            //{
            //    p1weaponFactory.CreateMeleeWeapon(content, new Vector2(32,32)),
            //    p1weaponFactory.CreateRangedWeapon(content, new Vector2(32,96)),
            //    p1weaponFactory.CreateShieldWeapon(content, new Vector2(32, 160))
            //};
            SpriteEffects p1flip;
            SpriteEffects p2flip;
            DeterminePlayerDirection(p1Pos.X, p2Pos.X, out p1flip, out p2flip);

            Attributes p1Attributes = new Attributes()
            {
                MaxHealth = battleInfo.Player1.BaseAttributes.MaxHealth,
                CurrHealth = battleInfo.Player1.BaseAttributes.CurrHealth,
                BaseDistance = battleInfo.Player1.BaseAttributes.BaseDistance * screenWidth,
                ArmourRating = battleInfo.Player1.BaseAttributes.ArmourRating
            };
            Attributes p2Attributes = new Attributes()
            {
                MaxHealth = battleInfo.Player2.BaseAttributes.MaxHealth,
                CurrHealth = battleInfo.Player2.BaseAttributes.CurrHealth,
                BaseDistance = battleInfo.Player2.BaseAttributes.BaseDistance * screenWidth,
                ArmourRating = battleInfo.Player2.BaseAttributes.ArmourRating
            };
            player = target.ProcessPlayer(battleInfo.Player1, content, p1Pos, p1flip, p1Attributes, false);
            opponent = target.ProcessPlayer(battleInfo.Player2, content, p2Pos, p2flip, p2Attributes, false);
            player.AddAbilityDoneHandler(OnAbilityDone);
            opponent.AddAbilityDoneHandler(OnAbilityDone);


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
        private void LogoutButton_Click(object sender, EventArgs e)
        {
            ConnectionManager.Instance.Invoke("LeaveSpectateBattle");
            CommandHelper.UndoCommand();
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
        private void DeterminePlayerDirection(float p1x, float p2x, out SpriteEffects p1flip, out SpriteEffects p2flip)
        {
            p1flip = SpriteEffects.None;
            p2flip = SpriteEffects.FlipHorizontally;
            if (p1x >= p2x)
            {
                p1flip = SpriteEffects.FlipHorizontally;
                p2flip = SpriteEffects.None;
            }
        }
        public void OnAbilityDone(object sender, EventArgs e)
        {
            turnDone = true;
        }

        private bool Intersects(Rectangle rect1, Rectangle rect2)
        {
            return rect1.Intersects(rect2);
        }
        public void MakeUseAbility(int playerNum, string name)
        {
            if (playerNum == 0)
                player.UseAbility(name);
            else
                opponent.UseAbility(name);

        }
        public void UpdateBattleInfo(int playerNum, BattleInfo info)
        {
            Attributes p1attrs = new Attributes()
            {
                MaxHealth = info.Player1.BaseAttributes.MaxHealth,
                CurrHealth = info.Player1.BaseAttributes.CurrHealth,
                BaseDistance = info.Player1.BaseAttributes.BaseDistance * screenWidth,
                ArmourRating = info.Player1.BaseAttributes.ArmourRating
            };
            Attributes p2attrs = new Attributes()
            {
                MaxHealth = info.Player2.BaseAttributes.MaxHealth,
                CurrHealth = info.Player2.BaseAttributes.CurrHealth,
                BaseDistance = info.Player2.BaseAttributes.BaseDistance * screenWidth,
                ArmourRating = info.Player2.BaseAttributes.ArmourRating
            };
            if (playerNum == 0)
            {
                player.Position = info.Player1.Position * new Vector2(screenWidth, screenHeight);
                player.BaseAttributes = p1attrs;
                opponent.Position = info.Player2.Position * new Vector2(screenWidth, screenHeight);
                opponent.BaseAttributes = p2attrs;
            }
            else
            {
                opponent.Position = info.Player1.Position * new Vector2(screenWidth, screenHeight);
                opponent.BaseAttributes = p1attrs;
                player.Position = info.Player2.Position * new Vector2(screenWidth, screenHeight);
                player.BaseAttributes = p2attrs;
            }
            BattleUpdateNeeded = null;
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
        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime, p1sprites);
            opponent.Update(gameTime, p2sprites);
            SpriteEffects p1flip;
            SpriteEffects p2flip;
            DeterminePlayerDirection(player.Position.X, opponent.Position.X, out p1flip, out p2flip);
            player.ChangeFlip(p1flip);
            opponent.ChangeFlip(p2flip);
            foreach (var b in buttons)
            {
                b.Update(gameTime);
            }

            if (turnDone && BattleInfoAvailable)
            {
                turnDone = false;
                BattleInfoAvailable = false;
                BattleUpdateNeeded?.Invoke(this, new EventArgs());
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
    }
}
