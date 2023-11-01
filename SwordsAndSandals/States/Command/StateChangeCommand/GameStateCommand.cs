using Microsoft.Xna.Framework;

using SwordsAndSandals.InfoStructs;

namespace SwordsAndSandals.States.Command
{
    public class GameStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private BattleInfo battleInfo;
        public GameStateCommand(GraphicsDeviceManager graphicsDeviceManager, BattleInfo battleInfo)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
            this.battleInfo = battleInfo;
        }

        public void Execute()
        {
            State gameState = new GameState(graphicsDeviceManager, battleInfo);
            StateManager.Instance.ChangeState(gameState);
        }
    }
}
