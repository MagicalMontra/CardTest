using System.Collections.Generic;
using UnityEngine;

namespace Modules.Card
{
    public class TopDownCardDrawer : MonoBehaviour, ICardDrawer
    {
        public CardData Draw(List<CardData> cards)
        {
            var drawnCard = cards[cards.Count - 1].Clone();
            cards.RemoveAt(cards.Count - 1);
            return drawnCard;
        }
    }
}