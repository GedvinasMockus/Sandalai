using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SwordsAndSandals.Command;
using SwordsAndSandals.Command.StateChangeCommand;
using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.States;

using System;
using System.Collections.Generic;

namespace SwordsAndSandals
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int _screenHeight = 1080;
        private const int _screenWidth = 1920;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ConnectionManager.Instance.AddHub("MainHub");
            ConnectionManager.Instance.AddHandler<BattleInfo>("OpponentFound", (info) =>
            {
                CommandHelper.ExecuteCommand(new GameStateCommand(_graphics, info));
            });
            ConnectionManager.Instance.AddHandler<string, BattleInfo>("AbilityUsed", (name, info) =>
            {
                //TODO refactor battle state update
                if (StateManager.Instance.CurrentState is GameState)
                {
                    GameState battle = (StateManager.Instance.CurrentState as GameState);
                    battle.MakeOpponentUseAbility(name);
                    battle.BInfo = info;
                    battle.BattleInfoAvailable = true;
                }
            });
            ConnectionManager.Instance.AddHandler<BattleInfo>("BattleInfoUpdated", (info) =>
            {
                //TODO refactor battle state update
                if (StateManager.Instance.CurrentState is GameState)
                {
                    GameState battle = (StateManager.Instance.CurrentState as GameState);
                    battle.BInfo = info;
                    battle.BattleInfoAvailable = true;
                }
            });
            ConnectionManager.Instance.AddHandler("BattleLeft", () =>
            {
                CommandHelper.UndoCommand(2);
            });
            ConnectionManager.Instance.AddHandler("BackToLoading", () =>
            {
                ConnectionManager.Instance.Invoke("FindOpponent");
                CommandHelper.UndoCommand();
            });
            ConnectionManager.Instance.AddHandler<List<BattleInfo>>("SpectateBattleInfo", (info) =>
            {
                if (StateManager.Instance.CurrentState is BattleListState)
                {
                    BattleListState battleListState = (StateManager.Instance.CurrentState as BattleListState);
                    EventHandler handler = new EventHandler((o, e) =>
                    {
                        battleListState.UpdateGrid(info);
                    });
                    battleListState.UpdateNeeded += handler;
                    battleListState.InfoAvailable = true;
                }
            });
            ConnectionManager.Instance.AddHandler<BattleInfo>("ShowMatch", (info) =>
            {
                CommandHelper.ExecuteCommand(new SpectateStateCommand(_graphics, info));
            });
            ConnectionManager.Instance.AddHandler<string, int, BattleInfo>("AbilityUsedSpectate", (name, player, info) =>
            {
                if (StateManager.Instance.CurrentState is SpectateState)
                {
                    SpectateState battle = (StateManager.Instance.CurrentState as SpectateState);
                    battle.MakeUseAbility(player, name);
                    EventHandler handler = new EventHandler((o, e) =>
                    {
                        battle.UpdateBattleInfo(player);
                    });
                    battle.BInfo = info;
                    battle.BattleUpdate += handler;
                    battle.BattleInfoAvailable = true;
                }
            });
            ConnectionManager.Instance.AddHandler("BackToBattleList", () =>
            {
                CommandHelper.UndoCommand();
            });
            ConnectionManager.Instance.StartConnection();
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            StateManager.Instance.SetContentManager(Content);
            CommandHelper.ExecuteCommand(new MenuStateCommand(_graphics));
        }

        protected override void Update(GameTime gameTime)
        {
            if (StateManager.Instance.NotInAState()) this.Exit();
            StateManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);
            StateManager.Instance.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            ConnectionManager.Instance.StopConnection();
            base.OnExiting(sender, args);
        }
    }
}