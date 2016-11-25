namespace Astute.Entity
{
    public class Water : IGridItem, ICollidable
    {
        public Water(Point location)
        {
            Location = location;
        }

        public void Collide(Direction direction, Tank tank)
        {
            tank.Health--;
        }

        public Point Location { get; }
    }
}