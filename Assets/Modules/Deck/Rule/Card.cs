using System.Collections;

namespace Modules.Card
{
    public class Card
    {
        public string suit;
        public string color;
        public string rank;

        public Card(string suit, string color, string rank)
        {
            this.suit = suit;
            this.color = color;
            this.rank = rank;
        }
    }
}