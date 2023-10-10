namespace SignalR.Sandalai.Objects
{
    public class Player
    {
        public string ConnectionId { get; private set; }

        public string ClassName { get; private set; }

        public Player(string connId, string className)
        {
            ConnectionId = connId;
            ClassName = className;
        }
    }

}
