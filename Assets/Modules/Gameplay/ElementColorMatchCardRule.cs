using System.Linq;
using Modules.Card;
using Modules.Infastructure;
using UnityEngine;

namespace Modules.Gameplay
{
    public class ElementColorMatchCardRule : MonoBehaviour, ICardRule
    {

        [SerializeField] private float _multipier = 2f;

        public float IsMatch(CardData[] commitCards)
        {
            var resultRank = 0f;

            for (int i = 0; i < commitCards.Length; i++)
            {
                int.TryParse(commitCards[i].rank, out var number);
                resultRank += number;
            }

            var elementColorMatch = commitCards.All(card => card.color == commitCards[0].color && card.element == commitCards[0].element);
            
            if (elementColorMatch)
                return resultRank * _multipier;

            return 0;
        }
    }
}