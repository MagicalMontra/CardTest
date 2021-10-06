using System.Linq;
using Modules.Card;
using Modules.Infastructure;
using UnityEngine;

namespace Modules.Gameplay
{
    public class ElementColorMatchCardRule : MonoBehaviour, ICardRule
    {
        [SerializeField] private float _multipier = 2f;
        [SerializeField] private string _colorValue;
        [SerializeField] private string _elementValue;

        public float IsMatch(CardData[] commitCards)
        {
            var resultRank = 0f;

            for (int i = 0; i < commitCards.Length; i++)
            {
                int.TryParse(commitCards[i].rank, out var number);
                resultRank += number;
            }

            var elementColorMatch = commitCards.All(card => card.element == _elementValue && card.color == _colorValue);
            
            if (elementColorMatch)
                return resultRank * _multipier;

            return resultRank;
        }
    }
}