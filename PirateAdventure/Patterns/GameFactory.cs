using PirateAdventure.Tiles;
using PirateAdventure.Items;
using PirateAdventure.Characters;

namespace PirateAdventure.Patterns
{
    public static class GameFactory
    {
        public static Tile CreateTile(string type)
        {
            if (type == "Ocean")
                return new OceanTile();
            else if (type == "Land")
                return new LandTile();
            else if (type == "Forest")
                return new ForestTile();
            else if (type == "Desert")
                return new DesertTile();
            else if (type == "Cave")
                return new CaveTile();
            else
                return new LandTile();
        }

        public static Enemy CreateEnemy(string type, int x, int y)
        {
            if (type == "Megalodon")
                return new Megalodon(x, y);
            else if (type == "GiantSpider")
                return new GiantSpider(x, y);
            else if (type == "Sphinx")
                return new Sphinx(x, y);
            else if (type == "Goblin")
                return new Goblin(x, y);
            return null;
        }

        public static Item CreateItem(string type)
        {
            if (type == "Boat")
                return new Boat();
            if (type == "Treasure")
                return new Treasure();
            if (type == "Torch")
                return new Torch();
            if (type == "DrinkingWater")
                return new DrinkingWater();
            if (type == "Axe")
                return new Axe();
            return null;
        }
    }
}
