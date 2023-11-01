using Microsoft.Xna.Framework;
using SwordsAndSandals.Command;
using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
{
    public class BattleListStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;

        public BattleListStateCommand(GraphicsDeviceManager graphicsDeviceManager)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
        }

        public void Execute()
        {
            State battleListState = new BattleListState(graphicsDeviceManager);
            StateManager.Instance.ChangeState(battleListState);
        }
    }
}
