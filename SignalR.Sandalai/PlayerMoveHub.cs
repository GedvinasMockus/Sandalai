using System;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;
namespace SignalR.Sandalai
{
    public class PlayerMoveHub : Hub
    {
        private const int _screenHeight = 1080;
        private const int _screenWidth = 1920;
        private const int _groundLevel = 11 * _screenHeight / 17;
        private static int _x = _screenWidth / 6;
        private static int _y = _groundLevel;
        public override Task OnConnected()
        {
            Console.WriteLine("New user connected");
            Clients.Caller.UpdateState(_x, _y);
            return base.OnConnected();
        }

        public void Move(int x, int y)
        {
            _x += x;
            _y += y;
            Console.WriteLine($"Moving character to x: {_x}, y: {_y}");
            Clients.All.UpdateState(_x, _y);
        }
    }
}
