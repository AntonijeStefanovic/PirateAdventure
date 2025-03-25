using PirateAdventure.Items;

namespace PirateAdventure.Characters
{
    public class Player : Character
    {
        public string Name { get; set; } = "Pirate Roberts";
        public List<Item> Inventory { get; set; } = new List<Item>();
        public char Symbol => 'P';

        public bool HasItem(string itemName)
        {
            foreach (var item in Inventory)
            {
                if (item.Name == itemName)
                    return true;
            }
            return false;
        }

    }
}
