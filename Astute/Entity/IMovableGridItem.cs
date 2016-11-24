namespace Astute.Entity
{
    public interface IMovableGridItem : IGridItem
    {
        new Point Location { get; set; }
    }
}