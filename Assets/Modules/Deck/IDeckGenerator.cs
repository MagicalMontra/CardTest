using System.Collections.Generic;
using Modules.Infastructure;

namespace Modules.Card
{
    public interface IDeckGenerator
    {
        List<CardData> Create();
    }
}