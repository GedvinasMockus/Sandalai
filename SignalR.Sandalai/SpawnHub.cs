using System;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;

namespace SignalR.Sandalai
{
    public class SpawnHub : Hub
    {
        private readonly Spawn _spawn;
        public SpawnHub() : this(Spawn.Instance) { }
        public SpawnHub(Spawn spawn)
        {
            _spawn = spawn;
        }
        public override Task OnConnected()
        {
            Console.WriteLine("New user connected");
            bool groupFound = false;
            foreach (var group in _spawn.GetAllGroups())
            {
                int count = _spawn.GetValue(group);
                if (count < 2)
                {
                    groupFound = true;
                    _spawn.UpdateValue(group, count + 1, count);
                    Console.WriteLine(group);
                    Clients.Caller.AssignGroup(group);
                    break;
                }
            }
            if (!groupFound)
            {
                DateTime now = DateTime.Now;
                string dateTime = now.GetHashCode().ToString();
                _spawn.AddGroup(dateTime);
                Console.WriteLine(dateTime);
                Clients.Caller.AssignGroup(dateTime);
            }
            return base.OnConnected();
        }


    }
}
