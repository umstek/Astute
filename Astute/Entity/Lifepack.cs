namespace Astute.Entity
{
    public class Lifepack : IGridItem, ICollidable, ITimeVariant
    {
        public Lifepack(Point location, int healthValue, int maxTimeToDisappear)
        {
            Location = location;
            HealthValue = healthValue;
            MaxTimeToDisappear = maxTimeToDisappear;
            TimeToDisappear = maxTimeToDisappear;
        }

        public int HealthValue { get; }

        public void Collide(Direction direction, Tank tank)
        {
        }

        public Point Location { get; }
        public int MaxTimeToDisappear { get; }
        public int TimeToDisappear { get; set; }

        public bool Tick()
        {
            return --TimeToDisappear == 0;
        }
    }
}