using System.Collections.Generic;
using Modules.Card;
using UnityEngine;

namespace Modules.Player
{
    public abstract class BasePlayer : MonoBehaviour
    {
        [SerializeField] private GameObject _lifePointObject;
        
        private ILifePoint _lifePoint;
        private CardData[] _selectedCard = new CardData[2];
        private List<CardData> _cards = new List<CardData>();
        
        public void TryAddCard(CardData card)
        {
            if (_cards.Count >= 7)
                return;

            _cards.Add(card);
        }
        public void CommitCard(int index)
        {
            if (index > _cards.Count)
                return;

            var slotOneEmpty = _selectedCard[0] == null;
            
            if (slotOneEmpty)
            {
                _selectedCard[0] = _cards[index];
                return;
            }

            var slotTwoEmpty = _selectedCard[1] == null;

            if (slotTwoEmpty)
            {
                _selectedCard[1] = _cards[index];
                return;
            }

            var bothSlotSelected = _selectedCard[0] != null && _selectedCard[1] != null;

            if (!bothSlotSelected) 
                return;
            
            _selectedCard[0] = null;
            _selectedCard[1] = null;
            CommitCard(index);
        }
    }
}