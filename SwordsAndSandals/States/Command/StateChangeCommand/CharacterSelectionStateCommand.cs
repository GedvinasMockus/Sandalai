using Microsoft.Xna.Framework;

namespace SwordsAndSandals.States.Command
{
    public class CharacterSelectionStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;

        public CharacterSelectionStateCommand(GraphicsDeviceManager graphicsDeviceManager)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
        }

        public void Execute()
        {
            State characterState = new CharacterSelectionState(graphicsDeviceManager);
            StateManager.Instance.ChangeState(characterState);
        }
    }
}
