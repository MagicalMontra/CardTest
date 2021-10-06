using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Card;
using Modules.Infastructure;
using UnityEngine;

namespace Modules.Gameplay
{
    public class TestPairRule : MonoBehaviour, IPairRule
    {
        [SerializeField] private GameObject[] _cardRuleObjects;

        private ICardRule[] _cardRules;
        public float CalculatePair(CardData[] pair)
        {
            _cardRules ??= new ICardRule[_cardRuleObjects.Length];

            for (int i = 0; i < _cardRuleObjects.Length; i++)
            {
                _cardRules[i] = _cardRuleObjects[i].GetComponent<ICardRule>();
            }
            
            var mostValue = _cardRules.Max(rule => rule.IsMatch(pair));
            return mostValue;
        }
    }

    public interface IGameRule
    {
        Dictionary<IPlayer, float> DecideWinner(Dictionary<IPlayer, float> playersValue);
    }
}