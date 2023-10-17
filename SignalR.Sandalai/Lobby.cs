using SignalR.Sandalai.Objects;

using System.Collections.Generic;
using System.Linq;

namespace SignalR.Sandalai
{
    public class Lobby
    {
        private Dictionary<string, Player> MyUsers;
        private static object Obj = new object();
        private static volatile Lobby instance;
        private Lobby()
        {
            MyUsers = new Dictionary<string, Player>();
        }
        public static Lobby Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (Obj)
                    {
                        if (instance == null)
                        {
                            instance = new Lobby();
                        }
                    }
                }
                return instance;
            }
        }
        public void AddUser(string userId, string className)
        {
            lock (MyUsers)
            {
                MyUsers.Add(userId, new Player(userId, className));
            }
        }
        public void RemoveUser(string userId)
        {
            lock (MyUsers)
            {
                MyUsers.Remove(userId);
            }
        }
        public Player FindAnotherUser(string userId)
        {
            Player anotherUser;
            lock (MyUsers)
            {
                anotherUser = MyUsers.FirstOrDefault(pair => !pair.Key.Equals(userId)).Value;
            }
            return anotherUser;
        }
        public Player GetUser(string userId)
        {
            Player player;
            lock (MyUsers)
            {
                player = MyUsers[userId];
            }
            Player playerCopy = (Player)player.Clone();
            return playerCopy;
        }
    }

}
