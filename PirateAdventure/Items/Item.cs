namespace PirateAdventure.Items
{
    public abstract class Item
    {
        public string Name { get; set; }
        public abstract char Symbol { get; }
    }
}
