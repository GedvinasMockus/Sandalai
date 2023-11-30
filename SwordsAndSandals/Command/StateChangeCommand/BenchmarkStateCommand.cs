using Microsoft.Xna.Framework;
using SwordsAndSandals.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordsAndSandals.Command.StateChangeCommand
{
    public class BenchmarkStateCommand : ICommand
    {
        private GraphicsDeviceManager graphicsDeviceManager;

        public BenchmarkStateCommand(GraphicsDeviceManager graphicsDeviceManager)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;
        }
        public void Execute()
        {
            State benchmarkState = new BenchmarkState(graphicsDeviceManager);
            StateManager.Instance.ChangeState(benchmarkState);
        }
    }
}
