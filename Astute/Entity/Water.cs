namespace Astute.Entity
{
    public struct Water : IGridItem, ICollidable
    {
        public Water(Point location)
        {
            Location = location;
        }

        public Point Location { get; }

        public void Collide(Direction direction, Tank tank)
        {
        }
    }
}