using Microsoft.Xna.Framework;

using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
{
    public class ShopStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private string playerClass;
        
        public ShopStateCommand(GraphicsDeviceManager graphicsDeviceManager, string playerClass)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
            this.playerClass = playerClass;
        }

        public void Execute()
        {
            State shopState = new ShopState(graphicsDeviceManager, playerClass);
            StateManager.Instance.ChangeState(shopState);
        }
    }
}