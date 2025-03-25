using PirateAdventure.Characters;
using PirateAdventure.Game;

namespace PirateAdventure.Patterns
{
    public interface ICombatStrategy
    {
        bool Fight(Player player, Enemy enemy, GameEventManager eventManager);
    }
}
