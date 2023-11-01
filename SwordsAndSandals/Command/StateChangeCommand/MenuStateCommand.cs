using Microsoft.Xna.Framework;
using SwordsAndSandals.Command;
using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
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
