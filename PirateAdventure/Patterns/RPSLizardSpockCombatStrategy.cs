using System;
using PirateAdventure.Characters;
using PirateAdventure.Game;

namespace PirateAdventure.Patterns
{
    public class RPSLizardSpockCombatStrategy : ICombatStrategy
    {
        public bool Fight(Player player, Enemy enemy, GameEventManager eventManager)
        {
            string[] options = { "rock", "paper", "scissors", "lizard", "spock" };
            Random rand = new Random();
            string enemyChoice = options[rand.Next(options.Length)];

            eventManager.Notify("Choose your move: Rock, Paper, Scissors, Lizard, or Spock?");
            string playerChoice = Console.ReadLine()?.Trim().ToLower();

            eventManager.Notify($"You chose {playerChoice}, enemy chose {enemyChoice}.");

            if (playerChoice == enemyChoice)
            {
                eventManager.Notify("It's a tie! Try again.");
                return Fight(player, enemy, eventManager);
            }

            bool win = false;
            switch (playerChoice)
            {
                case "rock":
                    if (enemyChoice == "scissors" || enemyChoice == "lizard")
                        win = true;
                    break;
                case "paper":
                    if (enemyChoice == "rock" || enemyChoice == "spock")
                        win = true;
                    break;
                case "scissors":
                    if (enemyChoice == "paper" || enemyChoice == "lizard")
                        win = true;
                    break;
                case "lizard":
                    if (enemyChoice == "spock" || enemyChoice == "paper")
                        win = true;
                    break;
                case "spock":
                    if (enemyChoice == "scissors" || enemyChoice == "rock")
                        win = true;
                    break;
                default:
                    eventManager.Notify("Invalid move, try again.");
                    return Fight(player, enemy, eventManager);
            }

            if (win)
            {
                eventManager.Notify("You win the round!");
                return true;
            }
            else
            {
                eventManager.Notify("You lose the round!");
                return false;
            }
        }
    }
}
