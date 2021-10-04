using Modules.Card;

namespace Modules.Gameplay
{
    public interface IGameRule
    {
        float CalculatePair(CardData[] pair);
    }
}