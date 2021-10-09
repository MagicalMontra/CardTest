using Cysharp.Threading.Tasks;
using Modules.Infastructure;

namespace Modules.Gameplay
{
    public interface IPlayer
    {
        string id { get; }
        bool commit { get; }
        int cardCount { get; }
        ILifePoint lifePoint { get; }
        void AddCard();
        void OnGameStart();
        void OnRoundStart();
        UniTask<CardData[]> Process();
    }
}