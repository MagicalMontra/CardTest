using Modules.Infastructure;

namespace Modules.Gameplay
{
    public interface IPairRule
    {
        float CalculatePair(CardData[] pair);
    }
}