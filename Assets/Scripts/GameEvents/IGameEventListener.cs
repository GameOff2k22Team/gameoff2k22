namespace Architecture
{
    public interface IGameEventListener
    {
        public void OnEventRaised();
        public void OnEventRaised<T0>(T0 input);
    }
}