using Microsoft.Xna.Framework;

namespace SwordsAndSandals.States.Command
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
