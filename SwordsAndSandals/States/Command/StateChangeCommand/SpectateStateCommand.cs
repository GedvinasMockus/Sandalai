using Microsoft.Xna.Framework;

using SwordsAndSandals.InfoStructs;

namespace SwordsAndSandals.States.Command
{
    public class SpectateStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private BattleInfo battleInfo;

        public SpectateStateCommand(GraphicsDeviceManager graphicsDeviceManager, BattleInfo battleInfo)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
            this.battleInfo = battleInfo;
        }

        public void Execute()
        {
            State spectateState = new SpectateState(graphicsDeviceManager, battleInfo);
            StateManager.Instance.ChangeState(spectateState);
        }
    }
}
