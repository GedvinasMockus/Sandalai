using Microsoft.Xna.Framework;
using SwordsAndSandals.Command;
using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
{
    public class TownStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        public TownStateCommand(GraphicsDeviceManager graphicsDeviceManager)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
        }

        public void Execute()
        {
            State townState = new TownState(graphicsDeviceManager);
            StateManager.Instance.ChangeState(townState);
        }
    }
}
