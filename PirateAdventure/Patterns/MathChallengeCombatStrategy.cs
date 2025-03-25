using PirateAdventure.Characters;
using PirateAdventure.Game;

namespace PirateAdventure.Patterns
{
    public class MathChallengeCombatStrategy : ICombatStrategy
    {
        private List<(string question, int answer)> mathChallenges = new List<(string, int)>
        {
            ("What is 13 * 93?", 1209),
            ("What is 7 + 5 * 3?", 22),
            ("What is 144 / 12?", 12),
            ("What is 15 - 3?", 12)
        };

        public bool Fight(Player player, Enemy enemy, GameEventManager eventManager)
        {
            Random rand = new Random();
            var (question, answer) = mathChallenges[rand.Next(mathChallenges.Count)];

            eventManager.Notify(enemy.Name + " challenges you with a math problem:");
            eventManager.Notify(question);
            eventManager.Notify("Your answer: ");
            string playerInput = Console.ReadLine()?.Trim();
            if (!int.TryParse(playerInput, out int playerAnswer))
            {
                eventManager.Notify("Invalid input! You lose the challenge.");
                return false;
            }

            eventManager.Notify($"You answered: {playerAnswer}");
            if (playerAnswer == answer)
            {
                eventManager.Notify("Correct! You solved the math challenge.");
                return true;
            }
            else
            {
                eventManager.Notify("Incorrect! The correct answer was: " + answer);
                return false;
            }
        }
    }
}
