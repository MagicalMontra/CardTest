using System.Linq;
using Modules.Card;
using UnityEngine;

namespace Modules.Gameplay
{
    public class ColorRankMatchCardRule : MonoBehaviour, ICardRule
    {
        [SerializeField] private float _value;
        [SerializeField] private string _rankValue;
        [SerializeField] private string _colorValue;

        public float IsMatch(CardData[] commitCards)
        {
            var resultRank = 0f;

            for (int i = 0; i < commitCards.Length; i++)
            {
                int.TryParse(commitCards[i].rank, out var number);
                resultRank += number;
            }
            
            var colorRankMatch = commitCards.All(card => card.color == _colorValue && card.rank == _rankValue);
            
            return colorRankMatch ? resultRank + _value : resultRank;
        }
    }
}