namespace Astute.Entity
{
    public class Lifepack : IGridItem, ITimeVariant
    {
        private const int MaxHealth = 20;

        public Lifepack(Point location, int healthValue, int timeToDisappear)
        {
            Location = location;
            HealthValue = healthValue;
            TimeToDisappear = timeToDisappear;
        }

        public Lifepack(Point location, int timeToDisappear) : this(location, MaxHealth, timeToDisappear)
        {
        }

        public int HealthValue { get; }

        public Point Location { get; }
        public int TimeToDisappear { get; set; }
    }
}