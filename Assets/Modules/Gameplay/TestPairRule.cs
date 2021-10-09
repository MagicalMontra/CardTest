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
        [SerializeField] private GameObject _normalRuleObject;
        [SerializeField] private GameObject[] _cardSpeicalRuleObjects;

        private ICardRule _normalRule;
        private ICardRule[] _cardRules;
        public float CalculatePair(CardData[] pair)
        {
            _normalRule ??= _normalRuleObject.GetComponent<ICardRule>();
            _cardRules ??= new ICardRule[_cardSpeicalRuleObjects.Length];

            for (int i = 0; i < _cardSpeicalRuleObjects.Length; i++)
            {
                _cardRules[i] = _cardSpeicalRuleObjects[i].GetComponent<ICardRule>();
            }

            var priority = 99;
            var mostValue = -1f;
            for (int i = 0; i < _cardRules.Length; i++)
            {
                var value = _cardRules[i].IsMatch(pair);

                if (value > 0 && value > mostValue)
                    mostValue = value;
            }

            if (mostValue <= 0)
                mostValue = _normalRule.IsMatch(pair);
            
            return mostValue;
        }
    }

    public interface IGameRule
    {
        Dictionary<IPlayer, float> DecideWinner(Dictionary<IPlayer, float> playersValue);
    }
}