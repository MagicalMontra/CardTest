using Modules.Infastructure;

namespace Modules.Gameplay
{
    public interface IAiBrain
    {
        CardData[] FindPair(CardData[] handCards);
    }
}