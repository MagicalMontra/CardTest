using Modules.Infastructure;
using UnityEngine;

namespace Modules.Gameplay
{
    public class FallbackCardRule : MonoBehaviour, ICardRule
    {
        public float IsMatch(CardData[] commitCards)
        {
            var resultRank = 0f;

            for (int i = 0; i < commitCards.Length; i++)
            {
                int.TryParse(commitCards[i].rank, out var number);
                resultRank += number;
            }

            return resultRank;
        }
    }
}