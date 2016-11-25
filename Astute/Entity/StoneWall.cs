namespace Astute.Entity
{
    public class StoneWall : IGridItem, ICollidable
    {
        public StoneWall(Point location)
        {
            Location = location;
        }

        public void Collide(Direction direction, Tank tank)
        {
        }

        public Point Location { get; }
    }
}