using System.Collections.Generic;

namespace Modules.Card
{
    public interface ICardDrawer
    {
        CardData Draw(List<CardData> cards);
    }
}