namespace Astute.Entity
{
    public struct Tank : IGridItem, ICollidable, IShootable
    {
        public Tank(Point location)
        {
            Location = location;
        }

        public Point Location { get; }

        public void Collide(Direction direction, Tank tank)
        {
        }

        public bool Shoot()
        {
            return false;
        }
    }
}