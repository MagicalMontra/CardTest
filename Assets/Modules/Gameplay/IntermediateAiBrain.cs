using Modules.Infastructure;
using UnityEngine;

namespace Modules.Gameplay
{
    public class IntermediateAiBrain : MonoBehaviour, IAiBrain
    {
        [SerializeField] private GameObject _gameRuleObject;

        private IPairRule _pairRule;

        private void Awake()
        {
            _pairRule ??= _gameRuleObject.GetComponent<IPairRule>();
        }
        public CardData[] FindPair(CardData[] handCards)
        {
            if (handCards.Length <= 2)
                return handCards;

            CardData[] selectedPair = new CardData[2];
            CardData[] candidatePair = new CardData[2];

            selectedPair[0] = handCards[0];
            selectedPair[1] = handCards[1];
            
            float previousValue = -1f;
            
            for (int i = 0; i < handCards.Length; i++)
            {
                candidatePair[0] = handCards[i];
                
                for (int j = 1; j < handCards.Length; j++)
                {
                    candidatePair[1] = handCards[j];
                    var currentValue = _pairRule.CalculatePair(candidatePair);

                    if (currentValue < previousValue) 
                        continue;
                    
                    selectedPair = candidatePair;
                    previousValue = currentValue;
                }
            }

            return selectedPair;
        }
    }
}