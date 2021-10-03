using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Modules.Card
{
    public class NoDuplicationDeckGenerator : MonoBehaviour, IDeckGenerator
    {
        [SerializeField] private CardSuitSettings _suitSettings;
        [SerializeField] private CardRankSettings _rankSettings;
        [SerializeField] private CardColorSettings _colorSettings;
        public Card[] Create()
        {
            List<Card> _cards = new List<Card>();
            
            for (int i = 0; i < _suitSettings.suits.Count; i++)
            {
                string selectSuit = _suitSettings.suits[i];
                string selectRank;
                string selectColor;

                for (int j = 0; j < _rankSettings.ranks.Count; j++)
                {
                    selectRank = _rankSettings.ranks[j];

                    for (int k = 0; k < _colorSettings.colors.Count; k++)
                    {
                        selectColor = _colorSettings.colors[k];
                        _cards.Add(new Card(selectSuit, selectColor, selectRank));
                    }
                }
            }

            ValidateDuplicate(_cards.ToArray(), _cards.Count);

            return _cards.ToArray();
        }
        private void ValidateDuplicate(Card[] cards, int size)
        {
            int i;
            string debug = "The repeating" + " elements are : ";
            int current = 1;
 
            for (i = 0; i < size; i++)
            {
                if (current >= size)
                    break;

                var sameSuit = cards[current].suit == cards[i].suit;
                var sameColor = cards[current].color == cards[i].color;
                var sameRank = cards[current].rank == cards[i].rank;
                
                if (sameSuit && sameColor && sameRank)
                    Debug.LogError(debug + i);
                
                current++;
            }
        }
    }
}