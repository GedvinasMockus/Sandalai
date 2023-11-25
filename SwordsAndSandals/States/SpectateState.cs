using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Classes;
using SwordsAndSandals.Command;
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
    public class SpectateState : State
    {
        private int screenWidth;
        private int screenHeight;

        public event EventHandler BattleUpdate;

        public BattleInfo BInfo { get; set; }
        public bool BattleInfoAvailable { get; set; }
        private bool turnDone;

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
            BInfo = bInfo;
            turnDone = false;
            BattleInfoAvailable = false;
            p1sprites = new List<Sprite>();
            p2sprites = new List<Sprite>();
        }

        public override void LoadContent(ContentManager content)
        {
            WeaponFactory weaponFactory = GetPlayerWeaponFactory(BInfo.Player1.ClassName);
            Vector2 p1Pos = new Vector2(BInfo.Player1.Position.X * screenWidth, BInfo.Player1.Position.Y * screenHeight);
            Vector2 p2Pos = new Vector2(BInfo.Player2.Position.X * screenWidth, BInfo.Player2.Position.Y * screenHeight);
            PlayerFactory factory = new PlayerFactory(content);

            background = new Background(content.Load<Texture2D>("Background/Battleground/PNG/Battleground4/Bright/back_trees"));

            music = new MusicPlayer(content);
            music.stopSong();

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
            player = factory.CreatePlayerWithoutButtons(BInfo.Player1.ClassName, p1Pos, p1flip, p1sprites, p1Attributes, BInfo.Player1.Name);
            opponent = factory.CreatePlayerWithoutButtons(BInfo.Player1.ClassName, p2Pos, p2flip, p2sprites, p2Attributes, BInfo.Player2.Name);
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
        public void UpdateBattleInfo(int playerNum)
        {
            Attributes p1attrs = BInfo.Player1.GetPlayerAttributes(screenWidth);
            Attributes p2attrs = BInfo.Player2.GetPlayerAttributes(screenWidth);
            if (playerNum == 0)
            {
                player.Position = BInfo.Player1.Position * new Vector2(screenWidth, screenHeight);
                player.BaseAttributes = p1attrs;
                opponent.Position = BInfo.Player2.Position * new Vector2(screenWidth, screenHeight);
                opponent.BaseAttributes = p2attrs;
            }
            else
            {
                opponent.Position = BInfo.Player1.Position * new Vector2(screenWidth, screenHeight);
                opponent.BaseAttributes = p1attrs;
                player.Position = BInfo.Player2.Position * new Vector2(screenWidth, screenHeight);
                player.BaseAttributes = p2attrs;
            }
            BattleUpdate = null;
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
                BattleUpdate?.Invoke(this, new EventArgs());
            }

            for (int i = 0; i < p1sprites.Count; i++)
            {
                Sprite s = p1sprites[i];
                if (s.Rectangle.Intersects(opponent.Rectangle))
                {
                    p1sprites.RemoveAt(i);
                    i--;
                }
                s.Update(gameTime);
            }
            for (int i = 0; i < p2sprites.Count; i++)
            {
                Sprite s = p2sprites[i];
                if (s.Rectangle.Intersects(player.Rectangle))
                {
                    p2sprites.RemoveAt(i);
                    i--;
                }
                s.Update(gameTime);
            }
        }
    }
}
