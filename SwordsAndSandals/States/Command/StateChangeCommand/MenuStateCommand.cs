using Microsoft.Xna.Framework;

namespace SwordsAndSandals.States.Command
{
    public class MenuStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;

        public MenuStateCommand(GraphicsDeviceManager graphicsDeviceManager)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
        }

        public void Execute()
        {
            State menuState = new MenuState(graphicsDeviceManager);
            StateManager.Instance.ChangeState(menuState);
        }
    }
}
