using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

using Microsoft.AspNet.SignalR;

namespace SignalR.Sandalai
{
    public class Spawn
    {
        private readonly static Lazy<Spawn> _spawn = new Lazy<Spawn>(() => new Spawn(GlobalHost.ConnectionManager.GetHubContext<SpawnHub>()));
        private IHubContext _context;
        private readonly ConcurrentDictionary<string, int> _groups;

        public static Spawn Instance
        {
            get { return _spawn.Value; }
        }
        public Spawn(IHubContext context)
        {
            _context = context;
            _groups = new ConcurrentDictionary<string, int>();
        }
        public IEnumerable<string> GetAllGroups()
        {
            return _groups.Keys;
        }
        public int GetValue(string groupId)
        {
            _groups.TryGetValue(groupId, out int i);
            return i;
        }
        public void AddGroup(string groupId)
        {
            _groups.TryAdd(groupId, 1);
        }
        public void RemoveGroup(string groupId)
        {
            _groups.TryRemove(groupId, out int i);
        }
        public void UpdateValue(string groupId, int newValue, int originalValue)
        {
            _groups.TryUpdate(groupId, newValue, originalValue);
        }
    }
}
