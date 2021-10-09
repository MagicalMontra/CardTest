using System.Linq;
using Modules.Card;
using Modules.Infastructure;
using UnityEngine;

namespace Modules.Gameplay
{
    public class ColorRankMatchCardRule : MonoBehaviour, ICardRule
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
            
            var colorRankMatch = commitCards.All(card => card.color == commitCards[0].color && card.rank == commitCards[0].rank);
            
            return colorRankMatch ? resultRank + _value : 0;
        }
    }
}