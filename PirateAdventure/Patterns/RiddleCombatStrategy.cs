using System;
using System.Collections.Generic;
using PirateAdventure.Characters;
using PirateAdventure.Game;

namespace PirateAdventure.Patterns
{
    public class RiddleCombatStrategy : ICombatStrategy
    {
        private List<(string question, string answer)> riddles = new List<(string, string)>
        {
            ("What has keys but can't open locks?", "piano"),
            ("What gets wetter as it dries?", "towel"),
            ("What has a head and a tail but no body?", "coin"),
            ("What has to be broken before you can use it?", "egg")
        };

        public bool Fight(Player player, Enemy enemy, GameEventManager eventManager)
        {
            Random rand = new Random();
            var (question, answer) = riddles[rand.Next(riddles.Count)];

            eventManager.Notify(enemy.Name + " asks: " + question);
            eventManager.Notify("Your answer (one word): ");
            string playerAnswer = Console.ReadLine()?.Trim().ToLower();

            if (playerAnswer == answer)
            {
                eventManager.Notify("Correct! You answered correctly.");
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
