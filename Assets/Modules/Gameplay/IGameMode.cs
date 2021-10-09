using Cysharp.Threading.Tasks;

namespace Modules.Gameplay
{
    public interface IGameMode
    {
        int turnCount { get; }
        void StartGame();
        void AssignState(IGameState nextState);
    }
}