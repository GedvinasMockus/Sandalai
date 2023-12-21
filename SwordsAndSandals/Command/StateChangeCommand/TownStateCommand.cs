using Microsoft.Xna.Framework;
using SwordsAndSandals.Memento;
using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
{
    public class TownStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private string playerClass;
        private Caretaker caretaker;
        
        public TownStateCommand(GraphicsDeviceManager graphicsDeviceManager, string playerClass, Caretaker caretaker)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
            this.playerClass = playerClass;
            this.caretaker = caretaker;
        }

        public void Execute()
        {
            State townState = new TownState(graphicsDeviceManager, playerClass, caretaker);
            StateManager.Instance.ChangeState(townState);
        }
    }
}