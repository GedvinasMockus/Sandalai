using Microsoft.Xna.Framework;

namespace SwordsAndSandals.States.Command
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
