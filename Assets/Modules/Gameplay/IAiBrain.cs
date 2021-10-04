using Modules.Card;

namespace Modules.Gameplay
{
    public interface IAiBrain
    {
        CardData[] FindPair(CardData[] handCards);
    }
}