using Microsoft.Xna.Framework;
using SwordsAndSandals.Command;
using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
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
