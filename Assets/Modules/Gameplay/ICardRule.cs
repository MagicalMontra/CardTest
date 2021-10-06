using Modules.Infastructure;

namespace Modules.Gameplay
{
    public interface ICardRule
    {
        float IsMatch(CardData[] commitCards);
    }
}