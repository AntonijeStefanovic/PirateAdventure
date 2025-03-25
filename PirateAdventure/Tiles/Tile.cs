using PirateAdventure.Items;
using PirateAdventure.Characters;

namespace PirateAdventure.Tiles
{
    public abstract class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsDiscovered { get; set; } = false;
        public Item ItemOnTile { get; set; }
        public Enemy EnemyOnTile { get; set; }
        public abstract char Symbol { get; }
    }
}
