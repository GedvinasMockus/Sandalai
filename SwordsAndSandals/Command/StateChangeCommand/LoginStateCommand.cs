using Microsoft.Xna.Framework;
using SwordsAndSandals.Command;
using SwordsAndSandals.States;

namespace SwordsAndSandals.Command.StateChangeCommand
{
    public class LoginStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;
        public LoginStateCommand(GraphicsDeviceManager graphicsDeviceManager)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
        }

        public void Execute()
        {
            State loginState = new LoginState(graphicsDeviceManager);
            StateManager.Instance.ChangeState(loginState);
        }
    }
}
