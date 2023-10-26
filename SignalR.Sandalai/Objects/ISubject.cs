namespace SignalR.Sandalai.Objects
{
    public interface ISubject
    {
        void AddSpectator();
        void RemoveSpectator();
        void Notify();
    }
}
