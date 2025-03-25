using PirateAdventure.Tiles;
using PirateAdventure.Patterns;
using PirateAdventure.Characters;
using PirateAdventure.Items;

namespace PirateAdventure.Game
{
    public class Map
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Tile[,] Tiles { get; set; }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[width, height];

            // Initialize all tiles as Land by default.
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Tiles[i, j] = GameFactory.CreateTile("Land");
                    Tiles[i, j].X = i;
                    Tiles[i, j].Y = j;
                }
            }

            // Create different terrains.
            CreateContiguousRegion("Ocean", 12);
            CreateContiguousRegion("Forest", 6);
            CreateContiguousRegion("Desert", 9);
            CreateContiguousRegion("Cave", 4);
        }

        public void Display(Player player, bool revealAll = false)
        {
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    Tile tile = Tiles[i, j];
                    // In normal mode, undiscovered tiles show as "?".
                    if (!tile.IsDiscovered && !revealAll)
                    {
                        Console.Write("? ");
                    }
                    else
                    {
                        // Always show the player's symbol if the player is on this tile.
                        if (player.X == i && player.Y == j)
                        {
                            Console.Write("P ");
                        }
                        else if (revealAll)
                        {
                            // In full reveal mode, show the actual contents item/enemy
                            if (tile.ItemOnTile != null)
                            {
                                Console.Write(tile.ItemOnTile.Symbol + " ");
                            }
                            else if (tile.EnemyOnTile != null)
                            {
                                Console.Write(tile.EnemyOnTile.Symbol + " ");
                            }
                            else
                            {
                                Console.Write(tile.Symbol + " ");
                            }
                        }
                        else
                        {
                            // Normal mode: hide treasure and enemy symbols.
                            // Boat and other non-hidden items are shown.
                            if (tile.ItemOnTile != null && tile.ItemOnTile is Treasure)
                                Console.Write(tile.Symbol + " ");
                            else if (tile.EnemyOnTile != null && tile.EnemyOnTile is Megalodon)
                                Console.Write(tile.Symbol + " ");
                            else if (tile.ItemOnTile != null)
                                Console.Write(tile.ItemOnTile.Symbol + " ");
                            else
                                Console.Write(tile.Symbol + " ");
                        }
                    }
                }
                Console.WriteLine();
            }
        }



        private void CreateContiguousRegion(string terrainType, int tileCount)
        {
            Random rand = new Random();

            int startX, startY;
            do
            {
                startX = rand.Next(Width);
                startY = rand.Next(Height);
            } while (!(Tiles[startX, startY] is LandTile));

            Tiles[startX, startY] = GameFactory.CreateTile(terrainType);
            Tiles[startX, startY].X = startX;
            Tiles[startX, startY].Y = startY;

            int converted = 1;
            Queue<(int x, int y)> queue = new Queue<(int, int)>();
            queue.Enqueue((startX, startY));

            while (converted < tileCount && queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();
                List<(int, int)> neighbors = new List<(int, int)>
        {
            (x - 1, y), (x + 1, y), (x, y - 1), (x, y + 1)
        };
                foreach (var (nx, ny) in neighbors)
                {
                    if (nx >= 0 && nx < Width && ny >= 0 && ny < Height)
                    {
                        if (Tiles[nx, ny] is LandTile)
                        {
                            Tiles[nx, ny] = GameFactory.CreateTile(terrainType);
                            Tiles[nx, ny].X = nx;
                            Tiles[nx, ny].Y = ny;
                            converted++;
                            queue.Enqueue((nx, ny));
                            if (converted >= tileCount)
                                break;
                        }
                    }
                }
            }
        }

        public void RevealFullMap()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Tiles[i, j].IsDiscovered = true;
                }
            }
        }

    }
}
