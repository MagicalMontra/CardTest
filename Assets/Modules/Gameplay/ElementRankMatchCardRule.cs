using System.Linq;
using Modules.Card;
using UnityEngine;

namespace Modules.Gameplay
{
    public class ElementRankMatchCardRule : MonoBehaviour, ICardRule
    {
        [SerializeField] private float _value;
        [SerializeField] private string _rankValue;
        [SerializeField] private string _elementValue;

        public float IsMatch(CardData[] commitCards)
        {
            var resultRank = 0f;

            for (int i = 0; i < commitCards.Length; i++)
            {
                int.TryParse(commitCards[i].rank, out var number);
                resultRank += number;
            }
            
            var elementRankMatch = commitCards.All(card => card.element == _elementValue && card.rank == _rankValue);
            
            return elementRankMatch ? _value : resultRank;
        }
    }
}