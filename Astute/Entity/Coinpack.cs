namespace Astute.Entity
{
    public class Coinpack : IGridItem, ICollidable, ITimeVariant
    {
        public Coinpack(Point location, int value, int maxTimeToDisappear)
        {
            Location = location;
            CoinValue = value;
            MaxTimeToDisappear = maxTimeToDisappear;
            TimeToDisappear = maxTimeToDisappear;
        }

        public int CoinValue { get; }

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