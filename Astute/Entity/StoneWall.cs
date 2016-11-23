namespace Astute.Entity
{
    public struct StoneWall : IGridItem, ICollidable
    {
        public StoneWall(Point location)
        {
            Location = location;
        }

        public Point Location { get; }

        public void Collide(Direction direction, Tank tank)
        {
        }
    }
}