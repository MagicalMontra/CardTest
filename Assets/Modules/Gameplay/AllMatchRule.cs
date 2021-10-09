using System.Linq;
using Modules.Card;
using Modules.Infastructure;
using UnityEngine;

namespace Modules.Gameplay
{
    public class AllMatchRule : MonoBehaviour, ICardRule
    {
        public float IsMatch(CardData[] commitCards)
        {
            var allMatch = commitCards.All(card => card.element == commitCards[0].element && card.color == commitCards[0].color && card.rank == commitCards[0].rank);
            
            if (allMatch)
                return float.PositiveInfinity;

            return 0;
        }
    }
}