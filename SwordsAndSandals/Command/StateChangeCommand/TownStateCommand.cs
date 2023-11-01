using Microsoft.Xna.Framework;

using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
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
            State townState = new TownState(graphicsDeviceManager, TownState.playerClass);
            StateManager.Instance.ChangeState(townState);
        }
    }
}