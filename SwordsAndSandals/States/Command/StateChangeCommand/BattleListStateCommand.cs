using Microsoft.Xna.Framework;

namespace SwordsAndSandals.States.Command
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
