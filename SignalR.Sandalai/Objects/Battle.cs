using SignalR.Sandalai.InfoStructs;

using System;
using System.Collections.Generic;
using System.Numerics;

namespace SignalR.Sandalai.Objects
{
    public class Battle
    {
        private const float Pos1x = 1f / 6;
        private const float Pos2x = 5f / 6;
        private const float PosGround = 11f / 17;
        private List<Player> observers = new List<Player>();
        private string battleStarted;
        public void BattleStart()
        {
            observers[0].Position = new Vector2(Pos1x, PosGround);
            observers[1].Position = new Vector2(Pos2x, PosGround);
            observers[0].Flip = FlipEnum.None;
            observers[1].Flip = FlipEnum.FlipHorizontally;
            battleStarted = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public void BattleStop()
        {
            observers[0].Position = new Vector2(-1, -1);
            observers[1].Position = new Vector2(-1, -1);
            observers[0].Flip = FlipEnum.None;
            observers[1].Flip = FlipEnum.None;
        }
        public BattleInfo GetInfo(bool flip)
        {
            return flip ? new BattleInfo(observers[1].GetInfo(), observers[0].GetInfo()) : new BattleInfo(observers[0].GetInfo(), observers[1].GetInfo());
        }
        public List<string> GetBattleInfo()
        {
            List<string> infos = new List<string>
            {
                observers[0].ClassName,
                observers[1].ClassName,
                battleStarted
            };
            return infos;
        }
        public void Attach(Player observer)
        {
            observers.Add(observer);
        }

        public void Detach(Player observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update();
            }
        }
        public void AbilityUsed(string name, string connectionId)
        {
            Console.WriteLine($"Server: user {connectionId} used ability {name}");
            Notify();
        }
        public Player GetPlayer(int id)
        {
            return observers[id];
        }
    }
}
