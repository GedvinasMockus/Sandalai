using Microsoft.Xna.Framework;
using SwordsAndSandals.Command;

using SwordsAndSandals.InfoStructs;
using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
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
