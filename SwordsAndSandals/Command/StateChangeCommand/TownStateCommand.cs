using Microsoft.Xna.Framework;

using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
{
    public class TownStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        private string playerClass;
        public TownStateCommand(GraphicsDeviceManager graphicsDeviceManager, string playerClass)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
            this.playerClass = playerClass;
        }

        public void Execute()
        {
            State townState = new TownState(graphicsDeviceManager, playerClass);
            StateManager.Instance.ChangeState(townState);
        }
    }
}