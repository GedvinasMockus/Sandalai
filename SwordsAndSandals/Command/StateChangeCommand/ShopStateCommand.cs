using Microsoft.Xna.Framework;

using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
{
    public class ShopStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        public ShopStateCommand(GraphicsDeviceManager graphicsDeviceManager)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
        }

        public void Execute()
        {
            State shopState = new ShopState(graphicsDeviceManager);
            StateManager.Instance.ChangeState(shopState);
        }
    }
}