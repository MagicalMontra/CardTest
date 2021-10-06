namespace Modules.Infastructure
{
    public interface ILifePoint
    {
        float value { get; }
        void Modify(float value);
    }
}
