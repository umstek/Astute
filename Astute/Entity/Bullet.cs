namespace Astute.Entity
{
    public class Bullet : IMovableGridItem, ICollidable
    {
        public void Collide(Direction direction, Tank tank)
        {
        }

        public Point Location { get; set; }
    }
}