using PirateAdventure.Items;

namespace PirateAdventure.Items
{
    public class DrinkingWater : Item
    {
        public DrinkingWater()
        {
            Name = "Drinking Water";
        }
        public override char Symbol => 'W';
    }
}
