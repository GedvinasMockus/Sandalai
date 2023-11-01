using Microsoft.Xna.Framework;
using SwordsAndSandals.Command;
using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
{
    public class LoadingScreenStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;

        public LoadingScreenStateCommand(GraphicsDeviceManager graphicsDeviceManager)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
        }

        public void Execute()
        {
            State loadingScreenState = new LoadingScreenState(graphicsDeviceManager);
            StateManager.Instance.ChangeState(loadingScreenState);
        }
    }
}
