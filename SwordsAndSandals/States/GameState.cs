using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Classes;
using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.Items;
using SwordsAndSandals.Sprites;
using SwordsAndSandals.Stats;
using SwordsAndSandals.UI;
using SwordsAndSandals.Music;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class GameState : State
    {
        //TODO refactor and optimize collisions and sprite code !!!!!!!!!!!!!!!!!!!!

        public event EventHandler BattleUpdateNeeded;

        private BattleInfo battleInfo;
        private bool turnDone;
        public bool BattleInfoAvailable { get; set; }

        private Player player;
        private List<Sprite> p1sprites;
        //private List<Weapon> p1Weapons;
        //private WeaponFactory p1weaponFactory;


        private Player opponent;
        private List<Sprite> p2sprites;
        //private WeaponFactory p2weaponFactory;

        private Background background;
        private IMusic music;
        private List<Button> buttons;

        private int screenWidth;
        private int screenHeight;
        public GameState(GraphicsDeviceManager graphicsDevice, BattleInfo bInfo) : base(graphicsDevice)
        {
            p1sprites = new List<Sprite>();
            p2sprites = new List<Sprite>();
            battleInfo = bInfo;
            turnDone = false;
            BattleInfoAvailable = false;
            screenWidth = graphicsDevice.PreferredBackBufferWidth;
            screenHeight = graphicsDevice.PreferredBackBufferHeight;
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

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            ConnectionManager.Instance.Invoke("LeaveBattle");
        }

        public void OnAbilityDone(object sender, EventArgs e)
        {
            turnDone = true;
        }

        public override void LoadContent(ContentManager content)
        {
            WeaponFactory weaponFactory = GetPlayerWeaponFactory(battleInfo.Player1.ClassName);
            Vector2 p1Pos = new Vector2(battleInfo.Player1.Position.X * screenWidth, battleInfo.Player1.Position.Y * screenHeight);
            Vector2 p2Pos = new Vector2(battleInfo.Player2.Position.X * screenWidth, battleInfo.Player2.Position.Y * screenHeight);
            PlayerFactory p1Factory = GetPlayerFactory(battleInfo.Player1.ClassName);
            PlayerFactory p2Factory = GetPlayerFactory(battleInfo.Player2.ClassName);

            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));

            music = new MusicPlayerAdapter(content);
            music.stopSong();
            music.playSong("BattleMusic");

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
            player = p1Factory.CreatePlayerWithButtons(content, p1Pos, p1flip, p1Attributes, battleInfo.Player1.Name);
            opponent = p2Factory.CreatePlayerWithoutButtons(content, p2Pos, p2flip, p2Attributes, battleInfo.Player2.Name);
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

        public override void UnloadContent()
        {

        }

        private bool Intersects(Rectangle rect1, Rectangle rect2)
        {
            return rect1.Intersects(rect2);
        }

        public void MakePlayerUseAbility(string name)
        {
            player.UseAbility(name);
        }

        public void MakeOpponentUseAbility(string name)
        {
            opponent.UseAbility(name);
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

        public void UpdateBattleInfo(BattleInfo info)
        {
            Attributes p1attrs = new Attributes()
            {
                MaxHealth = info.Player1.BaseAttributes.MaxHealth,
                CurrHealth = info.Player1.BaseAttributes.CurrHealth,
                BaseDistance = info.Player1.BaseAttributes.BaseDistance * screenWidth,
                ArmourRating = info.Player1.BaseAttributes.ArmourRating
            };
            player.Position = info.Player1.Position * new Vector2(screenWidth, screenHeight);
            player.BaseAttributes = p1attrs;
            Attributes p2attrs = new Attributes()
            {
                MaxHealth = info.Player2.BaseAttributes.MaxHealth,
                CurrHealth = info.Player2.BaseAttributes.CurrHealth,
                BaseDistance = info.Player2.BaseAttributes.BaseDistance * screenWidth,
                ArmourRating = info.Player2.BaseAttributes.ArmourRating
            };
            opponent.Position = info.Player2.Position * new Vector2(screenWidth, screenHeight);
            opponent.BaseAttributes = p2attrs;
            BattleUpdateNeeded = null;
        }
    }
}
