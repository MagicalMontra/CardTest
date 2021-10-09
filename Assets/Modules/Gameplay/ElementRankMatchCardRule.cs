using System.Linq;
using Modules.Card;
using Modules.Infastructure;
using UnityEngine;

namespace Modules.Gameplay
{
    public class ElementRankMatchCardRule : MonoBehaviour, ICardRule
    {

        [SerializeField] private float _value;

        public float IsMatch(CardData[] commitCards)
        {
            var resultRank = 0f;

            for (int i = 0; i < commitCards.Length; i++)
            {
                int.TryParse(commitCards[i].rank, out var number);
                resultRank += number;
            }
            
            var elementRankMatch = commitCards.All(card => card.element == commitCards[0].element && card.rank == commitCards[0].rank);
            
            return elementRankMatch ? _value : 0;
        }
    }
}