using Microsoft.Xna.Framework;

namespace SwordsAndSandals.States.Command
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
