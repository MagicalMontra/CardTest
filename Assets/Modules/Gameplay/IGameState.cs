namespace Modules.Gameplay
{
    public interface IGameState
    {
        string stateName { get; }
        bool Validate();
    }
}