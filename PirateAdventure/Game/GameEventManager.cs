namespace PirateAdventure.Game
{
    public class GameEventManager
    {
        public event Action<string> OnNotify;

        public void Notify(string message)
        {
            OnNotify?.Invoke(message);
        }
    }
}
