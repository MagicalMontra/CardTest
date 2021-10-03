using System.Collections;

namespace Modules.Card
{
    public class CardData
    {
        public string element;
        public string color;
        public string rank;

        public CardData(string element, string color, string rank)
        {
            this.element = element;
            this.color = color;
            this.rank = rank;
        }
        private CardData(CardData cardData)
        {
            element = cardData.element;
            color = cardData.color;
            rank = cardData.rank;
        }
        public CardData Clone()
        {
            return new CardData(this);
        }
    }
}