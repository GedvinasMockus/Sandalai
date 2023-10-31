using Microsoft.Xna.Framework;

namespace SwordsAndSandals.States.Command
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
