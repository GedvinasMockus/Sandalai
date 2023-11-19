using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Classes;
using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.Items;
using SwordsAndSandals.Music;
using SwordsAndSandals.Sprites;
using SwordsAndSandals.Stats;
using SwordsAndSandals.UI;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals.States
{
    public class GameState : State
    {
        //TODO refactor and optimize collisions and sprite code !!!!!!!!!!!!!!!!!!!!

        public BattleInfo BInfo { get; set; }
        public bool BattleInfoAvailable { get; set; }
        private bool turnDone;

        private Player player;
        private List<Sprite> p1sprites;
        //private List<Weapon> p1Weapons;
        //private WeaponFactory p1weaponFactory;


        private Player opponent;
        private List<Sprite> p2sprites;
        //private WeaponFactory p2weaponFactory;

        private Background background;
        private List<Button> buttons;

        private int screenWidth;
        private int screenHeight;
        public GameState(GraphicsDeviceManager graphicsDevice, BattleInfo bInfo) : base(graphicsDevice)
        {
            p1sprites = new List<Sprite>();
            p2sprites = new List<Sprite>();
            BInfo = bInfo;
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
            WeaponFactory weaponFactory = GetPlayerWeaponFactory(BInfo.Player1.ClassName);
            Vector2 p1Pos = new Vector2(BInfo.Player1.Position.X * screenWidth, BInfo.Player1.Position.Y * screenHeight);
            Vector2 p2Pos = new Vector2(BInfo.Player2.Position.X * screenWidth, BInfo.Player2.Position.Y * screenHeight);
            PlayerFactory p1Factory = GetPlayerFactory(BInfo.Player1.ClassName);
            PlayerFactory p2Factory = GetPlayerFactory(BInfo.Player2.ClassName);

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
            Attributes p1Attributes = BInfo.Player1.GetPlayerAttributes(screenWidth);
            Attributes p2Attributes = BInfo.Player2.GetPlayerAttributes(screenWidth);
            player = p1Factory.CreatePlayerWithButtons(content, p1Pos, p1flip, p1sprites, p1Attributes, BInfo.Player1.Name);
            opponent = p2Factory.CreatePlayerWithoutButtons(content, p2Pos, p2flip, p2sprites, p2Attributes, BInfo.Player2.Name);
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
            player.Update(gameTime);
            opponent.Update(gameTime);
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
                UpdateBattleInfo();
            }

            for(int i = 0; i < p1sprites.Count; i++)
            {
                Sprite s = p1sprites[i];
                if(s.Rectangle.Intersects(opponent.Rectangle))
                {
                    p1sprites.RemoveAt(i);
                    i--;
                }
                s.Update(gameTime);
            }
            for(int i = 0; i < p2sprites.Count; i++)
            {
                Sprite s = p2sprites[i];
                if(s.Rectangle.Intersects(player.Rectangle))
                {
                    p2sprites.RemoveAt(i);
                    i--;
                }
                s.Update(gameTime);
            }
        }

        public override void UnloadContent()
        {

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

        public void UpdateBattleInfo()
        {
            Attributes p1attrs = BInfo.Player1.GetPlayerAttributes(screenWidth);
            player.Position = BInfo.Player1.Position * new Vector2(screenWidth, screenHeight);
            player.BaseAttributes = p1attrs;
            Attributes p2attrs = BInfo.Player2.GetPlayerAttributes(screenWidth);
            opponent.Position = BInfo.Player2.Position * new Vector2(screenWidth, screenHeight);
            opponent.BaseAttributes = p2attrs;
        }
    }
}
