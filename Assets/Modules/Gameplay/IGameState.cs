using Cysharp.Threading.Tasks;

namespace Modules.Gameplay
{
    public interface IGameState
    {
        string stateName { get; }
        UniTaskVoid Process();
    }
}