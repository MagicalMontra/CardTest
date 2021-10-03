using System.Collections.Generic;
using Modules.Player;
using UnityEngine;

namespace Modules.Card
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private GameObject _cardDrawerObject;
        [SerializeField] private GameObject _generatorObject;
        
        private ICardDrawer _cardDrawer;
        private IDeckGenerator _deckGenerator;

        private List<CardData> _cards = new List<CardData>();

        public void TryCreateDeck()
        {
            _deckGenerator ??= _generatorObject.GetComponent<IDeckGenerator>();
            _cards = _deckGenerator.Create();
        }
        public CardData TryDraw(ILifePoint lifePoint)
        {
            lifePoint.Modify(-1);
            return _cardDrawer.Draw(_cards);
        }
    }
}