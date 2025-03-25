using PirateAdventure.Characters;
using PirateAdventure.Items;
using PirateAdventure.Tiles;
using PirateAdventure.Patterns;

namespace PirateAdventure.Game
{
    public class GameEngine
    {
        public Map Map { get; set; }
        public Player Player { get; set; }
        public List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public GameEventManager EventManager { get; set; } = new GameEventManager();
        public ICombatStrategy CombatStrategy { get; set; }
        public bool GameOver { get; set; } = false;

        public GameEngine()
        {
            Map = new Map(10, 10);
            Player = new Player();

            Random rand = new Random();
            // Place the player on a random free land tile.
            bool placed = false;
            while (!placed)
            {
                int x = rand.Next(Map.Width);
                int y = rand.Next(Map.Height);
                if (Map.Tiles[x, y] is LandTile && Map.Tiles[x, y].ItemOnTile == null && Map.Tiles[x, y].EnemyOnTile == null)
                {
                    Player.X = x;
                    Player.Y = y;
                    placed = true;
                }
            }

            // Reveal the starting tile and its immediate neighbors.
            RevealTiles();

            // Place the items

            placed = false;
            while (!placed)
            {
                int x = rand.Next(Map.Width);
                int y = rand.Next(Map.Height);
                if (Map.Tiles[x, y] is LandTile && (x != Player.X || y != Player.Y) && Map.Tiles[x, y].ItemOnTile == null)
                {
                    Map.Tiles[x, y].ItemOnTile = GameFactory.CreateItem("Boat");
                    placed = true;
                }
            }

            placed = false;
            while (!placed)
            {
                int x = rand.Next(Map.Width);
                int y = rand.Next(Map.Height);
                if (Map.Tiles[x, y] is LandTile && (x != Player.X || y != Player.Y) && Map.Tiles[x, y].ItemOnTile == null)
                {
                    Map.Tiles[x, y].ItemOnTile = GameFactory.CreateItem("Axe");
                    placed = true;
                }
            }

            placed = false;
            while (!placed)
            {
                int x = rand.Next(Map.Width);
                int y = rand.Next(Map.Height);
                if (Map.Tiles[x, y] is LandTile && (x != Player.X || y != Player.Y) && Map.Tiles[x, y].ItemOnTile == null)
                {
                    Map.Tiles[x, y].ItemOnTile = GameFactory.CreateItem("Torch");
                    placed = true;
                }
            }

            placed = false;
            while (!placed)
            {
                int x = rand.Next(Map.Width);
                int y = rand.Next(Map.Height);
                if (Map.Tiles[x, y] is LandTile && (x != Player.X || y != Player.Y) && Map.Tiles[x, y].ItemOnTile == null)
                {
                    Map.Tiles[x, y].ItemOnTile = GameFactory.CreateItem("DrinkingWater");
                    placed = true;
                }
            }


            // ensuring that no enemy is present on the tile.
            placed = false;
            while (!placed)
            {
                int x = rand.Next(Map.Width);
                int y = rand.Next(Map.Height);
                Tile tile = Map.Tiles[x, y];
                if ((tile is OceanTile || tile is ForestTile || tile is DesertTile || tile is CaveTile)
                     && tile.ItemOnTile == null && tile.EnemyOnTile == null)
                {
                    tile.ItemOnTile = GameFactory.CreateItem("Treasure");
                    placed = true;
                }
            }

            // Ocean: Place Megalodon.
            placed = false;
            while (!placed)
            {
                int x = rand.Next(Map.Width);
                int y = rand.Next(Map.Height);
                if (Map.Tiles[x, y] is OceanTile && Map.Tiles[x, y].EnemyOnTile == null && Map.Tiles[x, y].ItemOnTile == null)
                {
                    var mega = (Megalodon)GameFactory.CreateEnemy("Megalodon", x, y);
                    Map.Tiles[x, y].EnemyOnTile = mega;
                    Enemies.Add(mega);
                    placed = true;
                }
            }

            // Forest: Place Giant Spider.
            placed = false;
            while (!placed)
            {
                int x = rand.Next(Map.Width);
                int y = rand.Next(Map.Height);
                if (Map.Tiles[x, y] is ForestTile && Map.Tiles[x, y].EnemyOnTile == null && Map.Tiles[x, y].ItemOnTile == null)
                {
                    var spider = (GiantSpider)GameFactory.CreateEnemy("GiantSpider", x, y);
                    Map.Tiles[x, y].EnemyOnTile = spider;
                    Enemies.Add(spider);
                    placed = true;
                }
            }

            // Desert: Place Sphinx.
            placed = false;
            while (!placed)
            {
                int x = rand.Next(Map.Width);
                int y = rand.Next(Map.Height);
                if (Map.Tiles[x, y] is DesertTile && Map.Tiles[x, y].EnemyOnTile == null && Map.Tiles[x, y].ItemOnTile == null)
                {
                    var sphinx = (Sphinx)GameFactory.CreateEnemy("Sphinx", x, y);
                    Map.Tiles[x, y].EnemyOnTile = sphinx;
                    Enemies.Add(sphinx);
                    placed = true;
                }
            }

            // Cave: Place Goblin.
            placed = false;
            while (!placed)
            {
                int x = rand.Next(Map.Width);
                int y = rand.Next(Map.Height);
                if (Map.Tiles[x, y] is CaveTile && Map.Tiles[x, y].EnemyOnTile == null && Map.Tiles[x, y].ItemOnTile == null)
                {
                    var goblin = (Goblin)GameFactory.CreateEnemy("Goblin", x, y);
                    Map.Tiles[x, y].EnemyOnTile = goblin;
                    Enemies.Add(goblin);
                    placed = true;
                }
            }

            CombatStrategy = new RandomGuessCombatStrategy();

            EventManager.OnNotify += message => Console.WriteLine("[Event] " + message);
        }

