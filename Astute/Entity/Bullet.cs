namespace Astute.Entity
{
    public struct Bullet : IMovableGridItem, ICollidable
    {
        public Point Location { get; set; }

        public void Collide(Direction direction, Tank tank)
        {
        }
    }
}