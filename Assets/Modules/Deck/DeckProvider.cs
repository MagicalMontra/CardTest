using System.Collections.Generic;
using Modules.Infastructure;

namespace Modules.Card
{
    public interface ICardDrawer
    {
        CardData Draw(List<CardData> cards);
    }
}