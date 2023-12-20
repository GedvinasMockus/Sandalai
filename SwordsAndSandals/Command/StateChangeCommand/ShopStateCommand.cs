using Microsoft.Xna.Framework;
using SwordsAndSandals.Memento;
using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
{
    public class ShopStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private string playerClass;
        private Caretaker caretaker;
        
        public ShopStateCommand(GraphicsDeviceManager graphicsDeviceManager, string playerClass, Caretaker caretaker)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
            this.playerClass = playerClass;
            this.caretaker = caretaker;
        }

        public void Execute()
        {
            State shopState = new ShopState(graphicsDeviceManager, playerClass, caretaker);
            StateManager.Instance.ChangeState(shopState);
        }
    }
}