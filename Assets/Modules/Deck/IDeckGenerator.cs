using System.Collections.Generic;

namespace Modules.Card
{
    public interface IDeckGenerator
    {
        List<CardData> Create();
    }
}