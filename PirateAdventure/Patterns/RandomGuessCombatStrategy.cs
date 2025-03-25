using PirateAdventure.Characters;
using PirateAdventure.Game;

namespace PirateAdventure.Patterns
{
    public class RandomGuessCombatStrategy : ICombatStrategy
    {
        public bool Fight(Player player, Enemy enemy, GameEventManager eventManager)
        {
            Random rand = new Random();
            eventManager.Notify("Enter your guess (0-10): ");
            int playerGuess;
            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out playerGuess) && playerGuess >= 0 && playerGuess <= 10)
                    break;
                else
                    eventManager.Notify("Invalid number. Please enter a number between 0 and 10.");
            }
            int enemyGuess = rand.Next(0, 11);
            eventManager.Notify($"You guessed {playerGuess}, enemy guessed {enemyGuess}.");

            int target = rand.Next(0, 11);
            eventManager.Notify($"Target number is {target}.");
            int playerDiff = Math.Abs(target - playerGuess);
            int enemyDiff = Math.Abs(target - enemyGuess);

            return playerDiff <= enemyDiff;
        }
    }
}
