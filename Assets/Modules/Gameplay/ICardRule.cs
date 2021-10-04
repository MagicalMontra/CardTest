using Modules.Card;

namespace Modules.Gameplay
{
    public interface ICardRule
    {
        float IsMatch(CardData[] commitCards);
    }
}