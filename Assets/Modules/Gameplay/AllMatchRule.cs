using System.Linq;
using Modules.Card;
using Modules.Infastructure;
using UnityEngine;

namespace Modules.Gameplay
{
    public class AllMatchRule : MonoBehaviour, ICardRule
    {
        [SerializeField] private string _rankValue;
        [SerializeField] private string _colorValue;
        [SerializeField] private string _elementValue;

        public float IsMatch(CardData[] commitCards)
        {
            var allMatch = commitCards.Any(card => card.element == _elementValue && card.rank == _rankValue && card.color == _colorValue);
            
            if (allMatch)
                return float.PositiveInfinity;

            return 0;
        }
    }
}