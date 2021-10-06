using Cysharp.Threading.Tasks;

namespace Modules.Gameplay
{
    public interface IGameState
    {
        string stateName { get; }
        void Reset();
        bool Validate();
        UniTask<bool> Process();
    }
}