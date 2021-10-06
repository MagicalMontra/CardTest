using UnityEngine;
using System.Collections.Generic;
using Modules.Infastructure;

namespace Modules.Card
{
    public class NoDuplicationDeckGenerator : MonoBehaviour, IDeckGenerator
    {
        [SerializeField] private CardElementSettings _elementSettings;
        [SerializeField] private CardRankSettings _rankSettings;
        [SerializeField] private CardColorSettings _colorSettings;
        public List<CardData> Create()
        {
            List<CardData> _cards = new List<CardData>();
            
            for (int i = 0; i < _elementSettings.suits.Count; i++)
            {
                string selectSuit = _elementSettings.suits[i];
                string selectRank;
                string selectColor;

                for (int j = 0; j < _rankSettings.ranks.Count; j++)
                {
                    selectRank = _rankSettings.ranks[j];

                    for (int k = 0; k < _colorSettings.colors.Count; k++)
                    {
                        selectColor = _colorSettings.colors[k];
                        _cards.Add(new CardData(selectSuit, selectColor, selectRank));
                    }
                    
                }
                
            }

            ValidateDuplicate(_cards, _cards.Count);
            return Shuffle(_cards);
        }
        private void ValidateDuplicate(List<CardData> cards, int size)
        {
            int i;
            string debug = "The repeating" + " elements are : ";
            int current = 1;
 
            for (i = 0; i < size; i++)
            {
                if (current >= size)
                    break;

                var sameSuit = cards[current].element == cards[i].element;
                var sameColor = cards[current].color == cards[i].color;
                var sameRank = cards[current].rank == cards[i].rank;
                
                if (sameSuit && sameColor && sameRank)
                    Debug.LogError(debug + i);
                
                current++;
            }
        }
        private List<CardData> Shuffle(List<CardData> cards)
        {
            int n = cards.Count;
            while (n > 1)
            {
                int k = Random.Range(0, n);
                n--;
                (cards[n], cards[k]) = (cards[k], cards[n]);
            }

            return cards;
        }
    }
}