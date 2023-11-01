using Microsoft.Xna.Framework;
using SwordsAndSandals.Command;
using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
{
    public class SettingsStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;

        public SettingsStateCommand(GraphicsDeviceManager graphicsDeviceManager)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
        }

        public void Execute()
        {
            State settingsState = new SettingsState(graphicsDeviceManager);
            StateManager.Instance.ChangeState(settingsState);
        }
    }
}