        private void RevealTiles()
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    int nx = Player.X + dx;
                    int ny = Player.Y + dy;
                    if (nx >= 0 && nx < Map.Width && ny >= 0 && ny < Map.Height)
                    {
                        Map.Tiles[nx, ny].IsDiscovered = true;
                    }
                }
            }
        }

        public void Start()
        {
            while (!GameOver)
            {
                Console.Clear();
                DisplayLegend();
                DisplayInventory();
                Map.Display(Player);
                Console.WriteLine("Enter move (WASD): ");
                char move = Console.ReadKey().KeyChar;
                Console.WriteLine();
                ProcessMove(move);
            }

            Console.WriteLine("\n--- Game Over ---\n");
            Console.WriteLine("Press any key to reveal the full map and final game info...");
            Console.ReadKey();

            Map.RevealFullMap();

            Console.WriteLine("\nFinal Map:");
            Map.Display(Player, true);
            DisplayInventory();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }


        public void ProcessMove(char move)
        {
            int newX = Player.X;
            int newY = Player.Y;
            if (move == 'w' || move == 'W')
                newY -= 1;
            else if (move == 's' || move == 'S')
                newY += 1;
            else if (move == 'a' || move == 'A')
                newX -= 1;
            else if (move == 'd' || move == 'D')
                newX += 1;
            else
            {
                EventManager.Notify("Invalid input.");
                return;
            }

            if (newX < 0 || newX >= Map.Width || newY < 0 || newY >= Map.Height)
            {
                EventManager.Notify("Cannot move outside the map!");
                return;
            }

            Tile targetTile = Map.Tiles[newX, newY];

            if (targetTile is OceanTile && !Player.HasItem("Boat"))
            {
                EventManager.Notify("You need a boat to enter the ocean!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
            if (targetTile is ForestTile && !Player.HasItem("Axe"))
            {
                EventManager.Notify("You need an axe to enter the forest!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
            if (targetTile is DesertTile && !Player.HasItem("Drinking Water"))
            {
                EventManager.Notify("You need drinking water to cross the desert!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
            if (targetTile is CaveTile && !Player.HasItem("Torch"))
            {
                EventManager.Notify("You need a torch to enter the cave!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }


            Player.X = newX;
            Player.Y = newY;
            EventManager.Notify($"Player moved to ({newX}, {newY}).");

            RevealTiles();

            if (targetTile.ItemOnTile != null)
            {
                Item found = targetTile.ItemOnTile;
                if (found is Boat)
                {
                    EventManager.Notify("You found a boat! It has been added to your inventory.");
                    Player.Inventory.Add(found);
                    targetTile.ItemOnTile = null;
                }
                else if (found is Treasure)
                {
                    EventManager.Notify("Congratulations! You found the treasure and won the game!");
                    GameOver = true;
                    return;
                }
                else if (found is Axe)
                {
                    EventManager.Notify("You found an axe! It has been added to your inventory.");
                    Player.Inventory.Add(found);
                    targetTile.ItemOnTile = null;
                }
                else if (found is Torch)
                {
                    EventManager.Notify("You found a torch! It has been added to your inventory.");
                    Player.Inventory.Add(found);
                    targetTile.ItemOnTile = null;
                }
                else if (found is DrinkingWater)
                {
                    EventManager.Notify("You found drinking water! It has been added to your inventory.");
                    Player.Inventory.Add(found);
                    targetTile.ItemOnTile = null;
                }
            }

            if (targetTile.EnemyOnTile != null)
            {
                Enemy encounteredEnemy = targetTile.EnemyOnTile;
                EventManager.Notify($"You encountered a {encounteredEnemy.Name}!");

                ICombatStrategy strategy;
                if (encounteredEnemy is Sphinx)
                    strategy = new RiddleCombatStrategy();
                else if (encounteredEnemy is Goblin)
                    strategy = new MathChallengeCombatStrategy();
                else if (encounteredEnemy is GiantSpider)
                    strategy = new RPSLizardSpockCombatStrategy();
                else
                    strategy = new RandomGuessCombatStrategy();

                bool result = strategy.Fight(Player, encounteredEnemy, EventManager);
                if (result)
                {
                    EventManager.Notify($"You defeated the {encounteredEnemy.Name}.");
                    Enemies.Remove(encounteredEnemy);
                    targetTile.EnemyOnTile = null;
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    EventManager.Notify($"You were defeated by the {encounteredEnemy.Name}. Game Over!");
                    GameOver = true;
                    return;
                }
            }
        }

        private void DisplayInventory()
        {
            Console.Write("Inventory: ");
            if (Player.Inventory.Count == 0)
            {
                Console.Write("Empty");
            }
            else
            {
                foreach (var item in Player.Inventory)
                {
                    Console.Write(item.Name + " ");
                }
            }
            Console.WriteLine("\n");
        }

        private void DisplayLegend()
        {
            Console.WriteLine("Symbol Legend:");
            Console.WriteLine("P : Player");
            Console.WriteLine("? : Undiscovered Tile");
            Console.WriteLine(". : Land Tile");
            Console.WriteLine("~ : Ocean Tile");
            Console.WriteLine("F : Forest Tile");
            Console.WriteLine("D : Desert Tile");
            Console.WriteLine("C : Cave Tile");
            Console.WriteLine("B : Boat");
            Console.WriteLine("T : Treasure");
            Console.WriteLine("X : Axe");
            Console.WriteLine("L : Torch");
            Console.WriteLine("W : Drinking Water");
            Console.WriteLine("M : Megalodon");
            Console.WriteLine("S : Giant Spider");
            Console.WriteLine("Q : Sphinx");
            Console.WriteLine("G : Goblin");
            Console.WriteLine();
        }

    }
}