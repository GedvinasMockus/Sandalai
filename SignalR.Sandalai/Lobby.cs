using System.Collections.Generic;

namespace SignalR.Sandalai
{
    public class Lobby
    {
        private List<string> MyUsers;
        private Lobby()
        {
            MyUsers = new List<string>();
        }
        public void AddUser(string userId)
        {
            lock (MyUsers)
            {
                MyUsers.Add(userId);
            }
        }
        public void RemoveUser(string userId)
        {
            lock (MyUsers)
            {
                MyUsers.Remove(userId);
            }
        }
        public static Lobby GetInstance()
        {
            return LobbySingleton.instance;
        }
        private static class LobbySingleton
        {
            public static readonly Lobby instance = new Lobby();
        }
    }

}
