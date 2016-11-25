namespace Astute.Entity
{
    public class BrickWall : IGridItem, ICollidable, IShootable
    {
        public BrickWall(int maxHealth, Point location)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            Location = location;
        }

        public int MaxHealth { get; }
        public int Health { get; set; }

        public void Collide(Direction direction, Tank tank)
        {
        }

        public Point Location { get; }

        public bool Shoot() => Health-- == 0;
    }
}