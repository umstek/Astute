namespace Astute.Entity
{
    public struct BrickWall : IGridItem, ICollidable, IShootable
    {
        public BrickWall(int maxHealth, Point location)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            Location = location;
        }

        public Point Location { get; }
        public int MaxHealth { get; }
        public int Health { get; set; }

        public bool Shoot() => Health-- == 0;

        public void Collide(Direction direction, Tank tank)
        {
        }
    }
}