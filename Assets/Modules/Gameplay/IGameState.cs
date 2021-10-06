namespace Modules.Gameplay
{
    public interface IGameState
    {
        string stateName { get; }
        void Reset();
        bool Validate();
        void Process();
    }
}