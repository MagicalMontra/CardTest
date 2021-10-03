using System;

namespace Modules.Card
{
    [Serializable]
    public class CardData
    {
        public CardSuitData suit;
        public CardColorData color;
        public CardRankData rank;
    }
}