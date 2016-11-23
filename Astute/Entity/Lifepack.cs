namespace Astute.Entity
{
    public struct Lifepack : IGridItem, ICollidable, ITimeVariant
    {
        public Lifepack(Point location, int healthValue, int maxTimeToDisappear)
        {
            Location = location;
            HealthValue = healthValue;
            MaxTimeToDisappear = maxTimeToDisappear;
            TimeToDisappear = maxTimeToDisappear;
        }

        public Point Location { get; }
        public int HealthValue { get; }
        public int MaxTimeToDisappear { get; }
        public int TimeToDisappear { get; set; }

        public void Collide(Direction direction, Tank tank)
        {
        }

        public void Tick()
        {
        }
    }
}