namespace Astute.Entity
{
    public interface ITimeVariant
    {
        int MaxTimeToDisappear { get; }
        int TimeToDisappear { get; set; }
        bool Tick();
    }
}