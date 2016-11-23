namespace Astute.Entity
{
    public struct Coinpack : IGridItem, ICollidable, ITimeVariant
    {
        public Coinpack(Point location, int value, int maxTimeToDisappear)
        {
            Location = location;
            CoinValue = value;
            MaxTimeToDisappear = maxTimeToDisappear;
            TimeToDisappear = maxTimeToDisappear;
        }

        public Point Location { get; }
        public int CoinValue { get; }
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