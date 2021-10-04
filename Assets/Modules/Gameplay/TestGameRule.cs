using System.Linq;
using Modules.Card;
using UnityEngine;

namespace Modules.Gameplay
{
    public class TestGameRule : MonoBehaviour, IGameRule
    {
        [SerializeField] private GameObject[] _cardRuleObjects;

        private ICardRule[] _cardRules;

        public float CalculatePair(CardData[] pair)
        {
            var mostValue = _cardRules.Max(rule => rule.IsMatch(pair));
            return mostValue;
        }
    }
}